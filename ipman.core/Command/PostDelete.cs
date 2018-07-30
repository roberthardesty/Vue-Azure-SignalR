using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.core.Command
{
    public class PostDelete
    {
        private IPManDataContext _ipManDataContext;

        public PostDelete(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task ExecuteAsync(Guid postID)
        {
            _ipManDataContext.Posts.Remove(new Post { ID = postID });
            await _ipManDataContext.SaveChangesAsync();
        }
    }
}
