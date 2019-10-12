using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EKPIOperation
    {
        [Description(">=")]
        [Display(Name = ">=")]
        GreaterThanOrEqualTo,
        [Description("<=")]
        [Display(Name = "<=")]
        SmallerThanOrEqualTo
    }
}
