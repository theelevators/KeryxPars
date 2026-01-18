using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy/Treatment Dispense
    /// </summary>
    public class RXD : ISegment
    {
        public string SegmentId => nameof(RXD);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXD.1 - Dispense Sub-ID Counter
        /// </summary>
        public NM DispenseSubIDCounter { get; set; }

        /// <summary>
        /// RXD.2 - Dispense/Give Code
        /// </summary>
        public CE DispenseGiveCode { get; set; }

        /// <summary>
        /// RXD.3 - Date/Time Dispensed
        /// </summary>
        public DTM DateTimeDispensed { get; set; }

        /// <summary>
        /// RXD.4 - Actual Dispense Amount
        /// </summary>
        public NM ActualDispenseAmount { get; set; }

        /// <summary>
        /// RXD.5 - Actual Dispense Units
        /// </summary>
        public CE ActualDispenseUnits { get; set; }

        /// <summary>
        /// RXD.6 - Actual Dosage Form
        /// </summary>
        public CE ActualDosageForm { get; set; }

        /// <summary>
        /// RXD.7 - Prescription Number
        /// </summary>
        public ST PrescriptionNumber { get; set; }

        /// <summary>
        /// RXD.8 - Number of Refills Remaining
        /// </summary>
        public NM NumberOfRefillsRemaining { get; set; }

        /// <summary>
        /// RXD.9 - Dispense Notes (repeating)
        /// </summary>
        public ST[] DispenseNotes { get; set; }

        /// <summary>
        /// RXD.10 - Dispensing Provider (repeating)
        /// </summary>
        public XCN[] DispensingProvider { get; set; }

        /// <summary>
        /// RXD.11 - Substitution Status
        /// </summary>
        public ID SubstitutionStatus { get; set; }

        /// <summary>
        /// RXD.12 - Total Daily Dose
        /// </summary>
        public CQ TotalDailyDose { get; set; }

        /// <summary>
        /// RXD.13 - Dispense-to Location
        /// </summary>
        public PL DispenseToLocation { get; set; }

        /// <summary>
        /// RXD.14 - Needs Human Review
        /// </summary>
        public ID NeedsHumanReview { get; set; }

        /// <summary>
        /// RXD.15 - Pharmacy/Treatment Supplier's Special Dispensing Instructions (repeating)
        /// </summary>
        public CE[] PharmacyTreatmentSupplierSpecialDispensingInstructions { get; set; }

        /// <summary>
        /// RXD.16 - Actual Strength
        /// </summary>
        public NM ActualStrength { get; set; }

        /// <summary>
        /// RXD.17 - Actual Strength Unit
        /// </summary>
        public CE ActualStrengthUnit { get; set; }

        /// <summary>
        /// RXD.18 - Substance Lot Number (repeating)
        /// </summary>
        public ST[] SubstanceLotNumber { get; set; }

        /// <summary>
        /// RXD.19 - Substance Expiration Date (repeating)
        /// </summary>
        public DTM[] SubstanceExpirationDate { get; set; }

        /// <summary>
        /// RXD.20 - Substance Manufacturer Name (repeating)
        /// </summary>
        public CE[] SubstanceManufacturerName { get; set; }

        /// <summary>
        /// RXD.21 - Indication (repeating)
        /// </summary>
        public CE[] Indication { get; set; }

        /// <summary>
        /// RXD.22 - Dispense Package Size
        /// </summary>
        public NM DispensePackageSize { get; set; }

        /// <summary>
        /// RXD.23 - Dispense Package Size Unit
        /// </summary>
        public CE DispensePackageSizeUnit { get; set; }

        /// <summary>
        /// RXD.24 - Dispense Package Method
        /// </summary>
        public ID DispensePackageMethod { get; set; }

        public RXD()
        {
            SegmentType = SegmentType.MedOrder;
            DispenseSubIDCounter = default;
            DispenseGiveCode = default;
            DateTimeDispensed = default;
            ActualDispenseAmount = default;
            ActualDispenseUnits = default;
            ActualDosageForm = default;
            PrescriptionNumber = default;
            NumberOfRefillsRemaining = default;
            DispenseNotes = [];
            DispensingProvider = [];
            SubstitutionStatus = default;
            TotalDailyDose = default;
            DispenseToLocation = default;
            NeedsHumanReview = default;
            PharmacyTreatmentSupplierSpecialDispensingInstructions = [];
            ActualStrength = default;
            ActualStrengthUnit = default;
            SubstanceLotNumber = [];
            SubstanceExpirationDate = [];
            SubstanceManufacturerName = [];
            Indication = [];
            DispensePackageSize = default;
            DispensePackageSizeUnit = default;
            DispensePackageMethod = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: DispenseSubIDCounter = new NM(value); break;
                case 2:
                    var ce2 = new CE();
                    ce2.Parse(value.AsSpan(), delimiters);
                    DispenseGiveCode = ce2;
                    break;
                case 3: DateTimeDispensed = new DTM(value); break;
                case 4: ActualDispenseAmount = new NM(value); break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    ActualDispenseUnits = ce5;
                    break;
                case 6:
                    var ce6 = new CE();
                    ce6.Parse(value.AsSpan(), delimiters);
                    ActualDosageForm = ce6;
                    break;
                case 7: PrescriptionNumber = new ST(value); break;
                case 8: NumberOfRefillsRemaining = new NM(value); break;
                case 9: DispenseNotes = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 10: DispensingProvider = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 11: SubstitutionStatus = new ID(value); break;
                case 12:
                    var cq12 = new CQ();
                    cq12.Parse(value.AsSpan(), delimiters);
                    TotalDailyDose = cq12;
                    break;
                case 13:
                    var pl13 = new PL();
                    pl13.Parse(value.AsSpan(), delimiters);
                    DispenseToLocation = pl13;
                    break;
                case 14: NeedsHumanReview = new ID(value); break;
                case 15: PharmacyTreatmentSupplierSpecialDispensingInstructions = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 16: ActualStrength = new NM(value); break;
                case 17:
                    var ce17 = new CE();
                    ce17.Parse(value.AsSpan(), delimiters);
                    ActualStrengthUnit = ce17;
                    break;
                case 18: SubstanceLotNumber = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 19: SubstanceExpirationDate = SegmentFieldHelper.ParseRepeatingField<DTM>(value, delimiters); break;
                case 20: SubstanceManufacturerName = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 21: Indication = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 22: DispensePackageSize = new NM(value); break;
                case 23:
                    var ce23 = new CE();
                    ce23.Parse(value.AsSpan(), delimiters);
                    DispensePackageSizeUnit = ce23;
                    break;
                case 24: DispensePackageMethod = new ID(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                DispenseSubIDCounter.ToHL7String(delimiters),
                DispenseGiveCode.ToHL7String(delimiters),
                DateTimeDispensed.ToHL7String(delimiters),
                ActualDispenseAmount.ToHL7String(delimiters),
                ActualDispenseUnits.ToHL7String(delimiters),
                ActualDosageForm.ToHL7String(delimiters),
                PrescriptionNumber.ToHL7String(delimiters),
                NumberOfRefillsRemaining.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(DispenseNotes, delimiters),
                SegmentFieldHelper.JoinRepeatingField(DispensingProvider, delimiters),
                SubstitutionStatus.ToHL7String(delimiters),
                TotalDailyDose.ToHL7String(delimiters),
                DispenseToLocation.ToHL7String(delimiters),
                NeedsHumanReview.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmacyTreatmentSupplierSpecialDispensingInstructions, delimiters),
                ActualStrength.ToHL7String(delimiters),
                ActualStrengthUnit.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceLotNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceExpirationDate, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceManufacturerName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                DispensePackageSize.ToHL7String(delimiters),
                DispensePackageSizeUnit.ToHL7String(delimiters),
                DispensePackageMethod.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => DispenseSubIDCounter.Value,
                2 => DispenseGiveCode.ToHL7String(delimiters),
                3 => DateTimeDispensed.Value,
                4 => ActualDispenseAmount.Value,
                5 => ActualDispenseUnits.ToHL7String(delimiters),
                6 => ActualDosageForm.ToHL7String(delimiters),
                7 => PrescriptionNumber.Value,
                8 => NumberOfRefillsRemaining.Value,
                9 => SegmentFieldHelper.JoinRepeatingField(DispenseNotes, delimiters),
                10 => SegmentFieldHelper.JoinRepeatingField(DispensingProvider, delimiters),
                11 => SubstitutionStatus.Value,
                12 => TotalDailyDose.ToHL7String(delimiters),
                13 => DispenseToLocation.ToHL7String(delimiters),
                14 => NeedsHumanReview.Value,
                15 => SegmentFieldHelper.JoinRepeatingField(PharmacyTreatmentSupplierSpecialDispensingInstructions, delimiters),
                16 => ActualStrength.Value,
                17 => ActualStrengthUnit.ToHL7String(delimiters),
                18 => SegmentFieldHelper.JoinRepeatingField(SubstanceLotNumber, delimiters),
                19 => SegmentFieldHelper.JoinRepeatingField(SubstanceExpirationDate, delimiters),
                20 => SegmentFieldHelper.JoinRepeatingField(SubstanceManufacturerName, delimiters),
                21 => SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                22 => DispensePackageSize.Value,
                23 => DispensePackageSizeUnit.ToHL7String(delimiters),
                24 => DispensePackageMethod.Value,
                _ => null
            };
        }
    }
}
