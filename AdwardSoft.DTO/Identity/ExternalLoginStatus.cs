using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class ExternalLoginStatus
    {
        [ProtoMember(1)]
        public int Status { get; set; }
        [ProtoMember(2)]
        public string Message { get; set; }
        [ProtoMember(3)]
        public string Email { get; set; }
    }
}
