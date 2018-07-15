using FlexLabs.EntityFrameworkCore.Upsert;
using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ipman.core.Command
{
    public class UserAccountUpsert
    {
        private IPManDataContext _ipManDataContext;

        public UserAccountUpsert(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task ExecuteAsync(UserAccount userAccount, bool isInsert = false)
        {
            if(isInsert)
            {
                _ipManDataContext.Add<UserAccount>(userAccount);
            }
            else
            {
                _ipManDataContext.Update<UserAccount>(userAccount);
            }
            await _ipManDataContext.SaveChangesAsync();
        }
    }
}