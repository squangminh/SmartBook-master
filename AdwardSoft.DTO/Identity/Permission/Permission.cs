using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class Permission
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Description { get; set; }
        [ProtoMember(3)]
        public string ControllerName { get; set; }
        [ProtoMember(4)]
        public string ActionName { get; set; }
    }
}
