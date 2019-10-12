using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public  class ApplicationUser 
    {
        public ApplicationUser()
        {
            this.Claims = new HashSet<ApplicationClaim>();
            this.Roles = new HashSet<ApplicationRole>();
        }

        [ProtoMember(1)]
        public virtual long Id { get; set; }
        [ProtoMember(2)]
        public virtual string UserName { get; set; }
        [ProtoMember(3)]
        public virtual string NormalizedUserName { get; set; }
        [ProtoMember(4)]
        public virtual string Email { get; set; }
        [ProtoMember(5)]
        public string NormalizedEmail { get; set; }
        [ProtoMember(6)]
        public bool EmailConfirmed { get; set; }
        [ProtoMember(7)]
        public virtual String PasswordHash { get; set; }
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
        public string FullName { get; set; }
        [ProtoMember(17)]
        public string Avatar { get; set; }
        [ProtoMember(18)]
        public short Type { get; set; }

        public virtual ICollection<ApplicationClaim> Claims { get; set; }

        public virtual ICollection<ApplicationRole> Roles { get; set; }
    }

    [ProtoContract]
    public class UserInsert
    {
        [ProtoMember(1)]
        public virtual long Id { get; set; }
        [ProtoMember(2)]
        public virtual string UserName { get; set; }
        [ProtoMember(3)]
        public virtual string NormalizedUserName { get; set; }
        [ProtoMember(4)]
        public virtual string Email { get; set; }
        [ProtoMember(5)]
        public string NormalizedEmail { get; set; }
        [ProtoMember(6)]
        public bool EmailConfirmed { get; set; }
        [ProtoMember(7)]
        public virtual String PasswordHash { get; set; }
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
        public string FullName { get; set; }
        [ProtoMember(17)]
        public string Avatar { get; set; }
        [ProtoMember(18)]
        public short Type { get; set; }
        [ProtoMember(19)]
        public string LicensePlates { get; set; }

    }

    [ProtoContract]
    public class UserCreate
    {
        [ProtoMember(1)]
        public virtual long Id { get; set; }
        [ProtoMember(2)]
        public virtual string UserName { get; set; }
        [ProtoMember(3)]
        public virtual string NormalizedUserName { get; set; }
        [ProtoMember(4)]
        public virtual string Email { get; set; }
        [ProtoMember(5)]
        public string NormalizedEmail { get; set; }
        [ProtoMember(6)]
        public bool EmailConfirmed { get; set; }
        [ProtoMember(7)]
        public virtual String PasswordHash { get; set; }
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
        public string FullName { get; set; }
        [ProtoMember(17)]
        public string Avatar { get; set; }
        [ProtoMember(18)]
        public short Type { get; set; }
        
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    [ProtoContract]
    public class UserResetPassword
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        public string Password { get; set; }

        [ProtoMember(3)]
        public string RepeatPassword { get; set; }

        [ProtoMember(4)]
        public string Code { get; set; }
    }

    [ProtoContract]
    public class UserUpdatePassword
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        public string Username { get; set; }

        [ProtoMember(3)]
        public string Email { get; set; }

        [ProtoMember(4)]
        public string FullName { get; set; }

        [ProtoMember(5)]
        public string OldPassword { get; set; }

        [ProtoMember(6)]
        public string NewPassword { get; set; }

        [ProtoMember(7)]
        public string RepeatPassword { get; set; }
    }

    public class EmailConfirm
    {
        public long Id { get; set; }

        public string Code { get; set; }
    }
}
