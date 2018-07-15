import { ServiceBase } from "./ServiceBase";
import { AxiosSuccess, AxiosError } from "../ApiTypes";
import { ValidateUserContextRequest } from "@serviceModels"
export default class AuthService extends ServiceBase
{
    public ServiceName ="auth";

    public async ValidateUserContext(validateUserContextRequest: ValidateUserContextRequest): Promise<AxiosSuccess | AxiosError> 
    {
        return await this.Post(this.ValidateUserContext.name, validateUserContextRequest);
    }
}
