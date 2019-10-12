using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class SalePromotionFoodLocationViewModel
    {
        [ProtoMember(1)]
        public int SalePromotionId { get; set; }
        [ProtoMember(2)]
        public int FoodLocationId { get; set; }
    }
}
