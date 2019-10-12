using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public class SearchRecordModel
    {
        public int Type { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameDepartment { get; set; }
        public string NamePosition { get; set; }
        public string Value { get; set; }
    }
}
