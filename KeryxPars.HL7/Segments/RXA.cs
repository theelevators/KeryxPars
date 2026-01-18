using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy/Treatment Administration
    /// </summary>
    public class RXA : ISegment
    {
        public string SegmentId => nameof(RXA);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXA.1 - Give Sub-ID Counter
        /// </summary>
        public NM GiveSubIDCounter { get; set; }

        /// <summary>
        /// RXA.2 - Administration Sub-ID Counter
        /// </summary>
        public NM AdministrationSubIDCounter { get; set; }

        /// <summary>
        /// RXA.3 - Date/Time Start of Administration
        /// </summary>
        public DTM DateTimeStartOfAdministration { get; set; }

        /// <summary>
        /// RXA.4 - Date/Time End of Administration
        /// </summary>
        public DTM DateTimeEndOfAdministration { get; set; }

        /// <summary>
        /// RXA.5 - Administered Code
        /// </summary>
        public CE AdministeredCode { get; set; }

        /// <summary>
        /// RXA.6 - Administered Amount
        /// </summary>
        public NM AdministeredAmount { get; set; }

        /// <summary>
        /// RXA.7 - Administered Units
        /// </summary>
        public CE AdministeredUnits { get; set; }

        /// <summary>
        /// RXA.8 - Administered Dosage Form
        /// </summary>
        public CE AdministeredDosageForm { get; set; }

        /// <summary>
        /// RXA.9 - Administration Notes (repeating)
        /// </summary>
        public CE[] AdministrationNotes { get; set; }

        /// <summary>
        /// RXA.10 - Administering Provider (repeating)
        /// </summary>
        public XCN[] AdministeringProvider { get; set; }

        /// <summary>
        /// RXA.11 - Administered-at Location
        /// </summary>
        public PL AdministeredAtLocation { get; set; }

        /// <summary>
        /// RXA.12 - Administered Per (Time Unit)
        /// </summary>
        public ST AdministeredPerTimeUnit { get; set; }

        /// <summary>
        /// RXA.13 - Administered Strength
        /// </summary>
        public NM AdministeredStrength { get; set; }

        /// <summary>
        /// RXA.14 - Administered Strength Units
        /// </summary>
        public CE AdministeredStrengthUnits { get; set; }

        /// <summary>
        /// RXA.15 - Substance Lot Number (repeating)
        /// </summary>
        public ST[] SubstanceLotNumber { get; set; }

        /// <summary>
        /// RXA.16 - Substance Expiration Date (repeating)
        /// </summary>
        public DTM[] SubstanceExpirationDate { get; set; }

        /// <summary>
        /// RXA.17 - Substance Manufacturer Name (repeating)
        /// </summary>
        public CE[] SubstanceManufacturerName { get; set; }

        /// <summary>
        /// RXA.18 - Substance/Treatment Refusal Reason (repeating)
        /// </summary>
        public CE[] SubstanceTreatmentRefusalReason { get; set; }

        /// <summary>
        /// RXA.19 - Indication (repeating)
        /// </summary>
        public CE[] Indication { get; set; }

        /// <summary>
        /// RXA.20 - Completion Status
        /// </summary>
        public ID CompletionStatus { get; set; }

        /// <summary>
        /// RXA.21 - Action Code - RXA
        /// </summary>
        public ID ActionCodeRXA { get; set; }

        /// <summary>
        /// RXA.22 - System Entry Date/Time
        /// </summary>
        public DTM SystemEntryDateTime { get; set; }

        /// <summary>
        /// RXA.23 - Administered Drug Strength Volume
        /// </summary>
        public NM AdministeredDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXA.24 - Administered Drug Strength Volume Units
        /// </summary>
        public CWE AdministeredDrugStrengthVolumeUnits { get; set; }

        /// <summary>
        /// RXA.25 - Administered Barcode Identifier
        /// </summary>
        public CWE AdministeredBarcodeIdentifier { get; set; }

        public RXA()
        {
            SegmentType = SegmentType.MedOrder;
            GiveSubIDCounter = default;
            AdministrationSubIDCounter = default;
            DateTimeStartOfAdministration = default;
            DateTimeEndOfAdministration = default;
            AdministeredCode = default;
            AdministeredAmount = default;
            AdministeredUnits = default;
            AdministeredDosageForm = default;
            AdministrationNotes = [];
            AdministeringProvider = [];
            AdministeredAtLocation = default;
            AdministeredPerTimeUnit = default;
            AdministeredStrength = default;
            AdministeredStrengthUnits = default;
            SubstanceLotNumber = [];
            SubstanceExpirationDate = [];
            SubstanceManufacturerName = [];
            SubstanceTreatmentRefusalReason = [];
            Indication = [];
            CompletionStatus = default;
            ActionCodeRXA = default;
            SystemEntryDateTime = default;
            AdministeredDrugStrengthVolume = default;
            AdministeredDrugStrengthVolumeUnits = default;
            AdministeredBarcodeIdentifier = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: GiveSubIDCounter = new NM(value); break;
                case 2: AdministrationSubIDCounter = new NM(value); break;
                case 3: DateTimeStartOfAdministration = new DTM(value); break;
                case 4: DateTimeEndOfAdministration = new DTM(value); break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    AdministeredCode = ce5;
                    break;
                case 6: AdministeredAmount = new NM(value); break;
                case 7:
                    var ce7 = new CE();
                    ce7.Parse(value.AsSpan(), delimiters);
                    AdministeredUnits = ce7;
                    break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    AdministeredDosageForm = ce8;
                    break;
                case 9: AdministrationNotes = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 10: AdministeringProvider = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 11:
                    var pl11 = new PL();
                    pl11.Parse(value.AsSpan(), delimiters);
                    AdministeredAtLocation = pl11;
                    break;
                case 12: AdministeredPerTimeUnit = new ST(value); break;
                case 13: AdministeredStrength = new NM(value); break;
                case 14:
                    var ce14 = new CE();
                    ce14.Parse(value.AsSpan(), delimiters);
                    AdministeredStrengthUnits = ce14;
                    break;
                case 15: SubstanceLotNumber = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 16: SubstanceExpirationDate = SegmentFieldHelper.ParseRepeatingField<DTM>(value, delimiters); break;
                case 17: SubstanceManufacturerName = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 18: SubstanceTreatmentRefusalReason = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 19: Indication = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 20: CompletionStatus = new ID(value); break;
                case 21: ActionCodeRXA = new ID(value); break;
                case 22: SystemEntryDateTime = new DTM(value); break;
                case 23: AdministeredDrugStrengthVolume = new NM(value); break;
                case 24:
                    var cwe24 = new CWE();
                    cwe24.Parse(value.AsSpan(), delimiters);
                    AdministeredDrugStrengthVolumeUnits = cwe24;
                    break;
                case 25:
                    var cwe25 = new CWE();
                    cwe25.Parse(value.AsSpan(), delimiters);
                    AdministeredBarcodeIdentifier = cwe25;
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
                AdministrationSubIDCounter.ToHL7String(delimiters),
                DateTimeStartOfAdministration.ToHL7String(delimiters),
                DateTimeEndOfAdministration.ToHL7String(delimiters),
                AdministeredCode.ToHL7String(delimiters),
                AdministeredAmount.ToHL7String(delimiters),
                AdministeredUnits.ToHL7String(delimiters),
                AdministeredDosageForm.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AdministrationNotes, delimiters),
                SegmentFieldHelper.JoinRepeatingField(AdministeringProvider, delimiters),
                AdministeredAtLocation.ToHL7String(delimiters),
                AdministeredPerTimeUnit.ToHL7String(delimiters),
                AdministeredStrength.ToHL7String(delimiters),
                AdministeredStrengthUnits.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceLotNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceExpirationDate, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceManufacturerName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SubstanceTreatmentRefusalReason, delimiters),
                SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                CompletionStatus.ToHL7String(delimiters),
                ActionCodeRXA.ToHL7String(delimiters),
                SystemEntryDateTime.ToHL7String(delimiters),
                AdministeredDrugStrengthVolume.ToHL7String(delimiters),
                AdministeredDrugStrengthVolumeUnits.ToHL7String(delimiters),
                AdministeredBarcodeIdentifier.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => GiveSubIDCounter.Value,
                2 => AdministrationSubIDCounter.Value,
                3 => DateTimeStartOfAdministration.Value,
                4 => DateTimeEndOfAdministration.Value,
                5 => AdministeredCode.ToHL7String(delimiters),
                6 => AdministeredAmount.Value,
                7 => AdministeredUnits.ToHL7String(delimiters),
                8 => AdministeredDosageForm.ToHL7String(delimiters),
                9 => SegmentFieldHelper.JoinRepeatingField(AdministrationNotes, delimiters),
                10 => SegmentFieldHelper.JoinRepeatingField(AdministeringProvider, delimiters),
                11 => AdministeredAtLocation.ToHL7String(delimiters),
                12 => AdministeredPerTimeUnit.Value,
                13 => AdministeredStrength.Value,
                14 => AdministeredStrengthUnits.ToHL7String(delimiters),
                15 => SegmentFieldHelper.JoinRepeatingField(SubstanceLotNumber, delimiters),
                16 => SegmentFieldHelper.JoinRepeatingField(SubstanceExpirationDate, delimiters),
                17 => SegmentFieldHelper.JoinRepeatingField(SubstanceManufacturerName, delimiters),
                18 => SegmentFieldHelper.JoinRepeatingField(SubstanceTreatmentRefusalReason, delimiters),
                19 => SegmentFieldHelper.JoinRepeatingField(Indication, delimiters),
                20 => CompletionStatus.Value,
                21 => ActionCodeRXA.Value,
                22 => SystemEntryDateTime.Value,
                23 => AdministeredDrugStrengthVolume.Value,
                24 => AdministeredDrugStrengthVolumeUnits.ToHL7String(delimiters),
                25 => AdministeredBarcodeIdentifier.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
