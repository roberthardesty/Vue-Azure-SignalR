import { HubConnection } from "@aspnet/signalr";
import { ServiceBase } from "./ServiceBase";
import { UserAccount, UserAccountSearchCriteria } from "@entity";
import { AxiosSuccess, AxiosError } from "../ApiTypes";
import { SearchUserAccountsRequest, SaveUserAccountRequest } from "@serviceModels";

export default class UserAccountService extends ServiceBase
{
    public ServiceName ="UserAccount";

    public async SearchUserAccounts(searchCriteria: UserAccountSearchCriteria): Promise<AxiosSuccess | AxiosError>
    {
        let request: SearchUserAccountsRequest = { UserAccountSearchCriteria: searchCriteria}
        return await this.Post(this.SearchUserAccounts.name, request);
    }

    public async SaveUserAccount(userAccount: SaveUserAccountRequest): Promise<AxiosSuccess | AxiosError>
    {
        return await this.Post(this.SaveUserAccount.name, userAccount);
    }
}