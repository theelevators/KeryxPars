using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Observation Request
    /// </summary>
    public class OBR : ISegment
    {
        public string SegmentId => nameof(OBR);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// OBR.1 - Set ID - OBR
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// OBR.2 - Placer Order Number
        /// </summary>
        public EI PlacerOrderNumber { get; set; }

        /// <summary>
        /// OBR.3 - Filler Order Number
        /// </summary>
        public EI FillerOrderNumber { get; set; }

        /// <summary>
        /// OBR.4 - Universal Service Identifier
        /// </summary>
        public CE UniversalServiceIdentifier { get; set; }

        /// <summary>
        /// OBR.5 - Priority
        /// </summary>
        public ID Priority { get; set; }

        /// <summary>
        /// OBR.6 - Requested Date/Time
        /// </summary>
        public DTM RequestedDateTime { get; set; }

        /// <summary>
        /// OBR.7 - Observation Date/Time
        /// </summary>
        public DTM ObservationDateTime { get; set; }

        /// <summary>
        /// OBR.8 - Observation End Date/Time
        /// </summary>
        public DTM ObservationEndDateTime { get; set; }

        /// <summary>
        /// OBR.9 - Collection Volume
        /// </summary>
        public CQ CollectionVolume { get; set; }

        /// <summary>
        /// OBR.10 - Collector Identifier (repeating)
        /// </summary>
        public XCN[] CollectorIdentifier { get; set; }

        /// <summary>
        /// OBR.11 - Specimen Action Code
        /// </summary>
        public ID SpecimenActionCode { get; set; }

        /// <summary>
        /// OBR.12 - Danger Code
        /// </summary>
        public CE DangerCode { get; set; }

        /// <summary>
        /// OBR.13 - Relevant Clinical Information
        /// </summary>
        public ST RelevantClinicalInformation { get; set; }

        /// <summary>
        /// OBR.14 - Specimen Received Date/Time
        /// </summary>
        public DTM SpecimenReceivedDateTime { get; set; }

        /// <summary>
        /// OBR.15 - Specimen Source
        /// </summary>
        public ST SpecimenSource { get; set; }

        /// <summary>
        /// OBR.16 - Ordering Provider (repeating)
        /// </summary>
        public XCN[] OrderingProvider { get; set; }

        /// <summary>
        /// OBR.17 - Order Callback Phone Number (repeating)
        /// </summary>
        public XTN[] OrderCallbackPhoneNumber { get; set; }

        /// <summary>
        /// OBR.18 - Placer Field 1
        /// </summary>
        public ST PlacerField1 { get; set; }

        /// <summary>
        /// OBR.19 - Placer Field 2
        /// </summary>
        public ST PlacerField2 { get; set; }

        /// <summary>
        /// OBR.20 - Filler Field 1
        /// </summary>
        public ST FillerField1 { get; set; }

        /// <summary>
        /// OBR.21 - Filler Field 2
        /// </summary>
        public ST FillerField2 { get; set; }

        /// <summary>
        /// OBR.22 - Results Rpt/Status Chng - Date/Time
        /// </summary>
        public DTM ResultsRptStatusChngDateTime { get; set; }

        /// <summary>
        /// OBR.23 - Charge to Practice
        /// </summary>
        public ST ChargeToPractice { get; set; }

        /// <summary>
        /// OBR.24 - Diagnostic Serv Sect ID
        /// </summary>
        public ID DiagnosticServSectID { get; set; }

        /// <summary>
        /// OBR.25 - Result Status
        /// </summary>
        public ID ResultStatus { get; set; }

        /// <summary>
        /// OBR.26 - Parent Result
        /// </summary>
        public ST ParentResult { get; set; }

        /// <summary>
        /// OBR.27 - Quantity/Timing (repeating)
        /// </summary>
        public ST[] QuantityTiming { get; set; }

        /// <summary>
        /// OBR.28 - Result Copies To (repeating)
        /// </summary>
        public XCN[] ResultCopiesTo { get; set; }

        /// <summary>
        /// OBR.29 - Parent Number
        /// </summary>
        public ST ParentNumber { get; set; }

        /// <summary>
        /// OBR.30 - Transportation Mode
        /// </summary>
        public ID TransportationMode { get; set; }

        /// <summary>
        /// OBR.31 - Reason for Study (repeating)
        /// </summary>
        public CE[] ReasonForStudy { get; set; }

        /// <summary>
        /// OBR.32 - Principal Result Interpreter
        /// </summary>
        public XCN PrincipalResultInterpreter { get; set; }

        /// <summary>
        /// OBR.33 - Assistant Result Interpreter (repeating)
        /// </summary>
        public XCN[] AssistantResultInterpreter { get; set; }

        /// <summary>
        /// OBR.34 - Technician (repeating)
        /// </summary>
        public XCN[] Technician { get; set; }

        /// <summary>
        /// OBR.35 - Transcriptionist (repeating)
        /// </summary>
        public XCN[] Transcriptionist { get; set; }

        /// <summary>
        /// OBR.36 - Scheduled Date/Time
        /// </summary>
        public DTM ScheduledDateTime { get; set; }

        /// <summary>
        /// OBR.37 - Number of Sample Containers
        /// </summary>
        public NM NumberOfSampleContainers { get; set; }

        /// <summary>
        /// OBR.38 - Transport Logistics of Collected Sample (repeating)
        /// </summary>
        public CE[] TransportLogisticsOfCollectedSample { get; set; }

        /// <summary>
        /// OBR.39 - Collector's Comment (repeating)
        /// </summary>
        public CE[] CollectorComment { get; set; }

        /// <summary>
        /// OBR.40 - Transport Arrangement Responsibility
        /// </summary>
        public CE TransportArrangementResponsibility { get; set; }

        /// <summary>
        /// OBR.41 - Transport Arranged
        /// </summary>
        public ID TransportArranged { get; set; }

        /// <summary>
        /// OBR.42 - Escort Required
        /// </summary>
        public ID EscortRequired { get; set; }

        /// <summary>
        /// OBR.43 - Planned Patient Transport Comment (repeating)
        /// </summary>
        public CE[] PlannedPatientTransportComment { get; set; }

        /// <summary>
        /// OBR.44 - Procedure Code
        /// </summary>
        public CE ProcedureCode { get; set; }

        /// <summary>
        /// OBR.45 - Procedure Code Modifier (repeating)
        /// </summary>
        public CE[] ProcedureCodeModifier { get; set; }

        public OBR()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            PlacerOrderNumber = default;
            FillerOrderNumber = default;
            UniversalServiceIdentifier = default;
            Priority = default;
            RequestedDateTime = default;
            ObservationDateTime = default;
            ObservationEndDateTime = default;
            CollectionVolume = default;
            CollectorIdentifier = [];
            SpecimenActionCode = default;
            DangerCode = default;
            RelevantClinicalInformation = default;
            SpecimenReceivedDateTime = default;
            SpecimenSource = default;
            OrderingProvider = [];
            OrderCallbackPhoneNumber = [];
            PlacerField1 = default;
            PlacerField2 = default;
            FillerField1 = default;
            FillerField2 = default;
            ResultsRptStatusChngDateTime = default;
            ChargeToPractice = default;
            DiagnosticServSectID = default;
            ResultStatus = default;
            ParentResult = default;
            QuantityTiming = [];
            ResultCopiesTo = [];
            ParentNumber = default;
            TransportationMode = default;
            ReasonForStudy = [];
            PrincipalResultInterpreter = default;
            AssistantResultInterpreter = [];
            Technician = [];
            Transcriptionist = [];
            ScheduledDateTime = default;
            NumberOfSampleContainers = default;
            TransportLogisticsOfCollectedSample = [];
            CollectorComment = [];
            TransportArrangementResponsibility = default;
            TransportArranged = default;
            EscortRequired = default;
            PlannedPatientTransportComment = [];
            ProcedureCode = default;
            ProcedureCodeModifier = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2:
                    var ei2 = new EI();
                    ei2.Parse(value.AsSpan(), delimiters);
                    PlacerOrderNumber = ei2;
                    break;
                case 3:
                    var ei3 = new EI();
                    ei3.Parse(value.AsSpan(), delimiters);
                    FillerOrderNumber = ei3;
                    break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    UniversalServiceIdentifier = ce4;
                    break;
                case 5: Priority = new ID(value); break;
                case 6: RequestedDateTime = new DTM(value); break;
                case 7: ObservationDateTime = new DTM(value); break;
                case 8: ObservationEndDateTime = new DTM(value); break;
                case 9:
                    var cq9 = new CQ();
                    cq9.Parse(value.AsSpan(), delimiters);
                    CollectionVolume = cq9;
                    break;
                case 10: CollectorIdentifier = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 11: SpecimenActionCode = new ID(value); break;
                case 12:
                    var ce12 = new CE();
                    ce12.Parse(value.AsSpan(), delimiters);
                    DangerCode = ce12;
                    break;
                case 13: RelevantClinicalInformation = new ST(value); break;
                case 14: SpecimenReceivedDateTime = new DTM(value); break;
                case 15: SpecimenSource = new ST(value); break;
                case 16: OrderingProvider = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 17: OrderCallbackPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 18: PlacerField1 = new ST(value); break;
                case 19: PlacerField2 = new ST(value); break;
                case 20: FillerField1 = new ST(value); break;
                case 21: FillerField2 = new ST(value); break;
                case 22: ResultsRptStatusChngDateTime = new DTM(value); break;
                case 23: ChargeToPractice = new ST(value); break;
                case 24: DiagnosticServSectID = new ID(value); break;
                case 25: ResultStatus = new ID(value); break;
                case 26: ParentResult = new ST(value); break;
                case 27: QuantityTiming = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 28: ResultCopiesTo = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 29: ParentNumber = new ST(value); break;
                case 30: TransportationMode = new ID(value); break;
                case 31: ReasonForStudy = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 32:
                    var xcn32 = new XCN();
                    xcn32.Parse(value.AsSpan(), delimiters);
                    PrincipalResultInterpreter = xcn32;
                    break;
                case 33: AssistantResultInterpreter = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 34: Technician = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 35: Transcriptionist = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 36: ScheduledDateTime = new DTM(value); break;
                case 37: NumberOfSampleContainers = new NM(value); break;
                case 38: TransportLogisticsOfCollectedSample = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 39: CollectorComment = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 40:
                    var ce40 = new CE();
                    ce40.Parse(value.AsSpan(), delimiters);
                    TransportArrangementResponsibility = ce40;
                    break;
                case 41: TransportArranged = new ID(value); break;
                case 42: EscortRequired = new ID(value); break;
                case 43: PlannedPatientTransportComment = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 44:
                    var ce44 = new CE();
                    ce44.Parse(value.AsSpan(), delimiters);
                    ProcedureCode = ce44;
                    break;
                case 45: ProcedureCodeModifier = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                PlacerOrderNumber.ToHL7String(delimiters),
                FillerOrderNumber.ToHL7String(delimiters),
                UniversalServiceIdentifier.ToHL7String(delimiters),
                Priority.ToHL7String(delimiters),
                RequestedDateTime.ToHL7String(delimiters),
                ObservationDateTime.ToHL7String(delimiters),
                ObservationEndDateTime.ToHL7String(delimiters),
                CollectionVolume.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(CollectorIdentifier, delimiters),
                SpecimenActionCode.ToHL7String(delimiters),
                DangerCode.ToHL7String(delimiters),
                RelevantClinicalInformation.ToHL7String(delimiters),
                SpecimenReceivedDateTime.ToHL7String(delimiters),
                SpecimenSource.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderingProvider, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderCallbackPhoneNumber, delimiters),
                PlacerField1.ToHL7String(delimiters),
                PlacerField2.ToHL7String(delimiters),
                FillerField1.ToHL7String(delimiters),
                FillerField2.ToHL7String(delimiters),
                ResultsRptStatusChngDateTime.ToHL7String(delimiters),
                ChargeToPractice.ToHL7String(delimiters),
                DiagnosticServSectID.ToHL7String(delimiters),
                ResultStatus.ToHL7String(delimiters),
                ParentResult.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(QuantityTiming, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ResultCopiesTo, delimiters),
                ParentNumber.ToHL7String(delimiters),
                TransportationMode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ReasonForStudy, delimiters),
                PrincipalResultInterpreter.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AssistantResultInterpreter, delimiters),
                SegmentFieldHelper.JoinRepeatingField(Technician, delimiters),
                SegmentFieldHelper.JoinRepeatingField(Transcriptionist, delimiters),
                ScheduledDateTime.ToHL7String(delimiters),
                NumberOfSampleContainers.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(TransportLogisticsOfCollectedSample, delimiters),
                SegmentFieldHelper.JoinRepeatingField(CollectorComment, delimiters),
                TransportArrangementResponsibility.ToHL7String(delimiters),
                TransportArranged.ToHL7String(delimiters),
                EscortRequired.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PlannedPatientTransportComment, delimiters),
                ProcedureCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProcedureCodeModifier, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => PlacerOrderNumber.ToHL7String(delimiters),
                3 => FillerOrderNumber.ToHL7String(delimiters),
                4 => UniversalServiceIdentifier.ToHL7String(delimiters),
                5 => Priority.Value,
                6 => RequestedDateTime.Value,
                7 => ObservationDateTime.Value,
                8 => ObservationEndDateTime.Value,
                9 => CollectionVolume.ToHL7String(delimiters),
                10 => SegmentFieldHelper.JoinRepeatingField(CollectorIdentifier, delimiters),
                11 => SpecimenActionCode.Value,
                12 => DangerCode.ToHL7String(delimiters),
                13 => RelevantClinicalInformation.Value,
                14 => SpecimenReceivedDateTime.Value,
                15 => SpecimenSource.Value,
                16 => SegmentFieldHelper.JoinRepeatingField(OrderingProvider, delimiters),
                17 => SegmentFieldHelper.JoinRepeatingField(OrderCallbackPhoneNumber, delimiters),
                18 => PlacerField1.Value,
                19 => PlacerField2.Value,
                20 => FillerField1.Value,
                21 => FillerField2.Value,
                22 => ResultsRptStatusChngDateTime.Value,
                23 => ChargeToPractice.Value,
                24 => DiagnosticServSectID.Value,
                25 => ResultStatus.Value,
                26 => ParentResult.Value,
                27 => SegmentFieldHelper.JoinRepeatingField(QuantityTiming, delimiters),
                28 => SegmentFieldHelper.JoinRepeatingField(ResultCopiesTo, delimiters),
                29 => ParentNumber.Value,
                30 => TransportationMode.Value,
                31 => SegmentFieldHelper.JoinRepeatingField(ReasonForStudy, delimiters),
                32 => PrincipalResultInterpreter.ToHL7String(delimiters),
                33 => SegmentFieldHelper.JoinRepeatingField(AssistantResultInterpreter, delimiters),
                34 => SegmentFieldHelper.JoinRepeatingField(Technician, delimiters),
                35 => SegmentFieldHelper.JoinRepeatingField(Transcriptionist, delimiters),
                36 => ScheduledDateTime.Value,
                37 => NumberOfSampleContainers.Value,
                38 => SegmentFieldHelper.JoinRepeatingField(TransportLogisticsOfCollectedSample, delimiters),
                39 => SegmentFieldHelper.JoinRepeatingField(CollectorComment, delimiters),
                40 => TransportArrangementResponsibility.ToHL7String(delimiters),
                41 => TransportArranged.Value,
                42 => EscortRequired.Value,
                43 => SegmentFieldHelper.JoinRepeatingField(PlannedPatientTransportComment, delimiters),
                44 => ProcedureCode.ToHL7String(delimiters),
                45 => SegmentFieldHelper.JoinRepeatingField(ProcedureCodeModifier, delimiters),
                _ => null
            };
        }
    }
}
