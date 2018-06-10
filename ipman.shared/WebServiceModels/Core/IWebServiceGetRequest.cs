namespace ipman.shared.WebServiceModels
{
    public interface IWebServiceGetRequest<TResponseType> where TResponseType : IWebServiceResponse
    {
        string QueryString { get; }
    }
}