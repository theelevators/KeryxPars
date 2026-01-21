using System;
using KeryxPars.HL7.Mapping;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for the NEW HL7MessageAttribute patterns!
/// Ensures all 3 patterns work correctly:
/// 1. No restrictions (accept any)
/// 2. Whitelist (Allows)
/// 3. Blacklist (NotAllows)
/// </summary>
public class HL7MessageAttributeTests
{
    // ========== PATTERN 1: No Restrictions (Accept Any) ==========
    
    [Fact]
    public void NoRestrictions_AcceptsAnyMessageType()
    {
        // Arrange
        var attribute = new HL7MessageAttribute();
        
        // Act & Assert - Should accept EVERYTHING
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
        attribute.IsAllowed("OMP^O09").ShouldBeTrue();
        attribute.IsAllowed("ORU^R01").ShouldBeTrue();
        attribute.IsAllowed("SIU^S12").ShouldBeTrue();
        attribute.IsAllowed("MDM^T02").ShouldBeTrue();
        attribute.IsAllowed("BAR^P01").ShouldBeTrue();
        attribute.IsAllowed("DFT^P03").ShouldBeTrue();
        attribute.IsAllowed("UNKNOWN^X99").ShouldBeTrue();
    }
    
    [Fact]
    public void NoRestrictions_AllowsAndNotAllowsAreNull()
    {
        // Arrange
        var attribute = new HL7MessageAttribute();
        
        // Assert
        attribute.Allows.ShouldBeNull();
        attribute.NotAllows.ShouldBeNull();
    }
    
    // ========== PATTERN 2: Whitelist (Allows) ==========
    
    [Fact]
    public void Whitelist_AcceptsOnlyAllowedTypes()
    {
        // Arrange
        var attribute = new HL7MessageAttribute
        {
            Allows = new[] { "ADT^A01", "ADT^A04", "ADT^A08" }
        };
        
        // Act & Assert - Should accept ONLY allowed types
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
        attribute.IsAllowed("ADT^A04").ShouldBeTrue();
        attribute.IsAllowed("ADT^A08").ShouldBeTrue();
        
        // Should reject everything else
        attribute.IsAllowed("ADT^A03").ShouldBeFalse();
        attribute.IsAllowed("OMP^O09").ShouldBeFalse();
        attribute.IsAllowed("ORU^R01").ShouldBeFalse();
    }
    
    [Fact]
    public void Whitelist_CaseInsensitive()
    {
        // Arrange
        var attribute = new HL7MessageAttribute
        {
            Allows = new[] { "ADT^A01" }
        };
        
        // Act & Assert - Case insensitive matching
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
        attribute.IsAllowed("adt^a01").ShouldBeTrue();
        attribute.IsAllowed("Adt^A01").ShouldBeTrue();
    }
    
    [Fact]
    public void Whitelist_EmptyArray_RejectsEverything()
    {
        // Arrange
        var attribute = new HL7MessageAttribute
        {
            Allows = new string[] { }
        };
        
        // Act & Assert - Empty whitelist rejects everything
        attribute.IsAllowed("ADT^A01").ShouldBeFalse();
        attribute.IsAllowed("OMP^O09").ShouldBeFalse();
    }
    
    // ========== PATTERN 3: Blacklist (NotAllows) ==========
    
    [Fact]
    public void Blacklist_RejectsOnlyNotAllowedTypes()
    {
        // Arrange
        var attribute = new HL7MessageAttribute
        {
            NotAllows = new[] { "BAR^P01", "BAR^P02", "DFT^P03" }
        };
        
        // Act & Assert - Should reject ONLY disallowed types
        attribute.IsAllowed("BAR^P01").ShouldBeFalse();
        attribute.IsAllowed("BAR^P02").ShouldBeFalse();
        attribute.IsAllowed("DFT^P03").ShouldBeFalse();
        
        // Should accept everything else
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
        attribute.IsAllowed("OMP^O09").ShouldBeTrue();
        attribute.IsAllowed("ORU^R01").ShouldBeTrue();
    }
    
    [Fact]
    public void Blacklist_CaseInsensitive()
    {
        // Arrange
        var attribute = new HL7MessageAttribute
        {
            NotAllows = new[] { "BAR^P01" }
        };
        
        // Act & Assert
        attribute.IsAllowed("BAR^P01").ShouldBeFalse();
        attribute.IsAllowed("bar^p01").ShouldBeFalse();
        attribute.IsAllowed("Bar^P01").ShouldBeFalse();
    }
    
    [Fact]
    public void Blacklist_EmptyArray_AcceptsEverything()
    {
        // Arrange
        var attribute = new HL7MessageAttribute
        {
            NotAllows = new string[] { }
        };
        
        // Act & Assert - Empty blacklist accepts everything
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
        attribute.IsAllowed("BAR^P01").ShouldBeTrue();
        attribute.IsAllowed("UNKNOWN^X99").ShouldBeTrue();
    }
    
    // ========== EDGE CASES ==========
    
    [Fact]
    public void EdgeCase_NullOrEmptyMessageType_ReturnsFalse()
    {
        // Arrange
        var attribute = new HL7MessageAttribute();
        
        // Act & Assert
        attribute.IsAllowed(null!).ShouldBeFalse();
        attribute.IsAllowed("").ShouldBeFalse();
        attribute.IsAllowed("   ").ShouldBeFalse();
    }
    
    [Fact]
    public void EdgeCase_BothAllowsAndNotAllows_NotAllowsTakesPrecedence()
    {
        // Arrange - Conflicting configuration (NotAllows wins!)
        var attribute = new HL7MessageAttribute
        {
            Allows = new[] { "ADT^A01" },
            NotAllows = new[] { "ADT^A01" }
        };
        
        // Act & Assert - NotAllows takes precedence
        attribute.IsAllowed("ADT^A01").ShouldBeFalse();
    }
    
    [Fact]
    public void EdgeCase_NotAllowsWithOtherTypes_AllowsNonBlacklistedTypes()
    {
        // Arrange
        var attribute = new HL7MessageAttribute
        {
            Allows = new[] { "ADT^A01", "ADT^A04" },  // Whitelist
            NotAllows = new[] { "ADT^A01" }           // Blacklist one from whitelist
        };
        
        // Act & Assert
        attribute.IsAllowed("ADT^A01").ShouldBeFalse();  // Blacklisted
        attribute.IsAllowed("ADT^A04").ShouldBeTrue();   // NotAllows set, so allow anything not blacklisted
        attribute.IsAllowed("OMP^O09").ShouldBeTrue();   // NotAllows set, so allow anything not blacklisted
    }
    
    // ========== BACKWARD COMPATIBILITY (Legacy Constructor) ==========
    
    [Fact]
    public void BackwardCompatibility_ConstructorWithTypes_MapsToAllows()
    {
        // Arrange
        var attribute = new HL7MessageAttribute("ADT^A01", "ADT^A04");
        
        // Act & Assert - Should map to Allows property
        attribute.Allows.ShouldNotBeNull();
        attribute.Allows.ShouldContain("ADT^A01");
        attribute.Allows.ShouldContain("ADT^A04");
        attribute.Allows.Length.ShouldBe(2);
    }
    
    [Fact]
    public void BackwardCompatibility_ConstructorWithTypes_BehavesLikeWhitelist()
    {
        // Arrange
        var attribute = new HL7MessageAttribute("ADT^A01", "ADT^A04");
        
        // Act & Assert - Should accept only specified types
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
        attribute.IsAllowed("ADT^A04").ShouldBeTrue();
        attribute.IsAllowed("ADT^A03").ShouldBeFalse();
        attribute.IsAllowed("OMP^O09").ShouldBeFalse();
    }
    
    [Fact]
    public void BackwardCompatibility_MessageTypesProperty_ReturnsAllows()
    {
        // Arrange
        var attribute = new HL7MessageAttribute("ADT^A01", "ADT^A04");
        
        // Act & Assert - Legacy MessageTypes property should still work
        attribute.MessageTypes.ShouldNotBeNull();
        attribute.MessageTypes.ShouldBe(attribute.Allows);
    }
    
    [Fact]
    public void BackwardCompatibility_ConstructorThrowsOnNull()
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() => new HL7MessageAttribute(null!));
    }
    
    [Fact]
    public void BackwardCompatibility_ConstructorThrowsOnEmpty()
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() => new HL7MessageAttribute(new string[] { }));
    }
    
    // ========== REAL-WORLD SCENARIOS ==========
    
    [Fact]
    public void RealWorld_UniversalModel_AcceptsEverything()
    {
        // Arrange - Universal patient model
        var attribute = new HL7MessageAttribute();
        
        // Act & Assert - Should accept all common message types
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();  // Admission
        attribute.IsAllowed("ADT^A03").ShouldBeTrue();  // Discharge
        attribute.IsAllowed("OMP^O09").ShouldBeTrue();  // Pharmacy
        attribute.IsAllowed("ORU^R01").ShouldBeTrue();  // Lab results
        attribute.IsAllowed("SIU^S12").ShouldBeTrue();  // Scheduling
        attribute.IsAllowed("MDM^T02").ShouldBeTrue();  // Documents
    }
    
    [Fact]
    public void RealWorld_LabOnly_AcceptsOnlyLabMessages()
    {
        // Arrange - Lab-only model
        var attribute = new HL7MessageAttribute
        {
            Allows = new[] { "ORU^R01", "ORU^R03", "ORU^R32" }
        };
        
        // Act & Assert
        attribute.IsAllowed("ORU^R01").ShouldBeTrue();
        attribute.IsAllowed("ORU^R03").ShouldBeTrue();
        attribute.IsAllowed("ORU^R32").ShouldBeTrue();
        attribute.IsAllowed("ADT^A01").ShouldBeFalse();
        attribute.IsAllowed("OMP^O09").ShouldBeFalse();
    }
    
    [Fact]
    public void RealWorld_NoBillingMessages_RejectsFinancial()
    {
        // Arrange - Clinical-only model (no billing)
        var attribute = new HL7MessageAttribute
        {
            NotAllows = new[] { "BAR^P01", "BAR^P02", "DFT^P03", "DFT^P11" }
        };
        
        // Act & Assert - Reject billing
        attribute.IsAllowed("BAR^P01").ShouldBeFalse();
        attribute.IsAllowed("BAR^P02").ShouldBeFalse();
        attribute.IsAllowed("DFT^P03").ShouldBeFalse();
        attribute.IsAllowed("DFT^P11").ShouldBeFalse();
        
        // Accept clinical
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
        attribute.IsAllowed("ORU^R01").ShouldBeTrue();
        attribute.IsAllowed("OMP^O09").ShouldBeTrue();
    }
    
    [Fact]
    public void RealWorld_NoTestMessages_RejectsTest()
    {
        // Arrange - Production system (no test messages)
        var attribute = new HL7MessageAttribute
        {
            NotAllows = new[] { "TST^T01", "DBG^D01" }
        };
        
        // Act & Assert
        attribute.IsAllowed("TST^T01").ShouldBeFalse();
        attribute.IsAllowed("DBG^D01").ShouldBeFalse();
        attribute.IsAllowed("ADT^A01").ShouldBeTrue();
    }
}
