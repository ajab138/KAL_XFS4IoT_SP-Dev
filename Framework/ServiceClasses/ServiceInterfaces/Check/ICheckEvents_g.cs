/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Check interface.
 * ICheckEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Check
{
    public interface ICheckUnsolicitedEvents
    {

        Task MediaTakenEvent(XFS4IoT.Check.Events.MediaTakenEvent.PayloadData Payload);

        Task MediaDetectedEvent(XFS4IoT.Check.Events.MediaDetectedEvent.PayloadData Payload);

        Task ShutterStatusChangedEvent(XFS4IoT.Check.Events.ShutterStatusChangedEvent.PayloadData Payload);

    }
}
