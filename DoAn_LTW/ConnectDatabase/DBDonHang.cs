using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DoAn_LTW.Models;

namespace DoAn_LTW.ConnectDatabase
{
    public class DBDonHang
    {
        QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities db = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities();
        public List<PROC_GET_DATA_CHI_TIET_DON_MUA_Result> GetDonMua(string maKhachHang, string maDonMua)
        {
            List<PROC_GET_DATA_CHI_TIET_DON_MUA_Result> proc = new List<PROC_GET_DATA_CHI_TIET_DON_MUA_Result>();
            using (var context = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities())
            {
                // Gọi stored procedure và lấy kết quả
                var parameters = new object[]
                {
                    new SqlParameter("@MAKHACHHANG", maKhachHang),
                    new SqlParameter("@MADONMUA", maDonMua)
                };
                proc = context.Database.SqlQuery<PROC_GET_DATA_CHI_TIET_DON_MUA_Result>("EXEC PROC_GET_DATA_CHI_TIET_DON_MUA @MAKHACHHANG, @MADONMUA", parameters).ToList();

            }
            return proc;
        }
        public List<PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN_Result> GetDonMuaDaThanhToan(string maKhachHang, string maDonHang)
        {
            List<PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN_Result> proc = new List<PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN_Result>();
            using (var context = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities())
            {
                // Gọi stored procedure và lấy kết quả
                var parameters = new object[]
                {
                new SqlParameter("@MAKHACHHANG", maKhachHang),
                new SqlParameter("@MADONMUA", maDonHang)
                };

                proc = context.Database.SqlQuery<PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN_Result>("EXEC PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN @MAKHACHHANG, @MADONMUA", parameters).ToList();

            }
            return proc;
        }
        public List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result> GetDonMuaChuaThanhToan(string maKhachHang, string maDonHang)
        {
            List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result> proc = new List<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result>();
            using (var context = new QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities())
            {
                // Gọi stored procedure và lấy kết quả
                var parameters = new object[]
                {
                new SqlParameter("@MAKHACHHANG", maKhachHang),
                new SqlParameter("@MADONMUA", maDonHang)
                };

                proc = context.Database.SqlQuery<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result>("EXEC PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN @MAKHACHHANG, @MADONMUA", parameters).ToList();

            }
            return proc;
        }

    }
}