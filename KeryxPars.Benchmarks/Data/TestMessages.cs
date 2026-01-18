namespace KeryxPars.Benchmarks.Data;

/// <summary>
/// Sample HL7 messages for benchmarking.
/// </summary>
public static class TestMessages
{
    /// <summary>
    /// Simple ADT^A01 message with basic patient demographics
    /// </summary>
    public const string SimpleADT = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";

    /// <summary>
    /// ADT message with allergies and diagnosis
    /// </summary>
    public const string ADTWithClinical = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG002|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JANE^M||19900215|F|||456 ELM ST^^CITY^ST^54321|||||||
PV1|1|I|ICU^201^A||||DOCTOR^ATTENDING|||||||||||
AL1|1|DA|1545^PENICILLIN^RX|SV|RASH
AL1|2|MA|^DUST^RX|MO|SNEEZING
DG1|1|I10|E11.9^Type 2 diabetes mellitus^I10||20230101|A|
DG1|2|I10|I10^Essential hypertension^I10||20230101|A|";

    /// <summary>
    /// Medication order (RDE^O11) message
    /// </summary>
    public const string MedicationOrder = @"MSH|^~\&|SENDING_APPLICATION|SENDING_FACILITY|RECEIVING_APPLICATION|RECEIVING_FACILITY|201305171259||RDE^O11|2244455|P|2.5||
PID|1||123456||DUCK^DAISY^L||19690912|F|||123 NORTHWOOD ST APT 9^^NEW CITY^NC^27262-9944|||||||
ORC|NW|ORD123456|FIL789012||||^^^20230101||20230101101530|ORDERING^PHYSICIAN||
RXO|00378-1805-10^Metformin 500mg Tab^NDC|500||MG|TABS|||
RXR|PO^Oral^HL70162|||
TQ1|1|BID||20230101|20230201|||";

    /// <summary>
    /// Complex medication order with multiple components
    /// </summary>
    public const string ComplexMedicationOrder = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||RDE^O11|MSG003|P|2.5||
PID|1||987654||SMITH^ROBERT^J||19750520|M|||789 OAK AVE^^CITY^ST^98765|||||||
PV1|1|O|CLINIC^EXAM1||||PROVIDER^PRIMARY|||||||||||
ORC|NW|ORD456789|FIL654321||||^^^20230101||20230101143000|ORDERING^PROVIDER||
RXE|^^^20230101^20230201|00004-0005-01^Acetaminophen 500mg^NDC|500||MG||||||10|||||||||||||||
RXR|PO^Oral^HL70162|MOUTH^Mouth^HL70163|||
RXC|B|00004-0005-02^Component A^NDC|100|MG|||
RXC|B|00004-0006-03^Component B^NDC|50|MG|||
TQ1|1|PRN||20230101|20230131|||
TQ1|2|Q4H||20230101|20230131|||";

    /// <summary>
    /// Immunization message (VXU^V04)
    /// </summary>
    public const string ImmunizationMessage = @"MSH|^~\&|SENDING_APPLICATION|SENDING_FACILITY|RECEIVING_APPLICATION|RECEIVING_FACILITY|201305171259|12|VXU^V04|2244455|P|2.3||||||
PID|1||123456||DUCK^DAISY^L||19690912|F|||123 NORTHWOOD ST APT 9^^NEW CITY^NC^27262-9944|||||||||||||||||||
ORC|OK|664443333^EEE|33994499||||^^^20220301||20220301101531|DAVE^DAVID^DAVE^D||444999^DAVID JR^JAMES^DAVID^^^^^LAB&PROVID&ISO^L^^^PROVID^FACILITY_CODE&1.2.888.444999.1.13.308.2.7.2.696969&ISO|1021209999^^^10299^^^^^WD999 09 LABORATORY NAME|^^^^^333^8022999||||CCC528Y73^CCC-528Y73||||||
RXA|0|999|20220301|20220301|217^PFIZER 12 YEARS \T\ UP SARS-COV-2 VACCINE^LIM_CVX|0.3|ML||00^New immunization record^NIP001|459920^DUCK^DAISY^L^^^^^LAB&PROVID&ISO^L^^^PROVID^FACILITY_CODE&1.2.888.444999.1.13.308.2.7.2.696969&ISO|1021209999^^^10299^^^^^WD999 09 LABORATORY NAME||||FK9999|20220531|PPR|||CP|A|20220301101531
RXR|IM^Intramuscular^HL70162|LD^Left Deltoid^HL70163|||";

    /// <summary>
    /// Large message with multiple segments
    /// </summary>
    public const string LargeMessage = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG004|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||PATIENT^TEST^LARGE||19850315|M|||1234 MAIN STREET APT 5B^^METROPOLIS^ST^12345-6789||(555)123-4567|(555)987-6543||S|PROTESTANT|987654321|||123-45-6789|||N||||||||
PD1||D|FACILITY^CLINIC^200|||||||||||
NK1|1|CONTACT^EMERGENCY^A|SPO|1234 SAME ST^^METROPOLIS^ST^12345|(555)111-2222|(555)333-4444|
PV1|1|I|NORTH^201^A^MAIN HOSPITAL^N^^BED^2^200|28b|||1234567^DOCTOR^ATTENDING^A^^^MD|2345678^REFERRING^DOCTOR^^^MD|3456789^CONSULTING^DOCTOR^^^MD|MED||||19|A0|5678901^ADMITTING^DOCTOR^^^MD||VIP12345|INS||||||||||||||||||||HOSP||||20230101120000|
PV2||PREADMIT|REASON FOR ADMISSION||||||||||||||||||||||||||||||||||||
AL1|1|DA|1545^PENICILLIN^RX|SV|HIVES~ITCHING
AL1|2|MA|^PEANUTS^FOOD|MO|ANAPHYLAXIS
AL1|3|EA|^POLLEN^ENV|MI|SNEEZING
DG1|1|I10|E11.9^Type 2 diabetes mellitus without complications^I10||20230101|A|
DG1|2|I10|I10^Essential (primary) hypertension^I10||20230101|A|
DG1|3|I10|E78.5^Hyperlipidemia, unspecified^I10||20230101|W|
IN1|1|PLAN001|INS123|INSURANCE COMPANY NAME|PO BOX 12345^^INSURANCE CITY^ST^54321|(800)555-1212||GROUP123|GROUP NAME|||||||||PATIENT^TEST^LARGE|SELF|19850315|1234 MAIN STREET APT 5B^^METROPOLIS^ST^12345|||||||||||||||||POLICY123456|";

    /// <summary>
    /// Minimal MSH-only message
    /// </summary>
    public const string MinimalMessage = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||";

    /// <summary>
    /// All available test messages
    /// </summary>
    public static readonly Dictionary<string, string> AllMessages = new()
    {
        ["Simple ADT"] = SimpleADT,
        ["ADT with Clinical Data"] = ADTWithClinical,
        ["Medication Order"] = MedicationOrder,
        ["Complex Medication Order"] = ComplexMedicationOrder,
        ["Immunization"] = ImmunizationMessage,
        ["Large Message"] = LargeMessage,
        ["Minimal Message"] = MinimalMessage
    };
}
