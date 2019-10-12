using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public class DataTableAjaxPostModel
    {
        public DataTableAjaxPostModel()
        {
            AdvancedSearch = new AdvancedSearch();
        }

        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<Column> columns { get; set; }
        public Search Search { get; set; }
        public List<Order> Order { get; set; }
        public AdvancedSearch AdvancedSearch { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}
