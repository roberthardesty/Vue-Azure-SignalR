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
using System.Security.Claims;

namespace IPMan.Controllers 
{ 
    [Route("api/[controller]")] 
    [Authorize]
    public class PostController : Controller 
    { 
        private PostGetBySiteAccountName _postGetBySiteAccountName;
        private PostGetBySiteAccountID _postGetBySiteAccountID;
        private UserAccountGetByEmail _userAccountGetByEmail;
        private IAuthorizationService _authorizationService;
        public PostController(PostGetBySiteAccountName postGetBySiteAccountName,
                              PostGetBySiteAccountID postGetBySiteAccountID,
                              UserAccountGetByEmail userAccountGetByEmail,
                              IAuthorizationService authorizationService)
        {
            _postGetBySiteAccountName = postGetBySiteAccountName;
            _postGetBySiteAccountID = postGetBySiteAccountID;
            _userAccountGetByEmail = userAccountGetByEmail;
            _authorizationService = authorizationService;
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
            string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            UserAccount user = await _userAccountGetByEmail.ExecuteAsync(email, true);
            AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User,
                                                                                        new SiteAccountUserAccountRoleModel { SiteAccountID = request.SiteAccountID, UserAccount = user },
                                                                                        new SiteAccountRoleRequirement(new Role[]{ Role.AdminRole, Role.OwnerRole, Role.BasicRole }));

            if(authResult.Succeeded)
                return _postGetBySiteAccountID.Execute(request.SiteAccountID, true, true);

            return new List<Post> { };
        }
    } 
} 
