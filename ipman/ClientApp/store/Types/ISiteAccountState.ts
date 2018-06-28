import { SiteAccount } from "@entity";

export interface ISiteAccountState
{
    siteAccountSearchCriteria: any;
    isSearchingSiteAccountList: boolean;
    siteAccountList: SiteAccount[];
}