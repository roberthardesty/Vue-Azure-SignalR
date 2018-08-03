import * as signalR from '@aspnet/signalr'

export abstract class ServiceClient
{
    private _serviceName: string;
    constructor(serviceName: string)
    {
        this._serviceName = serviceName;
    }

    protected BuildConnection() : signalR.HubConnection
    {
        let connection =  new signalR.HubConnectionBuilder()
                                .withUrl("/" + this._serviceName)
                                .build();

        connection.onclose(this.OnConnectionClosed);
        
        return connection;
    }

    public abstract OnConnectionClosed(error: Error): void
}