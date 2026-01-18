namespace KeryxPars.HL7.Definitions;

using KeryxPars.HL7.Segments;

/// <summary>
/// Specialized HL7 message for financial/billing transactions.
/// Supports BAR (billing account) and DFT (detailed financial transaction) message types.
/// </summary>
public class FinancialMessage : HL7BaseMessage
{
    public PV1 Pv1 { get; set; } = new();

    /// <summary>
    /// Guarantor information
    /// </summary>
    public List<GT1> Guarantors { get; set; } = [];

    /// <summary>
    /// Insurance information
    /// </summary>
    public List<IN1> Insurance { get; set; } = [];

    /// <summary>
    /// Additional insurance details
    /// </summary>
    public List<IN2> InsuranceAdditional { get; set; } = [];

    /// <summary>
    /// Diagnosis Related Group
    /// </summary>
    public DRG? DiagnosisRelatedGroup { get; set; }

    /// <summary>
    /// Financial transactions
    /// </summary>
    public List<FT1> Transactions { get; set; } = [];

    /// <summary>
    /// Procedures for billing
    /// </summary>
    public List<PR1> Procedures { get; set; } = [];

    /// <summary>
    /// Diagnoses for billing
    /// </summary>
    public List<DG1> Diagnoses { get; set; } = [];

    /// <summary>
    /// Accident information for claims
    /// </summary>
    public ACC? Accident { get; set; }
}
