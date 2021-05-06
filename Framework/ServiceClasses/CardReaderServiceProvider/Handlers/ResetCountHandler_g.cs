/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ResetCountHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.CardReader
{
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(ResetCountCommand))]
    public partial class ResetCountHandler : ICommandHandler
    {
        public ResetCountHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetCountHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ResetCountHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICardReaderDevice>();

            CardReader = Provider.IsA<ICardReaderServiceClass>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ResetCountHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var resetCountCmd = command.IsA<ResetCountCommand>($"Invalid parameter in the ResetCount Handle method. {nameof(ResetCountCommand)}");
            
            IResetCountEvents events = new ResetCountEvents(Connection, resetCountCmd.Headers.RequestId);

            var result = await HandleResetCount(events, resetCountCmd, cancel);
            await Connection.SendMessageAsync(new ResetCountCompletion(resetCountCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var resetCountcommand = command.IsA<ResetCountCommand>();

            ResetCountCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ResetCountCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => ResetCountCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => ResetCountCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ResetCountCompletion(resetCountcommand.Headers.RequestId, new ResetCountCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICardReaderDevice Device { get => Provider.Device.IsA<ICardReaderDevice>(); }
        private IServiceProvider Provider { get; }
        private ICardReaderServiceClass CardReader { get; }
        private ILogger Logger { get; }
    }

}