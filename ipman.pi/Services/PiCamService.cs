using ipman.shared.Communications;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ipman.pi.Services
{
    public class PiCamService : ServiceClient
    {
        private HubConnection _connection;
        private CancellationTokenSource _cts;
        public PiCamService() : base(HubNames.PiCamHub)
        {
            _connection = GetHubConnection();
            _cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                _cts.Cancel();
            };

            _connection.Closed += e =>
            {
                Console.WriteLine("Connection closed with error: {0}", e);

                _cts.Cancel();
                return Task.CompletedTask;
            };
        }

        public async Task<SignalRServerResponse> JoinPiCams()
        {
            await _connection.StartAsync();

            return await MutableCallSameMethodOnTheHub<SignalRServerResponse>(connection: _connection).ConfigureAwait(false);
        }

        public void RequestSingleImageCapture(Action handler)
        {
            Console.WriteLine($"Registering handler for {HubNames.PiCamHub}/{nameof(RequestSingleImageCapture)}.....");
            _connection.On(nameof(RequestSingleImageCapture), handler);
            Console.WriteLine("Registered.");
        }
    }
}