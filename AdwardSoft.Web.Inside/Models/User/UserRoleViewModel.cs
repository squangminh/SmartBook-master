using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class UserRoleViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        [Required(ErrorMessage = "Trường dữ liệu không được để trống")]
        public string Name { get; set; }
        [ProtoMember(3)]
        [Required(ErrorMessage = "Trường dữ liệu không được để trống")]
        public string NormalizedName { get; set; }
        [ProtoMember(4)]
        public string ConcurrencyStamp { get; set; }
        [ProtoMember(5)]
        public bool IsDefault { get; set; }
        [ProtoMember(6)]
        public List<PermissionViewModel> ListPermissions { get; set; }
        [ProtoMember(7)]
        public List<int> Permissions { get; set; }

        public bool isChecked { get; set; }

    }

    [ProtoContract]
    public class UserRolePermissionViewModel
    {
        [ProtoMember(1)]
        public int RoleId { get; set; }
        [ProtoMember(2)]
        public int PermissionId { get; set; }
    }

    [ProtoContract]
    public class UserRoleClaimVewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public int RoleId { get; set; }
        [ProtoMember(3)]
        public string ClaimType { get; set; }
        [ProtoMember(4)]
        public string ClaimValue { get; set; }
    }

    [ProtoContract]
    public class RoleUserViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        [Required(ErrorMessage ="Người dùng chưa được chọn")]
        public Int64 UserId { get; set; }

        public List<UserInfoViewModel> Users { get; set; }

        public List<int> RolesId { get; set; }

        public List<UserRoleViewModel> Roles { get; set; }

    }

    [ProtoContract]
    public class RolesUserViewModel
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
