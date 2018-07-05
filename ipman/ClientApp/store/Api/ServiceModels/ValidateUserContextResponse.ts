import { UserAccount } from "@entity";
import { BaseResponse } from "./BaseResponse";

export interface ValidateUserContextResponse extends BaseResponse
{
    NewToken: string;
    UserAcount: UserAccount;
}