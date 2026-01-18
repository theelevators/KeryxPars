using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// PL - Person Location
/// HL7 2.5 Composite Data Type representing a person's location within a healthcare facility.
/// Format: PointOfCare^Room^Bed^Facility^LocationStatus^PersonLocationType^Building^Floor^LocationDescription^ComprehensiveLocationIdentifier^AssigningAuthority
/// </summary>
public readonly struct PL : ICompositeDataType
{
    private readonly IS _pointOfCare;
    private readonly IS _room;
    private readonly IS _bed;
    private readonly HD _facility;
    private readonly IS _locationStatus;
    private readonly IS _personLocationType;
    private readonly IS _building;
    private readonly IS _floor;
    private readonly ST _locationDescription;
    private readonly EI _comprehensiveLocationIdentifier;
    private readonly HD _assigningAuthority;

    /// <summary>
    /// Initializes a new instance of the PL data type with all components.
    /// </summary>
    public PL(
        IS pointOfCare = default,
        IS room = default,
        IS bed = default,
        HD facility = default,
        IS locationStatus = default,
        IS personLocationType = default,
        IS building = default,
        IS floor = default,
        ST locationDescription = default,
        EI comprehensiveLocationIdentifier = default,
        HD assigningAuthority = default)
    {
        _pointOfCare = pointOfCare;
        _room = room;
        _bed = bed;
        _facility = facility;
        _locationStatus = locationStatus;
        _personLocationType = personLocationType;
        _building = building;
        _floor = floor;
        _locationDescription = locationDescription;
        _comprehensiveLocationIdentifier = comprehensiveLocationIdentifier;
        _assigningAuthority = assigningAuthority;
    }

    /// <inheritdoc/>
    public string TypeCode => "PL";

    /// <inheritdoc/>
    public int ComponentCount => 11;

    /// <inheritdoc/>
    public bool IsEmpty => _pointOfCare.IsEmpty && _room.IsEmpty && _bed.IsEmpty &&
                          _facility.IsEmpty && _locationStatus.IsEmpty && _personLocationType.IsEmpty &&
                          _building.IsEmpty && _floor.IsEmpty && _locationDescription.IsEmpty &&
                          _comprehensiveLocationIdentifier.IsEmpty && _assigningAuthority.IsEmpty;

    /// <summary>
    /// PL.1 - Point of Care
    /// </summary>
    public IS PointOfCare => _pointOfCare;

    /// <summary>
    /// PL.2 - Room
    /// </summary>
    public IS Room => _room;

    /// <summary>
    /// PL.3 - Bed
    /// </summary>
    public IS Bed => _bed;

    /// <summary>
    /// PL.4 - Facility
    /// </summary>
    public HD Facility => _facility;

    /// <summary>
    /// PL.5 - Location Status
    /// </summary>
    public IS LocationStatus => _locationStatus;

    /// <summary>
    /// PL.6 - Person Location Type
    /// </summary>
    public IS PersonLocationType => _personLocationType;

    /// <summary>
    /// PL.7 - Building
    /// </summary>
    public IS Building => _building;

    /// <summary>
    /// PL.8 - Floor
    /// </summary>
    public IS Floor => _floor;

    /// <summary>
    /// PL.9 - Location Description
    /// </summary>
    public ST LocationDescription => _locationDescription;

    /// <summary>
    /// PL.10 - Comprehensive Location Identifier
    /// </summary>
    public EI ComprehensiveLocationIdentifier => _comprehensiveLocationIdentifier;

    /// <summary>
    /// PL.11 - Assigning Authority for Location
    /// </summary>
    public HD AssigningAuthority => _assigningAuthority;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _pointOfCare,
            1 => _room,
            2 => _bed,
            3 => _facility,
            4 => _locationStatus,
            5 => _personLocationType,
            6 => _building,
            7 => _floor,
            8 => _locationDescription,
            9 => _comprehensiveLocationIdentifier,
            10 => _assigningAuthority,
            _ => null
        };
    }

    /// <inheritdoc/>
    public void SetComponent(int index, IHL7DataType? value)
    {
        if (value == null) return;

        switch (index)
        {
            case 0:
                if (value is IS is0) System.Runtime.CompilerServices.Unsafe.AsRef(in _pointOfCare) = is0;
                break;
            case 1:
                if (value is IS is1) System.Runtime.CompilerServices.Unsafe.AsRef(in _room) = is1;
                break;
            case 2:
                if (value is IS is2) System.Runtime.CompilerServices.Unsafe.AsRef(in _bed) = is2;
                break;
            case 3:
                if (value is HD hd3) System.Runtime.CompilerServices.Unsafe.AsRef(in _facility) = hd3;
                break;
            case 4:
                if (value is IS is4) System.Runtime.CompilerServices.Unsafe.AsRef(in _locationStatus) = is4;
                break;
            case 5:
                if (value is IS is5) System.Runtime.CompilerServices.Unsafe.AsRef(in _personLocationType) = is5;
                break;
            case 6:
                if (value is IS is6) System.Runtime.CompilerServices.Unsafe.AsRef(in _building) = is6;
                break;
            case 7:
                if (value is IS is7) System.Runtime.CompilerServices.Unsafe.AsRef(in _floor) = is7;
                break;
            case 8:
                if (value is ST st8) System.Runtime.CompilerServices.Unsafe.AsRef(in _locationDescription) = st8;
                break;
            case 9:
                if (value is EI ei9) System.Runtime.CompilerServices.Unsafe.AsRef(in _comprehensiveLocationIdentifier) = ei9;
                break;
            case 10:
                if (value is HD hd10) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd10;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _pointOfCare, delimiters, false);
        AppendComponent(sb, _room, delimiters);
        AppendComponent(sb, _bed, delimiters);
        AppendComponent(sb, _facility, delimiters);
        AppendComponent(sb, _locationStatus, delimiters);
        AppendComponent(sb, _personLocationType, delimiters);
        AppendComponent(sb, _building, delimiters);
        AppendComponent(sb, _floor, delimiters);
        AppendComponent(sb, _locationDescription, delimiters);
        AppendComponent(sb, _comprehensiveLocationIdentifier, delimiters);
        AppendComponent(sb, _assigningAuthority, delimiters);
        
        return sb.ToString().TrimEnd(delimiters.ComponentSeparator);
    }

    private static void AppendComponent<T>(StringBuilder sb, T component, in HL7Delimiters delimiters, bool addSeparator = true) where T : IHL7DataType
    {
        if (addSeparator)
            sb.Append(delimiters.ComponentSeparator);
        if (!component.IsEmpty)
            sb.Append(component.ToHL7String(delimiters));
    }

    /// <inheritdoc/>
    public void Parse(ReadOnlySpan<char> value, in HL7Delimiters delimiters)
    {
        var enumerator = new ComponentEnumerator(value, delimiters.ComponentSeparator);
        int index = 0;

        while (enumerator.MoveNext() && index < ComponentCount)
        {
            var component = enumerator.Current;
            
            switch (index)
            {
                case 0:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _pointOfCare) = new IS(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _room) = new IS(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _bed) = new IS(component.ToString());
                    break;
                case 3:
                    var hd3 = new HD();
                    hd3.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _facility) = hd3;
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _locationStatus) = new IS(component.ToString());
                    break;
                case 5:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _personLocationType) = new IS(component.ToString());
                    break;
                case 6:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _building) = new IS(component.ToString());
                    break;
                case 7:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _floor) = new IS(component.ToString());
                    break;
                case 8:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _locationDescription) = new ST(component.ToString());
                    break;
                case 9:
                    var ei = new EI();
                    ei.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _comprehensiveLocationIdentifier) = ei;
                    break;
                case 10:
                    var hd10 = new HD();
                    hd10.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd10;
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_pointOfCare.Validate(out var pocErrors))
            errors.AddRange(pocErrors.Select(e => $"PointOfCare: {e}"));
        if (!_room.Validate(out var roomErrors))
            errors.AddRange(roomErrors.Select(e => $"Room: {e}"));
        if (!_bed.Validate(out var bedErrors))
            errors.AddRange(bedErrors.Select(e => $"Bed: {e}"));
        if (!_facility.Validate(out var facilityErrors))
            errors.AddRange(facilityErrors.Select(e => $"Facility: {e}"));
        if (!_locationStatus.Validate(out var lsErrors))
            errors.AddRange(lsErrors.Select(e => $"LocationStatus: {e}"));
        if (!_personLocationType.Validate(out var pltErrors))
            errors.AddRange(pltErrors.Select(e => $"PersonLocationType: {e}"));
        if (!_building.Validate(out var buildingErrors))
            errors.AddRange(buildingErrors.Select(e => $"Building: {e}"));
        if (!_floor.Validate(out var floorErrors))
            errors.AddRange(floorErrors.Select(e => $"Floor: {e}"));
        if (!_locationDescription.Validate(out var ldErrors))
            errors.AddRange(ldErrors.Select(e => $"LocationDescription: {e}"));
        if (!_comprehensiveLocationIdentifier.Validate(out var cliErrors))
            errors.AddRange(cliErrors.Select(e => $"ComprehensiveLocationIdentifier: {e}"));
        if (!_assigningAuthority.Validate(out var aaErrors))
            errors.AddRange(aaErrors.Select(e => $"AssigningAuthority: {e}"));

        return errors.Count == 0;
    }

    /// <summary>
    /// Gets a formatted representation of the location.
    /// </summary>
    /// <returns>The formatted location string.</returns>
    public string GetFormattedLocation()
    {
        var parts = new List<string>();
        
        if (!_pointOfCare.IsEmpty)
            parts.Add(_pointOfCare.Value);
        
        if (!_room.IsEmpty)
            parts.Add($"Room {_room.Value}");
        
        if (!_bed.IsEmpty)
            parts.Add($"Bed {_bed.Value}");
        
        if (!_building.IsEmpty)
            parts.Add($"Building {_building.Value}");
        
        if (!_floor.IsEmpty)
            parts.Add($"Floor {_floor.Value}");
        
        if (!_facility.IsEmpty)
            parts.Add(_facility.ToString());
        
        return string.Join(", ", parts);
    }

    /// <inheritdoc/>
    public override string ToString() => GetFormattedLocation();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is PL other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to PL.
    /// </summary>
    public static implicit operator PL(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var pl = new PL();
        pl.Parse(value.AsSpan(), HL7Delimiters.Default);
        return pl;
    }

    /// <summary>
    /// Implicit conversion from PL to string.
    /// </summary>
    public static implicit operator string(PL pl) => pl.ToString();
}
