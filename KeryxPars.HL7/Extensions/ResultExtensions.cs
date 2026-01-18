
using KeryxPars.Core.Models;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;
using System.Text;

namespace KeryxPars.HL7.Extensions;

public static class ResultExtensions
{
    public static string AsNack(this Result<HL7Message, HL7Error[]> serializationResult, string sendingFacility, string sendingApplication, string versionId, string? receivingApplication = null, string? receivingFacility = null, string? security = null, string? processingId = null, string? controlId = null, string? newLineSeparator = "\r\n")
    {

        var hl7Base = HL7Delimiters.Default;
        MSH mshResponse = new();
        string messageType = "ACK" + hl7Base.ComponentSeparator;

        StringBuilder ack = new();
        string fs = hl7Base.FieldSeparator.ToString();


        mshResponse.SendingApplication = sendingApplication;
        mshResponse.SendingFacility = sendingFacility;
        mshResponse.ReceivingFacility = receivingFacility ?? "";
        mshResponse.ReceivingApplication = receivingApplication ?? "";
        mshResponse.Security = security ?? "";
        mshResponse.ProcessingID = processingId ?? "P";
        mshResponse.VersionID = versionId ?? "2.5";
        mshResponse.MessageType = messageType + "";
        mshResponse.MessageControlID = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        mshResponse.DateTimeOfMessage = DateTime.Now.ToString("yyyyMMddHHmmss");

        ack.Append("MSH");
        ack.Append(hl7Base.FieldSeparator);
        ack.Append(hl7Base.ComponentSeparator);
        ack.Append(hl7Base.FieldRepeatSeparator);
        ack.Append(hl7Base.EscapeCharacter);
        ack.Append(hl7Base.SubComponentSeparator);
        ack.Append(hl7Base.FieldSeparator);
        ack.Append(mshResponse.SendingApplication + fs);
        ack.Append(mshResponse.SendingFacility + fs);
        ack.Append(mshResponse.ReceivingApplication + fs);
        ack.Append(mshResponse.ReceivingFacility + fs);
        ack.Append(mshResponse.DateTimeOfMessage + fs);
        ack.Append(mshResponse.Security + fs);
        ack.Append(mshResponse.MessageType + fs);
        ack.Append(mshResponse.MessageControlID + fs);
        ack.Append(mshResponse.ProcessingID + fs);
        ack.Append(mshResponse.VersionID + fs + fs + fs + fs + fs + fs);
        ack.Append("ASCII" + fs + fs + fs + newLineSeparator);


        ack.Append("MSA" + fs);
        ack.Append("AR" + fs);
        ack.Append(controlId ?? "" + fs + fs + fs + fs);

        // Order errors by severity (forces severe errors first) and add each one to ack
        foreach (var error in serializationResult.Error!)
        {
            ack.Append(newLineSeparator);
            ack.Append("ERR" + fs + fs);
            ack.Append(fs); // Error Location
            ack.Append(error.Code + fs);
            ack.Append(error.Severity + fs);
            ack.Append(fs); // Application Error Code
            ack.Append(fs); // Application Error Parameter
            ack.Append(error.Message + fs); // Diagnostic Information
            ack.Append(fs); //User Message
            ack.Append(fs); // Inform person indicator
            ack.Append(fs); // Override type
            ack.Append(fs); // Override reason code
            ack.Append(fs); // Help desk contact point
        }

        return ack.ToString();
    }


}
