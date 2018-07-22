import * as signalR from '@aspnet/signalr'
import { ServiceClient } from './ServiceClient';
import ServiceNames from './ServiceNames';

export default class PiCamService extends ServiceClient
{
    private _connection: signalR.HubConnection;

    constructor() { super(ServiceNames.PiCamService); }

    public async RequestSingleImageCapture()
    {
        let self = this;

        if(!self._connection)
            self._connection = self.BuildConnection();

        await self._connection.start()
            .then(async () => await self._connection.invoke(self.RequestSingleImageCapture.name))
            .then(async () => await self._connection.stop());
    }
}