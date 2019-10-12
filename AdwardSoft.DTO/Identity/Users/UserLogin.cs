using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class UserLogin
    {
        [ProtoMember(1)]
        public string UserName { get; set; }
        [ProtoMember(2)]
        public string Password { get; set; }
        [ProtoMember(3)]
        public bool IsRememberMe { get; set; }
    }
}
