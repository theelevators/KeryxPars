using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Visit
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class PV1 : ISegment
    {
        public string SegmentId => nameof(PV1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// PV1.1 - Set ID - PV1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// PV1.2 - Patient Class
        /// </summary>
        public IS PatientClass { get; set; }

        /// <summary>
        /// PV1.3 - Assigned Patient Location
        /// </summary>
        public PL AssignedPatientLocation { get; set; }

        /// <summary>
        /// PV1.4 - Admission Type
        /// </summary>
        public IS AdmissionType { get; set; }

        /// <summary>
        /// PV1.5 - Preadmit Number
        /// </summary>
        public CX PreadmitNumber { get; set; }

        /// <summary>
        /// PV1.6 - Prior Patient Location
        /// </summary>
        public PL PriorPatientLocation { get; set; }

        /// <summary>
        /// PV1.7 - Attending Doctor (repeating)
        /// </summary>
        public XCN[] AttendingDoctor { get; set; }

        /// <summary>
        /// PV1.8 - Referring Doctor (repeating)
        /// </summary>
        public XCN[] ReferringDoctor { get; set; }

        /// <summary>
        /// PV1.9 - Consulting Doctor (repeating)
        /// </summary>
        public XCN[] ConsultingDoctor { get; set; }

        /// <summary>
        /// PV1.10 - Hospital Service
        /// </summary>
        public IS HospitalService { get; set; }

        /// <summary>
        /// PV1.11 - Temporary Location
        /// </summary>
        public PL TemporaryLocation { get; set; }

        /// <summary>
        /// PV1.12 - Preadmit Test Indicator
        /// </summary>
        public IS PreadmitTestIndicator { get; set; }

        /// <summary>
        /// PV1.13 - Re-admission Indicator
        /// </summary>
        public IS ReadmissionIndicator { get; set; }

        /// <summary>
        /// PV1.14 - Admit Source
        /// </summary>
        public IS AdmitSource { get; set; }

        /// <summary>
        /// PV1.15 - Ambulatory Status (repeating)
        /// </summary>
        public IS[] AmbulatoryStatus { get; set; }

        /// <summary>
        /// PV1.16 - VIP Indicator
        /// </summary>
        public IS VipIndicator { get; set; }

        /// <summary>
        /// PV1.17 - Admitting Doctor (repeating)
        /// </summary>
        public XCN[] AdmittingDoctor { get; set; }

        /// <summary>
        /// PV1.18 - Patient Type
        /// </summary>
        public IS PatientType { get; set; }

        /// <summary>
        /// PV1.19 - Visit Number
        /// </summary>
        public CX VisitNumber { get; set; }

        /// <summary>
        /// PV1.20 - Financial Class (repeating)
        /// </summary>
        public ST[] FinancialClass { get; set; }

        /// <summary>
        /// PV1.21 - Charge Price Indicator
        /// </summary>
        public IS ChargePriceIndicator { get; set; }

        /// <summary>
        /// PV1.22 - Courtesy Code
        /// </summary>
        public IS CourtesyCode { get; set; }

        /// <summary>
        /// PV1.23 - Credit Rating
        /// </summary>
        public IS CreditRating { get; set; }

        /// <summary>
        /// PV1.24 - Contract Code (repeating)
        /// </summary>
        public IS[] ContractCode { get; set; }

        /// <summary>
        /// PV1.25 - Contract Effective Date (repeating)
        /// </summary>
        public DT[] ContractEffectiveDate { get; set; }

        /// <summary>
        /// PV1.26 - Contract Amount (repeating)
        /// </summary>
        public NM[] ContractAmount { get; set; }

        /// <summary>
        /// PV1.27 - Contract Period (repeating)
        /// </summary>
        public NM[] ContractPeriod { get; set; }

        /// <summary>
        /// PV1.28 - Interest Code
        /// </summary>
        public IS InterestCode { get; set; }

        /// <summary>
        /// PV1.29 - Transfer to Bad Debt Code
        /// </summary>
        public IS TransferToBadDebtCode { get; set; }

        /// <summary>
        /// PV1.30 - Transfer to Bad Debt Date
        /// </summary>
        public DT TransferToBadDebtDate { get; set; }

        /// <summary>
        /// PV1.31 - Bad Debt Agency Code
        /// </summary>
        public IS BadDebtAgencyCode { get; set; }

        /// <summary>
        /// PV1.32 - Bad Debt Transfer Amount
        /// </summary>
        public NM BadDebtTransferAmount { get; set; }

        /// <summary>
        /// PV1.33 - Bad Debt Recovery Amount
        /// </summary>
        public NM BadDebtRecoveryAmount { get; set; }

        /// <summary>
        /// PV1.34 - Delete Account Indicator
        /// </summary>
        public IS DeleteAccountIndicator { get; set; }

        /// <summary>
        /// PV1.35 - Delete Account Date
        /// </summary>
        public DT DeleteaccountDate { get; set; }

        /// <summary>
        /// PV1.36 - Discharge Disposition
        /// </summary>
        public IS DischargeDisposition { get; set; }

        /// <summary>
        /// PV1.37 - Discharged to Location
        /// </summary>
        public ST DischargedToLocation { get; set; }

        /// <summary>
        /// PV1.38 - Diet Type
        /// </summary>
        public CE DietType { get; set; }

        /// <summary>
        /// PV1.39 - Servicing Facility
        /// </summary>
        public IS ServicingFacility { get; set; }

        /// <summary>
        /// PV1.40 - Bed Status - deprecated
        /// </summary>
        public IS BedStatus { get; set; }

        /// <summary>
        /// PV1.41 - Account Status
        /// </summary>
        public IS AccountStatus { get; set; }

        /// <summary>
        /// PV1.42 - Pending Location
        /// </summary>
        public PL PendingLocation { get; set; }

        /// <summary>
        /// PV1.43 - Prior Temporary Location
        /// </summary>
        public PL PriorTemporaryLocation { get; set; }

        /// <summary>
        /// PV1.44 - Admit Date/Time
        /// </summary>
        public DTM AdmitDateTime { get; set; }

        /// <summary>
        /// PV1.45 - Discharge Date/Time (repeating)
        /// </summary>
        public DTM[] DischargeDateTime { get; set; }

        /// <summary>
        /// PV1.46 - Current Patient Balance
        /// </summary>
        public NM CurrentPatientBalance { get; set; }

        /// <summary>
        /// PV1.47 - Total Charges
        /// </summary>
        public NM TotalCharges { get; set; }

        /// <summary>
        /// PV1.48 - Total Adjustments
        /// </summary>
        public NM TotalAdjustments { get; set; }

        /// <summary>
        /// PV1.49 - Total Payments
        /// </summary>
        public NM TotalPayment { get; set; }

        /// <summary>
        /// PV1.50 - Alternate Visit ID
        /// </summary>
        public CX AlternateVisitID { get; set; }

        /// <summary>
        /// PV1.51 - Visit Indicator
        /// </summary>
        public IS VisitIndicator { get; set; }

        /// <summary>
        /// PV1.52 - Other Healthcare Provider (repeating)
        /// </summary>
        public XCN[] OtherHealthcareProvider { get; set; }

        /// <summary>
        /// PV1.53 - Service Episode Description
        /// </summary>
        public ST ServiceEpisodeDescription { get; set; }

        /// <summary>
        /// PV1.54 - Service Episode Identifier
        /// </summary>
        public CX ServiceEpisodeIdentifier { get; set; }

        public PV1()
        {
            SegmentType = SegmentType.ADT;
            
            SetID = default;
            PatientClass = default;
            AssignedPatientLocation = default;
            AdmissionType = default;
            PreadmitNumber = default;
            PriorPatientLocation = default;
            HospitalService = default;
            TemporaryLocation = default;
            PreadmitTestIndicator = default;
            ReadmissionIndicator = default;
            AdmitSource = default;
            VipIndicator = default;
            PatientType = default;
            VisitNumber = default;
            ChargePriceIndicator = default;
            CourtesyCode = default;
            CreditRating = default;
            InterestCode = default;
            TransferToBadDebtCode = default;
            TransferToBadDebtDate = default;
            BadDebtAgencyCode = default;
            BadDebtTransferAmount = default;
            BadDebtRecoveryAmount = default;
            DeleteAccountIndicator = default;
            DeleteaccountDate = default;
            DischargeDisposition = default;
            DischargedToLocation = default;
            DietType = default;
            ServicingFacility = default;
            BedStatus = default;
            AccountStatus = default;
            PendingLocation = default;
            PriorTemporaryLocation = default;
            AdmitDateTime = default;
            CurrentPatientBalance = default;
            TotalCharges = default;
            TotalAdjustments = default;
            TotalPayment = default;
            AlternateVisitID = default;
            VisitIndicator = default;
            ServiceEpisodeDescription = default;
            ServiceEpisodeIdentifier = default;
            
            AttendingDoctor = Array.Empty<XCN>();
            ReferringDoctor = Array.Empty<XCN>();
            ConsultingDoctor = Array.Empty<XCN>();
            AmbulatoryStatus = Array.Empty<IS>();
            AdmittingDoctor = Array.Empty<XCN>();
            FinancialClass = Array.Empty<ST>();
            ContractCode = Array.Empty<IS>();
            ContractEffectiveDate = Array.Empty<DT>();
            ContractAmount = Array.Empty<NM>();
            ContractPeriod = Array.Empty<NM>();
            DischargeDateTime = Array.Empty<DTM>();
            OtherHealthcareProvider = Array.Empty<XCN>();
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: PatientClass = new IS(value); break;
                case 3:
                    var pl3 = new PL();
                    pl3.Parse(value.AsSpan(), delimiters);
                    AssignedPatientLocation = pl3;
                    break;
                case 4: AdmissionType = new IS(value); break;
                case 5: PreadmitNumber = value; break;
                case 6:
                    var pl6 = new PL();
                    pl6.Parse(value.AsSpan(), delimiters);
                    PriorPatientLocation = pl6;
                    break;
                case 7: AttendingDoctor = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 8: ReferringDoctor = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 9: ConsultingDoctor = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 10: HospitalService = new IS(value); break;
                case 11:
                    var pl11 = new PL();
                    pl11.Parse(value.AsSpan(), delimiters);
                    TemporaryLocation = pl11;
                    break;
                case 12: PreadmitTestIndicator = new IS(value); break;
                case 13: ReadmissionIndicator = new IS(value); break;
                case 14: AdmitSource = new IS(value); break;
                case 15: AmbulatoryStatus = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters); break;
                case 16: VipIndicator = new IS(value); break;
                case 17: AdmittingDoctor = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 18: PatientType = new IS(value); break;
                case 19: VisitNumber = value; break;
                case 20: FinancialClass = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 21: ChargePriceIndicator = new IS(value); break;
                case 22: CourtesyCode = new IS(value); break;
                case 23: CreditRating = new IS(value); break;
                case 24: ContractCode = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters); break;
                case 25: ContractEffectiveDate = SegmentFieldHelper.ParseRepeatingField<DT>(value, delimiters); break;
                case 26: ContractAmount = SegmentFieldHelper.ParseRepeatingField<NM>(value, delimiters); break;
                case 27: ContractPeriod = SegmentFieldHelper.ParseRepeatingField<NM>(value, delimiters); break;
                case 28: InterestCode = new IS(value); break;
                case 29: TransferToBadDebtCode = new IS(value); break;
                case 30: TransferToBadDebtDate = new DT(value); break;
                case 31: BadDebtAgencyCode = new IS(value); break;
                case 32: BadDebtTransferAmount = new NM(value); break;
                case 33: BadDebtRecoveryAmount = new NM(value); break;
                case 34: DeleteAccountIndicator = new IS(value); break;
                case 35: DeleteaccountDate = new DT(value); break;
                case 36: DischargeDisposition = new IS(value); break;
                case 37: DischargedToLocation = new ST(value); break;
                case 38: DietType = value; break;
                case 39: ServicingFacility = new IS(value); break;
                case 40: BedStatus = new IS(value); break;
                case 41: AccountStatus = new IS(value); break;
                case 42:
                    var pl42 = new PL();
                    pl42.Parse(value.AsSpan(), delimiters);
                    PendingLocation = pl42;
                    break;
                case 43:
                    var pl43 = new PL();
                    pl43.Parse(value.AsSpan(), delimiters);
                    PriorTemporaryLocation = pl43;
                    break;
                case 44: AdmitDateTime = new DTM(value); break;
                case 45: DischargeDateTime = SegmentFieldHelper.ParseRepeatingField<DTM>(value, delimiters); break;
                case 46: CurrentPatientBalance = new NM(value); break;
                case 47: TotalCharges = new NM(value); break;
                case 48: TotalAdjustments = new NM(value); break;
                case 49: TotalPayment = new NM(value); break;
                case 50: AlternateVisitID = value; break;
                case 51: VisitIndicator = new IS(value); break;
                case 52: OtherHealthcareProvider = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 53: ServiceEpisodeDescription = new ST(value); break;
                case 54: ServiceEpisodeIdentifier = value; break;
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
                PatientClass.ToHL7String(delimiters),
                AssignedPatientLocation.ToHL7String(delimiters),
                AdmissionType.ToHL7String(delimiters),
                PreadmitNumber.ToHL7String(delimiters),
                PriorPatientLocation.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AttendingDoctor, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ReferringDoctor, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ConsultingDoctor, delimiters),
                HospitalService.ToHL7String(delimiters),
                TemporaryLocation.ToHL7String(delimiters),
                PreadmitTestIndicator.ToHL7String(delimiters),
                ReadmissionIndicator.ToHL7String(delimiters),
                AdmitSource.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AmbulatoryStatus, delimiters),
                VipIndicator.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AdmittingDoctor, delimiters),
                PatientType.ToHL7String(delimiters),
                VisitNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(FinancialClass, delimiters),
                ChargePriceIndicator.ToHL7String(delimiters),
                CourtesyCode.ToHL7String(delimiters),
                CreditRating.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContractCode, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContractEffectiveDate, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContractAmount, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContractPeriod, delimiters),
                InterestCode.ToHL7String(delimiters),
                TransferToBadDebtCode.ToHL7String(delimiters),
                TransferToBadDebtDate.ToHL7String(delimiters),
                BadDebtAgencyCode.ToHL7String(delimiters),
                BadDebtTransferAmount.ToHL7String(delimiters),
                BadDebtRecoveryAmount.ToHL7String(delimiters),
                DeleteAccountIndicator.ToHL7String(delimiters),
                DeleteaccountDate.ToHL7String(delimiters),
                DischargeDisposition.ToHL7String(delimiters),
                DischargedToLocation.ToHL7String(delimiters),
                DietType.ToHL7String(delimiters),
                ServicingFacility.ToHL7String(delimiters),
                BedStatus.ToHL7String(delimiters),
                AccountStatus.ToHL7String(delimiters),
                PendingLocation.ToHL7String(delimiters),
                PriorTemporaryLocation.ToHL7String(delimiters),
                AdmitDateTime.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(DischargeDateTime, delimiters),
                CurrentPatientBalance.ToHL7String(delimiters),
                TotalCharges.ToHL7String(delimiters),
                TotalAdjustments.ToHL7String(delimiters),
                TotalPayment.ToHL7String(delimiters),
                AlternateVisitID.ToHL7String(delimiters),
                VisitIndicator.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OtherHealthcareProvider, delimiters),
                ServiceEpisodeDescription.ToHL7String(delimiters),
                ServiceEpisodeIdentifier.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var values = GetValues();
            return index >= 0 && index < values.Length ? values[index] : null;
        }
    }
}
