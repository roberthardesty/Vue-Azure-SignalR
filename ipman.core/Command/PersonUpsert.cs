using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ipman.core.Command
{
    public class PersonUpsert
    {
        private IPManDataContext _ipManDataContext;

        public PersonUpsert(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task ExecuteAsync(Person person, bool isInsert = false)
        {
            if (isInsert)
            {
                var savedPost = _ipManDataContext.Add<Person>(person).Entity;
            }
            else
            {
                _ipManDataContext.Update<Person>(person);
            }
            await _ipManDataContext.SaveChangesAsync();
        }
    }
}
