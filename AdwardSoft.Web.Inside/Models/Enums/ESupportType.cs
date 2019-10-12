using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum ESupportType
    {
        [Description("Giao hàng")]
        [Display(Name = "Giao hàng")]
        delivery,
        [Description("Đồ ăn")]
        [Display(Name = "Đồ ăn")]
        food ,
        [Description("Lái xe hộ")]
        [Display(Name = "Lái xe hộ")]
        drive,
        [Description("Tài khoản")]
        [Display(Name = "Tài khoản")]
        account,
        [Description("Khác")]
        [Display(Name = "Khác")]
        other
    }
}
