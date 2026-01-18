using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Serialization.Configuration;

namespace KeryxPars.HL7.Examples;

/// <summary>
/// Examples demonstrating the use of different HL7 message types
/// </summary>
public static class MessageTypeExamples
{
    /// <summary>
    /// Example: Using HL7DefaultMessage for general-purpose messages
    /// </summary>
    public static void DefaultMessageExample()
    {
        var hl7Text = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTEND_DOC^ATTENDING^A|||||||||||V12345|||||||||||||||||||||||||20230101120000|
AL1|1|DA|00000741^PENICILLIN^NDFRT||RASH";

        // Default deserialization returns HL7DefaultMessage
        var result = HL7Serializer.Deserialize(hl7Text);

        if (result.IsSuccess)
        {
            var message = result.Value;
            Console.WriteLine($"Message Type: {message.GetType().Name}");
            Console.WriteLine($"Patient: {message.Pid.PatientName[0].FamilyName}, {message.Pid.PatientName[0].GivenName}");
            Console.WriteLine($"MRN: {message.Pid.PatientIdentifierList[0].Id}");
            Console.WriteLine($"Visit: {message.Pv1.VisitNumber.Id}");
            Console.WriteLine($"Allergies: {message.Allergies.Count}");
        }
    }

    /// <summary>
    /// Example: Using PharmacyMessage for medication orders
    /// </summary>
    public static void PharmacyMessageExample()
    {
        var hl7Text = @"MSH|^~\&|PHARMACY|HOSPITAL|RX_SYSTEM|HOSPITAL|20230101120000||RDE^O11|ORD001|P|2.5||
PID|1||987654||SMITH^JANE^M||19900215|F|||
ORC|NW|ORD123456|FIL789012||||^^^20230101||
RXE|^^^20230101^20230201|00378-1805-10^Metformin 500mg^NDC|500||MG||||||10|||
RXR|PO^Oral^HL70162|||
TQ1|1||BID^Twice daily|||20230101120000|20230201120000|";

        // Use PharmacyMessage for medication orders
        var options = SerializerOptions.ForMedicationOrders();
        var result = HL7Serializer.Deserialize<PharmacyMessage>(hl7Text, options);

        if (result.IsSuccess)
        {
            var message = result.Value;
            Console.WriteLine($"Message Type: {message.GetType().Name}");
            Console.WriteLine($"Patient: {message.Pid.PatientName[0].FamilyName}");
            Console.WriteLine($"Number of Orders: {message.Orders.Count}");

            foreach (var order in message.Orders)
            {
                if (order.TryGetSegment<ORC>("ORC", out var orc))
                    Console.WriteLine($"  Order Control: {orc.OrderControl.Value}");
                    
                if (order.TryGetSegment<RXE>("RXE", out var rxe))
                {
                    Console.WriteLine($"  Medication: {rxe.GiveCode.Text}");
                    Console.WriteLine($"  Dose: {rxe.GiveAmountMinimum.Value} {rxe.GiveUnits.Text}");
                }
                
                if (order.TryGetRepeatableSegments<RXR>("RXR", out var rxrList) && rxrList.Count > 0)
                    Console.WriteLine($"  Route: {rxrList[0].Route.Text}");
                    
                if (order.TryGetRepeatableSegments<TQ1>("TQ1", out var tq1List) && tq1List.Count > 0)
                    Console.WriteLine($"  Frequency: {tq1List[0].Quantity}");
            }
        }
    }

    /// <summary>
    /// Example: Using LabMessage for laboratory results
    /// </summary>
    public static void LabMessageExample()
    {
        var hl7Text = @"MSH|^~\&|LAB|HOSPITAL|EMR|HOSPITAL|20230101120000||ORU^R01|LAB001|P|2.5||
PID|1||123456||DOE^JOHN||19800101|M|||
OBR|1|ORD123|FIL456|CBC^Complete Blood Count^LN|||20230101100000|
OBX|1|NM|WBC^White Blood Count^LN||7.5|10*3/uL|4.5-11.0|N|||F|||
OBX|2|NM|RBC^Red Blood Count^LN||4.8|10*6/uL|4.5-5.9|N|||F|||
OBX|3|NM|HGB^Hemoglobin^LN||14.2|g/dL|13.5-17.5|N|||F|||";

        var options = SerializerOptions.ForLabOrders();
        var result = HL7Serializer.Deserialize<LabMessage>(hl7Text, options);

        if (result.IsSuccess)
        {
            var message = result.Value;
            Console.WriteLine($"Message Type: {message.GetType().Name}");
            Console.WriteLine($"Patient: {message.Pid.PatientName[0].FamilyName}");
            Console.WriteLine($"Number of Orders: {message.Orders.Count}");
            Console.WriteLine($"Diagnoses: {message.Diagnoses.Count}");
        }
    }

    /// <summary>
    /// Example: Using HospiceMessage for comprehensive patient information
    /// </summary>
    public static void HospiceMessageExample()
    {
        var hl7Text = @"MSH|^~\&|ADT|HOSPICE|EMR|HOSPICE|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTEND_DOC^ATTENDING^A|||||||||||V12345|||||||||||||||||||||||||20230101120000|
NK1|1|DOE^JANE^B|SPO^Spouse|123 MAIN ST^^CITY^ST^12345|555-1234
AL1|1|DA|00000741^PENICILLIN^NDFRT||RASH
DG1|1|ICD10|C34.90^Malignant neoplasm of unspecified part of bronchus or lung|||A
PR1|1||99.15^Parenteral infusion of concentrated nutritional substances|||20230101|
GT1|1||DOE^JANE^B|DOE^JANE^B|123 MAIN ST^^CITY^ST^12345|555-1234||19780515|F||
IN1|1|1234|INS_CO^Insurance Company|||GROUP123|||||||||||DOE^JOHN^A|SELF|19800101|
ROL|1|AD|ADM^Admitting Provider|SMITH^ROBERT^J|||20230101120000|";

        var options = SerializerOptions.ForHospice();
        var result = HL7Serializer.Deserialize<HospiceMessage>(hl7Text, options);

        if (result.IsSuccess)
        {
            var message = result.Value;
            Console.WriteLine($"Message Type: {message.GetType().Name}");
            Console.WriteLine($"Patient: {message.Pid.PatientName[0].FamilyName}, {message.Pid.PatientName[0].GivenName}");
            Console.WriteLine($"Visit: {message.Pv1.VisitNumber.Id}");
            Console.WriteLine($"Next of Kin: {message.NextOfKin.Count}");
            Console.WriteLine($"Allergies: {message.Allergies.Count}");
            Console.WriteLine($"Diagnoses: {message.Diagnoses.Count}");
            Console.WriteLine($"Procedures: {message.Procedures.Count}");
            Console.WriteLine($"Guarantors: {message.Guarantors.Count}");
            Console.WriteLine($"Insurance: {message.Insurance.Count}");
            Console.WriteLine($"Roles: {message.Roles.Count}");

            // Access specific segment details
            if (message.NextOfKin.Count > 0)
            {
                var nk = message.NextOfKin[0];
                Console.WriteLine($"Emergency Contact: {nk.Name[0].FamilyName}, {nk.Name[0].GivenName}");
                Console.WriteLine($"Relationship: {nk.Relationship.Text}");
            }
        }
    }

    /// <summary>
    /// Example: Using SchedulingMessage for appointments
    /// </summary>
    public static void SchedulingMessageExample()
    {
        var hl7Text = @"MSH|^~\&|SCHEDULING|HOSPITAL|EMR|HOSPITAL|20230101120000||SIU^S12|SCH001|P|2.5||
PID|1||123456||DOE^JOHN||19800101|M|||
SCH||ORD123456|||||NEW PATIENT^Office Visit|||60|MIN||20230115100000|||||DR_SMITH|||||||SCHEDULED||
AIL|1||CLINIC_A^Main Clinic Room A^L|||20230115100000|20230115110000|60|MIN||
AIP|1||SMITH^ROBERT^J^DR|||20230115100000|20230115110000|60|MIN||
NTE|1||Patient requested morning appointment";

        var options = SerializerOptions.ForScheduling();
        var result = HL7Serializer.Deserialize<SchedulingMessage>(hl7Text, options);

        if (result.IsSuccess)
        {
            var message = result.Value;
            Console.WriteLine($"Message Type: {message.GetType().Name}");
            Console.WriteLine($"Patient: {message.Pid.PatientName[0].FamilyName}");
            Console.WriteLine($"Schedule ID: {message.Schedule?.PlacerAppointmentID.EntityIdentifier}");
            Console.WriteLine($"Location Resources: {message.LocationResources.Count}");
            Console.WriteLine($"Personnel Resources: {message.PersonnelResources.Count}");
            Console.WriteLine($"Service Resources: {message.ServiceResources.Count}");
            Console.WriteLine($"Notes: {message.Notes.Count}");
        }
    }

    /// <summary>
    /// Example: Using FinancialMessage for billing
    /// </summary>
    public static void FinancialMessageExample()
    {
        var hl7Text = @"MSH|^~\&|BILLING|HOSPITAL|EMR|HOSPITAL|20230101120000||DFT^P03|BILL001|P|2.5||
EVN|P03|20230101120000||
PID|1||123456||DOE^JOHN||19800101|M|||
PV1|1|I|WARD^ROOM^BED||||ATTEND_DOC^ATTENDING^A|||||||||||V12345|||||||||||||||||||||||||20230101120000|
GT1|1||DOE^JANE^B|DOE^JANE^B|123 MAIN ST^^CITY^ST^12345|555-1234||19780515|F||
IN1|1|1234|INS_CO^Insurance Company|||GROUP123|||||||||||DOE^JOHN^A|SELF|19800101|
FT1|1||TRX123456|20230101|20230101|CG|99213^Office Visit^CPT|Office Visit|||1|150.00|||||
DG1|1|ICD10|Z00.00^Encounter for general adult medical examination without abnormal findings|||A";

        var options = SerializerOptions.ForFinancial();
        var result = HL7Serializer.Deserialize<FinancialMessage>(hl7Text, options);

        if (result.IsSuccess)
        {
            var message = result.Value;
            Console.WriteLine($"Message Type: {message.GetType().Name}");
            Console.WriteLine($"Patient: {message.Pid.PatientName[0].FamilyName}");
            Console.WriteLine($"Guarantors: {message.Guarantors.Count}");
            Console.WriteLine($"Insurance: {message.Insurance.Count}");
            Console.WriteLine($"Transactions: {message.Transactions.Count}");
            Console.WriteLine($"Diagnoses: {message.Diagnoses.Count}");

            if (message.Transactions.Count > 0)
            {
                var ft1 = message.Transactions[0];
                Console.WriteLine($"Transaction: {ft1.TransactionID}");
                // Note: FT1 properties vary, check actual implementation for correct property names
            }
        }
    }

    /// <summary>
    /// Example: Using DietaryMessage for meal orders
    /// </summary>
    public static void DietaryMessageExample()
    {
        var hl7Text = @"MSH|^~\&|DIETARY|HOSPITAL|EMR|HOSPITAL|20230101120000||ORM^O01|DIET001|P|2.5||
PID|1||123456||DOE^JOHN||19800101|M|||
ORC|NW|DIET123456||||^^^^20230101||
ODS|D|REGULAR^Regular Diet^DIET|||20230101|
ODT|L|TRAY1^Standard Tray|||";

        var options = SerializerOptions.ForDietary();
        var result = HL7Serializer.Deserialize<DietaryMessage>(hl7Text, options);

        if (result.IsSuccess)
        {
            var message = result.Value;
            Console.WriteLine($"Message Type: {message.GetType().Name}");
            Console.WriteLine($"Patient: {message.Pid.PatientName[0].FamilyName}");
            Console.WriteLine($"Orders: {message.Orders.Count}");
            Console.WriteLine($"Dietary Orders: {message.DietaryOrders.Count}");
            Console.WriteLine($"Tray Instructions: {message.TrayInstructions.Count}");
            Console.WriteLine($"Allergies (restrictions): {message.Allergies.Count}");
        }
    }

    /// <summary>
    /// Example: Using pattern matching to handle multiple message types
    /// </summary>
    public static void PolymorphicMessageHandling(IHL7Message message)
    {
        Console.WriteLine($"Processing {message.GetType().Name}");
        Console.WriteLine($"Patient: {message.Pid?.PatientName[0].FamilyName ?? "Unknown"}");

        switch (message)
        {
            case PharmacyMessage pharmMsg:
                Console.WriteLine($"Pharmacy - Orders: {pharmMsg.Orders.Count}, Allergies: {pharmMsg.Allergies.Count}");
                break;

            case LabMessage labMsg:
                Console.WriteLine($"Lab - Orders: {labMsg.Orders.Count}, Specimens: {labMsg.Specimens.Count}");
                break;

            case HospiceMessage hospiceMsg:
                Console.WriteLine($"Hospice - Diagnoses: {hospiceMsg.Diagnoses.Count}, Procedures: {hospiceMsg.Procedures.Count}");
                break;

            case SchedulingMessage schedMsg:
                Console.WriteLine($"Scheduling - Locations: {schedMsg.LocationResources.Count}, Personnel: {schedMsg.PersonnelResources.Count}");
                break;

            case FinancialMessage finMsg:
                Console.WriteLine($"Financial - Transactions: {finMsg.Transactions.Count}, Insurance: {finMsg.Insurance.Count}");
                break;

            case DietaryMessage dietMsg:
                Console.WriteLine($"Dietary - Orders: {dietMsg.Orders.Count}, Tray Instructions: {dietMsg.TrayInstructions.Count}");
                break;

            case HL7DefaultMessage defaultMsg:
                Console.WriteLine($"Default - Allergies: {defaultMsg.Allergies.Count}, Diagnoses: {defaultMsg.Diagnoses.Count}");
                break;
        }
    }

    /// <summary>
    /// Example: Creating and serializing different message types
    /// </summary>
    public static void SerializationExamples()
    {
        // Create a simple message for serialization
        var message = new HL7DefaultMessage
        {
            Msh = new()
            {
                SendingApplication = "PHARMACY",
                SendingFacility = "HOSPITAL",
                ReceivingApplication = "RX_SYSTEM",
                ReceivingFacility = "HOSPITAL",
                MessageType = "RDE^O11",
                MessageControlID = "ORD001",
                ProcessingID = "P",
                VersionID = "2.5"
            }
        };

        var result = HL7Serializer.Serialize(message);
        if (result.IsSuccess)
        {
            Console.WriteLine("Serialized Message:");
            Console.WriteLine(result.Value);
        }
    }
}
