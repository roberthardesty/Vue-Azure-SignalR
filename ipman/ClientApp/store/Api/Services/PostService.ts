import { ServiceBase } from "./ServiceBase";
import { AxiosSuccess, AxiosError } from "../ApiTypes";
import { GetPostsBySiteAccountRequest } from "../ServiceModels/GetPostsBySiteAccountRequest";
import { DeletePostRequest } from "@serviceModels";

export default class PostService extends ServiceBase
{
    public ServiceName ="Post";

    public async DeletePost(postID: string, siteID: string)
    {
        let request: DeletePostRequest = { SiteAccountID: siteID, PostID: postID}
        return await this.Post(this.DeletePost.name, request);
    }

    public async GetPostsBySiteAccount(id: string): Promise<AxiosSuccess | AxiosError> 
    {
        let request: GetPostsBySiteAccountRequest = { SiteAccountID: id };
        return await this.Get(this.GetPostsBySiteAccount.name, request);
    }
}
