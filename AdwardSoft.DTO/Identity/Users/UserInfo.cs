using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class UserInfo
    {
        [ProtoMember(1, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        [JsonProperty("id")]
        public long Id { get; set; }
        [ProtoMember(2)]
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [ProtoMember(3)]
        [JsonProperty("email")]
        public string Email { get; set; }
        [ProtoMember(4)]
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [ProtoMember(5)]
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [ProtoMember(6)]
        [JsonProperty("permissions")]
        public string Permissions { get; set; }
        [ProtoMember(7)]
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [ProtoMember(8)]
        [JsonProperty("letterAvatar")]
        public string LetterAvatar { get; set; }
        [ProtoMember(9)]
        [JsonProperty("type")]
        public short Type { get; set; }
        [ProtoMember(10)]
        [JsonProperty("isNew")]
        public bool IsNew { get; set; }
    }
}
