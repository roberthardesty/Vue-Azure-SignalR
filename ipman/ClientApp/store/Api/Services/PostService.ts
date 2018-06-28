import { ServiceBase } from "./ServiceBase";
import { AxiosSuccess, AxiosError } from "../ApiTypes";

export default class PostService extends ServiceBase
{
    public ServiceName ="Post";

    public async GetPostsBySiteAccountName(name: string): Promise<AxiosSuccess | AxiosError> 
    {
        return await this.Get(this.GetPostsBySiteAccountName.name + "?name=" + name);
    }
}
