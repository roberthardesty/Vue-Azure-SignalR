import { BaseResponse } from "./BaseResponse";
import { Person } from "@entity";

export interface SavePersonResponse extends BaseResponse
{
    Person: Person;
}