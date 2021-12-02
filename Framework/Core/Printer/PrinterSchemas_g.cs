/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * PrinterSchemas_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XFS4IoT.Printer
{

    public enum PaperSupplyEnum
    {
        NotSupported,
        Unknown,
        Full,
        Low,
        Out,
        Jammed
    }


    public enum PaperTypeEnum
    {
        Unknown,
        Single,
        Dual
    }


    [DataContract]
    public sealed class StatusClass
    {
        public StatusClass(MediaEnum? Media = null, PaperClass Paper = null, TonerEnum? Toner = null, InkEnum? Ink = null, LampEnum? Lamp = null, List<RetractBinsClass> RetractBins = null, int? MediaOnStacker = null, PaperTypeClass PaperType = null, BlackMarkModeEnum? BlackMarkMode = null)
        {
            this.Media = Media;
            this.Paper = Paper;
            this.Toner = Toner;
            this.Ink = Ink;
            this.Lamp = Lamp;
            this.RetractBins = RetractBins;
            this.MediaOnStacker = MediaOnStacker;
            this.PaperType = PaperType;
            this.BlackMarkMode = BlackMarkMode;
        }

        public enum MediaEnum
        {
            NotSupported,
            Unknown,
            Present,
            NotPresent,
            Jammed,
            Entering,
            Retracted
        }

        /// <summary>
        /// Specifies the state of the print media (i.e. receipt, statement, passbook, etc.) as one of the following
        /// values. This field does not apply to journal printers:
        /// 
        /// * ```notSupported``` - The capability to report the state of the print media is not supported by the device.
        /// * ```unknown``` - The state of the print media cannot be determined with the device in its current state.
        /// * ```present``` - Media is in the print position, on the stacker or on the transport (i.e. a passbook in the
        ///   parking station is not considered to be present). On devices with continuous paper supplies, this value is
        ///   set when paper is under the print head. On devices with no supply or individual sheet supplies, this value
        ///   is set when paper/media is successfully inserted/loaded.
        /// * ```notPresent``` - Media is not in the print position or on the stacker.
        /// * ```jammed``` - Media is jammed in the device.
        /// * ```entering``` - Media is at the entry/exit slot of the device.
        /// * ```retracted``` - Media was retracted during the last command which controlled media.
        /// </summary>
        [DataMember(Name = "media")]
        public MediaEnum? Media { get; init; }

        [DataContract]
        public sealed class PaperClass
        {
            public PaperClass(PaperSupplyEnum? Upper = null, PaperSupplyEnum? Lower = null, PaperSupplyEnum? External = null, PaperSupplyEnum? Aux = null, PaperSupplyEnum? Aux2 = null, PaperSupplyEnum? Park = null)
            {
                this.Upper = Upper;
                this.Lower = Lower;
                this.External = External;
                this.Aux = Aux;
                this.Aux2 = Aux2;
                this.Park = Park;
            }

            /// <summary>
            /// The state of the upper paper supply.
            /// </summary>
            [DataMember(Name = "upper")]
            public PaperSupplyEnum? Upper { get; init; }

            /// <summary>
            /// The state of the lower paper supply.
            /// </summary>
            [DataMember(Name = "lower")]
            public PaperSupplyEnum? Lower { get; init; }

            /// <summary>
            /// The state of the external paper supply.
            /// </summary>
            [DataMember(Name = "external")]
            public PaperSupplyEnum? External { get; init; }

            /// <summary>
            /// The state of the auxiliary paper supply.
            /// </summary>
            [DataMember(Name = "aux")]
            public PaperSupplyEnum? Aux { get; init; }

            /// <summary>
            /// The state of the second auxiliary paper supply.
            /// </summary>
            [DataMember(Name = "aux2")]
            public PaperSupplyEnum? Aux2 { get; init; }

            /// <summary>
            /// The state of the parking station paper supply.
            /// </summary>
            [DataMember(Name = "park")]
            public PaperSupplyEnum? Park { get; init; }

            [System.Text.Json.Serialization.JsonExtensionData]
            public Dictionary<string, System.Text.Json.JsonElement> ExtensionData { get; set; } = new();

            [System.Text.Json.Serialization.JsonIgnore]
            public Dictionary<string, PaperSupplyEnum> ExtendedProperties
            {
                get => MessageBase.ParseExtendedProperties<PaperSupplyEnum>(ExtensionData);
                set => ExtensionData = MessageBase.CreateExtensionData<PaperSupplyEnum>(value);
            }

        }

        /// <summary>
        /// Specifies the state of paper supplies as one of the following values:
        /// 
        /// * ```notSupported``` - Capability not supported by the device.
        /// * ```unknown``` - Status cannot be determined with device in its current state.
        /// * ```full``` - The paper supply is full.
        /// * ```low``` - The paper supply is low.
        /// * ```out``` - The paper supply is empty.
        /// * ```jammed``` - The paper supply is jammed.
        /// </summary>
        [DataMember(Name = "paper")]
        public PaperClass Paper { get; init; }

        public enum TonerEnum
        {
            NotSupported,
            Unknown,
            Full,
            Low,
            Out
        }

        /// <summary>
        /// Specifies the state of the toner or ink supply or the state of the ribbon as one of the following:
        /// 
        /// * ```notSupported``` - Capability not supported by device.
        /// * ```unknown``` - Status of toner or ink supply or the ribbon cannot be determined with device in its 
        ///   current state.
        /// * ```full``` - The toner or ink supply is full or the ribbon is OK.
        /// * ```low``` - The toner or ink supply is low or the print contrast with a ribbon is weak.
        /// * ```out``` - The toner or ink supply is empty or the print contrast with a ribbon is not sufficient any
        ///   more.
        /// </summary>
        [DataMember(Name = "toner")]
        public TonerEnum? Toner { get; init; }

        public enum InkEnum
        {
            NotSupported,
            Unknown,
            Full,
            Low,
            Out
        }

        /// <summary>
        /// Specifies the status of the stamping ink in the printer as one of the following values:
        /// 
        /// * ```notSupported``` - Capability not supported by device.
        /// * ```unknown``` - Status of the stamping ink supply cannot be determined with device in its current state.
        /// * ```full``` - Ink supply in device is full.
        /// * ```low``` - Ink supply in device is low.
        /// * ```out``` - Ink supply in device is empty.
        /// </summary>
        [DataMember(Name = "ink")]
        public InkEnum? Ink { get; init; }

        public enum LampEnum
        {
            NotSupported,
            Unknown,
            Ok,
            Fading,
            Inop
        }

        /// <summary>
        /// Specifies the status of the printer imaging lamp as one of the following values:
        /// 
        /// * ```notSupported``` - Capability not supported by device.
        /// * ```unknown``` - Status of the imaging lamp cannot be determined with device in its current state.
        /// * ```ok``` - The lamp is OK.
        /// * ```fading``` - The lamp should be changed.
        /// * ```inop``` - The lamp is inoperative.
        /// </summary>
        [DataMember(Name = "lamp")]
        public LampEnum? Lamp { get; init; }

        [DataContract]
        public sealed class RetractBinsClass
        {
            public RetractBinsClass(StateEnum? State = null, int? Count = null)
            {
                this.State = State;
                this.Count = Count;
            }

            public enum StateEnum
            {
                Unknown,
                Ok,
                Full,
                High,
                Missing
            }

            /// <summary>
            /// Specifies the state of the printer retract bin as one of the following:
            /// 
            /// * ```ok``` - The retract bin of the printer is in a healthy state.
            /// * ```full``` - The retract bin of the printer is full.
            /// * ```unknown``` - Status cannot be determined with device in its current state.
            /// * ```high``` - The retract bin of the printer is nearly full.
            /// * ```missing``` - The retract bin is missing.
            /// </summary>
            [DataMember(Name = "state")]
            public StateEnum? State { get; init; }

            /// <summary>
            /// The number of media retracted to this bin. This value is persistent; it may be reset to zero by the
            /// [Printer.ResetCount](#printer.resetcount) command.
            /// </summary>
            [DataMember(Name = "count")]
            public int? Count { get; init; }

        }

        /// <summary>
        /// An array of bin state objects. If no retain bins are supported, the array will be empty.
        /// </summary>
        [DataMember(Name = "retractBins")]
        public List<RetractBinsClass> RetractBins { get; init; }

        /// <summary>
        /// The number of media on stacker; applicable only to printers with stacking capability.
        /// </summary>
        [DataMember(Name = "mediaOnStacker")]
        public int? MediaOnStacker { get; init; }

        [DataContract]
        public sealed class PaperTypeClass
        {
            public PaperTypeClass(PaperTypeEnum? Upper = null, PaperTypeEnum? Lower = null, PaperTypeEnum? External = null, PaperTypeEnum? Aux = null, PaperTypeEnum? Aux2 = null, PaperTypeEnum? Park = null)
            {
                this.Upper = Upper;
                this.Lower = Lower;
                this.External = External;
                this.Aux = Aux;
                this.Aux2 = Aux2;
                this.Park = Park;
            }

            /// <summary>
            /// The upper paper supply paper type.
            /// </summary>
            [DataMember(Name = "upper")]
            public PaperTypeEnum? Upper { get; init; }

            /// <summary>
            /// The lower paper supply paper type.
            /// </summary>
            [DataMember(Name = "lower")]
            public PaperTypeEnum? Lower { get; init; }

            /// <summary>
            /// The external paper supply paper type.
            /// </summary>
            [DataMember(Name = "external")]
            public PaperTypeEnum? External { get; init; }

            /// <summary>
            /// The auxililliary paper supply paper type.
            /// </summary>
            [DataMember(Name = "aux")]
            public PaperTypeEnum? Aux { get; init; }

            /// <summary>
            /// The second auxililliary paper supply paper type.
            /// </summary>
            [DataMember(Name = "aux2")]
            public PaperTypeEnum? Aux2 { get; init; }

            /// <summary>
            /// The parking station paper supply paper type.
            /// </summary>
            [DataMember(Name = "park")]
            public PaperTypeEnum? Park { get; init; }

            [System.Text.Json.Serialization.JsonExtensionData]
            public Dictionary<string, System.Text.Json.JsonElement> ExtensionData { get; set; } = new();

            [System.Text.Json.Serialization.JsonIgnore]
            public Dictionary<string, PaperTypeEnum> ExtendedProperties
            {
                get => MessageBase.ParseExtendedProperties<PaperTypeEnum>(ExtensionData);
                set => ExtensionData = MessageBase.CreateExtensionData<PaperTypeEnum>(value);
            }

        }

        /// <summary>
        /// Specifies the type of paper loaded as one of the following:
        /// 
        /// * ```unknown``` - No paper is loaded, reporting of this paper type is not supported or the paper type cannot
        ///   be determined.
        /// * ```single``` - The paper can be printed on only one side.
        /// * ```dual``` - The paper can be printed on both sides.
        /// </summary>
        [DataMember(Name = "paperType")]
        public PaperTypeClass PaperType { get; init; }

        public enum BlackMarkModeEnum
        {
            NotSupported,
            Unknown,
            On,
            Off
        }

        /// <summary>
        /// Specifies the status of the black mark detection and associated functionality:
        /// 
        /// * ```notSupported``` - Black mark detection is not supported.
        /// * ```unknown``` - The status of the black mark detection cannot be determined.
        /// * ```on``` - Black mark detection and associated functionality is switched on.
        /// * ```off``` - Black mark detection and associated functionality is switched off.
        /// </summary>
        [DataMember(Name = "blackMarkMode")]
        public BlackMarkModeEnum? BlackMarkMode { get; init; }

    }


    [DataContract]
    public sealed class PaperSourcesClass
    {
        public PaperSourcesClass(bool? Upper = null, bool? Lower = null, bool? External = null, bool? Aux = null, bool? Aux2 = null, bool? Park = null)
        {
            this.Upper = Upper;
            this.Lower = Lower;
            this.External = External;
            this.Aux = Aux;
            this.Aux2 = Aux2;
            this.Park = Park;
        }

        /// <summary>
        /// The upper paper source.
        /// </summary>
        [DataMember(Name = "upper")]
        public bool? Upper { get; init; }

        /// <summary>
        /// The lower paper source.
        /// </summary>
        [DataMember(Name = "lower")]
        public bool? Lower { get; init; }

        /// <summary>
        /// The external paper source.
        /// </summary>
        [DataMember(Name = "external")]
        public bool? External { get; init; }

        /// <summary>
        /// The auxiliary paper source.
        /// </summary>
        [DataMember(Name = "aux")]
        public bool? Aux { get; init; }

        /// <summary>
        /// The second auxiliary paper source.
        /// </summary>
        [DataMember(Name = "aux2")]
        public bool? Aux2 { get; init; }

        /// <summary>
        /// The parking station.
        /// </summary>
        [DataMember(Name = "park")]
        public bool? Park { get; init; }

        [DataTypes(Pattern = @"^[a-zA-Z]([a-zA-Z0-9]*)$")]
        [System.Text.Json.Serialization.JsonExtensionData]
        public Dictionary<string, System.Text.Json.JsonElement> ExtensionData { get; set; } = new();

        [DataTypes(Pattern = @"^[a-zA-Z]([a-zA-Z0-9]*)$")]
        [System.Text.Json.Serialization.JsonIgnore]
        public Dictionary<string, bool> ExtendedProperties
        {
            get => MessageBase.ParseExtendedProperties<bool>(ExtensionData);
            set => ExtensionData = MessageBase.CreateExtensionData<bool>(value);
        }

    }


    [DataContract]
    public sealed class CapabilitiesClass
    {
        public CapabilitiesClass(TypeClass Type = null, ResolutionClass Resolution = null, ReadFormClass ReadForm = null, WriteFormClass WriteForm = null, ExtentsClass Extents = null, ControlClass Control = null, int? MaxMediaOnStacker = null, bool? AcceptMedia = null, bool? MultiPage = null, PaperSourcesClass PaperSources = null, bool? MediaTaken = null, int? RetractBins = null, List<int> MaxRetract = null, ImageTypeClass ImageType = null, FrontImageColorFormatClass FrontImageColorFormat = null, BackImageColorFormatClass BackImageColorFormat = null, CodelineFormatClass CodelineFormat = null, ImageSourceClass ImageSource = null, bool? DispensePaper = null, string OsPrinter = null, bool? MediaPresented = null, int? AutoRetractPeriod = null, bool? RetractToTransport = null, CoercivityTypeClass CoercivityType = null, ControlPassbookClass ControlPassbook = null, PrintSidesEnum? PrintSides = null)
        {
            this.Type = Type;
            this.Resolution = Resolution;
            this.ReadForm = ReadForm;
            this.WriteForm = WriteForm;
            this.Extents = Extents;
            this.Control = Control;
            this.MaxMediaOnStacker = MaxMediaOnStacker;
            this.AcceptMedia = AcceptMedia;
            this.MultiPage = MultiPage;
            this.PaperSources = PaperSources;
            this.MediaTaken = MediaTaken;
            this.RetractBins = RetractBins;
            this.MaxRetract = MaxRetract;
            this.ImageType = ImageType;
            this.FrontImageColorFormat = FrontImageColorFormat;
            this.BackImageColorFormat = BackImageColorFormat;
            this.CodelineFormat = CodelineFormat;
            this.ImageSource = ImageSource;
            this.DispensePaper = DispensePaper;
            this.OsPrinter = OsPrinter;
            this.MediaPresented = MediaPresented;
            this.AutoRetractPeriod = AutoRetractPeriod;
            this.RetractToTransport = RetractToTransport;
            this.CoercivityType = CoercivityType;
            this.ControlPassbook = ControlPassbook;
            this.PrintSides = PrintSides;
        }

        [DataContract]
        public sealed class TypeClass
        {
            public TypeClass(bool? Receipt = null, bool? Passbook = null, bool? Journal = null, bool? Document = null, bool? Scanner = null)
            {
                this.Receipt = Receipt;
                this.Passbook = Passbook;
                this.Journal = Journal;
                this.Document = Document;
                this.Scanner = Scanner;
            }

            /// <summary>
            /// The device is a receipt printer.
            /// </summary>
            [DataMember(Name = "receipt")]
            public bool? Receipt { get; init; }

            /// <summary>
            /// The device is a passbook printer.
            /// </summary>
            [DataMember(Name = "passbook")]
            public bool? Passbook { get; init; }

            /// <summary>
            /// The device is a journal printer.
            /// </summary>
            [DataMember(Name = "journal")]
            public bool? Journal { get; init; }

            /// <summary>
            /// The device is a document printer.
            /// </summary>
            [DataMember(Name = "document")]
            public bool? Document { get; init; }

            /// <summary>
            /// The device is a scanner that may have printing capabilities.
            /// </summary>
            [DataMember(Name = "scanner")]
            public bool? Scanner { get; init; }

        }

        /// <summary>
        /// Specifies the type(s) of the physical device driven by the logical service.
        /// </summary>
        [DataMember(Name = "type")]
        public TypeClass Type { get; init; }

        [DataContract]
        public sealed class ResolutionClass
        {
            public ResolutionClass(bool? Low = null, bool? Medium = null, bool? High = null, bool? VeryHigh = null)
            {
                this.Low = Low;
                this.Medium = Medium;
                this.High = High;
                this.VeryHigh = VeryHigh;
            }

            /// <summary>
            /// The device can print low resolution.
            /// </summary>
            [DataMember(Name = "low")]
            public bool? Low { get; init; }

            /// <summary>
            /// The device can print medium resolution.
            /// </summary>
            [DataMember(Name = "medium")]
            public bool? Medium { get; init; }

            /// <summary>
            /// The device can print high resolution.
            /// </summary>
            [DataMember(Name = "high")]
            public bool? High { get; init; }

            /// <summary>
            /// The device can print very high resolution.
            /// </summary>
            [DataMember(Name = "veryHigh")]
            public bool? VeryHigh { get; init; }

        }

        /// <summary>
        /// Specifies at which resolution(s) the physical device can print. Used by the application to select the level
        /// of print quality desired; does not imply any absolute level of resolution, only relative.
        /// </summary>
        [DataMember(Name = "resolution")]
        public ResolutionClass Resolution { get; init; }

        [DataContract]
        public sealed class ReadFormClass
        {
            public ReadFormClass(bool? Ocr = null, bool? Micr = null, bool? Msf = null, bool? Barcode = null, bool? PageMark = null, bool? ReadImage = null, bool? ReadEmptyLine = null)
            {
                this.Ocr = Ocr;
                this.Micr = Micr;
                this.Msf = Msf;
                this.Barcode = Barcode;
                this.PageMark = PageMark;
                this.ReadImage = ReadImage;
                this.ReadEmptyLine = ReadEmptyLine;
            }

            /// <summary>
            /// Device has OCR capability.
            /// </summary>
            [DataMember(Name = "ocr")]
            public bool? Ocr { get; init; }

            /// <summary>
            /// Device has MICR capability.
            /// </summary>
            [DataMember(Name = "micr")]
            public bool? Micr { get; init; }

            /// <summary>
            /// Device has MSF capability.
            /// </summary>
            [DataMember(Name = "msf")]
            public bool? Msf { get; init; }

            /// <summary>
            /// Device has Barcode capability.
            /// </summary>
            [DataMember(Name = "barcode")]
            public bool? Barcode { get; init; }

            /// <summary>
            /// Device has Page Mark capability.
            /// </summary>
            [DataMember(Name = "pageMark")]
            public bool? PageMark { get; init; }

            /// <summary>
            /// Device has imaging capability.
            /// </summary>
            [DataMember(Name = "readImage")]
            public bool? ReadImage { get; init; }

            /// <summary>
            /// Device has capability to detect empty print lines for passbook printing.
            /// </summary>
            [DataMember(Name = "readEmptyLine")]
            public bool? ReadEmptyLine { get; init; }

        }

        /// <summary>
        /// Specifies whether the device can read data from media, as a combination of the following flags.
        /// </summary>
        [DataMember(Name = "readForm")]
        public ReadFormClass ReadForm { get; init; }

        [DataContract]
        public sealed class WriteFormClass
        {
            public WriteFormClass(bool? Text = null, bool? Graphics = null, bool? Ocr = null, bool? Micr = null, bool? Msf = null, bool? Barcode = null, bool? Stamp = null)
            {
                this.Text = Text;
                this.Graphics = Graphics;
                this.Ocr = Ocr;
                this.Micr = Micr;
                this.Msf = Msf;
                this.Barcode = Barcode;
                this.Stamp = Stamp;
            }

            /// <summary>
            /// Device has Text capability.
            /// </summary>
            [DataMember(Name = "text")]
            public bool? Text { get; init; }

            /// <summary>
            /// Device has Graphics capability.
            /// </summary>
            [DataMember(Name = "graphics")]
            public bool? Graphics { get; init; }

            /// <summary>
            /// Device has OCR capability.
            /// </summary>
            [DataMember(Name = "ocr")]
            public bool? Ocr { get; init; }

            /// <summary>
            /// Device has MICR capability.
            /// </summary>
            [DataMember(Name = "micr")]
            public bool? Micr { get; init; }

            /// <summary>
            /// Device has MSF capability.
            /// </summary>
            [DataMember(Name = "msf")]
            public bool? Msf { get; init; }

            /// <summary>
            /// Device has Barcode capability.
            /// </summary>
            [DataMember(Name = "barcode")]
            public bool? Barcode { get; init; }

            /// <summary>
            /// Device has stamping capability.
            /// </summary>
            [DataMember(Name = "stamp")]
            public bool? Stamp { get; init; }

        }

        /// <summary>
        /// Specifies whether the device can write data to the media, as a combination of the following flags.
        /// </summary>
        [DataMember(Name = "writeForm")]
        public WriteFormClass WriteForm { get; init; }

        [DataContract]
        public sealed class ExtentsClass
        {
            public ExtentsClass(bool? Horizontal = null, bool? Vertical = null)
            {
                this.Horizontal = Horizontal;
                this.Vertical = Vertical;
            }

            /// <summary>
            /// Device has horizontal size detection capability.
            /// </summary>
            [DataMember(Name = "horizontal")]
            public bool? Horizontal { get; init; }

            /// <summary>
            /// Device has vertical size detection capability.
            /// </summary>
            [DataMember(Name = "vertical")]
            public bool? Vertical { get; init; }

        }

        /// <summary>
        /// Specifies whether the device is able to measure the inserted media, as a combination of the following flags.
        /// </summary>
        [DataMember(Name = "extents")]
        public ExtentsClass Extents { get; init; }

        [DataContract]
        public sealed class ControlClass
        {
            public ControlClass(bool? Eject = null, bool? Perforate = null, bool? Cut = null, bool? Skip = null, bool? Flush = null, bool? Retract = null, bool? Stack = null, bool? PartialCut = null, bool? Alarm = null, bool? PageForward = null, bool? PageBackward = null, bool? TurnMedia = null, bool? Stamp = null, bool? Park = null, bool? Expel = null, bool? EjectToTransport = null, bool? Rotate180 = null, bool? ClearBuffer = null)
            {
                this.Eject = Eject;
                this.Perforate = Perforate;
                this.Cut = Cut;
                this.Skip = Skip;
                this.Flush = Flush;
                this.Retract = Retract;
                this.Stack = Stack;
                this.PartialCut = PartialCut;
                this.Alarm = Alarm;
                this.PageForward = PageForward;
                this.PageBackward = PageBackward;
                this.TurnMedia = TurnMedia;
                this.Stamp = Stamp;
                this.Park = Park;
                this.Expel = Expel;
                this.EjectToTransport = EjectToTransport;
                this.Rotate180 = Rotate180;
                this.ClearBuffer = ClearBuffer;
            }

            /// <summary>
            /// Device can eject media.
            /// </summary>
            [DataMember(Name = "eject")]
            public bool? Eject { get; init; }

            /// <summary>
            /// Device can perforate media.
            /// </summary>
            [DataMember(Name = "perforate")]
            public bool? Perforate { get; init; }

            /// <summary>
            /// Device can cut media.
            /// </summary>
            [DataMember(Name = "cut")]
            public bool? Cut { get; init; }

            /// <summary>
            /// Device can skip to mark.
            /// </summary>
            [DataMember(Name = "skip")]
            public bool? Skip { get; init; }

            /// <summary>
            /// Device can be sent data that is buffered internally, and flushed to the printer on request.
            /// </summary>
            [DataMember(Name = "flush")]
            public bool? Flush { get; init; }

            /// <summary>
            /// Device can retract media under application control.
            /// </summary>
            [DataMember(Name = "retract")]
            public bool? Retract { get; init; }

            /// <summary>
            /// Device can stack media items before ejecting as a bundle.
            /// </summary>
            [DataMember(Name = "stack")]
            public bool? Stack { get; init; }

            /// <summary>
            /// Device can partially cut the media.
            /// </summary>
            [DataMember(Name = "partialCut")]
            public bool? PartialCut { get; init; }

            /// <summary>
            /// Device can ring a bell, beep or otherwise sound an audible alarm.
            /// </summary>
            [DataMember(Name = "alarm")]
            public bool? Alarm { get; init; }

            /// <summary>
            /// Capability to turn one page forward.
            /// </summary>
            [DataMember(Name = "pageForward")]
            public bool? PageForward { get; init; }

            /// <summary>
            /// Capability to turn one page backward.
            /// </summary>
            [DataMember(Name = "pageBackward")]
            public bool? PageBackward { get; init; }

            /// <summary>
            /// Device can turn inserted media.
            /// </summary>
            [DataMember(Name = "turnMedia")]
            public bool? TurnMedia { get; init; }

            /// <summary>
            /// Device can stamp on media.
            /// </summary>
            [DataMember(Name = "stamp")]
            public bool? Stamp { get; init; }

            /// <summary>
            /// Device can park a document into the parking station.
            /// </summary>
            [DataMember(Name = "park")]
            public bool? Park { get; init; }

            /// <summary>
            /// Device can expel media out of the exit slot.
            /// </summary>
            [DataMember(Name = "expel")]
            public bool? Expel { get; init; }

            /// <summary>
            /// Device can move media to a position on the transport just behind the exit slot.
            /// </summary>
            [DataMember(Name = "ejectToTransport")]
            public bool? EjectToTransport { get; init; }

            /// <summary>
            /// Device can rotate media 180 degrees in the printing plane.
            /// </summary>
            [DataMember(Name = "rotate180")]
            public bool? Rotate180 { get; init; }

            /// <summary>
            /// The Service Provider can clear buffered data.
            /// </summary>
            [DataMember(Name = "clearBuffer")]
            public bool? ClearBuffer { get; init; }

        }

        /// <summary>
        /// Specifies the manner in which media can be controlled, as a combination of the following flags.
        /// </summary>
        [DataMember(Name = "control")]
        public ControlClass Control { get; init; }

        /// <summary>
        /// Specifies the maximum number of media items that the stacker can hold (zero if not available).
        /// </summary>
        [DataMember(Name = "maxMediaOnStacker")]
        public int? MaxMediaOnStacker { get; init; }

        /// <summary>
        /// Specifies whether the device is able to accept media while no execute command is running that is waiting
        /// explicitly for media to be inserted.
        /// </summary>
        [DataMember(Name = "acceptMedia")]
        public bool? AcceptMedia { get; init; }

        /// <summary>
        /// Specifies whether the device is able to support multiple page print jobs.
        /// </summary>
        [DataMember(Name = "multiPage")]
        public bool? MultiPage { get; init; }

        /// <summary>
        /// Specifies the paper sources available for this printer as a combination of the following flags.
        /// </summary>
        [DataMember(Name = "paperSources")]
        public PaperSourcesClass PaperSources { get; init; }

        /// <summary>
        /// Specifies whether the device is able to detect when the media is taken from the exit slot. If false, the
        /// [Printer.MediaTakenEvent](#printer.mediatakenevent) event is not fired.
        /// </summary>
        [DataMember(Name = "mediaTaken")]
        public bool? MediaTaken { get; init; }

        /// <summary>
        /// Specifies the number of retract bins (zero if not supported).
        /// </summary>
        [DataMember(Name = "retractBins")]
        public int? RetractBins { get; init; }

        /// <summary>
        /// An array of the length [retractBins](#common.capabilities.completion.properties.printer.retractbins) with
        /// the maximum number of media items that each retract bin can hold (one count for each supported bin, starting
        /// from zero for bin number one to retractBins - 1 for bin number retractBins). This will be omitted if there
        /// are no retract bins.
        /// </summary>
        [DataMember(Name = "maxRetract")]
        public List<int> MaxRetract { get; init; }

        [DataContract]
        public sealed class ImageTypeClass
        {
            public ImageTypeClass(bool? Tif = null, bool? Wmf = null, bool? Bmp = null, bool? Jpg = null)
            {
                this.Tif = Tif;
                this.Wmf = Wmf;
                this.Bmp = Bmp;
                this.Jpg = Jpg;
            }

            /// <summary>
            /// The device can return scanned images in TIFF 6.0 format.
            /// </summary>
            [DataMember(Name = "tif")]
            public bool? Tif { get; init; }

            /// <summary>
            /// The device can return scanned images in WMF (Windows Metafile) format.
            /// </summary>
            [DataMember(Name = "wmf")]
            public bool? Wmf { get; init; }

            /// <summary>
            /// The device can return scanned images in Windows BMP format.
            /// </summary>
            [DataMember(Name = "bmp")]
            public bool? Bmp { get; init; }

            /// <summary>
            /// The device can return scanned images in JPG format.
            /// </summary>
            [DataMember(Name = "jpg")]
            public bool? Jpg { get; init; }

        }

        /// <summary>
        /// Specifies the image format supported by this device, as a combination of following flags.
        /// </summary>
        [DataMember(Name = "imageType")]
        public ImageTypeClass ImageType { get; init; }

        [DataContract]
        public sealed class FrontImageColorFormatClass
        {
            public FrontImageColorFormatClass(bool? Binary = null, bool? Grayscale = null, bool? Full = null)
            {
                this.Binary = Binary;
                this.Grayscale = Grayscale;
                this.Full = Full;
            }

            /// <summary>
            /// The device can return scanned images in binary (image contains two colors, usually the colors black and
            /// white).
            /// </summary>
            [DataMember(Name = "binary")]
            public bool? Binary { get; init; }

            /// <summary>
            /// The device can return scanned images in gray scale (image contains multiple gray colors).
            /// </summary>
            [DataMember(Name = "grayscale")]
            public bool? Grayscale { get; init; }

            /// <summary>
            /// The device can return scanned images in full color (image contains colors like red, green, blue etc.).
            /// </summary>
            [DataMember(Name = "full")]
            public bool? Full { get; init; }

        }

        /// <summary>
        /// Specifies the front image color formats supported by this device, as a combination of following flags.
        /// </summary>
        [DataMember(Name = "frontImageColorFormat")]
        public FrontImageColorFormatClass FrontImageColorFormat { get; init; }

        [DataContract]
        public sealed class BackImageColorFormatClass
        {
            public BackImageColorFormatClass(bool? Binary = null, bool? GrayScale = null, bool? Full = null)
            {
                this.Binary = Binary;
                this.GrayScale = GrayScale;
                this.Full = Full;
            }

            /// <summary>
            /// The device can return scanned images in binary (image contains two colors, usually the colors black and
            /// white).
            /// </summary>
            [DataMember(Name = "binary")]
            public bool? Binary { get; init; }

            /// <summary>
            /// The device can return scanned images in gray scale (image contains multiple gray colors).
            /// </summary>
            [DataMember(Name = "grayScale")]
            public bool? GrayScale { get; init; }

            /// <summary>
            /// The device can return scanned images in full color (image contains colors like red, green, blue etc.).
            /// </summary>
            [DataMember(Name = "full")]
            public bool? Full { get; init; }

        }

        /// <summary>
        /// Specifies the back image color formats supported by this device, as a combination of following flags.
        /// </summary>
        [DataMember(Name = "backImageColorFormat")]
        public BackImageColorFormatClass BackImageColorFormat { get; init; }

        [DataContract]
        public sealed class CodelineFormatClass
        {
            public CodelineFormatClass(bool? Cmc7 = null, bool? E13b = null, bool? Ocr = null)
            {
                this.Cmc7 = Cmc7;
                this.E13b = E13b;
                this.Ocr = Ocr;
            }

            /// <summary>
            /// The device can read CMC7 code lines.
            /// </summary>
            [DataMember(Name = "cmc7")]
            public bool? Cmc7 { get; init; }

            /// <summary>
            /// The device can read E13B code lines.
            /// </summary>
            [DataMember(Name = "e13b")]
            public bool? E13b { get; init; }

            /// <summary>
            /// The device can read code lines using Optical Character Recognition.
            /// </summary>
            [DataMember(Name = "ocr")]
            public bool? Ocr { get; init; }

        }

        /// <summary>
        /// Specifies the code line (MICR data) formats supported by this device, as a combination of following flags.
        /// </summary>
        [DataMember(Name = "codelineFormat")]
        public CodelineFormatClass CodelineFormat { get; init; }

        [DataContract]
        public sealed class ImageSourceClass
        {
            public ImageSourceClass(bool? ImageFront = null, bool? ImageBack = null, bool? CodeLine = null)
            {
                this.ImageFront = ImageFront;
                this.ImageBack = ImageBack;
                this.CodeLine = CodeLine;
            }

            /// <summary>
            /// The device can scan the front image of the document.
            /// </summary>
            [DataMember(Name = "imageFront")]
            public bool? ImageFront { get; init; }

            /// <summary>
            /// The device can scan the back image of the document.
            /// </summary>
            [DataMember(Name = "imageBack")]
            public bool? ImageBack { get; init; }

            /// <summary>
            /// The device can recognize the code line.
            /// </summary>
            [DataMember(Name = "codeLine")]
            public bool? CodeLine { get; init; }

        }

        /// <summary>
        /// Specifies the source for the read image command supported by this device, as a combination of the following
        /// flags.
        /// </summary>
        [DataMember(Name = "imageSource")]
        public ImageSourceClass ImageSource { get; init; }

        /// <summary>
        /// Specifies whether the device is able to dispense paper.
        /// </summary>
        [DataMember(Name = "dispensePaper")]
        public bool? DispensePaper { get; init; }

        /// <summary>
        /// Specifies the name of the default logical operating system printer that is associated with this Service
        /// Provider. Applications should use this printer name to generate native printer files to be printed through
        /// the [Printer.PrintNative](#printer.printnative) command. This value will be omitted if the Service
        /// Provider does not support the *Printer.PrintNative* command.
        /// </summary>
        [DataMember(Name = "osPrinter")]
        public string OsPrinter { get; init; }

        /// <summary>
        /// Specifies whether the device is able to detect when the media is presented to the user for removal. If true,
        /// the [Printer.MediaPresentedEvent](#printer.mediapresentedevent) event is fired. If false, the
        /// Printer.MediaPresentedEvent event is not fired.
        /// </summary>
        [DataMember(Name = "mediaPresented")]
        public bool? MediaPresented { get; init; }

        /// <summary>
        /// Specifies the number of seconds before the device will automatically retract the presented media. If the
        /// command that generated the media is still active when the media is automatically retracted, the command will
        /// complete with an error. If the device does not retract media automatically this value will be zero.
        /// </summary>
        [DataMember(Name = "autoRetractPeriod")]
        public int? AutoRetractPeriod { get; init; }

        /// <summary>
        /// Specifies whether the device is able to retract the previously ejected media to the transport.
        /// </summary>
        [DataMember(Name = "retractToTransport")]
        public bool? RetractToTransport { get; init; }

        [DataContract]
        public sealed class CoercivityTypeClass
        {
            public CoercivityTypeClass(bool? Low = null, bool? High = null, bool? Auto = null)
            {
                this.Low = Low;
                this.High = High;
                this.Auto = Auto;
            }

            /// <summary>
            /// This device can write the magnetic stripe by low coercivity mode.
            /// </summary>
            [DataMember(Name = "low")]
            public bool? Low { get; init; }

            /// <summary>
            /// This device can write the magnetic stripe by high coercivity mode.
            /// </summary>
            [DataMember(Name = "high")]
            public bool? High { get; init; }

            /// <summary>
            /// The Service Provider or the device is capable of automatically determining whether low or high
            /// coercivity magnetic stripe should be written.
            /// </summary>
            [DataMember(Name = "auto")]
            public bool? Auto { get; init; }

        }

        /// <summary>
        /// Specifies the form write modes supported by this device, as a combination of the following flags.
        /// </summary>
        [DataMember(Name = "coercivityType")]
        public CoercivityTypeClass CoercivityType { get; init; }

        [DataContract]
        public sealed class ControlPassbookClass
        {
            public ControlPassbookClass(bool? TurnForward = null, bool? TurnBackward = null, bool? CloseForward = null, bool? CloseBackward = null)
            {
                this.TurnForward = TurnForward;
                this.TurnBackward = TurnBackward;
                this.CloseForward = CloseForward;
                this.CloseBackward = CloseBackward;
            }

            /// <summary>
            /// The device can turn forward multiple pages of the passbook.
            /// </summary>
            [DataMember(Name = "turnForward")]
            public bool? TurnForward { get; init; }

            /// <summary>
            /// The device can turn backward multiple pages of the passbook.
            /// </summary>
            [DataMember(Name = "turnBackward")]
            public bool? TurnBackward { get; init; }

            /// <summary>
            /// The device can close the passbook forward.
            /// </summary>
            [DataMember(Name = "closeForward")]
            public bool? CloseForward { get; init; }

            /// <summary>
            /// The device can close the passbook backward.
            /// </summary>
            [DataMember(Name = "closeBackward")]
            public bool? CloseBackward { get; init; }

        }

        /// <summary>
        /// Specifies how the passbook can be controlled with the [Printer.ControlPassbook](#printer.controlpassbook)
        /// command, as a combination of the following flags.
        /// </summary>
        [DataMember(Name = "controlPassbook")]
        public ControlPassbookClass ControlPassbook { get; init; }

        public enum PrintSidesEnum
        {
            NotSupp,
            Single,
            Dual
        }

        /// <summary>
        /// Specifies on which sides of the media this device can print as one of the following values.
        /// 
        /// * ```notSupp``` - The device is not capable of printing on any sides of the media.
        /// * ```single``` - The device is capable of printing on one side of the media.
        /// * ```dual``` - The device is capable of printing on two sides of the media.
        /// </summary>
        [DataMember(Name = "printSides")]
        public PrintSidesEnum? PrintSides { get; init; }

    }


}
