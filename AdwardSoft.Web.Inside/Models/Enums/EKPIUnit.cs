using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EKPIUnit
    {
        [Description("%")]
        [Display(Name = "%")]
        Percent,
        [Description("Trường hợp")]
        [Display(Name = "Trường hợp")]
        Case,
        [Description("Điểm")]
        [Display(Name = "Điểm")]
        Point
    }
}
