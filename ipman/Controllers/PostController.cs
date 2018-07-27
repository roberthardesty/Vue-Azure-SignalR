using System; 
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ipman.core.Query;
using ipman.shared.Entity;
using ipman.shared.WebServiceModels;
using System.Threading.Tasks;
using IPMan.Authorization;

namespace IPMan.Controllers 
{ 
    [Route("api/[controller]")] 
    [Authorize]
    public class PostController : Controller 
    { 
        private PostGetBySiteAccountName _postGetBySiteAccountName;
        private PostGetBySiteAccountID _postGetBySiteAccountID;
        private IAuthorizationService _authorizationService;
        public PostController(PostGetBySiteAccountName postGetBySiteAccountName,
                              PostGetBySiteAccountID postGetBySiteAccountID,
                              IAuthorizationService authorizationService)
        {
            _postGetBySiteAccountName = postGetBySiteAccountName;
            _postGetBySiteAccountID = postGetBySiteAccountID;

        }

        [HttpGet("[action]")] 
        public IEnumerable<Post> GetPostsBySiteAccountName(string name) 
        {
            List<Post> posts = _postGetBySiteAccountName.Execute(name, true, true);
            return posts;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Post>> GetPostsBySiteAccount(GetPostsBySiteAccountRequest request)
        {

            AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User,
                                                                                        new SiteAccount { ID = request.SiteAccountID },
                                                                                        new SiteAccountRoleRequirement(new Role[]{ Role.AdminRole, Role.OwnerRole, Role.BasicRole }));

            if(authResult.Succeeded)
                return _postGetBySiteAccountID.Execute(request.SiteAccountID, true, true);

            return new List<Post> { };
        }
    } 
} 
