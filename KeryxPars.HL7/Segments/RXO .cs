using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy Prescription Order
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class RXO : ISegment
    {
        public string SegmentId => nameof(RXO);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXO.1 - Requested Give Code
        /// </summary>
        public CE RequestedGiveCode { get; set; }

        /// <summary>
        /// RXO.2 - Requested Give Amount - Minimum
        /// </summary>
        public NM RequestedGiveAmountMinimum { get; set; }

        /// <summary>
        /// RXO.3 - Requested Give Amount - Maximum
        /// </summary>
        public NM RequestedGiveAmountMaximum { get; set; }

        /// <summary>
        /// RXO.4 - Requested Give Units
        /// </summary>
        public CE RequestedGiveUnits { get; set; }

        /// <summary>
        /// RXO.5 - Requested Dosage Form
        /// </summary>
        public CE RequestedDosageForm { get; set; }

        /// <summary>
        /// RXO.6 - Provider's Pharmacy/Treatment Instructions
        /// </summary>
        public CE[] ProvidersPharmacyTreatmentInstructions { get; set; }

        /// <summary>
        /// RXO.7 - Provider's Administration Instructions
        /// </summary>
        public CE[] ProvidersAdministrationInstructions { get; set; }

        /// <summary>
        /// RXO.8 - Deliver-To Location
        /// </summary>
        public PL DeliverToLocation { get; set; }

        /// <summary>
        /// RXO.9 - Allow Substitutions
        /// </summary>
        public ID AllowSubstitutions { get; set; }

        /// <summary>
        /// RXO.10 - Requested Dispense Code
        /// </summary>
        public CE RequestedDispenseCode { get; set; }

        /// <summary>
        /// RXO.11 - Requested Dispense Amount
        /// </summary>
        public NM RequestedDispenseAmount { get; set; }

        /// <summary>
        /// RXO.12 - Requested Dispense Units
        /// </summary>
        public CE RequestedDispenseUnits { get; set; }

        /// <summary>
        /// RXO.13 - Number Of Refills
        /// </summary>
        public NM NumberOfRefills { get; set; }

        /// <summary>
        /// RXO.14 - Ordering Provider's DEA Number
        /// </summary>
        public XCN[] OrderingProvidersDeaNumber { get; set; }

        /// <summary>
        /// RXO.15 - Pharmacist/Treatment Supplier's Verifier ID
        /// </summary>
        public XCN[] PharmacistTreatmentSuppliersVerifierID { get; set; }

        /// <summary>
        /// RXO.16 - Needs Human Review
        /// </summary>
        public ID NeedsHumanReview { get; set; }

        /// <summary>
        /// RXO.17 - Requested Give Per (Time Unit)
        /// </summary>
        public ST RequestedGivePerTimeUnit { get; set; }

        /// <summary>
        /// RXO.18 - Requested Give Strength
        /// </summary>
        public NM RequestedGiveStrength { get; set; }

        /// <summary>
        /// RXO.19 - Requested Give Strength Units
        /// </summary>
        public CE RequestedGiveStrengthUnits { get; set; }

        /// <summary>
        /// RXO.20 - Indication
        /// </summary>
        public CE[] Indication { get; set; }

        /// <summary>
        /// RXO.21 - Requested Give Rate Amount
        /// </summary>
        public ST RequestedGiveRateAmount { get; set; }

        /// <summary>
        /// RXO.22 - Requested Give Rate Units
        /// </summary>
        public CE RequestedGiveRateUnits { get; set; }

        /// <summary>
        /// RXO.23 - Total Daily Dose
        /// </summary>
        public CQ TotalDailyDose { get; set; }

        /// <summary>
        /// RXO.24 - Supplementary Code
        /// </summary>
        public CE[] SupplementaryCode { get; set; }

        /// <summary>
        /// RXO.25 - Requested Drug Strength Volume
        /// </summary>
        public NM RequestedDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXO.26 - Requested Drug Strength Volume Units
        /// </summary>
        public CWE RequestedDrugStrengthVolumeUnits { get; set; }

        /// <summary>
        /// RXO.27 - Pharmacy Order Type
        /// </summary>
        public ID PharmacyOrderType { get; set; }

        /// <summary>
        /// RXO.28 - Dispensing Interval
        /// </summary>
        public NM DispensingInterval { get; set; }

        /// <summary>
        /// RXO.29 - Medication Instance Identifier
        /// </summary>
        public EI MedicationInstanceIdentifier { get; set; }

        /// <summary>
        /// RXO.30 - Segment Instance Identifier
        /// </summary>
        public EI SegmentInstanceIdentifier { get; set; }

        /// <summary>
        /// RXO.31 - Mood Code
        /// </summary>
        public ID MoodCode { get; set; }

        /// <summary>
        /// RXO.32 - Dispensing Pharmacy
        /// </summary>
        public CWE DispensingPharmacy { get; set; }

        /// <summary>
        /// RXO.33 - Dispensing Pharmacy Address
        /// </summary>
        public XAD DispensingPharmacyAddress { get; set; }

        /// <summary>
        /// RXO.34 - Deliver-to Patient Location
        /// </summary>
        public PL DeliverToPatientLocation { get; set; }

        /// <summary>
        /// RXO.35 - Deliver-to Address
        /// </summary>
        public XAD DeliverToAddress { get; set; }

        /// <summary>
        /// RXO.36 - Pharmacy Phone Number
        /// </summary>
        public XTN[] PharmacyPhoneNumber { get; set; }

        public RXO()
        {
            SegmentType = SegmentType.MedOrder;
            RequestedGiveCode = default;
            RequestedGiveAmountMinimum = default;
            RequestedGiveAmountMaximum = default;
            RequestedGiveUnits = default;
            RequestedDosageForm = default;
            ProvidersPharmacyTreatmentInstructions = [];
            ProvidersAdministrationInstructions = [];
            DeliverToLocation = default;
            AllowSubstitutions = default;
            RequestedDispenseCode = default;
            RequestedDispenseAmount = default;
            RequestedDispenseUnits = default;
            NumberOfRefills = default;
            OrderingProvidersDeaNumber = [];
            PharmacistTreatmentSuppliersVerifierID = [];
            NeedsHumanReview = default;
            RequestedGivePerTimeUnit = default;
            RequestedGiveStrength = default;
            RequestedGiveStrengthUnits = default;
            Indication = [];
            RequestedGiveRateAmount = default;
            RequestedGiveRateUnits = default;
            TotalDailyDose = default;
            SupplementaryCode = [];
            RequestedDrugStrengthVolume = default;
            RequestedDrugStrengthVolumeUnits = default;
            PharmacyOrderType = default;
            DispensingInterval = default;
            MedicationInstanceIdentifier = default;
            SegmentInstanceIdentifier = default;
            MoodCode = default;
            DispensingPharmacy = default;
            DispensingPharmacyAddress = default;
            DeliverToPatientLocation = default;
            DeliverToAddress = default;
            PharmacyPhoneNumber = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ce1 = new CE();
                    ce1.Parse(value.AsSpan(), delimiters);
                    RequestedGiveCode = ce1;
                    break;
                case 2: RequestedGiveAmountMinimum = new NM(value); break;
                case 3: RequestedGiveAmountMaximum = new NM(value); break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    RequestedGiveUnits = ce4;
                    break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    RequestedDosageForm = ce5;
                    break;
                case 6:
                    ProvidersPharmacyTreatmentInstructions = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 7:
                    ProvidersAdministrationInstructions = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 8:
                    var pl8 = new PL();
                    pl8.Parse(value.AsSpan(), delimiters);
                    DeliverToLocation = pl8;
                    break;
                case 9: AllowSubstitutions = new ID(value); break;
                case 10:
                    var ce10 = new CE();
                    ce10.Parse(value.AsSpan(), delimiters);
                    RequestedDispenseCode = ce10;
                    break;
                case 11: RequestedDispenseAmount = new NM(value); break;
                case 12:
                    var ce12 = new CE();
                    ce12.Parse(value.AsSpan(), delimiters);
                    RequestedDispenseUnits = ce12;
                    break;
                case 13: NumberOfRefills = new NM(value); break;
                case 14:
                    OrderingProvidersDeaNumber = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters);
                    break;
                case 15:
                    PharmacistTreatmentSuppliersVerifierID = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters);
                    break;
                case 16: NeedsHumanReview = new ID(value); break;
                case 17: RequestedGivePerTimeUnit = new ST(value); break;
                case 18: RequestedGiveStrength = new NM(value); break;
                case 19:
                    var ce19 = new CE();
                    ce19.Parse(value.AsSpan(), delimiters);
                    RequestedGiveStrengthUnits = ce19;
                    break;
                case 20:
                    Indication = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 21: RequestedGiveRateAmount = new ST(value); break;
                case 22:
                    var ce22 = new CE();
                    ce22.Parse(value.AsSpan(), delimiters);
                    RequestedGiveRateUnits = ce22;
                    break;
                case 23:
                    var cq23 = new CQ();
                    cq23.Parse(value.AsSpan(), delimiters);
                    TotalDailyDose = cq23;
                    break;
                case 24:
                    SupplementaryCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 25: RequestedDrugStrengthVolume = new NM(value); break;
                case 26:
                    var cwe26 = new CWE();
                    cwe26.Parse(value.AsSpan(), delimiters);
                    RequestedDrugStrengthVolumeUnits = cwe26;
                    break;
                case 27: PharmacyOrderType = new ID(value); break;
                case 28: DispensingInterval = new NM(value); break;
                case 29:
                    var ei29 = new EI();
                    ei29.Parse(value.AsSpan(), delimiters);
                    MedicationInstanceIdentifier = ei29;
                    break;
                case 30:
                    var ei30 = new EI();
                    ei30.Parse(value.AsSpan(), delimiters);
                    SegmentInstanceIdentifier = ei30;
                    break;
                case 31: MoodCode = new ID(value); break;
                case 32:
                    var cwe32 = new CWE();
                    cwe32.Parse(value.AsSpan(), delimiters);
                    DispensingPharmacy = cwe32;
                    break;
                case 33:
                    var xad33 = new XAD();
                    xad33.Parse(value.AsSpan(), delimiters);
                    DispensingPharmacyAddress = xad33;
                    break;
                case 34:
                    var pl34 = new PL();
                    pl34.Parse(value.AsSpan(), delimiters);
                    DeliverToPatientLocation = pl34;
                    break;
                case 35:
                    var xad35 = new XAD();
                    xad35.Parse(value.AsSpan(), delimiters);
                    DeliverToAddress = xad35;
                    break;
                case 36:
                    PharmacyPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters);
                    break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                RequestedGiveCode.ToHL7String(delimiters),
                RequestedGiveAmountMinimum.ToHL7String(delimiters),
                RequestedGiveAmountMaximum.ToHL7String(delimiters),
                RequestedGiveUnits.ToHL7String(delimiters),
                RequestedDosageForm.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProvidersPharmacyTreatmentInstructions, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProvidersAdministrationInstructions, delimiters),
                DeliverToLocation.ToHL7String(delimiters),
                AllowSubstitutions.ToHL7String(delimiters),
                RequestedDispenseCode.ToHL7String(delimiters),
                RequestedDispenseAmount.ToHL7String(delimiters),
                RequestedDispenseUnits.ToHL7String(delimiters),
                NumberOfRefills.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingProvidersDeaNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmacistTreatmentSuppliersVerifierID, delimiters),
                NeedsHumanReview.ToHL7String(delimiters),
                RequestedGivePerTimeUnit.ToHL7String(delimiters),
                RequestedGiveStrength.ToHL7String(delimiters),
                RequestedGiveStrengthUnits.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                RequestedGiveRateAmount.ToHL7String(delimiters),
                RequestedGiveRateUnits.ToHL7String(delimiters),
                TotalDailyDose.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SupplementaryCode, delimiters),
                RequestedDrugStrengthVolume.ToHL7String(delimiters),
                RequestedDrugStrengthVolumeUnits.ToHL7String(delimiters),
                PharmacyOrderType.ToHL7String(delimiters),
                DispensingInterval.ToHL7String(delimiters),
                MedicationInstanceIdentifier.ToHL7String(delimiters),
                SegmentInstanceIdentifier.ToHL7String(delimiters),
                MoodCode.ToHL7String(delimiters),
                DispensingPharmacy.ToHL7String(delimiters),
                DispensingPharmacyAddress.ToHL7String(delimiters),
                DeliverToPatientLocation.ToHL7String(delimiters),
                DeliverToAddress.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmacyPhoneNumber, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => RequestedGiveCode.ToHL7String(delimiters),
                2 => RequestedGiveAmountMinimum.Value,
                3 => RequestedGiveAmountMaximum.Value,
                4 => RequestedGiveUnits.ToHL7String(delimiters),
                5 => RequestedDosageForm.ToHL7String(delimiters),
                6 => SegmentFieldHelper.JoinRepeatingField(ProvidersPharmacyTreatmentInstructions, delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(ProvidersAdministrationInstructions, delimiters),
                8 => DeliverToLocation.ToHL7String(delimiters),
                9 => AllowSubstitutions.Value,
                10 => RequestedDispenseCode.ToHL7String(delimiters),
                11 => RequestedDispenseAmount.Value,
                12 => RequestedDispenseUnits.ToHL7String(delimiters),
                13 => NumberOfRefills.Value,
                14 => SegmentFieldHelper.JoinRepeatingField(OrderingProvidersDeaNumber, delimiters),
                15 => SegmentFieldHelper.JoinRepeatingField(PharmacistTreatmentSuppliersVerifierID, delimiters),
                16 => NeedsHumanReview.Value,
                17 => RequestedGivePerTimeUnit.Value,
                18 => RequestedGiveStrength.Value,
                19 => RequestedGiveStrengthUnits.ToHL7String(delimiters),
                20 => SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                21 => RequestedGiveRateAmount.Value,
                22 => RequestedGiveRateUnits.ToHL7String(delimiters),
                23 => TotalDailyDose.ToHL7String(delimiters),
                24 => SegmentFieldHelper.JoinRepeatingField(SupplementaryCode, delimiters),
                25 => RequestedDrugStrengthVolume.Value,
                26 => RequestedDrugStrengthVolumeUnits.ToHL7String(delimiters),
                27 => PharmacyOrderType.Value,
                28 => DispensingInterval.Value,
                29 => MedicationInstanceIdentifier.ToHL7String(delimiters),
                30 => SegmentInstanceIdentifier.ToHL7String(delimiters),
                31 => MoodCode.Value,
                32 => DispensingPharmacy.ToHL7String(delimiters),
                33 => DispensingPharmacyAddress.ToHL7String(delimiters),
                34 => DeliverToPatientLocation.ToHL7String(delimiters),
                35 => DeliverToAddress.ToHL7String(delimiters),
                36 => SegmentFieldHelper.JoinRepeatingField(PharmacyPhoneNumber, delimiters),
                _ => null
            };
        }
    }
}
