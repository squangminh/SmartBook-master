using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    public enum EReligious
    {
        [Description("Không")]
        [Display(Name = "Không")]
        No,
        [Description("Phật giáo")]
        [Display(Name = "Phật giáo")]
        Buddhism,
        [Description("Công giáo")]
        [Display(Name = "Công giáo")]
        Catholic
    }
}
