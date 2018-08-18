using System; 
using System.Collections.Generic; 
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ipman.core.Query;
using ipman.shared.Entity;
using System.Security.Claims;
using ipman.shared.WebServiceModels;
using IPMan.Utilities;
using ipman.core.Command;
using System.Threading.Tasks;
using ipman.shared.Utilities;
using ipman.shared.Entity.SearchCriteria;
using IPMan.Authorization;

namespace IPMan.Controllers 
{ 
    [Authorize]
    [Route("api/[controller]")] 
    public class PersonController : Controller 
    { 
        private readonly PersonSearch _personSearch;
        private readonly PersonUpsert _personUpsert;
        private UserAccountGetByEmail _userAccountGetByEmail;
        private IAuthorizationService _authorizationService;
        public PersonController(PersonSearch personSearch,
                                     PersonUpsert personUpsert,
                                     UserAccountGetByEmail userAccountGetByEmail,
                                     IAuthorizationService authorizationService)
        {
            _personSearch = personSearch;
            _personUpsert = personUpsert;
            _userAccountGetByEmail = userAccountGetByEmail;
            _authorizationService = authorizationService;
        }

        [HttpPost("[action]")]
        public SearchPersonResponse Search([FromBody] SearchPersonRequest request)
        {
            SearchPersonResponse searchResponse = new SearchPersonResponse
            {
                Persons = new List<Person>(),
            };

            searchResponse.Persons = _personSearch.Execute(request.SearchCriteria);

            return searchResponse;
        }

        [HttpPost("[action]")]
        public async Task<SavePersonResponse> Save([FromBody] SavePersonRequest request)
        {
            SavePersonResponse response = new SavePersonResponse();

            if (response.InitializeFromModelStateIfInvalid(ModelState))
                return response;

            if(request.Person.ID == Guid.Empty)
            {
                request.Person.ID = Guid.NewGuid();
                await _personUpsert.ExecuteAsync(request.Person, true);
            }
            else
            {
                var existingSite = _personSearch.Execute(new PersonSearchCriteria
                {
                    IncludedPersonIDs = new List<Guid> { request.Person.ID }
                }).FirstOrDefault();
                await _personUpsert.ExecuteAsync(request.Person);
            }
            response.Person = request.Person;
            return response;
        }
    } 
} 
