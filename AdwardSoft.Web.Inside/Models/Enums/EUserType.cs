using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EUserType
    {
        [Description("Nội bộ")]
        [Display(Name="Nội bộ")]
        Internal = 1,
        [Description("Khách hàng")]
        [Display(Name="Khách hàng")]
        Customer = 2,
        [Description("Tài xế")]
        [Display(Name="Tài xế")]
        Driver = 3,
        [Description("Lái xe hộ")]
        [Display(Name = "Lái xe hộ")]
        DriverPartner = 4
    }
}
