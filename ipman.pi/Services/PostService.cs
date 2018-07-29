using ipman.shared.Communications;
using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.pi.Services
{
    public class PostService: ServiceClient
    {
        public PostService(): base(HubNames.PostHub)
        {

        }

        public async Task<SignalRServerResponse> TestConnection()
        {
            return await CallTheSameMethodOnTheHub<SignalRServerResponse>();
        }

        public async Task<SignalRServerResponse> AddPost(Post post)
        {
            return await CallTheSameMethodOnTheHub<SignalRServerResponse>(new object[] { post });
        }
    }
}
