using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WedsiteBanHang.Context;

namespace WedsiteBanHang.Models
{
    public class ProductSearch
    {
        WedBanHangEntities1 objWedBanHangEntities = new WedBanHangEntities1();
        public List<Product> SearchByKey(string key)
        {
            return objWedBanHangEntities.Products.SqlQuery("Select * From Product Where Name like '%" + key + "%'").ToList();
        }
    }
}
