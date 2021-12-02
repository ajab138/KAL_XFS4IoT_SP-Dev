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
using XFS4IoT.Completions;
using XFS4IoTServer;
using XFS4IoT.Common.Commands;
using XFS4IoT.Common.Completions;

namespace XFS4IoTFramework.Common
{
    public partial class GetTransactionStateHandler
    {
        private async Task<GetTransactionStateCompletion.PayloadData> HandleGetTransactionState(IGetTransactionStateEvents events, GetTransactionStateCommand getTransactionState, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CommonDev.GetTransactionState()");
            var result = await Device.GetTransactionState();
            Logger.Log(Constants.DeviceClass, $"CommonDev.GetTransactionState() -> {result.CompletionCode}");

            return new GetTransactionStateCompletion.PayloadData(result.CompletionCode,
                                                                 result.ErrorDescription,
                                                                 result.State switch
                                                                 {
                                                                     GetTransactionStateResult.StateEnum.Active => GetTransactionStateCompletion.PayloadData.StateEnum.Active,
                                                                     GetTransactionStateResult.StateEnum.Inactive => GetTransactionStateCompletion.PayloadData.StateEnum.Inactive,
                                                                     _ => null,
                                                                 },
                                                                 result.TransactionID,
                                                                 result.Extra);
        }
    }
}
