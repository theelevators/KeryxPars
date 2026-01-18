namespace KeryxPars.HL7.Tests.Serialization;

/// <summary>
/// Tests for HL7Delimiters parsing and validation
/// </summary>
public class DelimitersTests
{
    [Fact]
    public void TryParse_WithValidMessage_ShouldParseDelimitersCorrectly()
    {
        
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||".AsSpan();

        
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        
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
        
        var message = "MSH$*#@%$SEND$FAC$REC$FAC".AsSpan();

        
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        
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
        
        var message = "MSH|^~".AsSpan();

        
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        
        success.ShouldBeFalse();
        error.ShouldNotBeNull();
        error!.Message.ShouldContain("Control characters not provided");
    }

    [Fact]
    public void TryParse_WithEmptyMessage_ShouldReturnError()
    {
        
        var message = "".AsSpan();

        
        var success = HL7Delimiters.TryParse(message, out var delimiters, out var error);

        
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
        
        var span = message.AsSpan();

        
        var success = HL7Delimiters.TryParse(span, out var delimiters, out var error);

        
        success.ShouldBeFalse();
        error.ShouldNotBeNull();
    }

    [Fact]
    public void Default_ShouldReturnStandardDelimiters()
    {
        
        var delimiters = HL7Delimiters.Default;

        
        delimiters.FieldSeparator.ShouldBe('|');
        delimiters.ComponentSeparator.ShouldBe('^');
        delimiters.FieldRepeatSeparator.ShouldBe('~');
        delimiters.EscapeCharacter.ShouldBe('\\');
        delimiters.SubComponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void AreUninitialized_WithDefaultDelimiters_ShouldReturnTrue()
    {
        
        var delimiters = new HL7Delimiters();

        
        var result = delimiters.AreUninitialized;

        
        result.ShouldBeTrue();
    }

    [Fact]
    public void AreUninitialized_WithCustomDelimiters_ShouldReturnFalse()
    {
        
        var delimiters = new HL7Delimiters('$', '*', '#', '@', '%');

        
        var result = delimiters.AreUninitialized;

        
        result.ShouldBeFalse();
    }
}
