﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2024
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/
using System;
using System.Threading.Tasks;
using XFS4IoT;

namespace XFS4IoTFramework.CardReader
{
    public sealed class WriteRawDataCommandEvents(IWriteRawDataEvents events) : CommonCardCommandEvents(events)
    { }
}