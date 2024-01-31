/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Check interface.
 * MediaInEndHandler.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Check.Commands;
using XFS4IoT.Check.Completions;

namespace XFS4IoTFramework.Check
{
    public partial class MediaInEndHandler
    {

        private Task<MediaInEndCompletion.PayloadData> HandleMediaInEnd(IMediaInEndEvents events, MediaInEndCommand mediaInEnd, CancellationToken cancel)
        {
            //ToDo: Implement HandleMediaInEnd for Check.
            throw new NotImplementedException("HandleMediaInEnd for Check is not implemented in MediaInEndHandler.cs");
        }

    }
}
