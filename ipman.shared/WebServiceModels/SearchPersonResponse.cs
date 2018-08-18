using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SearchPersonResponse: BaseResponse
    {
        public List<Person> Persons { get; set; }
    }
}
