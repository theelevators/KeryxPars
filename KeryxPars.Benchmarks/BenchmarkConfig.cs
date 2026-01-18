using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;

namespace KeryxPars.Benchmarks;

/// <summary>
/// Custom configuration for KeryxPars benchmarks.
/// </summary>
public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        // Add default job
        AddJob(Job.Default
            .WithGcServer(true)
            .WithGcConcurrent(true)
            .WithGcForce(false));

        // Add memory diagnoser
        AddDiagnoser(MemoryDiagnoser.Default);

        // Add loggers
        AddLogger(ConsoleLogger.Default);

        // Add exporters
        AddExporter(MarkdownExporter.GitHub);
        AddExporter(HtmlExporter.Default);
        AddExporter(JsonExporter.Brief);

        // Add columns
        AddColumn(StatisticColumn.Mean);
        AddColumn(StatisticColumn.StdDev);
        AddColumn(StatisticColumn.Median);
        AddColumn(StatisticColumn.Min);
        AddColumn(StatisticColumn.Max);
        AddColumn(RankColumn.Arabic);
        AddColumn(BaselineRatioColumn.RatioMean);

        // Ordering
        WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

        // Options
        WithOptions(ConfigOptions.DisableOptimizationsValidator);
    }
}
