/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashDispenser interface.
 * Dispense_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashDispenser.Completions
{
    [DataContract]
    [Completion(Name = "CashDispenser.Dispense")]
    public sealed class DispenseCompletion : Completion<DispenseCompletion.PayloadData>
    {
        public DispenseCompletion(int RequestId, DispenseCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, DenominationClass Denomination = null, string Bunches = null, CashManagement.StorageInOutClass Storage = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.Denomination = Denomination;
                this.Bunches = Bunches;
                this.Storage = Storage;
            }

            public enum ErrorCodeEnum
            {
                InvalidCurrency,
                InvalidTellerID,
                CashUnitError,
                InvalidDenomination,
                InvalidMixNumber,
                NoCurrencyMix,
                NotDispensable,
                TooManyItems,
                UnsupportedPosition,
                ExchangeActive,
                NoCashBoxPresent,
                AmountNotInMixTable,
                ItemsLeft,
                ShutterOpen
            }

            /// <summary>
            /// Specifies the error code if applicable. Following values are possible:
            /// 
            /// * ```invalidCurrency``` - There are no storage units in the device of the currency specified in the request.
            /// * ```invalidTellerID``` - Invalid teller ID. This error will never be generated by a Self-Service device.
            /// * ```cashUnitError``` - There is a problem with a storage unit. A 
            /// [Storage.StorageErrorEvent](#storage.storageerrorevent) will be posted with the details.
            /// * ```invalidDenomination``` - No _mix_ is specified and the sum of the values for _counts_ and 
            /// _cashBox_ does not match the non-zero _currencies_ specified.
            /// * ```invalidMixNumber``` - Unknown mix algorithm.
            /// * ```noCurrencyMix``` - The storage units specified in the request were not all of the same currency 
            /// and this device does not support multiple currencies.
            /// * ```notDispensable``` - The amount is not dispensable by the device. This error code is also returned
            /// if a unit is specified in the _counts_ list which is not a dispensing cash unit, e.g., a
            /// retract/reject cash unit.
            /// * ```tooManyItems``` - The request requires too many items to be dispensed.
            /// * ```exchangeActive``` - The device is in an exchange state (see 
            /// [CashManagement.StartExchange](#cashmanagement.startexchange)).
            /// * ```noCashBoxPresent``` - Cash box amount needed, however teller is not assigned a cash box.
            /// * ```amountNotInMixTable``` - A mix table is being used to determine the denomination but the amount 
            /// specified in the request is not in the mix table.
            /// * ```unsupportedPosition``` - The specified output position is not supported.
            /// * ```itemsLeft``` - Items have been left in the transport or exit slot as a result of a prior 
            /// dispense, present or recycler cash-in operation.
            /// * ```shutterOpen``` - The Service cannot dispense items with an open output shutter.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            /// <summary>
            /// Denomination object describing the contents of the denomination operation.
            /// </summary>
            [DataMember(Name = "denomination")]
            public DenominationClass Denomination { get; init; }

            /// <summary>
            /// Specifies how many bunches will be required to present the request. Following values are possible:
            /// 
            ///   * ```&lt;number&gt;``` - The number of bunches to be presented.
            ///   * ```unknown``` - More than one bunch is required but the precise number is unknown.
            /// <example>1</example>
            /// </summary>
            [DataMember(Name = "bunches")]
            [DataTypes(Pattern = @"^unknown$|^[0-9]*$")]
            public string Bunches { get; init; }

            [DataMember(Name = "storage")]
            public CashManagement.StorageInOutClass Storage { get; init; }

        }
    }
}
