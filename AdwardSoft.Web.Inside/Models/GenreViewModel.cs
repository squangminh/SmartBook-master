using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class GenreViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Description { get; set; }

        public bool IsCheck { get; set; }
    }
}
