using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Scheduling Activity Information
    /// </summary>
    public class SCH : ISegment
    {
        public string SegmentId => nameof(SCH);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// SCH.1 - Placer Appointment ID
        /// </summary>
        public EI PlacerAppointmentID { get; set; }

        /// <summary>
        /// SCH.2 - Filler Appointment ID
        /// </summary>
        public EI FillerAppointmentID { get; set; }

        /// <summary>
        /// SCH.3 - Occurrence Number
        /// </summary>
        public NM OccurrenceNumber { get; set; }

        /// <summary>
        /// SCH.4 - Placer Group Number
        /// </summary>
        public EI PlacerGroupNumber { get; set; }

        /// <summary>
        /// SCH.5 - Schedule ID
        /// </summary>
        public CE ScheduleID { get; set; }

        /// <summary>
        /// SCH.6 - Event Reason
        /// </summary>
        public CE EventReason { get; set; }

        /// <summary>
        /// SCH.7 - Appointment Reason
        /// </summary>
        public CE AppointmentReason { get; set; }

        /// <summary>
        /// SCH.8 - Appointment Type
        /// </summary>
        public CE AppointmentType { get; set; }

        /// <summary>
        /// SCH.9 - Appointment Duration
        /// </summary>
        public NM AppointmentDuration { get; set; }

        /// <summary>
        /// SCH.10 - Appointment Duration Units
        /// </summary>
        public CE AppointmentDurationUnits { get; set; }

        /// <summary>
        /// SCH.11 - Appointment Timing Quantity (repeating)
        /// </summary>
        public ST[] AppointmentTimingQuantity { get; set; }

        /// <summary>
        /// SCH.12 - Placer Contact Person (repeating)
        /// </summary>
        public XCN[] PlacerContactPerson { get; set; }

        /// <summary>
        /// SCH.13 - Placer Contact Phone Number
        /// </summary>
        public XTN PlacerContactPhoneNumber { get; set; }

        /// <summary>
        /// SCH.14 - Placer Contact Address (repeating)
        /// </summary>
        public XAD[] PlacerContactAddress { get; set; }

        /// <summary>
        /// SCH.15 - Placer Contact Location
        /// </summary>
        public PL PlacerContactLocation { get; set; }

        /// <summary>
        /// SCH.16 - Filler Contact Person (repeating)
        /// </summary>
        public XCN[] FillerContactPerson { get; set; }

        /// <summary>
        /// SCH.17 - Filler Contact Phone Number
        /// </summary>
        public XTN FillerContactPhoneNumber { get; set; }

        /// <summary>
        /// SCH.18 - Filler Contact Address (repeating)
        /// </summary>
        public XAD[] FillerContactAddress { get; set; }

        /// <summary>
        /// SCH.19 - Filler Contact Location
        /// </summary>
        public PL FillerContactLocation { get; set; }

        /// <summary>
        /// SCH.20 - Entered By Person (repeating)
        /// </summary>
        public XCN[] EnteredByPerson { get; set; }

        /// <summary>
        /// SCH.21 - Entered By Phone Number (repeating)
        /// </summary>
        public XTN[] EnteredByPhoneNumber { get; set; }

        /// <summary>
        /// SCH.22 - Entered By Location
        /// </summary>
        public PL EnteredByLocation { get; set; }

        /// <summary>
        /// SCH.23 - Parent Placer Appointment ID
        /// </summary>
        public EI ParentPlacerAppointmentID { get; set; }

        /// <summary>
        /// SCH.24 - Parent Filler Appointment ID
        /// </summary>
        public EI ParentFillerAppointmentID { get; set; }

        /// <summary>
        /// SCH.25 - Filler Status Code
        /// </summary>
        public CE FillerStatusCode { get; set; }

        /// <summary>
        /// SCH.26 - Placer Order Number (repeating)
        /// </summary>
        public EI[] PlacerOrderNumber { get; set; }

        /// <summary>
        /// SCH.27 - Filler Order Number (repeating)
        /// </summary>
        public EI[] FillerOrderNumber { get; set; }

        public SCH()
        {
            SegmentType = SegmentType.Universal;
            PlacerAppointmentID = default;
            FillerAppointmentID = default;
            OccurrenceNumber = default;
            PlacerGroupNumber = default;
            ScheduleID = default;
            EventReason = default;
            AppointmentReason = default;
            AppointmentType = default;
            AppointmentDuration = default;
            AppointmentDurationUnits = default;
            AppointmentTimingQuantity = [];
            PlacerContactPerson = [];
            PlacerContactPhoneNumber = default;
            PlacerContactAddress = [];
            PlacerContactLocation = default;
            FillerContactPerson = [];
            FillerContactPhoneNumber = default;
            FillerContactAddress = [];
            FillerContactLocation = default;
            EnteredByPerson = [];
            EnteredByPhoneNumber = [];
            EnteredByLocation = default;
            ParentPlacerAppointmentID = default;
            ParentFillerAppointmentID = default;
            FillerStatusCode = default;
            PlacerOrderNumber = [];
            FillerOrderNumber = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ei1 = new EI();
                    ei1.Parse(value.AsSpan(), delimiters);
                    PlacerAppointmentID = ei1;
                    break;
                case 2:
                    var ei2 = new EI();
                    ei2.Parse(value.AsSpan(), delimiters);
                    FillerAppointmentID = ei2;
                    break;
                case 3: OccurrenceNumber = new NM(value); break;
                case 4:
                    var ei4 = new EI();
                    ei4.Parse(value.AsSpan(), delimiters);
                    PlacerGroupNumber = ei4;
                    break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    ScheduleID = ce5;
                    break;
                case 6:
                    var ce6 = new CE();
                    ce6.Parse(value.AsSpan(), delimiters);
                    EventReason = ce6;
                    break;
                case 7:
                    var ce7 = new CE();
                    ce7.Parse(value.AsSpan(), delimiters);
                    AppointmentReason = ce7;
                    break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    AppointmentType = ce8;
                    break;
                case 9: AppointmentDuration = new NM(value); break;
                case 10:
                    var ce10 = new CE();
                    ce10.Parse(value.AsSpan(), delimiters);
                    AppointmentDurationUnits = ce10;
                    break;
                case 11: AppointmentTimingQuantity = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 12: PlacerContactPerson = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 13:
                    var xtn13 = new XTN();
                    xtn13.Parse(value.AsSpan(), delimiters);
                    PlacerContactPhoneNumber = xtn13;
                    break;
                case 14: PlacerContactAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 15:
                    var pl15 = new PL();
                    pl15.Parse(value.AsSpan(), delimiters);
                    PlacerContactLocation = pl15;
                    break;
                case 16: FillerContactPerson = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 17:
                    var xtn17 = new XTN();
                    xtn17.Parse(value.AsSpan(), delimiters);
                    FillerContactPhoneNumber = xtn17;
                    break;
                case 18: FillerContactAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 19:
                    var pl19 = new PL();
                    pl19.Parse(value.AsSpan(), delimiters);
                    FillerContactLocation = pl19;
                    break;
                case 20: EnteredByPerson = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 21: EnteredByPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 22:
                    var pl22 = new PL();
                    pl22.Parse(value.AsSpan(), delimiters);
                    EnteredByLocation = pl22;
                    break;
                case 23:
                    var ei23 = new EI();
                    ei23.Parse(value.AsSpan(), delimiters);
                    ParentPlacerAppointmentID = ei23;
                    break;
                case 24:
                    var ei24 = new EI();
                    ei24.Parse(value.AsSpan(), delimiters);
                    ParentFillerAppointmentID = ei24;
                    break;
                case 25:
                    var ce25 = new CE();
                    ce25.Parse(value.AsSpan(), delimiters);
                    FillerStatusCode = ce25;
                    break;
                case 26: PlacerOrderNumber = SegmentFieldHelper.ParseRepeatingField<EI>(value, delimiters); break;
                case 27: FillerOrderNumber = SegmentFieldHelper.ParseRepeatingField<EI>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                PlacerAppointmentID.ToHL7String(delimiters),
                FillerAppointmentID.ToHL7String(delimiters),
                OccurrenceNumber.ToHL7String(delimiters),
                PlacerGroupNumber.ToHL7String(delimiters),
                ScheduleID.ToHL7String(delimiters),
                EventReason.ToHL7String(delimiters),
                AppointmentReason.ToHL7String(delimiters),
                AppointmentType.ToHL7String(delimiters),
                AppointmentDuration.ToHL7String(delimiters),
                AppointmentDurationUnits.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AppointmentTimingQuantity, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PlacerContactPerson, delimiters),
                PlacerContactPhoneNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PlacerContactAddress, delimiters),
                PlacerContactLocation.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(FillerContactPerson, delimiters),
                FillerContactPhoneNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(FillerContactAddress, delimiters),
                FillerContactLocation.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(EnteredByPerson, delimiters),
                SegmentFieldHelper.JoinRepeatingField(EnteredByPhoneNumber, delimiters),
                EnteredByLocation.ToHL7String(delimiters),
                ParentPlacerAppointmentID.ToHL7String(delimiters),
                ParentFillerAppointmentID.ToHL7String(delimiters),
                FillerStatusCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PlacerOrderNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(FillerOrderNumber, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => PlacerAppointmentID.ToHL7String(delimiters),
                2 => FillerAppointmentID.ToHL7String(delimiters),
                3 => OccurrenceNumber.Value,
                4 => PlacerGroupNumber.ToHL7String(delimiters),
                5 => ScheduleID.ToHL7String(delimiters),
                6 => EventReason.ToHL7String(delimiters),
                7 => AppointmentReason.ToHL7String(delimiters),
                8 => AppointmentType.ToHL7String(delimiters),
                9 => AppointmentDuration.Value,
                10 => AppointmentDurationUnits.ToHL7String(delimiters),
                11 => SegmentFieldHelper.JoinRepeatingField(AppointmentTimingQuantity, delimiters),
                12 => SegmentFieldHelper.JoinRepeatingField(PlacerContactPerson, delimiters),
                13 => PlacerContactPhoneNumber.ToHL7String(delimiters),
                14 => SegmentFieldHelper.JoinRepeatingField(PlacerContactAddress, delimiters),
                15 => PlacerContactLocation.ToHL7String(delimiters),
                16 => SegmentFieldHelper.JoinRepeatingField(FillerContactPerson, delimiters),
                17 => FillerContactPhoneNumber.ToHL7String(delimiters),
                18 => SegmentFieldHelper.JoinRepeatingField(FillerContactAddress, delimiters),
                19 => FillerContactLocation.ToHL7String(delimiters),
                20 => SegmentFieldHelper.JoinRepeatingField(EnteredByPerson, delimiters),
                21 => SegmentFieldHelper.JoinRepeatingField(EnteredByPhoneNumber, delimiters),
                22 => EnteredByLocation.ToHL7String(delimiters),
                23 => ParentPlacerAppointmentID.ToHL7String(delimiters),
                24 => ParentFillerAppointmentID.ToHL7String(delimiters),
                25 => FillerStatusCode.ToHL7String(delimiters),
                26 => SegmentFieldHelper.JoinRepeatingField(PlacerOrderNumber, delimiters),
                27 => SegmentFieldHelper.JoinRepeatingField(FillerOrderNumber, delimiters),
                _ => null
            };
        }
    }
}
