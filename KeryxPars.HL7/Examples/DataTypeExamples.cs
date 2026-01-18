using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Examples;

/// <summary>
/// Examples demonstrating how to use the new HL7 2.5 primitive data types.
/// </summary>
public static class DataTypeExamples
{
    /// <summary>
    /// Example 1: Working with String (ST) data type
    /// </summary>
    public static void StringDataTypeExample()
    {
        // Create from string
        ST patientName = "JOHN DOE";
        
        // Parse from HL7 string
        var st = new ST();
        st.Parse("SAMPLE TEXT".AsSpan(), HL7Delimiters.Default);
        
        // Validate
        if (st.Validate(out var errors))
        {
            Console.WriteLine($"Valid string: {st.Value}");
        }
        
        // Convert to HL7 format
        string hl7String = st.ToHL7String(HL7Delimiters.Default);
        
        // Implicit conversion
        string nativeString = st; // Implicit to string
        ST fromNative = "Another string"; // Implicit from string
    }

    /// <summary>
    /// Example 2: Working with Date (DT) data type
    /// </summary>
    public static void DateDataTypeExample()
    {
        // Create from string
        DT admitDate = "20230115";
        
        // Create from DateTime
        DT todayDate = DateTime.Today;
        
        // Create from DateOnly
        DT specificDate = new DateOnly(2023, 1, 15);
        
        // Parse from HL7
        var dt = new DT();
        dt.Parse("20230115".AsSpan(), HL7Delimiters.Default);
        
        // Convert to DateTime
        DateTime? dateTime = dt.ToDateTime();
        if (dateTime.HasValue)
        {
            Console.WriteLine($"Parsed date: {dateTime.Value:yyyy-MM-dd}");
        }
        
        // Convert to DateOnly
        DateOnly? dateOnly = dt.ToDateOnly();
        
        // Validate
        if (dt.Validate(out var errors))
        {
            Console.WriteLine($"Valid date: {dt.Value}");
        }
        else
        {
            Console.WriteLine($"Invalid date: {string.Join(", ", errors)}");
        }
        
        // Partial dates are supported
        DT yearOnly = "2023";
        DT yearMonth = "202301";
        DT fullDate = "20230115";
    }

    /// <summary>
    /// Example 3: Working with Numeric (NM) data type
    /// </summary>
    public static void NumericDataTypeExample()
    {
        // Create from various numeric types
        NM fromDecimal = 123.45m;
        NM fromDouble = 67.89;
        NM fromString = "99.99";
        
        // Parse from HL7
        var nm = new NM();
        nm.Parse("500.75".AsSpan(), HL7Delimiters.Default);
        
        // Convert to different numeric types
        decimal? decimalValue = nm.ToDecimal();
        double? doubleValue = nm.ToDouble();
        int? intValue = nm.ToInt32(); // Returns null if not a whole number
        
        if (decimalValue.HasValue)
        {
            Console.WriteLine($"Decimal value: {decimalValue.Value}");
        }
        
        // Validate
        if (nm.Validate(out var errors))
        {
            Console.WriteLine($"Valid number: {nm.Value}");
        }
        else
        {
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation error: {error}");
            }
        }
        
        // Negative numbers are supported
        NM negativeNum = -123.45m;
    }

    /// <summary>
    /// Example 4: Working with DateTime (DTM) data type
    /// </summary>
    public static void DateTimeDataTypeExample()
    {
        // Create from string
        DTM timestamp = "20230115143000";
        
        // Create from DateTime
        DTM now = DateTime.Now;
        
        // Parse from HL7 (with timezone)
        var dtm = new DTM();
        dtm.Parse("20230115143000-0500".AsSpan(), HL7Delimiters.Default);
        
        // Convert to DateTime
        DateTime? dateTime = dtm.ToDateTime();
        if (dateTime.HasValue)
        {
            Console.WriteLine($"Parsed datetime: {dateTime.Value}");
        }
        
        // Extract timezone offset
        TimeSpan? timezone = dtm.GetTimezoneOffset();
        if (timezone.HasValue)
        {
            Console.WriteLine($"Timezone offset: {timezone.Value.TotalHours} hours");
        }
        
        // Validate
        if (dtm.Validate(out var errors))
        {
            Console.WriteLine($"Valid datetime: {dtm.Value}");
        }
        
        // Flexible precision
        DTM yearOnly = "2023";
        DTM yearMonthDay = "20230115";
        DTM fullDateTime = "20230115143000";
        DTM withFractionalSeconds = "20230115143000.1234";
        DTM withTimezone = "20230115143000-0500";
    }

    /// <summary>
    /// Example 5: Working with Sequence ID (SI) data type
    /// </summary>
    public static void SequenceIdExample()
    {
        // Create from int
        SI setId = 1;
        
        // Create from string
        SI fromString = "5";
        
        // Parse from HL7
        var si = new SI();
        si.Parse("3".AsSpan(), HL7Delimiters.Default);
        
        // Convert to int
        int? intValue = si.ToInt32();
        if (intValue.HasValue)
        {
            Console.WriteLine($"Set ID: {intValue.Value}");
        }
        
        // Validate (must be non-negative)
        if (si.Validate(out var errors))
        {
            Console.WriteLine($"Valid sequence ID: {si.Value}");
        }
        else
        {
            Console.WriteLine($"Invalid sequence ID: {string.Join(", ", errors)}");
        }
    }

    /// <summary>
    /// Example 6: Working with Coded Values (ID and IS)
    /// </summary>
    public static void CodedValueExample()
    {
        // ID - for HL7-defined tables
        ID administrativeSex = "M"; // Male
        ID patientClass = "I"; // Inpatient
        
        // IS - for user-defined tables
        IS customCode = "STAT"; // User-defined priority code
        
        // Both validate length constraints
        if (administrativeSex.Validate(out var errors))
        {
            Console.WriteLine($"Valid coded value: {administrativeSex.Value}");
        }
    }

    /// <summary>
    /// Example 7: Parsing a complete HL7 field with validation
    /// </summary>
    public static void CompleteFieldExample()
    {
        // Example: Parse a date of birth from PID segment
        string hl7DateOfBirth = "19800515";
        
        var dob = new DT();
        dob.Parse(hl7DateOfBirth.AsSpan(), HL7Delimiters.Default);
        
        // Validate the parsed value
        if (dob.Validate(out var errors))
        {
            // Convert to DateTime for use in application
            var birthDate = dob.ToDateTime();
            if (birthDate.HasValue)
            {
                // Calculate age
                var age = DateTime.Today.Year - birthDate.Value.Year;
                if (birthDate.Value > DateTime.Today.AddYears(-age)) age--;
                
                Console.WriteLine($"Patient DOB: {birthDate.Value:yyyy-MM-dd}");
                Console.WriteLine($"Patient Age: {age}");
            }
        }
        else
        {
            Console.WriteLine($"Invalid date of birth: {string.Join(", ", errors)}");
        }
    }

    /// <summary>
    /// Example 8: Using data types in segment parsing
    /// </summary>
    public static void SegmentParsingExample()
    {
        // Example: Parse fields from a PID segment
        // PID|1||12345||DOE^JOHN||19800515|M
        
        var setId = new SI();
        setId.Parse("1".AsSpan(), HL7Delimiters.Default);
        
        var patientId = new ST();
        patientId.Parse("12345".AsSpan(), HL7Delimiters.Default);
        
        var dateOfBirth = new DT();
        dateOfBirth.Parse("19800515".AsSpan(), HL7Delimiters.Default);
        
        var administrativeSex = new ID();
        administrativeSex.Parse("M".AsSpan(), HL7Delimiters.Default);
        
        // All fields are now properly typed and validated
        Console.WriteLine($"Set ID: {setId.ToInt32()}");
        Console.WriteLine($"Patient ID: {patientId.Value}");
        Console.WriteLine($"DOB: {dateOfBirth.ToDateTime():yyyy-MM-dd}");
        Console.WriteLine($"Sex: {administrativeSex.Value}");
    }

    /// <summary>
    /// Example 9: Error handling and validation
    /// </summary>
    public static void ValidationExample()
    {
        // Example: Validate various data types
        var invalidDate = new DT("2023-01-15"); // Wrong format
        var invalidNumber = new NM("abc"); // Not a number
        var tooLongString = new ST(new string('X', 200)); // Exceeds max length
        
        // Check validation results
        if (!invalidDate.Validate(out var dateErrors))
        {
            Console.WriteLine("Date validation errors:");
            foreach (var error in dateErrors)
            {
                Console.WriteLine($"  - {error}");
            }
        }
        
        if (!invalidNumber.Validate(out var numErrors))
        {
            Console.WriteLine("Number validation errors:");
            foreach (var error in numErrors)
            {
                Console.WriteLine($"  - {error}");
            }
        }
        
        if (!tooLongString.Validate(out var strErrors))
        {
            Console.WriteLine("String validation errors:");
            foreach (var error in strErrors)
            {
                Console.WriteLine($"  - {error}");
            }
        }
    }

    /// <summary>
    /// Example 10: Building HL7 output
    /// </summary>
    public static void BuildingHL7Example()
    {
        var delimiters = HL7Delimiters.Default;
        
        // Create typed data
        SI setId = 1;
        DT admitDate = DateTime.Today;
        NM temperature = 98.6;
        ID sex = "F";
        
        // Convert to HL7 format
        var hl7SetId = setId.ToHL7String(delimiters);
        var hl7AdmitDate = admitDate.ToHL7String(delimiters);
        var hl7Temperature = temperature.ToHL7String(delimiters);
        var hl7Sex = sex.ToHL7String(delimiters);
        
        // Build segment
        var segment = $"PID|{hl7SetId}||12345||DOE^JANE||{hl7AdmitDate}|{hl7Sex}";
        Console.WriteLine($"Generated segment: {segment}");
    }
}
