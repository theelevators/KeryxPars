using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using KeryxPars.Benchmarks.Data;
using KeryxPars.Benchmarks.Parsers;

namespace KeryxPars.Benchmarks.Benchmarks;

/// <summary>
/// Generic parser comparison using adapters.
/// Makes it easy to add new third-party parsers for comparison.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class GenericParserComparison
{
    private readonly IHL7ParserAdapter[] _parsers;
    private string _testMessage = null!;

    public GenericParserComparison()
    {
        _parsers = new IHL7ParserAdapter[]
        {
            new KeryxParsAdapter(),
            new HL7DotNetCoreAdapter(),
            new NHapiAdapter()
            // Add more parsers here as needed
        };
    }

    [ParamsSource(nameof(MessageNames))]
    public string MessageType { get; set; } = null!;

    public IEnumerable<string> MessageNames => TestMessages.AllMessages.Keys;

    [GlobalSetup]
    public void Setup()
    {
        _testMessage = TestMessages.AllMessages[MessageType];
    }

    [Benchmark(Description = "KeryxPars")]
    [BenchmarkCategory("Parse")]
    public object ParseWithKeryxPars()
    {
        return _parsers[0].Parse(_testMessage);
    }

    [Benchmark(Description = "HL7-dotnetcore")]
    [BenchmarkCategory("Parse")]
    public object ParseWithHL7DotNetCore()
    {
        return _parsers[1].Parse(_testMessage);
    }

    [Benchmark(Description = "NHapi")]
    [BenchmarkCategory("Parse")]
    public object ParseWithNHapi()
    {
        return _parsers[2].Parse(_testMessage);
    }

    // Add methods for new parsers here following the same pattern
}

/// <summary>
/// How to add a new parser:
/// 
/// 1. Add NuGet package to KeryxPars.Benchmarks.csproj
/// 
/// 2. Create adapter class implementing IHL7ParserAdapter:
/// 
///    public class MyParserAdapter : IHL7ParserAdapter
///    {
///        public string Name => "MyParser";
///        public string Version => "1.0.0";
///        
///        public object Parse(string message)
///        {
///            var parser = new MyParser();
///            return parser.Parse(message);
///        }
///        
///        public string Serialize(object message)
///        {
///            // Implementation
///        }
///    }
/// 
/// 3. Add to constructor:
/// 
///    _parsers = new IHL7ParserAdapter[]
///    {
///        new KeryxParsAdapter(),
///        new HL7DotNetCoreAdapter(),
///        new NHapiAdapter(),
///        new MyParserAdapter()  // Add here
///    };
/// 
/// 4. Add benchmark method:
/// 
///    [Benchmark(Description = "MyParser")]
///    [BenchmarkCategory("Parse")]
///    public object ParseWithMyParser()
///    {
///        return _parsers[3].Parse(_testMessage);
///    }
/// 
/// </summary>
public static class ParserComparisonGuide
{
    // Documentation class - no implementation needed
}
