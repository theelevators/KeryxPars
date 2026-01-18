using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace Shouldly;

/// <summary>
/// Shouldly extension methods for HL7 datatypes to enable test assertions.
/// This allows .ShouldBe() to work directly with HL7 datatypes.
/// </summary>
public static class HL7DataTypeShouldlyExtensions
{
    #region Primitive Type Extensions

    /// <summary>
    /// Asserts that an ST datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this ST actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a DTM datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this DTM actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a DT datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this DT actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a TM datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this TM actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an ID datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this ID actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an IS datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this IS actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an NM datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this NM actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an SI datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this SI actual, string? expected, string? customMessage = null)
    {
        actual.Value.ShouldBe(expected, customMessage);
    }

    #endregion

    #region Composite Type Extensions

    /// <summary>
    /// Asserts that an HD datatype should equal the specified string.
    /// Compares using NamespaceId for simple cases or full HL7 string.
    /// </summary>
    public static void ShouldBe(this HD actual, string? expected, string? customMessage = null)
    {
        // Try NamespaceId comparison first (most common case)
        if (actual.NamespaceId.Value == expected)
            return;
        
        // Fall back to full HL7 string comparison
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a CE datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this CE actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a CWE datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this CWE actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a CX datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this CX actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an EI datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this EI actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an XPN datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this XPN actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an XAD datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this XAD actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an XTN datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this XTN actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an XCN datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this XCN actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an XON datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this XON actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a PL datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this PL actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that a CQ datatype should equal the specified string.
    /// </summary>
    public static void ShouldBe(this CQ actual, string? expected, string? customMessage = null)
    {
        var delimiters = KeryxPars.HL7.Definitions.HL7Delimiters.Default;
        actual.ToHL7String(delimiters).ShouldBe(expected, customMessage);
    }

    #endregion

    #region Numeric Comparisons for NM

    /// <summary>
    /// Asserts that an NM datatype should equal the specified decimal.
    /// </summary>
    public static void ShouldBe(this NM actual, decimal expected, string? customMessage = null)
    {
        actual.ToDecimal().ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an NM datatype should equal the specified int.
    /// </summary>
    public static void ShouldBe(this NM actual, int expected, string? customMessage = null)
    {
        actual.ToInt32().ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Asserts that an NM datatype should equal the specified double.
    /// </summary>
    public static void ShouldBe(this NM actual, double expected, double tolerance = 0, string? customMessage = null)
    {
        var actualValue = actual.ToDouble();
        if (actualValue.HasValue)
        {
            actualValue.Value.ShouldBe(expected, tolerance, customMessage);
        }
        else
        {
            throw new ShouldAssertException($"NM value '{actual.Value}' could not be converted to double");
        }
    }

    #endregion
}
