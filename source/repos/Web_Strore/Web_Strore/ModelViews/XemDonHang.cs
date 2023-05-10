using System;
using System.Collections.Generic;
using Web_Strore.Models;

namespace Web_Strore.ModelViews
{
    public class XemDonHang
    {
        public Order DonHang { get; set; }
        public List<OrderDetail> ChiTietDonHang { get; set; }
    }
}
