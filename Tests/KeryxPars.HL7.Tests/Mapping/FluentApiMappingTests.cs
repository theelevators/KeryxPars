using System;
using System.Collections.Generic;
using KeryxPars.HL7.Mapping;
using KeryxPars.HL7.Mapping.Fluent;
using KeryxPars.HL7.Tests.TestInfrastructure;
using Shouldly;
using Xunit;
using static KeryxPars.HL7.Tests.TestInfrastructure.TestEnums;
using static KeryxPars.HL7.Tests.TestInfrastructure.TestMessages;
using static KeryxPars.HL7.Tests.TestInfrastructure.TestModels;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for FluentAPI mapping functionality.
/// Validates runtime configuration-based mapping as an alternative to attributes.
/// </summary>
public class FluentApiMappingTests
{
    #region Basic Fluent Mapping Tests

    [Fact]
    public void FluentMapper_ShouldMapSimpleStringProperties()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<SimplePatientProfile>();

        // Act
        var result = mapper.Map<SimplePatient>(BasicMalePatient);

        // Assert
        result.ShouldNotBeNull();
        result.PatientId.ShouldBe("PAT123456");
        result.LastName.ShouldBe("DOE");
        result.FirstName.ShouldBe("JOHN");
        result.MiddleName.ShouldBe("A");
    }

    [Fact]
    public void FluentMapper_ShouldHandleMissingOptionalFields()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<SimplePatientProfile>();

        // Act
        var result = mapper.Map<SimplePatient>(MinimalMessage);

        // Assert
        result.ShouldNotBeNull();
        result.PatientId.ShouldBeNull();
        result.LastName.ShouldBeNull();
        result.FirstName.ShouldBeNull();
    }

    [Fact]
    public void FluentMapper_ShouldApplyDefaultValues()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithDefaultsProfile>();

        // Act
        var result = mapper.Map<PatientWithDefaults>(MinimalMessage);

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe("ACTIVE"); // Default value
    }

    [Fact]
    public void FluentMapper_ShouldEnforceRequiredFields()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithRequiredProfile>();

        // Act & Assert
        var ex = Should.Throw<HL7MappingException>(() => 
            mapper.Map<PatientWithRequired>(MinimalMessage));
        
        ex.FieldPath.ShouldBe("PID.3");
        ex.Message.ShouldContain("Required field");
    }

    #endregion

    #region Type Conversion Tests

    [Fact]
    public void FluentMapper_ShouldConvertDateTimeWithFormat()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithDateTimeProfile>();

        // Act
        var result = mapper.Map<PatientWithDateTime>(BasicMalePatient);

        // Assert
        result.ShouldNotBeNull();
        result.DateOfBirth.ShouldNotBeNull();
        result.DateOfBirth.ShouldBe(new DateTime(1985, 6, 15));
        
        result.MessageDateTime.ShouldNotBeNull();
        result.MessageDateTime.ShouldBe(new DateTime(2023, 6, 15, 14, 30, 0));
    }

    [Fact]
    public void FluentMapper_ShouldConvertEnums()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithEnumsProfile>();

        // Act
        var result = mapper.Map<PatientWithEnums>(BasicMalePatient);

        // Assert
        result.ShouldNotBeNull();
        result.Gender.ShouldBe(Gender.M);
        result.MaritalStatus.ShouldBe(MaritalStatus.M);
    }

    [Fact]
    public void FluentMapper_ShouldConvertNullableEnums()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithEnumsProfile>();

        // Act
        var result = mapper.Map<PatientWithEnums>(BasicFemalePatient);

        // Assert
        result.ShouldNotBeNull();
        result.Gender.ShouldBe(Gender.F);
        result.MaritalStatus.ShouldBe(MaritalStatus.S);
    }

    [Fact]
    public void FluentMapper_ShouldHandleMissingDateTime()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithDateTimeProfile>();

        // Act  - Use PatientWithMissingFields which has no PID.7
        var result = mapper.Map<PatientWithDateTime>(PatientWithMissingFields);

        // Assert
        result.ShouldNotBeNull();
        result.DateOfBirth.ShouldBeNull();
    }

    #endregion

    #region Profile Registration Tests

    [Fact]
    public void FluentMapper_ShouldThrowWhenProfileNotRegistered()
    {
        // Arrange
        var mapper = new HL7FluentMapper();

        // Act & Assert
        var ex = Should.Throw<InvalidOperationException>(() => 
            mapper.Map<SimplePatient>(BasicMalePatient));
        
        ex.Message.ShouldContain("No profile registered");
        ex.Message.ShouldContain("SimplePatient");
    }

    [Fact]
    public void FluentMapper_ShouldAllowMultipleProfiles()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<SimplePatientProfile>();
        mapper.RegisterProfile<PatientWithDateTimeProfile>();

        // Act
        var result1 = mapper.Map<SimplePatient>(BasicMalePatient);
        var result2 = mapper.Map<PatientWithDateTime>(BasicMalePatient);

        // Assert
        result1.ShouldNotBeNull();
        result2.ShouldNotBeNull();
    }

    #endregion

    #region Complex Type Mapping Tests

    [Fact(Skip = "Complex type mapping requires string overload on generated mappers - v0.5.1 enhancement")]
    public void FluentMapper_ShouldMapComplexTypeUsingGeneratedMapper()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithAddressProfile>();

        // Act
        var result = mapper.Map<PatientWithAddress>(PatientWithCompleteAddress);

        // Assert
        result.ShouldNotBeNull();
        result.PatientId.ShouldBe("PAT111222");
        result.LastName.ShouldBe("WILSON");
        
        // Complex type should be mapped using TestAddressMapper from v0.4.0
        result.HomeAddress.ShouldNotBeNull();
        result.HomeAddress.Street.ShouldBe("123 MAIN ST");
        result.HomeAddress.City.ShouldBe("ANYTOWN");
        result.HomeAddress.State.ShouldBe("CA");
        result.HomeAddress.ZipCode.ShouldBe("12345");
    }

    [Fact]
    public void FluentMapper_ComplexTypesCurrentlyReturnNull()
    {
        // Arrange
        var mapper = new HL7FluentMapper();
        mapper.RegisterProfile<PatientWithAddressProfile>();

        // Act
        var result = mapper.Map<PatientWithAddress>(PatientWithCompleteAddress);

        // Assert - FluentAPI v0.5.0 doesn't support complex types yet
        // This is a known limitation - complex types work with attributes but not FluentAPI yet
        result.ShouldNotBeNull();
        result.HomeAddress.ShouldBeNull(); // Expected for v0.5.0 MVP
    }

    #endregion

    #region Builder Pattern Tests (NEW v0.5.2 - Result Pattern!)

    [Fact]
    public void Builder_InlineMappingWithParse_ShouldReturnResult()
    {
        // Arrange
        var builder = HL7FluentMapper.Create<SimplePatient>();
        builder.Map(x => x.PatientId).FromField("PID.3");
        builder.Map(x => x.LastName).FromField("PID.5.1");
        builder.Map(x => x.FirstName).FromField("PID.5.2");
        
        // Act - Returns Result!
        var result = builder.Parse(BasicMalePatient);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value!.PatientId.ShouldBe("PAT123456");
        result.Value.LastName.ShouldBe("DOE");
        result.Value.FirstName.ShouldBe("JOHN");
    }

    [Fact]
    public void Builder_WithDateTimeConversion_ShouldReturnResult()
    {
        // Arrange
        var builder = HL7FluentMapper.Create<PatientWithDateTime>();
        builder.Map(x => x.DateOfBirth).FromField("PID.7").AsDateTime("yyyyMMdd");
        builder.Map(x => x.MessageDateTime).FromField("MSH.7").AsDateTime("yyyyMMddHHmmss");
        
        // Act
        var result = builder.Parse(BasicMalePatient);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value!.DateOfBirth.ShouldBe(new DateTime(1985, 6, 15));
        result.Value.MessageDateTime.ShouldBe(new DateTime(2023, 6, 15, 14, 30, 0));
    }

    [Fact]
    public void Builder_WithEnumConversion_ShouldReturnResult()
    {
        // Arrange
        var builder = HL7FluentMapper.Create<PatientWithEnums>();
        builder.Map(x => x.Gender).FromField("PID.8").AsEnum<Gender>();
        builder.Map(x => x.MaritalStatus).FromField("PID.16").AsEnum<MaritalStatus>();
        
        // Act
        var result = builder.Parse(BasicMalePatient);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Gender.ShouldBe(Gender.M);
        result.Value.MaritalStatus.ShouldBe(MaritalStatus.M);
    }

    [Fact]
    public void Builder_Build_ShouldCreateReusableMapper()
    {
        // Arrange - Build once, use many times!
        var builder = HL7FluentMapper.Create<SimplePatient>();
        builder.Map(x => x.PatientId).FromField("PID.3");
        builder.Map(x => x.LastName).FromField("PID.5.1");
        builder.Map(x => x.FirstName).FromField("PID.5.2");
        var mapper = builder.Build();

        // Act - Use the mapper multiple times
        var result1 = mapper(BasicMalePatient);
        var result2 = mapper(BasicFemalePatient);

        // Assert
        result1.IsSuccess.ShouldBeTrue();
        result2.IsSuccess.ShouldBeTrue();
        result1.Value!.PatientId.ShouldBe("PAT123456");
        result2.Value!.PatientId.ShouldBe("PAT789012");
    }

    [Fact]
    public void Builder_WithDefaults_ShouldReturnResult()
    {
        // Arrange
        var builder = HL7FluentMapper.Create<PatientWithDefaults>();
        builder.Map(x => x.Status).FromField("PID.99").WithDefault("ACTIVE");
        
        // Act
        var result = builder.Parse(MinimalMessage);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Status.ShouldBe("ACTIVE");
    }

    [Fact]
    public void Builder_WithRequired_ShouldReturnErrorResult()
    {
        // Arrange
        var builder = HL7FluentMapper.Create<PatientWithRequired>();
        builder.Map(x => x.PatientId).FromField("PID.3").Required();
        
        // Act - Missing required field should return error!
        var result = builder.Parse(MinimalMessage);

        // Assert - No exception! Just Result with error
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error!.FieldPath.ShouldBe("PID.3");
        result.Error.Message.ShouldContain("Required");
    }

    [Fact]
    public void Builder_WithInvalidData_ReturnsSuccessWithNulls()
    {
        // Arrange - Message with missing PID segment
        var invalidMessage = "MSH|^~\\&|APP||";
        var builder = HL7FluentMapper.Create<SimplePatient>();
        builder.Map(x => x.PatientId).FromField("PID.3");
        
        // Act - Missing fields just return null (not an error)
        var result = builder.Parse(invalidMessage);

        // Assert - Success with null values (HL7 is forgiving)
        result.IsSuccess.ShouldBeTrue();
        result.Value!.PatientId.ShouldBeNull();
    }

    #endregion
}





#region Fluent Profiles

/// <summary>
/// Simple profile mapping basic string fields.
/// </summary>
public class SimplePatientProfile : HL7MappingProfile<SimplePatient>
{
    public SimplePatientProfile()
    {
        ForMessages("ADT^A01");

        Map(x => x.PatientId).FromField("PID.3");
        Map(x => x.LastName).FromField("PID.5.1");
        Map(x => x.FirstName).FromField("PID.5.2");
        Map(x => x.MiddleName).FromField("PID.5.3");
    }
}

/// <summary>
/// Profile with default values.
/// </summary>
public class PatientWithDefaultsProfile : HL7MappingProfile<PatientWithDefaults>
{
    public PatientWithDefaultsProfile()
    {
        ForMessages("ADT^A01");

        Map(x => x.Status)
            .FromField("PID.99")  // Non-existent field
            .WithDefault("ACTIVE");
    }
}

/// <summary>
/// Profile with required fields.
/// </summary>
public class PatientWithRequiredProfile : HL7MappingProfile<PatientWithRequired>
{
    public PatientWithRequiredProfile()
    {
        ForMessages("ADT^A01");

        Map(x => x.PatientId)
            .FromField("PID.3")
            .Required();
    }
}

/// <summary>
/// Profile with DateTime conversions.
/// </summary>
public class PatientWithDateTimeProfile : HL7MappingProfile<PatientWithDateTime>
{
    public PatientWithDateTimeProfile()
    {
        ForMessages("ADT^A01");

        Map(x => x.DateOfBirth)
            .FromField("PID.7")
            .AsDateTime("yyyyMMdd");

        Map(x => x.MessageDateTime)
            .FromField("MSH.7")
            .AsDateTime("yyyyMMddHHmmss");
    }
}

/// <summary>
/// Profile with enum conversions.
/// </summary>
public class PatientWithEnumsProfile : HL7MappingProfile<PatientWithEnums>
{
    public PatientWithEnumsProfile()
    {
        ForMessages("ADT^A01");

        Map(x => x.Gender)
            .FromField("PID.8")
            .AsEnum<Gender>();

        Map(x => x.MaritalStatus)
            .FromField("PID.16")
            .AsEnum<MaritalStatus>();
    }
}

/// <summary>
/// Profile with complex type mapping using generated HL7ComplexType mapper.
/// </summary>
public class PatientWithAddressProfile : HL7MappingProfile<PatientWithAddress>
{
    public PatientWithAddressProfile()
    {
        ForMessages("ADT^A01");

        Map(x => x.PatientId).FromField("PID.3");
        Map(x => x.LastName).FromField("PID.5.1");

        // Map complex type - FluentAPI will auto-detect AddressMapper from v0.4.0
        MapComplex(x => x.HomeAddress)
            .FromField("PID.11");
    }
}

#endregion

