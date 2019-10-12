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
    public class FoodViewModel
    {
        public FoodViewModel()
        {
            Categories = new List<FoodFoodCategoryViewModel>();

            Refections = new List<FoodFoodRefectionViewModel>();
        }

        [ProtoMember(1)]
        public int Id { get; set; }
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên không được để trống")]
        [ProtoMember(2)]
        public string Name { get; set; }
        [Display(Name = "Giá cũ")]
        [Required(ErrorMessage = "Giá cũ không được để trống")]
        [ProtoMember(3)]
        public decimal PriceOld { get; set; }
        [Display(Name = "Giá hiện tại")]
        [Required(ErrorMessage = "Price hiện tại không được để trống")]
        [ProtoMember(4)]
        public decimal Price { get; set; }
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [ProtoMember(5)]
        public string Image { get; set; }
        [Display(Name = "Địa điểm")]
        [ProtoMember(6)]
        public int FoodLocationId { get; set; }
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [ProtoMember(7)]
        public string Description { get; set; }
        [ProtoMember(8)]
        public string FoodLocationName { get; set; }
        [ProtoMember(9)]
        public int Count { get; set; }
        [ProtoMember(10)]
        [Display(Name = "Loại chính")]
        public int CategoryId { get; set; }

        public IFormFile ImageFile { get; set; }

        public List<FoodFoodCategoryViewModel> Categories { get; set; }

        public List<FoodFoodRefectionViewModel> Refections { get; set; }
    }

    [ProtoContract]
    public class FoodFoodCategoryViewModel
    {
        [ProtoMember(1)]
        public int FoodId { get; set; }
        [ProtoMember(2)]
        public int CategoryId { get; set; }
        [ProtoMember(3)]
        public string Name { get; set; }
        [ProtoMember(4)]
        public bool Active { get; set; }
        [ProtoMember(5)]
        public bool IsDefault { get; set; }
    }

    [ProtoContract]
    public class FoodFoodRefectionViewModel
    {
        [ProtoMember(1)]
        public int FoodId { get; set; }
        [ProtoMember(2)]
        public int RefectionId { get; set; }
        [ProtoMember(3)]
        public string Name { get; set; }
        [ProtoMember(4)]
        public bool Active { get; set; }
    }

    [ProtoContract]
    public class FoodSearchViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public decimal PriceOld { get; set; }
        [ProtoMember(4)]
        public decimal Price { get; set; }
        [ProtoMember(5)]
        public string Image { get; set; }
        [ProtoMember(6)]
        public int FoodLocationId { get; set; }
        [ProtoMember(7)]
        public string Description { get; set; }
        [ProtoMember(8)]
        public string FoodLocationName { get; set; }
        [ProtoMember(9)]
        public double Latitude { get; set; }
        [ProtoMember(10)]
        public double Longitude { get; set; }
        [ProtoMember(11)]
        public string Category { get; set; }
        [ProtoMember(12)]
        public string CategoryLocation { get; set; }
    }
}
