# KeryxPars Benchmarks - Quick Start Guide

## ?? Running Your First Benchmark

### Windows
```cmd
cd KeryxPars.Benchmarks
run-benchmarks.bat ParserComparison
```

### Linux/macOS
```bash
cd KeryxPars.Benchmarks
chmod +x run-benchmarks.sh
./run-benchmarks.sh ParserComparison
```

### Manual
```bash
dotnet run -c Release --filter *ParserComparison*
```

## ?? What Gets Tested

### Parser Comparison
Compares KeryxPars against:
- **HL7-dotnetcore** (v2.37.0) - Popular open-source library
- **NHapi** (v3.2.0) - Enterprise-grade HL7 library

### Test Messages
- ? Simple ADT (admission)
- ? ADT with allergies/diagnoses
- ? Medication orders
- ? Complex multi-component orders
- ? Immunization records
- ? Large messages with all segments
- ? Minimal messages (MSH only)

## ?? Expected Results

Based on our optimizations, you should see:

### Speed
```
Method                          Mean      Ratio
KeryxPars - Simple ADT          12 ?s     1.00x (baseline)
HL7-dotnetcore - Simple ADT     45 ?s     3.75x slower
NHapi - Simple ADT              78 ?s     6.50x slower
```

### Memory
```
Method                          Allocated   Ratio
KeryxPars - Simple ADT          2.5 KB      1.00x (baseline)
HL7-dotnetcore - Simple ADT     15 KB       6.00x more
NHapi - Simple ADT              32 KB       12.8x more
```

## ?? Specific Benchmarks

### 1. Memory Efficiency
```bash
dotnet run -c Release --filter *Memory*
```
Shows lazy initialization benefits - unused segments aren't allocated.

### 2. Deserialization Performance
```bash
dotnet run -c Release --filter *Deserialization*
```
Tests parsing with different options and configurations.

### 3. Serialization Performance
```bash
dotnet run -c Release --filter *Serialization*
```
Tests message writing and roundtrip scenarios.

### 4. All Comparisons
```bash
dotnet run -c Release --filter *Comparison*
```
Runs all parser comparison tests.

## ?? Reading Results

### Console Output
```
| Method              | Mean     | Error    | Allocated |
|-------------------- |---------:|---------:|----------:|
| KeryxPars_Simple    | 12.34 ?s | 0.23 ?s  |   2.45 KB |
| HL7DotNet_Simple    | 45.67 ?s | 0.89 ?s  |  15.20 KB |
| NHapi_Simple        | 78.90 ?s | 1.45 ?s  |  32.10 KB |
```

**Key metrics:**
- **Mean**: Average execution time (lower is better)
- **Error**: Standard deviation (lower is more consistent)
- **Allocated**: Memory used (lower is better)

### Result Files
After running, check `BenchmarkDotNet.Artifacts/results/`:
- `*.html` - Interactive visualization
- `*.json` - Raw data for further analysis
- `*.md` - Markdown summary

## ?? Adding Your Own Parser

1. **Add NuGet package** to `KeryxPars.Benchmarks.csproj`:
```xml
<PackageReference Include="MyHL7Parser" Version="1.0.0" />
```

2. **Create adapter** in `Parsers/` folder:
```csharp
public class MyParserAdapter : IHL7ParserAdapter
{
    public string Name => "MyParser";
    public string Version => "1.0.0";

    public object Parse(string message)
    {
        var parser = new MyParser();
        return parser.Parse(message);
    }

    public string Serialize(object message)
    {
        // Your serialization code
        return serialized;
    }
}
```

3. **Add benchmark method** in `GenericParserComparison.cs`:
```csharp
[Benchmark(Description = "MyParser")]
public object ParseWithMyParser()
{
    return _parsers[3].Parse(_testMessage); // Index depends on order
}
```

4. **Run the benchmark**:
```bash
dotnet run -c Release --filter *GenericParserComparison*
```

## ?? Tips for Accurate Results

### DO:
? Run in **Release** mode (`-c Release`)
? Close unnecessary applications
? Run on AC power (laptops)
? Let warmup complete
? Run multiple times to verify consistency

### DON'T:
? Run in Debug mode
? Run while system is under load
? Interrupt the benchmark
? Compare results from different machines
? Use results from first run (warmup needed)

## ?? Troubleshooting

### "Benchmark takes too long"
- Use `--filter` to run specific tests
- Reduce iterations (not recommended for final results)

### "Out of memory"
- Run one category at a time
- Close other applications

### "Results seem wrong"
- Ensure Release build: `dotnet run -c Release`
- Check if antivirus is scanning
- Disable CPU frequency scaling
- Run as administrator (Windows) for accurate timings

## ?? Further Reading

- [BenchmarkDotNet Documentation](https://benchmarkdotnet.org/)
- [Understanding Memory Diagnoser](https://benchmarkdotnet.org/articles/configs/diagnosers.html)
- [Best Practices](https://benchmarkdotnet.org/articles/guides/good-practices.html)

## ?? Common Scenarios

### Compare against specific parser
```bash
dotnet run -c Release --filter *NHapi*
```

### Test specific message type
```bash
dotnet run -c Release --filter *Medication*
```

### Export for reporting
```bash
dotnet run -c Release --exporters json html markdown
```

### CI/CD Integration
```bash
dotnet run -c Release --filter *ParserComparison* --exporters json
# Parse JSON results in your pipeline
```

---

**Need help?** Check the full [README.md](README.md) for detailed documentation.
