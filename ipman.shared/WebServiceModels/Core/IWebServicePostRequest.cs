using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipman.shared.WebServiceModels
{
    public interface IWebServicePostRequest<TResponseType> where TResponseType : IWebServiceResponse
    {
    }
}
