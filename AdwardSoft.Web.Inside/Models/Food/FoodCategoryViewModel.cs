using Microsoft.AspNetCore.Http;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{

    [ProtoContract]
    public class FoodCategoryViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        [Display(Name ="Tên danh mục")]
        [Required(ErrorMessage ="Tên không được để trống")]
        public string Name { get; set; }
        [ProtoMember(3)]
        [Display(Name = "Icon")]
        [Required(ErrorMessage = "Icon không được để trống")]
        public string Icon { get; set; }
        [ProtoMember(4)]
        public int Sort { get; set; }
        public IFormFile IconFile { get; set; }
        [Required(ErrorMessage = "Icon không được để trống")]
        public string IconShow { get; set; }
    }
}
