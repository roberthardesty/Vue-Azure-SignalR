using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SavePersonResponse: BaseResponse
    {
        public Person Person { get; set; }
    }
}
