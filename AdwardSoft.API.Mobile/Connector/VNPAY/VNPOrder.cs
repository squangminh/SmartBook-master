using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdwardSoft.API.Mobile.Connector
{
    public class VNPOrder
    {
        /// <summary>
        /// Merchant OrderId
        /// </summary>
        public decimal OrderId { get; set; }
        /// <summary>
        /// Payment amount
        /// </summary>
        public decimal Amount { get; set; }
        public string OrderDescription { get; set; }

        public string BankCode { get; set; }

        /// <summary>
        /// Order Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Creaed date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// VNPAY Transaction Id
        /// </summary>
        public decimal vnp_TransactionNo { get; set; }
        public string vpn_Message { get; set; }
        public string vpn_TxnResponseCode { get; set; }
    }
}
