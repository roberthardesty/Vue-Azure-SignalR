import * as signalR from '@aspnet/signalr'
import { ServiceClient } from './ServiceClient';
import ServiceNames from './ServiceNames';

export default class PiCamService extends ServiceClient
{
    private _connection: signalR.HubConnection;

    constructor() { super(ServiceNames.PiCamService); }

    public async InitializeConnection()
    {
        let self = this;

        if(!self._connection)
            self._connection = self.BuildConnection();

        await self._connection.start();
    }

    public async CloseConnection()
    {
        let self = this;
        await self._connection.stop();
    }

    public async JoinSpectators()
    {
        let self = this;

        if(!self._connection)
            await self.InitializeConnection();
            
        await self._connection.invoke(self.JoinSpectators.name);
    }

    public async UpdateProgress(callBack: (progress: {StepName: string, CurrentStep: number, TotalSteps:number}) => void)
    {
        let self = this;

        if(!self._connection)
            throw "No connection! - Rob Error"

        setTimeout(()=>{
            self._connection.on(self.UpdateProgress.name, callBack);
        });
    }

    public async RequestSingleImageCapture()
    {
        let self = this;

        if(!self._connection)
            await self.InitializeConnection();

        await self._connection.invoke(self.RequestSingleImageCapture.name);
    }

    public OnConnectionClosed(error: Error)
    {
        console.log(error);
    }
}