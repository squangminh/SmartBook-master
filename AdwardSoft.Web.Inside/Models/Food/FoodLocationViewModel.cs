using Microsoft.AspNetCore.Http;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;


namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class FoodLocationViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        [Display(Name = "Ảnh")]
        [Required(ErrorMessage = "Ảnh không được để trống")]
        public string Image { get; set; }
        [ProtoMember(3)]
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
        [ProtoMember(4)]
        [StringLength(50,ErrorMessage ="Tối đa 50 ký tự")]
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string Title { get; set; }
        [ProtoMember(5)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ đề không được để trống")]
        public string Address { get; set; }
        [ProtoMember(6)]
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Tel { get; set; }
        [ProtoMember(7)]
        [Display(Name = "Ghi chú")]
        [Required(ErrorMessage = "Ghi chú không được để trống")]
        public string Note { get; set; }
        [ProtoMember(8)]
        [Display(Name = "Kích hoạt")]
        public string IsActive { get; set; }
        [ProtoMember(9)]
        [Display(Name = "Kinh độ")]
        [Required(ErrorMessage = "Kinh độ không được để trống")]
        public double Latitude { get; set; }
        [ProtoMember(10)]
        [Display(Name = "Vĩ độ")]
        [Required(ErrorMessage = "Vĩ độ không được để trống")]
        public double Longitude { get; set; }
        [ProtoMember(11)]
        public int Count { get; set; }
        public IFormFile ImageFile { get; set; }

        public bool isChecked { get; set; }

        [ProtoMember(12)]
        [Display(Name = "Thứ hai từ")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Monday1 { get; set; }
        [ProtoMember(13)]
        [Display(Name = "đến")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Monday2 { get; set; }
        [ProtoMember(14)]
        [Display(Name = "Thứ ba từ")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Tuesday1 { get; set; }
        [ProtoMember(15)]
        [Display(Name = "đến")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Tuesday2 { get; set; }
        [ProtoMember(16)]
        [Display(Name = "Thứ tư từ")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Webnesday1 { get; set; }
        [ProtoMember(17)]
        [Display(Name = "đến")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Webnesday2 { get; set; }
        [ProtoMember(18)]
        [Display(Name = "Thứ năm từ")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Thursday1 { get; set; }
        [ProtoMember(19)]
        [Display(Name = "đến")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Thursday2 { get; set; }
        [ProtoMember(20)]
        [Display(Name = "Thứ sáu từ")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Friday1 { get; set; }
        [ProtoMember(21)]
        [Display(Name = "đến")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Friday2 { get; set; }
        [ProtoMember(22)]
        [Display(Name = "Thứ bảy từ")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Saturday1 { get; set; }
        [ProtoMember(23)]
        [Display(Name = "đến")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Saturday2 { get; set; }
        [ProtoMember(24)]
        [Display(Name = "Chủ nhật từ")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Sunday1 { get; set; }
        [ProtoMember(25)]
        [Display(Name = "đến")]
        [Required(ErrorMessage = "Thời gian không được để trống")]
        public string Sunday2 { get; set; }
    }

    [ProtoContract]
    public class FoodLocationSearchViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]    
        public string Image { get; set; }
        [ProtoMember(3)]     
        public string Name { get; set; }
        [ProtoMember(4)]  
        public string Title { get; set; }
        [ProtoMember(5)]
        public string Address { get; set; }
        [ProtoMember(6)]
        public string Tel { get; set; }
        [ProtoMember(7)]
        public string Note { get; set; }
        [ProtoMember(8)]
        public double Latitude { get; set; }
        [ProtoMember(9)]
        public double Longitude { get; set; }
        [ProtoMember(10)]
        public string Category { get; set; }
    }
}
