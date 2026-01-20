



using KeryxPars.HL7.Extensions;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Validation;
using KeryxPars.HL7.Serialization.Configuration;

Console.WriteLine("=== KeryxPars Validation Framework Examples ===\n");

var message = @"MSH|^~\&|SENDING_APPLICATION|SENDING_FACILITY|RECEIVING_APPLICATION|RECEIVING_FACILITY|201305171259|12|VXU^V04|2244455|P|2.3||||||
PID|1||123456||DUCK^DAISY^L||19690912|F|||123 NORTHWOOD ST APT 9^^NEW CITY^NC^27262-9944|||||||||||||||||||
ORC|OK|664443333^EEE|33994499||||^^^20220301||20220301101531|DAVE^DAVID^DAVE^D||444999^DAVID JR^JAMES^DAVID^^^^^LAB&PROVID&ISO^L^^^PROVID^FACILITY_CODE&1.2.888.444999.1.13.308.2.7.2.696969&ISO|1021209999^^^10299^^^^^WD999 09 LABORATORY NAME|^^^^^333^8022999||||CCC528Y73^CCC-528Y73||||||
RXA|0|999|20220301|20220301|217^PFIZER 12 YEARS \T\ UP SARS-COV-2 VACCINE^LIM_CVX|0.3|ML||00^New immunization record^NIP001|459920^DUCK^DAISY^L^^^^^LAB&PROVID&ISO^L^^^PROVID^FACILITY_CODE&1.2.888.444999.1.13.308.2.7.2.696969&ISO|1021209999^^^10299^^^^^WD999 09 LABORATORY NAME||||FK9999|20220531|PPR|||CP|A|20220301101531
RXR|IM^Intramuscular^HL70162|LD^Left Deltoid^HL70163|||";

// Example 1: Basic deserialization
Console.WriteLine("Example 1: Basic Deserialization");
Console.WriteLine("=================================");
var result = HL7Serializer.Deserialize(message);

if (result.IsSuccess)
{
    var hl7Message = result.Value;
    Console.WriteLine("✓ HL7 message deserialized successfully.");
    Console.WriteLine($"  Message Type: {hl7Message.Msh?.MessageType}");
    Console.WriteLine($"  Control ID: {hl7Message.Msh?.MessageControlID}");
}
else
{
    Console.WriteLine("✗ Failed to deserialize HL7 message:");
    Console.WriteLine(result.AsNack("SendingFacility", "SendingApplication", "VersionID"));
}

// Example 2: Quick Validation (Required Segments)
Console.WriteLine("\n\nExample 2: Quick Validation - Required Segments");
Console.WriteLine("===============================================");
var quickValidation = Validator.Required(result.Value!, "MSH", "PID", "EVN");
Console.WriteLine($"Is Valid: {quickValidation.IsValid}");
if (!quickValidation.IsValid)
{
    foreach (var error in quickValidation.Errors)
    {
        Console.WriteLine($"  ✗ {error.Severity}: {error.Message}");
    }
}
else
{
    Console.WriteLine("  ✓ All required segments present!");
}

// Example 3: Using Pre-built Templates
Console.WriteLine("\n\nExample 3: Using Pre-built Pharmacy Template");
Console.WriteLine("===========================================");
var pharmacyRules = ValidationTemplates.Pharmacy();
var pharmacyValidation = pharmacyRules.Validate(result.Value!);
Console.WriteLine($"Is Valid: {pharmacyValidation.IsValid}");
Console.WriteLine($"Total Errors: {pharmacyValidation.Errors.Count}");
foreach (var error in pharmacyValidation.Errors.Take(5))
{
    Console.WriteLine($"  [{error.Severity}] {error.Field}: {error.Message}");
}

// Example 4: Custom Validation Rules
Console.WriteLine("\n\nExample 4: Custom Validation Rules");
Console.WriteLine("==================================");
var customRules = new ValidationRules
{
    RequiredSegments = ["MSH", "PID"],
    Fields = new()
    {
        ["MSH.9"] = new() { Required = true, CustomMessage = "Message Type is mandatory" },
        ["MSH.10"] = new() { Required = true, MaxLength = 20 },
        ["PID.3"] = new() { Required = true, CustomMessage = "Patient ID is required" },
        ["PID.5"] = new() { MaxLength = 250 },
        ["PID.7"] = new() { Pattern = @"^\d{8}$", CustomMessage = "DOB must be YYYYMMDD format" }
    }
};

var customValidation = customRules.Validate(result.Value!);
Console.WriteLine($"Is Valid: {customValidation.IsValid}");
foreach (var error in customValidation.Errors)
{
    Console.WriteLine($"  [{error.Severity}] {error.Field}: {error.Message}");
}

// Example 5: Validation with SerializerOptions
Console.WriteLine("\n\nExample 5: Validation during Deserialization");
Console.WriteLine("===========================================");
var optionsWithValidation = new SerializerOptions
{
    Validation = ValidationTemplates.ADT()
};

var validatedResult = HL7Serializer.Deserialize(message, optionsWithValidation);
if (validatedResult.IsSuccess)
{
    Console.WriteLine("✓ Message parsed and validated successfully!");
}

// Example 6: Load Validation from JSON
Console.WriteLine("\n\nExample 6: JSON-based Validation Rules");
Console.WriteLine("=====================================");
var jsonRules = @"{
  ""RequiredSegments"": [""MSH"", ""PID""],
  ""Fields"": {
    ""MSH.9"": { ""Required"": true },
    ""MSH.10"": { ""Required"": true },
    ""PID.3"": { ""Required"": true }
  }
}";

var jsonValidation = Validator.FromJson(result.Value!, jsonRules);
Console.WriteLine($"Is Valid: {jsonValidation.IsValid}");
Console.WriteLine($"Errors: {jsonValidation.Errors.Count}");

Console.WriteLine("\n\n=== All Examples Complete! ===");

