using KeryxPars.HL7.Segments;

namespace KeryxPars.HL7.Definitions;

/// <summary>
/// Comprehensive HL7 message model that includes all possible segment types.
/// Designed for tools and viewers that need to display any segment from any message type.
/// This message type captures segments that might not be present in specialized message types.
/// </summary>
public class HL7ComprehensiveMessage : HL7DefaultMessage
{
    // Additional patient segments
    public PD1? Pd1 { get; set; }
    
    // Next of Kin
    public List<NK1> NextOfKin { get; set; } = [];
    
    // Guarantor
    public List<GT1> Guarantors { get; set; } = [];
    
    // Additional Insurance
    public List<IN2> InsuranceAdditional { get; set; } = [];
    
    // Procedures
    public List<PR1> Procedures { get; set; } = [];
    
    // Diagnosis Related Group
    public DRG? DiagnosisRelatedGroup { get; set; }
    
    // Accident Information
    public ACC? Accident { get; set; }
    
    // Merge Information
    public MRG? MergeInfo { get; set; }
    
    // Roles
    public List<ROL> Roles { get; set; } = [];
    
    // Notes and Comments
    public List<NTE> Notes { get; set; } = [];
    
    // Scheduling
    public SCH? Schedule { get; set; }
    public List<AIL> LocationResources { get; set; } = [];
    public List<AIP> PersonnelResources { get; set; } = [];
    public List<AIS> ServiceResources { get; set; } = [];
    
    // Lab/Observation
    public List<OBR> ObservationRequests { get; set; } = [];
    public List<OBX> ObservationResults { get; set; } = [];
    public List<SPM> Specimens { get; set; } = [];
    public List<SAC> Containers { get; set; } = [];
    
    // Orders (Common Order segment for medication/lab orders)
    public List<ORC> CommonOrders { get; set; } = [];
    
    // Pharmacy
    public List<RXA> PharmacyAdministrations { get; set; } = [];
    public List<RXC> PharmacyComponents { get; set; } = [];
    public List<RXD> PharmacyDispenses { get; set; } = [];
    public List<RXG> PharmacyGives { get; set; } = [];


    
    // Financial
    public List<FT1> Transactions { get; set; } = [];
    
    // Query segments
    public QRD? QueryDefinition { get; set; }
    public QRF? QueryFilter { get; set; }
    public QPD? QueryParameterDefinition { get; set; }
    public RCP? ResponseControlParameter { get; set; }
    
    // Continuation/Control
    public DSC? ContinuationPointer { get; set; }
    
    // Contact Data
    public List<CTD> ContactData { get; set; } = [];
    
    // Clinical Trial Identification
    public List<CTI> ClinicalTrials { get; set; } = [];
    
    // Diet/Nutrition
    public List<ODS> DietaryOrders { get; set; } = [];
    public List<ODT> TrayInstructions { get; set; } = [];
    
    // Acknowledgment
    public MSA? MessageAcknowledgment { get; set; }
}
