namespace KeryxPars.HL7.Definitions;

using KeryxPars.HL7.Segments;

/// <summary>
/// Specialized HL7 message for laboratory orders and results.
/// Supports ORU (observation result) and ORM (order) message types.
/// </summary>
public class LabMessage : HL7BaseMessage
{
    public PV1? Pv1 { get; set; }
    public PV2? Pv2 { get; set; }

    /// <summary>
    /// Lab orders with associated OBR/OBX segments
    /// </summary>
    public List<OrderGroup> Orders { get; set; } = [];

    /// <summary>
    /// Specimen information for lab orders
    /// </summary>
    public List<SPM> Specimens { get; set; } = [];

    /// <summary>
    /// Specimen container details
    /// </summary>
    public List<SAC> Containers { get; set; } = [];

    /// <summary>
    /// Clinical trial identification for research labs
    /// </summary>
    public List<CTI> ClinicalTrials { get; set; } = [];

    /// <summary>
    /// Diagnoses relevant to lab orders
    /// </summary>
    public List<DG1> Diagnoses { get; set; } = [];

    /// <summary>
    /// Notes and comments for lab orders/results
    /// </summary>
    public List<NTE> Notes { get; set; } = [];

}
