using KeryxPars.HL7.Segments;
using System.Text.Json;

namespace KeryxPars.HL7.Definitions;

/// <summary>
/// Legacy HL7 message class maintained for backward compatibility.
/// This is now a type alias for HL7DefaultMessage.
/// 
/// For new code, consider using:
/// - HL7DefaultMessage for general-purpose messages
/// - PharmacyMessage for pharmacy/medication orders
/// - LabMessage for laboratory orders and results
/// - HospiceMessage for hospice patient care
/// - SchedulingMessage for appointment scheduling
/// - FinancialMessage for billing transactions
/// - DietaryMessage for dietary orders
/// </summary>
[Obsolete("Use HL7DefaultMessage or specialized message types (PharmacyMessage, LabMessage, etc.) instead. This class is maintained for backward compatibility.", false)]
public class HL7Message : HL7DefaultMessage
{
    // All implementation inherited from HL7DefaultMessage
}