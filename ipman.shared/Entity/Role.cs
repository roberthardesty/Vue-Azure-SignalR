using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ipman.shared.Entity
{
    public class Role
    {
        // These facilitate unit test cases that require constants.
        public const string AdminRoleIDString = "222DD46F-9B44-415C-B6D0-25EB8EE431A5";
        public const string BasicRoleIDString = "16E3371B-C4CD-4BEB-9ACF-3B0404646A37";
        public const string GuestRoleIDString = "09D60B1D-C9F9-48E6-AAF9-8B0E277D95ED";
        public const string OwnerRoleIDString = "2FA6B838-8B5B-4BB0-B888-FFCE4FB7B7B7";

        // These have been seeded in the database. 
        public static readonly Guid AdminRoleID = new Guid(AdminRoleIDString);
        public static readonly Guid BasicRoleID = new Guid(BasicRoleIDString);
        public static readonly Guid GuestRoleID = new Guid(GuestRoleIDString);
        public static readonly Guid OwnerRoleID = new Guid(OwnerRoleIDString);

        public static Role AdminRole = new Role { RoleID = AdminRoleID, RoleName = "Admin" };
        public static Role BasicRole = new Role { RoleID = BasicRoleID, RoleName = "Basic" };
        public static Role GuestRole = new Role { RoleID = GuestRoleID, RoleName = "Guest" };
        public static Role OwnerRole = new Role { RoleID = OwnerRoleID, RoleName = "Owner" };

        private static ReadOnlyCollection<Role> _allRoles;

        public static ReadOnlyCollection<Role> AllRoles => _allRoles ?? (_allRoles = new ReadOnlyCollection<Role>(new List<Role>
                                                           {
                                                               AdminRole,
                                                               BasicRole,
                                                               GuestRole,
                                                               OwnerRole
                                                           }));

        public Guid RoleID { get; set; }
        public string RoleName { get; set; }

        #region Overrides of Object

        public override bool Equals(object obj)
        {
            var other = obj as Role;

            return other?.RoleID == RoleID;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return RoleID.GetHashCode();
        }

        #endregion

        #region Methods

        public static Role Find(Guid roleID)
        {
            return AllRoles.FirstOrDefault(role => role.RoleID == roleID);
        }

        #endregion
    }
}
