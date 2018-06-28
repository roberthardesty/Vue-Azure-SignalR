import { HubConnection } from "@aspnet/signalr";
import Request from '../Instance';
export * from '../ApiTypes';
import axios from 'axios'

export abstract class ServiceBase
{
    public abstract ServiceName: string;

    protected async Get(endPoint: string, payload?)
    {
        return await Request("get", "/api/" + this.ServiceName + "/" + endPoint, payload);
    }
    
    protected async Post(endPoint: string, payload?: any) 
    {
        return await Request('post', "/api/" + this.ServiceName + "/" + endPoint, payload)
    }
}
