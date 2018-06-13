using FlexLabs.EntityFrameworkCore.Upsert;
using ipman.core.Utilities;
using ipman.shared.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ipman.core.Query
{
    public class UserAccountUpsert
    {
        private IPManDataContext _ipManDataContext;

        public UserAccountUpsert(IPManDataContext ipManDataContext)
        {
            _ipManDataContext = ipManDataContext;
        }

        public async Task Execute(UserAccount userAccount)
        {
            await _ipManDataContext.Upsert(userAccount)
                             .On(ua => ua.ID)
                             .RunAsync();
        }
    }
}