import { HubConnection } from "@aspnet/signalr";

export default class SiteAccountService 
{
    private _signalR: any;
    private _connection: HubConnection
    constructor(signalR: any) 
    {
        this._signalR = signalR;
    }

    public async Connect() 
    {
        this._connection = new this._signalR.HubConnectionBuilder()
            .withUrl("/siteAccount")
            .configureLogging(this._signalR.LogLevel.Error)
            .build();
    }

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