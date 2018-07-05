import { ResponseError } from "./ResponseError";

export  class BaseResponse {
    /**simple helper for checking if this response was an error */
    IsError: boolean;
    
    ResponseError: ResponseError;
}