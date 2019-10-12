using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity.Users
{
    [ProtoContract]
    public class UserRole
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
        public Int64 UserId { get; set; }
    }

    [ProtoContract]
    public class RolePermission
    {
        [ProtoMember(1)]
        public int RoleId { get; set; }
        [ProtoMember(2)]
        public int PermissionId { get; set; }
    }

    [ProtoContract]
    public class RoleUser
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public Int64 UserId { get; set; }

    }

    [ProtoContract]
    public class RolesUser
    {
        [ProtoMember(1)]
        public Int64 UserId { get; set; }
        [ProtoMember(2)]
        public List<int> Roles { get; set; }
        [ProtoMember(3)]
        public string RolesOfUser { get; set; }
        [ProtoMember(4)]
        public string FullName { get; set; }
    }

}
