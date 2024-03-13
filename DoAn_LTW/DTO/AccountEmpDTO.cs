using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DoAn_LTW.DTO
{
    public class AccountEmpDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }

        public AccountEmpDTO() { }

        public AccountEmpDTO(string id, string user, string pass, string Empid)
        {
            this.ID = id;
            this.UserName = user;
            this.Password = pass;
            this.EmployeeID = Empid;
        }

        public AccountEmpDTO(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.UserName = row["THENTAIKHOAN"].ToString();
            this.Password = row["MATKHAU"].ToString();
            this.EmployeeID = row["MANHANVIEN"].ToString();
        }
    }
}