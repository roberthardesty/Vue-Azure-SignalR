﻿using ipman.pi.Utilities;
using ipman.shared.Communications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ipman.pi.Services
{
    public abstract class ServiceClient
    {
        private const int METHOD_INVOCATION_MAX_RETRY_COUNT = 3;

        #region Member Variables

        private readonly string _hubName;

        #endregion

        #region Constructors

        protected ServiceClient(string hubName)
        {
            _hubName = hubName;
        }

        #endregion

        #region Methods
        protected async Task<T> MutableCallSameMethodOnTheHub<T>(HubConnection connection, object[] args = null, [CallerMemberName] string hubMethodName = null) where T : SignalRServerResponse, new()
        {
            Exception lastException;

            var invocationAttempt = 1;

            args = args ?? new object[0];

            Func<string> getLoggingHeader = () => $"{GetType().Name}.{hubMethodName}:";

            string loggingHeader = $"{GetType().Name}.{hubMethodName}:";

            do
            {
                Console.WriteLine($"{loggingHeader} Begin attempt #{invocationAttempt} of {METHOD_INVOCATION_MAX_RETRY_COUNT}");

                try
                {
                    Console.WriteLine($"{loggingHeader} Invoking SignalR method on hub...");

                    var result = await connection.InvokeCoreAsync<T>(hubMethodName, args).ConfigureAwait(false);

                    Console.WriteLine($"{loggingHeader} Response received from hub.");

                    return result;

                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{loggingHeader} Error while invoking SignalR method.", exception);
                    lastException = exception;
                }
                finally
                {
                    Console.WriteLine($"{loggingHeader} End attempt #{invocationAttempt}");

                    invocationAttempt++;
                }

            } while (invocationAttempt <= METHOD_INVOCATION_MAX_RETRY_COUNT);

            return new T { Error = lastException, Success = false };
        }

        protected async Task<T> CallTheSameMethodOnTheHub<T>(object[] args = null, [CallerMemberName] string hubMethodName = null) where T : SignalRServerResponse, new()
        {
            return await CallMethodOnTheHub<T>(args, hubMethodName).ConfigureAwait(false);
        }

        protected async Task<T> CallMethodOnTheHub<T>(object[] args = null, string hubMethodName = null) where T : SignalRServerResponse, new()
        {
            Exception lastException;

            var invocationAttempt = 1;

            args = args ?? new object[0];

            Func<string> getLoggingHeader = () => $"{GetType().Name}.{hubMethodName}:";

            string loggingHeader = $"{GetType().Name}.{hubMethodName}:";

            do
            {
                Console.WriteLine($"{loggingHeader} Begin attempt #{invocationAttempt} of {METHOD_INVOCATION_MAX_RETRY_COUNT}");

                try
                {
                    Console.WriteLine($"{loggingHeader} Creating hub connection...");

                    var hubConnection = GetHubConnection();

                    Console.WriteLine($"{loggingHeader} Creating hub proxy...");

                    await hubConnection.StartAsync();

                    Console.WriteLine($"{loggingHeader} Invoking SignalR method on hub...");

                    var result = await hubConnection.InvokeCoreAsync<T>(hubMethodName, args).ConfigureAwait(false);

                    Console.WriteLine($"{loggingHeader} Response received from hub.");

                    await hubConnection.DisposeAsync();

                    return result;

                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{loggingHeader} Error while invoking SignalR method.", exception);
                    Console.WriteLine($"Error: {exception.Message}");
                    lastException = exception;
                }
                finally
                {
                    Console.WriteLine($"{loggingHeader} End attempt #{invocationAttempt}");

                    invocationAttempt++;
                }

            } while (invocationAttempt <= METHOD_INVOCATION_MAX_RETRY_COUNT);

            return new T { Error = lastException, Success = false };
        }

        protected HubConnection GetHubConnection()
        {
            var hubConnection = new HubConnectionBuilder()
                                                .WithUrl($"{PiConfiguration.DevSignalRAddress}/{_hubName}",
                                                    (options) => 
                                                    {
                                                        options.AccessTokenProvider = () => Task.FromResult(PiConfiguration.AzureSignalRAccessKey);
                                                    })
                                                .Build();
            hubConnection.Closed -= ConnectionClosedHandler;
            hubConnection.Closed += ConnectionClosedHandler;

            return hubConnection;
        }

        protected virtual Task ConnectionClosedHandler(Exception e)
        {
            Console.WriteLine($"{e.GetType().Name}");
            if(e is HubException)
            {
            }
            return Task.FromResult("");
        }
        #endregion
    }
}