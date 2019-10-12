namespace AdwardSoft.Web.Inside.Models
{
    using System.ComponentModel;
    public class LoginViewModel
    {
        [DisplayName("Tên tài khoản")]
        public string Username { get; set; }

       
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
    }
}
