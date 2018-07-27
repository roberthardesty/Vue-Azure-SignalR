using ipman.core.Utilities;
using ipman.shared.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ipman.core.Query
{
    public class PostGetBySiteAccountID
    {
        private readonly IPManDataContext _dbContext;

        public PostGetBySiteAccountID(IPManDataContext ipManDataContext)
        {
            _dbContext = ipManDataContext;
        }

        public List<Post> Execute(Guid id, bool includeTags = false, bool includeUser = false)
        {
            IQueryable<Post> posts = _dbContext.Posts.Where(p => p.SiteAccount.ID == id);

            if (includeTags)
                posts = posts.Include(p => p.PostTags)
                        .ThenInclude(pt => pt.Tag);

            if (includeUser)
                posts = posts.Include(p => p.UserAccountCreator);

            return posts.ToList();
        }
    }
}
