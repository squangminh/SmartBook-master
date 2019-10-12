using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class ModuleViewModel
    {

        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Không được để trống")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; }

        [ProtoMember(3)]
        [Display(Name = "Link")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Link { get; set; }

        [ProtoMember(4)]
        [Display(Name = "ClassName")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ClassName { get; set; }

        [ProtoMember(5)]
        [Display(Name = "Controller")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ControllerName { get; set; }

        [ProtoMember(6)]
        [Display(Name = "ParentId")]
        public int ParentId { get; set; }

        [ProtoMember(7)]
        [Display(Name = "Sort")]
        public int Sort { get; set; }


        public ModuleViewModel()
        {         
            ListModule = new List<ModuleViewModel>();
        }
        public IEnumerable<ModuleViewModel> ListModule { get; set; }
    }


    [ProtoContract]
    public class ModuleJsonViewModel
    {

        [ProtoMember(1)]
        public string Json { get; set; }
    
    }
}
