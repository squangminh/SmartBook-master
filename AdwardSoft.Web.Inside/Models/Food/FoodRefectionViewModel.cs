using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class FoodRefectionViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
        [ProtoMember(3)]
        [Display(Name = "Thời gian đầu")]
        [Required(ErrorMessage = "Thời gian đầu không được để trống")]
        public string DisplayTimeFrom { get; set; }
        [ProtoMember(4)]
        [Display(Name = "Thời gian cuối")]
        [Required(ErrorMessage = "Thời gian cuối không được để trống")]
        public string DisplayTimeTo { get; set; }
    }
}
