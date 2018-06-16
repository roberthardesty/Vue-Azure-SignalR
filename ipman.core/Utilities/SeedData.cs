using ipman.shared.Entity;
using ipman.shared.Entity.Join;
using ipman.shared.Entity.Lookups;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.core.Utilities
{
    public class SeedData
    {
        public static UserAccount[] Users = 
        {
            new UserAccount()
            {
                ID = Guid.Parse("4D8881BD-DB0A-4725-9CF0-2C4390013A30"),
                EmailAddress = "budnjoe@gmail.com",
                FirstName = "rob",
                LastName = "Hardesty",
                GitHubID = null,
                GoogleID = null,
                AvatarLink = @"https://lh4.googleusercontent.com/-gPvw9sU8Mpc/AAAAAAAAAAI/AAAAAAAAAOY/HQ2yjXFKeEk/photo.jpg?sz=50",
                LastLoginProvider = AuthenticationProvider.Google,
                LastLoginUTC = new DateTime(2018, 6, 13),
                LastUpdatedUTC = new DateTime(2018, 6, 13),
                CreatedUTC = new DateTime(2018, 6, 13)
            },
            new UserAccount()
            {
                ID = Guid.Parse("F84D5D8F-131E-4554-A45D-E1D03C02BC43"),
                EmailAddress = "robert.hardesty.mail@gmail.com",
                FirstName = "Robert",
                LastName = "Hardesty",
                GitHubID = null,
                GoogleID = null,
                AvatarLink = @"https://lh3.googleusercontent.com/-pu8oCttY3pE/AAAAAAAAAAI/AAAAAAAAAAA/h5YVW6XWCK4/photo.jpg?sz=50",
                LastLoginProvider = AuthenticationProvider.Google,
                LastLoginUTC = new DateTime(2018, 6, 13),
                LastUpdatedUTC = new DateTime(2018, 6, 13),
                CreatedUTC = new DateTime(2018, 6, 13)
            }
        };

        public static SiteAccount[] Sites =
        {
            new SiteAccount()
            {
                ID = Guid.Parse("4a93afc2-8ef0-4d91-9374-67e60fc336a8"),
                SiteAccountName = "Awesome Possum Admins",
                IsActive = true,
                LastUpdatedUTC = new DateTime(2018,6,13),
            }
        };

        public static SiteAccountUserAccount[] SiteUsers =
        {
            new SiteAccountUserAccount()
            {
                UserAccountID = Guid.Parse("4D8881BD-DB0A-4725-9CF0-2C4390013A30"),
                SiteAccountID = Guid.Parse("4a93afc2-8ef0-4d91-9374-67e60fc336a8"),
                RoleID = Role.OwnerRoleID,
                IsActive = true,
                IsMemberOfAllDepartments = true,
                LastLoginUTC = new DateTime(2018,6,13)
            },
            new SiteAccountUserAccount()
            {
                UserAccountID = Guid.Parse("F84D5D8F-131E-4554-A45D-E1D03C02BC43"),
                SiteAccountID = Guid.Parse("4a93afc2-8ef0-4d91-9374-67e60fc336a8"),
                RoleID = Role.OwnerRoleID,
                IsActive = true,
                IsMemberOfAllDepartments = true,
                LastLoginUTC = new DateTime(2018,6,13)
            }
        };

        public static SiteAccountUserAccountDepartment[] SiteUserDepartments =
        {

        }
    }
}
