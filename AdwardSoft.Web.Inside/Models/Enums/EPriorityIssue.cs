using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EPriorityIssue
    {
        [Description("<i class='fa fa-arrow-up' style='color: #bb0000'></i>")]
        //[Description("&#xf062;")]
        [Display(Name = "Khẩn cấp")]
        Emergency,
        [Description("<i class='fa fa-arrow-up' style='color: red'></i>")]
        [Display(Name = "Cao")]
        High,
        [Description("<i class='fa fa-arrow-down' style='color: #ff7043'></i>")]
        [Display(Name = "Trung bình")]
        Normal,
        [Description("<i class='fa fa-arrow-down' style='color: green'></i>")]
        [Display(Name = "Thấp")]
        Low
    }
}
