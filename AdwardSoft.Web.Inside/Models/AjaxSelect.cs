using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public class AjaxSelect
    {
        public string Search { get; set; }

        public int Page { get; set; }
    }

    public class SelectModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class SelectResultModel
    {
        public List<SelectModel> Items { get; set; }

        public int Count { get; set; }
    }
}
