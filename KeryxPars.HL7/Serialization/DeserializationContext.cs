using KeryxPars.Core.Models;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.Serialization.Configuration;

namespace KeryxPars.HL7.Serialization;

/// <summary>
/// Context for managing state during message deserialization.
/// </summary>
internal sealed class DeserializationContext
{
    private readonly List<HL7Error> _errors;
    private readonly SerializerOptions _options;

    public HL7Message Message { get; }
    public IReadOnlyList<HL7Error> Errors => _errors;
    public bool HasErrors => _errors.Count > 0;

    public DeserializationContext(SerializerOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _errors = [];
        Message = new HL7Message();
    }

    /// <summary>
    /// Adds an error to the context.
    /// </summary>
    public void AddError(HL7Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        _errors.Add(error);
    }

    /// <summary>
    /// Adds an error with the specified details.
    /// </summary>
    public void AddError(ErrorSeverity severity, ErrorCode code, string message)
    {
        _errors.Add(new HL7Error(severity, code, message));
    }

    /// <summary>
    /// Adds multiple errors to the context.
    /// </summary>
    public void AddErrors(IEnumerable<HL7Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        _errors.AddRange(errors);
    }

    /// <summary>
    /// Adds a warning to the context.
    /// </summary>
    public void AddWarning(string message)
    {
        _errors.Add(new HL7Error(ErrorSeverity.Warning, ErrorCode.ApplicationInternalError, message));
    }

    /// <summary>
    /// Converts the context to a serializer result.
    /// </summary>
    public Result<HL7Message, HL7Error[]> ToResult()
    {
        if (_errors.Count > 0)
        {
            // Add errors to the message for ACK generation
            foreach (var error in _errors)
            {
                Message.Errors.Add(new ERR
                {
                    Severity = error.Severity.ToString(),
                    Hl7ErrorCode = $"{error.Code}",
                    DiagnosticInformation = error.Message,
                    UserMessage = error.Message
                });
            }

            return _errors.ToArray();
        }

        return Message;
    }
}