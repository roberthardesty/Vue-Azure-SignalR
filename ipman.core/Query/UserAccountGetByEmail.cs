using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ipman.core.Query
{
    public class UserAccountGetByEmail
    {
        private IPManDataContext _ipManDataContext;

        public UserAccountGetByEmail(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public UserAccount Execute(string email)
        {
            return _ipManDataContext.UserAccounts.FirstOrDefault(user => user.EmailAddress == email);
        }
    }
}