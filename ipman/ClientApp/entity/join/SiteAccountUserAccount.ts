import SiteAccount from "../SiteAccount";
import UserAccount from "../UserAccount";
import Role from "../Role";

export default interface SiteAccountUserAccount {
    SiteAccountID: string;
    SiteAccount?: SiteAccount;
    UserAccountID: string;
    UserAccount?: UserAccount;
    RoleID: string;
    IsActive: boolean;
    IsMemberOfAllDepartments: boolean;
    LastLoginUTC: Date | string;
    Role: Role;
}