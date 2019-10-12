using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public class Nestable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }
    }
}
