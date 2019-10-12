using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Identity
{
    [ProtoContract]
    public class LoginProvider
    {
        //
        // Summary:
        //     The name of the authentication scheme.
        [ProtoMember(1)]
        public string Name { get; set; }
        //
        // Summary:
        //     The display name for the scheme. Null is valid and used for non user facing schemes.
        [ProtoMember(2)]
        public string DisplayName { get; set; }
        //
        // Summary:
        //     The Microsoft.AspNetCore.Authentication.IAuthenticationHandler type that handles
        //     this scheme.
        [ProtoMember(3)]
        public Type HandlerType { get; set; }
    }
}
