using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy/Treatment Give
    /// </summary>
    public class RXG : ISegment
    {
        public string SegmentId => nameof(RXG);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXG.1 - Give Sub-ID Counter
        /// </summary>
        public NM GiveSubIDCounter { get; set; }

        /// <summary>
        /// RXG.2 - Dispense Sub-ID Counter
        /// </summary>
        public NM DispenseSubIDCounter { get; set; }

        /// <summary>
        /// RXG.3 - Quantity/Timing
        /// </summary>
        public ST QuantityTiming { get; set; }

        /// <summary>
        /// RXG.4 - Give Code
        /// </summary>
        public CE GiveCode { get; set; }

        /// <summary>
        /// RXG.5 - Give Amount - Minimum
        /// </summary>
        public NM GiveAmountMinimum { get; set; }

        /// <summary>
        /// RXG.6 - Give Amount - Maximum
        /// </summary>
        public NM GiveAmountMaximum { get; set; }

        /// <summary>
        /// RXG.7 - Give Units
        /// </summary>
        public CE GiveUnits { get; set; }

        /// <summary>
        /// RXG.8 - Give Dosage Form
        /// </summary>
        public CE GiveDosageForm { get; set; }

        /// <summary>
        /// RXG.9 - Administration Notes (repeating)
        /// </summary>
        public CE[] AdministrationNotes { get; set; }

        /// <summary>
        /// RXG.10 - Substitution Status
        /// </summary>
        public ID SubstitutionStatus { get; set; }

        /// <summary>
        /// RXG.11 - Dispense-to Location
        /// </summary>
        public PL DispenseToLocation { get; set; }

        /// <summary>
        /// RXG.12 - Needs Human Review
        /// </summary>
        public ID NeedsHumanReview { get; set; }

        /// <summary>
        /// RXG.13 - Pharmacy/Treatment Supplier's Special Administration Instructions (repeating)
        /// </summary>
        public CE[] PharmacyTreatmentSupplierSpecialAdministrationInstructions { get; set; }

        /// <summary>
        /// RXG.14 - Give Per (Time Unit)
        /// </summary>
        public ST GivePerTimeUnit { get; set; }

        /// <summary>
        /// RXG.15 - Give Rate Amount
        /// </summary>
        public ST GiveRateAmount { get; set; }

        /// <summary>
        /// RXG.16 - Give Rate Units
        /// </summary>
        public CE GiveRateUnits { get; set; }

        /// <summary>
        /// RXG.17 - Give Strength
        /// </summary>
        public NM GiveStrength { get; set; }

        /// <summary>
        /// RXG.18 - Give Strength Units
        /// </summary>
        public CE GiveStrengthUnits { get; set; }

        /// <summary>
        /// RXG.19 - Substance Lot Number (repeating)
        /// </summary>
        public ST[] SubstanceLotNumber { get; set; }

        /// <summary>
        /// RXG.20 - Substance Expiration Date (repeating)
        /// </summary>
        public DTM[] SubstanceExpirationDate { get; set; }

        /// <summary>
        /// RXG.21 - Substance Manufacturer Name (repeating)
        /// </summary>
        public CE[] SubstanceManufacturerName { get; set; }

        /// <summary>
        /// RXG.22 - Indication (repeating)
        /// </summary>
        public CE[] Indication { get; set; }

        /// <summary>
        /// RXG.23 - Give Drug Strength Volume
        /// </summary>
        public NM GiveDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXG.24 - Give Drug Strength Volume Units
        /// </summary>
        public CWE GiveDrugStrengthVolumeUnits { get; set; }

        /// <summary>
        /// RXG.25 - Give Barcode Identifier
        /// </summary>
        public CWE GiveBarcodeIdentifier { get; set; }

        public RXG()
        {
            SegmentType = SegmentType.MedOrder;
            GiveSubIDCounter = default;
            DispenseSubIDCounter = default;
            QuantityTiming = default;
            GiveCode = default;
            GiveAmountMinimum = default;
            GiveAmountMaximum = default;
            GiveUnits = default;
            GiveDosageForm = default;
            AdministrationNotes = [];
            SubstitutionStatus = default;
            DispenseToLocation = default;
            NeedsHumanReview = default;
            PharmacyTreatmentSupplierSpecialAdministrationInstructions = [];
            GivePerTimeUnit = default;
            GiveRateAmount = default;
            GiveRateUnits = default;
            GiveStrength = default;
            GiveStrengthUnits = default;
            SubstanceLotNumber = [];
            SubstanceExpirationDate = [];
            SubstanceManufacturerName = [];
            Indication = [];
            GiveDrugStrengthVolume = default;
            GiveDrugStrengthVolumeUnits = default;
            GiveBarcodeIdentifier = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: GiveSubIDCounter = new NM(value); break;
                case 2: DispenseSubIDCounter = new NM(value); break;
                case 3: QuantityTiming = new ST(value); break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    GiveCode = ce4;
                    break;
                case 5: GiveAmountMinimum = new NM(value); break;
                case 6: GiveAmountMaximum = new NM(value); break;
                case 7:
                    var ce7 = new CE();
                    ce7.Parse(value.AsSpan(), delimiters);
                    GiveUnits = ce7;
                    break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    GiveDosageForm = ce8;
                    break;
                case 9: AdministrationNotes = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 10: SubstitutionStatus = new ID(value); break;
                case 11:
                    var pl11 = new PL();
                    pl11.Parse(value.AsSpan(), delimiters);
                    DispenseToLocation = pl11;
                    break;
                case 12: NeedsHumanReview = new ID(value); break;
                case 13: PharmacyTreatmentSupplierSpecialAdministrationInstructions = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 14: GivePerTimeUnit = new ST(value); break;
                case 15: GiveRateAmount = new ST(value); break;
                case 16:
                    var ce16 = new CE();
                    ce16.Parse(value.AsSpan(), delimiters);
                    GiveRateUnits = ce16;
                    break;
                case 17: GiveStrength = new NM(value); break;
                case 18:
                    var ce18 = new CE();
                    ce18.Parse(value.AsSpan(), delimiters);
                    GiveStrengthUnits = ce18;
                    break;
                case 19: SubstanceLotNumber = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 20: SubstanceExpirationDate = SegmentFieldHelper.ParseRepeatingField<DTM>(value, delimiters); break;
                case 21: SubstanceManufacturerName = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 22: Indication = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 23: GiveDrugStrengthVolume = new NM(value); break;
                case 24:
                    var cwe24 = new CWE();
                    cwe24.Parse(value.AsSpan(), delimiters);
                    GiveDrugStrengthVolumeUnits = cwe24;
                    break;
                case 25:
                    var cwe25 = new CWE();
                    cwe25.Parse(value.AsSpan(), delimiters);
                    GiveBarcodeIdentifier = cwe25;
                    break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                GiveSubIDCounter.ToHL7String(delimiters),
                DispenseSubIDCounter.ToHL7String(delimiters),
                QuantityTiming.ToHL7String(delimiters),
                GiveCode.ToHL7String(delimiters),
                GiveAmountMinimum.ToHL7String(delimiters),
                GiveAmountMaximum.ToHL7String(delimiters),
                GiveUnits.ToHL7String(delimiters),
                GiveDosageForm.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AdministrationNotes, delimiters),
                SubstitutionStatus.ToHL7String(delimiters),
                DispenseToLocation.ToHL7String(delimiters),
                NeedsHumanReview.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PharmacyTreatmentSupplierSpecialAdministrationInstructions, delimiters),
                GivePerTimeUnit.ToHL7String(delimiters),
                GiveRateAmount.ToHL7String(delimiters),
                GiveRateUnits.ToHL7String(delimiters),
                GiveStrength.ToHL7String(delimiters),
                GiveStrengthUnits.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceLotNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceExpirationDate, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceManufacturerName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                GiveDrugStrengthVolume.ToHL7String(delimiters),
                GiveDrugStrengthVolumeUnits.ToHL7String(delimiters),
                GiveBarcodeIdentifier.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => GiveSubIDCounter.Value,
                2 => DispenseSubIDCounter.Value,
                3 => QuantityTiming.Value,
                4 => GiveCode.ToHL7String(delimiters),
                5 => GiveAmountMinimum.Value,
                6 => GiveAmountMaximum.Value,
                7 => GiveUnits.ToHL7String(delimiters),
                8 => GiveDosageForm.ToHL7String(delimiters),
                9 => SegmentFieldHelper.JoinRepeatingField(AdministrationNotes, delimiters),
                10 => SubstitutionStatus.Value,
                11 => DispenseToLocation.ToHL7String(delimiters),
                12 => NeedsHumanReview.Value,
                13 => SegmentFieldHelper.JoinRepeatingField(PharmacyTreatmentSupplierSpecialAdministrationInstructions, delimiters),
                14 => GivePerTimeUnit.Value,
                15 => GiveRateAmount.Value,
                16 => GiveRateUnits.ToHL7String(delimiters),
                17 => GiveStrength.Value,
                18 => GiveStrengthUnits.ToHL7String(delimiters),
                19 => SegmentFieldHelper.JoinRepeatingField(SubstanceLotNumber, delimiters),
                20 => SegmentFieldHelper.JoinRepeatingField(SubstanceExpirationDate, delimiters),
                21 => SegmentFieldHelper.JoinRepeatingField(SubstanceManufacturerName, delimiters),
                22 => SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                23 => GiveDrugStrengthVolume.Value,
                24 => GiveDrugStrengthVolumeUnits.ToHL7String(delimiters),
                25 => GiveBarcodeIdentifier.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
