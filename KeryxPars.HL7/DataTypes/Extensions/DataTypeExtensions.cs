using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Extensions;

/// <summary>
/// Extension methods to make HL7 datatypes behave like primitives.
/// Enables seamless integration with existing code and test assertions.
/// </summary>
public static class DataTypeExtensions
{
    #region String Compatibility Extensions

    /// <summary>
    /// Enables string comparison operations on ST datatype.
    /// </summary>
    public static bool Equals(this ST st, string? other) => 
        st.Value == other;

    /// <summary>
    /// Enables string comparison operations on TX datatype.
    /// </summary>
    public static bool Equals(this TX tx, string? other) => 
        tx.Value == other;

    /// <summary>
    /// Enables string comparison operations on FT datatype.
    /// </summary>
    public static bool Equals(this FT ft, string? other) => 
        ft.Value == other;

    /// <summary>
    /// Enables string comparison operations on ID datatype.
    /// </summary>
    public static bool Equals(this ID id, string? other) => 
        id.Value == other;

    /// <summary>
    /// Enables string comparison operations on IS datatype.
    /// </summary>
    public static bool Equals(this IS isType, string? other) => 
        isType.Value == other;

    /// <summary>
    /// Enables string comparison operations on NM datatype.
    /// </summary>
    public static bool Equals(this NM nm, string? other) => 
        nm.Value == other;

    /// <summary>
    /// Enables string comparison operations on SI datatype.
    /// </summary>
    public static bool Equals(this SI si, string? other) => 
        si.Value == other;

    /// <summary>
    /// Enables string comparison operations on DT datatype.
    /// </summary>
    public static bool Equals(this DT dt, string? other) => 
        dt.Value == other;

    /// <summary>
    /// Enables string comparison operations on TM datatype.
    /// </summary>
    public static bool Equals(this TM tm, string? other) => 
        tm.Value == other;

    /// <summary>
    /// Enables string comparison operations on DTM datatype.
    /// </summary>
    public static bool Equals(this DTM dtm, string? other) => 
        dtm.Value == other;

    #endregion

    #region Composite Type Extensions

    /// <summary>
    /// Enables string comparison for HD (Hierarchic Designator) using NamespaceId.
    /// </summary>
    public static bool Equals(this HD hd, string? other)
    {
        // First try full HL7 string comparison
        var hl7String = hd.ToHL7String(HL7Delimiters.Default);
        if (hl7String == other) return true;
        
        // Fallback to NamespaceId comparison for simple cases
        return hd.NamespaceId.Value == other;
    }

    /// <summary>
    /// Enables string comparison for CE (Coded Element) using Text or Identifier.
    /// </summary>
    public static bool Equals(this CE ce, string? other)
    {
        var hl7String = ce.ToHL7String(HL7Delimiters.Default);
        if (hl7String == other) return true;
        
        // Try text then identifier
        return ce.Text.Value == other || ce.Identifier.Value == other;
    }

    /// <summary>
    /// Enables string comparison for CWE using Text or Identifier.
    /// </summary>
    public static bool Equals(this CWE cwe, string? other)
    {
        var hl7String = cwe.ToHL7String(HL7Delimiters.Default);
        if (hl7String == other) return true;
        
        return cwe.Text.Value == other || cwe.Identifier.Value == other;
    }

    /// <summary>
    /// Enables string comparison for CX using Id.
    /// </summary>
    public static bool Equals(this CX cx, string? other)
    {
        var hl7String = cx.ToHL7String(HL7Delimiters.Default);
        if (hl7String == other) return true;
        
        return cx.Id.Value == other;
    }

    /// <summary>
    /// Enables string comparison for EI using EntityIdentifier.
    /// </summary>
    public static bool Equals(this EI ei, string? other)
    {
        var hl7String = ei.ToHL7String(HL7Delimiters.Default);
        if (hl7String == other) return true;
        
        return ei.EntityIdentifier.Value == other;
    }

    /// <summary>
    /// Enables string comparison for XPN using HL7 string format.
    /// </summary>
    public static bool Equals(this XPN xpn, string? other)
    {
        var hl7String = xpn.ToHL7String(HL7Delimiters.Default);
        return hl7String == other;
    }

    /// <summary>
    /// Enables string comparison for XAD using HL7 string format.
    /// </summary>
    public static bool Equals(this XAD xad, string? other)
    {
        var hl7String = xad.ToHL7String(HL7Delimiters.Default);
        return hl7String == other;
    }

    /// <summary>
    /// Enables string comparison for XTN using HL7 string format.
    /// </summary>
    public static bool Equals(this XTN xtn, string? other)
    {
        var hl7String = xtn.ToHL7String(HL7Delimiters.Default);
        return hl7String == other;
    }

    /// <summary>
    /// Enables string comparison for XCN using HL7 string format.
    /// </summary>
    public static bool Equals(this XCN xcn, string? other)
    {
        var hl7String = xcn.ToHL7String(HL7Delimiters.Default);
        return hl7String == other;
    }

    /// <summary>
    /// Enables string comparison for XON using OrganizationName.
    /// </summary>
    public static bool Equals(this XON xon, string? other)
    {
        var hl7String = xon.ToHL7String(HL7Delimiters.Default);
        if (hl7String == other) return true;
        
        return xon.OrganizationName.Value == other;
    }

    /// <summary>
    /// Enables string comparison for PL using HL7 string format.
    /// </summary>
    public static bool Equals(this PL pl, string? other)
    {
        var hl7String = pl.ToHL7String(HL7Delimiters.Default);
        return hl7String == other;
    }

    /// <summary>
    /// Enables string comparison for CQ using HL7 string format.
    /// </summary>
    public static bool Equals(this CQ cq, string? other)
    {
        var hl7String = cq.ToHL7String(HL7Delimiters.Default);
        return hl7String == other;
    }

    #endregion

    #region Helper Methods for Testing

    /// <summary>
    /// Gets the underlying string value, useful for testing and debugging.
    /// </summary>
    public static string? GetValue(this ST st) => st.Value;

    /// <summary>
    /// Gets the underlying string value, useful for testing and debugging.
    /// </summary>
    public static string? GetValue(this DTM dtm) => dtm.Value;

    /// <summary>
    /// Gets the underlying string value, useful for testing and debugging.
    /// </summary>
    public static string? GetValue(this DT dt) => dt.Value;

    /// <summary>
    /// Gets the underlying string value, useful for testing and debugging.
    /// </summary>
    public static string? GetValue(this TM tm) => tm.Value;

    /// <summary>
    /// Gets the underlying string value, useful for testing and debugging.
    /// </summary>
    public static string? GetValue(this ID id) => id.Value;

    /// <summary>
    /// Gets the underlying string value, useful for testing and debugging.
    /// </summary>
    public static string? GetValue(this IS isType) => isType.Value;

    /// <summary>
    /// Gets the underlying string value, useful for testing and debugging.
    /// </summary>
    public static string? GetValue(this NM nm) => nm.Value;

    /// <summary>
    /// Gets the namespace ID from HD, useful for simple comparisons.
    /// </summary>
    public static string? GetNamespaceId(this HD hd) => hd.NamespaceId.Value;

    /// <summary>
    /// Gets the HL7 string representation of any datatype.
    /// </summary>
    public static string AsHL7String<T>(this T datatype, in HL7Delimiters delimiters) where T : IHL7DataType
    {
        return datatype.ToHL7String(delimiters);
    }

    #endregion

    #region Numeric Compatibility

    /// <summary>
    /// Enables numeric comparison operations on NM datatype.
    /// </summary>
    public static bool Equals(this NM nm, decimal other) => 
        nm.ToDecimal() == other;

    /// <summary>
    /// Enables numeric comparison operations on NM datatype.
    /// </summary>
    public static bool Equals(this NM nm, int other) => 
        nm.ToInt32() == other;

    /// <summary>
    /// Enables numeric comparison operations on NM datatype.
    /// </summary>
    public static bool Equals(this NM nm, double other) => 
        nm.ToDouble() == other;

    /// <summary>
    /// Enables numeric comparison operations on SI datatype.
    /// </summary>
    public static bool Equals(this SI si, int other) => 
        si.ToInt32() == other;

    #endregion

    #region Length and String Operations

    /// <summary>
    /// Gets the length of the underlying string value.
    /// </summary>
    public static int Length(this ST st) => st.Value?.Length ?? 0;

    /// <summary>
    /// Gets the length of the underlying string value.
    /// </summary>
    public static int Length(this DTM dtm) => dtm.Value?.Length ?? 0;

    /// <summary>
    /// Gets the length of the underlying string value.
    /// </summary>
    public static int Length(this DT dt) => dt.Value?.Length ?? 0;

    /// <summary>
    /// Gets the length of the HL7 string representation for HD.
    /// </summary>
    public static int Length(this HD hd) => hd.ToHL7String(HL7Delimiters.Default).Length;

    /// <summary>
    /// Checks if the datatype is null or empty.
    /// </summary>
    public static bool IsNullOrEmpty(this ST st) => 
        string.IsNullOrEmpty(st.Value);

    /// <summary>
    /// Checks if the datatype is null or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(this ST st) => 
        string.IsNullOrWhiteSpace(st.Value);

    #endregion
}
