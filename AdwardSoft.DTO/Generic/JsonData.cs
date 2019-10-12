using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Generic
{
    [ProtoContract]
    public class JsonData
    {
        [ProtoMember(1)]
        public string Data { get; set; }
    }
}
