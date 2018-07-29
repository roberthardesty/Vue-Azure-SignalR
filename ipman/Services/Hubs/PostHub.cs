using ipman.core.Command;
using ipman.shared.Communications;
using ipman.shared.Entity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPMan.Services.Hubs
{
    public class PostHub: Hub
    {
        private readonly PostUpsert _postUpsert;

        public PostHub(PostUpsert postUpsert)
        {
            _postUpsert = postUpsert;
        }

        public Task<SignalRServerResponse> TestConnection()
        {
            return Task.FromResult(new SignalRServerResponse() { Success = true });
        }
        public async Task<SignalRServerResponse> AddPost(Post post)
        {
            try
            {
                await _postUpsert.ExecuteAsync(post, true);
            }
            catch(Exception e)
            {
                return new SignalRServerResponse
                {
                    Error = e,
                    Success = false
                };
            }
            return new SignalRServerResponse { Success = true };
        }
    }
}
