# KeryxPars MessageViewer

A modern, professional HL7 v2.x message viewer and medical data review tool built with Blazor and .NET.

## ?? Overview

KeryxPars MessageViewer is a comprehensive tool for reviewing and analyzing HL7 v2.x messages in a clinically meaningful way. Built on top of the high-performance KeryxPars.HL7 parsing library, it provides healthcare IT professionals and developers with an intuitive, efficient interface for working with HL7 medical data.

**This is not just a parsing demonstration** - it's a practical tool designed for real-world medical data review workflows.

## ? Features

### Current Features

- **Comprehensive Segment Support**
  - Displays ALL segment types from any HL7 message
  - Supports 40+ segment types (MSH, PID, PV1, OBR, OBX, RXE, NK1, GT1, SPM, etc.)
  - Automatically captures and displays segments not in standard message types
  - Custom `HL7ComprehensiveMessage` model for maximum compatibility

- **Patient-Focused Medical Data Review**
  - Dedicated Patient Summary view with clinical context
  - Patient demographics and identifiers
  - Visit and admission information
  - Allergies with severity indicators
  - Diagnoses with coding information
  - Medication and lab orders
  - Insurance details

- **Multiple View Modes**
  - **Patient Summary**: Clinical view optimized for medical data review
  - **Overview**: Message statistics and header information
  - **Segments**: Clean segment-by-segment display with descriptions
  - **Details**: Hierarchical field-level view with HL7 field names
  - **Validation**: Real-time compliance checking

- **High Performance**
  - Zero-reflection implementation for fast rendering
  - Leverages native KeryxPars.HL7 APIs
  - Optimized for large messages
  - Real-time parsing with performance metrics

- **Message Parsing**
  - Fast, real-time HL7 message parsing
  - Performance metrics and diagnostics
  - Comprehensive error reporting
  - Support for all KeryxPars message types and segments

- **Validation**
  - Real-time message validation
  - Severity-based error reporting (Critical, Error, Warning, Info)
  - Actionable suggestions for fixes
  - HL7 specification compliance checks

- **Field-Level Detail**
  - Proper HL7 field descriptions for 15+ segment types
  - Field index references (e.g., "MSH.10 - Message Control ID")
  - Toggle empty fields visibility
  - Expand/collapse all segments
  - Search across all fields

- **User Experience**
  - Clean, modern UI optimized for medical data
  - Responsive layouts for all screen sizes
  - Intuitive navigation
  - Copy/paste support
  - Sample messages for testing

### Cross-Platform Support

- ? Web (Blazor WebAssembly)
- ? Windows
- ? Linux
- ? macOS

## ?? Getting Started

### Prerequisites

- .NET 8.0 SDK or later (or .NET 10 for latest features)
- Modern web browser (Chrome, Firefox, Edge, Safari)
- Visual Studio 2022, VS Code, or JetBrains Rider (optional)

### Running the Application

#### Web Version (Recommended)

1. **Clone the repository**
   ```bash
   git clone https://github.com/theelevators/KeryxPars.git
   cd KeryxPars/KeryxPars.MessageViewer/KeryxPars.MessageViewer.Client
   ```

2. **Run the application**
   ```bash
   dotnet run
   ```

3. **Open in browser**
   Navigate to the URL shown in the console (typically `https://localhost:5001` or `http://localhost:5000`)

### Building from Source

```bash
cd KeryxPars/KeryxPars.MessageViewer/KeryxPars.MessageViewer.Client
dotnet build
dotnet publish -c Release
```

## ?? Usage

### Parsing a Message

1. **Launch the application** in your web browser
2. **Paste your HL7 message** in the text editor
3. **Click "Parse"** to analyze the message
4. **Review results** in the Patient Summary tab

### Using Sample Messages

1. **Click "Load Sample"** button
2. The message will be loaded into the editor
3. **Click "Parse"** to view it

### Understanding the Views

#### Patient Summary Tab
- **Purpose**: Clinical review of medical data
- **Best for**: Understanding patient context, allergies, diagnoses, orders
- **Shows**: Patient demographics, visit info, allergies, diagnoses, orders, insurance

#### Overview Tab
- **Purpose**: Message-level statistics and metadata
- **Best for**: Quick message validation and header inspection
- **Shows**: Message type, version, segment counts, header details

#### Segments Tab
- **Purpose**: Segment-by-segment review with descriptions
- **Best for**: Reviewing individual segments with their raw data
- **Shows**: Each segment with field-level breakdown and descriptions

#### Details Tab
- **Purpose**: Complete field-level detail with HL7 naming
- **Best for**: Deep dive into specific fields, debugging
- **Shows**: Hierarchical tree view with proper HL7 field names (e.g., MSH.9, PID.5)

#### Validation Tab
- **Purpose**: HL7 compliance checking
- **Best for**: Finding message issues and errors
- **Shows**: Validation errors with severity levels and suggestions

## ??? Architecture

### Project Structure

```
KeryxPars.MessageViewer/
??? KeryxPars.MessageViewer.Core/     # Business logic & services
?   ??? Interfaces/                   # Service abstractions
?   ??? Models/                       # Data models
?   ??? Services/                     # Implementation
?   ?   ??? MessageParserService      # HL7 parsing
?   ?   ??? SqliteMessageRepository   # Data persistence
?   ??? Extensions/                   # Helper extensions
?
??? KeryxPars.MessageViewer.Client/   # Blazor WebAssembly UI
    ??? Pages/                        # Main pages
    ?   ??? MessageViewer.razor       # Primary viewer page
    ??? Components/                   # Reusable components
    ?   ??? PatientSummary.razor      # Patient-focused view
    ?   ??? MessageOverview.razor     # Statistics view
    ?   ??? SegmentList.razor         # Segment display
    ?   ??? MessageDetails.razor      # Field details
    ?   ??? ValidationResults.razor   # Validation display
    ??? Extensions/                   # Client extensions
    ??? Services/                     # Client services
```

### Design Principles

- **Zero Reflection**: Uses native KeryxPars.HL7 APIs (`GetValues()`, `GetField()`) instead of reflection
- **Performance First**: Optimized for fast rendering of large messages
- **Medical Data Focus**: Designed for clinical review workflows, not just technical parsing
- **Clean Separation**: Core business logic separated from UI concerns
- **Type Safety**: Leverages strong typing from KeryxPars.HL7

### Technologies Used

- **Blazor WebAssembly** - Modern web UI framework
- **KeryxPars.HL7** - High-performance HL7 parsing library
- **.NET 8/10** - Latest .NET platform
- **Bootstrap Icons** - Icon library

## ?? Performance

### Benchmarks

- **Parse Time**: <10ms for typical messages (via KeryxPars.HL7)
- **UI Render**: <50ms for patient summary view
- **Field Display**: Zero reflection overhead
- **Memory**: Efficient - reuses parsed data structures

### Optimization Features

- Native API usage (no reflection)
- Efficient HL7 field access via `GetValues()` and `GetField()`
- Lazy rendering of collapsed segments
- Direct access to segment data
- Minimal object allocations

## ??? Roadmap

### ? Completed (Current Release)
- [x] Patient-focused medical data review
- [x] Proper HL7 field descriptions
- [x] Zero-reflection implementation
- [x] Multiple view modes
- [x] Real-time validation
- [x] Segment-level detail views

### Phase 2 (Planned)
- [ ] Export to JSON, XML, CSV, PDF
- [ ] File picker for opening .hl7 files
- [ ] Batch import from folders
- [ ] Message comparison (diff view)
- [ ] Print-friendly views
- [ ] Message history/library

### Phase 3 (Future)
- [ ] Message editing capabilities
- [ ] Network capture (MLLP support)
- [ ] Message templates
- [ ] Custom Z-segment support
- [ ] Visual diagram view
- [ ] FHIR conversion

### Phase 4 (Vision)
- [ ] Cloud synchronization
- [ ] Team collaboration
- [ ] Advanced analytics
- [ ] Plugin system
- [ ] AI-powered validation

## ?? Testing

### Current Status
- ? Zero-reflection implementation verified
- ? Native API integration confirmed
- ? Unit tests pending
- ? Integration tests pending

### Manual Testing
```bash
dotnet run
# Test with sample messages
# Verify all view modes work correctly
```

## ?? Contributing

Contributions are welcome! Please see the main [KeryxPars Contributing Guide](../CONTRIBUTING.md) for details.

### Areas for Contribution
- Additional field descriptions for more segment types
- Export format implementations
- UI/UX improvements
- Additional validation rules
- Documentation
- Testing
- Accessibility enhancements

### Development Notes
- **No Reflection**: Always use `ISegment.GetValues()` or `ISegment.GetField(index)` instead of reflection
- **Performance**: Keep rendering fast and efficient
- **Medical Context**: Features should be useful for actual medical data review
- **Type Safety**: Leverage KeryxPars.HL7's strong typing

## ?? License

This project is licensed under the Apache License 2.0 - see the [LICENSE](../LICENSE) file for details.

## ?? Acknowledgments

- Built on **KeryxPars.HL7** - High-performance, zero-allocation HL7 parsing library
- **HL7 International** - For the HL7 v2.x specification
- **Blazor Team** - For the amazing web framework
- Healthcare IT community for feedback and testing

## ?? Support

- **Issues**: [GitHub Issues](https://github.com/theelevators/KeryxPars/issues)
- **Discussions**: [GitHub Discussions](https://github.com/theelevators/KeryxPars/discussions)
- **Documentation**: See [Docs](../Docs/) folder

---

**Built with ?? for healthcare IT professionals**

*Professional medical data review made simple, fast, and beautiful.*
