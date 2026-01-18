using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy Prescription Order
    /// </summary>
    public class RXO : ISegment
    {
        public string SegmentId => nameof(RXO);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXO.1
        /// </summary>
        public string RequestedGiveCode { get; set; }

        /// <summary>
        /// RXO.2
        /// </summary>
        public string RequestedGiveAmountMinimum { get; set; }

        /// <summary>
        /// RXO.3
        /// </summary>
        public string RequestedGiveAmountMaximum { get; set; }

        /// <summary>
        /// RXO.4
        /// </summary>
        public string RequestedGiveUnits { get; set; }

        /// <summary>
        /// RXO.5
        /// </summary>
        public string RequestedDosageForm { get; set; }

        /// <summary>
        /// RXO.6
        /// </summary>
        public string ProvidersPharmacyTreatmentInstructions { get; set; }

        /// <summary>
        /// RXO.7
        /// </summary>
        public string ProvidersAdministrationInstructions { get; set; }

        /// <summary>
        /// RXO.8
        /// </summary>
        public string DeliverToLocation { get; set; }

        /// <summary>
        /// RXO.9
        /// </summary>
        public string AllowSubstitutions { get; set; }

        /// <summary>
        /// RXO.10
        /// </summary>
        public string RequestedDispenseCode { get; set; }

        /// <summary>
        /// RXO.11
        /// </summary>
        public string RequestedDispenseAmount { get; set; }

        /// <summary>
        /// RXO.12
        /// </summary>
        public string RequestedDispenseUnits { get; set; }

        /// <summary>
        /// RXO.13
        /// </summary>
        public string NumberOfRefills { get; set; }

        /// <summary>
        /// RXO.14
        /// </summary>
        public string OrderingProvidersDeaNumber { get; set; }

        /// <summary>
        /// RXO.15
        /// </summary>
        public string PharmacistTreatmentSuppliersVerifierID { get; set; }

        /// <summary>
        /// RXO.16
        /// </summary>
        public string NeedsHumanReview { get; set; }

        /// <summary>
        /// RXO.17
        /// </summary>
        public string RequestedGivePerTimeUnit { get; set; }

        /// <summary>
        /// RXO.18
        /// </summary>
        public string RequestedGiveStrength { get; set; }

        /// <summary>
        /// RXO.19
        /// </summary>
        public string RequestedGiveStrengthUnits { get; set; }

        /// <summary>
        /// RXO.20
        /// </summary>
        public string Indication { get; set; }

        /// <summary>
        /// RXO.21
        /// </summary>
        public string RequestedGiveRateAmount { get; set; }

        /// <summary>
        /// RXO.22
        /// </summary>
        public string RequestedGiveRateUnits { get; set; }

        /// <summary>
        /// RXO.23
        /// </summary>
        public string TotalDailyDose { get; set; }

        /// <summary>
        /// RXO.24
        /// </summary>
        public string SupplementaryCode { get; set; }

        /// <summary>
        /// RXO.25
        /// </summary>
        public string RequestedDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXO.26
        /// </summary>
        public string RequestedDrugStrengthVolumeUnits { get; set; }

        /// <summary>
        /// RXO.27
        /// </summary>
        public string PharmacyOrderType { get; set; }

        /// <summary>
        /// RXO.28
        /// </summary>
        public string DispensingInterval { get; set; }

        /// <summary>
        /// RXO.29
        /// </summary>
        public string MedicationInstanceIdentifier { get; set; }

        /// <summary>
        /// RXO.30
        /// </summary>
        public string SegmentInstanceIdentifier { get; set; }

        /// <summary>
        /// RXO.31
        /// </summary>
        public string MoodCode { get; set; }

        /// <summary>
        /// RXO.32
        /// </summary>
        public string DispensingPharmacy { get; set; }

        /// <summary>
        /// RXO.33
        /// </summary>
        public string DispensingPharmacyAddress { get; set; }

        /// <summary>
        /// RXO.34
        /// </summary>
        public string DeliverToPatientLocation { get; set; }

        /// <summary>
        /// RXO.35
        /// </summary>
        public string DeliverToAddress { get; set; }

        /// <summary>
        /// RXO.36
        /// </summary>
        public string PharmacyPhoneNumber { get; set; }

        // Constructors
        public RXO()
        {
            SegmentType = SegmentType.MedOrder;
            RequestedGiveCode = string.Empty;
            RequestedGiveAmountMinimum = string.Empty;
            RequestedGiveAmountMaximum = string.Empty;
            RequestedGiveUnits = string.Empty;
            RequestedDosageForm = string.Empty;
            ProvidersPharmacyTreatmentInstructions = string.Empty;
            ProvidersAdministrationInstructions = string.Empty;
            DeliverToLocation = string.Empty;
            AllowSubstitutions = string.Empty;
            RequestedDispenseCode = string.Empty;
            RequestedDispenseAmount = string.Empty;
            RequestedDispenseUnits = string.Empty;
            NumberOfRefills = string.Empty;
            OrderingProvidersDeaNumber = string.Empty;
            PharmacistTreatmentSuppliersVerifierID = string.Empty;
            NeedsHumanReview = string.Empty;
            RequestedGivePerTimeUnit = string.Empty;
            RequestedGiveStrength = string.Empty;
            RequestedGiveStrengthUnits = string.Empty;
            Indication = string.Empty;
            RequestedGiveRateAmount = string.Empty;
            RequestedGiveRateUnits = string.Empty;
            TotalDailyDose = string.Empty;
            SupplementaryCode = string.Empty;
            RequestedDrugStrengthVolume = string.Empty;
            RequestedDrugStrengthVolumeUnits = string.Empty;
            PharmacyOrderType = string.Empty;
            DispensingInterval = string.Empty;
            MedicationInstanceIdentifier = string.Empty;
            SegmentInstanceIdentifier = string.Empty;
            MoodCode = string.Empty;
            DispensingPharmacy = string.Empty;
            DispensingPharmacyAddress = string.Empty;
            DeliverToPatientLocation = string.Empty;
            DeliverToAddress = string.Empty;
            PharmacyPhoneNumber = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: RequestedGiveCode = value; break;
                case 2: RequestedGiveAmountMinimum = value; break;
                case 3: RequestedGiveAmountMaximum = value; break;
                case 4: RequestedGiveUnits = value; break;
                case 5: RequestedDosageForm = value; break;
                case 6: ProvidersPharmacyTreatmentInstructions = value; break;
                case 7: ProvidersAdministrationInstructions = value; break;
                case 8: DeliverToLocation = value; break;
                case 9: AllowSubstitutions = value; break;
                case 10: RequestedDispenseCode = value; break;
                case 11: RequestedDispenseAmount = value; break;
                case 12: RequestedDispenseUnits = value; break;
                case 13: NumberOfRefills = value; break;
                case 14: OrderingProvidersDeaNumber = value; break;
                case 15: PharmacistTreatmentSuppliersVerifierID = value; break;
                case 16: NeedsHumanReview = value; break;
                case 17: RequestedGivePerTimeUnit = value; break;
                case 18: RequestedGiveStrength = value; break;
                case 19: RequestedGiveStrengthUnits = value; break;
                case 20: Indication = value; break;
                case 21: RequestedGiveRateAmount = value; break;
                case 22: RequestedGiveRateUnits = value; break;
                case 23: TotalDailyDose = value; break;
                case 24: SupplementaryCode = value; break;
                case 25: RequestedDrugStrengthVolume = value; break;
                case 26: RequestedDrugStrengthVolumeUnits = value; break;
                case 27: PharmacyOrderType = value; break;
                case 28: DispensingInterval = value; break;
                case 29: MedicationInstanceIdentifier = value; break;
                case 30: SegmentInstanceIdentifier = value; break;
                case 31: MoodCode = value; break;
                case 32: DispensingPharmacy = value; break;
                case 33: DispensingPharmacyAddress = value; break;
                case 34: DeliverToPatientLocation = value; break;
                case 35: DeliverToAddress = value; break;
                case 36: PharmacyPhoneNumber = value; break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                RequestedGiveCode,
                RequestedGiveAmountMinimum,
                RequestedGiveAmountMaximum,
                RequestedGiveUnits,
                RequestedDosageForm,
                ProvidersPharmacyTreatmentInstructions,
                ProvidersAdministrationInstructions,
                DeliverToLocation,
                AllowSubstitutions,
                RequestedDispenseCode,
                RequestedDispenseAmount,
                RequestedDispenseUnits,
                NumberOfRefills,
                OrderingProvidersDeaNumber,
                PharmacistTreatmentSuppliersVerifierID,
                NeedsHumanReview,
                RequestedGivePerTimeUnit,
                RequestedGiveStrength,
                RequestedGiveStrengthUnits,
                Indication,
                RequestedGiveRateAmount,
                RequestedGiveRateUnits,
                TotalDailyDose,
                SupplementaryCode,
                RequestedDrugStrengthVolume,
                RequestedDrugStrengthVolumeUnits,
                PharmacyOrderType,
                DispensingInterval,
                MedicationInstanceIdentifier,
                SegmentInstanceIdentifier,
                MoodCode,
                DispensingPharmacy,
                DispensingPharmacyAddress,
                DeliverToPatientLocation,
                DeliverToAddress,
                PharmacyPhoneNumber
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => RequestedGiveCode,
                2 => RequestedGiveAmountMinimum,
                3 => RequestedGiveAmountMaximum,
                4 => RequestedGiveUnits,
                5 => RequestedDosageForm,
                6 => ProvidersPharmacyTreatmentInstructions,
                7 => ProvidersAdministrationInstructions,
                8 => DeliverToLocation,
                9 => AllowSubstitutions,
                10 => RequestedDispenseCode,
                11 => RequestedDispenseAmount,
                12 => RequestedDispenseUnits,
                13 => NumberOfRefills,
                14 => OrderingProvidersDeaNumber,
                15 => PharmacistTreatmentSuppliersVerifierID,
                16 => NeedsHumanReview,
                17 => RequestedGivePerTimeUnit,
                18 => RequestedGiveStrength,
                19 => RequestedGiveStrengthUnits,
                20 => Indication,
                21 => RequestedGiveRateAmount,
                22 => RequestedGiveRateUnits,
                23 => TotalDailyDose,
                24 => SupplementaryCode,
                25 => RequestedDrugStrengthVolume,
                26 => RequestedDrugStrengthVolumeUnits,
                27 => PharmacyOrderType,
                28 => DispensingInterval,
                29 => MedicationInstanceIdentifier,
                30 => SegmentInstanceIdentifier,
                31 => MoodCode,
                32 => DispensingPharmacy,
                33 => DispensingPharmacyAddress,
                34 => DeliverToPatientLocation,
                35 => DeliverToAddress,
                36 => PharmacyPhoneNumber,
                _ => null
            };
        }
    }
}
