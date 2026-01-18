using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Specimen Container Detail
    /// </summary>
    public class SAC : ISegment
    {
        public string SegmentId => nameof(SAC);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// SAC.1 - External Accession Identifier
        /// </summary>
        public EI ExternalAccessionIdentifier { get; set; }

        /// <summary>
        /// SAC.2 - Accession Identifier
        /// </summary>
        public EI AccessionIdentifier { get; set; }

        /// <summary>
        /// SAC.3 - Container Identifier
        /// </summary>
        public EI ContainerIdentifier { get; set; }

        /// <summary>
        /// SAC.4 - Primary (parent) Container Identifier
        /// </summary>
        public EI PrimaryContainerIdentifier { get; set; }

        /// <summary>
        /// SAC.5 - Equipment Container Identifier
        /// </summary>
        public EI EquipmentContainerIdentifier { get; set; }

        /// <summary>
        /// SAC.6 - Specimen Source
        /// </summary>
        public ST SpecimenSource { get; set; }

        /// <summary>
        /// SAC.7 - Registration Date/Time
        /// </summary>
        public DTM RegistrationDateTime { get; set; }

        /// <summary>
        /// SAC.8 - Container Status
        /// </summary>
        public CE ContainerStatus { get; set; }

        /// <summary>
        /// SAC.9 - Carrier Type
        /// </summary>
        public CE CarrierType { get; set; }

        /// <summary>
        /// SAC.10 - Carrier Identifier
        /// </summary>
        public EI CarrierIdentifier { get; set; }

        /// <summary>
        /// SAC.11 - Position in Carrier
        /// </summary>
        public ST PositionInCarrier { get; set; }

        /// <summary>
        /// SAC.12 - Tray Type - SAC
        /// </summary>
        public CE TrayTypeSAC { get; set; }

        /// <summary>
        /// SAC.13 - Tray Identifier
        /// </summary>
        public EI TrayIdentifier { get; set; }

        /// <summary>
        /// SAC.14 - Position in Tray
        /// </summary>
        public ST PositionInTray { get; set; }

        /// <summary>
        /// SAC.15 - Location (repeating)
        /// </summary>
        public CE[] Location { get; set; }

        /// <summary>
        /// SAC.16 - Container Height
        /// </summary>
        public NM ContainerHeight { get; set; }

        /// <summary>
        /// SAC.17 - Container Diameter
        /// </summary>
        public NM ContainerDiameter { get; set; }

        /// <summary>
        /// SAC.18 - Barrier Delta
        /// </summary>
        public NM BarrierDelta { get; set; }

        /// <summary>
        /// SAC.19 - Bottom Delta
        /// </summary>
        public NM BottomDelta { get; set; }

        /// <summary>
        /// SAC.20 - Container Height/Diameter/Delta Units
        /// </summary>
        public CE ContainerHeightDiameterDeltaUnits { get; set; }

        /// <summary>
        /// SAC.21 - Container Volume
        /// </summary>
        public NM ContainerVolume { get; set; }

        /// <summary>
        /// SAC.22 - Available Specimen Volume
        /// </summary>
        public NM AvailableSpecimenVolume { get; set; }

        /// <summary>
        /// SAC.23 - Initial Specimen Volume
        /// </summary>
        public NM InitialSpecimenVolume { get; set; }

        /// <summary>
        /// SAC.24 - Volume Units
        /// </summary>
        public CE VolumeUnits { get; set; }

        /// <summary>
        /// SAC.25 - Separator Type
        /// </summary>
        public CE SeparatorType { get; set; }

        /// <summary>
        /// SAC.26 - Cap Type
        /// </summary>
        public CE CapType { get; set; }

        /// <summary>
        /// SAC.27 - Additive (repeating)
        /// </summary>
        public CWE[] Additive { get; set; }

        /// <summary>
        /// SAC.28 - Specimen Component
        /// </summary>
        public CE SpecimenComponent { get; set; }

        /// <summary>
        /// SAC.29 - Dilution Factor
        /// </summary>
        public NM DilutionFactor { get; set; }

        /// <summary>
        /// SAC.30 - Treatment
        /// </summary>
        public CE Treatment { get; set; }

        /// <summary>
        /// SAC.31 - Temperature
        /// </summary>
        public NM Temperature { get; set; }

        /// <summary>
        /// SAC.32 - Hemolysis Index
        /// </summary>
        public NM HemolysisIndex { get; set; }

        /// <summary>
        /// SAC.33 - Hemolysis Index Units
        /// </summary>
        public CE HemolysisIndexUnits { get; set; }

        /// <summary>
        /// SAC.34 - Lipemia Index
        /// </summary>
        public NM LipemiaIndex { get; set; }

        /// <summary>
        /// SAC.35 - Lipemia Index Units
        /// </summary>
        public CE LipemiaIndexUnits { get; set; }

        /// <summary>
        /// SAC.36 - Icterus Index
        /// </summary>
        public NM IcterusIndex { get; set; }

        /// <summary>
        /// SAC.37 - Icterus Index Units
        /// </summary>
        public CE IcterusIndexUnits { get; set; }

        /// <summary>
        /// SAC.38 - Fibrin Index
        /// </summary>
        public NM FibrinIndex { get; set; }

        /// <summary>
        /// SAC.39 - Fibrin Index Units
        /// </summary>
        public CE FibrinIndexUnits { get; set; }

        /// <summary>
        /// SAC.40 - System Induced Contaminants (repeating)
        /// </summary>
        public CE[] SystemInducedContaminants { get; set; }

        /// <summary>
        /// SAC.41 - Drug Interference (repeating)
        /// </summary>
        public CE[] DrugInterference { get; set; }

        /// <summary>
        /// SAC.42 - Artificial Blood
        /// </summary>
        public CE ArtificialBlood { get; set; }

        /// <summary>
        /// SAC.43 - Special Handling Code (repeating)
        /// </summary>
        public CWE[] SpecialHandlingCode { get; set; }

        /// <summary>
        /// SAC.44 - Other Environmental Factors (repeating)
        /// </summary>
        public CE[] OtherEnvironmentalFactors { get; set; }

        public SAC()
        {
            SegmentType = SegmentType.Universal;
            ExternalAccessionIdentifier = default;
            AccessionIdentifier = default;
            ContainerIdentifier = default;
            PrimaryContainerIdentifier = default;
            EquipmentContainerIdentifier = default;
            SpecimenSource = default;
            RegistrationDateTime = default;
            ContainerStatus = default;
            CarrierType = default;
            CarrierIdentifier = default;
            PositionInCarrier = default;
            TrayTypeSAC = default;
            TrayIdentifier = default;
            PositionInTray = default;
            Location = [];
            ContainerHeight = default;
            ContainerDiameter = default;
            BarrierDelta = default;
            BottomDelta = default;
            ContainerHeightDiameterDeltaUnits = default;
            ContainerVolume = default;
            AvailableSpecimenVolume = default;
            InitialSpecimenVolume = default;
            VolumeUnits = default;
            SeparatorType = default;
            CapType = default;
            Additive = [];
            SpecimenComponent = default;
            DilutionFactor = default;
            Treatment = default;
            Temperature = default;
            HemolysisIndex = default;
            HemolysisIndexUnits = default;
            LipemiaIndex = default;
            LipemiaIndexUnits = default;
            IcterusIndex = default;
            IcterusIndexUnits = default;
            FibrinIndex = default;
            FibrinIndexUnits = default;
            SystemInducedContaminants = [];
            DrugInterference = [];
            ArtificialBlood = default;
            SpecialHandlingCode = [];
            OtherEnvironmentalFactors = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ei1 = new EI();
                    ei1.Parse(value.AsSpan(), delimiters);
                    ExternalAccessionIdentifier = ei1;
                    break;
                case 2:
                    var ei2 = new EI();
                    ei2.Parse(value.AsSpan(), delimiters);
                    AccessionIdentifier = ei2;
                    break;
                case 3:
                    var ei3 = new EI();
                    ei3.Parse(value.AsSpan(), delimiters);
                    ContainerIdentifier = ei3;
                    break;
                case 4:
                    var ei4 = new EI();
                    ei4.Parse(value.AsSpan(), delimiters);
                    PrimaryContainerIdentifier = ei4;
                    break;
                case 5:
                    var ei5 = new EI();
                    ei5.Parse(value.AsSpan(), delimiters);
                    EquipmentContainerIdentifier = ei5;
                    break;
                case 6: SpecimenSource = new ST(value); break;
                case 7: RegistrationDateTime = new DTM(value); break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    ContainerStatus = ce8;
                    break;
                case 9:
                    var ce9 = new CE();
                    ce9.Parse(value.AsSpan(), delimiters);
                    CarrierType = ce9;
                    break;
                case 10:
                    var ei10 = new EI();
                    ei10.Parse(value.AsSpan(), delimiters);
                    CarrierIdentifier = ei10;
                    break;
                case 11: PositionInCarrier = new ST(value); break;
                case 12:
                    var ce12 = new CE();
                    ce12.Parse(value.AsSpan(), delimiters);
                    TrayTypeSAC = ce12;
                    break;
                case 13:
                    var ei13 = new EI();
                    ei13.Parse(value.AsSpan(), delimiters);
                    TrayIdentifier = ei13;
                    break;
                case 14: PositionInTray = new ST(value); break;
                case 15: Location = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 16: ContainerHeight = new NM(value); break;
                case 17: ContainerDiameter = new NM(value); break;
                case 18: BarrierDelta = new NM(value); break;
                case 19: BottomDelta = new NM(value); break;
                case 20:
                    var ce20 = new CE();
                    ce20.Parse(value.AsSpan(), delimiters);
                    ContainerHeightDiameterDeltaUnits = ce20;
                    break;
                case 21: ContainerVolume = new NM(value); break;
                case 22: AvailableSpecimenVolume = new NM(value); break;
                case 23: InitialSpecimenVolume = new NM(value); break;
                case 24:
                    var ce24 = new CE();
                    ce24.Parse(value.AsSpan(), delimiters);
                    VolumeUnits = ce24;
                    break;
                case 25:
                    var ce25 = new CE();
                    ce25.Parse(value.AsSpan(), delimiters);
                    SeparatorType = ce25;
                    break;
                case 26:
                    var ce26 = new CE();
                    ce26.Parse(value.AsSpan(), delimiters);
                    CapType = ce26;
                    break;
                case 27: Additive = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 28:
                    var ce28 = new CE();
                    ce28.Parse(value.AsSpan(), delimiters);
                    SpecimenComponent = ce28;
                    break;
                case 29: DilutionFactor = new NM(value); break;
                case 30:
                    var ce30 = new CE();
                    ce30.Parse(value.AsSpan(), delimiters);
                    Treatment = ce30;
                    break;
                case 31: Temperature = new NM(value); break;
                case 32: HemolysisIndex = new NM(value); break;
                case 33:
                    var ce33 = new CE();
                    ce33.Parse(value.AsSpan(), delimiters);
                    HemolysisIndexUnits = ce33;
                    break;
                case 34: LipemiaIndex = new NM(value); break;
                case 35:
                    var ce35 = new CE();
                    ce35.Parse(value.AsSpan(), delimiters);
                    LipemiaIndexUnits = ce35;
                    break;
                case 36: IcterusIndex = new NM(value); break;
                case 37:
                    var ce37 = new CE();
                    ce37.Parse(value.AsSpan(), delimiters);
                    IcterusIndexUnits = ce37;
                    break;
                case 38: FibrinIndex = new NM(value); break;
                case 39:
                    var ce39 = new CE();
                    ce39.Parse(value.AsSpan(), delimiters);
                    FibrinIndexUnits = ce39;
                    break;
                case 40: SystemInducedContaminants = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 41: DrugInterference = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 42:
                    var ce42 = new CE();
                    ce42.Parse(value.AsSpan(), delimiters);
                    ArtificialBlood = ce42;
                    break;
                case 43: SpecialHandlingCode = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 44: OtherEnvironmentalFactors = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                ExternalAccessionIdentifier.ToHL7String(delimiters),
                AccessionIdentifier.ToHL7String(delimiters),
                ContainerIdentifier.ToHL7String(delimiters),
                PrimaryContainerIdentifier.ToHL7String(delimiters),
                EquipmentContainerIdentifier.ToHL7String(delimiters),
                SpecimenSource.ToHL7String(delimiters),
                RegistrationDateTime.ToHL7String(delimiters),
                ContainerStatus.ToHL7String(delimiters),
                CarrierType.ToHL7String(delimiters),
                CarrierIdentifier.ToHL7String(delimiters),
                PositionInCarrier.ToHL7String(delimiters),
                TrayTypeSAC.ToHL7String(delimiters),
                TrayIdentifier.ToHL7String(delimiters),
                PositionInTray.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Location, delimiters),
                ContainerHeight.ToHL7String(delimiters),
                ContainerDiameter.ToHL7String(delimiters),
                BarrierDelta.ToHL7String(delimiters),
                BottomDelta.ToHL7String(delimiters),
                ContainerHeightDiameterDeltaUnits.ToHL7String(delimiters),
                ContainerVolume.ToHL7String(delimiters),
                AvailableSpecimenVolume.ToHL7String(delimiters),
                InitialSpecimenVolume.ToHL7String(delimiters),
                VolumeUnits.ToHL7String(delimiters),
                SeparatorType.ToHL7String(delimiters),
                CapType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Additive, delimiters),
                SpecimenComponent.ToHL7String(delimiters),
                DilutionFactor.ToHL7String(delimiters),
                Treatment.ToHL7String(delimiters),
                Temperature.ToHL7String(delimiters),
                HemolysisIndex.ToHL7String(delimiters),
                HemolysisIndexUnits.ToHL7String(delimiters),
                LipemiaIndex.ToHL7String(delimiters),
                LipemiaIndexUnits.ToHL7String(delimiters),
                IcterusIndex.ToHL7String(delimiters),
                IcterusIndexUnits.ToHL7String(delimiters),
                FibrinIndex.ToHL7String(delimiters),
                FibrinIndexUnits.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SystemInducedContaminants, delimiters),
                SegmentFieldHelper.JoinRepeatingField(DrugInterference, delimiters),
                ArtificialBlood.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecialHandlingCode, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OtherEnvironmentalFactors, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => ExternalAccessionIdentifier.ToHL7String(delimiters),
                2 => AccessionIdentifier.ToHL7String(delimiters),
                3 => ContainerIdentifier.ToHL7String(delimiters),
                4 => PrimaryContainerIdentifier.ToHL7String(delimiters),
                5 => EquipmentContainerIdentifier.ToHL7String(delimiters),
                6 => SpecimenSource.Value,
                7 => RegistrationDateTime.Value,
                8 => ContainerStatus.ToHL7String(delimiters),
                9 => CarrierType.ToHL7String(delimiters),
                10 => CarrierIdentifier.ToHL7String(delimiters),
                11 => PositionInCarrier.Value,
                12 => TrayTypeSAC.ToHL7String(delimiters),
                13 => TrayIdentifier.ToHL7String(delimiters),
                14 => PositionInTray.Value,
                15 => SegmentFieldHelper.JoinRepeatingField(Location, delimiters),
                16 => ContainerHeight.Value,
                17 => ContainerDiameter.Value,
                18 => BarrierDelta.Value,
                19 => BottomDelta.Value,
                20 => ContainerHeightDiameterDeltaUnits.ToHL7String(delimiters),
                21 => ContainerVolume.Value,
                22 => AvailableSpecimenVolume.Value,
                23 => InitialSpecimenVolume.Value,
                24 => VolumeUnits.ToHL7String(delimiters),
                25 => SeparatorType.ToHL7String(delimiters),
                26 => CapType.ToHL7String(delimiters),
                27 => SegmentFieldHelper.JoinRepeatingField(Additive, delimiters),
                28 => SpecimenComponent.ToHL7String(delimiters),
                29 => DilutionFactor.Value,
                30 => Treatment.ToHL7String(delimiters),
                31 => Temperature.Value,
                32 => HemolysisIndex.Value,
                33 => HemolysisIndexUnits.ToHL7String(delimiters),
                34 => LipemiaIndex.Value,
                35 => LipemiaIndexUnits.ToHL7String(delimiters),
                36 => IcterusIndex.Value,
                37 => IcterusIndexUnits.ToHL7String(delimiters),
                38 => FibrinIndex.Value,
                39 => FibrinIndexUnits.ToHL7String(delimiters),
                40 => SegmentFieldHelper.JoinRepeatingField(SystemInducedContaminants, delimiters),
                41 => SegmentFieldHelper.JoinRepeatingField(DrugInterference, delimiters),
                42 => ArtificialBlood.ToHL7String(delimiters),
                43 => SegmentFieldHelper.JoinRepeatingField(SpecialHandlingCode, delimiters),
                44 => SegmentFieldHelper.JoinRepeatingField(OtherEnvironmentalFactors, delimiters),
                _ => null
            };
        }
    }
}
