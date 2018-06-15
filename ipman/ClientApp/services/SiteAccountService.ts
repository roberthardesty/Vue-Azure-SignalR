import { HubConnection } from "@aspnet/signalr";

export default class SiteAccountService
{
    private _signalR: any;
    private _connection: HubConnection
    constructor(signalR: any)
    {
        this._signalR = signalR;
    }

    public Connect()
    {
        this._connection = this._signalR.HubConnectionBuilder()
                                        .withUrl("/siteAccount")
                                        .configureLogging(this._signalR.LogLevel.Error)
                                        .build();
    }

    public GetUserSiteAccounts(): Promise<any>
    {
        return new Promise((resolve, reject) => 
        {
            console.log(this.GetUserSiteAccounts.name);
            this._connection.invoke(this.GetUserSiteAccounts.name, {})
                            .then(resolve)
                            .catch(reject);
        })
    }
}