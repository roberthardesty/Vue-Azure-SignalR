using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class AddPostRequest
    {
        public Guid SiteAccountID { get; set; }
        public Post Post { get; set; }
    }
}
