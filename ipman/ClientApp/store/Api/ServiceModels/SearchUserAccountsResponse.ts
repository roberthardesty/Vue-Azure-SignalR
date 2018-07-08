import { UserAccount } from "@entity";
import { BaseResponse } from "./BaseResponse";

export interface SearchUserAccountsResponse extends BaseResponse
{
    UserAccounts: UserAccount[];
}