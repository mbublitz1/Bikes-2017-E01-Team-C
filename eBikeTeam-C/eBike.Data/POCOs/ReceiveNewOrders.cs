﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBike.Data.Entities;

namespace eBike.Data.POCOs
{
    public class ReceiveNewOrders
    {
        public int PurchaseOrderDetailId { get; set; }
        public int PartId { get; set; }
        public string PartDescription { get; set; }
        public int QtyReceived { get; set; }
        public int QtyReturned { get; set; }
        public int Outstanding { get; set; }
        public string Notes { get; set; }

    }
}
