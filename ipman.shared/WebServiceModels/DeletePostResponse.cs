using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class DeletePostResponse: BaseResponse
    {
        public Guid PostID { get; set; }
    }
}
