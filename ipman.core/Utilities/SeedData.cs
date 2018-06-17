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
        public static Guid UserRobertID = Guid.Parse("F84D5D8F-131E-4554-A45D-E1D03C02BC43");
        public static Guid UserBudNJoeID = Guid.Parse("4D8881BD-DB0A-4725-9CF0-2C4390013A30");
        public static Guid SiteAdminID = Guid.Parse("4a93afc2-8ef0-4d91-9374-67e60fc336a8");
        public static Guid DepartmentWalrusID = Guid.Parse("e29241de-853c-4a05-9928-1c6ac7f73b25");
        public static Guid SiteUserRobertID = Guid.Parse("f9c6aae5-fe6c-43bf-92b4-bf79ba92d2fe");
        public static Guid SiteUserBudNJoeID = Guid.Parse("143bf075-478f-4e1a-b8d8-889be2af42a4");

        public static DateTime TheDate = new DateTime(2018, 6, 13);

        public static UserAccount[] Users = 
        {
            new UserAccount()
            {
                ID = UserBudNJoeID,
                EmailAddress = "budnjoe@gmail.com",
                FirstName = "rob",
                LastName = "Hardesty",
                GitHubID = null,
                GoogleID = null,
                AvatarLink = @"https://lh4.googleusercontent.com/-gPvw9sU8Mpc/AAAAAAAAAAI/AAAAAAAAAOY/HQ2yjXFKeEk/photo.jpg?sz=50",
                LastLoginProvider = AuthenticationProvider.Google,
                LastLoginUTC = TheDate,
                LastUpdatedUTC = TheDate,
                CreatedUTC = TheDate
            },
            new UserAccount()
            {
                ID = UserRobertID,
                EmailAddress = "robert.hardesty.mail@gmail.com",
                FirstName = "Robert",
                LastName = "Hardesty",
                GitHubID = null,
                GoogleID = null,
                AvatarLink = @"https://lh3.googleusercontent.com/-pu8oCttY3pE/AAAAAAAAAAI/AAAAAAAAAAA/h5YVW6XWCK4/photo.jpg?sz=50",
                LastLoginProvider = AuthenticationProvider.Google,
                LastLoginUTC = TheDate,
                LastUpdatedUTC = TheDate,
                CreatedUTC = TheDate
            }
        };

        public static SiteAccount[] Sites =
        {
            new SiteAccount()
            {
                ID = SiteAdminID,
                SiteAccountName = "Awesome Possum Admins",
                IsActive = true,
                LastUpdatedUTC = TheDate,
                CreatedUTC = TheDate
            }
        };

        public static Department[] Departments =
        {
            new Department()
            {
                ID = DepartmentWalrusID,
                DepartmentName = "I AM THE WALRUS",
                IsActive = true,
                CreatedUTC = TheDate,
                LastUpdatedUTC = TheDate,
                SiteAccountID = SiteAdminID
            }
        };

        public static SiteAccountUserAccount[] SiteUsers =
        {
            new SiteAccountUserAccount()
            {
                UserAccountID = UserBudNJoeID,
                SiteAccountID = SiteAdminID,
                RoleID = Role.OwnerRoleID,
                IsActive = true,
                IsMemberOfAllDepartments = true,
                LastLoginUTC = TheDate,
                CreatedUTC = TheDate
            },
            new SiteAccountUserAccount()
            {
                UserAccountID = UserRobertID,
                SiteAccountID = SiteAdminID,
                RoleID = Role.OwnerRoleID,
                IsActive = true,
                IsMemberOfAllDepartments = true,
                LastLoginUTC = TheDate,
                CreatedUTC = TheDate
            }
        };

        public static SiteAccountUserAccountDepartment[] SiteUserDepartments =
        {
            new SiteAccountUserAccountDepartment()
            {
                IsActive = true,
                CreatedUTC = TheDate,
                SiteAccountUserAccountID = SiteUserBudNJoeID,
                DepartmentID = DepartmentWalrusID
            },
            new SiteAccountUserAccountDepartment()
            {
                IsActive = true,
                CreatedUTC = TheDate,
                SiteAccountUserAccountID = SiteUserRobertID,
                DepartmentID = DepartmentWalrusID
            }
        };
    }
}
