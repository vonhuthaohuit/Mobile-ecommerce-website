using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DoAn_LTW.DAO
{
    public class AccountEmpDAO
    {
        private static AccountEmpDAO instance;

        public static AccountEmpDAO Instance
        {
            get { if (instance == null) instance = new AccountEmpDAO(); return AccountEmpDAO.instance; }
            private set { AccountEmpDAO.instance = value; }
        }

        private AccountEmpDAO() { }

        public bool Check_Login(string name, string pass)
        {
            object Check = DataProvider.Instance.ExecuteScalar("EXEC dbo.USP_CheckLogin_Emp @user , @Pass ", new object[] { name , pass });
            if (Check == null)
                return false;
            else
                return true;
        }
    }
}