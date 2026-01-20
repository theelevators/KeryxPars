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
                // Detect the appropriate message type based on content
                var detectedType = MessageTypeDetector.DetectMessageType(rawMessage);
                
                // Configure options with detected message type
                var opts = options ?? new SerializerOptions
                {
                    MessageType = detectedType,
                    OrderGrouping = GetOrderGroupingForType(detectedType)
                };

                // If options were provided but don't specify a type, use detected type
                if (options != null && options.MessageType == null)
                {
                    opts = new SerializerOptions
                    {
                        SegmentRegistry = options.SegmentRegistry,
                        IgnoreUnknownSegments = options.IgnoreUnknownSegments,
                        ErrorHandling = options.ErrorHandling,
                        ValidationStrategy = options.ValidationStrategy,
                        UseStringPooling = options.UseStringPooling,
                        InitialBufferSize = options.InitialBufferSize,
                        MessageType = detectedType,
                        OrderGrouping = options.OrderGrouping ?? GetOrderGroupingForType(detectedType)
                    };
                }

                // Parse using comprehensive message always (for viewer compatibility)
                // But use the detected type's parser options for better grouping
                var result = HL7Serializer.Deserialize<HL7ComprehensiveMessage>(rawMessage, opts);
                
                stopwatch.Stop();

                if (result.IsSuccess && result.Value != null)
                {
                    var metadata = ExtractMetadata(result.Value, rawMessage, detectedType);

                    return new ParsedMessageResult
                    {
                        IsSuccess = true,
                        Message = result.Value,
                        Metadata = metadata,
                        ParseDuration = stopwatch.Elapsed
                    };
                }
                
                // Handle error case
                var errors = result.Error?.Select(e => e.Message).ToArray() ?? ["Parse failed"];
                return new ParsedMessageResult
                {
                    IsSuccess = false,
                    Errors = errors,
                    ParseDuration = stopwatch.Elapsed
                };


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

    private static OrderGroupingConfiguration? GetOrderGroupingForType(Type messageType)
    {
        return messageType.Name switch
        {
            nameof(PharmacyMessage) => OrderGroupingConfiguration.Medication,
            nameof(LabMessage) => OrderGroupingConfiguration.Lab,
            nameof(DietaryMessage) => OrderGroupingConfiguration.Dietary,
            _ => OrderGroupingConfiguration.None
        };
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

    private ParseMetadata ExtractMetadata(HL7DefaultMessage message, string rawMessage, Type detectedType)


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

        // Add detected message class info
        metadata.DetectedMessageClass = detectedType.Name;
        metadata.MessageClassDescription = MessageTypeDetector.GetMessageTypeDescription(detectedType);

        // Count total fields (approximate)
        metadata.FieldCount = lines.Sum(line => line.Count(c => c == '|'));

        return metadata;
    }
}


