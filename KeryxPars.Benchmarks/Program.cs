using BenchmarkDotNet.Running;
using KeryxPars.Benchmarks.Benchmarks;

namespace KeryxPars.Benchmarks;

/// <summary>
/// Benchmark runner for HL7 parser performance comparison.
/// 
/// Usage:
///   dotnet run -c Release
///   dotnet run -c Release --filter *ParserComparison*
///   dotnet run -c Release --filter *Memory*
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Run all benchmarks
        var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        
        // Or run specific benchmarks:
        // BenchmarkRunner.Run<ParserComparison>();
        // BenchmarkRunner.Run<MemoryBenchmarks>();
        // BenchmarkRunner.Run<DeserializationBenchmarks>();
    }
}
