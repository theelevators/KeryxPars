using System.Data;
using Microsoft.Data.Sqlite;
using KeryxPars.MessageViewer.Core.Interfaces;
using KeryxPars.MessageViewer.Core.Models;
using KeryxPars.HL7.Definitions;
using System.Text.Json;

namespace KeryxPars.MessageViewer.Core.Services;

public class SqliteMessageRepository : IMessageRepository, IDisposable
{
    private readonly string _connectionString;
    private readonly SqliteConnection _connection;

    public SqliteMessageRepository(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}";
        _connection = new SqliteConnection(_connectionString);
        _connection.Open();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        const string createTableSql = @"
            CREATE TABLE IF NOT EXISTS Messages (
                Id TEXT PRIMARY KEY,
                RawMessage TEXT NOT NULL,
                ImportedDate TEXT NOT NULL,
                MessageType TEXT NOT NULL,
                PatientName TEXT,
                MessageControlId TEXT,
                Metadata TEXT
            );
            
            CREATE INDEX IF NOT EXISTS idx_imported_date ON Messages(ImportedDate DESC);
            CREATE INDEX IF NOT EXISTS idx_message_type ON Messages(MessageType);
            CREATE INDEX IF NOT EXISTS idx_patient_name ON Messages(PatientName);
        ";

        using var command = _connection.CreateCommand();
        command.CommandText = createTableSql;
        command.ExecuteNonQuery();
    }

    public async Task<IEnumerable<StoredMessage>> GetRecentMessagesAsync(int count)
    {
        var messages = new List<StoredMessage>();

        const string sql = @"
            SELECT Id, RawMessage, ImportedDate, MessageType, PatientName, MessageControlId, Metadata
            FROM Messages
            ORDER BY ImportedDate DESC
            LIMIT @count";

        using var command = _connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@count", count);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            messages.Add(MapFromReader(reader));
        }

        return messages;
    }

    public async Task<StoredMessage> SaveMessageAsync(string rawMessage, HL7DefaultMessage parsed)
    {
        var storedMessage = new StoredMessage
        {
            Id = Guid.NewGuid(),
            RawMessage = rawMessage,
            ImportedDate = DateTime.UtcNow,
            MessageType = parsed.Msh?.MessageType.Value ?? "Unknown",
            MessageControlId = parsed.Msh?.MessageControlID.Value ?? string.Empty,
            PatientName = GetPatientName(parsed),
            Metadata = new Dictionary<string, object>
            {
                ["HL7Version"] = parsed.Msh?.VersionID.Value ?? "Unknown",
                ["SendingApplication"] = parsed.Msh?.SendingApplication.NamespaceId.Value ?? "Unknown",
                ["ReceivingApplication"] = parsed.Msh?.ReceivingApplication.NamespaceId.Value ?? "Unknown"
            }
        };

        const string sql = @"
            INSERT INTO Messages (Id, RawMessage, ImportedDate, MessageType, PatientName, MessageControlId, Metadata)
            VALUES (@id, @rawMessage, @importedDate, @messageType, @patientName, @messageControlId, @metadata)";

        using var command = _connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@id", storedMessage.Id.ToString());
        command.Parameters.AddWithValue("@rawMessage", storedMessage.RawMessage);
        command.Parameters.AddWithValue("@importedDate", storedMessage.ImportedDate.ToString("O"));
        command.Parameters.AddWithValue("@messageType", storedMessage.MessageType);
        command.Parameters.AddWithValue("@patientName", storedMessage.PatientName);
        command.Parameters.AddWithValue("@messageControlId", storedMessage.MessageControlId);
        command.Parameters.AddWithValue("@metadata", JsonSerializer.Serialize(storedMessage.Metadata));

        await command.ExecuteNonQueryAsync();
        return storedMessage;
    }

    public async Task<bool> DeleteMessageAsync(Guid id)
    {
        const string sql = "DELETE FROM Messages WHERE Id = @id";

        using var command = _connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@id", id.ToString());

        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<StoredMessage>> SearchAsync(string query)
    {
        var messages = new List<StoredMessage>();

        const string sql = @"
            SELECT Id, RawMessage, ImportedDate, MessageType, PatientName, MessageControlId, Metadata
            FROM Messages
            WHERE RawMessage LIKE @query 
               OR MessageType LIKE @query 
               OR PatientName LIKE @query 
               OR MessageControlId LIKE @query
            ORDER BY ImportedDate DESC
            LIMIT 100";

        using var command = _connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@query", $"%{query}%");

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            messages.Add(MapFromReader(reader));
        }

        return messages;
    }

    public async Task<StoredMessage?> GetMessageByIdAsync(Guid id)
    {
        const string sql = @"
            SELECT Id, RawMessage, ImportedDate, MessageType, PatientName, MessageControlId, Metadata
            FROM Messages
            WHERE Id = @id";

        using var command = _connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@id", id.ToString());

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }

        return null;
    }

    private StoredMessage MapFromReader(SqliteDataReader reader)
    {
        var metadataJson = reader.GetString(6);
        var metadata = string.IsNullOrEmpty(metadataJson)
            ? new Dictionary<string, object>()
            : JsonSerializer.Deserialize<Dictionary<string, object>>(metadataJson) ?? new Dictionary<string, object>();

        return new StoredMessage
        {
            Id = Guid.Parse(reader.GetString(0)),
            RawMessage = reader.GetString(1),
            ImportedDate = DateTime.Parse(reader.GetString(2)),
            MessageType = reader.GetString(3),
            PatientName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
            MessageControlId = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
            Metadata = metadata
        };
    }

    private string GetPatientName(HL7DefaultMessage message)
    {
        if (message.Pid?.PatientName != null && message.Pid.PatientName.Length > 0)
        {
            var name = message.Pid.PatientName[0];
            var familyName = name.FamilyName.Surname.Value ?? string.Empty;
            var givenName = name.GivenName.Value ?? string.Empty;
            var middleName = name.SecondNames.Value ?? string.Empty;

            return $"{familyName}, {givenName} {middleName}".Trim();
        }

        return string.Empty;
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
