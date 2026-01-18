using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy Encoded Order
    /// </summary>
    public class RXE : ISegment
    {
        public string SegmentId => nameof(RXE);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXE.1
        /// </summary>
        public string QuantityTiming { get; set; }

        /// <summary>
        /// RXE.2
        /// </summary>
        public string GiveCode { get; set; }

        /// <summary>
        /// RXE.3
        /// </summary>
        public string GiveAmountMinimum { get; set; }

        /// <summary>
        /// RXE.4
        /// </summary>
        public string GiveAmountMaximum { get; set; }

        /// <summary>
        /// RXE.5
        /// </summary>
        public string GiveUnits { get; set; }

        /// <summary>
        /// RXE.6
        /// </summary>
        public string GiveDosageForm { get; set; }

        /// <summary>
        /// RXE.7
        /// </summary>
        public string ProvidersAdministrationInstructions { get; set; }

        /// <summary>
        /// RXE.8
        /// </summary>
        public string DeliverToLocation { get; set; }

        /// <summary>
        /// RXE.9
        /// </summary>
        public string SubstitutionStatus { get; set; }

        /// <summary>
        /// RXE.10
        /// </summary>
        public string DispenseAmount { get; set; }

        /// <summary>
        /// RXE.11
        /// </summary>
        public string DispenseUnits { get; set; }

        /// <summary>
        /// RXE.12
        /// </summary>
        public string NumberOfRefills { get; set; }

        /// <summary>
        /// RXE.13
        /// </summary>
        public string OrderingProvidersDeaNumber { get; set; }

        /// <summary>
        /// RXE.14
        /// </summary>
        public string PharmacistTreatmentSuppliersVerifierID { get; set; }

        /// <summary>
        /// RXE.15
        /// </summary>
        public string PrescriptionNumber { get; set; }

        /// <summary>
        /// RXE.16
        /// </summary>
        public string NumberOfRefillsRemaining { get; set; }

        /// <summary>
        /// RXE.17
        /// </summary>
        public string NumberOfRefillsDosesDispensed { get; set; }

        /// <summary>
        /// RXE.18
        /// </summary>
        public string DateOfMostRecentRefillOrDoseDispensed { get; set; }

        /// <summary>
        /// RXE.19
        /// </summary>
        public string TotalDailyDose { get; set; }

        /// <summary>
        /// RXE.20
        /// </summary>
        public string NeedsHumanReview { get; set; }

        /// <summary>
        /// RXE.21
        /// </summary>
        public string PharmacyTreatmentSuppliersSpecialDispensingInstructions { get; set; }

        /// <summary>
        /// RXE.22
        /// </summary>
        public string GivePerTimeUnit { get; set; }

        /// <summary>
        /// RXE.23
        /// </summary>
        public string GiveRateAmount { get; set; }

        /// <summary>
        /// RXE.24
        /// </summary>
        public string GiveRateUnits { get; set; }

        /// <summary>
        /// RXE.25
        /// </summary>
        public string GiveStrength { get; set; }

        /// <summary>
        /// RXE.26
        /// </summary>
        public string GiveStrengthUnits { get; set; }

        /// <summary>
        /// RXE.27
        /// </summary>
        public string GiveIndication { get; set; }

        /// <summary>
        /// RXE.28
        /// </summary>
        public string DispensePackageSize { get; set; }

        /// <summary>
        /// RXE.29
        /// </summary>
        public string DispensePackageSizeUnit { get; set; }

        /// <summary>
        /// RXE.30
        /// </summary>
        public string DispensePackageMethod { get; set; }

        /// <summary>
        /// RXE.31
        /// </summary>
        public string SupplementaryCode { get; set; }

        /// <summary>
        /// RXE.32
        /// </summary>
        public string OriginalOrderDateTime { get; set; }

        /// <summary>
        /// RXE.33
        /// </summary>
        public string GiveDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXE.34
        /// </summary>
        public string GiveDrugStrengthVolumeUnits { get; set; }

        /// <summary>
        /// RXE.35
        /// </summary>
        public string ControlledSubstanceSchedule { get; set; }

        /// <summary>
        /// RXE.36
        /// </summary>
        public string FormularyStatus { get; set; }

        /// <summary>
        /// RXE.37
        /// </summary>
        public string PharmaceuticalSubstanceAlternative { get; set; }

        /// <summary>
        /// RXE.38
        /// </summary>
        public string PharmacyOfMostRecentFill { get; set; }

        /// <summary>
        /// RXE.39
        /// </summary>
        public string InitialDispenseAmount { get; set; }

        /// <summary>
        /// RXE.40
        /// </summary>
        public string DispensingPharmacy { get; set; }

        /// <summary>
        /// RXE.41
        /// </summary>
        public string DispensingPharmacyAddress { get; set; }

        /// <summary>
        /// RXE.42
        /// </summary>
        public string DeliverToPatientLocation { get; set; }

        /// <summary>
        /// RXE.43
        /// </summary>
        public string DeliverToAddress { get; set; }

        /// <summary>
        /// RXE.44
        /// </summary>
        public string PharmacyOrderType { get; set; }

        /// <summary>
        /// RXE.45
        /// </summary>
        public string PharmacyPhoneNumber { get; set; }

        // Constructors
        public RXE()
        {
            SegmentType = SegmentType.MedOrder;
            QuantityTiming = string.Empty;
            GiveCode = string.Empty;
            GiveAmountMinimum = string.Empty;
            GiveAmountMaximum = string.Empty;
            GiveUnits = string.Empty;
            GiveDosageForm = string.Empty;
            ProvidersAdministrationInstructions = string.Empty;
            DeliverToLocation = string.Empty;
            SubstitutionStatus = string.Empty;
            DispenseAmount = string.Empty;
            DispenseUnits = string.Empty;
            NumberOfRefills = string.Empty;
            OrderingProvidersDeaNumber = string.Empty;
            PharmacistTreatmentSuppliersVerifierID = string.Empty;
            PrescriptionNumber = string.Empty;
            NumberOfRefillsRemaining = string.Empty;
            NumberOfRefillsDosesDispensed = string.Empty;
            DateOfMostRecentRefillOrDoseDispensed = string.Empty;
            TotalDailyDose = string.Empty;
            NeedsHumanReview = string.Empty;
            PharmacyTreatmentSuppliersSpecialDispensingInstructions = string.Empty;
            GivePerTimeUnit = string.Empty;
            GiveRateAmount = string.Empty;
            GiveRateUnits = string.Empty;
            GiveStrength = string.Empty;
            GiveStrengthUnits = string.Empty;
            GiveIndication = string.Empty;
            DispensePackageSize = string.Empty;
            DispensePackageSizeUnit = string.Empty;
            DispensePackageMethod = string.Empty;
            SupplementaryCode = string.Empty;
            OriginalOrderDateTime = string.Empty;
            GiveDrugStrengthVolume = string.Empty;
            GiveDrugStrengthVolumeUnits = string.Empty;
            ControlledSubstanceSchedule = string.Empty;
            FormularyStatus = string.Empty;
            PharmaceuticalSubstanceAlternative = string.Empty;
            PharmacyOfMostRecentFill = string.Empty;
            InitialDispenseAmount = string.Empty;
            DispensingPharmacy = string.Empty;
            DispensingPharmacyAddress = string.Empty;
            DeliverToPatientLocation = string.Empty;
            DeliverToAddress = string.Empty;
            PharmacyOrderType = string.Empty;
            PharmacyPhoneNumber = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: QuantityTiming = value; break;
                case 2: GiveCode = value; break;
                case 3: GiveAmountMinimum = value; break;
                case 4: GiveAmountMaximum = value; break;
                case 5: GiveUnits = value; break;
                case 6: GiveDosageForm = value; break;
                case 7: ProvidersAdministrationInstructions = value; break;
                case 8: DeliverToLocation = value; break;
                case 9: SubstitutionStatus = value; break;
                case 10: DispenseAmount = value; break;
                case 11: DispenseUnits = value; break;
                case 12: NumberOfRefills = value; break;
                case 13: OrderingProvidersDeaNumber = value; break;
                case 14: PharmacistTreatmentSuppliersVerifierID = value; break;
                case 15: PrescriptionNumber = value; break;
                case 16: NumberOfRefillsRemaining = value; break;
                case 17: NumberOfRefillsDosesDispensed = value; break;
                case 18: DateOfMostRecentRefillOrDoseDispensed = value; break;
                case 19: TotalDailyDose = value; break;
                case 20: NeedsHumanReview = value; break;
                case 21: PharmacyTreatmentSuppliersSpecialDispensingInstructions = value; break;
                case 22: GivePerTimeUnit = value; break;
                case 23: GiveRateAmount = value; break;
                case 24: GiveRateUnits = value; break;
                case 25: GiveStrength = value; break;
                case 26: GiveStrengthUnits = value; break;
                case 27: GiveIndication = value; break;
                case 28: DispensePackageSize = value; break;
                case 29: DispensePackageSizeUnit = value; break;
                case 30: DispensePackageMethod = value; break;
                case 31: SupplementaryCode = value; break;
                case 32: OriginalOrderDateTime = value; break;
                case 33: GiveDrugStrengthVolume = value; break;
                case 34: GiveDrugStrengthVolumeUnits = value; break;
                case 35: ControlledSubstanceSchedule = value; break;
                case 36: FormularyStatus = value; break;
                case 37: PharmaceuticalSubstanceAlternative = value; break;
                case 38: PharmacyOfMostRecentFill = value; break;
                case 39: InitialDispenseAmount = value; break;
                case 40: DispensingPharmacy = value; break;
                case 41: DispensingPharmacyAddress = value; break;
                case 42: DeliverToPatientLocation = value; break;
                case 43: DeliverToAddress = value; break;
                case 44: PharmacyOrderType = value; break;
                case 45: PharmacyPhoneNumber = value; break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                QuantityTiming,
                GiveCode,
                GiveAmountMinimum,
                GiveAmountMaximum,
                GiveUnits,
                GiveDosageForm,
                ProvidersAdministrationInstructions,
                DeliverToLocation,
                SubstitutionStatus,
                DispenseAmount,
                DispenseUnits,
                NumberOfRefills,
                OrderingProvidersDeaNumber,
                PharmacistTreatmentSuppliersVerifierID,
                PrescriptionNumber,
                NumberOfRefillsRemaining,
                NumberOfRefillsDosesDispensed,
                DateOfMostRecentRefillOrDoseDispensed,
                TotalDailyDose,
                NeedsHumanReview,
                PharmacyTreatmentSuppliersSpecialDispensingInstructions,
                GivePerTimeUnit,
                GiveRateAmount,
                GiveRateUnits,
                GiveStrength,
                GiveStrengthUnits,
                GiveIndication,
                DispensePackageSize,
                DispensePackageSizeUnit,
                DispensePackageMethod,
                SupplementaryCode,
                OriginalOrderDateTime,
                GiveDrugStrengthVolume,
                GiveDrugStrengthVolumeUnits,
                ControlledSubstanceSchedule,
                FormularyStatus,
                PharmaceuticalSubstanceAlternative,
                PharmacyOfMostRecentFill,
                InitialDispenseAmount,
                DispensingPharmacy,
                DispensingPharmacyAddress,
                DeliverToPatientLocation,
                DeliverToAddress,
                PharmacyOrderType,
                PharmacyPhoneNumber
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => QuantityTiming,
                2 => GiveCode,
                3 => GiveAmountMinimum,
                4 => GiveAmountMaximum,
                5 => GiveUnits,
                6 => GiveDosageForm,
                7 => ProvidersAdministrationInstructions,
                8 => DeliverToLocation,
                9 => SubstitutionStatus,
                10 => DispenseAmount,
                11 => DispenseUnits,
                12 => NumberOfRefills,
                13 => OrderingProvidersDeaNumber,
                14 => PharmacistTreatmentSuppliersVerifierID,
                15 => PrescriptionNumber,
                16 => NumberOfRefillsRemaining,
                17 => NumberOfRefillsDosesDispensed,
                18 => DateOfMostRecentRefillOrDoseDispensed,
                19 => TotalDailyDose,
                20 => NeedsHumanReview,
                21 => PharmacyTreatmentSuppliersSpecialDispensingInstructions,
                22 => GivePerTimeUnit,
                23 => GiveRateAmount,
                24 => GiveRateUnits,
                25 => GiveStrength,
                26 => GiveStrengthUnits,
                27 => GiveIndication,
                28 => DispensePackageSize,
                29 => DispensePackageSizeUnit,
                30 => DispensePackageMethod,
                31 => SupplementaryCode,
                32 => OriginalOrderDateTime,
                33 => GiveDrugStrengthVolume,
                34 => GiveDrugStrengthVolumeUnits,
                35 => ControlledSubstanceSchedule,
                36 => FormularyStatus,
                37 => PharmaceuticalSubstanceAlternative,
                38 => PharmacyOfMostRecentFill,
                39 => InitialDispenseAmount,
                40 => DispensingPharmacy,
                41 => DispensingPharmacyAddress,
                42 => DeliverToPatientLocation,
                43 => DeliverToAddress,
                44 => PharmacyOrderType,
                45 => PharmacyPhoneNumber,
                _ => null
            };
        }
    }
}
