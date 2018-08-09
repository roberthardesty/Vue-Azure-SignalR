import { SiteAccount, SiteAccountUserAccount } from "@entity";

export interface SaveSiteAccountRequest
{
    SiteAccount: SiteAccount;
    SiteAccountUserAccounts: SiteAccountUserAccount[];
    ShouldUpdateAllProps: boolean;
    PropsToUpdate: string[];
}