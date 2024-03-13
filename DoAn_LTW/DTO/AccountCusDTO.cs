using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DoAn_LTW.DTO
{
    public class AccountCusDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CustomerID { get; set; }

        public AccountCusDTO() { }

        public AccountCusDTO(string id, string user, string pass, string Cusid)
        {
            this.ID = id;
            this.UserName = user;
            this.Password = pass;
            this.CustomerID = Cusid;
        }

        public AccountCusDTO(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.UserName = row["THENTAIKHOAN"].ToString();
            this.Password = row["MATKHAU"].ToString();
            this.CustomerID = row["MAKHACHHANG"].ToString();
        }
    }
}