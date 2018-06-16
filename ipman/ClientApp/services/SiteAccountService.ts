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
        await this._connection.start();
    }

    public GetUserSiteAccounts(): Promise<any>
    {
        return new Promise((resolve, reject) => 
        {
            this._connection.invoke(this.GetUserSiteAccounts.name)
                            .then(resolve)
                            .catch(reject);
        })
    }
}