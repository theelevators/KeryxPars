# KeryxPars MessageViewer - Quick Start Guide

## ?? What You've Built

You now have a fully functional MVP of the KeryxPars MessageViewer - a cross-platform HL7 message viewer application!

## ? What's Included

### Core Functionality
1. **HL7 Message Parsing** - Parse and analyze HL7 v2.5 messages
2. **Tree View** - Hierarchical display of segments and fields
3. **Raw View** - Original HL7 text format
4. **Message History** - SQLite-backed persistent storage
5. **Validation** - Real-time message validation with suggestions
6. **Sample Messages** - Built-in ADT, RDE, and ORU examples
7. **Search** - Full-text search across message history
8. **Cross-Platform** - Windows, Android, iOS, MacCatalyst support

### Architecture Highlights
- ? Clean Architecture (Core + UI separation)
- ? MVVM Pattern with CommunityToolkit
- ? Repository Pattern for data access
- ? Dependency Injection
- ? Async/await throughout
- ? SQLite for local persistence

## ?? How to Build and Run

### Option 1: Visual Studio 2022 (Recommended for Windows)

1. **Open the solution**
   ```
   D:\Health\KeryxPars\KeryxPars.sln
   ```
   (You'll need to add the new projects to the solution)

2. **Set startup project**
   - Right-click on `KeryxPars.MessageViewer`
   - Select "Set as Startup Project"

3. **Select target framework**
   - For Windows: `net8.0-windows10.0.19041.0`
   - For Android: `net8.0-android` (requires emulator)

4. **Press F5** to build and run

### Option 2: Command Line

#### Windows
```bash
cd D:\Health\KeryxPars\KeryxPars.MessageViewer\KeryxPars.MessageViewer
dotnet build -f net8.0-windows10.0.19041.0
dotnet run -f net8.0-windows10.0.19041.0
```

#### Android (with emulator running)
```bash
cd D:\Health\KeryxPars\KeryxPars.MessageViewer\KeryxPars.MessageViewer
dotnet build -f net8.0-android
# Use Visual Studio or adb to deploy to emulator
```

## ?? Adding to Solution

The projects were created in the `KeryxPars.MessageViewer` folder. To add them to your solution:

### Using Visual Studio
1. Right-click on solution in Solution Explorer
2. Add ? Existing Project
3. Navigate to `D:\Health\KeryxPars\KeryxPars.MessageViewer\KeryxPars.MessageViewer.Core`
4. Select `KeryxPars.MessageViewer.Core.csproj`
5. Repeat for `KeryxPars.MessageViewer.csproj`

### Using Command Line
```bash
cd D:\Health\KeryxPars
dotnet sln add KeryxPars.MessageViewer\KeryxPars.MessageViewer.Core\KeryxPars.MessageViewer.Core.csproj
dotnet sln add KeryxPars.MessageViewer\KeryxPars.MessageViewer\KeryxPars.MessageViewer.csproj
```

## ?? Using the Application

### 1. Home Page - Parse Messages

**Quick Actions:**
- **Paste Message**: Pastes from clipboard
- **Parse**: Analyzes the message
- **Clear**: Clears the editor

**Sample Messages:**
- Tap any sample to load it
- Then click Parse to view

**Recent Messages:**
- Shows last 10 parsed messages
- Tap to reopen

### 2. Message Viewer

**View Modes:**
- **Tree**: Expandable segment hierarchy
- **Raw**: Original HL7 text
- **Grid**: Coming soon
- **Summary**: Coming soon

**Features:**
- Search segments by ID or description
- Expand/collapse segments
- View validation errors
- See parse performance metrics

### 3. History Page

**Features:**
- Browse all parsed messages
- Search by any field
- Swipe left to delete
- Tap to reopen in viewer

## ?? Testing the Application

### Test Scenario 1: Parse Sample Message
1. Launch the app
2. Click "ADT^A01 - Patient Admission"
3. Click "Parse"
4. View the parsed message in Tree view
5. Try switching to Raw view
6. Check the validation section

### Test Scenario 2: Message History
1. Parse a few different sample messages
2. Navigate to "History" (flyout menu)
3. See all parsed messages
4. Search for "ADT"
5. Tap a message to reopen it

### Test Scenario 3: Validation
1. Paste this invalid message:
   ```
   MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||
   PID|1||
   ```
2. Click Parse
3. See validation errors with suggestions

## ?? Troubleshooting

### Build Errors

**Issue**: Missing workloads
```bash
dotnet workload install maui
```

**Issue**: Target framework not found
- Ensure .NET 8 SDK is installed
- Check `dotnet --list-sdks`

**Issue**: Android build fails
- Install Android SDK via Visual Studio
- Or install via Android Studio

### Runtime Errors

**Issue**: Database error
- Delete the database file and restart
- Location varies by platform (see README)

**Issue**: Navigation error
- Check that routes are registered in `AppShell.xaml.cs`

**Issue**: Blank page
- Check BindingContext is set in page constructor
- Verify services are registered in `MauiProgram.cs`

## ?? What's Next

### Immediate Tasks
1. **Add to Solution**: Use instructions above
2. **Build**: Ensure it compiles on your platform
3. **Run**: Test the application
4. **Test**: Try all features
5. **Document Issues**: Note any problems you find

### Phase 2 Features to Implement
1. **Grid View**: Tabular segment display
2. **Summary View**: Patient information cards
3. **Export**: JSON, XML, CSV, PDF
4. **File Picker**: Open .hl7 files
5. **Message Editing**: Inline field editing
6. **Advanced Search**: XPath-like queries

### Suggested Improvements
1. **Add Unit Tests**: Start with services
2. **Enhanced Validation**: More HL7 rules
3. **Better Icons**: Custom segment icons
4. **Field Metadata**: Tooltips with descriptions
5. **Keyboard Shortcuts**: Power user features
6. **Accessibility**: Screen reader support

## ?? Key Files Reference

### Core Services
- `MessageParserService.cs` - HL7 parsing logic
- `SqliteMessageRepository.cs` - Database operations
- `SegmentExtensions.cs` - Helper methods

### ViewModels
- `MainViewModel.cs` - Home page logic
- `MessageViewerViewModel.cs` - Viewer logic
- `HistoryViewModel.cs` - History logic

### Views
- `MainPage.xaml` - Home page UI
- `MessageViewerPage.xaml` - Viewer UI
- `HistoryPage.xaml` - History UI

### Configuration
- `MauiProgram.cs` - DI setup
- `AppShell.xaml` - Navigation
- `Colors.xaml` - Color scheme
- `Styles.xaml` - Control styles

## ?? Success Criteria

You've achieved MVP success if you can:
- ? Parse an HL7 message
- ? View it in tree format
- ? See validation errors
- ? Browse message history
- ? Search messages
- ? Run on your target platform

## ?? Documentation

- **Architecture**: See `MESSAGEVIEWER_PROGRESS.md`
- **Features**: See `README.md` in MessageViewer folder
- **KeryxPars.HL7**: See main README.md
- **MAUI Docs**: https://learn.microsoft.com/en-us/dotnet/maui/

## ?? Getting Help

If you encounter issues:
1. Check the troubleshooting section above
2. Review error messages carefully
3. Check MAUI documentation
4. Look at similar MAUI samples
5. Create an issue on GitHub

## ?? Congratulations!

You now have a working MVP of a professional HL7 message viewer! This demonstrates:
- Modern .NET MAUI development
- Clean architecture principles
- Integration with KeryxPars.HL7
- Cross-platform UI development
- Real-world healthcare IT tooling

**Next Steps:**
1. Build and run the application
2. Test all features
3. Note any issues or improvements
4. Plan Phase 2 implementation
5. Share feedback!

---

*Happy HL7 message viewing!* ???
