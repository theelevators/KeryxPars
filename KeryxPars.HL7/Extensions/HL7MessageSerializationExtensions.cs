namespace KeryxPars.HL7.Extensions;

using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Extension methods for serializing HL7 messages - follows Domain-Driven Design principles
/// by encapsulating message-specific serialization logic with the domain models.
/// </summary>
internal static class HL7MessageSerializationExtensions
{
    /// <summary>
    /// Writes all segments for this message type to the writer.
    /// </summary>
    internal static void WriteSegments(this HL7DefaultMessage message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        writer.WriteSegmentIfPresent(message.Pv1, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Pv2, in delimiters, options);
        writer.WriteSegmentCollection(message.Allergies, in delimiters, options);
        writer.WriteSegmentCollection(message.Diagnoses, in delimiters, options);
        writer.WriteSegmentCollection(message.Insurance, in delimiters, options);
        writer.WriteOrderGroups(message.Orders, in delimiters, options);
    }

    /// <summary>
    /// Writes all segments for this pharmacy message to the writer.
    /// </summary>
    internal static void WriteSegments(this PharmacyMessage message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        writer.WriteSegmentIfPresent(message.Pv1, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Pv2, in delimiters, options);
        writer.WriteSegmentCollection(message.Allergies, in delimiters, options);
        writer.WriteSegmentCollection(message.Diagnoses, in delimiters, options);
        writer.WriteSegmentCollection(message.Insurance, in delimiters, options);
        writer.WriteOrderGroups(message.Orders, in delimiters, options);
        writer.WriteSegmentCollection(message.Notes, in delimiters, options);
    }

    /// <summary>
    /// Writes all segments for this lab message to the writer.
    /// </summary>
    internal static void WriteSegments(this LabMessage message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        writer.WriteSegmentIfPresent(message.Pv1, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Pv2, in delimiters, options);
        writer.WriteSegmentCollection(message.Diagnoses, in delimiters, options);
        writer.WriteOrderGroups(message.Orders, in delimiters, options);
        writer.WriteSegmentCollection(message.Specimens, in delimiters, options);
        writer.WriteSegmentCollection(message.Containers, in delimiters, options);
        writer.WriteSegmentCollection(message.ClinicalTrials, in delimiters, options);
        writer.WriteSegmentCollection(message.Notes, in delimiters, options);
    }

    /// <summary>
    /// Writes all segments for this hospice message to the writer.
    /// </summary>
    internal static void WriteSegments(this HospiceMessage message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        writer.WriteSegmentIfPresent(message.Pv1, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Pv2, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Pd1, in delimiters, options);
        writer.WriteSegmentCollection(message.NextOfKin, in delimiters, options);
        writer.WriteSegmentCollection(message.Allergies, in delimiters, options);
        writer.WriteSegmentCollection(message.Diagnoses, in delimiters, options);
        writer.WriteSegmentCollection(message.Procedures, in delimiters, options);
        writer.WriteSegmentCollection(message.Guarantors, in delimiters, options);
        writer.WriteSegmentCollection(message.Insurance, in delimiters, options);
        writer.WriteSegmentCollection(message.InsuranceAdditional, in delimiters, options);
        writer.WriteSegmentIfPresent(message.DiagnosisRelatedGroup, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Accident, in delimiters, options);
        writer.WriteSegmentCollection(message.Roles, in delimiters, options);
        writer.WriteSegmentIfPresent(message.MergeInfo, in delimiters, options);
    }

    /// <summary>
    /// Writes all segments for this scheduling message to the writer.
    /// </summary>
    internal static void WriteSegments(this SchedulingMessage message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        writer.WriteSegmentIfPresent(message.Pv1, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Schedule, in delimiters, options);
        writer.WriteSegmentCollection(message.LocationResources, in delimiters, options);
        writer.WriteSegmentCollection(message.PersonnelResources, in delimiters, options);
        writer.WriteSegmentCollection(message.ServiceResources, in delimiters, options);
        writer.WriteSegmentCollection(message.Notes, in delimiters, options);
        writer.WriteSegmentCollection(message.Diagnoses, in delimiters, options);
    }

    /// <summary>
    /// Writes all segments for this financial message to the writer.
    /// </summary>
    internal static void WriteSegments(this FinancialMessage message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        writer.WriteSegmentIfPresent(message.Pv1, in delimiters, options);
        writer.WriteSegmentCollection(message.Guarantors, in delimiters, options);
        writer.WriteSegmentCollection(message.Insurance, in delimiters, options);
        writer.WriteSegmentCollection(message.InsuranceAdditional, in delimiters, options);
        writer.WriteSegmentIfPresent(message.DiagnosisRelatedGroup, in delimiters, options);
        writer.WriteSegmentCollection(message.Transactions, in delimiters, options);
        writer.WriteSegmentCollection(message.Procedures, in delimiters, options);
        writer.WriteSegmentCollection(message.Diagnoses, in delimiters, options);
        writer.WriteSegmentIfPresent(message.Accident, in delimiters, options);
    }

    /// <summary>
    /// Writes all segments for this dietary message to the writer.
    /// </summary>
    internal static void WriteSegments(this DietaryMessage message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        writer.WriteSegmentIfPresent(message.Pv1, in delimiters, options);
        writer.WriteSegmentCollection(message.Orders, in delimiters, options);
        writer.WriteSegmentCollection(message.DietaryOrders, in delimiters, options);
        writer.WriteSegmentCollection(message.TrayInstructions, in delimiters, options);
        writer.WriteSegmentCollection(message.Notes, in delimiters, options);
        writer.WriteSegmentCollection(message.Allergies, in delimiters, options);
        writer.WriteSegmentCollection(message.Diagnoses, in delimiters, options);
    }
}
