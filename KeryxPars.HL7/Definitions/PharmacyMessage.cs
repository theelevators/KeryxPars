namespace KeryxPars.HL7.Definitions;

using KeryxPars.HL7.Segments;

/// <summary>
/// Specialized HL7 message for pharmacy/medication orders.
/// Focuses on pharmacy-specific segments and workflows (RDE, RDS, RAS, RGV message types).
/// </summary>
public class PharmacyMessage : HL7BaseMessage
{
    public PV1? Pv1 { get; set; }
    public PV2? Pv2 { get; set; }

    /// <summary>
    /// Medication orders with associated pharmacy segments
    /// </summary>
    public List<OrderGroup> Orders { get; set; } = [];

    /// <summary>
    /// Patient allergies relevant to medication ordering
    /// </summary>
    public List<AL1> Allergies { get; set; } = [];

    /// <summary>
    /// Diagnoses relevant to medication orders
    /// </summary>
    public List<DG1> Diagnoses { get; set; } = [];

    /// <summary>
    /// Insurance information for pharmacy billing
    /// </summary>
    public List<IN1> Insurance { get; set; } = [];

    /// <summary>
    /// Notes and comments related to pharmacy orders
    /// </summary>
    public List<NTE> Notes { get; set; } = [];
}
