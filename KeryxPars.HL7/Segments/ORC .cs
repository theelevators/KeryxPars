using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Common Order
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class ORC : ISegment
    {
        public string SegmentId => nameof(ORC);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// ORC.1 - Order Control
        /// </summary>
        public ID OrderControl { get; set; }

        /// <summary>
        /// ORC.2 - Placer Order Number
        /// </summary>
        public EI PlacerOrderNumber { get; set; }

        /// <summary>
        /// ORC.3 - Filler Order Number
        /// </summary>
        public EI FillerOrderNumber { get; set; }

        /// <summary>
        /// ORC.4 - Placer Group Number
        /// </summary>
        public EI PlacerGroupNumber { get; set; }

        /// <summary>
        /// ORC.5 - Order Status
        /// </summary>
        public ID OrderStatus { get; set; }

        /// <summary>
        /// ORC.6 - Response Flag
        /// </summary>
        public ID ResponseFlag { get; set; }

        /// <summary>
        /// ORC.7 - Quantity/Timing - deprecated, use TQ1/TQ2
        /// </summary>
        public ST QuantityTiming { get; set; }

        /// <summary>
        /// ORC.8 - Parent Order
        /// </summary>
        public ST ParentOrder { get; set; }

        /// <summary>
        /// ORC.9 - Date/Time of Transaction
        /// </summary>
        public DTM DateTimeOfTransaction { get; set; }

        /// <summary>
        /// ORC.10 - Entered By (repeating)
        /// </summary>
        public XCN[] EnteredBy { get; set; }

        /// <summary>
        /// ORC.11 - Verified By (repeating)
        /// </summary>
        public XCN[] VerifiedBy { get; set; }

        /// <summary>
        /// ORC.12 - Ordering Provider (repeating)
        /// </summary>
        public XCN[] OrderingProvider { get; set; }

        /// <summary>
        /// ORC.13 - Enterer's Location
        /// </summary>
        public PL EnterersLocation { get; set; }

        /// <summary>
        /// ORC.14 - Call Back Phone Number (repeating)
        /// </summary>
        public XTN[] CallBackPhoneNumber { get; set; }

        /// <summary>
        /// ORC.15 - Order Effective Date/Time
        /// </summary>
        public DTM OrderEffectiveDateTime { get; set; }

        /// <summary>
        /// ORC.16 - Order Control Code Reason
        /// </summary>
        public CE OrderControlCodeReason { get; set; }

        /// <summary>
        /// ORC.17 - Entering Organization
        /// </summary>
        public CE EnteringOrganization { get; set; }

        /// <summary>
        /// ORC.18 - Entering Device
        /// </summary>
        public CE EnteringDevice { get; set; }

        /// <summary>
        /// ORC.19 - Action By (repeating)
        /// </summary>
        public XCN[] ActionBy { get; set; }

        /// <summary>
        /// ORC.20 - Advanced Beneficiary Notice Code
        /// </summary>
        public CE AdvancedBeneficiaryNoticeCode { get; set; }

        /// <summary>
        /// ORC.21 - Ordering Facility Name (repeating)
        /// </summary>
        public XON[] OrderingFacilityName { get; set; }

        /// <summary>
        /// ORC.22 - Ordering Facility Address (repeating)
        /// </summary>
        public XAD[] OrderingFacilityAddress { get; set; }

        /// <summary>
        /// ORC.23 - Ordering Facility Phone Number (repeating)
        /// </summary>
        public XTN[] OrderingFacilityPhoneNumber { get; set; }

        /// <summary>
        /// ORC.24 - Ordering Provider Address (repeating)
        /// </summary>
        public XAD[] OrderingProviderAddress { get; set; }

        /// <summary>
        /// ORC.25 - Order Status Modifier
        /// </summary>
        public CWE OrderStatusModifier { get; set; }

        /// <summary>
        /// ORC.26 - Advanced Beneficiary Notice Override Reason
        /// </summary>
        public CWE AdvancedBeneficiaryNoticeOverrideReason { get; set; }

        /// <summary>
        /// ORC.27 - Filler's Expected Availability Date/Time
        /// </summary>
        public DTM FillersExpectedAvailabilityDateTime { get; set; }

        /// <summary>
        /// ORC.28 - Confidentiality Code
        /// </summary>
        public CWE ConfidentialityCode { get; set; }

        /// <summary>
        /// ORC.29 - Order Type
        /// </summary>
        public CWE OrderType { get; set; }

        /// <summary>
        /// ORC.30 - Enterer Authorization Mode
        /// </summary>
        public ST EntererAuthorizationMode { get; set; }

        /// <summary>
        /// ORC.31 - Parent Universal Service Identifier
        /// </summary>
        public CWE ParentUniversalServiceIdentifier { get; set; }

        /// <summary>
        /// ORC.32 - Advanced Beneficiary Notice Date
        /// </summary>
        public DT AdvancedBeneficiaryNoticeDate { get; set; }

        /// <summary>
        /// ORC.33 - Alternate Placer Order Number (repeating)
        /// </summary>
        public CX[] AlternatePlacerOrderNumber { get; set; }

        public ORC()
        {
            SegmentType = SegmentType.MedOrder;
            
            OrderControl = default;
            PlacerOrderNumber = default;
            FillerOrderNumber = default;
            PlacerGroupNumber = default;
            OrderStatus = default;
            ResponseFlag = default;
            QuantityTiming = default;
            ParentOrder = default;
            DateTimeOfTransaction = default;
            EnterersLocation = default;
            OrderEffectiveDateTime = default;
            OrderControlCodeReason = default;
            EnteringOrganization = default;
            EnteringDevice = default;
            AdvancedBeneficiaryNoticeCode = default;
            OrderStatusModifier = default;
            AdvancedBeneficiaryNoticeOverrideReason = default;
            FillersExpectedAvailabilityDateTime = default;
            ConfidentialityCode = default;
            OrderType = default;
            EntererAuthorizationMode = default;
            ParentUniversalServiceIdentifier = default;
            AdvancedBeneficiaryNoticeDate = default;
            
            EnteredBy = Array.Empty<XCN>();
            VerifiedBy = Array.Empty<XCN>();
            OrderingProvider = Array.Empty<XCN>();
            CallBackPhoneNumber = Array.Empty<XTN>();
            ActionBy = Array.Empty<XCN>();
            OrderingFacilityName = Array.Empty<XON>();
            OrderingFacilityAddress = Array.Empty<XAD>();
            OrderingFacilityPhoneNumber = Array.Empty<XTN>();
            OrderingProviderAddress = Array.Empty<XAD>();
            AlternatePlacerOrderNumber = Array.Empty<CX>();
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: OrderControl = new ID(value); break;
                case 2: PlacerOrderNumber = value; break;
                case 3: FillerOrderNumber = value; break;
                case 4: PlacerGroupNumber = value; break;
                case 5: OrderStatus = new ID(value); break;
                case 6: ResponseFlag = new ID(value); break;
                case 7: QuantityTiming = new ST(value); break;
                case 8: ParentOrder = new ST(value); break;
                case 9: DateTimeOfTransaction = new DTM(value); break;
                case 10: EnteredBy = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 11: VerifiedBy = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 12: OrderingProvider = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 13:
                    var pl = new PL();
                    pl.Parse(value.AsSpan(), delimiters);
                    EnterersLocation = pl;
                    break;
                case 14: CallBackPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 15: OrderEffectiveDateTime = new DTM(value); break;
                case 16: OrderControlCodeReason = value; break;
                case 17: EnteringOrganization = value; break;
                case 18: EnteringDevice = value; break;
                case 19: ActionBy = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 20: AdvancedBeneficiaryNoticeCode = value; break;
                case 21: OrderingFacilityName = SegmentFieldHelper.ParseRepeatingField<XON>(value, delimiters); break;
                case 22: OrderingFacilityAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 23: OrderingFacilityPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 24: OrderingProviderAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 25: OrderStatusModifier = value; break;
                case 26: AdvancedBeneficiaryNoticeOverrideReason = value; break;
                case 27: FillersExpectedAvailabilityDateTime = new DTM(value); break;
                case 28: ConfidentialityCode = value; break;
                case 29: OrderType = value; break;
                case 30: EntererAuthorizationMode = new ST(value); break;
                case 31: ParentUniversalServiceIdentifier = value; break;
                case 32: AdvancedBeneficiaryNoticeDate = new DT(value); break;
                case 33: AlternatePlacerOrderNumber = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                OrderControl.ToHL7String(delimiters),
                PlacerOrderNumber.ToHL7String(delimiters),
                FillerOrderNumber.ToHL7String(delimiters),
                PlacerGroupNumber.ToHL7String(delimiters),
                OrderStatus.ToHL7String(delimiters),
                ResponseFlag.ToHL7String(delimiters),
                QuantityTiming.ToHL7String(delimiters),
                ParentOrder.ToHL7String(delimiters),
                DateTimeOfTransaction.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(EnteredBy, delimiters),
                SegmentFieldHelper.JoinRepeatingField(VerifiedBy, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingProvider, delimiters),
                EnterersLocation.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(CallBackPhoneNumber, delimiters),
                OrderEffectiveDateTime.ToHL7String(delimiters),
                OrderControlCodeReason.ToHL7String(delimiters),
                EnteringOrganization.ToHL7String(delimiters),
                EnteringDevice.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ActionBy, delimiters),
                AdvancedBeneficiaryNoticeCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingFacilityName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingFacilityAddress, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingFacilityPhoneNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingProviderAddress, delimiters),
                OrderStatusModifier.ToHL7String(delimiters),
                AdvancedBeneficiaryNoticeOverrideReason.ToHL7String(delimiters),
                FillersExpectedAvailabilityDateTime.ToHL7String(delimiters),
                ConfidentialityCode.ToHL7String(delimiters),
                OrderType.ToHL7String(delimiters),
                EntererAuthorizationMode.ToHL7String(delimiters),
                ParentUniversalServiceIdentifier.ToHL7String(delimiters),
                AdvancedBeneficiaryNoticeDate.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AlternatePlacerOrderNumber, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var values = GetValues();
            return index >= 0 && index < values.Length ? values[index] : null;
        }
    }
}
