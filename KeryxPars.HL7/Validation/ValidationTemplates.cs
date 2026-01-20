using KeryxPars.HL7.Contracts;

namespace KeryxPars.HL7.Validation;

/// <summary>
/// Pre-built validation rule templates for common HL7 scenarios.
/// </summary>
public static class ValidationTemplates
{
    /// <summary>
    /// Standard ADT message validation.
    /// </summary>
    public static ValidationRules ADT() => new()
    {
        RequiredSegments = ["MSH", "EVN", "PID"],
        Fields = new()
        {
            ["MSH.9"] = new() { Required = true, CustomMessage = "Message Type is required" },
            ["MSH.10"] = new() { Required = true, CustomMessage = "Message Control ID is required" },
            ["MSH.11"] = new() { Pattern = @"^(P|D|T)$", CustomMessage = "Processing ID must be P, D, or T" },
            ["EVN.1"] = new() { Required = true, CustomMessage = "Event Type Code is required" },
            ["PID.3"] = new() { Required = true, CustomMessage = "Patient ID is required" },
            ["PID.5"] = new() { Required = true, MaxLength = 250, CustomMessage = "Patient Name is required" },
            ["PID.7"] = new() { Pattern = @"^\d{8}$", CustomMessage = "DOB must be YYYYMMDD format" }
        }
    };

    /// <summary>
    /// Pharmacy order message validation.
    /// </summary>
    public static ValidationRules Pharmacy() => new()
    {
        RequiredSegments = ["MSH", "PID", "ORC"],
        Fields = new()
        {
            ["MSH.9"] = new() { Required = true },
            ["MSH.10"] = new() { Required = true },
            ["PID.3"] = new() { Required = true },
            ["PID.5"] = new() { Required = true, MaxLength = 250 },
            ["ORC.1"] = new() { Required = true, AllowedValues = ["NW", "OK", "CA", "DC", "DE", "RE", "RL", "HD"] },
            ["ORC.2"] = new() { Required = true, CustomMessage = "Placer Order Number is required" },
            ["RXE.2"] = new() { Required = true, CustomMessage = "Give Code is required" },
            ["RXE.3"] = new() { Required = true, MinValue = 0, CustomMessage = "Give Amount must be positive" },
            ["RXE.4"] = new() { Required = true, CustomMessage = "Give Units is required" }
        },
        Conditional = 
        [
            new ConditionalRule
            {
                When = "ORC.1 = NW",
                Then = new()
                {
                    ["RXE.2"] = new() { Required = true, CustomMessage = "Give Code required for new orders" },
                    ["RXE.3"] = new() { Required = true, CustomMessage = "Give Amount required for new orders" }
                }
            }
        ]
    };

    /// <summary>
    /// Lab order/result message validation.
    /// </summary>
    public static ValidationRules Lab() => new()
    {
        RequiredSegments = ["MSH", "PID", "OBR"],
        Fields = new()
        {
            ["MSH.9"] = new() { Required = true },
            ["MSH.10"] = new() { Required = true },
            ["PID.3"] = new() { Required = true },
            ["PID.5"] = new() { Required = true, MaxLength = 250 },
            ["OBR.4"] = new() { Required = true, CustomMessage = "Universal Service ID is required" },
            ["OBR.7"] = new() { Pattern = @"^\d{14}$", CustomMessage = "Observation Date/Time must be YYYYMMDDHHmmss" },
            ["OBX.2"] = new() { AllowedValues = ["NM", "ST", "TX", "FT", "CE", "CWE", "DT", "TM", "TS"] },
            ["OBX.3"] = new() { Required = true, CustomMessage = "Observation Identifier is required" }
        },
        Conditional =
        [
            new ConditionalRule
            {
                When = "OBR.25 = F",
                Then = new()
                {
                    ["OBR.22"] = new() { Required = true, CustomMessage = "Results Report Date/Time required for final results" }
                }
            },
            new ConditionalRule
            {
                When = "OBX.2 = NM",
                Then = new()
                {
                    ["OBX.6"] = new() { Required = true, CustomMessage = "Units required for numeric results" }
                }
            }
        ]
    };

    /// <summary>
    /// Scheduling message validation.
    /// </summary>
    public static ValidationRules Scheduling() => new()
    {
        RequiredSegments = ["MSH", "SCH"],
        Fields = new()
        {
            ["MSH.9"] = new() { Required = true },
            ["MSH.10"] = new() { Required = true },
            ["SCH.1"] = new() { Required = true, CustomMessage = "Placer Appointment ID is required" },
            ["SCH.2"] = new() { Required = true, CustomMessage = "Filler Appointment ID is required" },
            ["SCH.11"] = new() { Required = true, CustomMessage = "Appointment Timing Quantity is required" }
        }
    };

    /// <summary>
    /// Financial/billing message validation.
    /// </summary>
    public static ValidationRules Financial() => new()
    {
        RequiredSegments = ["MSH", "PID", "FT1"],
        Fields = new()
        {
            ["MSH.9"] = new() { Required = true },
            ["MSH.10"] = new() { Required = true },
            ["PID.3"] = new() { Required = true },
            ["FT1.4"] = new() { Required = true, CustomMessage = "Transaction Date is required" },
            ["FT1.7"] = new() { Required = true, CustomMessage = "Transaction Code is required" },
            ["FT1.12"] = new() { Required = true, CustomMessage = "Transaction Amount is required" }
        }
    };

    /// <summary>
    /// Minimal validation - only critical fields.
    /// </summary>
    public static ValidationRules Minimal() => new()
    {
        RequiredSegments = ["MSH"],
        Fields = new()
        {
            ["MSH.9"] = new() { Required = true, Severity = ValidationSeverity.Critical },
            ["MSH.10"] = new() { Required = true, Severity = ValidationSeverity.Warning }
        }
    };

    /// <summary>
    /// Strict validation - all recommended fields required.
    /// </summary>
    public static ValidationRules Strict() => new()
    {
        RequiredSegments = ["MSH", "PID"],
        Fields = new()
        {
            ["MSH.1"] = new() { Required = true },
            ["MSH.2"] = new() { Required = true },
            ["MSH.3"] = new() { Required = true, MaxLength = 227 },
            ["MSH.4"] = new() { Required = true, MaxLength = 227 },
            ["MSH.5"] = new() { Required = true, MaxLength = 227 },
            ["MSH.6"] = new() { Required = true, MaxLength = 227 },
            ["MSH.7"] = new() { Required = true, Pattern = @"^\d{14}$" },
            ["MSH.9"] = new() { Required = true },
            ["MSH.10"] = new() { Required = true, MaxLength = 199 },
            ["MSH.11"] = new() { Required = true, Pattern = @"^(P|D|T)$" },
            ["MSH.12"] = new() { Required = true },
            ["PID.3"] = new() { Required = true },
            ["PID.5"] = new() { Required = true, MaxLength = 250 },
            ["PID.7"] = new() { Required = true, Pattern = @"^\d{8}$" },
            ["PID.8"] = new() { Required = true, AllowedValues = ["M", "F", "O", "U", "A", "N"] }
        }
    };
}
