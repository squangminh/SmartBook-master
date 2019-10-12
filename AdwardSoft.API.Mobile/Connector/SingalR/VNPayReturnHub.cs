using AdwardSoft.API.Mobile.Connector.VNPAY;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Mobile.Connector.SingalR
{
    public class VNPayReturnHub: Hub
    {
        public async Task VNPNotification(VNPReturn data)
        {
            await Clients.All.SendAsync("ReturnPayment", data);
        }
    }
}
