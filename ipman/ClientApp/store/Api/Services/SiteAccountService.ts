import { HubConnection } from "@aspnet/signalr";
import { ServiceBase } from "./ServiceBase";
import { SiteAccount } from "@entity";
import { AxiosSuccess, AxiosError } from "../ApiTypes";

export default class SiteAccountService extends ServiceBase
{
    public ServiceName ="SiteAccount";

    public async GetUserSiteAccounts(): Promise<AxiosSuccess | AxiosError>
    {
        return await this.Get(this.GetUserSiteAccounts.name);
    }
}