using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class UserDriverSearchViewModel
    {
        [ProtoMember(1)]
        public long Id { get; set; }
        [ProtoMember(2)]
        public string UserName { get; set; }
        [ProtoMember(3)]
        public string PhoneNumber { get; set; }
        [ProtoMember(4)]
        public string Avatar { get; set; }
        [ProtoMember(5)]
        public string LicensePlates { get; set; }
        [ProtoMember(6)]
        public bool isActive { get; set; }
        [ProtoMember(7)]
        public double Latitude { get; set; }
        [ProtoMember(8)]
        public double Longitude { get; set; }
        [ProtoMember(9)]
        public long ConnectUser { get; set; }
        [ProtoMember(10)]
        public int DriverActivity { get; set; }
        [ProtoMember(11)]
        public string FullName { get; set; }
        [ProtoMember(12)]
        public short Type { get; set; }
    }

    [ProtoContract]
    public class UserDriverViewModel
    {
        [ProtoMember(1)]
        public long UserId { get; set; }
        [ProtoMember(2)]
        public string LicensePlates { get; set; }
        [ProtoMember(3)]
        public bool IsActive { get; set; }
    }
}
