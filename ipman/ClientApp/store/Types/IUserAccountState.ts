import { UserAccount } from "@entity";

export interface IUserAccountState
{
    userAccountSearchCriteria: any;
    isSearchingUserAccountList: boolean;
    userAccountList: UserAccount[];
}