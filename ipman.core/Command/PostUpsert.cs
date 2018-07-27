using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.core.Command
{
    public class PostUpsert
    {
        private IPManDataContext _ipManDataContext;

        public PostUpsert(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task ExecuteAsync(Post post, bool isInsert = false)
        {
            if (isInsert)
            {
                _ipManDataContext.Add<Post>(post);
            }
            else
            {
                _ipManDataContext.Update<Post>(post);
            }
            await _ipManDataContext.SaveChangesAsync();
        }
    }
}
