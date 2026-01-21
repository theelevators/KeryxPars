using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Demonstrates PRIORITY-BASED FALLBACK - multiple [HL7Field] attributes!
/// </summary>
[HL7Message("ADT^A01")]
public class PatientWithPriorityFallback
{
    [HL7Field("PID.3")]
    public string MRN { get; set; } = string.Empty;

    [HL7Field("PID.5.1")]
    public string LastName { get; set; } = string.Empty;

    // ========== PRIORITY FALLBACK EXAMPLES ==========

    /// <summary>
    /// Phone number with 3-level priority fallback:
    /// 1. Home phone (PID.13) - Priority 0 (default)
    /// 2. Work phone (PID.14) - Priority 1
    /// 3. Mobile phone (PID.40) - Priority 2
    /// </summary>
    [HL7Field("PID.13", Priority = 0)]  // Highest priority (lowest number)
    [HL7Field("PID.14", Priority = 1)]
    [HL7Field("PID.40", Priority = 2)]  // Lowest priority
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// ID with multi-source fallback:
    /// 1. SSN (PID.19) - Priority 0
    /// 2. Driver's License (PID.20) - Priority 1
    /// 3. Passport (PID.21.1) - Priority 2
    /// 4. Patient ID (PID.2) - Priority 3
    /// </summary>
    [HL7Field("PID.19", Priority = 0)]
    [HL7Field("PID.20", Priority = 1)]
    [HL7Field("PID.21.1", Priority = 2)]
    [HL7Field("PID.2", Priority = 3)]
    public string? PatientIdentifier { get; set; }

    /// <summary>
    /// Doctor name with cross-segment fallback:
    /// 1. Attending doctor (PV1.7) - Priority 0
    /// 2. Admitting doctor (PV1.17) - Priority 1  
    /// 3. Consulting doctor (PV1.9) - Priority 2
    /// </summary>
    [HL7Field("PV1.7", Priority = 0)]
    [HL7Field("PV1.17", Priority = 1)]
    [HL7Field("PV1.9", Priority = 2)]
    public string? PrimaryDoctor { get; set; }
}
