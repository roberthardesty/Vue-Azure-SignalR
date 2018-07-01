using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPMan.Utilities
{
    public static class UserAccountExtensions
    {
        public static UserAccount RemoveSensitiveData(this UserAccount user)
        {
            user.UserAccountSalt = "";
            user.EmailAddress = "";
            user.FirstName = "";
            user.LastName = "";
            user.LastLoginUTC = new DateTime();
            return user;
        }
    }
}
