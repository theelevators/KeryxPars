namespace KeryxPars.HL7.Tests.Serialization;

/// <summary>
/// Tests for HL7Delimiters parsing and validation
/// </summary>
public class DelimitersTests
{
    [Fact]
    public void TryParse_WithValidMessage_ShouldParseDelimitersCorrectly()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||".AsSpan();

        // Act
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        // Assert
        success.ShouldBeTrue();
        error.ShouldBeNull();
        delimiters.FieldSeparator.ShouldBe('|');
        delimiters.ComponentSeparator.ShouldBe('^');
        delimiters.FieldRepeatSeparator.ShouldBe('~');
        delimiters.EscapeCharacter.ShouldBe('\\');
        delimiters.SubComponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void TryParse_WithCustomDelimiters_ShouldParseCorrectly()
    {
        // Arrange
        var message = "MSH$*#@%$SEND$FAC$REC$FAC".AsSpan();

        // Act
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        // Assert
        success.ShouldBeTrue();
        error.ShouldBeNull();
        delimiters.FieldSeparator.ShouldBe('$');
        delimiters.ComponentSeparator.ShouldBe('*');
        delimiters.FieldRepeatSeparator.ShouldBe('#');
        delimiters.EscapeCharacter.ShouldBe('@');
        delimiters.SubComponentSeparator.ShouldBe('%');
    }

    [Fact]
    public void TryParse_WithMessageTooShort_ShouldReturnError()
    {
        // Arrange
        var message = "MSH|^~".AsSpan();

        // Act
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        // Assert
        success.ShouldBeFalse();
        error.ShouldNotBeNull();
        error!.Message.ShouldContain("Control characters not provided");
    }

    [Fact]
    public void TryParse_WithEmptyMessage_ShouldReturnError()
    {
        // Arrange
        var message = "".AsSpan();

        // Act
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        // Assert
        success.ShouldBeFalse();
        error.ShouldNotBeNull();
    }

    [Theory]
    [InlineData("MSH")]
    [InlineData("MSH|")]
    [InlineData("MSH|^")]
    [InlineData("MSH|^~")]
    [InlineData("MSH|^~\\")]
    public void TryParse_WithIncompleteDelimiters_ShouldReturnError(string message)
    {
        // Arrange
        var span = message.AsSpan();

        // Act
        var success = HL7Delimiters.TryParse(span, out var delimiters, out var error);

        // Assert
        success.ShouldBeFalse();
        error.ShouldNotBeNull();
    }

    [Fact]
    public void Default_ShouldReturnStandardDelimiters()
    {
        // Act
        var delimiters = HL7Delimiters.Default;

        // Assert
        delimiters.FieldSeparator.ShouldBe('|');
        delimiters.ComponentSeparator.ShouldBe('^');
        delimiters.FieldRepeatSeparator.ShouldBe('~');
        delimiters.EscapeCharacter.ShouldBe('\\');
        delimiters.SubComponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void AreUninitialized_WithDefaultDelimiters_ShouldReturnTrue()
    {
        // Arrange
        var delimiters = new HL7Delimiters();

        // Act
        var result = delimiters.AreUninitialized;

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void AreUninitialized_WithCustomDelimiters_ShouldReturnFalse()
    {
        // Arrange
        var delimiters = new HL7Delimiters('$', '*', '#', '@', '%');

        // Act
        var result = delimiters.AreUninitialized;

        // Assert
        result.ShouldBeFalse();
    }
}
