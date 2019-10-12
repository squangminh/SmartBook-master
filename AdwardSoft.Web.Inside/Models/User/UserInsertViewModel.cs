using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class UserInsertViewModel
    {
        [ProtoMember(1)]
        public virtual long Id { get; set; }
        [ProtoMember(2)]
        public virtual string UserName { get; set; }
        [ProtoMember(3)]
        public virtual string NormalizedUserName { get; set; }
        [ProtoMember(4)]
        [Required(ErrorMessage ="Không được để trống")]
        [EmailAddress(ErrorMessage ="Không đúng định dạng email")]
        [Remote("IsAlreadySigned", "User", ErrorMessage = "Tài khoản đã tồn tại.")]
        public virtual string Email { get; set; }
        [ProtoMember(5)]
        public string NormalizedEmail { get; set; }
        [ProtoMember(6)]
        public bool EmailConfirmed { get; set; }
        [ProtoMember(7)]
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Tối thiểu 8 ký tự")]
        public virtual string PasswordHash { get; set; }
        [ProtoMember(8)]
        public virtual string SecurityStamp { get; set; }
        [ProtoMember(9)]
        public string ConcurrencyStamp { get; set; }
        [ProtoMember(10)]
        public virtual string PhoneNumber { get; set; }
        [ProtoMember(11)]
        public bool PhoneNumberConfirmed { get; set; }
        [ProtoMember(12)]
        public bool TwoFactorEnabled { get; set; }
        [ProtoMember(13)]
        public DateTime LockoutEndDateUtc { get; set; }
        [ProtoMember(14)]
        public bool LockoutEnabled { get; set; }
        [ProtoMember(15)]
        public int AccessFailedCount { get; set; }
        [ProtoMember(16)]
        [Required(ErrorMessage = "Không được để trống ")]
        public string FullName { get; set; }
        [ProtoMember(17)]
        public string Avatar { get; set; }
        [ProtoMember(18)]
        public short Type { get; set; } = 1;
        [ProtoMember(19)]
        public string LicensePlates { get; set; }

        [Required(ErrorMessage = "Không được để trống ")]
        [Compare("PasswordHash", ErrorMessage = "Nhập lại mật khẩu không đúng")]
        public string PasswordConfirmed { get; set; }
        public IFormFile FileImage { get; set; }  
    }
}
