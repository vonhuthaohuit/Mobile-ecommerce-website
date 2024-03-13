using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DoAn_LTW.Models
{
    public class Cartxx
    {
        public string MAGIOHANG { get; set; }
        public string TENSANPHAM { get; set; }
        public string GIA { get; set; }
        public string MAUSAC { get; set; }
        public int SOLUONG { get; set; }
        public string MOTASANPHAM { get; set; }
        public string HINHANH { get; set; }
        public decimal THANHTIEN { get; set; }
        public Cartxx() { }
        public Cartxx(string id, string ten, string gia, string mau, int sl, string mota, string anh, decimal tt)
        {
            this.MAGIOHANG = id;
            this.TENSANPHAM = ten;
            this.GIA = gia;
            this.MAUSAC = mau;
            this.SOLUONG = sl;
            this.MOTASANPHAM = mota;
            this.HINHANH = anh;
            this.THANHTIEN = tt;
        }

        public Cartxx(DataRow row)
        {
            this.MAGIOHANG = row["MAGIOHANG"].ToString();
            this.TENSANPHAM = row["TENSANPHAM"].ToString();
            this.GIA = row["GIA"].ToString();
            this.MAUSAC = row["MAUSAC"].ToString();
            this.SOLUONG = (int)row["SOLUONG"];
            this.MOTASANPHAM = row["MOTASANPHAM"].ToString();
            this.HINHANH = row["HINHANH"].ToString();
            this.THANHTIEN = decimal.Parse(row["GIA"].ToString()) * Convert.ToInt32(row["SOLUONG"]);
        }
    }
}