using HL7.Dotnetcore;

namespace KeryxPars.Benchmarks.Parsers;

/// <summary>
/// Adapter for HL7-dotnetcore library.
/// </summary>
public class HL7DotNetCoreAdapter : IHL7ParserAdapter
{
    public string Name => "HL7-dotnetcore";
    public string Version => "2.37.0";

    public object Parse(string message)
    {
        var msg = new Message(message);
        msg.ParseMessage();
        return msg;
    }

    public string Serialize(object message)
    {
        if (message is Message msg)
        {
            return msg.SerializeMessage(false);
        }
        return string.Empty;
    }
}
