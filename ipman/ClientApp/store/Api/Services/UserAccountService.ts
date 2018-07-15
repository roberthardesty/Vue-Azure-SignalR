import { HubConnection } from "@aspnet/signalr";
import { ServiceBase } from "./ServiceBase";
import { UserAccount } from "@entity";
import { AxiosSuccess, AxiosError } from "../ApiTypes";
import { SearchUserAccountsRequest, SaveUserAccountRequest } from "@serviceModels";

export default class UserAccountService extends ServiceBase
{
    public ServiceName ="UserAccount";

    public async SearchUserAccounts(searchCriteria: SearchUserAccountsRequest): Promise<AxiosSuccess | AxiosError>
    {
        return await this.Post(this.SearchUserAccounts.name, searchCriteria);
    }

    public async SaveUserAccount(userAccount: SaveUserAccountRequest): Promise<AxiosSuccess | AxiosError>
    {
        return await this.Post(this.SaveUserAccount.name, userAccount);
    }
}