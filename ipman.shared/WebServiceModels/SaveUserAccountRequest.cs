using ipman.shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public class SaveUserAccountRequest
    {
        [Required]
        public UserAccount UserAccount { get; set; }
        public bool ShouldUpdateAllProps { get; set; }
        public List<string> PropsToUpdate { get; set; }
    }
}
