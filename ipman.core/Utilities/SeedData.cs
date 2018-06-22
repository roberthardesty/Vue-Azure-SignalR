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

        public static Guid PostLegalID = Guid.Parse("7e6333c5-8223-4ff4-a3f5-27e97fa087a6");
        public static Guid PostRobWizardID = Guid.Parse("8c84658a-2210-4f5d-a7e2-7da2b32e64c3");
        public static Guid PostAverageKillCountID = Guid.Parse("f723bae5-b6c0-43e0-8828-82adb3e9b088");

        public static Guid TagAdminsID = Guid.Parse("afd3afe9-f67a-47e4-81f2-91efa7a2fccd");
        public static Guid TagBasedOnUserID = Guid.Parse("221e7112-b974-4269-b3c2-1f9a0c5e8fff");
        public static Guid TagTrueFalseID = Guid.Parse("09965041-0340-4fc7-82c4-0e65811c2ca2");
        public static Guid TagPickANumberID = Guid.Parse("1a24966b-2979-49f9-8cc4-b21b6449f53e");

        public static DateTime TheDate = new DateTime(2018, 6, 13);

        public static UserAccount[] Users = 
        {
            new UserAccount()
            {
                ID = UserBudNJoeID,
                EmailAddress = "budnjoe@gmail.com",
                Username = "Trundle",
                FirstName = "rob",
                LastName = "Hardesty",
                GitHubID = null,
                GoogleID = null,
                AvatarLink = @"https://lh4.googleusercontent.com/-gPvw9sU8Mpc/AAAAAAAAAAI/AAAAAAAAAOY/HQ2yjXFKeEk/photo.jpg?sz=50",
                LastLoginProvider = AuthenticationProvider.Google,
                LastLoginUTC = TheDate,
                LastUpdatedUTC = TheDate,
                CreatedUTC = TheDate,
            },
            new UserAccount()
            {
                ID = UserRobertID,
                EmailAddress = "robert.hardesty.mail@gmail.com",
                Username = "DreadPirateRobert",
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

        public static Post[] Posts =
        {
            new Post
            {
                ID = PostLegalID,
                PostTitle = "I BET: All Admins Go Directly To Jail, Do Not Pass GO. Do Not Collect 200 Dollars",
                PostDescription = "Lawyers were not obtained and therefore this endeavor has led to all of us going directly to jail. See you there! Also if you know any lawyers please send me their contact info.",
                SiteAccountID = SiteAdminID,
                UserAccountCreatorID = UserRobertID,
                CreatedUTC = TheDate,
                LastUpdatedUTC = TheDate + new TimeSpan(120,0,0) // plus five days
            },
            new Post
            {
                ID = PostRobWizardID,
                PostTitle = "I BET: Rob is 80% Wizard",
                PostDescription = "How else could this all have been possible?",
                SiteAccountID = SiteAdminID,
                UserAccountCreatorID = UserBudNJoeID,
                CreatedUTC = TheDate,
                LastUpdatedUTC = TheDate + new TimeSpan(168,0,0) // plus five days
            },
            new Post
            {
                ID = PostRobWizardID,
                PostTitle = "Average Kills Per Day In The Month May for MANG0",
                PostDescription = "How many kills will mango average on a daily basis for the month of may?",
                SiteAccountID = SiteAdminID,
                UserAccountCreatorID = UserBudNJoeID,
                CreatedUTC = TheDate,
                LastUpdatedUTC = TheDate + new TimeSpan(96,0,0) // plus five days
            }
        };

        public static Tag[] Tags =
        {
            new Tag
            {
                ID = TagAdminsID,
                TagName = "Admin",
                TagImage = "admins.svg",
                IsActive = true,
                SiteAccountID = SiteAdminID,
                CreatedUTC = TheDate
            },
            new Tag
            {
                ID = TagBasedOnUserID,
                TagName = "BasedOnUser",
                TagImage = "user.svg",
                IsActive = true,
                SiteAccountID = SiteAdminID,
                CreatedUTC = TheDate
            },
            new Tag
            {
                ID = TagPickANumberID,
                TagName = "PickANumber",
                TagImage = "admins.svg",
                IsActive = true,
                SiteAccountID = SiteAdminID,
                CreatedUTC = TheDate
            }
        };

        public static PostTag[] PostTags =
        {
            new PostTag
            {
                PostID = PostAverageKillCountID,
                TagID = TagPickANumberID,
                CreatedUTC = TheDate
            },
            new PostTag
            {
                PostID = PostAverageKillCountID,
                TagID = TagBasedOnUserID,
                CreatedUTC = TheDate
            },
            new PostTag
            {
                PostID = PostLegalID,
                TagID = TagAdminsID,
                CreatedUTC = TheDate
            },
            new PostTag
            {
                PostID = PostRobWizardID,
                TagID = TagTrueFalseID,
                CreatedUTC = TheDate
            },
            new PostTag
            {
                PostID = PostRobWizardID,
                TagID = TagBasedOnUserID,
                CreatedUTC = TheDate
            }
        };

        public static SiteAccountUserAccount[] SiteUsers =
        {
            new SiteAccountUserAccount
            {
                UserAccountID = UserBudNJoeID,
                SiteAccountID = SiteAdminID,
                RoleID = Role.OwnerRoleID,
                IsActive = true,
                IsMemberOfAllDepartments = true,
                LastLoginUTC = TheDate,
                CreatedUTC = TheDate
            },
            new SiteAccountUserAccount
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
            new SiteAccountUserAccountDepartment
            {
                IsActive = true,
                CreatedUTC = TheDate,
                SiteAccountUserAccountID = SiteUserBudNJoeID,
                DepartmentID = DepartmentWalrusID
            },
            new SiteAccountUserAccountDepartment
            {
                IsActive = true,
                CreatedUTC = TheDate,
                SiteAccountUserAccountID = SiteUserRobertID,
                DepartmentID = DepartmentWalrusID
            }
        };
    }
}
