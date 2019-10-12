using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class ExternalLogin
    {
        [ProtoMember(1)]
        public string Provider { get; set; }
        [ProtoMember(1)]
        public string ReturnUrl { get; set; }
    }
}
