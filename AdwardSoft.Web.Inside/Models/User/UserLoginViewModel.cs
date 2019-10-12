using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class UserLoginViewModel
    {
        [ProtoMember(1)]
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Bạn cần nhập thông tin tài khoản")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserName { get; set; }
        [ProtoMember(2)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu truy cập hệ thống")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password { get; set; }
        [ProtoMember(3)]
        [Display(Name = "Ghi nhớ truy cập")]
        public bool IsRememberMe { get; set; }
    }
}
