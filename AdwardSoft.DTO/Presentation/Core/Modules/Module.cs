using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class Module
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Title { get; set; }
        [ProtoMember(3)]
        public string Link { get; set; }
        [ProtoMember(4)]
        public string ClassName { get; set; }
        [ProtoMember(5)]
        public string ControllerName { get; set; }
        [ProtoMember(6)]
        public int ParentId { get; set; }
        [ProtoMember(7)]
        public int Sort { get; set; }
    }

    [ProtoContract]
    public class ModuleJson
    {
        [ProtoMember(1)]
        public string Json { get; set; }
      
    }
}
