using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Common Order
    /// </summary>
    public class ORC : ISegment
    {
        public string SegmentId => nameof(ORC);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties
        /// <summary>
        /// ORC.1
        /// </summary>
        public string OrderControl { get; set; } // values in table 0119

        /// <summary>
        /// ORC.2
        /// </summary>
        public string PlacerOrderNumber { get; set; }

        /// <summary>
        /// ORC.3
        /// </summary>
        public string FillerOrderNumber { get; set; }

        /// <summary>
        /// ORC.4
        /// </summary>
        public string PlacerGroupNumber { get; set; }

        /// <summary>
        /// ORC.5
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// ORC.6
        /// </summary>
        public string ResponseFlag { get; set; }

        /// <summary>
        /// ORC.7
        /// </summary>
        public string QuantityTiming { get; set; }

        /// <summary>
        /// ORC.8
        /// </summary>
        public string ParentOrder { get; set; }

        /// <summary>
        /// ORC.9
        /// </summary>
        public string DateTimeOfTransaction { get; set; }

        /// <summary>
        /// ORC.10
        /// </summary>
        public string EnteredBy { get; set; }

        /// <summary>
        /// ORC.11
        /// </summary>
        public string VerifiedBy { get; set; }

        /// <summary>
        /// ORC.12
        /// </summary>
        public string OrderingProvider { get; set; }

        /// <summary>
        /// ORC.13
        /// </summary>
        public string EnterersLocation { get; set; }

        /// <summary>
        /// ORC.14
        /// </summary>
        public string CallBackPhoneNumber { get; set; }

        /// <summary>
        /// ORC.15
        /// </summary>
        public string OrderEffectiveDateTime { get; set; }

        /// <summary>
        /// ORC.16
        /// </summary>
        public string OrderControlCodeReason { get; set; }

        /// <summary>
        /// ORC.17
        /// </summary>
        public string EnteringOrganization { get; set; }

        /// <summary>
        /// ORC.18
        /// </summary>
        public string EnteringDevice { get; set; }

        /// <summary>
        /// ORC.19
        /// </summary>
        public string ActionBy { get; set; }

        /// <summary>
        /// ORC.20
        /// </summary>
        public string AdvancedBeneficiaryNoticeCode { get; set; }

        /// <summary>
        /// ORC.21
        /// </summary>
        public string OrderingFacilityName { get; set; }

        /// <summary>
        /// ORC.22
        /// </summary>
        public string OrderingFacilityAddress { get; set; }

        /// <summary>
        /// ORC.23
        /// </summary>
        public string OrderingFacilityPhoneNumber { get; set; }

        /// <summary>
        /// ORC.24
        /// </summary>
        public string OrderingProviderAddress { get; set; }

        /// <summary>
        /// ORC.25
        /// </summary>
        public string OrderStatusModifier { get; set; }

        /// <summary>
        /// ORC.26
        /// </summary>
        public string AdvancedBeneficiaryNoticeOverrideReason { get; set; }

        /// <summary>
        /// ORC.27
        /// </summary>
        public string FillersExpectedAvailabilityDateTime { get; set; }

        /// <summary>
        /// ORC.28
        /// </summary>
        public string ConfidentialityCode { get; set; }

        /// <summary>
        /// ORC.29
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// ORC.30
        /// </summary>
        public string EntererAuthorizationMode { get; set; }

        /// <summary>
        /// ORC.31
        /// </summary>
        public string ParentUniversalServiceIdentifier { get; set; }

        /// <summary>
        /// ORC.32
        /// </summary>
        public string AdvancedBeneficiaryNoticeDate { get; set; }

        /// <summary>
        /// ORC.33
        /// </summary>
        public string AlternatePlacerOrderNumber { get; set; }

        // constructors
        public ORC()
        {
            SegmentType = SegmentType.MedOrder;
            OrderControl = string.Empty;
            PlacerOrderNumber = string.Empty;
            FillerOrderNumber = string.Empty;
            PlacerGroupNumber = string.Empty;
            OrderStatus = string.Empty;
            ResponseFlag = string.Empty;
            QuantityTiming = string.Empty;
            ParentOrder = string.Empty;
            DateTimeOfTransaction = string.Empty;
            EnteredBy = string.Empty;
            VerifiedBy = string.Empty;
            OrderingProvider = string.Empty;
            EnterersLocation = string.Empty;
            CallBackPhoneNumber = string.Empty;
            OrderEffectiveDateTime = string.Empty;
            OrderControlCodeReason = string.Empty;
            EnteringOrganization = string.Empty;
            EnteringDevice = string.Empty;
            ActionBy = string.Empty;
            AdvancedBeneficiaryNoticeCode = string.Empty;
            OrderingFacilityName = string.Empty;
            OrderingFacilityAddress = string.Empty;
            OrderingFacilityPhoneNumber = string.Empty;
            OrderingProviderAddress = string.Empty;
            OrderStatusModifier = string.Empty;
            AdvancedBeneficiaryNoticeOverrideReason = string.Empty;
            FillersExpectedAvailabilityDateTime = string.Empty;
            ConfidentialityCode = string.Empty;
            OrderType = string.Empty;
            EntererAuthorizationMode = string.Empty;
            ParentUniversalServiceIdentifier = string.Empty;
            AdvancedBeneficiaryNoticeDate = string.Empty;
            AlternatePlacerOrderNumber = string.Empty;
        }

        // methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: OrderControl = value; break;
                case 2: PlacerOrderNumber = value; break;
                case 3: FillerOrderNumber = value; break;
                case 4: PlacerGroupNumber = value; break;
                case 5: OrderStatus = value; break;
                case 6: ResponseFlag = value; break;
                case 7: QuantityTiming = value; break;
                case 8: ParentOrder = value; break;
                case 9: DateTimeOfTransaction = value; break;
                case 10: EnteredBy = value; break;
                case 11: VerifiedBy = value; break;
                case 12: OrderingProvider = value; break;
                case 13: EnterersLocation = value; break;
                case 14: CallBackPhoneNumber = value; break;
                case 15: OrderEffectiveDateTime = value; break;
                case 16: OrderControlCodeReason = value; break;
                case 17: EnteringOrganization = value; break;
                case 18: EnteringDevice = value; break;
                case 19: ActionBy = value; break;
                case 20: AdvancedBeneficiaryNoticeCode = value; break;
                case 21: OrderingFacilityName = value; break;
                case 22: OrderingFacilityAddress = value; break;
                case 23: OrderingFacilityPhoneNumber = value; break;
                case 24: OrderingProviderAddress = value; break;
                case 25: OrderStatusModifier = value; break;
                case 26: AdvancedBeneficiaryNoticeOverrideReason = value; break;
                case 27: FillersExpectedAvailabilityDateTime = value; break;
                case 28: ConfidentialityCode = value; break;
                case 29: OrderType = value; break;
                case 30: EntererAuthorizationMode = value; break;
                case 31: ParentUniversalServiceIdentifier = value; break;
                case 32: AdvancedBeneficiaryNoticeDate = value; break;
                case 33: AlternatePlacerOrderNumber = value; break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                OrderControl,
                PlacerOrderNumber,
                FillerOrderNumber,
                PlacerGroupNumber,
                OrderStatus,
                ResponseFlag,
                QuantityTiming,
                ParentOrder,
                DateTimeOfTransaction,
                EnteredBy,
                VerifiedBy,
                OrderingProvider,
                EnterersLocation,
                CallBackPhoneNumber,
                OrderEffectiveDateTime,
                OrderControlCodeReason,
                EnteringOrganization,
                EnteringDevice,
                ActionBy,
                AdvancedBeneficiaryNoticeCode,
                OrderingFacilityName,
                OrderingFacilityAddress,
                OrderingFacilityPhoneNumber,
                OrderingProviderAddress,
                OrderStatusModifier,
                AdvancedBeneficiaryNoticeOverrideReason,
                FillersExpectedAvailabilityDateTime,
                ConfidentialityCode,
                OrderType,
                EntererAuthorizationMode,
                ParentUniversalServiceIdentifier,
                AdvancedBeneficiaryNoticeDate,
                AlternatePlacerOrderNumber
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => OrderControl,
                2 => PlacerOrderNumber,
                3 => FillerOrderNumber,
                4 => PlacerGroupNumber,
                5 => OrderStatus,
                6 => ResponseFlag,
                7 => QuantityTiming,
                8 => ParentOrder,
                9 => DateTimeOfTransaction,
                10 => EnteredBy,
                11 => VerifiedBy,
                12 => OrderingProvider,
                13 => EnterersLocation,
                14 => CallBackPhoneNumber,
                15 => OrderEffectiveDateTime,
                16 => OrderControlCodeReason,
                17 => EnteringOrganization,
                18 => EnteringDevice,
                19 => ActionBy,
                20 => AdvancedBeneficiaryNoticeCode,
                21 => OrderingFacilityName,
                22 => OrderingFacilityAddress,
                23 => OrderingFacilityPhoneNumber,
                24 => OrderingProviderAddress,
                25 => OrderStatusModifier,
                26 => AdvancedBeneficiaryNoticeOverrideReason,
                27 => FillersExpectedAvailabilityDateTime,
                28 => ConfidentialityCode,
                29 => OrderType,
                30 => EntererAuthorizationMode,
                31 => ParentUniversalServiceIdentifier,
                32 => AdvancedBeneficiaryNoticeDate,
                33 => AlternatePlacerOrderNumber,
                _ => null
            };
        }
    }
}
