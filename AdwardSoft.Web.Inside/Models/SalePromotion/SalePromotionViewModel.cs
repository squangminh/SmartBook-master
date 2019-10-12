using Microsoft.AspNetCore.Http;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;


namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class SalePromotionViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(50, ErrorMessage ="Tối đa 50 ký tự")]
        public string Title { get; set; }
        [ProtoMember(3)]
        [Display(Name = "Ảnh")]
        [Required(ErrorMessage = "Ảnh không được để trống")]
        public string Image { get; set; }
        [ProtoMember(4)]
        public int Sort { get; set; }
        [ProtoMember(5)]
        [Display(Name = "Loại")]
        public short Type { get; set; }
        [ProtoMember(6)]
        [Display(Name = "Trang chủ")]
        public bool IsHomepage { get; set; }
        [ProtoMember(7)]
        [Display(Name = "Kích hoạt")]
        public bool IsActive { get; set; }
        [ProtoMember(8)]
        public int Count { get; set; }

        public IFormFile ImageFile { get; set; }
        [Display(Name = "Địa điểm ăn uống")]
        public string[] FoodLocation { get; set; }
    }
}
