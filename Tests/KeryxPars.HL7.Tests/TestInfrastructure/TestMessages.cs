namespace KeryxPars.HL7.Tests.TestInfrastructure;

/// <summary>
/// Shared HL7 test messages used across multiple test files.
/// Provides consistent, reusable test data.
/// </summary>
public static class TestMessages
{
    #region Basic Patient Messages

    /// <summary>
    /// Basic ADT^A01 message with male patient.
    /// </summary>
    public const string BasicMalePatient =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143000||ADT^A01|MSG001|P|2.5||\r" +
        "EVN|A01|20230615143000|||\r" +
        "PID|1||PAT123456||DOE^JOHN^A||19850615|M|||123 MAIN ST^^ANYTOWN^CA^12345||555-1234|||M|NON|";

    /// <summary>
    /// Basic ADT^A01 message with female patient.
    /// </summary>
    public const string BasicFemalePatient =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143000||ADT^A01|MSG002|P|2.5||\r" +
        "EVN|A01|20230615143000|||\r" +
        "PID|1||PAT789012||SMITH^JANE^B||19900101|F|||456 ELM ST^^SOMECITY^NY^67890||555-9999|||S|NON|";

    /// <summary>
    /// Enhanced patient message with visit information.
    /// </summary>
    public const string PatientWithVisit =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143000||ADT^A01|MSG003|P|2.5||\r" +
        "EVN|A01|20230615143000|||\r" +
        "PID|1||PAT345678||JONES^ROBERT^C||19750320|M|||789 OAK AVE^^SOMEWHERE^TX^45678||555-7777|||M|NON|\r" +
        "PV1|1|I|WARD^101^1^HOSPITAL||||123^SMITH^JOHN|||SUR||||ADM|A0|||||||||||||||||||||||||20230615100000";

    /// <summary>
    /// Minimal MSH-only message.
    /// </summary>
    public const string MinimalMessage =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143000||ADT^A01|MSG999|P|2.5||";

    #endregion

    #region Inpatient vs Outpatient

    /// <summary>
    /// Female inpatient with complete visit data.
    /// </summary>
    public const string FemaleInpatient =
        "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
        "PID|1||MRN123||DOE^JANE||19850315|F|||123 MAIN ST|||||||||\r" +
        "PV1|1|I|ICU^201^A||||||||||||||||VISIT123|||||||||||||||||||||||||||||||||||||||";

    /// <summary>
    /// Male outpatient.
    /// </summary>
    public const string MaleOutpatient =
        "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG002|P|2.5||\r" +
        "PID|1||MRN456||DOE^JOHN||19800115|M|||456 ELM ST|||||||||\r" +
        "PV1|1|O||||||||||||||||||VISIT456|||||||||||||||||||||||||||||||||||||||";

    #endregion

    #region Complex Address Data

    /// <summary>
    /// Patient with complete address information.
    /// </summary>
    public const string PatientWithCompleteAddress =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143000||ADT^A01|MSG004|P|2.5||\r" +
        "PID|1||PAT111222||WILSON^MARY^D||19920710|F|||123 MAIN ST^^ANYTOWN^CA^12345|ORANGE|555-4444|||M|NON||||||USA|||";

    #endregion

    #region DateTime Testing

    /// <summary>
    /// Message with various datetime formats for testing.
    /// </summary>
    public const string MessageWithDateTimes =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143000||ADT^A01|MSG005|P|2.5||\r" +
        "EVN|A01|20230615143000|||\r" +
        "PID|1||PAT555666||BROWN^DAVID^E||19851215|M|||321 PINE RD^^NEWTOWN^FL^23456||555-2222|||S|NON|\r" +
        "PV1|1|E|ER^01^1^HOSPITAL||||456^JONES^MARY|||MED||||ADM|A0|||||||||||||||||||||||||20230615120000";

    #endregion

    #region Empty/Missing Data

    /// <summary>
    /// Patient with many missing/empty fields.
    /// </summary>
    public const string PatientWithMissingFields =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143000||ADT^A01|MSG006|P|2.5||\r" +
        "PID|1||PAT999888||INCOMPLETE^TEST||||||||||";

    #endregion
}
