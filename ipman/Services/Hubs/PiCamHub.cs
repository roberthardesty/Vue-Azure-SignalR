using ipman.shared.Communications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPMan.Services.Hubs
{
    public class PiCamHub : Hub
    {
        private const string SPECTATORS_GROUP = "PiSpectators";
        private const string Cam_GROUP = "PiCam";
        [Authorize]
        public async Task<SignalRServerResponse> RequestSingleImageCapture()
        {
            var response = new SignalRServerResponse();

            await Clients.Group(Cam_GROUP).SendAsync(nameof(RequestSingleImageCapture));

            return response;
        }

        public async Task<SignalRServerResponse> UpdateProgress(string stepName, int currentStep, int totalSteps)
        {
            var response = new SignalRServerResponse();

            await Clients.Group(SPECTATORS_GROUP).SendAsync(nameof(UpdateProgress), new
            {
                StepName = stepName,
                CurrentStep = currentStep,
                TotalSteps = totalSteps
            });

            return response;
        }

        [Authorize]
        public async Task<SignalRServerResponse> JoinSpectators()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, SPECTATORS_GROUP);
            return new SignalRServerResponse()
            {
                Success = true
            };
        }

        public async Task<SignalRServerResponse> JoinPiCams()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Cam_GROUP);
            return new SignalRServerResponse()
            {
                Success = true
            };
        }
    }
}
