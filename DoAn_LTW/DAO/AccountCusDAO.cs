using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DoAn_LTW.DAO
{
    public class AccountCusDAO
    {
        private static AccountCusDAO instance;

        public static AccountCusDAO Instance
        {
            get { if (instance == null) instance = new AccountCusDAO(); return AccountCusDAO.instance; }
            private set { AccountCusDAO.instance = value; }
        }

        private AccountCusDAO() { }

        public bool Check_Login(string name, string pass)
        {
            object Check = DataProvider.Instance.ExecuteScalar("EXEC dbo.USP_CheckLogin_Cus @user , @Pass", new object[] { name, pass });
            if (Check == null)
                return false;
            else
                return true;
        }
    }
}