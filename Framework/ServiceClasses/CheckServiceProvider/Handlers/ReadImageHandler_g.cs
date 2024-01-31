/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Check interface.
 * ReadImageHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.Check.Commands;
using XFS4IoT.Check.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.Check
{
    [CommandHandler(XFSConstants.ServiceClass.Check, typeof(ReadImageCommand))]
    public partial class ReadImageHandler : ICommandHandler
    {
        public ReadImageHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReadImageHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ReadImageHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICheckDevice>();

            Check = Provider.IsA<ICheckService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ReadImageHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(ReadImageHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var readImageCmd = command.IsA<ReadImageCommand>($"Invalid parameter in the ReadImage Handle method. {nameof(ReadImageCommand)}");
            readImageCmd.Header.RequestId.HasValue.IsTrue();

            IReadImageEvents events = new ReadImageEvents(Connection, readImageCmd.Header.RequestId.Value);

            var result = await HandleReadImage(events, readImageCmd, cancel);
            await Connection.SendMessageAsync(new ReadImageCompletion(readImageCmd.Header.RequestId.Value, result));

            await this.IsA<ICommandHandler>().CommandPostProcessing(result);
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var readImagecommand = command.IsA<ReadImageCommand>();
            readImagecommand.Header.RequestId.HasValue.IsTrue();

            ReadImageCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ReadImageCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                InternalErrorException => ReadImageCompletion.PayloadData.CompletionCodeEnum.InternalError,
                UnsupportedDataException => ReadImageCompletion.PayloadData.CompletionCodeEnum.UnsupportedData,
                SequenceErrorException => ReadImageCompletion.PayloadData.CompletionCodeEnum.SequenceError,
                AuthorisationRequiredException => ReadImageCompletion.PayloadData.CompletionCodeEnum.AuthorisationRequired,
                HardwareErrorException => ReadImageCompletion.PayloadData.CompletionCodeEnum.HardwareError,
                UserErrorException => ReadImageCompletion.PayloadData.CompletionCodeEnum.UserError,
                FraudAttemptException => ReadImageCompletion.PayloadData.CompletionCodeEnum.FraudAttempt,
                DeviceNotReadyException => ReadImageCompletion.PayloadData.CompletionCodeEnum.DeviceNotReady,
                InvalidCommandException => ReadImageCompletion.PayloadData.CompletionCodeEnum.InvalidCommand,
                NotEnoughSpaceException => ReadImageCompletion.PayloadData.CompletionCodeEnum.NotEnoughSpace,
                NotImplementedException or NotSupportedException => ReadImageCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => ReadImageCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => ReadImageCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => ReadImageCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ReadImageCompletion(readImagecommand.Header.RequestId.Value, new ReadImageCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICheckDevice Device { get => Provider.Device.IsA<ICheckDevice>(); }
        private IServiceProvider Provider { get; }
        private ICheckService Check { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
