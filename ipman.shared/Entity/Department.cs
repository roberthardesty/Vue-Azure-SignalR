﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity
{
    public class Department: EntityBase
    {
        public string DepartmentName { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime LastUpdatedUTC { get; set; }
        public bool IsActive { get; set; }
    }
}
