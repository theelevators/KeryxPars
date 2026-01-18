namespace KeryxPars.HL7.Definitions;

using KeryxPars.HL7.Segments;

/// <summary>
/// Specialized HL7 message for scheduling appointments and resources.
/// Supports SIU message types (S12-S26).
/// </summary>
public class SchedulingMessage : HL7BaseMessage
{
    public PV1? Pv1 { get; set; }

    /// <summary>
    /// Scheduling activity information
    /// </summary>
    public SCH? Schedule { get; set; }

    /// <summary>
    /// Appointment location resources
    /// </summary>
    public List<AIL> LocationResources { get; set; } = [];

    /// <summary>
    /// Appointment personnel resources
    /// </summary>
    public List<AIP> PersonnelResources { get; set; } = [];

    /// <summary>
    /// Appointment service resources
    /// </summary>
    public List<AIS> ServiceResources { get; set; } = [];

    /// <summary>
    /// Notes and comments for appointments
    /// </summary>
    public List<NTE> Notes { get; set; } = [];

    /// <summary>
    /// Diagnoses related to scheduled appointments
    /// </summary>
    public List<DG1> Diagnoses { get; set; } = [];
}
