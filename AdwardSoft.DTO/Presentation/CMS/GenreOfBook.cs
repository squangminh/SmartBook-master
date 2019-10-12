using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.CMS
{
    [ProtoContract]
    public class GenreOfBook
    {
        [ProtoMember(1)]
        public int GenreId { get; set; }
        [ProtoMember(2)]
        public int BookId { get; set; }
    }
}
