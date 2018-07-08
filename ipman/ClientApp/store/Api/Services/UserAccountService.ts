import { HubConnection } from "@aspnet/signalr";
import { ServiceBase } from "./ServiceBase";
import { UserAccount } from "@entity";
import { AxiosSuccess, AxiosError } from "../ApiTypes";

export default class UserAccountService extends ServiceBase
{
    public ServiceName ="UserAccount";

    public async SearchUserAccounts(searchCriteria: {username:string}): Promise<AxiosSuccess | AxiosError>
    {
        return await this.Post(this.SearchUserAccounts.name, searchCriteria.username);
    }
}