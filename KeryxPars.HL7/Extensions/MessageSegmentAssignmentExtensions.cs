namespace KeryxPars.HL7.Extensions;

using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;

/// <summary>
/// Extension methods for assigning segments to message types.
/// Implements polymorphic assignment following Domain-Driven Design principles.
/// </summary>
internal static class MessageSegmentAssignmentExtensions
{
    /// <summary>
    /// Assigns a PV1 segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, PV1 pv1)
    {
        switch (message)
        {
            case HL7DefaultMessage msg:
                msg.Pv1 = pv1;
                break;
            case PharmacyMessage msg:
                msg.Pv1 = pv1;
                break;
            case LabMessage msg:
                msg.Pv1 = pv1;
                break;
            case HospiceMessage msg:
                msg.Pv1 = pv1;
                break;
            case SchedulingMessage msg:
                msg.Pv1 = pv1;
                break;
            case FinancialMessage msg:
                msg.Pv1 = pv1;
                break;
            case DietaryMessage msg:
                msg.Pv1 = pv1;
                break;
        }
    }

    /// <summary>
    /// Assigns a PV2 segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, PV2 pv2)
    {
        switch (message)
        {
            case HL7DefaultMessage msg:
                msg.Pv2 = pv2;
                break;
            case PharmacyMessage msg:
                msg.Pv2 = pv2;
                break;
            case LabMessage msg:
                msg.Pv2 = pv2;
                break;
            case HospiceMessage msg:
                msg.Pv2 = pv2;
                break;
        }
    }

    /// <summary>
    /// Assigns an AL1 (allergy) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, AL1 allergy)
    {
        switch (message)
        {
            case HL7DefaultMessage msg:
                msg.Allergies.Add(allergy);
                break;
            case PharmacyMessage msg:
                msg.Allergies.Add(allergy);
                break;
            case HospiceMessage msg:
                msg.Allergies.Add(allergy);
                break;
            case DietaryMessage msg:
                msg.Allergies.Add(allergy);
                break;
        }
    }

    /// <summary>
    /// Assigns a DG1 (diagnosis) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, DG1 diagnosis)
    {
        switch (message)
        {
            case HL7DefaultMessage msg:
                msg.Diagnoses.Add(diagnosis);
                break;
            case PharmacyMessage msg:
                msg.Diagnoses.Add(diagnosis);
                break;
            case LabMessage msg:
                msg.Diagnoses.Add(diagnosis);
                break;
            case HospiceMessage msg:
                msg.Diagnoses.Add(diagnosis);
                break;
            case SchedulingMessage msg:
                msg.Diagnoses.Add(diagnosis);
                break;
            case FinancialMessage msg:
                msg.Diagnoses.Add(diagnosis);
                break;
            case DietaryMessage msg:
                msg.Diagnoses.Add(diagnosis);
                break;
        }
    }

    /// <summary>
    /// Assigns an IN1 (insurance) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, IN1 insurance)
    {
        switch (message)
        {
            case HL7DefaultMessage msg:
                msg.Insurance.Add(insurance);
                break;
            case PharmacyMessage msg:
                msg.Insurance.Add(insurance);
                break;
            case HospiceMessage msg:
                msg.Insurance.Add(insurance);
                break;
            case FinancialMessage msg:
                msg.Insurance.Add(insurance);
                break;
        }
    }

    /// <summary>
    /// Assigns an IN2 (additional insurance) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, IN2 insuranceAdditional)
    {
        switch (message)
        {
            case HL7ComprehensiveMessage msg:
                msg.InsuranceAdditional.Add(insuranceAdditional);
                break;
            case HospiceMessage msg:
                msg.InsuranceAdditional.Add(insuranceAdditional);
                break;
            case FinancialMessage msg:
                msg.InsuranceAdditional.Add(insuranceAdditional);
                break;
        }
    }

    /// <summary>
    /// Assigns a PR1 (procedure) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, PR1 procedure)
    {
        switch (message)
        {
            case HL7ComprehensiveMessage msg:
                msg.Procedures.Add(procedure);
                break;
            case HospiceMessage msg:
                msg.Procedures.Add(procedure);
                break;
            case FinancialMessage msg:
                msg.Procedures.Add(procedure);
                break;
        }
    }

    /// <summary>
    /// Assigns a GT1 (guarantor) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, GT1 guarantor)
    {
        switch (message)
        {
            case HL7ComprehensiveMessage msg:
                msg.Guarantors.Add(guarantor);
                break;
            case HospiceMessage msg:
                msg.Guarantors.Add(guarantor);
                break;
            case FinancialMessage msg:
                msg.Guarantors.Add(guarantor);
                break;
        }
    }

    /// <summary>
    /// Assigns a DRG (diagnosis related group) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, DRG drg)
    {
        switch (message)
        {
            case HL7ComprehensiveMessage msg:
                msg.DiagnosisRelatedGroup = drg;
                break;
            case HospiceMessage msg:
                msg.DiagnosisRelatedGroup = drg;
                break;
            case FinancialMessage msg:
                msg.DiagnosisRelatedGroup = drg;
                break;
        }
    }

    /// <summary>
    /// Assigns an ACC (accident) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, ACC accident)
    {
        switch (message)
        {
            case HL7ComprehensiveMessage msg:
                msg.Accident = accident;
                break;
            case HospiceMessage msg:
                msg.Accident = accident;
                break;
            case FinancialMessage msg:
                msg.Accident = accident;
                break;
        }
    }

    /// <summary>
    /// Assigns an NTE (notes) segment to the appropriate message type.
    /// </summary>
    internal static void AssignSegment(this IHL7Message message, NTE note)
    {
        switch (message)
        {
            case HL7ComprehensiveMessage msg:
                msg.Notes.Add(note);
                break;
            case PharmacyMessage msg:
                msg.Notes.Add(note);
                break;
            case LabMessage msg:
                msg.Notes.Add(note);
                break;
            case SchedulingMessage msg:
                msg.Notes.Add(note);
                break;
            case DietaryMessage msg:
                msg.Notes.Add(note);
                break;
        }
    }

    /// <summary>
    /// Adds an order group to the appropriate message type.
    /// </summary>
    internal static void AddOrderGroup(this IHL7Message message, OrderGroup orderGroup)
    {
        switch (message)
        {
            case HL7DefaultMessage msg:
                msg.Orders.Add(orderGroup);
                break;
            case PharmacyMessage msg:
                msg.Orders.Add(orderGroup);
                break;
            case LabMessage msg:
                msg.Orders.Add(orderGroup);
                break;
            case DietaryMessage msg:
                msg.GroupedOrders.Add(orderGroup);
                break;
        }
    }
}
