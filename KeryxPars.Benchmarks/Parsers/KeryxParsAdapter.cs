using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.Benchmarks.Parsers;

/// <summary>
/// Adapter for KeryxPars HL7 parser.
/// </summary>
public class KeryxParsAdapter : IHL7ParserAdapter
{
    public string Name => "KeryxPars";
    public string Version => "1.0.0";

    public object Parse(string message)
    {
        var result = HL7Serializer.Deserialize(message);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    public string Serialize(object message)
    {
        if (message is HL7Message hl7Message)
        {
            var result = HL7Serializer.Serialize(hl7Message);
            return result.IsSuccess ? result.Value : string.Empty;
        }
        return string.Empty;
    }
}
