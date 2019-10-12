using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class ApplicationRole
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string NormalizedName { get; set; }
        [ProtoMember(4)]
        public string ConcurrencyStamp { get; set; }
        [ProtoMember(5)]
        public bool IsDefault { get; set; }
    }
    [ProtoContract]
    public class ApplicationRolePermission
    {
        [ProtoMember(1)]
        public int RoleId { get; set; }
        [ProtoMember(2)]
        public int PermissionId { get; set; }
    }
}
