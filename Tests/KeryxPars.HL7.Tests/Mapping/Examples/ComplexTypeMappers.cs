using System;
using System.Runtime.CompilerServices;
using KeryxPars.HL7.Mapping.Parsers;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Manual mapper for PersonName (temporary until source generator is enhanced).
/// </summary>
public static class PersonNameMapper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PersonName MapFromField(ReadOnlySpan<char> field)
    {
        var result = new PersonName();
        
        var lastName = HL7SpanParser.GetComponent(field, 1);
        if (!HL7SpanParser.IsNullOrWhiteSpace(lastName))
            result.LastName = lastName.ToString();
            
        var firstName = HL7SpanParser.GetComponent(field, 2);
        if (!HL7SpanParser.IsNullOrWhiteSpace(firstName))
            result.FirstName = firstName.ToString();
            
        var middleName = HL7SpanParser.GetComponent(field, 3);
        if (!HL7SpanParser.IsNullOrWhiteSpace(middleName))
            result.MiddleName = middleName.ToString();
            
        var suffix = HL7SpanParser.GetComponent(field, 4);
        if (!HL7SpanParser.IsNullOrWhiteSpace(suffix))
            result.Suffix = suffix.ToString();
            
        var prefix = HL7SpanParser.GetComponent(field, 5);
        if (!HL7SpanParser.IsNullOrWhiteSpace(prefix))
            result.Prefix = prefix.ToString();
            
        var degree = HL7SpanParser.GetComponent(field, 6);
        if (!HL7SpanParser.IsNullOrWhiteSpace(degree))
            result.Degree = degree.ToString();
        
        return result;
    }
}

public static class AddressMapper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Address MapFromField(ReadOnlySpan<char> field)
    {
        var result = new Address();
        
        var street = HL7SpanParser.GetComponent(field, 1);
        if (!HL7SpanParser.IsNullOrWhiteSpace(street))
            result.StreetAddress = street.ToString();
            
        var other = HL7SpanParser.GetComponent(field, 2);
        if (!HL7SpanParser.IsNullOrWhiteSpace(other))
            result.OtherDesignation = other.ToString();
            
        var city = HL7SpanParser.GetComponent(field, 3);
        if (!HL7SpanParser.IsNullOrWhiteSpace(city))
            result.City = city.ToString();
            
        var state = HL7SpanParser.GetComponent(field, 4);
        if (!HL7SpanParser.IsNullOrWhiteSpace(state))
            result.StateOrProvince = state.ToString();
            
        var zip = HL7SpanParser.GetComponent(field, 5);
        if (!HL7SpanParser.IsNullOrWhiteSpace(zip))
            result.ZipOrPostalCode = zip.ToString();
            
        var country = HL7SpanParser.GetComponent(field, 6);
        if (!HL7SpanParser.IsNullOrWhiteSpace(country))
            result.Country = country.ToString();
            
        var type = HL7SpanParser.GetComponent(field, 7);
        if (!HL7SpanParser.IsNullOrWhiteSpace(type))
            result.AddressType = type.ToString();
        
        return result;
    }
}

public static class PhoneNumberMapper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PhoneNumber MapFromField(ReadOnlySpan<char> field)
    {
        var result = new PhoneNumber();
        
        var formatted = HL7SpanParser.GetComponent(field, 1);
        if (!HL7SpanParser.IsNullOrWhiteSpace(formatted))
            result.PhoneNumberFormatted = formatted.ToString();
            
        var useCode = HL7SpanParser.GetComponent(field, 2);
        if (!HL7SpanParser.IsNullOrWhiteSpace(useCode))
            result.TelecommunicationUseCode = useCode.ToString();
            
        var equipType = HL7SpanParser.GetComponent(field, 3);
        if (!HL7SpanParser.IsNullOrWhiteSpace(equipType))
            result.TelecommunicationEquipmentType = equipType.ToString();
            
        var email = HL7SpanParser.GetComponent(field, 4);
        if (!HL7SpanParser.IsNullOrWhiteSpace(email))
            result.EmailAddress = email.ToString();
            
        var countryCode = HL7SpanParser.GetComponent(field, 5);
        if (!HL7SpanParser.IsNullOrWhiteSpace(countryCode))
            result.CountryCode = countryCode.ToString();
            
        var areaCode = HL7SpanParser.GetComponent(field, 6);
        if (!HL7SpanParser.IsNullOrWhiteSpace(areaCode))
            result.AreaCityCode = areaCode.ToString();
            
        var localNumber = HL7SpanParser.GetComponent(field, 7);
        if (!HL7SpanParser.IsNullOrWhiteSpace(localNumber))
            result.LocalNumber = localNumber.ToString();
        
        return result;
    }
}
