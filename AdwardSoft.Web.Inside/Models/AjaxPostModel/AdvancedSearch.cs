using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public class AdvancedSearch
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        DateTime? _fromdate = null;
        public DateTime? FormDate
        {
            get
            {
                if (!string.IsNullOrEmpty(formDateString) || _fromdate != null)
                    return _fromdate == null ? _fromdate = DateTime.ParseExact(formDateString, "dd/MM/yyyy", CultureInfo.InstalledUICulture) : _fromdate;
                return null;
            }
            set { _fromdate = value; }
        }
        DateTime? _todate = null;
        public DateTime? ToDate
        {
            get
            {
                if (!string.IsNullOrEmpty(toDateString) || _todate != null)
                    return _todate == null ? _todate = DateTime.ParseExact(toDateString, "dd/MM/yyyy", CultureInfo.InstalledUICulture) : _todate;
                return null;
            }
            set { _todate = value; }
        }
        public string formDateString { get; set; }
        public string toDateString { get; set; }
        public string timeCheckIn { get; set; }
        public string timeCheckOut { get; set; }
        public string customerName { get; set; }
        public string staff { get; set; }
        public bool isPrint { get; set; }
    }
}
