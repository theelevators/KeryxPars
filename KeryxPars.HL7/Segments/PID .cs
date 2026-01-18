using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Identification
    /// Refactored to use strongly-typed HL7 datatypes for improved validation and type safety.
    /// </summary>
    public class PID : ISegment
    {
        public string SegmentId => nameof(PID);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// PID.1 - Set ID - PID
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// PID.2 - Patient ID (External ID) - deprecated
        /// </summary>
        public CX PatientID { get; set; }

        /// <summary>
        /// PID.3 - Patient Identifier List (repeating)
        /// </summary>
        public CX[] PatientIdentifierList { get; set; }

        /// <summary>
        /// PID.4 - Alternate Patient ID - PID (repeating) - deprecated
        /// </summary>
        public CX[] AlternatePatientID { get; set; }

        /// <summary>
        /// PID.5 - Patient Name (repeating)
        /// </summary>
        public XPN[] PatientName { get; set; }

        /// <summary>
        /// PID.6 - Mother's Maiden Name (repeating)
        /// </summary>
        public XPN[] MothersMaidenName { get; set; }

        /// <summary>
        /// PID.7 - Date/Time of Birth
        /// </summary>
        public DTM DateTimeofBirth { get; set; }

        /// <summary>
        /// PID.8 - Administrative Sex
        /// </summary>
        public IS AdministrativeSex { get; set; }

        /// <summary>
        /// PID.9 - Patient Alias (repeating) - deprecated
        /// </summary>
        public XPN[] PatientAlias { get; set; }

        /// <summary>
        /// PID.10 - Race (repeating)
        /// </summary>
        public CE[] Race { get; set; }

        /// <summary>
        /// PID.11 - Patient Address (repeating)
        /// </summary>
        public XAD[] PatientAddress { get; set; }

        /// <summary>
        /// PID.12 - County Code - deprecated
        /// </summary>
        public IS CountyCode { get; set; }

        /// <summary>
        /// PID.13 - Phone Number - Home (repeating)
        /// </summary>
        public XTN[] PhoneNumberHome { get; set; }

        /// <summary>
        /// PID.14 - Phone Number - Business (repeating)
        /// </summary>
        public XTN[] PhoneNumberBusiness { get; set; }

        /// <summary>
        /// PID.15 - Primary Language
        /// </summary>
        public CE PrimaryLanguage { get; set; }

        /// <summary>
        /// PID.16 - Marital Status
        /// </summary>
        public CE MaritalStatus { get; set; }

        /// <summary>
        /// PID.17 - Religion
        /// </summary>
        public CE Religion { get; set; }

        /// <summary>
        /// PID.18 - Patient Account Number
        /// </summary>
        public CX PatientAccountNumber { get; set; }

        /// <summary>
        /// PID.19 - SSN Number - Patient - deprecated
        /// </summary>
        public ST SocialSecurityNumberPatient { get; set; }

        /// <summary>
        /// PID.20 - Driver's License Number - Patient - deprecated
        /// </summary>
        public ST DriversLicenseNumberPatient { get; set; }

        /// <summary>
        /// PID.21 - Mother's Identifier (repeating)
        /// </summary>
        public CX[] MothersIdentifier { get; set; }

        /// <summary>
        /// PID.22 - Ethnic Group (repeating)
        /// </summary>
        public CE[] EthnicGroup { get; set; }

        /// <summary>
        /// PID.23 - Birth Place
        /// </summary>
        public ST BirthPlace { get; set; }

        /// <summary>
        /// PID.24 - Multiple Birth Indicator
        /// </summary>
        public ID MultipleBirthIndicator { get; set; }

        /// <summary>
        /// PID.25 - Birth Order
        /// </summary>
        public NM BirthOrder { get; set; }

        /// <summary>
        /// PID.26 - Citizenship (repeating)
        /// </summary>
        public CE[] Citizenship { get; set; }

        /// <summary>
        /// PID.27 - Veterans Military Status
        /// </summary>
        public CE VeteransMilitaryStatus { get; set; }

        /// <summary>
        /// PID.28 - Nationality
        /// </summary>
        public CE Nationality { get; set; }

        /// <summary>
        /// PID.29 - Patient Death Date and Time
        /// </summary>
        public DTM PatientDeathDateandTime { get; set; }

        /// <summary>
        /// PID.30 - Patient Death Indicator
        /// </summary>
        public ID PatientDeathIndicator { get; set; }

        /// <summary>
        /// PID.31 - Identity Unknown Indicator
        /// </summary>
        public ID IdentityUnknownIndicator { get; set; }

        /// <summary>
        /// PID.32 - Identity Reliability Code (repeating)
        /// </summary>
        public IS[] IdentityReliabilityCode { get; set; }

        /// <summary>
        /// PID.33 - Last Update Date/Time
        /// </summary>
        public DTM LastUpdateDateTime { get; set; }

        /// <summary>
        /// PID.34 - Last Update Facility
        /// </summary>
        public HD LastUpdateFacility { get; set; }

        /// <summary>
        /// PID.35 - Species Code
        /// </summary>
        public CE SpeciesCode { get; set; }

        /// <summary>
        /// PID.36 - Breed Code
        /// </summary>
        public CE BreedCode { get; set; }

        /// <summary>
        /// PID.37 - Strain
        /// </summary>
        public ST Strain { get; set; }

        /// <summary>
        /// PID.38 - Production Class Code
        /// </summary>
        public CE ProductionClassCode { get; set; }

        /// <summary>
        /// PID.39 - Tribal Citizenship (repeating)
        /// </summary>
        public CWE[] TribalCitizenship { get; set; }

        /// <summary>
        /// PID.40 - Patient Telecommunication Information (repeating)
        /// </summary>
        public XTN[] PatientTelecommunicationInformation { get; set; }

        public PID()
        {
            SegmentType = SegmentType.Universal;
            
            SetID = default;
            PatientID = default;
            DateTimeofBirth = default;
            AdministrativeSex = default;
            CountyCode = default;
            PrimaryLanguage = default;
            MaritalStatus = default;
            Religion = default;
            PatientAccountNumber = default;
            SocialSecurityNumberPatient = default;
            DriversLicenseNumberPatient = default;
            BirthPlace = default;
            MultipleBirthIndicator = default;
            BirthOrder = default;
            VeteransMilitaryStatus = default;
            Nationality = default;
            PatientDeathDateandTime = default;
            PatientDeathIndicator = default;
            IdentityUnknownIndicator = default;
            LastUpdateDateTime = default;
            LastUpdateFacility = default;
            SpeciesCode = default;
            BreedCode = default;
            Strain = default;
            ProductionClassCode = default;
            
            PatientIdentifierList = Array.Empty<CX>();
            AlternatePatientID = Array.Empty<CX>();
            PatientName = Array.Empty<XPN>();
            MothersMaidenName = Array.Empty<XPN>();
            PatientAlias = Array.Empty<XPN>();
            Race = Array.Empty<CE>();
            PatientAddress = Array.Empty<XAD>();
            PhoneNumberHome = Array.Empty<XTN>();
            PhoneNumberBusiness = Array.Empty<XTN>();
            MothersIdentifier = Array.Empty<CX>();
            EthnicGroup = Array.Empty<CE>();
            Citizenship = Array.Empty<CE>();
            IdentityReliabilityCode = Array.Empty<IS>();
            TribalCitizenship = Array.Empty<CWE>();
            PatientTelecommunicationInformation = Array.Empty<XTN>();
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: PatientID = value; break;
                case 3: PatientIdentifierList = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 4: AlternatePatientID = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 5: PatientName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 6: MothersMaidenName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 7: DateTimeofBirth = new DTM(value); break;
                case 8: AdministrativeSex = new IS(value); break;
                case 9: PatientAlias = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 10: Race = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 11: PatientAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 12: CountyCode = new IS(value); break;
                case 13: PhoneNumberHome = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 14: PhoneNumberBusiness = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 15: PrimaryLanguage = value; break;
                case 16: MaritalStatus = value; break;
                case 17: Religion = value; break;
                case 18: PatientAccountNumber = value; break;
                case 19: SocialSecurityNumberPatient = new ST(value); break;
                case 20: DriversLicenseNumberPatient = new ST(value); break;
                case 21: MothersIdentifier = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 22: EthnicGroup = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 23: BirthPlace = new ST(value); break;
                case 24: MultipleBirthIndicator = new ID(value); break;
                case 25: BirthOrder = new NM(value); break;
                case 26: Citizenship = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 27: VeteransMilitaryStatus = value; break;
                case 28: Nationality = value; break;
                case 29: PatientDeathDateandTime = new DTM(value); break;
                case 30: PatientDeathIndicator = new ID(value); break;
                case 31: IdentityUnknownIndicator = new ID(value); break;
                case 32: IdentityReliabilityCode = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters); break;
                case 33: LastUpdateDateTime = new DTM(value); break;
                case 34:
                    var hd = new HD();
                    hd.Parse(value.AsSpan(), delimiters);
                    LastUpdateFacility = hd;
                    break;
                case 35: SpeciesCode = value; break;
                case 36: BreedCode = value; break;
                case 37: Strain = new ST(value); break;
                case 38: ProductionClassCode = value; break;
                case 39: TribalCitizenship = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 40: PatientTelecommunicationInformation = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                PatientID.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientIdentifierList, delimiters),
                SegmentFieldHelper.JoinRepeatingField(AlternatePatientID, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(MothersMaidenName, delimiters),
                DateTimeofBirth.ToHL7String(delimiters),
                AdministrativeSex.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientAlias, delimiters),
                SegmentFieldHelper.JoinRepeatingField(Race, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientAddress, delimiters),
                CountyCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PhoneNumberHome, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PhoneNumberBusiness, delimiters),
                PrimaryLanguage.ToHL7String(delimiters),
                MaritalStatus.ToHL7String(delimiters),
                Religion.ToHL7String(delimiters),
                PatientAccountNumber.ToHL7String(delimiters),
                SocialSecurityNumberPatient.ToHL7String(delimiters),
                DriversLicenseNumberPatient.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(MothersIdentifier, delimiters),
                SegmentFieldHelper.JoinRepeatingField(EthnicGroup, delimiters),
                BirthPlace.ToHL7String(delimiters),
                MultipleBirthIndicator.ToHL7String(delimiters),
                BirthOrder.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Citizenship, delimiters),
                VeteransMilitaryStatus.ToHL7String(delimiters),
                Nationality.ToHL7String(delimiters),
                PatientDeathDateandTime.ToHL7String(delimiters),
                PatientDeathIndicator.ToHL7String(delimiters),
                IdentityUnknownIndicator.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(IdentityReliabilityCode, delimiters),
                LastUpdateDateTime.ToHL7String(delimiters),
                LastUpdateFacility.ToHL7String(delimiters),
                SpeciesCode.ToHL7String(delimiters),
                BreedCode.ToHL7String(delimiters),
                Strain.ToHL7String(delimiters),
                ProductionClassCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(TribalCitizenship, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientTelecommunicationInformation, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var values = GetValues();
            return index >= 0 && index < values.Length ? values[index] : null;
        }
    }
}
