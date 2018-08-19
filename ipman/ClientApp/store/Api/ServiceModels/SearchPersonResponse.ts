import { BaseResponse } from "./BaseResponse";
import { Person } from "@entity";

export interface SearchPersonResponse extends BaseResponse
{
    Persons: Person[];
}