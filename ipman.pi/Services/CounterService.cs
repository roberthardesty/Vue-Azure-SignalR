using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ipman.pi.Services
{
    public class CounterService: ServiceClient
    {
        private readonly HubConnection _connection;
        private CancellationTokenSource _cts;
        public CounterService(): base(HubNames.CounterHub)
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

        public async Task StartLoggingIncrement()
        {
            Console.WriteLine("Starting connection. Press Ctrl-C to close.");
            await _connection.StartAsync();
            _connection.On<int>("increment", (count) =>
            {
                if(!_cts.IsCancellationRequested)
                    Console.WriteLine($"The count is now {count}.");
            });
        }
    }
}
