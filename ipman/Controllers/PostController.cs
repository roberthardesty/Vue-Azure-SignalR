using System; 
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ipman.core.Query;
using ipman.shared.Entity;

namespace IPMan.Controllers 
{ 
    [Route("api/[controller]")] 
    [Authorize]
    public class PostController : Controller 
    { 
        private PostGetBySiteAccountName _postGetBySiteAccountName;
        public PostController(PostGetBySiteAccountName postGetBySiteAccountName)
        {
            _postGetBySiteAccountName = postGetBySiteAccountName;
        }

        [HttpGet("[action]")] 
        public IEnumerable<Post> GetPostsBySiteAccountName(string name) 
        {
            List<Post> posts = _postGetBySiteAccountName.Execute(name, true, true);
            return posts;
        } 
    } 
} 
