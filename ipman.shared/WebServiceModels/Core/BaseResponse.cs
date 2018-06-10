namespace ipman.shared.WebServiceModels
{
    public abstract class BaseResponse : IWebServiceResponse
    {
        /// <summary>
        /// simple helper for checking if this response was an error
        /// </summary>
        public bool IsError => ResponseError != null;
        
        public ResponseError ResponseError { get; set; }
    }
}
