using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class GenreOfBookViewModel
    {
        [ProtoMember(1)]
        public int GenreId { get; set; }
        [ProtoMember(2)]
        public int BookId { get; set; }
    }
}
