import { UserAccount, SiteAccountUserAccount } from "@entity";

export interface SaveUserAccountRequest
{
    UserAccount: UserAccount;
    SiteAccountUserAccounts?: SiteAccountUserAccount[];
    ShouldUpdateAllProps: boolean;
    PropsToUpdate: string[];
}