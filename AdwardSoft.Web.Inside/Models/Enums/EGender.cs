using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EGender
    {
        [Description("Nam")]
        [Display(Name = "Nam")]
        male,
        [Description("Nữ")]
        [Display(Name = "Nữ")]
        female,
        [Description("Giới tính khác")]
        [Display(Name = "Giới tính khác")]
        other
    }
}
