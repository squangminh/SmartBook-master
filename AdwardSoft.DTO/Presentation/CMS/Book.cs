using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.CMS
{
    [ProtoContract]
    public class Book
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Image { get; set; }
        [ProtoMember(4)]
        public string Description { get; set; }
        [ProtoMember(5)]
        public int Status { get; set; }
        [ProtoMember(6)]
        public DateTime TimeCreate { get; set; }
        [ProtoMember(7)]
        public long CreateUserId { get; set; }
        [ProtoMember(8)]
        public int Count { get; set; }
    }
}
