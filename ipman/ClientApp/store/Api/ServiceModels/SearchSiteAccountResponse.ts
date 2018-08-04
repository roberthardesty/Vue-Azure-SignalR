import { SiteAccount } from "@entity";
import { BaseResponse } from "./BaseResponse";

export interface SearchSiteAccountResponse extends BaseResponse
{
    SiteAccounts: SiteAccount[];
}