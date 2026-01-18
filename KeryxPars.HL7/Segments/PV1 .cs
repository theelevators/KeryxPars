using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Visit
    /// </summary>
    public class PV1 : ISegment
    {
        public string SegmentId => nameof(PV1);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// PV1.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// PV1.2
        /// </summary>
        public string PatientClass { get; set; }

        /// <summary>
        /// PV1.3
        /// </summary>
        public string AssignedPatientLocation { get; set; }

        /// <summary>
        /// PV1.4
        /// </summary>
        public string AdmissionType { get; set; }

        /// <summary>
        /// PV1.5
        /// </summary>
        public string PreadmitNumber { get; set; }

        /// <summary>
        /// PV1.6
        /// </summary>
        public string PriorPatientLocation { get; set; }

        /// <summary>
        /// PV1.7
        /// </summary>
        public string AttendingDoctor { get; set; }

        /// <summary>
        /// PV1.8
        /// </summary>
        public string ReferringDoctor { get; set; }

        /// <summary>
        /// PV1.9
        /// </summary>
        public string ConsultingDoctor { get; set; }

        /// <summary>
        /// PV1.10
        /// </summary>
        public string HospitalService { get; set; }

        /// <summary>
        /// PV1.11
        /// </summary>
        public string TemporaryLocation { get; set; }

        /// <summary>
        /// PV1.12
        /// </summary>
        public string PreadmitTestIndicator { get; set; }

        /// <summary>
        /// PV1.13
        /// </summary>
        public string ReadmissionIndicator { get; set; }

        /// <summary>
        /// PV1.14
        /// </summary>
        public string AdmitSource { get; set; }

        /// <summary>
        /// PV1.15
        /// </summary>
        public string AmbulatoryStatus { get; set; }

        /// <summary>
        /// PV1.16
        /// </summary>
        public string VipIndicator { get; set; }

        /// <summary>
        /// PV1.17
        /// </summary>
        public string AdmittingDoctor { get; set; }

        /// <summary>
        /// PV1.18
        /// </summary>
        public string PatientType { get; set; }

        /// <summary>
        /// PV1.19
        /// </summary>
        public string VisitNumber { get; set; }

        /// <summary>
        /// PV1.20
        /// </summary>
        public string FinancialClass { get; set; }

        /// <summary>
        /// PV1.21
        /// </summary>
        public string ChargePriceIndicator { get; set; }

        /// <summary>
        /// PV1.22
        /// </summary>
        public string CourtesyCode { get; set; }

        /// <summary>
        /// PV1.23
        /// </summary>
        public string CreditRating { get; set; }

        /// <summary>
        /// PV1.24
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// PV1.25
        /// </summary>
        public string ContractEffectiveDate { get; set; }

        /// <summary>
        /// PV1.26
        /// </summary>
        public string ContractAmount { get; set; }

        /// <summary>
        /// PV1.27
        /// </summary>
        public string ContractPeriod { get; set; }

        /// <summary>
        /// PV1.28
        /// </summary>
        public string InterestCode { get; set; }

        /// <summary>
        /// PV1.29
        /// </summary>
        public string TransferToBadDebtCode { get; set; }

        /// <summary>
        /// PV1.30
        /// </summary>
        public string TransferToBadDebtDate { get; set; }

        /// <summary>
        /// PV1.31
        /// </summary>
        public string BadDebtAgencyCode { get; set; }

        /// <summary>
        /// PV1.32
        /// </summary>
        public string BadDebtTransferAmount { get; set; }

        /// <summary>
        /// PV1.33
        /// </summary>
        public string BadDebtRecoveryAmount { get; set; }

        /// <summary>
        /// PV1.34
        /// </summary>
        public string DeleteAccountIndicator { get; set; }

        /// <summary>
        /// PV1.35
        /// </summary>
        public string DeleteaccountDate { get; set; }

        /// <summary>
        /// PV1.36
        /// </summary>
        public string DischargeDisposition { get; set; }

        /// <summary>
        /// PV1.37
        /// </summary>
        public string DischargedToLocation { get; set; }

        /// <summary>
        /// PV1.38
        /// </summary>
        public string DietType { get; set; }

        /// <summary>
        /// PV1.39
        /// </summary>
        public string ServicingFacility { get; set; }

        /// <summary>
        /// PV1.40
        /// </summary>
        public string BedStatus { get; set; }

        /// <summary>
        /// PV1.41
        /// </summary>
        public string AccountStatus { get; set; }

        /// <summary>
        /// PV1.42
        /// </summary>
        public string PendingLocation { get; set; }

        /// <summary>
        /// PV1.43
        /// </summary>
        public string PriorTemporaryLocation { get; set; }

        /// <summary>
        /// PV1.44
        /// </summary>
        public string AdmitDateTime { get; set; }

        /// <summary>
        /// PV1.45
        /// </summary>
        public string DischargeDateTime { get; set; }

        /// <summary>
        /// PV1.46
        /// </summary>
        public string CurrentPatientBalance { get; set; }

        /// <summary>
        /// PV1.47
        /// </summary>
        public string TotalCharges { get; set; }

        /// <summary>
        /// PV1.48
        /// </summary>
        public string TotalAdjustments { get; set; }

        /// <summary>
        /// PV1.49
        /// </summary>
        public string TotalPayment { get; set; }

        /// <summary>
        /// PV1.50
        /// </summary>
        public string AlternateVisitID { get; set; }

        /// <summary>
        /// PV1.51
        /// </summary>
        public string VisitIndicator { get; set; }

        /// <summary>
        /// PV1.52
        /// </summary>
        public string OtherHealthcareProvider { get; set; }

        /// <summary>
        /// PV1.53
        /// </summary>
        public string ServiceEpisodeDescription { get; set; }

        /// <summary>
        /// PV1.54
        /// </summary>
        public string ServiceEpisodeIdentifier { get; set; }


        // Constructors
        public PV1()
        {
            SegmentType = SegmentType.ADT;
            SetID = string.Empty;
            PatientClass = string.Empty;
            AssignedPatientLocation = string.Empty;
            AdmissionType = string.Empty;
            PreadmitNumber = string.Empty;
            PriorPatientLocation = string.Empty;
            AttendingDoctor = string.Empty;
            ReferringDoctor = string.Empty;
            ConsultingDoctor = string.Empty;
            HospitalService = string.Empty;
            TemporaryLocation = string.Empty;
            PreadmitTestIndicator = string.Empty;
            ReadmissionIndicator = string.Empty;
            AdmitSource = string.Empty;
            AmbulatoryStatus = string.Empty;
            VipIndicator = string.Empty;
            AdmittingDoctor = string.Empty;
            PatientType = string.Empty;
            VisitNumber = string.Empty;
            FinancialClass = string.Empty;
            ChargePriceIndicator = string.Empty;
            CourtesyCode = string.Empty;
            CreditRating = string.Empty;
            ContractCode = string.Empty;
            ContractEffectiveDate = string.Empty;
            ContractAmount = string.Empty;
            ContractPeriod = string.Empty;
            InterestCode = string.Empty;
            TransferToBadDebtCode = string.Empty;
            TransferToBadDebtDate = string.Empty;
            BadDebtAgencyCode = string.Empty;
            BadDebtTransferAmount = string.Empty;
            BadDebtRecoveryAmount = string.Empty;
            DeleteAccountIndicator = string.Empty;
            DeleteaccountDate = string.Empty;
            DischargeDisposition = string.Empty;
            DischargedToLocation = string.Empty;
            DietType = string.Empty;
            ServicingFacility = string.Empty;
            BedStatus = string.Empty;
            AccountStatus = string.Empty;
            PendingLocation = string.Empty;
            PriorTemporaryLocation = string.Empty;
            AdmitDateTime = string.Empty;
            DischargeDateTime = string.Empty;
            CurrentPatientBalance = string.Empty;
            TotalCharges = string.Empty;
            TotalAdjustments = string.Empty;
            TotalPayment = string.Empty;
            AlternateVisitID = string.Empty;
            VisitIndicator = string.Empty;
        }


        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: PatientClass = value; break;
                case 3: AssignedPatientLocation = value; break;
                case 4: AdmissionType = value; break;
                case 5: PreadmitNumber = value; break;
                case 6: PriorPatientLocation = value; break;
                case 7: AttendingDoctor = value; break;
                case 8: ReferringDoctor = value; break;
                case 9: ConsultingDoctor = value; break;
                case 10: HospitalService = value; break;
                case 11: TemporaryLocation = value; break;
                case 12: PreadmitTestIndicator = value; break;
                case 13: ReadmissionIndicator = value; break;
                case 14: AdmitSource = value; break;
                case 15: AmbulatoryStatus = value; break;
                case 16: VipIndicator = value; break;
                case 17: AdmittingDoctor = value; break;
                case 18: PatientType = value; break;
                case 19: VisitNumber = value; break;
                case 20: FinancialClass = value; break;
                case 21: ChargePriceIndicator = value; break;
                case 22: CourtesyCode = value; break;
                case 23: CreditRating = value; break;
                case 24: ContractCode = value; break;
                case 25: ContractEffectiveDate = value; break;
                case 26: ContractAmount = value; break;
                case 27: ContractPeriod = value; break;
                case 28: InterestCode = value; break;
                case 29: TransferToBadDebtCode = value; break;
                case 30: TransferToBadDebtDate = value; break;
                case 31: BadDebtAgencyCode = value; break;
                case 32: BadDebtTransferAmount = value; break;
                case 33: BadDebtRecoveryAmount = value; break;
                case 34: DeleteAccountIndicator = value; break;
                case 35: DeleteaccountDate = value; break;
                case 36: DischargeDisposition = value; break;
                case 37: DischargedToLocation = value; break;
                case 38: DietType = value; break;
                case 39: ServicingFacility = value; break;
                case 40: BedStatus = value; break;
                case 41: AccountStatus = value; break;
                case 42: PendingLocation = value; break;
                case 43: PriorTemporaryLocation = value; break;
                case 44: AdmitDateTime = value; break;
                case 45: DischargeDateTime = value; break;
                case 46: CurrentPatientBalance = value; break;
                case 47: TotalCharges = value; break;
                case 48: TotalAdjustments = value; break;
                case 49: TotalPayment = value; break;
                case 50: AlternateVisitID = value; break;
                case 51: VisitIndicator = value; break;
                case 52: OtherHealthcareProvider = value; break;
                case 53: ServiceEpisodeDescription = value; break;
                case 54: ServiceEpisodeIdentifier = value; break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                PatientClass,
                AssignedPatientLocation,
                AdmissionType,
                PreadmitNumber,
                PriorPatientLocation,
                AttendingDoctor,
                ReferringDoctor,
                ConsultingDoctor,
                HospitalService,
                TemporaryLocation,
                PreadmitTestIndicator,
                ReadmissionIndicator,
                AdmitSource,
                AmbulatoryStatus,
                VipIndicator,
                AdmittingDoctor,
                PatientType,
                VisitNumber,
                FinancialClass,
                ChargePriceIndicator,
                CourtesyCode,
                CreditRating,
                ContractCode,
                ContractEffectiveDate,
                ContractAmount,
                ContractPeriod,
                InterestCode,
                TransferToBadDebtCode,
                TransferToBadDebtDate,
                BadDebtAgencyCode,
                BadDebtTransferAmount,
                BadDebtRecoveryAmount,
                DeleteAccountIndicator,
                DeleteaccountDate,
                DischargeDisposition,
                DischargedToLocation,
                DietType,
                ServicingFacility,
                BedStatus,
                AccountStatus,
                PendingLocation,
                PriorTemporaryLocation,
                AdmitDateTime,
                DischargeDateTime,
                CurrentPatientBalance,
                TotalCharges,
                TotalAdjustments,
                TotalPayment,
                AlternateVisitID,
                VisitIndicator,
                OtherHealthcareProvider,
                ServiceEpisodeDescription,
                ServiceEpisodeIdentifier
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => PatientClass,
                3 => AssignedPatientLocation,
                4 => AdmissionType,
                5 => PreadmitNumber,
                6 => PriorPatientLocation,
                7 => AttendingDoctor,
                8 => ReferringDoctor,
                9 => ConsultingDoctor,
                10 => HospitalService,
                11 => TemporaryLocation,
                12 => PreadmitTestIndicator,
                13 => ReadmissionIndicator,
                14 => AdmitSource,
                15 => AmbulatoryStatus,
                16 => VipIndicator,
                17 => AdmittingDoctor,
                18 => PatientType,
                19 => VisitNumber,
                20 => FinancialClass,
                21 => ChargePriceIndicator,
                22 => CourtesyCode,
                23 => CreditRating,
                24 => ContractCode,
                25 => ContractEffectiveDate,
                26 => ContractAmount,
                27 => ContractPeriod,
                28 => InterestCode,
                29 => TransferToBadDebtCode,
                30 => TransferToBadDebtDate,
                31 => BadDebtAgencyCode,
                32 => BadDebtTransferAmount,
                33 => BadDebtRecoveryAmount,
                34 => DeleteAccountIndicator,
                35 => DeleteaccountDate,
                36 => DischargeDisposition,
                37 => DischargedToLocation,
                38 => DietType,
                39 => ServicingFacility,
                40 => BedStatus,
                41 => AccountStatus,
                42 => PendingLocation,
                43 => PriorTemporaryLocation,
                44 => AdmitDateTime,
                45 => DischargeDateTime,
                46 => CurrentPatientBalance,
                47 => TotalCharges,
                48 => TotalAdjustments,
                49 => TotalPayment,
                50 => AlternateVisitID,
                51 => VisitIndicator,
                52 => OtherHealthcareProvider,
                53 => ServiceEpisodeDescription,
                54 => ServiceEpisodeIdentifier,
                _ => null
            };
        }
    }
}
