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
    public class PermissionViewModel
    {
        [ProtoMember(1)]
        //[Remote("IsActionControllerExist", "Permission", AdditionalFields = "Controller, Action", ErrorMessage = "Action và Controller đã tồn tại")]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Description { get; set; }
        [ProtoMember(3)]
        [Remote("IsActionControllerExist", "Permission", AdditionalFields = "ActionName, Id", ErrorMessage = "Action và Controller đã tồn tại")]
        public string ControllerName { get; set; }
        [ProtoMember(4)]
        [Remote("IsActionControllerExist", "Permission", AdditionalFields = "ControllerName, Id", ErrorMessage = "Action và Controller đã tồn tại")]
        public string ActionName { get; set; }
    }
}
