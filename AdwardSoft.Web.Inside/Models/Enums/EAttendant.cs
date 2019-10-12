using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EAttendant
    {
        [Description("Công ty/Chi nhánh")]
        [Display(Name = "Công ty/Chi nhánh")]
        company = 0,
        [Description("Đơn vị/Bộ phận")]
        [Display(Name = "Đơn vị/Bộ phận")]
        department = 1
    }
}
