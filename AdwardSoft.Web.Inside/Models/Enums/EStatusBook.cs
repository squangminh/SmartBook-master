using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EStatusBook
    {
        [Description("Đang tiến hành")]
        [Display(Name = "Đang tiến hành")]
        Processing,
        [Description("Hoàn thành")]
        [Display(Name = "Hoàn thành")]
        Complete,
        [Description("Tạm dùng")]
        [Display(Name = "Tạm dùng")]
        Pause
    }
}
