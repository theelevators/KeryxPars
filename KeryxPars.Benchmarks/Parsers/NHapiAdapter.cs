using NHapi.Base.Parser;
using NHapi.Base.Model;

namespace KeryxPars.Benchmarks.Parsers;

/// <summary>
/// Adapter for NHapi library (enterprise-grade HL7 parser).
/// </summary>
public class NHapiAdapter : IHL7ParserAdapter
{
    private readonly PipeParser _parser = new();

    public string Name => "NHapi";
    public string Version => "3.2.0";

    public object Parse(string message)
    {
        return _parser.Parse(message);
    }

    public string Serialize(object message)
    {
        if (message is IMessage msg)
        {
            return _parser.Encode(msg);
        }
        return string.Empty;
    }
}
