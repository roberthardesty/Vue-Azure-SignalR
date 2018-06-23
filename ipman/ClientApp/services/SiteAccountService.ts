import { HubConnection } from "@aspnet/signalr";
import { ServiceBase } from "./ServiceBase";

export default class SiteAccountService extends ServiceBase
{
    constructor(signalR: any) 
    {
        super(signalR)
    }

    public ServiceName ="siteAccount";

    public async GetUserSiteAccounts(): Promise<any> 
    {
        return new Promise(async (resolve, reject) => {
            await this._connection.start();
            this._connection.invoke(this.GetUserSiteAccounts.name)
                .then(resolve)
                .catch(reject);
        })
    }
}