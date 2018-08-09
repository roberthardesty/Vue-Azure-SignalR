import { BaseResponse } from "./BaseResponse";
import { SiteAccount } from "@entity";

export interface SaveSiteAccountResponse extends BaseResponse
{
    SiteAccount: SiteAccount;
}