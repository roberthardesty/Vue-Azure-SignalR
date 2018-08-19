import { SearchCriteria } from "./SearchCriteria";

export default interface PersonSearchCriteria extends SearchCriteria
{
    IncludedPersonIDs: string[];
    ExcludedPersonIDs: string[];
    Keyword: string;
    CommunityID: string;
}