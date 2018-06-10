namespace ipman.shared.WebServiceModels
{
    public interface IWebServiceResponse
    {
        bool IsError { get; }

        ResponseError ResponseError { get; set; }
    }
}
