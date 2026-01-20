using KeryxPars.HL7.Definitions;

namespace KeryxPars.MessageViewer.Core.Services;

/// <summary>
/// Detects the appropriate HL7 message type based on message content.
/// Maps HL7 message types to specialized message classes.
/// </summary>
public static class MessageTypeDetector
{
    /// <summary>
    /// Determines the best message class type based on the raw HL7 message.
    /// </summary>
    public static Type DetectMessageType(string rawMessage)
    {
        if (string.IsNullOrWhiteSpace(rawMessage))
            return typeof(HL7ComprehensiveMessage);

        // Extract MSH segment (first line)
        var firstLine = rawMessage.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        if (firstLine == null || !firstLine.StartsWith("MSH"))
            return typeof(HL7ComprehensiveMessage);

        // Parse MSH.9 (Message Type) - format: MessageType^TriggerEvent
        var fields = firstLine.Split('|');
        if (fields.Length < 9)
            return typeof(HL7ComprehensiveMessage);

        var messageTypeField = fields[8]; // MSH.9 (0-based: field 8)
        var messageTypeParts = messageTypeField.Split('^');
        
        if (messageTypeParts.Length == 0)
            return typeof(HL7ComprehensiveMessage);

        var messageType = messageTypeParts[0];
        var triggerEvent = messageTypeParts.Length > 1 ? messageTypeParts[1] : "";

        // Map message types to specialized classes
        return messageType switch
        {
            // Pharmacy/Medication Messages
            "RDE" => typeof(PharmacyMessage),  // Pharmacy/Treatment Encoded Order
            "RDS" => typeof(PharmacyMessage),  // Pharmacy/Treatment Dispense
            "RGV" => typeof(PharmacyMessage),  // Pharmacy/Treatment Give
            "RAS" => typeof(PharmacyMessage),  // Pharmacy/Treatment Administration
            "OMP" => typeof(PharmacyMessage),  // Pharmacy/Treatment Order
            
            // Lab/Observation Messages
            "ORU" => typeof(LabMessage),       // Observation Result
            "OUL" => typeof(LabMessage),       // Unsolicited Laboratory Observation
            "OML" => typeof(LabMessage),       // Laboratory Order
            
            // Order Messages (could be lab or pharmacy - check trigger)
            "ORM" when IsLabOrder(rawMessage) => typeof(LabMessage),
            "ORM" when IsPharmacyOrder(rawMessage) => typeof(PharmacyMessage),
            "ORM" when IsDietaryOrder(rawMessage) => typeof(DietaryMessage),
            "ORM" => typeof(HL7ComprehensiveMessage), // Generic order
            
            // Scheduling Messages
            "SIU" => typeof(SchedulingMessage), // Scheduling Information Unsolicited
            
            // Financial Messages
            "DFT" => typeof(FinancialMessage),  // Detailed Financial Transaction
            "BAR" => typeof(FinancialMessage),  // Add/Change Billing Account
            
            // ADT Messages - Use HospiceMessage for better clinical detail
            "ADT" => typeof(HospiceMessage),    // Admit/Discharge/Transfer
            
            // Default to comprehensive for everything else
            _ => typeof(HL7ComprehensiveMessage)
        };
    }

    /// <summary>
    /// Gets a human-readable description of the detected message type.
    /// </summary>
    public static string GetMessageTypeDescription(Type messageType)
    {
        return messageType.Name switch
        {
            nameof(PharmacyMessage) => "Pharmacy/Medication Order",
            nameof(LabMessage) => "Laboratory Order/Result",
            nameof(SchedulingMessage) => "Appointment/Scheduling",
            nameof(FinancialMessage) => "Financial/Billing Transaction",
            nameof(HospiceMessage) => "Patient Administration (ADT)",
            nameof(DietaryMessage) => "Dietary/Nutrition Order",
            nameof(HL7ComprehensiveMessage) => "General HL7 Message",
            _ => "HL7 Message"
        };
    }

    private static bool IsLabOrder(string rawMessage)
    {
        // Check for OBR (Observation Request) or OBX (Observation Result) segments
        return rawMessage.Contains("\nOBR|") || rawMessage.Contains("\rOBR|") ||
               rawMessage.Contains("\nOBX|") || rawMessage.Contains("\rOBX|");
    }

    private static bool IsPharmacyOrder(string rawMessage)
    {
        // Check for RXE, RXO, RXA (pharmacy-specific segments)
        return rawMessage.Contains("\nRXE|") || rawMessage.Contains("\rRXE|") ||
               rawMessage.Contains("\nRXO|") || rawMessage.Contains("\rRXO|") ||
               rawMessage.Contains("\nRXA|") || rawMessage.Contains("\rRXA|") ||
               rawMessage.Contains("\nRXD|") || rawMessage.Contains("\rRXD|");
    }

    private static bool IsDietaryOrder(string rawMessage)
    {
        // Check for ODS (Dietary Orders) or ODT (Diet Tray) segments
        return rawMessage.Contains("\nODS|") || rawMessage.Contains("\rODS|") ||
               rawMessage.Contains("\nODT|") || rawMessage.Contains("\rODT|");
    }
}
