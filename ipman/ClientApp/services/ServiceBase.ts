import { HubConnection } from "@aspnet/signalr";
import axios from 'axios'

export abstract class ServiceBase
{
    constructor(signalR: any)
    {
        this._signalR = signalR;
    }

    protected _connection: HubConnection;
    protected _signalR : any;
    
    public abstract ServiceName: string;

    public async Connect()
    {
        this._connection = new this._signalR.HubConnectionBuilder()
            .withUrl("/" + this.ServiceName)
            .configureLogging(this._signalR.LogLevel.Error)
            .build();
    }

    public async Get(method: string)
    {
        return await axios.get("/api/" + this.ServiceName + "/" + method);
    }
}