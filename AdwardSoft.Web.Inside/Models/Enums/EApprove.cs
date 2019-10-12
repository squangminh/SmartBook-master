using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EApprove
    {
        [Description("Chưa duyệt")]
        [Display(Name = "Chưa duyệt")]
        unapproved = 0,
        [Description("đã duyệt")]
        [Display(Name = "đã duyệt")]
        approved = 1
    }
}
