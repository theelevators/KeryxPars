@echo off
REM KeryxPars Benchmarks Runner Script (Windows)
REM Usage: run-benchmarks.bat [category]

echo ============================================================
echo            KeryxPars HL7 Parser Benchmarks
echo ============================================================
echo.

if "%1"=="" (
    echo Running ALL benchmarks...
    echo.
    dotnet run -c Release --exporters json html markdown
) else (
    echo Running benchmarks: %1
    echo.
    dotnet run -c Release --filter "*%1*" --exporters json html markdown
)

echo.
echo ============================================================
echo                  Benchmark Complete!
echo ============================================================
echo.
echo Results saved to: BenchmarkDotNet.Artifacts\
echo.
echo Available categories:
echo   - ParserComparison   : Compare against third-party parsers
echo   - Memory             : Memory allocation tests
echo   - Deserialization    : Parse performance tests
echo   - Serialization      : Write performance tests
echo.
echo Example: run-benchmarks.bat ParserComparison
pause
