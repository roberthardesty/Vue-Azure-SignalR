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
using ipman.core.Command;
using ipman.Utilities;

namespace IPMan.Controllers 
{ 
    [Route("api/[controller]")] 
    [Authorize]
    public class PostController : Controller 
    { 
        private PostGetBySiteAccountName _postGetBySiteAccountName;
        private PostGetBySiteAccountID _postGetBySiteAccountID;
        private PostUpsert _postUpsert;
        private PostDelete _postDelete;
        private UserAccountGetByEmail _userAccountGetByEmail;
        private IAuthorizationService _authorizationService;
        public PostController(PostGetBySiteAccountName postGetBySiteAccountName,
                              PostGetBySiteAccountID postGetBySiteAccountID,
                              PostUpsert postUpsert,
                              PostDelete postDelete,
                              UserAccountGetByEmail userAccountGetByEmail,
                              IAuthorizationService authorizationService)
        {
            _postGetBySiteAccountName = postGetBySiteAccountName;
            _postGetBySiteAccountID = postGetBySiteAccountID;
            _postUpsert = postUpsert;
            _postDelete = postDelete;
            _userAccountGetByEmail = userAccountGetByEmail;
            _authorizationService = authorizationService;
        }

        [HttpPost("[action]")]
        public async Task<DeletePostResponse> DeletePost([FromBody]DeletePostRequest deletePostRequest)
        {
            string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            UserAccount user = await _userAccountGetByEmail.ExecuteAsync(email, true);
            AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User,
                                                                                        new SiteAccountUserAccountRoleModel { SiteAccountID = deletePostRequest.SiteAccountID, UserAccount = user },
                                                                                        new SiteAccountRoleRequirement(new Role[] { Role.AdminRole, Role.OwnerRole, Role.BasicRole }));
            if (authResult.Succeeded)
            {
                await AzureStorage.DeletePostImage(deletePostRequest.PostID);
                await _postDelete.ExecuteAsync(deletePostRequest.PostID);
            }
            return new DeletePostResponse { PostID = deletePostRequest.PostID };
        }
        //[HttpPost("[action]")]
        //public async Task<AddPostResponse> AddPost(AddPostRequest addPostRequest)
        //{
        //    string email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        //    UserAccount user = await _userAccountGetByEmail.ExecuteAsync(email, true);
        //    AuthorizationResult authResult = await _authorizationService.AuthorizeAsync(User,
        //                                                                                new SiteAccountUserAccountRoleModel { SiteAccountID = addPostRequest.SiteAccountID, UserAccount = user },
        //                                                                                new SiteAccountRoleRequirement(new Role[] { Role.AdminRole, Role.OwnerRole}));

        //    if (authResult.Succeeded)
        //}

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
