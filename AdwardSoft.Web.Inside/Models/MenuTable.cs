﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public class MenuTable
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string ClassName { get; set; }
        public int ParentId { get; set; }
        public string ControllerName { get; set; }
    }
}
