using KeryxPars.HL7.Contracts;
using KeryxPars.MessageViewer.Core.Models;

namespace KeryxPars.MessageViewer.Core.Services;

/// <summary>
/// Service providing built-in validation templates.
/// </summary>
public static class RuleTemplateService
{
    /// <summary>
    /// Gets all built-in validation profiles.
    /// </summary>
    public static List<ValidationProfile> GetBuiltInProfiles()
    {
        return 
        [
            CreateADTProfile(),
            CreatePharmacyProfile(),
            CreateLabProfile(),
            CreateSchedulingProfile(),
            CreateFinancialProfile(),
            CreateMSHProfile(),
            CreatePIDProfile(),
            CreateORCProfile()
        ];
    }

    private static ValidationProfile CreateADTProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000001"),
        Name = "ADT Messages",
        Description = "Admission, Discharge, Transfer validation",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "MSH", Type = RuleType.RequiredSegment },
            new() { SegmentId = "EVN", Type = RuleType.RequiredSegment },
            new() { SegmentId = "PID", Type = RuleType.RequiredSegment },
            new() { SegmentId = "MSH", FieldIndex = 9, Type = RuleType.Required, CustomMessage = "Message Type is required" },
            new() { SegmentId = "MSH", FieldIndex = 10, Type = RuleType.Required, CustomMessage = "Message Control ID is required" },
            new() { SegmentId = "MSH", FieldIndex = 11, Type = RuleType.Pattern, Config = new() { ["pattern"] = @"^(P|D|T)$" }, CustomMessage = "Processing ID must be P, D, or T" },
            new() { SegmentId = "EVN", FieldIndex = 1, Type = RuleType.Required, CustomMessage = "Event Type Code is required" },
            new() { SegmentId = "PID", FieldIndex = 3, Type = RuleType.Required, CustomMessage = "Patient ID is required" },
            new() { SegmentId = "PID", FieldIndex = 5, Type = RuleType.Required, CustomMessage = "Patient Name is required" },
            new() { SegmentId = "PID", FieldIndex = 5, Type = RuleType.MaxLength, Config = new() { ["maxLength"] = 250 } },
            new() { SegmentId = "PID", FieldIndex = 7, Type = RuleType.Pattern, Config = new() { ["pattern"] = @"^\d{8}$" }, CustomMessage = "DOB must be YYYYMMDD format", Severity = ValidationSeverity.Warning }
        ]
    };

    private static ValidationProfile CreatePharmacyProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000002"),
        Name = "Pharmacy Orders",
        Description = "Medication orders & dispensing validation",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "MSH", Type = RuleType.RequiredSegment },
            new() { SegmentId = "PID", Type = RuleType.RequiredSegment },
            new() { SegmentId = "ORC", Type = RuleType.RequiredSegment },
            new() { SegmentId = "MSH", FieldIndex = 9, Type = RuleType.Required },
            new() { SegmentId = "MSH", FieldIndex = 10, Type = RuleType.Required },
            new() { SegmentId = "PID", FieldIndex = 3, Type = RuleType.Required },
            new() { SegmentId = "PID", FieldIndex = 5, Type = RuleType.Required, Config = new() { ["maxLength"] = 250 } },
            new() { SegmentId = "ORC", FieldIndex = 1, Type = RuleType.Required },
            new() { SegmentId = "ORC", FieldIndex = 1, Type = RuleType.AllowedValues, Config = new() { ["allowedValues"] = new[] { "NW", "OK", "CA", "DC", "DE", "RE", "RL", "HD" } } },
            new() { SegmentId = "ORC", FieldIndex = 2, Type = RuleType.Required, CustomMessage = "Placer Order Number is required" },
            new() { SegmentId = "RXE", FieldIndex = 2, Type = RuleType.Required, CustomMessage = "Give Code is required" },
            new() { SegmentId = "RXE", FieldIndex = 3, Type = RuleType.Required, CustomMessage = "Give Amount is required" },
            new() { SegmentId = "RXE", FieldIndex = 3, Type = RuleType.NumericRange, Config = new() { ["minValue"] = 0m }, CustomMessage = "Give Amount must be positive" },
            new() { SegmentId = "RXE", FieldIndex = 4, Type = RuleType.Required, CustomMessage = "Give Units is required" }
        ]
    };

    private static ValidationProfile CreateLabProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000003"),
        Name = "Lab Results",
        Description = "Laboratory orders & results validation",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "MSH", Type = RuleType.RequiredSegment },
            new() { SegmentId = "PID", Type = RuleType.RequiredSegment },
            new() { SegmentId = "OBR", Type = RuleType.RequiredSegment },
            new() { SegmentId = "MSH", FieldIndex = 9, Type = RuleType.Required },
            new() { SegmentId = "MSH", FieldIndex = 10, Type = RuleType.Required },
            new() { SegmentId = "PID", FieldIndex = 3, Type = RuleType.Required },
            new() { SegmentId = "PID", FieldIndex = 5, Type = RuleType.Required, Config = new() { ["maxLength"] = 250 } },
            new() { SegmentId = "OBR", FieldIndex = 4, Type = RuleType.Required, CustomMessage = "Universal Service ID is required" },
            new() { SegmentId = "OBR", FieldIndex = 7, Type = RuleType.Pattern, Config = new() { ["pattern"] = @"^\d{14}$" }, CustomMessage = "Observation Date/Time must be YYYYMMDDHHmmss", Severity = ValidationSeverity.Warning },
            new() { SegmentId = "OBX", FieldIndex = 2, Type = RuleType.AllowedValues, Config = new() { ["allowedValues"] = new[] { "NM", "ST", "TX", "FT", "CE", "CWE", "DT", "TM", "TS" } } },
            new() { SegmentId = "OBX", FieldIndex = 3, Type = RuleType.Required, CustomMessage = "Observation Identifier is required" }
        ]
    };

    private static ValidationProfile CreateSchedulingProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000004"),
        Name = "Scheduling Messages",
        Description = "Appointment scheduling validation",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "MSH", Type = RuleType.RequiredSegment },
            new() { SegmentId = "SCH", Type = RuleType.RequiredSegment },
            new() { SegmentId = "MSH", FieldIndex = 9, Type = RuleType.Required },
            new() { SegmentId = "MSH", FieldIndex = 10, Type = RuleType.Required },
            new() { SegmentId = "SCH", FieldIndex = 1, Type = RuleType.Required, CustomMessage = "Placer Appointment ID is required" },
            new() { SegmentId = "SCH", FieldIndex = 2, Type = RuleType.Required, CustomMessage = "Filler Appointment ID is required" },
            new() { SegmentId = "SCH", FieldIndex = 11, Type = RuleType.Required, CustomMessage = "Appointment Timing Quantity is required" }
        ]
    };

    private static ValidationProfile CreateFinancialProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000005"),
        Name = "Financial/Billing",
        Description = "Financial transaction validation",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "MSH", Type = RuleType.RequiredSegment },
            new() { SegmentId = "PID", Type = RuleType.RequiredSegment },
            new() { SegmentId = "FT1", Type = RuleType.RequiredSegment },
            new() { SegmentId = "MSH", FieldIndex = 9, Type = RuleType.Required },
            new() { SegmentId = "MSH", FieldIndex = 10, Type = RuleType.Required },
            new() { SegmentId = "PID", FieldIndex = 3, Type = RuleType.Required },
            new() { SegmentId = "FT1", FieldIndex = 4, Type = RuleType.Required, CustomMessage = "Transaction Date is required" },
            new() { SegmentId = "FT1", FieldIndex = 7, Type = RuleType.Required, CustomMessage = "Transaction Code is required" },
            new() { SegmentId = "FT1", FieldIndex = 12, Type = RuleType.Required, CustomMessage = "Transaction Amount is required" }
        ]
    };

    private static ValidationProfile CreateMSHProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000006"),
        Name = "MSH - Message Header",
        Description = "Message header validation rules",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "MSH", FieldIndex = 9, Type = RuleType.Required, CustomMessage = "Message Type is required" },
            new() { SegmentId = "MSH", FieldIndex = 10, Type = RuleType.Required, CustomMessage = "Message Control ID is required" },
            new() { SegmentId = "MSH", FieldIndex = 11, Type = RuleType.Pattern, Config = new() { ["pattern"] = @"^(P|D|T)$" }, CustomMessage = "Processing ID must be P, D, or T" },
            new() { SegmentId = "MSH", FieldIndex = 7, Type = RuleType.Pattern, Config = new() { ["pattern"] = @"^\d{14}$" }, CustomMessage = "Message Date/Time must be YYYYMMDDHHmmss", Severity = ValidationSeverity.Warning },
            new() { SegmentId = "MSH", FieldIndex = 3, Type = RuleType.MaxLength, Config = new() { ["maxLength"] = 227 }, CustomMessage = "Sending Application exceeds max length", Severity = ValidationSeverity.Warning }
        ]
    };

    private static ValidationProfile CreatePIDProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000007"),
        Name = "PID - Patient Identification",
        Description = "Patient identification validation rules",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "PID", FieldIndex = 3, Type = RuleType.Required, CustomMessage = "Patient ID is required" },
            new() { SegmentId = "PID", FieldIndex = 5, Type = RuleType.Required, CustomMessage = "Patient Name is required" },
            new() { SegmentId = "PID", FieldIndex = 5, Type = RuleType.MaxLength, Config = new() { ["maxLength"] = 250 } },
            new() { SegmentId = "PID", FieldIndex = 7, Type = RuleType.Pattern, Config = new() { ["pattern"] = @"^\d{8}$" }, CustomMessage = "DOB must be YYYYMMDD format", Severity = ValidationSeverity.Warning },
            new() { SegmentId = "PID", FieldIndex = 8, Type = RuleType.AllowedValues, Config = new() { ["allowedValues"] = new[] { "M", "F", "O", "U", "A", "N" } }, Severity = ValidationSeverity.Warning }
        ]
    };

    private static ValidationProfile CreateORCProfile() => new()
    {
        Id = new Guid("00000000-0000-0000-0000-000000000008"),
        Name = "ORC - Common Order",
        Description = "Common order segment validation rules",
        IsBuiltIn = true,
        Rules =
        [
            new() { SegmentId = "ORC", FieldIndex = 1, Type = RuleType.Required, CustomMessage = "Order Control is required" },
            new() { SegmentId = "ORC", FieldIndex = 1, Type = RuleType.AllowedValues, Config = new() { ["allowedValues"] = new[] { "NW", "OK", "UA", "CA", "OC", "CR", "DC", "DE", "DF", "DR", "FU", "HD", "HR", "LI", "NA", "NW", "OC", "OD", "OE", "OF", "OH", "OK", "OP", "OR", "PA", "PR", "RE", "RF", "RL", "RO", "RP", "RQ", "RR", "RU", "SC", "SN", "SR", "SS", "UA", "UC", "UD", "UF", "UH", "UM", "UN", "UP", "UR", "UX", "XO", "XR" } } },
            new() { SegmentId = "ORC", FieldIndex = 2, Type = RuleType.Required, CustomMessage = "Placer Order Number is required", Severity = ValidationSeverity.Warning },
            new() { SegmentId = "ORC", FieldIndex = 3, Type = RuleType.Required, CustomMessage = "Filler Order Number is required", Severity = ValidationSeverity.Warning }
        ]
    };
}
