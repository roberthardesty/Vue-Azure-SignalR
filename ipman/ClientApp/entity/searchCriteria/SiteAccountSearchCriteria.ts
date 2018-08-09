import { SearchCriteria } from "./SearchCriteria";

export default interface SiteAccountSearchCriteria extends SearchCriteria
{
    Keyword: string;
    OtherUserSites: boolean;
    CurrentUserSites: boolean;
    UserEmail: string;
    IncludeSiteAccountUserAccounts: boolean;
    ExcludedSiteAccounts?: string[];
    IncludedSiteAccounts?: string[];
}