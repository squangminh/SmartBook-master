using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Models
{
    [ProtoContract]
    public class JsonData
    {
        [ProtoMember(1)]
        public string Data { get; set; }
    }
}
