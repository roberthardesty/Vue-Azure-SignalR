import { SiteAccount, SiteAccountSearchCriteria } from "@entity";

export interface ISiteAccountState
{
    searchCriteria: SiteAccountSearchCriteria;
    isSearchingSiteAccountList: boolean;
    userSiteAccountList: SiteAccount[];
    searchSiteAccountList: SiteAccount[];
    activeSiteAccount: SiteAccount;
}