
export class ResponseError
{
    
    /**basic message that could be shown to a user */
    ErrorMessage: string;
    
    /**for things like all the fields that failed model validation */
    ErrorDetails: string[];
    
    /**id of related entry in log if applicable  */
    ErrorLogId: number | null;
    
    ErrorType: ResponseErrorType;
    
    public ToFormattedString() {
        return 'Log ID: ${this.ErrorLogId} Type: ${this.ErrorType} Message: ${this.ErrorMessage} Details: ${this.ErrorDetails}';
    }
    
}

export enum ResponseErrorType {
    ModelValidation,
    Exception,
    Other
}

