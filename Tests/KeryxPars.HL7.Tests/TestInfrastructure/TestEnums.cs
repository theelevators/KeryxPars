using System;

namespace KeryxPars.HL7.Tests.TestInfrastructure;

/// <summary>
/// Shared enums for testing across all test files.
/// Prevents duplicate enum definitions and compilation conflicts.
/// </summary>
public static class TestEnums
{
    /// <summary>
    /// Gender enum for testing.
    /// </summary>
    public enum Gender
    {
        M, // Male
        F, // Female
        O, // Other
        U  // Unknown
    }

    /// <summary>
    /// Marital status enum for testing.
    /// </summary>
    public enum MaritalStatus
    {
        S, // Single
        M, // Married
        D, // Divorced
        W  // Widowed
    }

    /// <summary>
    /// Patient class enum for testing.
    /// </summary>
    public enum PatientClass
    {
        I, // Inpatient
        O, // Outpatient
        E, // Emergency
        R  // Recurring
    }

    /// <summary>
    /// Message event type enum for testing.
    /// </summary>
    public enum EventType
    {
        A01, // Admit
        A02, // Transfer
        A03, // Discharge
        A04, // Register
        A08  // Update
    }
}
