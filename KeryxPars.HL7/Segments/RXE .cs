using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy Encoded Order
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class RXE : ISegment
    {
        public string SegmentId => nameof(RXE);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXE.1 - Quantity/Timing - deprecated, use TQ1/TQ2
        /// </summary>
        public ST QuantityTiming { get; set; }

        /// <summary>
        /// RXE.2 - Give Code
        /// </summary>
        public CE GiveCode { get; set; }

        /// <summary>
        /// RXE.3 - Give Amount - Minimum
        /// </summary>
        public NM GiveAmountMinimum { get; set; }

        /// <summary>
        /// RXE.4 - Give Amount - Maximum
        /// </summary>
        public NM GiveAmountMaximum { get; set; }

        /// <summary>
        /// RXE.5 - Give Units
        /// </summary>
        public CE GiveUnits { get; set; }

        /// <summary>
        /// RXE.6 - Give Dosage Form
        /// </summary>
        public CE GiveDosageForm { get; set; }

        /// <summary>
        /// RXE.7 - Provider's Administration Instructions (repeating)
        /// </summary>
        public CE[] ProvidersAdministrationInstructions { get; set; }

        /// <summary>
        /// RXE.8 - Deliver-To Location
        /// </summary>
        public ST DeliverToLocation { get; set; }

        /// <summary>
        /// RXE.9 - Substitution Status
        /// </summary>
        public ID SubstitutionStatus { get; set; }

        /// <summary>
        /// RXE.10 - Dispense Amount
        /// </summary>
        public NM DispenseAmount { get; set; }

        /// <summary>
        /// RXE.11 - Dispense Units
        /// </summary>
        public CE DispenseUnits { get; set; }

        /// <summary>
        /// RXE.12 - Number Of Refills
        /// </summary>
        public NM NumberOfRefills { get; set; }

        /// <summary>
        /// RXE.13 - Ordering Provider's DEA Number (repeating)
        /// </summary>
        public XCN[] OrderingProvidersDeaNumber { get; set; }

        /// <summary>
        /// RXE.14 - Pharmacist/Treatment Supplier's Verifier ID (repeating)
        /// </summary>
        public XCN[] PharmacistTreatmentSuppliersVerifierID { get; set; }

        /// <summary>
        /// RXE.15 - Prescription Number
        /// </summary>
        public ST PrescriptionNumber { get; set; }

        /// <summary>
        /// RXE.16 - Number of Refills Remaining
        /// </summary>
        public NM NumberOfRefillsRemaining { get; set; }

        /// <summary>
        /// RXE.17 - Number of Refills/Doses Dispensed
        /// </summary>
        public NM NumberOfRefillsDosesDispensed { get; set; }

        /// <summary>
        /// RXE.18 - D/T of Most Recent Refill or Dose Dispensed
        /// </summary>
        public DTM DateOfMostRecentRefillOrDoseDispensed { get; set; }

        /// <summary>
        /// RXE.19 - Total Daily Dose
        /// </summary>
        public CQ TotalDailyDose { get; set; }

        /// <summary>
        /// RXE.20 - Needs Human Review
        /// </summary>
        public ID NeedsHumanReview { get; set; }

        /// <summary>
        /// RXE.21 - Pharmacy/Treatment Supplier's Special Dispensing Instructions (repeating)
        /// </summary>
        public CE[] PharmacyTreatmentSuppliersSpecialDispensingInstructions { get; set; }

        /// <summary>
        /// RXE.22 - Give Per (Time Unit)
        /// </summary>
        public ST GivePerTimeUnit { get; set; }

        /// <summary>
        /// RXE.23 - Give Rate Amount
        /// </summary>
        public ST GiveRateAmount { get; set; }

        /// <summary>
        /// RXE.24 - Give Rate Units
        /// </summary>
        public CE GiveRateUnits { get; set; }

        /// <summary>
        /// RXE.25 - Give Strength
        /// </summary>
        public NM GiveStrength { get; set; }

        /// <summary>
        /// RXE.26 - Give Strength Units
        /// </summary>
        public CE GiveStrengthUnits { get; set; }

        /// <summary>
        /// RXE.27 - Give Indication (repeating)
        /// </summary>
        public CE[] GiveIndication { get; set; }

        /// <summary>
        /// RXE.28 - Dispense Package Size
        /// </summary>
        public NM DispensePackageSize { get; set; }

        /// <summary>
        /// RXE.29 - Dispense Package Size Unit
        /// </summary>
        public CE DispensePackageSizeUnit { get; set; }

        /// <summary>
        /// RXE.30 - Dispense Package Method
        /// </summary>
        public ID DispensePackageMethod { get; set; }

        /// <summary>
        /// RXE.31 - Supplementary Code (repeating)
        /// </summary>
        public CE[] SupplementaryCode { get; set; }

        /// <summary>
        /// RXE.32 - Original Order Date/Time
        /// </summary>
        public DTM OriginalOrderDateTime { get; set; }

        /// <summary>
        /// RXE.33 - Give Drug Strength Volume
        /// </summary>
        public NM GiveDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXE.34 - Give Drug Strength Volume Units
        /// </summary>
        public CWE GiveDrugStrengthVolumeUnits { get; set; }

        /// <summary>
        /// RXE.35 - Controlled Substance Schedule
        /// </summary>
        public CWE ControlledSubstanceSchedule { get; set; }

        /// <summary>
        /// RXE.36 - Formulary Status
        /// </summary>
        public ID FormularyStatus { get; set; }

        /// <summary>
        /// RXE.37 - Pharmaceutical Substance Alternative (repeating)
        /// </summary>
        public CWE[] PharmaceuticalSubstanceAlternative { get; set; }

        /// <summary>
        /// RXE.38 - Pharmacy of Most Recent Fill
        /// </summary>
        public CWE PharmacyOfMostRecentFill { get; set; }

        /// <summary>
        /// RXE.39 - Initial Dispense Amount
        /// </summary>
        public NM InitialDispenseAmount { get; set; }

        /// <summary>
        /// RXE.40 - Dispensing Pharmacy
        /// </summary>
        public CWE DispensingPharmacy { get; set; }

        /// <summary>
        /// RXE.41 - Dispensing Pharmacy Address
        /// </summary>
        public XAD DispensingPharmacyAddress { get; set; }

        /// <summary>
        /// RXE.42 - Deliver-to Patient Location
        /// </summary>
        public PL DeliverToPatientLocation { get; set; }

        /// <summary>
        /// RXE.43 - Deliver-to Address
        /// </summary>
        public XAD DeliverToAddress { get; set; }

        /// <summary>
        /// RXE.44 - Pharmacy Order Type
        /// </summary>
        public ID PharmacyOrderType { get; set; }

        /// <summary>
        /// RXE.45 - Pharmacy Phone Number (repeating)
        /// </summary>
        public XTN[] PharmacyPhoneNumber { get; set; }

        public RXE()
        {
            SegmentType = SegmentType.MedOrder;
            
            QuantityTiming = default;
            GiveCode = default;
            GiveAmountMinimum = default;
            GiveAmountMaximum = default;
            GiveUnits = default;
            GiveDosageForm = default;
            DeliverToLocation = default;
            SubstitutionStatus = default;
            DispenseAmount = default;
            DispenseUnits = default;
            NumberOfRefills = default;
            PrescriptionNumber = default;
            NumberOfRefillsRemaining = default;
            NumberOfRefillsDosesDispensed = default;
            DateOfMostRecentRefillOrDoseDispensed = default;
            TotalDailyDose = default;
            NeedsHumanReview = default;
            GivePerTimeUnit = default;
            GiveRateAmount = default;
            GiveRateUnits = default;
            GiveStrength = default;
            GiveStrengthUnits = default;
            DispensePackageSize = default;
            DispensePackageSizeUnit = default;
            DispensePackageMethod = default;
            OriginalOrderDateTime = default;
            GiveDrugStrengthVolume = default;
            GiveDrugStrengthVolumeUnits = default;
            ControlledSubstanceSchedule = default;
            FormularyStatus = default;
            PharmacyOfMostRecentFill = default;
            InitialDispenseAmount = default;
            DispensingPharmacy = default;
            DispensingPharmacyAddress = default;
            DeliverToPatientLocation = default;
            DeliverToAddress = default;
            PharmacyOrderType = default;
            
            ProvidersAdministrationInstructions = Array.Empty<CE>();
            OrderingProvidersDeaNumber = Array.Empty<XCN>();
            PharmacistTreatmentSuppliersVerifierID = Array.Empty<XCN>();
            PharmacyTreatmentSuppliersSpecialDispensingInstructions = Array.Empty<CE>();
            GiveIndication = Array.Empty<CE>();
            SupplementaryCode = Array.Empty<CE>();
            PharmaceuticalSubstanceAlternative = Array.Empty<CWE>();
            PharmacyPhoneNumber = Array.Empty<XTN>();
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: QuantityTiming = new ST(value); break;
                case 2: GiveCode = value; break;
                case 3: GiveAmountMinimum = new NM(value); break;
                case 4: GiveAmountMaximum = new NM(value); break;
                case 5: GiveUnits = value; break;
                case 6: GiveDosageForm = value; break;
                case 7: ProvidersAdministrationInstructions = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 8: DeliverToLocation = new ST(value); break;
                case 9: SubstitutionStatus = new ID(value); break;
                case 10: DispenseAmount = new NM(value); break;
                case 11: DispenseUnits = value; break;
                case 12: NumberOfRefills = new NM(value); break;
                case 13: OrderingProvidersDeaNumber = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 14: PharmacistTreatmentSuppliersVerifierID = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 15: PrescriptionNumber = new ST(value); break;
                case 16: NumberOfRefillsRemaining = new NM(value); break;
                case 17: NumberOfRefillsDosesDispensed = new NM(value); break;
                case 18: DateOfMostRecentRefillOrDoseDispensed = new DTM(value); break;
                case 19: TotalDailyDose = value; break;
                case 20: NeedsHumanReview = new ID(value); break;
                case 21: PharmacyTreatmentSuppliersSpecialDispensingInstructions = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 22: GivePerTimeUnit = new ST(value); break;
                case 23: GiveRateAmount = new ST(value); break;
                case 24: GiveRateUnits = value; break;
                case 25: GiveStrength = new NM(value); break;
                case 26: GiveStrengthUnits = value; break;
                case 27: GiveIndication = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 28: DispensePackageSize = new NM(value); break;
                case 29: DispensePackageSizeUnit = value; break;
                case 30: DispensePackageMethod = new ID(value); break;
                case 31: SupplementaryCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 32: OriginalOrderDateTime = new DTM(value); break;
                case 33: GiveDrugStrengthVolume = new NM(value); break;
                case 34: GiveDrugStrengthVolumeUnits = value; break;
                case 35: ControlledSubstanceSchedule = value; break;
                case 36: FormularyStatus = new ID(value); break;
                case 37: PharmaceuticalSubstanceAlternative = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 38: PharmacyOfMostRecentFill = value; break;
                case 39: InitialDispenseAmount = new NM(value); break;
                case 40: DispensingPharmacy = value; break;
                case 41: DispensingPharmacyAddress = value; break;
                case 42:
                    var pl = new PL();
                    pl.Parse(value.AsSpan(), delimiters);
                    DeliverToPatientLocation = pl;
                    break;
                case 43: DeliverToAddress = value; break;
                case 44: PharmacyOrderType = new ID(value); break;
                case 45: PharmacyPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                QuantityTiming.ToHL7String(delimiters),
                GiveCode.ToHL7String(delimiters),
                GiveAmountMinimum.ToHL7String(delimiters),
                GiveAmountMaximum.ToHL7String(delimiters),
                GiveUnits.ToHL7String(delimiters),
                GiveDosageForm.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProvidersAdministrationInstructions, delimiters),
                DeliverToLocation.ToHL7String(delimiters),
                SubstitutionStatus.ToHL7String(delimiters),
                DispenseAmount.ToHL7String(delimiters),
                DispenseUnits.ToHL7String(delimiters),
                NumberOfRefills.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingProvidersDeaNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmacistTreatmentSuppliersVerifierID, delimiters),
                PrescriptionNumber.ToHL7String(delimiters),
                NumberOfRefillsRemaining.ToHL7String(delimiters),
                NumberOfRefillsDosesDispensed.ToHL7String(delimiters),
                DateOfMostRecentRefillOrDoseDispensed.ToHL7String(delimiters),
                TotalDailyDose.ToHL7String(delimiters),
                NeedsHumanReview.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmacyTreatmentSuppliersSpecialDispensingInstructions, delimiters),
                GivePerTimeUnit.ToHL7String(delimiters),
                GiveRateAmount.ToHL7String(delimiters),
                GiveRateUnits.ToHL7String(delimiters),
                GiveStrength.ToHL7String(delimiters),
                GiveStrengthUnits.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(GiveIndication, delimiters),
                DispensePackageSize.ToHL7String(delimiters),
                DispensePackageSizeUnit.ToHL7String(delimiters),
                DispensePackageMethod.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SupplementaryCode, delimiters),
                OriginalOrderDateTime.ToHL7String(delimiters),
                GiveDrugStrengthVolume.ToHL7String(delimiters),
                GiveDrugStrengthVolumeUnits.ToHL7String(delimiters),
                ControlledSubstanceSchedule.ToHL7String(delimiters),
                FormularyStatus.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmaceuticalSubstanceAlternative, delimiters),
                PharmacyOfMostRecentFill.ToHL7String(delimiters),
                InitialDispenseAmount.ToHL7String(delimiters),
                DispensingPharmacy.ToHL7String(delimiters),
                DispensingPharmacyAddress.ToHL7String(delimiters),
                DeliverToPatientLocation.ToHL7String(delimiters),
                DeliverToAddress.ToHL7String(delimiters),
                PharmacyOrderType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmacyPhoneNumber, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var values = GetValues();
            return index >= 0 && index < values.Length ? values[index] : null;
        }
    }
}
