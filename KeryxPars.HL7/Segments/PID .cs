using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Idenfitication
    /// </summary>
    public class PID : ISegment
    {
        public string SegmentId => nameof(PID);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// PID.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// PID.2
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// PID.3
        /// </summary>
        public string PatientIdentifierList { get; set; }

        /// <summary>
        /// PID.4
        /// </summary>
        public string AlternatePatientID { get; set; }

        /// <summary>
        /// PID.5
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// PID.6
        /// </summary>
        public string MothersMaidenName { get; set; }

        /// <summary>
        /// PID.7
        /// </summary>
        public string DateTimeofBirth { get; set; }

        /// <summary>
        /// PID.8
        /// </summary>
        public string AdministrativeSex { get; set; }

        /// <summary>
        /// PID.9
        /// </summary>
        public string PatientAlias { get; set; }

        /// <summary>
        /// PID.10
        /// </summary>
        public string Race { get; set; }

        /// <summary>
        /// PID.11
        /// </summary>
        public string PatientAddress { get; set; }

        /// <summary>
        /// PID.12
        /// </summary>
        public string CountyCode { get; set; }

        /// <summary>
        /// PID.13
        /// </summary>
        public string PhoneNumberHome { get; set; }

        /// <summary>
        /// PID.14
        /// </summary>
        public string PhoneNumberBusiness { get; set; }

        /// <summary>
        /// PID.15
        /// </summary>
        public string PrimaryLanguage { get; set; }

        /// <summary>
        /// PID.16
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// PID.17
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// PID.18
        /// </summary>
        public string PatientAccountNumber { get; set; }

        /// <summary>
        /// PID.19
        /// </summary>
        public string SocialSecurityNumberPatient { get; set; }

        /// <summary>
        /// PID.20
        /// </summary>
        public string DriversLicenseNumberPatient { get; set; }

        /// <summary>
        /// PID.21
        /// </summary>
        public string MothersIdentifier { get; set; }

        /// <summary>
        /// PID.22
        /// </summary>
        public string EthnicGroup { get; set; }

        /// <summary>
        /// PID.23
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// PID.24
        /// </summary>
        public string MultipleBirthIndicator { get; set; }

        /// <summary>
        /// PID.25
        /// </summary>
        public string BirthOrder { get; set; }

        /// <summary>
        /// PID.26
        /// </summary>
        public string Citizenship { get; set; }

        /// <summary>
        /// PID.27
        /// </summary>
        public string VeteransMilitaryStatus { get; set; }

        /// <summary>
        /// PID.28
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// PID.29
        /// </summary>
        public string PatientDeathDateandTime { get; set; }

        /// <summary>
        /// PID.30
        /// </summary>
        public string PatientDeathIndicator { get; set; }

        /// <summary>
        /// PID.31
        /// </summary>
        public string IdentityUnknownIndicator { get; set; }

        /// <summary>
        /// PID.32
        /// </summary>
        public string IdentityReliabilityCode { get; set; }

        /// <summary>
        /// PID.33
        /// </summary>
        public string LastUpdateDateTime { get; set; }

        /// <summary>
        /// PID.34
        /// </summary>
        public string LastUpdateFacility { get; set; }

        /// <summary>
        /// PID.35
        /// </summary>
        public string SpeciesCode { get; set; }

        /// <summary>
        /// PID.36
        /// </summary>
        public string BreedCode { get; set; }

        /// <summary>
        /// PID.37
        /// </summary>
        public string Strain { get; set; }

        /// <summary>
        /// PID.38
        /// </summary>
        public string ProductionClassCode { get; set; }

        /// <summary>
        /// PID.39
        /// </summary>
        public string TribalCitizenship { get; set; }

        /// <summary>
        /// PID.40
        /// </summary>
        public string PatientTelecommunicationInformation { get; set; }

        // Constructors
        public PID()
        {
            SegmentType = SegmentType.Universal;
            SetID = string.Empty;
            PatientID = string.Empty;
            PatientIdentifierList = string.Empty;
            AlternatePatientID = string.Empty;
            PatientName = string.Empty;
            MothersMaidenName = string.Empty;
            DateTimeofBirth = string.Empty;
            AdministrativeSex = string.Empty;
            PatientAlias = string.Empty;
            Race = string.Empty;
            PatientAddress = string.Empty;
            CountyCode = string.Empty;
            PhoneNumberHome = string.Empty;
            PhoneNumberBusiness = string.Empty;
            PrimaryLanguage = string.Empty;
            MaritalStatus = string.Empty;
            Religion = string.Empty;
            PatientAccountNumber = string.Empty;
            SocialSecurityNumberPatient = string.Empty;
            DriversLicenseNumberPatient = string.Empty;
            MothersIdentifier = string.Empty;
            EthnicGroup = string.Empty;
            BirthPlace = string.Empty;
            MultipleBirthIndicator = string.Empty;
            BirthOrder = string.Empty;
            Citizenship = string.Empty;
            VeteransMilitaryStatus = string.Empty;
            Nationality = string.Empty;
            PatientDeathDateandTime = string.Empty;
            PatientDeathIndicator = string.Empty;
            IdentityUnknownIndicator = string.Empty;
            IdentityReliabilityCode = string.Empty;
            LastUpdateDateTime = string.Empty;
            LastUpdateFacility = string.Empty;
            SpeciesCode = string.Empty;
            BreedCode = string.Empty;
            Strain = string.Empty;
            ProductionClassCode = string.Empty;
            TribalCitizenship = string.Empty;
            PatientTelecommunicationInformation = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: PatientID = value; break;
                case 3: PatientIdentifierList = value; break;
                case 4: AlternatePatientID = value; break;
                case 5: PatientName = value; break;
                case 6: MothersMaidenName = value; break;
                case 7: DateTimeofBirth = value; break;
                case 8: AdministrativeSex = value; break;
                case 9: PatientAlias = value; break;
                case 10: Race = value; break;
                case 11: PatientAddress = value; break;
                case 12: CountyCode = value; break;
                case 13: PhoneNumberHome = value; break;
                case 14: PhoneNumberBusiness = value; break;
                case 15: PrimaryLanguage = value; break;
                case 16: MaritalStatus = value; break;
                case 17: Religion = value; break;
                case 18: PatientAccountNumber = value; break;
                case 19: SocialSecurityNumberPatient = value; break;
                case 20: DriversLicenseNumberPatient = value; break;
                case 21: MothersIdentifier = value; break;
                case 22: EthnicGroup = value; break;
                case 23: BirthPlace = value; break;
                case 24: MultipleBirthIndicator = value; break;
                case 25: BirthOrder = value; break;
                case 26: Citizenship = value; break;
                case 27: VeteransMilitaryStatus = value; break;
                case 28: Nationality = value; break;
                case 29: PatientDeathDateandTime = value; break;
                case 30: PatientDeathIndicator = value; break;
                case 31: IdentityUnknownIndicator = value; break;
                case 32: IdentityReliabilityCode = value; break;
                case 33: LastUpdateDateTime = value; break;
                case 34: LastUpdateFacility = value; break;
                case 35: SpeciesCode = value; break;
                case 36: BreedCode = value; break;
                case 37: Strain = value; break;
                case 38: ProductionClassCode = value; break;
                case 39: TribalCitizenship = value; break;
                case 40: PatientTelecommunicationInformation = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                PatientID,
                PatientIdentifierList,
                AlternatePatientID,
                PatientName,
                MothersMaidenName,
                DateTimeofBirth,
                AdministrativeSex,
                PatientAlias,
                Race,
                PatientAddress,
                CountyCode,
                PhoneNumberHome,
                PhoneNumberBusiness,
                PrimaryLanguage,
                MaritalStatus,
                Religion,
                PatientAccountNumber,
                SocialSecurityNumberPatient,
                DriversLicenseNumberPatient,
                MothersIdentifier,
                EthnicGroup,
                BirthPlace,
                MultipleBirthIndicator,
                BirthOrder,
                Citizenship,
                VeteransMilitaryStatus,
                Nationality,
                PatientDeathDateandTime,
                PatientDeathIndicator,
                IdentityUnknownIndicator,
                IdentityReliabilityCode,
                LastUpdateDateTime,
                LastUpdateFacility,
                SpeciesCode,
                BreedCode,
                Strain,
                ProductionClassCode,
                TribalCitizenship,
                PatientTelecommunicationInformation
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => PatientID,
                3 => PatientIdentifierList,
                4 => AlternatePatientID,
                5 => PatientName,
                6 => MothersMaidenName,
                7 => DateTimeofBirth,
                8 => AdministrativeSex,
                9 => PatientAlias,
                10 => Race,
                11 => PatientAddress,
                12 => CountyCode,
                13 => PhoneNumberHome,
                14 => PhoneNumberBusiness,
                15 => PrimaryLanguage,
                16 => MaritalStatus,
                17 => Religion,
                18 => PatientAccountNumber,
                19 => SocialSecurityNumberPatient,
                20 => DriversLicenseNumberPatient,
                21 => MothersIdentifier,
                22 => EthnicGroup,
                23 => BirthPlace,
                24 => MultipleBirthIndicator,
                25 => BirthOrder,
                26 => Citizenship,
                27 => VeteransMilitaryStatus,
                28 => Nationality,
                29 => PatientDeathDateandTime,
                30 => PatientDeathIndicator,
                31 => IdentityUnknownIndicator,
                32 => IdentityReliabilityCode,
                33 => LastUpdateDateTime,
                34 => LastUpdateFacility,
                35 => SpeciesCode,
                36 => BreedCode,
                37 => Strain,
                38 => ProductionClassCode,
                39 => TribalCitizenship,
                40 => PatientTelecommunicationInformation,
                _ => null
            };
        }
    }
}
