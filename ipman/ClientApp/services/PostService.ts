import { ServiceBase } from "./ServiceBase";

export default class PostService extends ServiceBase
{
    constructor(signalR: any) 
    {
        super(signalR)
    }

    public ServiceName ="Post";

    public async GetPostsBySiteAccountName(name: string): Promise<any> 
    {
        return await this.Get(this.GetPostsBySiteAccountName.name + "?name=" + name);
        // return new Promise(async (resolve, reject) => {
        //     await this._connection.start();
        //     this._connection.invoke(this.GetPostsBySiteAccountName.name)
        //         .then(resolve)
        //         .catch(reject);
        // })
    }
}
