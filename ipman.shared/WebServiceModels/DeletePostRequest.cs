using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class DeletePostRequest
    {
        public Guid SiteAccountID {get;set;}
        public Guid PostID { get; set; }
    }
}
