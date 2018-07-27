import { ServiceBase } from "./ServiceBase";
import { AxiosSuccess, AxiosError } from "../ApiTypes";
import { GetPostsBySiteAccountRequest } from "../ServiceModels/GetPostsBySiteAccountRequest";

export default class PostService extends ServiceBase
{
    public ServiceName ="Post";

    public async GetPostsBySiteAccount(id: string): Promise<AxiosSuccess | AxiosError> 
    {
        let request: GetPostsBySiteAccountRequest = { SiteAccountID: id };
        return await this.Get(this.GetPostsBySiteAccount.name, request);
    }
}
