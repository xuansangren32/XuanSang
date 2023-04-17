using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WedsiteBanHang.Context;

namespace WedsiteBanHang.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}