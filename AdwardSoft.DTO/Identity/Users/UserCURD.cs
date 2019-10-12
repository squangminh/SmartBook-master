//using ProtoBuf;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AdwardSoft.DTO.Identity
//{
//    public class UserCreate
//    {
//        public string UserName { get; set; }
//        public string NormalizedUserName { get; set; }
//        public string Email { get; set; }
//        public string NormalizedEmail { get; set; }
//        public bool? EmailConfirmed { get; set; }
//        public string PasswordHash { get; set; }
//        public string SecurityStamp { get; set; }
//        public string ConcurrencyStamp { get; set; }
//        public string PhoneNumber { get; set; }
//        public bool? PhoneNumberConfirmed { get; set; }
//        public bool? TwoFactorEnabled { get; set; }
//        public DateTime? LockoutEndDateUtc { get; set; }
//        public bool? LockoutEnabled { get; set; }
//        public int? AccessFailedCount { get; set; }
//        public string FullName { get; set; }
//        public string Avatar { get; set; }
//        public short Type { get; set; }
//    }

//    public class UserInfor
//    {
//        public long Id { get; set; }
//        public string UserName { get; set; }
//        public string NormalizedUserName { get; set; }
//        public string Email { get; set; }
//        public string NormalizedEmail { get; set; }
//        public bool? EmailConfirmed { get; set; }
//        public string PasswordHash { get; set; }
//        public string SecurityStamp { get; set; }
//        public string ConcurrencyStamp { get; set; }
//        public string PhoneNumber { get; set; }
//        public bool? PhoneNumberConfirmed { get; set; }
//        public bool? TwoFactorEnabled { get; set; }
//        public DateTime? LockoutEndDateUtc { get; set; }
//        public bool? LockoutEnabled { get; set; }
//        public int? AccessFailedCount { get; set; }
//        public string FullName { get; set; }
//        public string Avatar { get; set; }
//        public short Type { get; set; }
//    }

//    public class UserDelete
//    {
//        public long Id { get; set; }        
//    }

//    public class UserUpdatePassword
//    {
//        public long Id { get; set; }

//        public string PasswordHash { get; set; }
//    }

//    [ProtoContract]
//    public class UserChangePassword
//    {
//        [ProtoMember(1)]
//        public long Id { get; set; }      
//        [ProtoMember(8)]
//        public string OldPassword { get; set; }
//        [ProtoMember(9)]
//        public string NewPassword { get; set; }   
//    }
//}
