using System;
using KeryxPars.HL7.Mapping;
using static KeryxPars.HL7.Tests.TestInfrastructure.TestEnums;

namespace KeryxPars.HL7.Tests.TestInfrastructure;

/// <summary>
/// Address model using HL7ComplexType (from v0.4.0).
/// </summary>
[HL7ComplexType(BaseFieldPath = "PID.11")]
public class TestAddress
{
    [HL7Field("1")]
    public string? Street { get; set; }

    [HL7Field("3")]
    public string? City { get; set; }

    [HL7Field("4")]
    public string? State { get; set; }

    [HL7Field("5")]
    public string? ZipCode { get; set; }
}

/// <summary>
/// Shared test models used across multiple test files.
/// Prevents duplicate model definitions and maintains consistency.
/// </summary>
public static class TestModels
{
    /// <summary>
    /// Simple patient model with string properties only.
    /// </summary>
    public class SimplePatient
    {
        public string? PatientId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }

    /// <summary>
    /// Patient model with DateTime properties.
    /// </summary>
    public class PatientWithDateTime
    {
        public string? PatientId { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? MessageDateTime { get; set; }
        public DateTime? AdmitDateTime { get; set; }
    }

    /// <summary>
    /// Patient model with enum properties.
    /// </summary>
    public class PatientWithEnums
    {
        public string? PatientId { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public PatientClass? PatientClass { get; set; }
    }

    /// <summary>
    /// Patient model with all types (string, DateTime, enum).
    /// </summary>
    public class CompletePatient
    {
        public string? PatientId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }

    /// <summary>
    /// Patient model for testing default values.
    /// </summary>
    public class PatientWithDefaults
    {
        public string? Status { get; set; }
        public string? DefaultField { get; set; }
    }

    /// <summary>
    /// Patient model for testing required fields.
    /// </summary>
    public class PatientWithRequired
    {
        public string? PatientId { get; set; }
        public string? LastName { get; set; }
    }

    /// <summary>
    /// Visit/encounter information model.
    /// </summary>
    public class VisitInfo
    {
        public PatientClass? PatientClass { get; set; }
        public string? AssignedLocation { get; set; }
        public string? PointOfCare { get; set; }
        public string? Room { get; set; }
        public string? Bed { get; set; }
        public string? VisitNumber { get; set; }
        public DateTime? AdmitDateTime { get; set; }
    }

    /// <summary>
    /// Patient model with nested complex types.
    /// </summary>
    public class PatientWithAddress
    {
        public string? PatientId { get; set; }
        public string? LastName { get; set; }
        public TestAddress? HomeAddress { get; set; }
    }

    /// <summary>
    /// Patient model with multiple complex types.
    /// </summary>
    public class PatientWithComplexTypes
    {
        public string? PatientId { get; set; }
        public string? LastName { get; set; }
        public TestAddress? HomeAddress { get; set; }
        public VisitInfo? Visit { get; set; }
    }
}
