using System;
using KeryxPars.Examples;

namespace KeryxPars.Examples;

/// <summary>
/// Demonstrates ONE model working with 3 different HL7 message types!
/// ADT (admission), OMP (pharmacy), and ORU (lab results)
/// </summary>
public class UniversalPatientDemo
{
    public static void Main()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("  UNIVERSAL PATIENT MODEL DEMO");
        Console.WriteLine("  ONE Model, MULTIPLE Message Types!");
        Console.WriteLine("========================================\n");

        // ========== SCENARIO 1: ADT Message (Admission) ==========
        
        Console.WriteLine("?? SCENARIO 1: ADT Message (Patient Admission)\n");
        
        var adtMessage = 
            "MSH|^~\\&|EPIC|UCSF|HL7|UCSF|20240115100000||ADT^A01|ADT001|P|2.5||\r" +
            "PID|1||MRN123456||SMITH^JOHN^MICHAEL||19750815|M|||123 MAIN ST^^SF^CA^94102||555-1234|||EN|M|\r" +
            "PV1|1|I|4WEST^401^A|||7890^DOE^JANE^A^^DR||||MED||||2|||8901^JONES^ROBERT^^DR|\r" +
            "DG1|1||I10^Essential Hypertension^ICD10||20240115|A|\r" +
            "DG1|2||E11.9^Type 2 Diabetes^ICD10||20240115|S|\r" +
            "IN1|1|PPO|BLUE123|Blue Cross Blue Shield||||||||||||\r" +
            "AL1|1||PENICILLIN^Penicillin||SEVERE|";

        var adtPatient = UniversalPatientMapper.MapFromSpan(adtMessage.AsSpan());

        Console.WriteLine($"? Patient: {adtPatient.FirstName} {adtPatient.LastName}");
        Console.WriteLine($"   MRN: {adtPatient.MRN}");
        Console.WriteLine($"   Message Type: {adtPatient.MessageType}");
        Console.WriteLine();

        Console.WriteLine("?? VISIT INFO (ADT-specific):");
        Console.WriteLine($"   Patient Class: {adtPatient.PatientClass}");
        Console.WriteLine($"   Bed: {adtPatient.BedAssignment}");
        Console.WriteLine($"   Attending: {adtPatient.AttendingPhysician}");
        Console.WriteLine();

        Console.WriteLine("?? PHARMACY ORDERS:");
        Console.WriteLine($"   Count: {adtPatient.PharmacyOrders.Count}");
        Console.WriteLine($"   ? No pharmacy data in ADT (empty list - NO ERROR!)");
        Console.WriteLine();

        Console.WriteLine("?? LAB RESULTS:");
        Console.WriteLine($"   Count: {adtPatient.LabResults.Count}");
        Console.WriteLine($"   ? No lab data in ADT (empty list - NO ERROR!)");
        Console.WriteLine();

        Console.WriteLine("?? DIAGNOSES:");
        foreach (var dx in adtPatient.Diagnoses)
        {
            Console.WriteLine($"   {dx.SetID}. {dx.DiagnosisCode} - {dx.DiagnosisDescription}");
        }
        Console.WriteLine();

        Console.WriteLine("?? INSURANCE:");
        foreach (var ins in adtPatient.InsuranceInfo)
        {
            Console.WriteLine($"   {ins.SetID}. {ins.InsuranceCompanyName} ({ins.InsuranceCompanyID})");
        }
        Console.WriteLine();

        Console.WriteLine("?? ALLERGIES:");
        foreach (var allergy in adtPatient.Allergies)
        {
            Console.WriteLine($"   {allergy.SetID}. {allergy.AllergenDescription} - {allergy.AllergySeverity}");
        }
        Console.WriteLine();

        // ========== SCENARIO 2: OMP Message (Pharmacy Order) ==========
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("?? SCENARIO 2: OMP Message (Pharmacy Order)\n");
        
        var ompMessage = 
            "MSH|^~\\&|PHARMACY|UCSF|HL7|UCSF|20240115110000||OMP^O09|OMP001|P|2.5||\r" +
            "PID|1||MRN123456||SMITH^JOHN^MICHAEL||19750815|M|||123 MAIN ST^^SF^CA^94102||555-1234|\r" +
            "PV1|1|O|||||||8901^JONES^ROBERT^^DR|\r" +
            "RXO|1234^Metformin 500mg^NDC|500|mg|ORAL||||||||||||||||9012^BROWN^MARY^^DR|\r" +
            "RXO|5678^Lisinopril 10mg^NDC|10|mg|ORAL||||||||||||||||9012^BROWN^MARY^^DR|\r" +
            "RXD|1|1234^Metformin 500mg^NDC||500|mg|20240115110000|\r" +
            "AL1|1||PENICILLIN^Penicillin||SEVERE|";

        var pharmacyPatient = UniversalPatientMapper.MapFromSpan(ompMessage.AsSpan());

        Console.WriteLine($"? Patient: {pharmacyPatient.FirstName} {pharmacyPatient.LastName}");
        Console.WriteLine($"   MRN: {pharmacyPatient.MRN}");
        Console.WriteLine($"   Message Type: {pharmacyPatient.MessageType}");
        Console.WriteLine();

        Console.WriteLine("?? VISIT INFO:");
        Console.WriteLine($"   Patient Class: {pharmacyPatient.PatientClass ?? "N/A"}");
        Console.WriteLine($"   Bed: {pharmacyPatient.BedAssignment ?? "N/A (Outpatient)"}");
        Console.WriteLine();

        Console.WriteLine("?? PHARMACY ORDERS (OMP-specific):");
        foreach (var order in pharmacyPatient.PharmacyOrders)
        {
            Console.WriteLine($"   • {order.DrugName}: {order.RequestedGiveAmountMinimum} {order.RequestedGiveUnits}");
        }
        Console.WriteLine();

        Console.WriteLine("?? DISPENSES:");
        foreach (var dispense in pharmacyPatient.PharmacyDispenses)
        {
            Console.WriteLine($"   • {dispense.DispenseName}: {dispense.ActualDispenseAmount}");
        }
        Console.WriteLine();

        Console.WriteLine("?? LAB RESULTS:");
        Console.WriteLine($"   Count: {pharmacyPatient.LabResults.Count}");
        Console.WriteLine($"   ? No lab data in pharmacy message (empty list - NO ERROR!)");
        Console.WriteLine();

        Console.WriteLine("?? DIAGNOSES:");
        Console.WriteLine($"   Count: {pharmacyPatient.Diagnoses.Count}");
        Console.WriteLine($"   ? No diagnoses in pharmacy message (OK!)");
        Console.WriteLine();

        Console.WriteLine("?? ALLERGIES:");
        foreach (var allergy in pharmacyPatient.Allergies)
        {
            Console.WriteLine($"   {allergy.SetID}. {allergy.AllergenDescription} - {allergy.AllergySeverity}");
        }
        Console.WriteLine();

        // ========== SCENARIO 3: ORU Message (Lab Results) ==========
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("?? SCENARIO 3: ORU Message (Lab Results)\n");
        
        var oruMessage = 
            "MSH|^~\\&|LAB|UCSF|HL7|UCSF|20240115120000||ORU^R01|ORU001|P|2.5||\r" +
            "PID|1||MRN123456||SMITH^JOHN^MICHAEL||19750815|M|||123 MAIN ST^^SF^CA^94102||555-1234|\r" +
            "PV1|1|O|||||||8901^JONES^ROBERT^^DR|\r" +
            "OBR|1||LAB123|CBC^Complete Blood Count^LN||20240115120000|||||||||9012^BROWN^MARY^^DR|\r" +
            "OBX|1|NM|WBC^White Blood Cell Count^LN||8.5|10*3/uL|4.5-11.0|N|||F|\r" +
            "OBX|2|NM|RBC^Red Blood Cell Count^LN||4.8|10*6/uL|4.5-5.5|N|||F|\r" +
            "OBX|3|NM|HGB^Hemoglobin^LN||14.2|g/dL|13.5-17.5|N|||F|\r" +
            "OBX|4|NM|GLU^Glucose^LN||125|mg/dL|70-100|H|||F|\r" +
            "AL1|1||PENICILLIN^Penicillin||SEVERE|";

        var labPatient = UniversalPatientMapper.MapFromSpan(oruMessage.AsSpan());

        Console.WriteLine($"? Patient: {labPatient.FirstName} {labPatient.LastName}");
        Console.WriteLine($"   MRN: {labPatient.MRN}");
        Console.WriteLine($"   Message Type: {labPatient.MessageType}");
        Console.WriteLine();

        Console.WriteLine("?? VISIT INFO:");
        Console.WriteLine($"   Attending: {labPatient.AttendingPhysician}");
        Console.WriteLine();

        Console.WriteLine("?? LAB ORDERS:");
        foreach (var order in labPatient.OrderRequests)
        {
            Console.WriteLine($"   {order.SetID}. {order.UniversalServiceName}");
        }
        Console.WriteLine();

        Console.WriteLine("?? LAB RESULTS (ORU-specific):");
        foreach (var obs in labPatient.LabResults)
        {
            var flag = obs.AbnormalFlags == "H" ? "?? HIGH" : 
                       obs.AbnormalFlags == "L" ? "?? LOW" : "? Normal";
            Console.WriteLine($"   {obs.SetID}. {obs.ObservationText}: {obs.ObservationValue} {obs.Units} {flag}");
        }
        Console.WriteLine();

        Console.WriteLine("?? PHARMACY ORDERS:");
        Console.WriteLine($"   Count: {labPatient.PharmacyOrders.Count}");
        Console.WriteLine($"   ? No pharmacy data in lab message (empty list - NO ERROR!)");
        Console.WriteLine();

        Console.WriteLine("?? ALLERGIES:");
        foreach (var allergy in labPatient.Allergies)
        {
            Console.WriteLine($"   {allergy.SetID}. {allergy.AllergenDescription} - {allergy.AllergySeverity}");
        }
        Console.WriteLine();

        // ========== SUMMARY ==========
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("? SUMMARY - Same Model, Different Messages!");
        Console.WriteLine("========================================\n");

        Console.WriteLine("?? WHAT HAPPENED:");
        Console.WriteLine("  ? ONE UniversalPatient model");
        Console.WriteLine("  ? THREE different message types (ADT, OMP, ORU)");
        Console.WriteLine("  ? ZERO errors for missing segments!");
        Console.WriteLine("  ? Collections = empty list (not null!)");
        Console.WriteLine("  ? Nullable fields = null (safe!)");
        Console.WriteLine();

        Console.WriteLine("?? DATA AVAILABILITY:");
        Console.WriteLine("  • Demographics: Present in ALL messages ?");
        Console.WriteLine("  • Allergies: Present in ALL messages ?");
        Console.WriteLine("  • Visit Info: ADT only (null/default in others) ?");
        Console.WriteLine("  • Diagnoses: ADT only (empty in others) ?");
        Console.WriteLine("  • Pharmacy: OMP only (empty in others) ?");
        Console.WriteLine("  • Lab Results: ORU only (empty in others) ?");
        Console.WriteLine();

        Console.WriteLine("?? KEY BENEFITS:");
        Console.WriteLine("  ? Aggregate patient data from ANY source");
        Console.WriteLine("  ? No need for separate models per message type");
        Console.WriteLine("  ? Safe handling of missing data");
        Console.WriteLine("  ? Collections auto-initialize to empty");
        Console.WriteLine("  ? Nullable types for optional fields");
        Console.WriteLine();

        Console.WriteLine("?? USE CASES:");
        Console.WriteLine("  ?? Patient data aggregation dashboard");
        Console.WriteLine("  ?? Multi-source patient record merging");
        Console.WriteLine("  ?? Real-time patient monitoring (any message type)");
        Console.WriteLine("  ?? Unified patient view across systems");
        Console.WriteLine();

        Console.WriteLine("========================================");
        Console.WriteLine("? KeryxPars - ONE MODEL TO RULE THEM ALL! ?");
        Console.WriteLine("========================================");
    }
}
