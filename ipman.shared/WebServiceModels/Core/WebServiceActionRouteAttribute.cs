using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipman.shared.WebServiceModels
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class WebServiceActionRouteAttribute : Attribute
    {
        public string ActionRoute { get; }

        public WebServiceActionRouteAttribute(string actionRoute)
        {
            ActionRoute = actionRoute;
        }
    }
}
