using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class UserPlaceViewModel
    {
        [ProtoMember(1)]
        public long UserId { get; set; }
        [ProtoMember(2)]
        public long ParentUserId { get; set; }
        [ProtoMember(3)]
        public int PlaceId { get; set; }

    }
}
