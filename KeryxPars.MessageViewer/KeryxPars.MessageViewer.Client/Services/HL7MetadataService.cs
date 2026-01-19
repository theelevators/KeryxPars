namespace KeryxPars.MessageViewer.Client.Services;

/// <summary>
/// Provides metadata information for HL7 segments, fields, and components
/// </summary>
public static class HL7MetadataService
{
    /// <summary>
    /// Get description for a segment
    /// </summary>
    public static string GetSegmentDescription(string segmentId)
    {
        return segmentId switch
        {
            "MSH" => "Message Header - Contains message routing and identification information",
            "EVN" => "Event Type - Describes the event that triggered the message",
            "PID" => "Patient Identification - Contains patient demographic information",
            "PD1" => "Patient Additional Demographic - Additional patient information",
            "PV1" => "Patient Visit - Contains visit/encounter information",
            "PV2" => "Patient Visit - Additional Info - Extended visit information",
            "NK1" => "Next of Kin / Associated Parties - Emergency contacts and related persons",
            "ORC" => "Common Order - Order control segment for placing/canceling orders",
            "OBR" => "Observation Request - Details about ordered observations/tests",
            "OBX" => "Observation/Result - Contains observation results and values",
            "AL1" => "Patient Allergy Information - Allergy and adverse reaction data",
            "DG1" => "Diagnosis - Diagnosis information for the patient",
            "RXE" => "Pharmacy/Treatment Encoded Order - Pharmacy order details",
            "RXA" => "Pharmacy/Treatment Administration - Medication administration record",
            "RXD" => "Pharmacy/Treatment Dispense - Medication dispense information",
            "RXG" => "Pharmacy/Treatment Give - Medication give information",
            "RXC" => "Pharmacy/Treatment Component Order - Components of compound drugs",
            "NTE" => "Notes and Comments - Free text notes and comments",
            "GT1" => "Guarantor - Financial guarantor information",
            "IN1" => "Insurance - Primary insurance information",
            "IN2" => "Insurance Additional Info - Extended insurance data",
            "PR1" => "Procedures - Procedure information",
            "DRG" => "Diagnosis Related Group - DRG information for billing",
            "ACC" => "Accident - Accident-related information",
            "ROL" => "Role - Functional role in a transaction",
            "MRG" => "Merge Patient Information - Patient merge data",
            "SPM" => "Specimen - Laboratory specimen information",
            "SAC" => "Specimen Container Detail - Container details for specimens",
            "SCH" => "Scheduling Activity Information - Appointment scheduling",
            "AIL" => "Appointment Information - Location Resource - Location for appointments",
            "AIP" => "Appointment Information - Personnel Resource - Personnel for appointments",
            "AIS" => "Appointment Information - Service - Service for appointments",
            "CTI" => "Clinical Trial Identification - Clinical trial information",
            "CTD" => "Contact Data - Contact information",
            "ODS" => "Dietary Orders, Supplements, and Preferences - Dietary orders",
            "ODT" => "Diet Tray Instructions - Tray-specific instructions",
            "QRD" => "Original-Style Query Definition - Query parameters",
            "QRF" => "Original Style Query Filter - Query filter criteria",
            "QPD" => "Query Parameter Definition - Query parameters (newer style)",
            "RCP" => "Response Control Parameter - Query response control",
            "DSC" => "Continuation Pointer - Message continuation indicator",
            "MSA" => "Message Acknowledgment - Acknowledgment of received message",
            "ERR" => "Error - Error information",
            _ => "HL7 Segment"
        };
    }

    /// <summary>
    /// Get description for a specific field in a segment
    /// </summary>
    public static string? GetFieldDescription(string segmentId, int fieldIndex)
    {
        return (segmentId, fieldIndex) switch
        {
            // MSH - Message Header
            ("MSH", 1) => "Field Separator (always |)",
            ("MSH", 2) => "Encoding Characters (^~\\&)",
            ("MSH", 3) => "Sending Application - Application that sent the message",
            ("MSH", 4) => "Sending Facility - Facility that sent the message",
            ("MSH", 5) => "Receiving Application - Intended receiving application",
            ("MSH", 6) => "Receiving Facility - Intended receiving facility",
            ("MSH", 7) => "Date/Time of Message - When message was created",
            ("MSH", 8) => "Security - Security information",
            ("MSH", 9) => "Message Type - Type of message (e.g., ADT^A01)",
            ("MSH", 10) => "Message Control ID - Unique message identifier",
            ("MSH", 11) => "Processing ID - P (Production), T (Training), D (Debugging)",
            ("MSH", 12) => "Version ID - HL7 version (e.g., 2.5)",
            ("MSH", 13) => "Sequence Number - Message sequence number",
            ("MSH", 14) => "Continuation Pointer - For message continuation",
            ("MSH", 15) => "Accept Acknowledgment Type - When to send ACK",
            ("MSH", 16) => "Application Acknowledgment Type - Application ACK requirements",
            ("MSH", 17) => "Country Code - Country of origin",
            ("MSH", 18) => "Character Set - Character encoding used",
            ("MSH", 19) => "Principal Language of Message",
            ("MSH", 20) => "Alternate Character Set Handling Scheme",
            ("MSH", 21) => "Message Profile Identifier - Conformance profile",

            // EVN - Event Type
            ("EVN", 1) => "Event Type Code - Type of event (e.g., A01)",
            ("EVN", 2) => "Recorded Date/Time - When event was recorded",
            ("EVN", 3) => "Date/Time Planned Event - Scheduled time",
            ("EVN", 4) => "Event Reason Code - Why event occurred",
            ("EVN", 5) => "Operator ID - Who triggered the event",
            ("EVN", 6) => "Event Occurred - When event actually happened",
            ("EVN", 7) => "Event Facility - Where event occurred",

            // PID - Patient Identification
            ("PID", 1) => "Set ID - Sequence number for multiple PID segments",
            ("PID", 2) => "Patient ID (deprecated) - Use PID-3 instead",
            ("PID", 3) => "Patient Identifier List - Primary patient identifiers (MRN, etc.)",
            ("PID", 4) => "Alternate Patient ID - Alternative identifiers",
            ("PID", 5) => "Patient Name - Legal name (Last^First^Middle)",
            ("PID", 6) => "Mother's Maiden Name - Mother's birth surname",
            ("PID", 7) => "Date/Time of Birth - Patient's birth date (YYYYMMDD)",
            ("PID", 8) => "Administrative Sex - M (Male), F (Female), O (Other), U (Unknown)",
            ("PID", 9) => "Patient Alias - Other names patient goes by",
            ("PID", 10) => "Race - Patient's race",
            ("PID", 11) => "Patient Address - Current residential address",
            ("PID", 12) => "County Code - County of residence",
            ("PID", 13) => "Phone Number - Home - Home phone number",
            ("PID", 14) => "Phone Number - Business - Work phone number",
            ("PID", 15) => "Primary Language - Patient's preferred language",
            ("PID", 16) => "Marital Status - S (Single), M (Married), D (Divorced), W (Widowed)",
            ("PID", 17) => "Religion - Patient's religious affiliation",
            ("PID", 18) => "Patient Account Number - Billing account number",
            ("PID", 19) => "SSN Number - Patient - Social Security Number",
            ("PID", 20) => "Driver's License Number - Patient",
            ("PID", 21) => "Mother's Identifier - Mother's patient ID",
            ("PID", 22) => "Ethnic Group - Patient's ethnicity",
            ("PID", 23) => "Birth Place - Where patient was born",
            ("PID", 24) => "Multiple Birth Indicator - Y/N for twins, triplets",
            ("PID", 25) => "Birth Order - Birth sequence for multiples",
            ("PID", 26) => "Citizenship - Patient's citizenship",
            ("PID", 27) => "Veterans Military Status",
            ("PID", 28) => "Nationality - Patient's nationality",
            ("PID", 29) => "Patient Death Date and Time",
            ("PID", 30) => "Patient Death Indicator - Y/N",

            // PD1 - Patient Additional Demographic
            ("PD1", 1) => "Living Dependency - Spouse dependent, etc.",
            ("PD1", 2) => "Living Arrangement - Alone, family, institution",
            ("PD1", 3) => "Patient Primary Facility - Preferred facility",
            ("PD1", 4) => "Patient Primary Care Provider - PCP information",
            ("PD1", 5) => "Student Indicator - Y/N",
            ("PD1", 6) => "Handicap - Disability information",
            ("PD1", 7) => "Living Will Code - Advance directive status",
            ("PD1", 8) => "Organ Donor Code - Organ donation preference",
            ("PD1", 9) => "Separate Bill - Billing preference",
            ("PD1", 10) => "Duplicate Patient - Duplicate MRN indicator",
            ("PD1", 11) => "Publicity Code - Release of information",
            ("PD1", 12) => "Protection Indicator - VIP, confidential",

            // PV1 - Patient Visit
            ("PV1", 1) => "Set ID - Sequence number",
            ("PV1", 2) => "Patient Class - E (Emergency), I (Inpatient), O (Outpatient), P (Preadmit)",
            ("PV1", 3) => "Assigned Patient Location - Room/Bed (Building^Room^Bed)",
            ("PV1", 4) => "Admission Type - Elective, Emergency, etc.",
            ("PV1", 5) => "Preadmit Number - Pre-registration number",
            ("PV1", 6) => "Prior Patient Location - Previous room/bed",
            ("PV1", 7) => "Attending Doctor - Primary attending physician",
            ("PV1", 8) => "Referring Doctor - Referring physician",
            ("PV1", 9) => "Consulting Doctor - Consulting physicians",
            ("PV1", 10) => "Hospital Service - Medical, Surgical, etc.",
            ("PV1", 11) => "Temporary Location - Temporary location assignment",
            ("PV1", 12) => "Preadmit Test Indicator - Pre-admission testing",
            ("PV1", 13) => "Re-admission Indicator - Readmission flag",
            ("PV1", 14) => "Admit Source - Where patient came from",
            ("PV1", 15) => "Ambulatory Status - Ambulatory, wheelchair, etc.",
            ("PV1", 16) => "VIP Indicator - VIP status",
            ("PV1", 17) => "Admitting Doctor - Doctor who admitted patient",
            ("PV1", 18) => "Patient Type - Type of patient",
            ("PV1", 19) => "Visit Number - Unique visit/encounter identifier",
            ("PV1", 20) => "Financial Class - Payment category",
            ("PV1", 36) => "Discharge Disposition - Where patient went",
            ("PV1", 37) => "Discharged to Location - Specific discharge location",
            ("PV1", 38) => "Diet Type - Dietary restrictions",
            ("PV1", 39) => "Servicing Facility - Which facility",
            ("PV1", 44) => "Admit Date/Time - When patient was admitted",
            ("PV1", 45) => "Discharge Date/Time - When patient was discharged",

            // PV2 - Patient Visit - Additional
            ("PV2", 1) => "Prior Pending Location - Previous pending location",
            ("PV2", 2) => "Accommodation Code - Room accommodation level",
            ("PV2", 3) => "Admit Reason - Reason for admission",
            ("PV2", 4) => "Transfer Reason - Reason for transfer",
            ("PV2", 5) => "Patient Valuables - Valuables location",
            ("PV2", 6) => "Patient Valuables Location",
            ("PV2", 7) => "Visit User Code - User-defined visit code",
            ("PV2", 8) => "Expected Admit Date/Time",
            ("PV2", 9) => "Expected Discharge Date/Time",
            ("PV2", 10) => "Estimated Length of Inpatient Stay",
            ("PV2", 11) => "Actual Length of Inpatient Stay",
            ("PV2", 12) => "Visit Description",
            ("PV2", 13) => "Referral Source Code",
            ("PV2", 14) => "Previous Service Date",
            ("PV2", 15) => "Employment Illness Related Indicator",

            // NK1 - Next of Kin
            ("NK1", 1) => "Set ID - Sequence number",
            ("NK1", 2) => "Name - Next of kin/contact name",
            ("NK1", 3) => "Relationship - Relationship to patient (spouse, parent, etc.)",
            ("NK1", 4) => "Address - Contact address",
            ("NK1", 5) => "Phone Number - Contact phone",
            ("NK1", 6) => "Business Phone Number",
            ("NK1", 7) => "Contact Role - Emergency contact, power of attorney, etc.",
            ("NK1", 8) => "Start Date - When relationship began",
            ("NK1", 9) => "End Date - When relationship ended",
            ("NK1", 10) => "Next of Kin/Associated Parties Job Title",
            ("NK1", 11) => "Next of Kin/Associated Parties Job Code",
            ("NK1", 12) => "Next of Kin/Associated Parties Employee Number",
            ("NK1", 13) => "Organization Name",
            ("NK1", 14) => "Marital Status",
            ("NK1", 15) => "Administrative Sex",

            // ORC - Common Order
            ("ORC", 1) => "Order Control - NW (New), CA (Cancel), OC (Order Cancelled), etc.",
            ("ORC", 2) => "Placer Order Number - Ordering system's order ID",
            ("ORC", 3) => "Filler Order Number - Fulfilling system's order ID",
            ("ORC", 4) => "Placer Group Number - Group of related orders",
            ("ORC", 5) => "Order Status - IP (In Progress), CM (Complete), CA (Cancelled)",
            ("ORC", 6) => "Response Flag - How quickly response needed",
            ("ORC", 7) => "Quantity/Timing - When and how often",
            ("ORC", 8) => "Parent Order - Parent order reference",
            ("ORC", 9) => "Date/Time of Transaction - When order was placed/modified",
            ("ORC", 10) => "Entered By - Who entered the order",
            ("ORC", 11) => "Verified By - Who verified the order",
            ("ORC", 12) => "Ordering Provider - Who placed the order",
            ("ORC", 13) => "Enterer's Location - Where order was entered",
            ("ORC", 14) => "Call Back Phone Number",
            ("ORC", 15) => "Order Effective Date/Time - When order becomes active",
            ("ORC", 16) => "Order Control Code Reason",
            ("ORC", 17) => "Entering Organization",
            ("ORC", 18) => "Entering Device",
            ("ORC", 19) => "Action By - Who performed action",

            // OBR - Observation Request
            ("OBR", 1) => "Set ID - Sequence number",
            ("OBR", 2) => "Placer Order Number - Ordering system's order ID",
            ("OBR", 3) => "Filler Order Number - Lab/fulfilling system's order ID",
            ("OBR", 4) => "Universal Service Identifier - Test/procedure code and name",
            ("OBR", 5) => "Priority - STAT, Routine, ASAP",
            ("OBR", 6) => "Requested Date/Time - When test was requested",
            ("OBR", 7) => "Observation Date/Time - When specimen collected/observation made",
            ("OBR", 8) => "Observation End Date/Time",
            ("OBR", 9) => "Collection Volume - Amount collected",
            ("OBR", 10) => "Collector Identifier - Who collected specimen",
            ("OBR", 11) => "Specimen Action Code - Action taken on specimen",
            ("OBR", 12) => "Danger Code - Hazard warnings",
            ("OBR", 13) => "Relevant Clinical Information - Clinical context",
            ("OBR", 14) => "Specimen Received Date/Time",
            ("OBR", 15) => "Specimen Source - Where specimen came from",
            ("OBR", 16) => "Ordering Provider - Who ordered the test",
            ("OBR", 17) => "Order Callback Phone Number",
            ("OBR", 18) => "Placer Field 1 - Custom field",
            ("OBR", 19) => "Placer Field 2 - Custom field",
            ("OBR", 20) => "Filler Field 1 - Custom field",
            ("OBR", 21) => "Filler Field 2 - Custom field",
            ("OBR", 22) => "Results Rpt/Status Change - Date/Time",
            ("OBR", 23) => "Charge to Practice",
            ("OBR", 24) => "Diagnostic Service Section ID",
            ("OBR", 25) => "Result Status - F (Final), P (Preliminary), C (Corrected), X (Cannot obtain)",
            ("OBR", 26) => "Parent Result - Link to parent result",
            ("OBR", 27) => "Quantity/Timing - When to perform",
            ("OBR", 28) => "Result Copies To - Who gets results",
            ("OBR", 29) => "Parent Number - Parent observation",
            ("OBR", 30) => "Transportation Mode",
            ("OBR", 31) => "Reason for Study - Clinical indication",
            ("OBR", 32) => "Principal Result Interpreter",

            // OBX - Observation Result
            ("OBX", 1) => "Set ID - Sequence number for this observation",
            ("OBX", 2) => "Value Type - NM (Numeric), ST (String), CE (Coded), TX (Text), etc.",
            ("OBX", 3) => "Observation Identifier - LOINC code for what was measured",
            ("OBX", 4) => "Observation Sub-ID - Sub-identifier for repeating results",
            ("OBX", 5) => "Observation Value - The actual result value",
            ("OBX", 6) => "Units - Units of measurement (mg/dL, mmol/L, etc.)",
            ("OBX", 7) => "References Range - Normal range for this observation",
            ("OBX", 8) => "Abnormal Flags - H (High), L (Low), HH (Critical High), LL (Critical Low), N (Normal)",
            ("OBX", 9) => "Probability - Statistical probability",
            ("OBX", 10) => "Nature of Abnormal Test",
            ("OBX", 11) => "Observation Result Status - F (Final), P (Preliminary), C (Corrected), X (Cannot obtain)",
            ("OBX", 12) => "Effective Date of Reference Range",
            ("OBX", 13) => "User Defined Access Checks",
            ("OBX", 14) => "Date/Time of the Observation",
            ("OBX", 15) => "Producer's ID - Lab/device that produced result",
            ("OBX", 16) => "Responsible Observer - Who performed observation",
            ("OBX", 17) => "Observation Method - Method used for observation",
            ("OBX", 18) => "Equipment Instance Identifier",
            ("OBX", 19) => "Date/Time of Analysis",

            // AL1 - Patient Allergy Information
            ("AL1", 1) => "Set ID - Sequence number",
            ("AL1", 2) => "Allergen Type Code - DA (Drug), FA (Food), MA (Miscellaneous), EA (Environmental)",
            ("AL1", 3) => "Allergen Code/Mnemonic/Description - Specific allergen (Penicillin, Peanuts, etc.)",
            ("AL1", 4) => "Allergy Severity Code - MI (Mild), MO (Moderate), SV (Severe)",
            ("AL1", 5) => "Allergy Reaction Code - Reaction type (Rash, Anaphylaxis, etc.)",
            ("AL1", 6) => "Identification Date - When allergy was identified",

            // DG1 - Diagnosis
            ("DG1", 1) => "Set ID - Sequence number",
            ("DG1", 2) => "Diagnosis Coding Method - ICD-10-CM, ICD-9-CM, SNOMED",
            ("DG1", 3) => "Diagnosis Code - ICD-10 or other diagnosis code",
            ("DG1", 4) => "Diagnosis Description - Text description of diagnosis",
            ("DG1", 5) => "Diagnosis Date/Time - When diagnosis was made",
            ("DG1", 6) => "Diagnosis Type - A (Admitting), W (Working), F (Final)",
            ("DG1", 7) => "Major Diagnostic Category",
            ("DG1", 8) => "Diagnostic Related Group",
            ("DG1", 9) => "DRG Approval Indicator",
            ("DG1", 10) => "DRG Grouper Review Code",
            ("DG1", 15) => "Diagnosis Priority - Primary, secondary, etc.",
            ("DG1", 16) => "Diagnosing Clinician - Who made diagnosis",
            ("DG1", 17) => "Diagnosis Classification",
            ("DG1", 18) => "Confidential Indicator",
            ("DG1", 19) => "Attestation Date/Time",

            // IN1 - Insurance
            ("IN1", 1) => "Set ID - Sequence number",
            ("IN1", 2) => "Insurance Plan ID - Insurance company identifier",
            ("IN1", 3) => "Insurance Company ID - Payer ID",
            ("IN1", 4) => "Insurance Company Name - Name of insurance company",
            ("IN1", 5) => "Insurance Company Address",
            ("IN1", 6) => "Insurance Company Contact Person",
            ("IN1", 7) => "Insurance Company Phone Number",
            ("IN1", 8) => "Group Number - Policy group number",
            ("IN1", 9) => "Group Name - Group or employer name",
            ("IN1", 10) => "Insured's Group Employer ID",
            ("IN1", 11) => "Insured's Group Employer Name",
            ("IN1", 12) => "Plan Effective Date - When coverage started",
            ("IN1", 13) => "Plan Expiration Date - When coverage ends",
            ("IN1", 14) => "Authorization Information",
            ("IN1", 15) => "Plan Type - HMO, PPO, etc.",
            ("IN1", 16) => "Name of Insured - Policyholder name",
            ("IN1", 17) => "Insured's Relationship to Patient - Self, Spouse, Child",
            ("IN1", 18) => "Insured's Date of Birth",
            ("IN1", 19) => "Insured's Address",
            ("IN1", 20) => "Assignment of Benefits - Y/N",
            ("IN1", 22) => "Coordination of Benefits - Primary, Secondary",
            ("IN1", 35) => "Company Plan Code",
            ("IN1", 36) => "Policy Number - Insurance policy number",

            // IN2 - Insurance Additional Info
            ("IN2", 1) => "Insured's Employee ID",
            ("IN2", 2) => "Insured's Social Security Number",
            ("IN2", 3) => "Insured's Employer's Name and ID",
            ("IN2", 4) => "Employer Information Data",
            ("IN2", 5) => "Mail Claim Party",
            ("IN2", 6) => "Medicare Health Insurance Card Number",
            ("IN2", 7) => "Medicaid Case Name",
            ("IN2", 8) => "Medicaid Case Number",
            ("IN2", 9) => "Military Sponsor Name",
            ("IN2", 10) => "Military ID Number",
            ("IN2", 25) => "Payor Subscriber ID",
            ("IN2", 61) => "Patient Member Number",

            // GT1 - Guarantor
            ("GT1", 1) => "Set ID - Sequence number",
            ("GT1", 2) => "Guarantor Number - Guarantor identifier",
            ("GT1", 3) => "Guarantor Name - Name of financial guarantor",
            ("GT1", 4) => "Guarantor Spouse Name",
            ("GT1", 5) => "Guarantor Address",
            ("GT1", 6) => "Guarantor Phone Number - Home",
            ("GT1", 7) => "Guarantor Phone Number - Business",
            ("GT1", 8) => "Guarantor Date/Time of Birth",
            ("GT1", 9) => "Guarantor Administrative Sex",
            ("GT1", 10) => "Guarantor Type - Individual, Organization",
            ("GT1", 11) => "Guarantor Relationship - Relationship to patient",
            ("GT1", 12) => "Guarantor SSN",
            ("GT1", 13) => "Guarantor Date - Begin",
            ("GT1", 14) => "Guarantor Date - End",
            ("GT1", 15) => "Guarantor Priority",
            ("GT1", 16) => "Guarantor Employer Name",
            ("GT1", 17) => "Guarantor Employer Address",

            // PR1 - Procedures
            ("PR1", 1) => "Set ID - Sequence number",
            ("PR1", 2) => "Procedure Coding Method - ICD-10-PCS, CPT",
            ("PR1", 3) => "Procedure Code - CPT or ICD-10-PCS code",
            ("PR1", 4) => "Procedure Description - Text description",
            ("PR1", 5) => "Procedure Date/Time - When procedure performed",
            ("PR1", 6) => "Procedure Functional Type",
            ("PR1", 7) => "Procedure Minutes - Duration",
            ("PR1", 8) => "Anesthesiologist - Anesthesia provider",
            ("PR1", 9) => "Anesthesia Code",
            ("PR1", 10) => "Anesthesia Minutes",
            ("PR1", 11) => "Surgeon - Primary surgeon",
            ("PR1", 12) => "Procedure Practitioner",
            ("PR1", 16) => "Procedure Code Modifier - CPT modifiers",

            // RXE - Pharmacy/Treatment Encoded Order
            ("RXE", 1) => "Quantity/Timing - Dosing schedule",
            ("RXE", 2) => "Give Code - Medication code and name",
            ("RXE", 3) => "Give Amount - Minimum - Dose amount",
            ("RXE", 4) => "Give Amount - Maximum",
            ("RXE", 5) => "Give Units - mg, mL, etc.",
            ("RXE", 6) => "Give Dosage Form - Tablet, Capsule, Solution",
            ("RXE", 7) => "Provider's Administration Instructions",
            ("RXE", 13) => "Give Per (Time Unit) - Per day, per dose",
            ("RXE", 14) => "Give Rate Amount",
            ("RXE", 15) => "Give Rate Units",
            ("RXE", 21) => "Give Indication - Reason for medication",

            // RXA - Pharmacy/Treatment Administration
            ("RXA", 1) => "Give Sub-ID Counter",
            ("RXA", 2) => "Administration Sub-ID Counter",
            ("RXA", 3) => "Date/Time Start of Administration",
            ("RXA", 4) => "Date/Time End of Administration",
            ("RXA", 5) => "Administered Code - What was given",
            ("RXA", 6) => "Administered Amount - Dose given",
            ("RXA", 7) => "Administered Units",
            ("RXA", 8) => "Administered Dosage Form",
            ("RXA", 9) => "Administration Notes",
            ("RXA", 10) => "Administering Provider - Who gave medication",
            ("RXA", 11) => "Administered-at Location",
            ("RXA", 15) => "Substance Lot Number - Lot/batch number",
            ("RXA", 16) => "Substance Expiration Date",
            ("RXA", 17) => "Substance Manufacturer Name",
            ("RXA", 20) => "Completion Status - CP (Complete), RE (Refused), NA (Not Administered)",

            // RXD - Pharmacy/Treatment Dispense
            ("RXD", 1) => "Dispense Sub-ID Counter",
            ("RXD", 2) => "Dispense/Give Code - What was dispensed",
            ("RXD", 3) => "Date/Time Dispensed",
            ("RXD", 4) => "Actual Dispense Amount",
            ("RXD", 5) => "Actual Dispense Units",
            ("RXD", 6) => "Actual Dosage Form",
            ("RXD", 7) => "Prescription Number",
            ("RXD", 8) => "Number of Refills Remaining",
            ("RXD", 9) => "Dispense Notes",
            ("RXD", 10) => "Dispensing Provider - Pharmacist",
            ("RXD", 11) => "Substitution Status - Substitution allowed/occurred",
            ("RXD", 15) => "Prescription Number - Rx number",
            ("RXD", 25) => "Dispense to Location",

            // RXG - Pharmacy/Treatment Give
            ("RXG", 1) => "Give Sub-ID Counter",
            ("RXG", 2) => "Dispense Sub-ID Counter",
            ("RXG", 3) => "Quantity/Timing",
            ("RXG", 4) => "Give Code - Medication code",
            ("RXG", 5) => "Give Amount - Minimum",
            ("RXG", 6) => "Give Amount - Maximum",
            ("RXG", 7) => "Give Units",
            ("RXG", 8) => "Give Dosage Form",
            ("RXG", 9) => "Administration Notes",

            // RXC - Pharmacy/Treatment Component
            ("RXC", 1) => "RX Component Type - Base, Additive",
            ("RXC", 2) => "Component Code - Component medication",
            ("RXC", 3) => "Component Amount - Amount of component",
            ("RXC", 4) => "Component Units",
            ("RXC", 5) => "Component Strength",
            ("RXC", 6) => "Component Strength Units",

            // SPM - Specimen
            ("SPM", 1) => "Set ID - Sequence number",
            ("SPM", 2) => "Specimen ID - Specimen identifier",
            ("SPM", 3) => "Specimen Parent IDs",
            ("SPM", 4) => "Specimen Type - Blood, Urine, Tissue, etc.",
            ("SPM", 5) => "Specimen Type Modifier",
            ("SPM", 6) => "Specimen Additives - Preservatives, anticoagulants",
            ("SPM", 7) => "Specimen Collection Method",
            ("SPM", 8) => "Specimen Source Site - Body site",
            ("SPM", 9) => "Specimen Source Site Modifier",
            ("SPM", 10) => "Specimen Collection Site",
            ("SPM", 11) => "Specimen Role - Patient, Quality Control, etc.",
            ("SPM", 12) => "Specimen Collection Amount",
            ("SPM", 14) => "Specimen Description",
            ("SPM", 17) => "Specimen Collection Date/Time",
            ("SPM", 18) => "Specimen Received Date/Time",
            ("SPM", 24) => "Specimen Condition - Hemolyzed, Clotted, etc.",

            // SAC - Specimen Container
            ("SAC", 1) => "External Accession Identifier",
            ("SAC", 2) => "Accession Identifier",
            ("SAC", 3) => "Container Identifier",
            ("SAC", 4) => "Primary Container Identifier",
            ("SAC", 5) => "Equipment Container Identifier",
            ("SAC", 6) => "Specimen Source",
            ("SAC", 7) => "Registration Date/Time",
            ("SAC", 8) => "Container Status - Available, Processing, etc.",
            ("SAC", 9) => "Carrier Type - Tube, Vial, etc.",
            ("SAC", 10) => "Carrier Identifier",
            ("SAC", 15) => "Barrier Delta",
            ("SAC", 16) => "Bottom Delta",
            ("SAC", 17) => "Container Height/Diameter/Delta Units",
            ("SAC", 18) => "Container Volume",
            ("SAC", 19) => "Available Specimen Volume",

            // NTE - Notes and Comments
            ("NTE", 1) => "Set ID - Sequence number",
            ("NTE", 2) => "Source of Comment - Who/what is commenting",
            ("NTE", 3) => "Comment - The actual note text",
            ("NTE", 4) => "Comment Type - Generic, Patient Instructions, etc.",

            // DRG - Diagnosis Related Group
            ("DRG", 1) => "Diagnostic Related Group",
            ("DRG", 2) => "DRG Assigned Date/Time",
            ("DRG", 3) => "DRG Approval Indicator",
            ("DRG", 4) => "DRG Grouper Review Code",
            ("DRG", 5) => "Outlier Type",
            ("DRG", 6) => "Outlier Days",
            ("DRG", 7) => "Outlier Cost",
            ("DRG", 8) => "DRG Payor",

            // SCH - Scheduling Activity
            ("SCH", 1) => "Placer Appointment ID",
            ("SCH", 2) => "Filler Appointment ID",
            ("SCH", 3) => "Occurrence Number",
            ("SCH", 4) => "Placer Group Number",
            ("SCH", 5) => "Schedule ID",
            ("SCH", 6) => "Event Reason - Reason for appointment",
            ("SCH", 7) => "Appointment Reason - Clinical reason",
            ("SCH", 8) => "Appointment Type - Office visit, procedure, etc.",
            ("SCH", 11) => "Appointment Timing/Quantity - When and duration",
            ("SCH", 16) => "Filler Contact Person",
            ("SCH", 17) => "Filler Contact Phone Number",
            ("SCH", 25) => "Filler Status Code - Booked, Pending, etc.",

            // AIL - Appointment Information - Location
            ("AIL", 1) => "Set ID",
            ("AIL", 2) => "Segment Action Code",
            ("AIL", 3) => "Location Resource ID - Specific location/room",
            ("AIL", 4) => "Location Type - Room type",
            ("AIL", 5) => "Location Group",
            ("AIL", 12) => "Status Code - Available, In Use, etc.",

            // AIP - Appointment Information - Personnel
            ("AIP", 1) => "Set ID",
            ("AIP", 2) => "Segment Action Code",
            ("AIP", 3) => "Personnel Resource ID - Staff member",
            ("AIP", 4) => "Resource Type - Physician, Nurse, etc.",
            ("AIP", 5) => "Resource Group",
            ("AIP", 12) => "Status Code",

            // AIS - Appointment Information - Service
            ("AIS", 1) => "Set ID",
            ("AIS", 2) => "Segment Action Code",
            ("AIS", 3) => "Universal Service Identifier - Service to be performed",
            ("AIS", 4) => "Start Date/Time",
            ("AIS", 11) => "Status Code",

            // ACC - Accident
            ("ACC", 1) => "Accident Date/Time",
            ("ACC", 2) => "Accident Code - Type of accident",
            ("ACC", 3) => "Accident Location",
            ("ACC", 4) => "Auto Accident State",
            ("ACC", 5) => "Accident Job Related Indicator",
            ("ACC", 6) => "Accident Death Indicator",

            // ROL - Role
            ("ROL", 1) => "Role Instance ID",
            ("ROL", 2) => "Action Code",
            ("ROL", 3) => "Role - Role in transaction",
            ("ROL", 4) => "Role Person - Who has this role",
            ("ROL", 5) => "Role Begin Date/Time",
            ("ROL", 6) => "Role End Date/Time",
            ("ROL", 7) => "Role Duration",

            // MRG - Merge Patient Information
            ("MRG", 1) => "Prior Patient Identifier List - Old patient IDs",
            ("MRG", 2) => "Prior Alternate Patient ID",
            ("MRG", 3) => "Prior Patient Account Number",
            ("MRG", 4) => "Prior Patient ID",
            ("MRG", 5) => "Prior Visit Number",
            ("MRG", 6) => "Prior Alternate Visit ID",
            ("MRG", 7) => "Prior Patient Name",

            // CTI - Clinical Trial Identification
            ("CTI", 1) => "Sponsor Study ID - Study identifier",
            ("CTI", 2) => "Study Phase Identifier",
            ("CTI", 3) => "Study Scheduled Time Point",

            // CTD - Contact Data
            ("CTD", 1) => "Contact Role - Role of contact",
            ("CTD", 2) => "Contact Name",
            ("CTD", 3) => "Contact Address",
            ("CTD", 4) => "Contact Location",
            ("CTD", 5) => "Contact Communication Information",
            ("CTD", 6) => "Preferred Method of Contact",

            // ODS - Dietary Orders, Supplements, and Preferences
            ("ODS", 1) => "Type - Diet type",
            ("ODS", 2) => "Service Period - Meal period",
            ("ODS", 3) => "Diet, Supplement, or Preference Code",
            ("ODS", 4) => "Text Instruction",

            // ODT - Diet Tray Instructions
            ("ODT", 1) => "Tray Type - Regular, Special, etc.",
            ("ODT", 2) => "Service Period",
            ("ODT", 3) => "Text Instruction",

            // QRD - Query Definition
            ("QRD", 1) => "Query Date/Time",
            ("QRD", 2) => "Query Format Code - Response format",
            ("QRD", 3) => "Query Priority - I (Immediate), D (Deferred)",
            ("QRD", 4) => "Query ID",
            ("QRD", 7) => "Quantity Limited Request",
            ("QRD", 8) => "Who Subject Filter - Patient ID",
            ("QRD", 9) => "What Subject Filter - What data",
            ("QRD", 10) => "What Department Data Code",
            ("QRD", 12) => "Query Results Level",

            // QRF - Query Filter
            ("QRF", 1) => "Where Subject Filter - Location criteria",
            ("QRF", 2) => "When Data Start Date/Time",
            ("QRF", 3) => "When Data End Date/Time",
            ("QRF", 4) => "What User Qualifier",
            ("QRF", 5) => "Other QRY Subject Filter",

            // QPD - Query Parameter Definition
            ("QPD", 1) => "Message Query Name - Type of query",
            ("QPD", 2) => "Query Tag - Unique query identifier",
            ("QPD", 3) => "Demographics Fields - What to return",

            // RCP - Response Control Parameter
            ("RCP", 1) => "Query Priority - Response urgency",
            ("RCP", 2) => "Quantity Limited Request - Max records",
            ("RCP", 3) => "Response Modality",
            ("RCP", 4) => "Execution and Delivery Time",

            // DSC - Continuation Pointer
            ("DSC", 1) => "Continuation Pointer - Where to continue from",
            ("DSC", 2) => "Continuation Style - Fragmentation method",

            // MSA - Message Acknowledgment
            ("MSA", 1) => "Acknowledgment Code - AA (Accept), AE (Error), AR (Reject)",
            ("MSA", 2) => "Message Control ID - ID of message being ACKed",
            ("MSA", 3) => "Text Message - Error or info message",
            ("MSA", 4) => "Expected Sequence Number",
            ("MSA", 6) => "Error Condition - Error code if AE/AR",

            // ERR - Error
            ("ERR", 1) => "Error Code and Location",
            ("ERR", 2) => "Error Location - Where error occurred",
            ("ERR", 3) => "HL7 Error Code - Standard HL7 error code",
            ("ERR", 4) => "Severity - E (Error), W (Warning), I (Info)",
            ("ERR", 5) => "Application Error Code",
            ("ERR", 6) => "Application Error Parameter",
            ("ERR", 7) => "Diagnostic Information - Detailed error info",
            ("ERR", 8) => "User Message - User-friendly error message",

            _ => null
        };
    }


    /// <summary>
    /// Get description for components within a field
    /// </summary>
    public static string? GetComponentDescription(string segmentId, int fieldIndex, int componentIndex)
    {
        return (segmentId, fieldIndex, componentIndex) switch
        {
            // MSH.9 - Message Type components
            ("MSH", 9, 1) => "Message Code (ADT=Admit/Discharge/Transfer, ORU=Observation Result, ORM=Order, etc.)",
            ("MSH", 9, 2) => "Trigger Event (A01=Admit, A03=Discharge, A08=Update, R01=Observation Report, etc.)",
            ("MSH", 9, 3) => "Message Structure (ADT_A01, ORU_R01, etc.)",

            // PID.3 - Patient Identifier List components
            ("PID", 3, 1) => "ID Number - The actual patient identifier (MRN)",
            ("PID", 3, 2) => "Check Digit - Verification digit",
            ("PID", 3, 3) => "Check Digit Scheme - Algorithm used (M10, M11, ISO)",
            ("PID", 3, 4) => "Assigning Authority - Organization that assigned ID",
            ("PID", 3, 5) => "Identifier Type Code - MR (Medical Record), PI (Patient Internal), etc.",
            ("PID", 3, 6) => "Assigning Facility - Facility that assigned the ID",

            // PID.5 - Patient Name components
            ("PID", 5, 1) => "Family Name (Last Name/Surname)",
            ("PID", 5, 2) => "Given Name (First Name)",
            ("PID", 5, 3) => "Middle Name or Initial",
            ("PID", 5, 4) => "Suffix (Jr., Sr., III, IV, etc.)",
            ("PID", 5, 5) => "Prefix (Dr., Mr., Ms., Mrs., etc.)",
            ("PID", 5, 6) => "Degree (MD, PhD, RN, etc.)",
            ("PID", 5, 7) => "Name Type Code - L (Legal), A (Alias), M (Maiden)",
            ("PID", 5, 8) => "Name Representation Code",

            // PID.11 - Patient Address components
            ("PID", 11, 1) => "Street Address - Number and street name",
            ("PID", 11, 2) => "Other Designation - Apartment, Suite, Building",
            ("PID", 11, 3) => "City",
            ("PID", 11, 4) => "State or Province - Two-letter state code",
            ("PID", 11, 5) => "Zip or Postal Code",
            ("PID", 11, 6) => "Country - ISO country code",
            ("PID", 11, 7) => "Address Type - H (Home), O (Office), B (Business)",
            ("PID", 11, 8) => "Other Geographic Designation",
            ("PID", 11, 9) => "County/Parish Code",

            // PID.13, PID.14 - Phone Number components
            ("PID", 13, 1) => "Telephone Number - Full phone number",
            ("PID", 13, 2) => "Telecommunication Use Code - PRN (Primary), ORN (Other)",
            ("PID", 13, 3) => "Telecommunication Equipment Type - PH (Phone), CP (Cell), etc.",
            ("PID", 14, 1) => "Telephone Number",
            ("PID", 14, 2) => "Telecommunication Use Code",
            ("PID", 14, 3) => "Telecommunication Equipment Type",

            // PV1.3 - Assigned Patient Location components
            ("PV1", 3, 1) => "Point of Care - Nursing unit, ward",
            ("PV1", 3, 2) => "Room - Room number",
            ("PV1", 3, 3) => "Bed - Bed identifier",
            ("PV1", 3, 4) => "Facility - Building or facility",
            ("PV1", 3, 5) => "Location Status - Active, Inactive",
            ("PV1", 3, 6) => "Person Location Type",
            ("PV1", 3, 7) => "Building - Building identifier",
            ("PV1", 3, 8) => "Floor - Floor number",
            ("PV1", 3, 9) => "Location Description",

            // PV1.19 - Visit Number components
            ("PV1", 19, 1) => "ID Number - Visit/Encounter number",
            ("PV1", 19, 2) => "Check Digit",
            ("PV1", 19, 3) => "Check Digit Scheme",
            ("PV1", 19, 4) => "Assigning Authority",
            ("PV1", 19, 5) => "Identifier Type Code - VN (Visit Number)",

            // NK1.2 - Next of Kin Name components
            ("NK1", 2, 1) => "Family Name (Last Name)",
            ("NK1", 2, 2) => "Given Name (First Name)",
            ("NK1", 2, 3) => "Middle Name or Initial",
            ("NK1", 2, 4) => "Suffix",
            ("NK1", 2, 5) => "Prefix",
            ("NK1", 2, 6) => "Degree",

            // NK1.3 - Relationship components
            ("NK1", 3, 1) => "Identifier - Relationship code",
            ("NK1", 3, 2) => "Text - Relationship description (Spouse, Parent, Child, Sibling, etc.)",
            ("NK1", 3, 3) => "Name of Coding System - HL7, SNOMED, etc.",

            // NK1.4 - Address components (same as PID.11)
            ("NK1", 4, 1) => "Street Address",
            ("NK1", 4, 2) => "Other Designation",
            ("NK1", 4, 3) => "City",
            ("NK1", 4, 4) => "State or Province",
            ("NK1", 4, 5) => "Zip or Postal Code",
            ("NK1", 4, 6) => "Country",

            // OBR.4 - Universal Service Identifier components
            ("OBR", 4, 1) => "Identifier - Test/procedure code (LOINC, CPT)",
            ("OBR", 4, 2) => "Text - Test/procedure name (CBC, Basic Metabolic Panel, etc.)",
            ("OBR", 4, 3) => "Name of Coding System - LN (LOINC), CPT, LOCAL",
            ("OBR", 4, 4) => "Alternate Identifier",
            ("OBR", 4, 5) => "Alternate Text",
            ("OBR", 4, 6) => "Name of Alternate Coding System",

            // OBX.3 - Observation Identifier components (same structure as OBR.4)
            ("OBX", 3, 1) => "Identifier - LOINC or local code for observation",
            ("OBX", 3, 2) => "Text - Name of observation (Glucose, Hemoglobin, etc.)",
            ("OBX", 3, 3) => "Name of Coding System - LN (LOINC), LOCAL",
            ("OBX", 3, 4) => "Alternate Identifier",
            ("OBX", 3, 5) => "Alternate Text",
            ("OBX", 3, 6) => "Name of Alternate Coding System",

            // OBX.5 - Observation Value (varies by OBX.2 type, but if CE/CWE)
            ("OBX", 5, 1) when IsCodedValueType(segmentId, fieldIndex) => "Identifier - Result code",
            ("OBX", 5, 2) when IsCodedValueType(segmentId, fieldIndex) => "Text - Result description",
            ("OBX", 5, 3) when IsCodedValueType(segmentId, fieldIndex) => "Name of Coding System",

            // AL1.3 - Allergen Code components
            ("AL1", 3, 1) => "Identifier - Allergen code (RxNorm, SNOMED)",
            ("AL1", 3, 2) => "Text - Allergen name (Penicillin, Peanuts, Latex, etc.)",
            ("AL1", 3, 3) => "Name of Coding System",

            // DG1.3 - Diagnosis Code components
            ("DG1", 3, 1) => "Identifier - ICD-10-CM diagnosis code",
            ("DG1", 3, 2) => "Text - Diagnosis description",
            ("DG1", 3, 3) => "Name of Coding System - I10 (ICD-10-CM), I9 (ICD-9-CM)",
            ("DG1", 3, 4) => "Alternate Identifier",
            ("DG1", 3, 5) => "Alternate Text",
            ("DG1", 3, 6) => "Name of Alternate Coding System",

            // ORC.2, ORC.3 - Order Number components
            ("ORC", 2, 1) => "Entity Identifier - Order number",
            ("ORC", 2, 2) => "Namespace ID - Ordering system identifier",
            ("ORC", 2, 3) => "Universal ID",
            ("ORC", 2, 4) => "Universal ID Type",
            ("ORC", 3, 1) => "Entity Identifier - Order number",
            ("ORC", 3, 2) => "Namespace ID - Fulfilling system identifier",
            ("ORC", 3, 3) => "Universal ID",
            ("ORC", 3, 4) => "Universal ID Type",

            // IN1.2 - Insurance Plan ID components
            ("IN1", 2, 1) => "Identifier - Plan code",
            ("IN1", 2, 2) => "Text - Plan name",
            ("IN1", 2, 3) => "Name of Coding System",

            // IN1.16 - Name of Insured components (same as patient name)
            ("IN1", 16, 1) => "Family Name (Last Name)",
            ("IN1", 16, 2) => "Given Name (First Name)",
            ("IN1", 16, 3) => "Middle Name or Initial",
            ("IN1", 16, 4) => "Suffix",
            ("IN1", 16, 5) => "Prefix",
            ("IN1", 16, 6) => "Degree",

            // IN1.17 - Insured's Relationship to Patient components
            ("IN1", 17, 1) => "Identifier - Relationship code",
            ("IN1", 17, 2) => "Text - Self, Spouse, Child, Other, etc.",
            ("IN1", 17, 3) => "Name of Coding System",

            // GT1.3 - Guarantor Name components (same as patient name)
            ("GT1", 3, 1) => "Family Name (Last Name)",
            ("GT1", 3, 2) => "Given Name (First Name)",
            ("GT1", 3, 3) => "Middle Name or Initial",
            ("GT1", 3, 4) => "Suffix",
            ("GT1", 3, 5) => "Prefix",
            ("GT1", 3, 6) => "Degree",

            // GT1.5 - Guarantor Address components (same as patient address)
            ("GT1", 5, 1) => "Street Address",
            ("GT1", 5, 2) => "Other Designation",
            ("GT1", 5, 3) => "City",
            ("GT1", 5, 4) => "State or Province",
            ("GT1", 5, 5) => "Zip or Postal Code",
            ("GT1", 5, 6) => "Country",

            // PR1.3 - Procedure Code components
            ("PR1", 3, 1) => "Identifier - CPT or ICD-10-PCS code",
            ("PR1", 3, 2) => "Text - Procedure description",
            ("PR1", 3, 3) => "Name of Coding System - CPT4, I10P (ICD-10-PCS)",

            // RXE.2, RXA.5, RXD.2, RXG.4 - Give Code (medication) components
            (_, _, 1) when IsMedicationCodeField(segmentId, fieldIndex) => "Identifier - Drug code (NDC, RxNorm)",
            (_, _, 2) when IsMedicationCodeField(segmentId, fieldIndex) => "Text - Medication name (Amoxicillin 500mg, Lisinopril 10mg, etc.)",
            (_, _, 3) when IsMedicationCodeField(segmentId, fieldIndex) => "Name of Coding System - NDC, RXNORM",
            (_, _, 4) when IsMedicationCodeField(segmentId, fieldIndex) => "Alternate Identifier",
            (_, _, 5) when IsMedicationCodeField(segmentId, fieldIndex) => "Alternate Text",
            (_, _, 6) when IsMedicationCodeField(segmentId, fieldIndex) => "Name of Alternate Coding System",

            // SPM.4 - Specimen Type components
            ("SPM", 4, 1) => "Identifier - Specimen type code",
            ("SPM", 4, 2) => "Text - Blood, Urine, Serum, Plasma, Tissue, etc.",
            ("SPM", 4, 3) => "Name of Coding System - SNOMED, HL7",

            // SPM.8 - Specimen Source Site components
            ("SPM", 8, 1) => "Identifier - Anatomic site code",
            ("SPM", 8, 2) => "Text - Arm, Leg, Abdomen, etc.",
            ("SPM", 8, 3) => "Name of Coding System - SNOMED",

            // Provider components (PV1.7, PV1.8, OBR.16, ORC.12, etc.)
            (_, _, 1) when IsProviderField(segmentId, fieldIndex) => "ID Number - Provider NPI or local ID",
            (_, _, 2) when IsProviderField(segmentId, fieldIndex) => "Family Name (Last Name)",
            (_, _, 3) when IsProviderField(segmentId, fieldIndex) => "Given Name (First Name)",
            (_, _, 4) when IsProviderField(segmentId, fieldIndex) => "Middle Name or Initial",
            (_, _, 5) when IsProviderField(segmentId, fieldIndex) => "Suffix (Jr., Sr., III)",
            (_, _, 6) when IsProviderField(segmentId, fieldIndex) => "Prefix (Dr., Mr., Ms.)",
            (_, _, 7) when IsProviderField(segmentId, fieldIndex) => "Degree (MD, DO, NP, PA, RN)",
            (_, _, 8) when IsProviderField(segmentId, fieldIndex) => "Source Table",
            (_, _, 9) when IsProviderField(segmentId, fieldIndex) => "Assigning Authority",

            // Timestamp components (common pattern for date/time fields)
            (_, _, 1) when IsTimestampField(segmentId, fieldIndex) => "Date/Time - YYYYMMDDHHMMSS format",
            (_, _, 2) when IsTimestampField(segmentId, fieldIndex) => "Degree of Precision",

            _ => null
        };
    }

    private static bool IsCodedValueType(string segmentId, int fieldIndex)
    {
        // This would ideally check OBX.2, but for simplicity we assume field 5 might be coded
        return segmentId == "OBX" && fieldIndex == 5;
    }

    private static bool IsMedicationCodeField(string segmentId, int fieldIndex)
    {
        return (segmentId, fieldIndex) switch
        {
            ("RXE", 2) => true,  // Give Code
            ("RXA", 5) => true,  // Administered Code
            ("RXD", 2) => true,  // Dispense/Give Code
            ("RXG", 4) => true,  // Give Code
            ("RXC", 2) => true,  // Component Code
            _ => false
        };
    }

    private static bool IsTimestampField(string segmentId, int fieldIndex)
    {
        return (segmentId, fieldIndex) switch
        {
            ("MSH", 7) => true,
            ("EVN", 2) => true,
            ("EVN", 6) => true,
            ("PID", 7) => true,
            ("PV1", 44) => true,
            ("PV1", 45) => true,
            ("OBR", 7) => true,
            ("ORC", 9) => true,
            ("RXA", 3) => true,
            ("RXA", 4) => true,
            ("RXD", 3) => true,
            _ => false
        };
    }


    private static bool IsProviderField(string segmentId, int fieldIndex)
    {
        return (segmentId, fieldIndex) switch
        {
            ("PV1", 7) => true,  // Attending Doctor
            ("PV1", 8) => true,  // Referring Doctor
            ("OBR", 16) => true, // Ordering Provider
            ("ORC", 12) => true, // Ordering Provider
            _ => false
        };
    }

    /// <summary>
    /// Get value type description
    /// </summary>
    public static string GetValueTypeDescription(string valueType)
    {
        return valueType switch
        {
            "NM" => "Numeric - A number",
            "ST" => "String - Text data",
            "TX" => "Text - Multi-line text",
            "FT" => "Formatted Text - Rich text",
            "DT" => "Date - YYYYMMDD",
            "TM" => "Time - HHMMSS",
            "TS" => "Timestamp - Date and time",
            "CE" => "Coded Element - Code with description",
            "CWE" => "Coded with Exceptions - Coded or text",
            "ID" => "Identifier - Coded value from table",
            "IS" => "Coded value for user-defined tables",
            "SI" => "Sequence ID - Sequential number",
            _ => valueType
        };
    }
}
