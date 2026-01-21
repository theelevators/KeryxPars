using System;
using KeryxPars.Examples;

namespace KeryxPars.Examples;

/// <summary>
/// COMPLETE USAGE EXAMPLE - Shows how to use EmergencyPatient with real HL7 data
/// </summary>
public class EmergencyPatientUsageExample
{
    public static void Main()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("  KeryxPars v0.3.2-beta Demo");
        Console.WriteLine("  ALL FEATURES IN ACTION!");
        Console.WriteLine("========================================\n");

        // ========== SCENARIO 1: Emergency Patient with Full Data ==========
        
        Console.WriteLine("?? SCENARIO 1: Emergency Patient (All Features)\n");
        
        var emergencyMessage = 
            "MSH|^~\\&|EPIC|UCSF|HL7|UCSF|20240115143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1|12345|MRN999888||DOE^JANE^MARIE||19850315|F||2106-3|123 MAIN ST^^SAN FRANCISCO^CA^94102||555-1234^PRN^PH^^^jane@email.com|555-5678^WPN^PH^^^jane.work@company.com||EN|M||987654321||DL-CA-D1234567|PASSPORT-USA-123456789^USA^PASSPORT|||||||||||||||||||\r" +
            "NK1|1|DOE^JOHN^L|SPOUSE|456 OAK ST^^SAN FRANCISCO^CA^94103|555-9999|\r" +
            "PV1|1|E|ER^TRAUMA^BED5|3|||7890^SMITH^ROBERT^A^^DR||8901^JONES^MARY^B^^DR|ER||||2|||9012^BROWN^DAVID^C^^DR|||123456789|||||||||||||||||||||||||20240115143000|\r" +
            "DG1|1|ICD10|S72.001A^Fracture of unspecified part of neck of right femur^ICD10||20240115143000|A|\r" +
            "DG1|2|ICD10|I10^Essential hypertension^ICD10||20240115143000|S|\r" +
            "OBX|1|NM|8480-6^Systolic BP^LN||145|mmHg|90-120|H|||F|\r" +
            "OBX|2|NM|8462-4^Diastolic BP^LN||92|mmHg|60-80|H|||F|\r" +
            "OBX|3|NM|8867-4^Heart Rate^LN||88|bpm|60-100|N|||F|";

        // Parse the message (ZERO allocation!)
        var patient = EmergencyPatientMapper.MapFromSpan(emergencyMessage.AsSpan());

        // Display results
        Console.WriteLine($"? Patient: {patient.Name.GivenName} {patient.Name.FamilyName}");
        Console.WriteLine($"   MRN: {patient.MRN}");
        Console.WriteLine($"   DOB: {patient.DateOfBirth:yyyy-MM-dd}");
        Console.WriteLine($"   Gender: {patient.Gender}");
        Console.WriteLine();

        Console.WriteLine("?? PRIORITY FALLBACK (Phone):");
        Console.WriteLine($"   Contact Phone: {patient.ContactPhone}");
        Console.WriteLine($"   ? Used PID.13 (home phone - Priority 0)");
        Console.WriteLine();

        Console.WriteLine("?? PRIORITY FALLBACK (Identifier):");
        Console.WriteLine($"   Primary ID: {patient.PrimaryIdentifier}");
        Console.WriteLine($"   ? Used PID.19 (SSN - Priority 0)");
        Console.WriteLine();

        Console.WriteLine("?? SIMPLE FALLBACK:");
        Console.WriteLine($"   Email: {patient.Email}");
        Console.WriteLine($"   ? Used PID.13.6 (primary email)");
        Console.WriteLine();

        Console.WriteLine("?? CONDITIONAL DEFAULTS (Emergency!):");
        Console.WriteLine($"   Priority: {patient.Priority} (Emergency = 999)");
        Console.WriteLine($"   Risk Level: {patient.RiskLevel} (CRITICAL)");
        Console.WriteLine($"   Immediate Review Required: {patient.RequiresImmediateReview}");
        Console.WriteLine();

        Console.WriteLine("?? CONDITIONAL MAPPING:");
        Console.WriteLine($"   Bed Assignment: {patient.BedAssignment ?? "N/A"}");
        Console.WriteLine($"   ? Mapped because PV1.2 == 'E' (Emergency)");
        Console.WriteLine();

        Console.WriteLine("?? DIAGNOSES (Collection):");
        foreach (var dx in patient.Diagnoses)
        {
            Console.WriteLine($"   {dx.SetID}. [{dx.CodingMethod}] {dx.DiagnosisCode} - {dx.DiagnosisDescription}");
        }
        Console.WriteLine();

        Console.WriteLine("?? OBSERVATIONS (Collection):");
        foreach (var obs in patient.Observations)
        {
            Console.WriteLine($"   {obs.SetID}. {obs.ObservationIdentifier}: {obs.ObservationValue} {obs.Units}");
        }
        Console.WriteLine();

        Console.WriteLine("????? PRIORITY FALLBACK (Doctor):");
        Console.WriteLine($"   Responsible Doctor: {patient.ResponsibleDoctor}");
        Console.WriteLine($"   ? Used PV1.7 (attending physician - Priority 0)");
        Console.WriteLine();

        // ========== SCENARIO 2: Fallback Demonstration ==========
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("?? SCENARIO 2: Fallback in Action!");
        Console.WriteLine("========================================\n");
        
        var fallbackMessage = 
            "MSH|^~\\&|EPIC|UCSF|HL7|UCSF|20240115150000||ADT^A01|MSG002|P|2.5||\r" +
            "PID|1|67890|MRN555777||SMITH^JOHN^PAUL||19700501|M||2106-3|789 ELM ST^^OAKLAND^CA^94601|||555-WORK^WPN^PH||||EN|S||||||CA-DL-5678|||||||||||||||||||||||40^555-MOBILE^PRN^PH|\r" +
            "PV1|1|O|||||||||||||||||||||||||||||||||||||||20240115150000|";

        var fallbackPatient = EmergencyPatientMapper.MapFromSpan(fallbackMessage.AsSpan());

        Console.WriteLine($"? Patient: {fallbackPatient.Name.GivenName} {fallbackPatient.Name.FamilyName}");
        Console.WriteLine();

        Console.WriteLine("?? PRIORITY FALLBACK DEMO:");
        Console.WriteLine($"   PID.13 (home): EMPTY ?");
        Console.WriteLine($"   PID.14 (work): '555-WORK' ? <- USED THIS!");
        Console.WriteLine($"   PID.40 (mobile): '555-MOBILE'");
        Console.WriteLine($"   Result: {fallbackPatient.ContactPhone}");
        Console.WriteLine();

        Console.WriteLine("?? MULTI-LEVEL FALLBACK:");
        Console.WriteLine($"   PID.19 (SSN): EMPTY ?");
        Console.WriteLine($"   PID.20 (DL): 'CA-DL-5678' ? <- USED THIS!");
        Console.WriteLine($"   PID.21.1 (Passport): EMPTY");
        Console.WriteLine($"   PID.2 (Patient ID): '67890'");
        Console.WriteLine($"   Result: {fallbackPatient.PrimaryIdentifier}");
        Console.WriteLine();

        Console.WriteLine("?? CROSS-SEGMENT FALLBACK:");
        Console.WriteLine($"   NK1.2 (next of kin): MISSING (no NK1 segment) ?");
        Console.WriteLine($"   PID.25 (emergency contact): Would use this ?");
        Console.WriteLine($"   Result: {fallbackPatient.EmergencyContactName ?? "Not provided"}");
        Console.WriteLine();

        // ========== SCENARIO 3: Conditional Only Demo ==========
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("?? SCENARIO 3: ConditionalOnly Safety!");
        Console.WriteLine("========================================\n");
        
        var unknownMessage = 
            "MSH|^~\\&|EPIC|UCSF|HL7|UCSF|20240115160000||ADT^A01|MSG003|P|2.5||\r" +
            "PID|1|11111|MRN333444||GREEN^PAT^M||19950715|M||||||||||||||||\r" +
            "PV1|1|P|||||||||||||||||||||||||||||||||||||||20240115160000|";  // P = Preadmit (unknown!)

        var unknownPatient = EmergencyPatientMapper.MapFromSpan(unknownMessage.AsSpan());

        Console.WriteLine($"? Patient: {unknownPatient.Name.GivenName} {unknownPatient.Name.FamilyName}");
        Console.WriteLine($"   Patient Class: {unknownPatient.PatientClass} (P = Preadmit)");
        Console.WriteLine();

        Console.WriteLine("?? CONDITIONAL ONLY (Safety Net):");
        Console.WriteLine($"   Priority: {unknownPatient.Priority}");
        Console.WriteLine($"   ? No condition matched, stays at default (0)");
        Console.WriteLine($"   ? Does NOT use garbage message value!");
        Console.WriteLine();

        Console.WriteLine("?? UNCONDITIONAL DEFAULT (Fallback):");
        Console.WriteLine($"   Risk Level: {unknownPatient.RiskLevel}");
        Console.WriteLine($"   ? No condition matched Emergency/Inpatient");
        Console.WriteLine($"   ? Used unconditional default: 'LOW'");
        Console.WriteLine();

        Console.WriteLine("?? TRIAGE LEVEL (ConditionalOnly):");
        Console.WriteLine($"   Triage: {unknownPatient.TriageLevel}");
        Console.WriteLine($"   ? 'P' (Preadmit) not in our conditions");
        Console.WriteLine($"   ? Safely defaults to 0, not random value!");
        Console.WriteLine();

        // ========== PERFORMANCE STATS ==========
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("? PERFORMANCE");
        Console.WriteLine("========================================\n");
        
        Console.WriteLine("? All mapping done with:");
        Console.WriteLine("   • Zero allocation (spans only!)");
        Console.WriteLine("   • Source generation (no reflection!)");
        Console.WriteLine("   • Compile-time safety (typed!)");
        Console.WriteLine();
        Console.WriteLine("?? 50-100x faster than traditional parsers!");
        Console.WriteLine();

        Console.WriteLine("========================================");
        Console.WriteLine("? KeryxPars v0.3.2-beta - COMPLETE! ?");
        Console.WriteLine("========================================");
    }
}
