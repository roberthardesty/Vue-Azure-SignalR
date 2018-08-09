import { HubConnection } from "@aspnet/signalr";
import { ServiceBase } from "./ServiceBase";
import { SiteAccount, SiteAccountSearchCriteria } from "@entity";
import { AxiosSuccess, AxiosError } from "../ApiTypes";
import { SearchSiteAccountRequest, SaveSiteAccountRequest } from "@serviceModels";

export default class SiteAccountService extends ServiceBase
{
    public ServiceName ="SiteAccount";

    public async GetUserSiteAccounts(): Promise<AxiosSuccess | AxiosError>
    {
        return await this.Get(this.GetUserSiteAccounts.name);
    }

    public async Search(searchCriteria: SiteAccountSearchCriteria): Promise<AxiosSuccess | AxiosError>
    {
        let request: SearchSiteAccountRequest = 
        { 
            SiteAccountSearchCriteria : searchCriteria
        }
        return await this.Post(this.Search.name, request);
    }

    public async Save(saveRequest: SaveSiteAccountRequest) : Promise<AxiosSuccess | AxiosError>
    {
        return await this.Post(this.Save.name, saveRequest);
    }

}