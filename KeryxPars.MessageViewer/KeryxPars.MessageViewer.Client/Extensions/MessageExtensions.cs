using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Contracts;

namespace KeryxPars.MessageViewer.Client.Extensions;

public static class MessageExtensions
{
    public static IEnumerable<ISegment> GetAllSegments(this HL7DefaultMessage message)
    {
        var segments = new List<ISegment>();

        segments.Add(message.Msh);
        
        if (message.Evn != null)
            segments.Add(message.Evn);
            
        if (message.Pid != null)
            segments.Add(message.Pid);

        if (!IsEmpty(message.Pv1))
            segments.Add(message.Pv1);
            
        if (!IsEmpty(message.Pv2))
            segments.Add(message.Pv2);

        segments.AddRange(message.Allergies.Where(s => !IsEmpty(s)));
        segments.AddRange(message.Diagnoses.Where(s => !IsEmpty(s)));
        segments.AddRange(message.Insurance.Where(s => !IsEmpty(s)));

        foreach (var order in message.Orders)
        {
            if (order.PrimarySegment != null && !IsEmpty(order.PrimarySegment))
                segments.Add(order.PrimarySegment);
            
            foreach (var detailSegment in order.DetailSegments.Values)
            {
                if (!IsEmpty(detailSegment))
                    segments.Add(detailSegment);
            }
            
            foreach (var repeatableList in order.RepeatableSegments.Values)
            {
                segments.AddRange(repeatableList.Where(s => !IsEmpty(s)));
            }
        }

        segments.AddRange(message.Errors.Where(s => !IsEmpty(s)));

        // If it's a comprehensive message, add all additional segments
        if (message is HL7ComprehensiveMessage comprehensive)
        {
            if (comprehensive.Pd1 != null && !IsEmpty(comprehensive.Pd1))
                segments.Add(comprehensive.Pd1);

            segments.AddRange(comprehensive.NextOfKin.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.Guarantors.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.InsuranceAdditional.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.Procedures.Where(s => !IsEmpty(s)));

            if (comprehensive.DiagnosisRelatedGroup != null && !IsEmpty(comprehensive.DiagnosisRelatedGroup))
                segments.Add(comprehensive.DiagnosisRelatedGroup);

            if (comprehensive.Accident != null && !IsEmpty(comprehensive.Accident))
                segments.Add(comprehensive.Accident);

            if (comprehensive.MergeInfo != null && !IsEmpty(comprehensive.MergeInfo))
                segments.Add(comprehensive.MergeInfo);

            segments.AddRange(comprehensive.Roles.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.Notes.Where(s => !IsEmpty(s)));

            if (comprehensive.Schedule != null && !IsEmpty(comprehensive.Schedule))
                segments.Add(comprehensive.Schedule);

            segments.AddRange(comprehensive.LocationResources.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.PersonnelResources.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.ServiceResources.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.ObservationRequests.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.ObservationResults.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.Specimens.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.Containers.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.PharmacyAdministrations.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.PharmacyComponents.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.PharmacyDispenses.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.PharmacyGives.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.Transactions.Where(s => !IsEmpty(s)));

            if (comprehensive.QueryDefinition != null && !IsEmpty(comprehensive.QueryDefinition))
                segments.Add(comprehensive.QueryDefinition);

            if (comprehensive.QueryFilter != null && !IsEmpty(comprehensive.QueryFilter))
                segments.Add(comprehensive.QueryFilter);

            if (comprehensive.QueryParameterDefinition != null && !IsEmpty(comprehensive.QueryParameterDefinition))
                segments.Add(comprehensive.QueryParameterDefinition);

            if (comprehensive.ResponseControlParameter != null && !IsEmpty(comprehensive.ResponseControlParameter))
                segments.Add(comprehensive.ResponseControlParameter);

            if (comprehensive.ContinuationPointer != null && !IsEmpty(comprehensive.ContinuationPointer))
                segments.Add(comprehensive.ContinuationPointer);

            segments.AddRange(comprehensive.ContactData.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.ClinicalTrials.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.DietaryOrders.Where(s => !IsEmpty(s)));
            segments.AddRange(comprehensive.TrayInstructions.Where(s => !IsEmpty(s)));

            if (comprehensive.MessageAcknowledgment != null && !IsEmpty(comprehensive.MessageAcknowledgment))
                segments.Add(comprehensive.MessageAcknowledgment);
        }

        return segments;
    }

    private static bool IsEmpty(this ISegment? segment)
    {
        if (segment == null)
            return true;

        var values = segment.GetValues();
        
        for (int i = 1; i < values.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(values[i]))
                return false;
        }

        return true;
    }
}


