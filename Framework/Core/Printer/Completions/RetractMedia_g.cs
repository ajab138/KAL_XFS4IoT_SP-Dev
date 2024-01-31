/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * RetractMedia_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Printer.Completions
{
    [DataContract]
    [XFS4Version(Version = "2.0")]
    [Completion(Name = "Printer.RetractMedia")]
    public sealed class RetractMediaCompletion : Completion<RetractMediaCompletion.PayloadData>
    {
        public RetractMediaCompletion(int RequestId, RetractMediaCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, string Result = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.Result = Result;
            }

            public enum ErrorCodeEnum
            {
                NoMediaPresent,
                RetractBinFull,
                MediaJammed
            }

            /// <summary>
            /// Specifies the error code if applicable, otherwise null. The following values are possible:
            /// 
            /// * ```noMediaPresent``` - No media present on retract. Either there was no media present (in a position
            ///   to be retracted from) when the command was called or the media was removed during the retract.
            /// * ```retractBinFull``` - The retract bin is full; no more media can be retracted. The current media is
            ///   still in the device.
            /// * ```mediaJammed``` - The media is jammed; operator intervention is required.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            /// <summary>
            /// Specifies where the media has actually been deposited, as one of the following:
            /// 
            /// * ```transport``` - Media was retracted to the transport.
            /// * ```nomedia``` - No media was retracted.
            /// * ```unit&lt;retract bin number&gt;``` - Media was retracted to the retract bin specified.
            /// <example>unit1</example>
            /// </summary>
            [DataMember(Name = "result")]
            [DataTypes(Pattern = @"^transport$|^nomedia$|^unit[0-9]+$")]
            public string Result { get; init; }

        }
    }
}
