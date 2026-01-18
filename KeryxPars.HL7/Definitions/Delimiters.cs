namespace KeryxPars.HL7.Definitions;

/// <summary>
/// Value type for HL7 delimiters to avoid allocations.
/// MSH|^~\&|SenderApp|SenderFac|ReceiverApp|ReceiverFac|DateTime|Security|MessageType^TriggerEvent|MessageID|P|VersionID
/// </summary>
public readonly ref struct HL7Delimiters(
    char fieldSeparator = '|',
    char componentSeparator = '^',
    char fieldRepeatSeparator = '~',
    char escapeCharacter = '\\',
    char subComponentSeparator = '&')
{
    public readonly char FieldSeparator = fieldSeparator;
    public readonly char ComponentSeparator = componentSeparator;
    public readonly char FieldRepeatSeparator = fieldRepeatSeparator;
    public readonly char EscapeCharacter = escapeCharacter;
    public readonly char SubComponentSeparator = subComponentSeparator;

    public static HL7Delimiters Default => new();
    public bool AreUninitialized => default(HL7Delimiters) is HL7Delimiters other &&
                                   FieldSeparator == other.FieldSeparator &&
                                   ComponentSeparator == other.ComponentSeparator &&
                                   FieldRepeatSeparator == other.FieldRepeatSeparator &&
                                   EscapeCharacter == other.EscapeCharacter &&
                                   SubComponentSeparator == other.SubComponentSeparator;

    public static bool TryParse(ReadOnlySpan<char> message, out HL7Delimiters delimiters, out HL7Error? error)
    {
        error = null;

        //message needs to contain at least 8 characters to have the control characters + MSH segment
        if (message.Length < 8)
        {
            error = new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "Control characters not provided");
            delimiters = default;
            return false;
        }

        // validate message control characters
        var fieldSeparator = message[3];
        if (fieldSeparator == '\0')
        {
            error = new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "Invalid or Missing FieldSeparator");
            delimiters = default;
            return false;
        }

        var controlCharacters = message[3..8];

        // terminate parsing if the characters were not provided.
        if (controlCharacters.IsEmpty)
        {
            error = new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "Control characters not provided");
            delimiters = default;
            return false;
        }

        var componentSeparator = controlCharacters[0];
        if (componentSeparator == '\0')
        {
            error = new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "Invalid or Missing ComponentSeparator");
            delimiters = default;
            return false;
        }

        var fieldRepeatSeparator = controlCharacters[1];
        if (fieldRepeatSeparator == '\0')
        {
            error = new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "Invalid or Missing FieldRepeatSeparator");
            delimiters = default;
            return false;
        }

        var escapeCharacter = controlCharacters[2];
        if (escapeCharacter == '\0')
        {
            error = new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "Invalid or Missing EscapeCharacter");
            delimiters = default;
            return false;
        }

        var subComponentSeparator = controlCharacters[3];
        if (subComponentSeparator == '\0')
        {
            error = new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "Invalid or Missing SubComponentSeparator");
            delimiters = default;
            return false;
        }

        delimiters = new HL7Delimiters(
            fieldSeparator: fieldSeparator,
            componentSeparator: componentSeparator,
            fieldRepeatSeparator: fieldRepeatSeparator,
            escapeCharacter: escapeCharacter,
            subComponentSeparator: subComponentSeparator
        );

        return true;
    }
}