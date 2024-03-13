using DoAn_LTW.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DoAn_LTW.Models
{
    public class ListCart
    {
        private static ListCart instance;

        public static ListCart Instance
        {
            get { if (instance == null) instance = new ListCart(); return ListCart.instance; }
            private set { ListCart.instance = value; }
        }

        public List<Cartxx> LoadCart(string MaKH)
        {
            List<Cartxx> cart = new List<Cartxx>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC dbo.USP_LoadCart @MaKH ", new object[] { MaKH });
            foreach (DataRow item in data.Rows)
            {
                Cartxx temp = new Cartxx(item);
                cart.Add(temp);
            }
            return cart;
        }
    }
}