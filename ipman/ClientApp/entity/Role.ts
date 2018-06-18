export default class Role
{
    public constructor(roleID: string, roleName: string)
    {
        this.RoleID = roleID;
        this.RoleName = roleName;
    }

    public RoleID: string;
    public RoleName: string;

    private static AdminRoleID = "222DD46F-9B44-415C-B6D0-25EB8EE431A5";
    private static BasicRoleID = "16E3371B-C4CD-4BEB-9ACF-3B0404646A37";
    private static GuestRoleID = "09D60B1D-C9F9-48E6-AAF9-8B0E277D95ED";
    private static OwnerRoleID = "2FA6B838-8B5B-4BB0-B888-FFCE4FB7B7B7";

    public static AdminRole: Role = new Role(Role.AdminRoleID, "Admin");
    public static BasicRole: Role = new Role(Role.BasicRoleID, "Basic");
    public static GuestRole: Role = new Role(Role.GuestRoleID, "Guest");
    public static OwnerRole: Role = new Role(Role.OwnerRoleID, "Owner");

    public static Find(roleID: string) : Role
    {
        return [this.AdminRole, this.BasicRole, this.GuestRole, this.OwnerRole].find(role => role.RoleID.toLowerCase() === roleID.toLowerCase());
    }
}
