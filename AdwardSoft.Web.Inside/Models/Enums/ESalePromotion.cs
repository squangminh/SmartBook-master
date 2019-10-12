using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum ESalePromotion
    {
        [Description("Giao hàng")]
        [Display(Name = "Giao hàng")]
        Delivery,
        [Description("Đồ ăn")]
        [Display(Name = "Đồ ăn")]
        Food,
        [Description("Lái xe hộ")]
        [Display(Name = "Lái xe hộ")]
        Driver
    }
}
