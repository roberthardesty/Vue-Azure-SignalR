using ipman.core.Utilities;
using ipman.shared.Entity;
using ipman.shared.Entity.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ipman.core.Query
{
    public class PersonSearch
    {
        private IPManDataContext _dbContext;

        public PersonSearch(IPManDataContext ipManDataContext)
        {
            _dbContext = ipManDataContext;
        }

        public List<Person> Execute(PersonSearchCriteria searchCriteria)
        {
            var persons = (from person in _dbContext.Persons
                           where person.IsActive
                           where string.IsNullOrWhiteSpace(searchCriteria.Keyword)
                                  || $" {person.FirstName} {person.LastName} ".Contains(searchCriteria.Keyword)
                           where searchCriteria.IncludedPersonIDs.Count == 0
                                      || searchCriteria.IncludedPersonIDs.Contains(person.ID)
                           select person);
            return persons.ToList();
        }
    }
}
