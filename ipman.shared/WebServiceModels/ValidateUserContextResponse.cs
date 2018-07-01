using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class ValidateUserContextResponse: BaseResponse
    {
        public string NewToken { get; set; }
        public UserAccount UserAcount { get; set; }
    }
}
