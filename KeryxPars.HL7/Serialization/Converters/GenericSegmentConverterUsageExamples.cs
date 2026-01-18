namespace KeryxPars.HL7.Serialization.Converters;

/// <summary>
/// Example usage and documentation for the GenericSegmentConverter.
/// 
/// The GenericSegmentConverter provides automatic parsing and serialization for all HL7 segments
/// that implement the ISegment interface. It uses the segment's SetValue() method for parsing
/// and GetValues() method for serialization.
/// 
/// USAGE EXAMPLE:
/// 
/// // 1. Parse an HL7 message
/// var message = "MSH|^~\\&|SENDER|FACILITY|RECEIVER|FACILITY|20240101120000||ADT^A01|12345|P|2.5\r\n" +
///               "EVN|A01|20240101120000\r\n" +
///               "PID|1||123456||DOE^JOHN||19800101|M\r\n";
/// 
/// var result = HL7Serializer.Deserialize(message);
/// 
/// if (result.IsSuccess)
/// {
///     var hl7Message = result.Value;
///     Console.WriteLine($"Patient: {hl7Message.Pid.PatientName}");
///     Console.WriteLine($"Event: {hl7Message.Evn.EventType}");
/// }
/// 
/// // 2. Extract and serialize an HL7 message
/// var newMessage = new HL7Message
/// {
///     Msh = new MSH 
///     { 
///         SendingApplication = "MyApp",
///         SendingFacility = "MyFacility",
///         DateTimeOfMessage = "20240101120000",
///         MessageType = "ADT^A01",
///         MessageControlID = "12345"
///     },
///     Pid = new PID
///     {
///         PatientName = "DOE^JOHN",
///         DateTimeofBirth = "19800101",
///         AdministrativeSex = "M"
///     }
/// };
/// 
/// var serialized = HL7Serializer.Serialize(newMessage);
/// if (serialized.IsSuccess)
/// {
///     Console.WriteLine(serialized.Value);
/// }
/// 
/// // 3. Register a custom segment converter
/// var registry = new DefaultSegmentRegistry();
/// registry.Register(new GenericSegmentConverter<CustomSegment>());
/// 
/// var options = new SerializerOptions
/// {
///     SegmentRegistry = registry
/// };
/// 
/// var customResult = HL7Serializer.Deserialize(message, options);
/// 
/// SUPPORTED SEGMENTS:
/// - MSH (Message Header)
/// - MSA (Message Acknowledgement)
/// - ERR (Error Information)
/// - EVN (Event Type)
/// - PID (Patient Identification)
/// - PD1 (Additional Demographics)
/// - NK1 (Next of Kin)
/// - PV1 (Patient Visit)
/// - PV2 (Patient Visit - Additional)
/// - AL1 (Allergy Information)
/// - DG1 (Diagnosis Information)
/// - OBX (Observation/Result)
/// - IN1 (Insurance Information)
/// - ORC (Common Order)
/// - RXO (Pharmacy Prescription Order)
/// - RXE (Pharmacy Encoded Order)
/// - RXR (Pharmacy Route)
/// - RXC (Pharmacy Component)
/// - TQ1 (Timing Quantity)
/// - TQ2 (Timing/Quantity Relationship)
/// 
/// ADDING NEW SEGMENTS:
/// 
/// 1. Extract a new segment class implementing ISegment:
/// 
///    public class ZZZ : ISegment
///    {
///        public string SegmentId => nameof(ZZZ);
///        public SegmentType SegmentType { get; private set; }
///        
///        public string CustomField1 { get; set; }
///        public string CustomField2 { get; set; }
///        
///        public void SetValue(string value, int element)
///        {
///            switch (element)
///            {
///                case 1: CustomField1 = value; break;
///                case 2: CustomField2 = value; break;
///            }
///        }
///        
///        public string[] GetValues()
///        {
///            return new[] { SegmentId, CustomField1, CustomField2 };
///        }
///        
///        public string? GetField(int index) => index switch
///        {
///            0 => SegmentId,
///            1 => CustomField1,
///            2 => CustomField2,
///            _ => null
///        };
///    }
/// 
/// 2. Register it with the registry:
/// 
///    registry.Register(new GenericSegmentConverter<ZZZ>());
/// 
/// PERFORMANCE CHARACTERISTICS:
/// 
/// - Zero-allocation parsing using Span<char> and ref structs
/// - Pooled buffers for serialization using ArrayPool<char>
/// - Frozen dictionaries for fast segment lookup
/// - Generic converter reduces code duplication
/// - Type-safe with compile-time checking via nameof()
/// 
/// </summary>
internal class GenericSegmentConverterUsageExamples
{
    // This class is for documentation purposes only
}
