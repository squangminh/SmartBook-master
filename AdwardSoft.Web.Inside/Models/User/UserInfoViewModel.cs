using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class UserInfoViewModel
    {
        [ProtoMember(1)]
        public long Id { get; set; }
        [ProtoMember(2)]
        public string UserName { get; set; }
        [ProtoMember(3)]
        public string Email { get; set; }
        [ProtoMember(4)]
        public string FullName { get; set; }
        [ProtoMember(5)]
        public string Avatar { get; set; }
        [ProtoMember(6)]
        public string Permissions { get; set; }
        [ProtoMember(7)]
        public string PhoneNumber { get; set; }
        [ProtoMember(8)]
        public string LetterAvatar { get; set; }
        [ProtoMember(9)]
        public short Type { get; set; }

    }
}
