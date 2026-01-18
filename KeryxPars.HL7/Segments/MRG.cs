using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Merge Patient Information
    /// </summary>
    public class MRG : ISegment
    {
        public string SegmentId => nameof(MRG);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// MRG.1 - Prior Patient Identifier List (repeating)
        /// </summary>
        public CX[] PriorPatientIdentifierList { get; set; }

        /// <summary>
        /// MRG.2 - Prior Alternate Patient ID (repeating)
        /// </summary>
        public CX[] PriorAlternatePatientID { get; set; }

        /// <summary>
        /// MRG.3 - Prior Patient Account Number
        /// </summary>
        public CX PriorPatientAccountNumber { get; set; }

        /// <summary>
        /// MRG.4 - Prior Patient ID
        /// </summary>
        public CX PriorPatientID { get; set; }

        /// <summary>
        /// MRG.5 - Prior Visit Number
        /// </summary>
        public CX PriorVisitNumber { get; set; }

        /// <summary>
        /// MRG.6 - Prior Alternate Visit ID
        /// </summary>
        public CX PriorAlternateVisitID { get; set; }

        /// <summary>
        /// MRG.7 - Prior Patient Name (repeating)
        /// </summary>
        public XPN[] PriorPatientName { get; set; }

        public MRG()
        {
            SegmentType = SegmentType.ADT;
            PriorPatientIdentifierList = [];
            PriorAlternatePatientID = [];
            PriorPatientAccountNumber = default;
            PriorPatientID = default;
            PriorVisitNumber = default;
            PriorAlternateVisitID = default;
            PriorPatientName = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: PriorPatientIdentifierList = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 2: PriorAlternatePatientID = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 3:
                    var cx3 = new CX();
                    cx3.Parse(value.AsSpan(), delimiters);
                    PriorPatientAccountNumber = cx3;
                    break;
                case 4:
                    var cx4 = new CX();
                    cx4.Parse(value.AsSpan(), delimiters);
                    PriorPatientID = cx4;
                    break;
                case 5:
                    var cx5 = new CX();
                    cx5.Parse(value.AsSpan(), delimiters);
                    PriorVisitNumber = cx5;
                    break;
                case 6:
                    var cx6 = new CX();
                    cx6.Parse(value.AsSpan(), delimiters);
                    PriorAlternateVisitID = cx6;
                    break;
                case 7: PriorPatientName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SegmentFieldHelper.JoinRepeatingField(PriorPatientIdentifierList, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PriorAlternatePatientID, delimiters),
                PriorPatientAccountNumber.ToHL7String(delimiters),
                PriorPatientID.ToHL7String(delimiters),
                PriorVisitNumber.ToHL7String(delimiters),
                PriorAlternateVisitID.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PriorPatientName, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SegmentFieldHelper.JoinRepeatingField(PriorPatientIdentifierList, delimiters),
                2 => SegmentFieldHelper.JoinRepeatingField(PriorAlternatePatientID, delimiters),
                3 => PriorPatientAccountNumber.ToHL7String(delimiters),
                4 => PriorPatientID.ToHL7String(delimiters),
                5 => PriorVisitNumber.ToHL7String(delimiters),
                6 => PriorAlternateVisitID.ToHL7String(delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(PriorPatientName, delimiters),
                _ => null
            };
        }
    }
}
