/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Common;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.Common
{
    [CommandHandlerAsync]
    public partial class CapabilitiesHandler
    {

        private Task<CapabilitiesCompletion.PayloadData> HandleCapabilities(ICapabilitiesEvents events, CapabilitiesCommand capabilities, CancellationToken cancel)
        {
            if (Common.CommonCapabilities is null)
            {
                return Task.FromResult(new CapabilitiesCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InternalError, $"No common capabilities is reported by the device class."));
            }

            List<string> synchronizableCommands = new();
            List<InterfaceClass> interfaces = new();
            List<string> authenticationRequiredCommands = new();

            // The command and event versions are controlled by the framework, currently it's hardcoded.
            InterfaceClass.CommandsClass commandVersion = new (XFSConstants.SupportedVersionRange);
            InterfaceClass.EventsClass eventVersion = new (XFSConstants.SupportedVersionRange);

            InterfaceClass.NameEnum interfaceName = InterfaceClass.NameEnum.Common;
            // Common interface
            Common.CommonCapabilities.CommonInterface.IsNotNull($"Common interface must be supported.");
            interfaces.Add(new(interfaceName,
                               Common.CommonCapabilities.CommonInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                               Common.CommonCapabilities.CommonInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                               0));
            if (Common.CommonCapabilities.CommonInterface?.SynchronizableCommands?.Count > 0)
            {
                synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.CommonInterface.SynchronizableCommands
                                                select $"{interfaceName}.{cmd}");
            }
            if (Common.CommonCapabilities.CommonInterface.AuthenticationRequired?.Count > 0)
            {
                authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.CommonInterface.AuthenticationRequired
                                                        select $"{interfaceName}.{cmd}");
            }

            // CardReader interface
            if (Common.CommonCapabilities.CardReaderInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.CardReader;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.CardReaderInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.CardReaderInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.CardReaderInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.CardReaderInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.CardReaderInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.CardReaderInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // CashDispenser interface
            if (Common.CommonCapabilities.CashDispenserInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.CashDispenser;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.CashDispenserInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.CashDispenserInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.CashDispenserInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.CashDispenserInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.CashDispenserInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.CashDispenserInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // CashManagement interface
            if (Common.CommonCapabilities.CashManagementInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.CashManagement;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.CashManagementInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.CashManagementInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.CashManagementInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.CashManagementInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.CashManagementInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.CashManagementInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // Crypto interface
            if (Common.CommonCapabilities.CryptoInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.Crypto;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.CryptoInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.CryptoInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.CryptoInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.CryptoInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.CryptoInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.CryptoInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // Keyboard interface
            if (Common.CommonCapabilities.KeyboardInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.Keyboard;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.KeyboardInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.KeyboardInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.KeyboardInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.KeyboardInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.KeyboardInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.KeyboardInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // KeyManagement interface
            if (Common.CommonCapabilities.KeyManagementInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.KeyManagement;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.KeyManagementInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.KeyManagementInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.KeyManagementInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.KeyManagementInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.KeyManagementInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.KeyManagementInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // PinPad interface
            if (Common.CommonCapabilities.PinPadInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.PinPad;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.PinPadInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.PinPadInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.PinPadInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.PinPadInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.PinPadInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.PinPadInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // TextTerminal interface
            if (Common.CommonCapabilities.TextTerminalInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.TextTerminal;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.TextTerminalInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.TextTerminalInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.TextTerminalInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.TextTerminalInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.TextTerminalInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.TextTerminalInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // Storage interface
            if (Common.CommonCapabilities.StorageInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.Storage;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.StorageInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.StorageInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.StorageInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.StorageInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.StorageInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.StorageInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // Printer interface
            if (Common.CommonCapabilities.PrinterInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.Printer;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.PrinterInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.PrinterInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.PrinterInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.PrinterInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.PrinterInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.PrinterInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // Lights interface
            if (Common.CommonCapabilities.LightsInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.Lights;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.LightsInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.LightsInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.LightsInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.LightsInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.LightsInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.LightsInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }
            // Auxiliaries interface
            if (Common.CommonCapabilities.AuxiliariesInterface is not null)
            {
                interfaceName = InterfaceClass.NameEnum.Auxiliaries;
                interfaces.Add(new(interfaceName,
                                   Common.CommonCapabilities.AuxiliariesInterface.Commands?.ToDictionary(cmd => $"{interfaceName}.{cmd}", v => commandVersion),
                                   Common.CommonCapabilities.AuxiliariesInterface.Events?.ToDictionary(ev => $"{interfaceName}.{ev}", v => eventVersion),
                                   0));
                if (Common.CommonCapabilities.AuxiliariesInterface.SynchronizableCommands?.Count > 0)
                {
                    synchronizableCommands.AddRange(from cmd in Common.CommonCapabilities.AuxiliariesInterface.SynchronizableCommands
                                                    select $"{interfaceName}.{cmd}");
                }
                if (Common.CommonCapabilities.AuxiliariesInterface.AuthenticationRequired?.Count > 0)
                {
                    authenticationRequiredCommands.AddRange(from cmd in Common.CommonCapabilities.AuxiliariesInterface.AuthenticationRequired
                                                            select $"{interfaceName}.{cmd}");
                }
            }

            List<DeviceInformationClass> deviceInformation = null;
            if (Common.CommonCapabilities.DeviceInformation?.Count > 0)
            {
                deviceInformation = new();
                foreach (var device in Common.CommonCapabilities.DeviceInformation)
                {
                    List<FirmwareClass> firmware = null;
                    if (device.Firmware?.Count > 0)
                    {
                        firmware = new();
                        foreach (var firm in device.Firmware)
                        {
                            firmware.Add(new FirmwareClass(
                                FirmwareName: firm.FirmwareName,
                                FirmwareVersion: firm.FirmwareVersion,
                                HardwareRevision: firm.HardwareRevision
                                ));
                        }
                    }

                    List<SoftwareClass> software = null;
                    if (device.Software?.Count > 0)
                    {
                        software = new();
                        foreach (var soft in device.Software)
                        {
                            software.Add(new SoftwareClass(
                                SoftwareName: soft.SoftwareName,
                                SoftwareVersion: soft.SoftwareVersion
                                ));
                        }
                    }

                    deviceInformation.Add(new DeviceInformationClass(
                        ModelName: device.ModelName,
                        SerialNumber: device.SerialNumber,
                        RevisionNumber: device.RevisionNumber,
                        ModelDescription: device.ModelDescription,
                        Firmware: firmware,
                        Software: software
                        ));
                }
            }

            CapabilityPropertiesClass commonCapabilities = new(
                ServiceVersion: XFSConstants.ServiceVersion,
                DeviceInformation: deviceInformation,
                PowerSaveControl: Common.CommonCapabilities.PowerSaveControl,
                AntiFraudModule: Common.CommonCapabilities.AntiFraudModule,
                SynchronizableCommands: synchronizableCommands,
                EndToEndSecurity: Common.CommonCapabilities.EndToEndSecurity is null ? null :
                    new EndToEndSecurityClass(
                        Common.CommonCapabilities.EndToEndSecurity.Required switch
                        {
                            CommonCapabilitiesClass.EndToEndSecurityClass.RequiredEnum.Always => EndToEndSecurityClass.RequiredEnum.Always,
                            _=> EndToEndSecurityClass.RequiredEnum.IfConfigured,
                        },
                        Common.CommonCapabilities.EndToEndSecurity.HardwareSecurityElement,
                        Common.CommonCapabilities.EndToEndSecurity.ResponseSecurityEnabled switch
                        {
                            CommonCapabilitiesClass.EndToEndSecurityClass.ResponseSecurityEnabledEnum.Always => EndToEndSecurityClass.ResponseSecurityEnabledEnum.Always,
                            CommonCapabilitiesClass.EndToEndSecurityClass.ResponseSecurityEnabledEnum.IfConfigured => EndToEndSecurityClass.ResponseSecurityEnabledEnum.IfConfigured,
                            _ => EndToEndSecurityClass.ResponseSecurityEnabledEnum.NotSupported,
                        },
                        authenticationRequiredCommands.Count > 0 ? authenticationRequiredCommands : null,
                        Common.CommonCapabilities.EndToEndSecurity.CommandNonceTimeout
                        )
                    );

            XFS4IoT.CardReader.CapabilitiesClass cardReader = null;
            if (Common.CardReaderCapabilities is not null)
            {
                cardReader = new XFS4IoT.CardReader.CapabilitiesClass(
                    Common.CardReaderCapabilities.Type switch
                    {
                        CardReaderCapabilitiesClass.DeviceTypeEnum.Contactless => XFS4IoT.CardReader.CapabilitiesClass.TypeEnum.Contactless,
                        CardReaderCapabilitiesClass.DeviceTypeEnum.Dip => XFS4IoT.CardReader.CapabilitiesClass.TypeEnum.Dip,
                        CardReaderCapabilitiesClass.DeviceTypeEnum.IntelligentContactless => XFS4IoT.CardReader.CapabilitiesClass.TypeEnum.IntelligentContactless,
                        CardReaderCapabilitiesClass.DeviceTypeEnum.LatchedDip => XFS4IoT.CardReader.CapabilitiesClass.TypeEnum.LatchedDip,
                        CardReaderCapabilitiesClass.DeviceTypeEnum.Motor => XFS4IoT.CardReader.CapabilitiesClass.TypeEnum.Motor,
                        CardReaderCapabilitiesClass.DeviceTypeEnum.Permanent => XFS4IoT.CardReader.CapabilitiesClass.TypeEnum.Permanent,
                        _ => XFS4IoT.CardReader.CapabilitiesClass.TypeEnum.Swipe,
                    },
                    ReadTracks: new XFS4IoT.CardReader.CapabilitiesClass.ReadTracksClass(
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Track1),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Track2),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Track3),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Watermark),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Track1Front),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.FrontImage),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.BackImage),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Track1JIS),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Track3JIS),
                        Common.CardReaderCapabilities.ReadTracks.HasFlag(CardReaderCapabilitiesClass.ReadableDataTypesEnum.Ddi)
                        ),
                    WriteTracks: new XFS4IoT.CardReader.CapabilitiesClass.WriteTracksClass(
                        Common.CardReaderCapabilities.WriteTracks.HasFlag(CardReaderCapabilitiesClass.WritableDataTypesEnum.Track1),
                        Common.CardReaderCapabilities.WriteTracks.HasFlag(CardReaderCapabilitiesClass.WritableDataTypesEnum.Track2),
                        Common.CardReaderCapabilities.WriteTracks.HasFlag(CardReaderCapabilitiesClass.WritableDataTypesEnum.Track3),
                        Common.CardReaderCapabilities.WriteTracks.HasFlag(CardReaderCapabilitiesClass.WritableDataTypesEnum.Track1Front),
                        Common.CardReaderCapabilities.WriteTracks.HasFlag(CardReaderCapabilitiesClass.WritableDataTypesEnum.Track1JIS),
                        Common.CardReaderCapabilities.WriteTracks.HasFlag(CardReaderCapabilitiesClass.WritableDataTypesEnum.Track3JIS)
                        ),
                    ChipProtocols: new XFS4IoT.CardReader.CapabilitiesClass.ChipProtocolsClass(
                        Common.CardReaderCapabilities.ChipProtocols.HasFlag(CardReaderCapabilitiesClass.ChipProtocolsEnum.T0),
                        Common.CardReaderCapabilities.ChipProtocols.HasFlag(CardReaderCapabilitiesClass.ChipProtocolsEnum.T1),
                        Common.CardReaderCapabilities.ChipProtocols.HasFlag(CardReaderCapabilitiesClass.ChipProtocolsEnum.NotRequired),
                        Common.CardReaderCapabilities.ChipProtocols.HasFlag(CardReaderCapabilitiesClass.ChipProtocolsEnum.TypeAPart3),
                        Common.CardReaderCapabilities.ChipProtocols.HasFlag(CardReaderCapabilitiesClass.ChipProtocolsEnum.TypeAPart4),
                        Common.CardReaderCapabilities.ChipProtocols.HasFlag(CardReaderCapabilitiesClass.ChipProtocolsEnum.TypeB),
                        Common.CardReaderCapabilities.ChipProtocols.HasFlag(CardReaderCapabilitiesClass.ChipProtocolsEnum.TypeNFC)
                        ),
                    SecurityType: Common.CardReaderCapabilities.SecurityType switch
                    {
                        CardReaderCapabilitiesClass.SecurityTypeEnum.Cim86 => XFS4IoT.CardReader.CapabilitiesClass.SecurityTypeEnum.Cim86,
                        CardReaderCapabilitiesClass.SecurityTypeEnum.Mm => XFS4IoT.CardReader.CapabilitiesClass.SecurityTypeEnum.Mm,
                        _ => XFS4IoT.CardReader.CapabilitiesClass.SecurityTypeEnum.NotSupported,
                    },
                    PowerOnOption: Common.CardReaderCapabilities.PowerOnOption switch
                    {
                        CardReaderCapabilitiesClass.PowerOptionEnum.Exit => XFS4IoT.CardReader.CapabilitiesClass.PowerOnOptionEnum.Exit,
                        CardReaderCapabilitiesClass.PowerOptionEnum.ExitThenRetain => XFS4IoT.CardReader.CapabilitiesClass.PowerOnOptionEnum.ExitThenRetain,
                        CardReaderCapabilitiesClass.PowerOptionEnum.Retain => XFS4IoT.CardReader.CapabilitiesClass.PowerOnOptionEnum.Retain,
                        CardReaderCapabilitiesClass.PowerOptionEnum.Transport => XFS4IoT.CardReader.CapabilitiesClass.PowerOnOptionEnum.Transport,
                        _ => XFS4IoT.CardReader.CapabilitiesClass.PowerOnOptionEnum.NotSupported,
                    },
                    PowerOffOption: Common.CardReaderCapabilities.PowerOffOption switch
                    {
                        CardReaderCapabilitiesClass.PowerOptionEnum.Exit => XFS4IoT.CardReader.CapabilitiesClass.PowerOffOptionEnum.Exit,
                        CardReaderCapabilitiesClass.PowerOptionEnum.ExitThenRetain => XFS4IoT.CardReader.CapabilitiesClass.PowerOffOptionEnum.ExitThenRetain,
                        CardReaderCapabilitiesClass.PowerOptionEnum.Retain => XFS4IoT.CardReader.CapabilitiesClass.PowerOffOptionEnum.Retain,
                        CardReaderCapabilitiesClass.PowerOptionEnum.Transport => XFS4IoT.CardReader.CapabilitiesClass.PowerOffOptionEnum.Transport,
                        _ => XFS4IoT.CardReader.CapabilitiesClass.PowerOffOptionEnum.NotSupported,
                    },
                    Common.CardReaderCapabilities.FluxSensorProgrammable,
                    Common.CardReaderCapabilities.ReadWriteAccessFollowingExit,
                    WriteMode: new XFS4IoT.CardReader.CapabilitiesClass.WriteModeClass(
                        Common.CardReaderCapabilities.WriteMode == CardReaderCapabilitiesClass.WriteMethodsEnum.NotSupported,
                        Common.CardReaderCapabilities.WriteMode.HasFlag(CardReaderCapabilitiesClass.WriteMethodsEnum.Loco),
                        Common.CardReaderCapabilities.WriteMode.HasFlag(CardReaderCapabilitiesClass.WriteMethodsEnum.Hico),
                        Common.CardReaderCapabilities.WriteMode.HasFlag(CardReaderCapabilitiesClass.WriteMethodsEnum.Auto)),
                    ChipPower: new XFS4IoT.CardReader.CapabilitiesClass.ChipPowerClass(
                        Common.CardReaderCapabilities.ChipPower == CardReaderCapabilitiesClass.ChipPowerOptionsEnum.NotSupported,
                        Common.CardReaderCapabilities.ChipPower.HasFlag(CardReaderCapabilitiesClass.ChipPowerOptionsEnum.Cold),
                        Common.CardReaderCapabilities.ChipPower.HasFlag(CardReaderCapabilitiesClass.ChipPowerOptionsEnum.Warm),
                        Common.CardReaderCapabilities.ChipPower.HasFlag(CardReaderCapabilitiesClass.ChipPowerOptionsEnum.Off)),
                    MemoryChipProtocols: new XFS4IoT.CardReader.CapabilitiesClass.MemoryChipProtocolsClass(
                        Common.CardReaderCapabilities.MemoryChipProtocols.HasFlag(CardReaderCapabilitiesClass.MemoryChipProtocolsEnum.Siemens4442),
                        Common.CardReaderCapabilities.MemoryChipProtocols.HasFlag(CardReaderCapabilitiesClass.MemoryChipProtocolsEnum.Gpm896)),
                    Positions: new XFS4IoT.CardReader.CapabilitiesClass.PositionsClass(
                        Common.CardReaderCapabilities.Positions.HasFlag(CardReaderCapabilitiesClass.PositionsEnum.Exit),
                        Common.CardReaderCapabilities.Positions.HasFlag(CardReaderCapabilitiesClass.PositionsEnum.Transport)),
                    CardTakenSensor: Common.CardReaderCapabilities.CardTakenSensor
                    );
            }

            XFS4IoT.CashDispenser.CapabilitiesClass cashDispenser = null;
            if (Common.CashDispenserCapabilities is not null)
            {
                cashDispenser = new XFS4IoT.CashDispenser.CapabilitiesClass(
                    Type: Common.CashDispenserCapabilities.Type switch
                    {
                        CashDispenserCapabilitiesClass.TypeEnum.SelfServiceBill => XFS4IoT.CashDispenser.CapabilitiesClass.TypeEnum.SelfServiceBill,
                        CashDispenserCapabilitiesClass.TypeEnum.SelfServiceCoin => XFS4IoT.CashDispenser.CapabilitiesClass.TypeEnum.SelfServiceCoin,
                        CashDispenserCapabilitiesClass.TypeEnum.TellerBill => XFS4IoT.CashDispenser.CapabilitiesClass.TypeEnum.TellerBill,
                        _ => XFS4IoT.CashDispenser.CapabilitiesClass.TypeEnum.TellerCoin
                    },
                    MaxDispenseItems: Common.CashDispenserCapabilities.MaxDispenseItems,
                    ShutterControl: Common.CashDispenserCapabilities.ShutterControl,
                    RetractAreas: new XFS4IoT.CashDispenser.CapabilitiesClass.RetractAreasClass(
                        Common.CashDispenserCapabilities.RetractAreas.HasFlag(CashManagementCapabilitiesClass.RetractAreaEnum.Retract),
                        Common.CashDispenserCapabilities.RetractAreas.HasFlag(CashManagementCapabilitiesClass.RetractAreaEnum.Transport),
                        Common.CashDispenserCapabilities.RetractAreas.HasFlag(CashManagementCapabilitiesClass.RetractAreaEnum.Stacker),
                        Common.CashDispenserCapabilities.RetractAreas.HasFlag(CashManagementCapabilitiesClass.RetractAreaEnum.Reject),
                        Common.CashDispenserCapabilities.RetractAreas.HasFlag(CashManagementCapabilitiesClass.RetractAreaEnum.ItemCassette)
                        ),
                    RetractTransportActions: new XFS4IoT.CashDispenser.CapabilitiesClass.RetractTransportActionsClass(
                        Common.CashDispenserCapabilities.RetractTransportActions.HasFlag(CashManagementCapabilitiesClass.RetractTransportActionEnum.Present),
                        Common.CashDispenserCapabilities.RetractTransportActions.HasFlag(CashManagementCapabilitiesClass.RetractTransportActionEnum.Retract),
                        Common.CashDispenserCapabilities.RetractTransportActions.HasFlag(CashManagementCapabilitiesClass.RetractTransportActionEnum.Reject),
                        Common.CashDispenserCapabilities.RetractTransportActions.HasFlag(CashManagementCapabilitiesClass.RetractTransportActionEnum.ItemCassette)
                        ),
                    RetractStackerActions: new XFS4IoT.CashDispenser.CapabilitiesClass.RetractStackerActionsClass(
                        Common.CashDispenserCapabilities.RetractStackerActions.HasFlag(CashManagementCapabilitiesClass.RetractStackerActionEnum.Present),
                        Common.CashDispenserCapabilities.RetractStackerActions.HasFlag(CashManagementCapabilitiesClass.RetractStackerActionEnum.Retract),
                        Common.CashDispenserCapabilities.RetractStackerActions.HasFlag(CashManagementCapabilitiesClass.RetractStackerActionEnum.Reject),
                        Common.CashDispenserCapabilities.RetractStackerActions.HasFlag(CashManagementCapabilitiesClass.RetractStackerActionEnum.ItemCassette)
                        ),
                    IntermediateStacker: Common.CashDispenserCapabilities.IntermediateStacker,
                    ItemsTakenSensor: Common.CashDispenserCapabilities.ItemsTakenSensor,
                    Positions: new XFS4IoT.CashDispenser.CapabilitiesClass.PositionsClass(
                        Common.CashDispenserCapabilities.OutputPositions.HasFlag(CashDispenserCapabilitiesClass.OutputPositionEnum.Left),
                        Common.CashDispenserCapabilities.OutputPositions.HasFlag(CashDispenserCapabilitiesClass.OutputPositionEnum.Right),
                        Common.CashDispenserCapabilities.OutputPositions.HasFlag(CashDispenserCapabilitiesClass.OutputPositionEnum.Center),
                        Common.CashDispenserCapabilities.OutputPositions.HasFlag(CashDispenserCapabilitiesClass.OutputPositionEnum.Top),
                        Common.CashDispenserCapabilities.OutputPositions.HasFlag(CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom),
                        Common.CashDispenserCapabilities.OutputPositions.HasFlag(CashDispenserCapabilitiesClass.OutputPositionEnum.Front),
                        Common.CashDispenserCapabilities.OutputPositions.HasFlag(CashDispenserCapabilitiesClass.OutputPositionEnum.Rear)
                        ),
                    MoveItems: new XFS4IoT.CashDispenser.CapabilitiesClass.MoveItemsClass(
                        Common.CashDispenserCapabilities.MoveItems.HasFlag(CashDispenserCapabilitiesClass.MoveItemEnum.FromCashUnit),
                        Common.CashDispenserCapabilities.MoveItems.HasFlag(CashDispenserCapabilitiesClass.MoveItemEnum.ToCashUnit),
                        Common.CashDispenserCapabilities.MoveItems.HasFlag(CashDispenserCapabilitiesClass.MoveItemEnum.ToTransport),
                        Common.CashDispenserCapabilities.MoveItems.HasFlag(CashDispenserCapabilitiesClass.MoveItemEnum.ToStacker)
                        )
                    );
            }

            XFS4IoT.CashManagement.CapabilitiesClass cashManagement = null;
            if (Common.CashManagementCapabilities is not null)
            {
                cashManagement = new XFS4IoT.CashManagement.CapabilitiesClass(
                    SafeDoor: Common.CashManagementCapabilities.SafeDoor,
                    CashBox: Common.CashManagementCapabilities.CashBox,
                    ExchangeType: new XFS4IoT.CashManagement.CapabilitiesClass.ExchangeTypeClass(Common.CashManagementCapabilities.ExchangeTypes.HasFlag(CashManagementCapabilitiesClass.ExchangeTypesEnum.ByHand)),
                    ItemInfoTypes: new XFS4IoT.CashManagement.CapabilitiesClass.ItemInfoTypesClass(
                        Common.CashManagementCapabilities.ItemInfoTypes.HasFlag(CashManagementCapabilitiesClass.ItemInfoTypesEnum.SerialNumber),
                        Common.CashManagementCapabilities.ItemInfoTypes.HasFlag(CashManagementCapabilitiesClass.ItemInfoTypesEnum.Signature),
                        Common.CashManagementCapabilities.ItemInfoTypes.HasFlag(CashManagementCapabilitiesClass.ItemInfoTypesEnum.ImageFile)),
                    ClassificationList: Common.CashManagementCapabilities.ClassificationList
                    );
            }

            XFS4IoT.Crypto.CapabilitiesClass crypto = null;
            if (Common.CryptoCapabilities is not null)
            {
                Dictionary<string, Dictionary<string, Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.CryptoAttributesClass>>> cryptoAttrib = null;
                if (Common.CryptoCapabilities.CryptoAttributes?.Count > 0)
                {
                    cryptoAttrib = new();
                    foreach (var (keyUsage, algorithms) in Common.CryptoCapabilities.CryptoAttributes)
                    {
                        Dictionary<string, Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.CryptoAttributesClass>> dicAttributes = new();
                        foreach (var (algorithm, modeOfUses) in algorithms)
                        {
                            Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.CryptoAttributesClass> dicModeOfUse = new();
                            foreach (var (modeOfUse, method) in modeOfUses)
                            {
                                dicModeOfUse.Add(modeOfUse, new XFS4IoT.Crypto.CapabilitiesClass.CryptoAttributesClass(
                                    new XFS4IoT.Crypto.CapabilitiesClass.CryptoAttributesClass.CryptoMethodClass(
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.ECB),
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.CBC),
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.CFB),
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.OFB),
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.CTR),
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.XTS),
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.RSAES_PKCS1_V1_5),
                                        method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.CryptoAttributesClass.CryptoMethodEnum.RSAES_OAEP))
                                    ));
                            }
                            dicAttributes.Add(algorithm, dicModeOfUse);
                        }
                        cryptoAttrib.Add(keyUsage, dicAttributes);
                    }
                }

                Dictionary<string, Dictionary<string, Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.AuthenticationAttributesClass>>> authAttrib = null;
                if ( Common.CryptoCapabilities.AuthenticationAttributes?.Count > 0)
                {
                    authAttrib = new();
                    foreach (var (keyUsage, algorithms) in Common.CryptoCapabilities.AuthenticationAttributes)
                    {
                        Dictionary<string, Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.AuthenticationAttributesClass>> dicAttributes = new();
                        foreach (var (algorithm, modeOfUses) in algorithms)
                        {
                            Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.AuthenticationAttributesClass> dicModeOfUse = new();
                            foreach (var (modeOfUse, method) in modeOfUses)
                            {
                                dicModeOfUse.Add(modeOfUse, new XFS4IoT.Crypto.CapabilitiesClass.AuthenticationAttributesClass(
                                    new XFS4IoT.Crypto.CapabilitiesClass.AuthenticationAttributesClass.CryptoMethodClass(method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum.RSASSA_PKCS1_V1_5),
                                                                                                                         method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum.RSASSA_PSS)), 
                                    new XFS4IoT.Crypto.CapabilitiesClass.AuthenticationAttributesClass.HashAlgorithmClass(method?.HashAlgorithms.HasFlag(CryptoCapabilitiesClass.HashAlgorithmEnum.SHA1),
                                                                                                                          method?.HashAlgorithms.HasFlag(CryptoCapabilitiesClass.HashAlgorithmEnum.SHA256))
                                    ));
                            }
                            dicAttributes.Add(algorithm, dicModeOfUse);
                        }
                        authAttrib.Add(keyUsage, dicAttributes);
                    }
                }

                Dictionary<string, Dictionary<string, Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.VerifyAttributesClass>>> verifyAttrib = null;
                if (Common.CryptoCapabilities.VerifyAttributes?.Count > 0)
                {
                    verifyAttrib = new();
                    foreach (var (keyUsage, algorithms) in Common.CryptoCapabilities.VerifyAttributes)
                    {
                        Dictionary<string, Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.VerifyAttributesClass>> dicAttributes = new();
                        foreach (var (algorithm, modeOfUses) in algorithms)
                        {
                            Dictionary<string, XFS4IoT.Crypto.CapabilitiesClass.VerifyAttributesClass> dicModeOfUse = new();
                            foreach (var (modeOfUse, method) in modeOfUses)
                            {
                                dicModeOfUse.Add(modeOfUse, new XFS4IoT.Crypto.CapabilitiesClass.VerifyAttributesClass(
                                    new XFS4IoT.Crypto.CapabilitiesClass.VerifyAttributesClass.CryptoMethodClass(method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum.RSASSA_PKCS1_V1_5),
                                                                                                                 method?.CryptoMethods.HasFlag(CryptoCapabilitiesClass.VerifyAuthenticationAttributesClass.RSASignatureAlgorithmEnum.RSASSA_PSS)),
                                    new XFS4IoT.Crypto.CapabilitiesClass.VerifyAttributesClass.HashAlgorithmClass(method?.HashAlgorithms.HasFlag(CryptoCapabilitiesClass.HashAlgorithmEnum.SHA1),
                                                                                                                  method?.HashAlgorithms.HasFlag(CryptoCapabilitiesClass.HashAlgorithmEnum.SHA256))
                                    ));
                            }
                            dicAttributes.Add(algorithm, dicModeOfUse);
                        }
                        verifyAttrib.Add(keyUsage, dicAttributes);
                    }
                }

                crypto = new XFS4IoT.Crypto.CapabilitiesClass(
                    EmvHashAlgorithm: new XFS4IoT.Crypto.CapabilitiesClass.EmvHashAlgorithmClass(Common.CryptoCapabilities.EMVHashAlgorithms.HasFlag(CryptoCapabilitiesClass.EMVHashAlgorithmEnum.SHA1_Digest),
                                                                               Common.CryptoCapabilities.EMVHashAlgorithms.HasFlag(CryptoCapabilitiesClass.EMVHashAlgorithmEnum.SHA256_Digest)),
                    CryptoAttributes: cryptoAttrib,
                    AuthenticationAttributes: authAttrib,
                    VerifyAttributes: verifyAttrib);
            }

            XFS4IoT.KeyManagement.CapabilitiesClass keyManagement = null;
            if (Common.KeyManagementCapabilities is not null)
            {
                List<XFS4IoT.KeyManagement.CapabilitiesClass.LoadCertOptionsClass> loadCertOptions = null;
                if (Common.KeyManagementCapabilities.LoadCertificationOptions?.Count > 0)
                {
                    loadCertOptions = new();
                    foreach (var certOption in Common.KeyManagementCapabilities.LoadCertificationOptions)
                    {
                        loadCertOptions.Add(new XFS4IoT.KeyManagement.CapabilitiesClass.LoadCertOptionsClass(
                            certOption.Signer switch
                            {
                                KeyManagementCapabilitiesClass.LoadCertificateSignerEnum.CA => XFS4IoT.KeyManagement.CapabilitiesClass.LoadCertOptionsClass.SignerEnum.Ca,
                                KeyManagementCapabilitiesClass.LoadCertificateSignerEnum.CertHost => XFS4IoT.KeyManagement.CapabilitiesClass.LoadCertOptionsClass.SignerEnum.CertHost,
                                KeyManagementCapabilitiesClass.LoadCertificateSignerEnum.HL => XFS4IoT.KeyManagement.CapabilitiesClass.LoadCertOptionsClass.SignerEnum.Hl,
                                _ => XFS4IoT.KeyManagement.CapabilitiesClass.LoadCertOptionsClass.SignerEnum.SigHost,
                            },
                            new XFS4IoT.KeyManagement.CapabilitiesClass.LoadCertOptionsClass.OptionClass(
                                certOption.Option.HasFlag(KeyManagementCapabilitiesClass.LoadCertificateOptionEnum.NewHost),
                                certOption.Option.HasFlag(KeyManagementCapabilitiesClass.LoadCertificateOptionEnum.ReplaceHost)
                                )
                            )); 
                    }
                }

                Dictionary<string, Dictionary<string, Dictionary<string, XFS4IoT.KeyManagement.CapabilitiesClass.KeyAttributesClass>>> keyAttrib = null;
                if (Common.KeyManagementCapabilities.KeyAttributes?.Count > 0)
                {
                    keyAttrib = new();
                    foreach (var (keyUsage, algorithms) in Common.KeyManagementCapabilities.KeyAttributes)
                    {
                        Dictionary<string, Dictionary<string, XFS4IoT.KeyManagement.CapabilitiesClass.KeyAttributesClass>> dicAttributes = new();
                        foreach (var (algorithm, modeOfUses) in algorithms)
                        {
                            Dictionary<string, XFS4IoT.KeyManagement.CapabilitiesClass.KeyAttributesClass> dicModeOfUse = new();
                            foreach (var (modeOfUse, restrict) in modeOfUses)
                            {
                                dicModeOfUse.Add(modeOfUse, new XFS4IoT.KeyManagement.CapabilitiesClass.KeyAttributesClass(restrict?.Restricted));
                            }
                            dicAttributes.Add(algorithm, dicModeOfUse);
                        }
                        keyAttrib.Add(keyUsage, dicAttributes);
                    }
                }

                Dictionary<string, XFS4IoT.KeyManagement.CapabilitiesClass.DecryptAttributesClass> decryptAttrib = null;
                if (Common.KeyManagementCapabilities.DecryptAttributes?.Count > 0)
                {
                    decryptAttrib = new();
                    foreach (var (algorithm, method) in Common.KeyManagementCapabilities.DecryptAttributes)
                    {
                        decryptAttrib.Add(algorithm, new XFS4IoT.KeyManagement.CapabilitiesClass.DecryptAttributesClass(
                            new XFS4IoT.KeyManagement.CapabilitiesClass.DecryptAttributesClass.DecryptMethodClass(
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.ECB),
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.CBC),
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.CFB),
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.OFB),
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.CTR),
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.XTS),
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.RSAES_PKCS1_V1_5),
                                method?.DecryptMethods.HasFlag(KeyManagementCapabilitiesClass.DecryptMethodClass.DecryptMethodEnum.RSAES_OAEP))
                                )
                            );
                    }
                }

                Dictionary<string, Dictionary<string, Dictionary<string, XFS4IoT.KeyManagement.CapabilitiesClass.VerifyAttributesClass>>> verifyAttrib = new();
                if (Common.KeyManagementCapabilities.VerifyAttributes?.Count > 0)
                {
                    verifyAttrib = new();
                    foreach (var (keyUsage, algorithms) in Common.KeyManagementCapabilities.VerifyAttributes)
                    {
                        Dictionary<string, Dictionary<string, XFS4IoT.KeyManagement.CapabilitiesClass.VerifyAttributesClass>> dicAttributes = new();
                        foreach (var (algorithm, modeOfUses) in algorithms)
                        {
                            Dictionary<string, XFS4IoT.KeyManagement.CapabilitiesClass.VerifyAttributesClass> dicModeOfUse = new();
                            foreach (var (modeOfUse, method) in modeOfUses)
                            {
                                dicModeOfUse.Add(modeOfUse, new XFS4IoT.KeyManagement.CapabilitiesClass.VerifyAttributesClass(
                                    new XFS4IoT.KeyManagement.CapabilitiesClass.VerifyAttributesClass.CryptoMethodClass(
                                        method?.CryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.KCVNone),
                                        method?.CryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.KCVSelf),
                                        method?.CryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.KCVZero),
                                        method?.CryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.SignatureNone),
                                        method?.CryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.RSASSA_PKCS1_V1_5),
                                        method?.CryptoMethod.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.CryptoMethodEnum.RSASSA_PSS)),
                                    new XFS4IoT.KeyManagement.CapabilitiesClass.VerifyAttributesClass.HashAlgorithmClass(
                                        method?.HashAlgorithm.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.HashAlgorithmEnum.SHA1),
                                        method?.HashAlgorithm.HasFlag(KeyManagementCapabilitiesClass.VerifyMethodClass.HashAlgorithmEnum.SHA256))
                                        )
                                    );
                            }
                            dicAttributes.Add(algorithm, dicModeOfUse);
                        }
                        verifyAttrib.Add(keyUsage, dicAttributes);
                    }
                }

                keyManagement = new XFS4IoT.KeyManagement.CapabilitiesClass(
                    KeyNum: Common.KeyManagementCapabilities.MaxKeys,
                    DerivationAlgorithms: new XFS4IoT.KeyManagement.CapabilitiesClass.DerivationAlgorithmsClass(
                        ChipZka: false),
                    KeyCheckModes: new XFS4IoT.KeyManagement.CapabilitiesClass.KeyCheckModesClass(
                        Common.KeyManagementCapabilities.KeyCheckModes.HasFlag(KeyManagementCapabilitiesClass.KeyCheckModeEnum.Self),
                        Common.KeyManagementCapabilities.KeyCheckModes.HasFlag(KeyManagementCapabilitiesClass.KeyCheckModeEnum.Zero)),
                    HsmVendor: Common.KeyManagementCapabilities.HSMVendor,
                    RsaAuthenticationScheme: new XFS4IoT.KeyManagement.CapabilitiesClass.RsaAuthenticationSchemeClass(
                        Common.KeyManagementCapabilities.RSAAuthenticationScheme.HasFlag(KeyManagementCapabilitiesClass.RSAAuthenticationSchemeEnum.SecondPartySignature),
                        Common.KeyManagementCapabilities.RSAAuthenticationScheme.HasFlag(KeyManagementCapabilitiesClass.RSAAuthenticationSchemeEnum.ThirdPartyCertificate),
                        Common.KeyManagementCapabilities.RSAAuthenticationScheme.HasFlag(KeyManagementCapabilitiesClass.RSAAuthenticationSchemeEnum.ThirdPartyCertificateTR34)),
                    RsaSignatureAlgorithm: new XFS4IoT.KeyManagement.CapabilitiesClass.RsaSignatureAlgorithmClass(
                        Common.KeyManagementCapabilities.RSASignatureAlgorithm.HasFlag(KeyManagementCapabilitiesClass.RSASignatureAlgorithmEnum.RSASSA_PKCS1_V1_5),
                        Common.KeyManagementCapabilities.RSASignatureAlgorithm.HasFlag(KeyManagementCapabilitiesClass.RSASignatureAlgorithmEnum.RSASSA_PSS)),
                    RsaCryptAlgorithm: new XFS4IoT.KeyManagement.CapabilitiesClass.RsaCryptAlgorithmClass(
                        Common.KeyManagementCapabilities.RSACryptAlgorithm.HasFlag(KeyManagementCapabilitiesClass.RSACryptAlgorithmEnum.RSAES_PKCS1_V1_5),
                        Common.KeyManagementCapabilities.RSACryptAlgorithm.HasFlag(KeyManagementCapabilitiesClass.RSACryptAlgorithmEnum.RSAES_OAEP)),
                    RsaKeyCheckMode: new XFS4IoT.KeyManagement.CapabilitiesClass.RsaKeyCheckModeClass(
                        Common.KeyManagementCapabilities.RSAKeyCheckMode.HasFlag(KeyManagementCapabilitiesClass.RSAKeyCheckModeEnum.SHA1),
                        Common.KeyManagementCapabilities.RSAKeyCheckMode.HasFlag(KeyManagementCapabilitiesClass.RSAKeyCheckModeEnum.SHA256)),
                    SignatureScheme: new XFS4IoT.KeyManagement.CapabilitiesClass.SignatureSchemeClass(
                        Common.KeyManagementCapabilities.SignatureScheme.HasFlag(KeyManagementCapabilitiesClass.SignatureSchemeEnum.RSAKeyPair),
                        Common.KeyManagementCapabilities.SignatureScheme.HasFlag(KeyManagementCapabilitiesClass.SignatureSchemeEnum.RandomNumber),
                        Common.KeyManagementCapabilities.SignatureScheme.HasFlag(KeyManagementCapabilitiesClass.SignatureSchemeEnum.ExportEPPID),
                        Common.KeyManagementCapabilities.SignatureScheme.HasFlag(KeyManagementCapabilitiesClass.SignatureSchemeEnum.EnhancedRKL)),
                    EmvImportSchemes: new XFS4IoT.KeyManagement.CapabilitiesClass.EmvImportSchemesClass(
                        Common.KeyManagementCapabilities.EMVImportSchemes.HasFlag(KeyManagementCapabilitiesClass.EMVImportSchemeEnum.PlainCA),
                        Common.KeyManagementCapabilities.EMVImportSchemes.HasFlag(KeyManagementCapabilitiesClass.EMVImportSchemeEnum.ChecksumCA),
                        Common.KeyManagementCapabilities.EMVImportSchemes.HasFlag(KeyManagementCapabilitiesClass.EMVImportSchemeEnum.EPICA),
                        Common.KeyManagementCapabilities.EMVImportSchemes.HasFlag(KeyManagementCapabilitiesClass.EMVImportSchemeEnum.Issuer),
                        Common.KeyManagementCapabilities.EMVImportSchemes.HasFlag(KeyManagementCapabilitiesClass.EMVImportSchemeEnum.ICC),
                        Common.KeyManagementCapabilities.EMVImportSchemes.HasFlag(KeyManagementCapabilitiesClass.EMVImportSchemeEnum.ICCPIN),
                        Common.KeyManagementCapabilities.EMVImportSchemes.HasFlag(KeyManagementCapabilitiesClass.EMVImportSchemeEnum.PKCSV1_5CA)),
                    KeyBlockImportFormats: new XFS4IoT.KeyManagement.CapabilitiesClass.KeyBlockImportFormatsClass(
                        Common.KeyManagementCapabilities.KeyBlockImportFormats.HasFlag(KeyManagementCapabilitiesClass.KeyBlockImportFormatEmum.KEYBLOCKA),
                        Common.KeyManagementCapabilities.KeyBlockImportFormats.HasFlag(KeyManagementCapabilitiesClass.KeyBlockImportFormatEmum.KEYBLOCKB),
                        Common.KeyManagementCapabilities.KeyBlockImportFormats.HasFlag(KeyManagementCapabilitiesClass.KeyBlockImportFormatEmum.KEYBLOCKC),
                        Common.KeyManagementCapabilities.KeyBlockImportFormats.HasFlag(KeyManagementCapabilitiesClass.KeyBlockImportFormatEmum.KEYBLOCKD)),
                    KeyImportThroughParts: Common.KeyManagementCapabilities.KeyImportThroughParts,
                    DesKeyLength: new XFS4IoT.KeyManagement.CapabilitiesClass.DesKeyLengthClass(
                        Common.KeyManagementCapabilities.DESKeyLength.HasFlag(KeyManagementCapabilitiesClass.DESKeyLengthEmum.Single),
                        Common.KeyManagementCapabilities.DESKeyLength.HasFlag(KeyManagementCapabilitiesClass.DESKeyLengthEmum.Double),
                        Common.KeyManagementCapabilities.DESKeyLength.HasFlag(KeyManagementCapabilitiesClass.DESKeyLengthEmum.Triple)),
                    CertificateTypes: new XFS4IoT.KeyManagement.CapabilitiesClass.CertificateTypesClass(
                        Common.KeyManagementCapabilities.CertificateTypes.HasFlag(KeyManagementCapabilitiesClass.CertificateTypeEnum.EncKey),
                        Common.KeyManagementCapabilities.CertificateTypes.HasFlag(KeyManagementCapabilitiesClass.CertificateTypeEnum.VerificationKey),
                        Common.KeyManagementCapabilities.CertificateTypes.HasFlag(KeyManagementCapabilitiesClass.CertificateTypeEnum.HostKey)),
                    LoadCertOptions: loadCertOptions,
                    CrklLoadOptions: new XFS4IoT.KeyManagement.CapabilitiesClass.CrklLoadOptionsClass(
                        Common.KeyManagementCapabilities.CRKLLoadOption.HasFlag(KeyManagementCapabilitiesClass.CRKLLoadOptionEnum.NoRandom),
                        Common.KeyManagementCapabilities.CRKLLoadOption.HasFlag(KeyManagementCapabilitiesClass.CRKLLoadOptionEnum.NoRandomCRL),
                        Common.KeyManagementCapabilities.CRKLLoadOption.HasFlag(KeyManagementCapabilitiesClass.CRKLLoadOptionEnum.Random),
                        Common.KeyManagementCapabilities.CRKLLoadOption.HasFlag(KeyManagementCapabilitiesClass.CRKLLoadOptionEnum.RandomCRL)),
                    SymmetricKeyManagementMethods: new XFS4IoT.KeyManagement.CapabilitiesClass.SymmetricKeyManagementMethodsClass(
                        Common.KeyManagementCapabilities.SymmetricKeyManagementMethods.HasFlag(KeyManagementCapabilitiesClass.SymmetricKeyManagementMethodEnum.FixedKey),
                        Common.KeyManagementCapabilities.SymmetricKeyManagementMethods.HasFlag(KeyManagementCapabilitiesClass.SymmetricKeyManagementMethodEnum.MasterKey),
                        Common.KeyManagementCapabilities.SymmetricKeyManagementMethods.HasFlag(KeyManagementCapabilitiesClass.SymmetricKeyManagementMethodEnum.TripleDESDUKPT)),
                    KeyAttributes: keyAttrib,
                    DecryptAttributes: decryptAttrib,
                    VerifyAttributes: verifyAttrib);
            }

            XFS4IoT.Keyboard.CapabilitiesClass keyboard = null;
            if (Common.KeyboardCapabilities is not null)
            {
                List<XFS4IoT.Keyboard.CapabilitiesClass.EtsCapsClass> etsCaps = null;
                if (Common.KeyboardCapabilities.ETSCaps?.Count > 0)
                {
                    etsCaps = new();
                    foreach (var etc in Common.KeyboardCapabilities.ETSCaps)
                    {
                        etsCaps.Add(new XFS4IoT.Keyboard.CapabilitiesClass.EtsCapsClass(
                            etc.XPos,
                            etc.YPos,
                            etc.XSize,
                            etc.YSize,
                            etc.MaximumTouchFrames,
                            etc.MaximumTouchKeys,
                            new XFS4IoT.Keyboard.CapabilitiesClass.EtsCapsClass.FloatClass(etc.FloatFlags.HasFlag(KeyboardCapabilitiesClass.ETSCap.FloatPositionEnum.FloatX),
                                                                                           etc.FloatFlags.HasFlag(KeyboardCapabilitiesClass.ETSCap.FloatPositionEnum.FloatY))
                            ));
                    }
                }

                keyboard = new XFS4IoT.Keyboard.CapabilitiesClass(
                    AutoBeep: new XFS4IoT.Keyboard.CapabilitiesClass.AutoBeepClass(Common.KeyboardCapabilities.AutoBeep.HasFlag(KeyboardCapabilitiesClass.KeyboardBeepEnum.ActiveAvailable),
                                                                                   Common.KeyboardCapabilities.AutoBeep.HasFlag(KeyboardCapabilitiesClass.KeyboardBeepEnum.ActiveSelectable),
                                                                                   Common.KeyboardCapabilities.AutoBeep.HasFlag(KeyboardCapabilitiesClass.KeyboardBeepEnum.InActiveAvailable),
                                                                                   Common.KeyboardCapabilities.AutoBeep.HasFlag(KeyboardCapabilitiesClass.KeyboardBeepEnum.InActiveSelectable)),
                    EtsCaps: etsCaps
                    );
            }

            XFS4IoT.PinPad.CapabilitiesClass pinPad = null;
            if (Common.PinPadCapabilities is not null)
            {
                Dictionary<string, Dictionary<string, Dictionary<string, XFS4IoT.PinPad.CapabilitiesClass.PinBlockAttributesClass>>> pinblockAttrib = new();

                if (Common.PinPadCapabilities.PinBlockAttributes?.Count > 0)
                {
                    pinblockAttrib = new();
                    foreach (var (keyUsage, algorithms) in Common.PinPadCapabilities.PinBlockAttributes)
                    {
                        Dictionary<string, Dictionary<string, XFS4IoT.PinPad.CapabilitiesClass.PinBlockAttributesClass>> pinAlgorithms = new();
                        foreach (var (algorithm, modeOfUses) in algorithms)
                        {
                            Dictionary<string, XFS4IoT.PinPad.CapabilitiesClass.PinBlockAttributesClass> pinModeOfUse = new();
                            foreach (var (modeOfUse, method) in modeOfUses)
                            {
                                pinModeOfUse.Add(modeOfUse, new XFS4IoT.PinPad.CapabilitiesClass.PinBlockAttributesClass(
                                    new XFS4IoT.PinPad.CapabilitiesClass.PinBlockAttributesClass.CryptoMethodClass(
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.ECB),
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.CBC),
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.CFB),
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.OFB),
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.CTR),
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.XTS),
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.RSAES_PKCS1_V1_5),
                                        method?.EncryptionAlgorithm.HasFlag(PinPadCapabilitiesClass.PinBlockEncryptionAlgorithm.EncryptionAlgorithmEnum.RSAES_OAEP))
                                    ));
                            }
                            pinAlgorithms.Add(algorithm, pinModeOfUse);
                        }
                        pinblockAttrib.Add(keyUsage, pinAlgorithms);
                    }
                }

                pinPad = new XFS4IoT.PinPad.CapabilitiesClass(
                    PinFormats: new XFS4IoT.PinPad.CapabilitiesClass.PinFormatsClass(),
                    PresentationAlgorithms: new XFS4IoT.PinPad.CapabilitiesClass.PresentationAlgorithmsClass(),
                    Display: new XFS4IoT.PinPad.CapabilitiesClass.DisplayClass(),
                    IdcConnect: Common.PinPadCapabilities.IDConnect,
                    ValidationAlgorithms: new XFS4IoT.PinPad.CapabilitiesClass.ValidationAlgorithmsClass(),
                    PinCanPersistAfterUse: Common.PinPadCapabilities.PinCanPersistAfterUse,
                    TypeCombined: Common.PinPadCapabilities.TypeCombined,
                    SetPinblockDataRequired: Common.PinPadCapabilities.SetPinblockDataRequired,
                    PinBlockAttributes: pinblockAttrib
                    );
            }

            XFS4IoT.TextTerminal.CapabilitiesClass textTerminal = null;
            if (Common.TextTerminalCapabilities is not null)
            {
                List<XFS4IoT.TextTerminal.ResolutionClass> resolutions = null;
                if (Common.TextTerminalCapabilities.Resolutions?.Count > 0)
                {
                    resolutions = new();
                    foreach (var resolution in Common.TextTerminalCapabilities.Resolutions)
                    {
                        resolutions.Add(new XFS4IoT.TextTerminal.ResolutionClass(
                            resolution.Width,
                            resolution.Height
                            ));
                    }
                }

                textTerminal = new XFS4IoT.TextTerminal.CapabilitiesClass(
                    Type: Common.TextTerminalCapabilities.Type switch
                    {
                        TextTerminalCapabilitiesClass.TypeEnum.Fixed => XFS4IoT.TextTerminal.CapabilitiesClass.TypeEnum.Fixed,
                        _ => XFS4IoT.TextTerminal.CapabilitiesClass.TypeEnum.Removable
                    },
                    Resolutions: resolutions, 
                    KeyLock: Common.TextTerminalCapabilities.KeyLock,
                    Cursor: Common.TextTerminalCapabilities.Cursor,
                    Forms: Common.TextTerminalCapabilities.Forms
                    );
            }

            Dictionary<LightsCapabilitiesClass.DeviceEnum, XFS4IoT.Lights.LightCapabilitiesClass> stdLights = null;
            Dictionary<string, XFS4IoT.Lights.LightCapabilitiesClass> customLights = null;
            if (Common.LightsCapabilities?.Lights?.Count > 0)
            {
                stdLights = new();
                foreach (var light in Common.LightsCapabilities.Lights)
                {
                    stdLights.Add(light.Key, new XFS4IoT.Lights.LightCapabilitiesClass(
                                                new XFS4IoT.Lights.LightCapabilitiesClass.FlashRateClass(light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Off),
                                                                                                         light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Slow),
                                                                                                         light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Medium),
                                                                                                         light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Quick),
                                                                                                         light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Continuous)),
                                                new XFS4IoT.Lights.LightCapabilitiesClass.ColorClass(light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Red),
                                                                                                     light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Green),
                                                                                                     light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Yellow),
                                                                                                     light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Blue),
                                                                                                     light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Cyan),
                                                                                                     light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Magenta),
                                                                                                     light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.White)),
                                                new XFS4IoT.Lights.LightCapabilitiesClass.DirectionClass(light.Value.Direction.HasFlag(LightsCapabilitiesClass.DirectionEnum.Entry),
                                                                                                         light.Value.Direction.HasFlag(LightsCapabilitiesClass.DirectionEnum.Exit)),
                                                new XFS4IoT.Lights.LightCapabilitiesClass.PositionClass(light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Left),
                                                                                                        light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Right),
                                                                                                        light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Center),
                                                                                                        light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Top),
                                                                                                        light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Bottom),
                                                                                                        light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Front),
                                                                                                        light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Rear))
                                            ));
                }
            }
            if (Common.LightsCapabilities?.CustomLights?.Count > 0)
            {
                customLights = new();
                foreach (var light in Common.LightsCapabilities.CustomLights)
                {
                    customLights.Add(light.Key, new XFS4IoT.Lights.LightCapabilitiesClass(
                                                    new XFS4IoT.Lights.LightCapabilitiesClass.FlashRateClass(light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Off),
                                                                                                             light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Slow),
                                                                                                             light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Medium),
                                                                                                             light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Quick),
                                                                                                             light.Value.FlashRate.HasFlag(LightsCapabilitiesClass.FlashRateEnum.Continuous)),
                                                    new XFS4IoT.Lights.LightCapabilitiesClass.ColorClass(light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Red),
                                                                                                         light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Green),
                                                                                                         light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Yellow),
                                                                                                         light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Blue),
                                                                                                         light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Cyan),
                                                                                                         light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.Magenta),
                                                                                                         light.Value.Color.HasFlag(LightsCapabilitiesClass.ColorEnum.White)),
                                                    new XFS4IoT.Lights.LightCapabilitiesClass.DirectionClass(light.Value.Direction.HasFlag(LightsCapabilitiesClass.DirectionEnum.Entry),
                                                                                                             light.Value.Direction.HasFlag(LightsCapabilitiesClass.DirectionEnum.Exit)),
                                                    new XFS4IoT.Lights.LightCapabilitiesClass.PositionClass(light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Left),
                                                                                                            light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Right),
                                                                                                            light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Center),
                                                                                                            light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Top),
                                                                                                            light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Bottom),
                                                                                                            light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Front),
                                                                                                            light.Value.Position.HasFlag(LightsCapabilitiesClass.LightPostionEnum.Rear))
                                            ));
                }
            }

            XFS4IoT.Lights.CapabilitiesClass lights = null;
            if (stdLights?.Count > 0)
            {
                lights = new XFS4IoT.Lights.CapabilitiesClass
                    (
                    CardReader: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.CardReader) ? stdLights[LightsCapabilitiesClass.DeviceEnum.CardReader] : null,
                    PinPad: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.PinPad) ? stdLights[LightsCapabilitiesClass.DeviceEnum.PinPad] : null,
                    NotesDispenser: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.NotesDispenser) ? stdLights[LightsCapabilitiesClass.DeviceEnum.NotesDispenser] : null,
                    CoinDispenser: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.CoinDispenser) ? stdLights[LightsCapabilitiesClass.DeviceEnum.CoinDispenser] : null,
                    ReceiptPrinter: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.ReceiptPrinter) ? stdLights[LightsCapabilitiesClass.DeviceEnum.ReceiptPrinter] : null,
                    PassbookPrinter: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.PassbookPrinter) ? stdLights[LightsCapabilitiesClass.DeviceEnum.PassbookPrinter] : null,
                    EnvelopeDepository: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.EnvelopeDepository) ? stdLights[LightsCapabilitiesClass.DeviceEnum.EnvelopeDepository] : null,
                    ChequeUnit: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.ChequeUnit) ? stdLights[LightsCapabilitiesClass.DeviceEnum.ChequeUnit] : null,
                    BillAcceptor: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.BillAcceptor) ? stdLights[LightsCapabilitiesClass.DeviceEnum.BillAcceptor] : null,
                    EnvelopeDispenser: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.EnvelopeDispenser) ? stdLights[LightsCapabilitiesClass.DeviceEnum.EnvelopeDispenser] : null,
                    DocumentPrinter: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.DocumentPrinter) ? stdLights[LightsCapabilitiesClass.DeviceEnum.DocumentPrinter] : null,
                    CoinAcceptor: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.CoinAcceptor) ? stdLights[LightsCapabilitiesClass.DeviceEnum.CoinAcceptor] : null,
                    Scanner: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.Scanner) ? stdLights[LightsCapabilitiesClass.DeviceEnum.Scanner] : null,
                    CardUnit2: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.CardUnit2) ? stdLights[LightsCapabilitiesClass.DeviceEnum.CardUnit2] : null,
                    NotesDispenser2: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.NotesDispenser2) ? stdLights[LightsCapabilitiesClass.DeviceEnum.NotesDispenser2] : null,
                    BillAcceptor2: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.BillAcceptor2) ? stdLights[LightsCapabilitiesClass.DeviceEnum.BillAcceptor2] : null,
                    StatusGood: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.StatusGoodIndicator) ? stdLights[LightsCapabilitiesClass.DeviceEnum.StatusGoodIndicator] : null,
                    StatusWarning: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.StatusWarningIndicator) ? stdLights[LightsCapabilitiesClass.DeviceEnum.StatusWarningIndicator] : null,
                    StatusBad: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.StatusBadIndicator) ? stdLights[LightsCapabilitiesClass.DeviceEnum.StatusBadIndicator] : null,
                    FasciaLight: stdLights.ContainsKey(LightsCapabilitiesClass.DeviceEnum.FasciaLight) ? stdLights[LightsCapabilitiesClass.DeviceEnum.FasciaLight] : null
                    );
            }
            if (customLights?.Count > 0)
            {
                if (lights is null)
                {
                    lights = new();
                }
                lights.ExtendedProperties = customLights;
            }

            XFS4IoT.Printer.CapabilitiesClass printer = null;
            if (Common.PrinterCapabilities is not null)
            {
                XFS4IoT.Printer.PaperSourcesClass paperSources = new (Common.PrinterCapabilities.PaperSources.HasFlag(PrinterCapabilitiesClass.PaperSourceEnum.Upper),
                                                                      Common.PrinterCapabilities.PaperSources.HasFlag(PrinterCapabilitiesClass.PaperSourceEnum.Lower),
                                                                      Common.PrinterCapabilities.PaperSources.HasFlag(PrinterCapabilitiesClass.PaperSourceEnum.External),
                                                                      Common.PrinterCapabilities.PaperSources.HasFlag(PrinterCapabilitiesClass.PaperSourceEnum.AUX),
                                                                      Common.PrinterCapabilities.PaperSources.HasFlag(PrinterCapabilitiesClass.PaperSourceEnum.AUX2),
                                                                      Common.PrinterCapabilities.PaperSources.HasFlag(PrinterCapabilitiesClass.PaperSourceEnum.Park));

                if (Common.PrinterCapabilities.CustomPaperSources is not null)
                {
                    paperSources.ExtendedProperties = Common.PrinterCapabilities.CustomPaperSources;
                }

                printer = new XFS4IoT.Printer.CapabilitiesClass(
                    new XFS4IoT.Printer.CapabilitiesClass.TypeClass(Common.PrinterCapabilities.Types.HasFlag(PrinterCapabilitiesClass.TypeEnum.Receipt),
                                                                    Common.PrinterCapabilities.Types.HasFlag(PrinterCapabilitiesClass.TypeEnum.Passbook),
                                                                    Common.PrinterCapabilities.Types.HasFlag(PrinterCapabilitiesClass.TypeEnum.Journal),
                                                                    Common.PrinterCapabilities.Types.HasFlag(PrinterCapabilitiesClass.TypeEnum.Document),
                                                                    Common.PrinterCapabilities.Types.HasFlag(PrinterCapabilitiesClass.TypeEnum.Scanner)),
                    new XFS4IoT.Printer.CapabilitiesClass.ResolutionClass(Common.PrinterCapabilities.Resolutions.HasFlag(PrinterCapabilitiesClass.ResolutionEnum.Low),
                                                                          Common.PrinterCapabilities.Resolutions.HasFlag(PrinterCapabilitiesClass.ResolutionEnum.Medium),
                                                                          Common.PrinterCapabilities.Resolutions.HasFlag(PrinterCapabilitiesClass.ResolutionEnum.High),
                                                                          Common.PrinterCapabilities.Resolutions.HasFlag(PrinterCapabilitiesClass.ResolutionEnum.VeryHigh)),
                    new XFS4IoT.Printer.CapabilitiesClass.ReadFormClass(Common.PrinterCapabilities.ReadForms.HasFlag(PrinterCapabilitiesClass.ReadFormEnum.OCR),
                                                                        Common.PrinterCapabilities.ReadForms.HasFlag(PrinterCapabilitiesClass.ReadFormEnum.MICR),
                                                                        Common.PrinterCapabilities.ReadForms.HasFlag(PrinterCapabilitiesClass.ReadFormEnum.MSF),
                                                                        Common.PrinterCapabilities.ReadForms.HasFlag(PrinterCapabilitiesClass.ReadFormEnum.Barcode),
                                                                        Common.PrinterCapabilities.ReadForms.HasFlag(PrinterCapabilitiesClass.ReadFormEnum.PageMark),
                                                                        Common.PrinterCapabilities.ReadForms.HasFlag(PrinterCapabilitiesClass.ReadFormEnum.Image),
                                                                        Common.PrinterCapabilities.ReadForms.HasFlag(PrinterCapabilitiesClass.ReadFormEnum.EmptyLine)),
                    new XFS4IoT.Printer.CapabilitiesClass.WriteFormClass(Common.PrinterCapabilities.WriteForms.HasFlag(PrinterCapabilitiesClass.WriteFormEnum.Text),
                                                                         Common.PrinterCapabilities.WriteForms.HasFlag(PrinterCapabilitiesClass.WriteFormEnum.Graphics),
                                                                         Common.PrinterCapabilities.WriteForms.HasFlag(PrinterCapabilitiesClass.WriteFormEnum.OCR),
                                                                         Common.PrinterCapabilities.WriteForms.HasFlag(PrinterCapabilitiesClass.WriteFormEnum.MICR),
                                                                         Common.PrinterCapabilities.WriteForms.HasFlag(PrinterCapabilitiesClass.WriteFormEnum.MSF),
                                                                         Common.PrinterCapabilities.WriteForms.HasFlag(PrinterCapabilitiesClass.WriteFormEnum.Barcode),
                                                                         Common.PrinterCapabilities.WriteForms.HasFlag(PrinterCapabilitiesClass.WriteFormEnum.Stamp)),
                    new XFS4IoT.Printer.CapabilitiesClass.ExtentsClass(Common.PrinterCapabilities.Extents.HasFlag(PrinterCapabilitiesClass.ExtentEnum.Horizontal),
                                                                       Common.PrinterCapabilities.Extents.HasFlag(PrinterCapabilitiesClass.ExtentEnum.Vertical)),
                    new XFS4IoT.Printer.CapabilitiesClass.ControlClass(Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Eject),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Perforate),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Cut),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Skip),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Flush),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Retract),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Stack),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.PartialCut),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Alarm),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.PageForward),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.PageBackward),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.TurnMedia),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Stamp),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Park),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Expel),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.EjectToTransport),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.Rotate180),
                                                                       Common.PrinterCapabilities.Controls.HasFlag(PrinterCapabilitiesClass.ControlEnum.ClearBuffer)),
                    Common.PrinterCapabilities.MaxMediaOnStacker,
                    Common.PrinterCapabilities.AcceptMedia,
                    Common.PrinterCapabilities.MultiPage,
                    paperSources,
                    Common.PrinterCapabilities.MediaTaken,
                    Common.PrinterCapabilities.RetractBins,
                    Common.PrinterCapabilities.MaxRetract,
                    new XFS4IoT.Printer.CapabilitiesClass.ImageTypeClass(Common.PrinterCapabilities.ImageTypes.HasFlag(PrinterCapabilitiesClass.ImageTypeEnum.TIF),
                                                                         Common.PrinterCapabilities.ImageTypes.HasFlag(PrinterCapabilitiesClass.ImageTypeEnum.WMF),
                                                                         Common.PrinterCapabilities.ImageTypes.HasFlag(PrinterCapabilitiesClass.ImageTypeEnum.BMP),
                                                                         Common.PrinterCapabilities.ImageTypes.HasFlag(PrinterCapabilitiesClass.ImageTypeEnum.JPG)),
                    new XFS4IoT.Printer.CapabilitiesClass.FrontImageColorFormatClass(Common.PrinterCapabilities.FrontImageColorFormats.HasFlag(PrinterCapabilitiesClass.FrontImageColorFormatEnum.Binary),
                                                                                     Common.PrinterCapabilities.FrontImageColorFormats.HasFlag(PrinterCapabilitiesClass.FrontImageColorFormatEnum.GrayScale),
                                                                                     Common.PrinterCapabilities.FrontImageColorFormats.HasFlag(PrinterCapabilitiesClass.FrontImageColorFormatEnum.Full)),
                    new XFS4IoT.Printer.CapabilitiesClass.BackImageColorFormatClass(Common.PrinterCapabilities.BackImageColorFormats.HasFlag(PrinterCapabilitiesClass.BackImageColorFormatEnum.Binary),
                                                                                    Common.PrinterCapabilities.BackImageColorFormats.HasFlag(PrinterCapabilitiesClass.BackImageColorFormatEnum.GrayScale),
                                                                                    Common.PrinterCapabilities.BackImageColorFormats.HasFlag(PrinterCapabilitiesClass.BackImageColorFormatEnum.Full)),
                    new XFS4IoT.Printer.CapabilitiesClass.CodelineFormatClass(Common.PrinterCapabilities.CodelineFormats.HasFlag(PrinterCapabilitiesClass.CodelineFormatEnum.CMC7),
                                                                              Common.PrinterCapabilities.CodelineFormats.HasFlag(PrinterCapabilitiesClass.CodelineFormatEnum.E13B),
                                                                              Common.PrinterCapabilities.CodelineFormats.HasFlag(PrinterCapabilitiesClass.CodelineFormatEnum.OCR)),
                    new XFS4IoT.Printer.CapabilitiesClass.ImageSourceClass(Common.PrinterCapabilities.ImageSourceTypes.HasFlag(PrinterCapabilitiesClass.ImageSourceTypeEnum.ImageFront),
                                                                           Common.PrinterCapabilities.ImageSourceTypes.HasFlag(PrinterCapabilitiesClass.ImageSourceTypeEnum.ImageBack),
                                                                           Common.PrinterCapabilities.ImageSourceTypes.HasFlag(PrinterCapabilitiesClass.ImageSourceTypeEnum.CodeLine)),
                    Common.PrinterCapabilities.DispensePaper,
                    Common.PrinterCapabilities.OSPrinter,
                    Common.PrinterCapabilities.MediaPresented,
                    Common.PrinterCapabilities.AutoRetractPeriod,
                    Common.PrinterCapabilities.RetractToTransport);
            }

            return Task.FromResult(
                new CapabilitiesCompletion.PayloadData(
                    MessagePayload.CompletionCodeEnum.Success,
                    string.Empty,
                    Interfaces: interfaces,
                    Common: commonCapabilities,
                    CardReader: cardReader,
                    CashDispenser: cashDispenser,
                    CashManagement: cashManagement,
                    PinPad: pinPad,
                    Crypto: crypto,
                    KeyManagement: keyManagement,
                    Keyboard: keyboard,
                    TextTerminal: textTerminal,
                    Lights: lights,
                    Printer: printer)
                );
        }
    }
}
