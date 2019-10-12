using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class UserViewModel
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        [Required]
        [DisplayName("Tên tài khoản")]
        public string Username { get; set; }

        [ProtoMember(3)]
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [ProtoMember(4)]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        [ProtoMember(5)]
        [Required]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [ProtoMember(6)]
        public string Phone { get; set; }

        [ProtoMember(7)]
        public int Status { get; set; }

        [ProtoMember(8)]
        [Required]
        [DisplayName("Mật khẩu hiện tại")]
        [Remote("IsCorrectPassword", "User", ErrorMessage = "Sai mật khẩu.")]
        public string OldPassword { get; set; }

        [ProtoMember(9)]
        [Required]
        [DisplayName("Mật khẩu mới")]
        public string NewPassword { get; set; }

        [ProtoMember(10)]
        [Required]
        [DisplayName("Xác nhận mật khẩu mới")]
        public string RepeatPassword { get; set; }

        [ProtoMember(11)]
        [Required]
        [DisplayName("Xác nhận mật khẩu mới")]
        public string Avatar { get; set; }
    }

    [ProtoContract]
    public class UserResetPasswordViewModel
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        [Required]
        [DisplayName("Mật khẩu mới")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Tối thiểu 8 ký tự")]
        public string Password { get; set; }

        [ProtoMember(3)]
        [Required]
        [DisplayName("Xác nhận mật khẩu mới")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Tối thiểu 8 ký tự")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không đúng")]
        public string RepeatPassword { get; set; }

        [ProtoMember(4)]
        public string Code { get; set; }
    }

    public class EmailConfirmViewModel
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        public string Code { get; set; }
    }

    [ProtoContract]
    public class UserUpdatePasswordViewModel
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        [Required]
        [DisplayName("Tên tài khoản")]
        public string Username { get; set; }

        [ProtoMember(3)]
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [ProtoMember(4)]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        [ProtoMember(5)]
        [Required]
        [DisplayName("Mật khẩu hiện tại")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Tối thiểu 8 ký tự")]
        [Remote("IsCorrectPassword", "User", ErrorMessage = "Sai mật khẩu.")]
        public string OldPassword { get; set; }

        [ProtoMember(6)]
        [Required]
        [DisplayName("Mật khẩu mới")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Tối thiểu 8 ký tự")]
        public string NewPassword { get; set; }

        [ProtoMember(7)]
        [Required]
        [DisplayName("Xác nhận mật khẩu mới")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Tối thiểu 8 ký tự")]
        public string RepeatPassword { get; set; }
    }
}
