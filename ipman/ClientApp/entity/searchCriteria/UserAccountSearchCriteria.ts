import { SearchCriteria } from "./SearchCriteria";
import { Role } from "@entity";

export default interface UserAccountSearchCriteria extends SearchCriteria
    {
        Username: string;
        UserEmail: string;
        IncludeSiteAccountUserAccounts: boolean;
        SiteAccountID: string | null;
        Roles?: Role[];
        ExcludedUserAccounts?: string[];
        IncludedUserAccounts?: string[];
    }

