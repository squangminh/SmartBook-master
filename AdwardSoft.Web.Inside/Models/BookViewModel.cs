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
    public class BookViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        [Display(Name="Tên truyện")]
        [Required(ErrorMessage ="Tên truyện không được để trống")]
        public string Name { get; set; }
        [ProtoMember(3)]
        [Display(Name = "Ảnh bìa truyện")]
        [Required(ErrorMessage = "Ảnh bìa không được để trống")]
        public string Image { get; set; }
        [ProtoMember(4)]
        [Display(Name = "Nội dung truyện")]
        [Required(ErrorMessage = "Nội dung truyện không được để trống")]
        public string Description { get; set; }
        [ProtoMember(5)]
        [Display(Name = "Trạng thái truyện")]
        [Required(ErrorMessage = "Trạng thái truyện không được để trống")]
        public int Status { get; set; }
        [ProtoMember(6)]
        public DateTime TimeCreate { get; set; }
        [ProtoMember(7)]
        public long CreateUserId { get; set; }
        [ProtoMember(8)]
        public int Count { get; set; }

        public string DateShow => TimeCreate.ToString("dd/MM/yyyy");
        public IFormFile ImageFile { get; set; }
        public List<GenreViewModel> Genres { get; set; }
    }
}
