/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CashDispenser.Commands;
using XFS4IoT.CashDispenser.Completions;
using XFS4IoT.CashManagement;
using XFS4IoT.Completions;
using XFS4IoTFramework.Common;
using XFS4IoTFramework.Storage;
using XFS4IoTFramework.CashManagement;
namespace XFS4IoTFramework.CashDispenser
{
    public partial class CountHandler
    {
        private async Task<CountCompletion.PayloadData> HandleCount(ICountEvents events, CountCommand count, CancellationToken cancel)
        {
            CashDispenserCapabilitiesClass.OutputPositionEnum position = CashDispenserCapabilitiesClass.OutputPositionEnum.NotSupported;
            if (count.Payload.Position is not null)
            {
                position = count.Payload.Position switch
                {
                    OutputPositionEnum.OutBottom => CashDispenserCapabilitiesClass.OutputPositionEnum.Bottom,
                    OutputPositionEnum.OutCenter => CashDispenserCapabilitiesClass.OutputPositionEnum.Center,
                    OutputPositionEnum.OutDefault => CashDispenserCapabilitiesClass.OutputPositionEnum.Default,
                    OutputPositionEnum.OutFront => CashDispenserCapabilitiesClass.OutputPositionEnum.Front,
                    OutputPositionEnum.OutLeft => CashDispenserCapabilitiesClass.OutputPositionEnum.Left,
                    OutputPositionEnum.OutRear => CashDispenserCapabilitiesClass.OutputPositionEnum.Rear,
                    OutputPositionEnum.OutRight => CashDispenserCapabilitiesClass.OutputPositionEnum.Right,
                    OutputPositionEnum.OutTop => CashDispenserCapabilitiesClass.OutputPositionEnum.Top,
                    _ => CashDispenserCapabilitiesClass.OutputPositionEnum.NotSupported
                };
            }

            if (!Common.CashDispenserCapabilities.OutputPositions.HasFlag(position))
            {
                return new CountCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                       $"Unsupported position. {position}");
            }

            CountRequest request = new (position);
            if (!string.IsNullOrEmpty(count.Payload.Unit))
            {
                List<string> storageFrom = new();
                if (string.Compare(count.Payload.Unit, "all", ignoreCase: true) == 0)
                {
                    foreach (var unit in Storage.CashUnits)
                    {
                        if (unit.Value.Unit.Configuration.Types.HasFlag(CashCapabilitiesClass.TypesEnum.CashOut))
                            storageFrom.Add(unit.Key);
                    }
                }
                else
                {
                    if (Storage.CashUnits.ContainsKey(count.Payload.Unit))
                    {
                        return new CountCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                               $"Specified storage id is invalid. {count.Payload.Unit}");
                    }
                    storageFrom.Add(count.Payload.Unit);
                }

                request = new CountRequest(position, storageFrom);
            }

            Logger.Log(Constants.DeviceClass, "CashDispenserDev.CountAsync()");

            var result = await Device.CountAsync(new ItemErrorCommandEvents(events), request, cancel);

            Logger.Log(Constants.DeviceClass, $"CashDispenserDev.CountAsync() -> {result.CompletionCode}, {result.ErrorCode}");

            await Storage.UpdateCashAccounting(result.MovementResult);

            return new CountCompletion.PayloadData(result.CompletionCode, 
                                                   result.ErrorDescription, 
                                                   result.ErrorCode);
        }

        private IStorageService Storage { get => Provider.IsA<IStorageService>(); }
    }
}
