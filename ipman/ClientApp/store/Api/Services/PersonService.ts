import { ServiceBase } from "./ServiceBase";
import { PersonSearchCriteria } from "@entity";
import { AxiosSuccess, AxiosError } from "../ApiTypes";
import { SavePersonRequest, SearchPersonRequest } from "@serviceModels";

export default class PersonService extends ServiceBase
{
    public ServiceName ="Person";

    public async GetUserPersons(): Promise<AxiosSuccess | AxiosError>
    {
        return await this.Get(this.GetUserPersons.name);
    }

    public async Search(searchCriteria: PersonSearchCriteria): Promise<AxiosSuccess | AxiosError>
    {
        let request: SearchPersonRequest = 
        { 
            SearchCriteria : searchCriteria
        }
        return await this.Post(this.Search.name, request);
    }

    public async Save(saveRequest: SavePersonRequest) : Promise<AxiosSuccess | AxiosError>
    {
        return await this.Post(this.Save.name, saveRequest);
    }

}