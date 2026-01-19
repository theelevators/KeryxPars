using System.Diagnostics;
using KeryxPars.MessageViewer.Core.Interfaces;
using KeryxPars.MessageViewer.Core.Models;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Serialization.Configuration;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.MessageViewer.Core.Services;

public class MessageParserService : IMessageParserService
{
    public Task<ParsedMessageResult> ParseAsync(string rawMessage, SerializerOptions? options = null)
    {
        return Task.Run(() =>
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                // Configure options to use comprehensive message type
                var opts = options ?? SerializerOptions.Default;
                if (opts.MessageType != typeof(HL7ComprehensiveMessage))
                {
                    opts = new SerializerOptions
                    {
                        SegmentRegistry = opts.SegmentRegistry,
                        IgnoreUnknownSegments = opts.IgnoreUnknownSegments,
                        ErrorHandling = opts.ErrorHandling,
                        ValidationStrategy = opts.ValidationStrategy,
                        UseStringPooling = opts.UseStringPooling,
                        InitialBufferSize = opts.InitialBufferSize,
                        MessageType = typeof(HL7ComprehensiveMessage),
                        OrderGrouping = opts.OrderGrouping
                    };
                }

                // Parse as comprehensive message to capture all possible segments
                var result = HL7Serializer.Deserialize<HL7ComprehensiveMessage>(rawMessage, opts);
                stopwatch.Stop();

                if (result.IsSuccess)
                {
                    var message = result.Value;
                    var metadata = ExtractMetadata(message, rawMessage);

                    return new ParsedMessageResult
                    {
                        IsSuccess = true,
                        Message = message,
                        Metadata = metadata,
                        ParseDuration = stopwatch.Elapsed
                    };
                }
                else
                {
                    var errors = result.Error != null 
                        ? result.Error.Select(e => e.Message).ToArray()
                        : ["Unknown error"];
                    return new ParsedMessageResult
                    {
                        IsSuccess = false,
                        Errors = errors,
                        ParseDuration = stopwatch.Elapsed
                    };
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                return new ParsedMessageResult
                {
                    IsSuccess = false,
                    Errors = [$"Parse exception: {ex.Message}"],
                    ParseDuration = stopwatch.Elapsed
                };
            }
        });
    }

    public Task<IEnumerable<MessageValidationError>> ValidateAsync(HL7DefaultMessage message)
    {
        return Task.Run(() =>
        {
            var errors = new List<MessageValidationError>();

            // Validate MSH segment (required)
            if (message.Msh == null || string.IsNullOrEmpty(message.Msh.MessageType.Value))
            {
                errors.Add(new MessageValidationError
                {
                    Severity = ValidationSeverity.Critical,
                    SegmentId = "MSH",
                    FieldIndex = 9,
                    Message = "MSH.9 (Message Type) is required",
                    Suggestion = "Add message type (e.g., ADT^A01)"
                });
            }

            if (message.Msh != null && string.IsNullOrEmpty(message.Msh.MessageControlID.Value))
            {
                errors.Add(new MessageValidationError
                {
                    Severity = ValidationSeverity.Warning,
                    SegmentId = "MSH",
                    FieldIndex = 10,
                    Message = "MSH.10 (Message Control ID) is recommended",
                    Suggestion = "Add unique message control ID"
                });
            }

            // Validate PID if present
            if (message.Pid != null)
            {
                if (message.Pid.PatientIdentifierList == null || message.Pid.PatientIdentifierList.Length == 0)
                {
                    errors.Add(new MessageValidationError
                    {
                        Severity = ValidationSeverity.Error,
                        SegmentId = "PID",
                        FieldIndex = 3,
                        Message = "PID.3 (Patient Identifier List) is required",
                        Suggestion = "Add at least one patient identifier"
                    });
                }

                if (message.Pid.PatientName == null || message.Pid.PatientName.Length == 0)
                {
                    errors.Add(new MessageValidationError
                    {
                        Severity = ValidationSeverity.Warning,
                        SegmentId = "PID",
                        FieldIndex = 5,
                        Message = "PID.5 (Patient Name) is recommended",
                        Suggestion = "Add patient name"
                    });
                }
            }

            return errors.AsEnumerable();
        });
    }

    private ParseMetadata ExtractMetadata(HL7DefaultMessage message, string rawMessage)
    {
        var metadata = new ParseMetadata();
        
        // Count segments
        var lines = rawMessage.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        metadata.SegmentCount = lines.Length;

        // Count segment frequency
        var segmentFrequency = new Dictionary<string, int>();
        foreach (var line in lines)
        {
            var segmentId = line.Split('|')[0].Trim();
            segmentFrequency[segmentId] = segmentFrequency.GetValueOrDefault(segmentId, 0) + 1;
        }
        metadata.SegmentFrequency = segmentFrequency;

        // Extract from MSH
        if (message.Msh != null)
        {
            metadata.HL7Version = message.Msh.VersionID.Value ?? "Unknown";
            metadata.MessageType = message.Msh.MessageType.Value ?? "Unknown";
        }

        // Extract from EVN
        if (message.Evn != null)
        {
            metadata.EventType = message.Evn.EventType.Value ?? "Unknown";
        }

        // Count total fields (approximate)
        metadata.FieldCount = lines.Sum(line => line.Count(c => c == '|'));

        return metadata;
    }
}

