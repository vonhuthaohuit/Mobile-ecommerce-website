﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn_LTW.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities : DbContext
    {
        public QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities()
            : base("name=QUAN_LY_CUA_HANG_BAN_DIEN_THOAI_LTWEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CHITIETDONMUA> CHITIETDONMUAs { get; set; }
        public virtual DbSet<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }
        public virtual DbSet<DANHGIA> DANHGIAs { get; set; }
        public virtual DbSet<DONMUA> DONMUAs { get; set; }
        public virtual DbSet<GIOHANG> GIOHANGs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<KHO> KHOes { get; set; }
        public virtual DbSet<KHO_CHITIETSANPHAM> KHO_CHITIETSANPHAM { get; set; }
        public virtual DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PERSON> People { get; set; }
        public virtual DbSet<QUANGCAO> QUANGCAOs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TAIKHOANKHACHHANG> TAIKHOANKHACHHANGs { get; set; }
        public virtual DbSet<TAIKHOANNHANVIEN> TAIKHOANNHANVIENs { get; set; }
        public virtual DbSet<THANHTOANDONMUA> THANHTOANDONMUAs { get; set; }
        public virtual DbSet<VITRICUAHANG> VITRICUAHANGs { get; set; }
    
        public virtual ObjectResult<PROC_GET_DATA_CHI_TIET_DON_MUA_Result> PROC_GET_DATA_CHI_TIET_DON_MUA(string mAKHACHHANG, string mADONMUA)
        {
            var mAKHACHHANGParameter = mAKHACHHANG != null ?
                new ObjectParameter("MAKHACHHANG", mAKHACHHANG) :
                new ObjectParameter("MAKHACHHANG", typeof(string));
    
            var mADONMUAParameter = mADONMUA != null ?
                new ObjectParameter("MADONMUA", mADONMUA) :
                new ObjectParameter("MADONMUA", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROC_GET_DATA_CHI_TIET_DON_MUA_Result>("PROC_GET_DATA_CHI_TIET_DON_MUA", mAKHACHHANGParameter, mADONMUAParameter);
        }
    
        public virtual ObjectResult<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result> PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN(string mAKHACHHANG, string mADONMUA)
        {
            var mAKHACHHANGParameter = mAKHACHHANG != null ?
                new ObjectParameter("MAKHACHHANG", mAKHACHHANG) :
                new ObjectParameter("MAKHACHHANG", typeof(string));
    
            var mADONMUAParameter = mADONMUA != null ?
                new ObjectParameter("MADONMUA", mADONMUA) :
                new ObjectParameter("MADONMUA", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN_Result>("PROC_GET_DATA_CHI_TIET_DON_MUA_CHUA_THANH_TOAN", mAKHACHHANGParameter, mADONMUAParameter);
        }
    
        public virtual ObjectResult<PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN_Result> PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN(string mAKHACHHANG, string mADONMUA)
        {
            var mAKHACHHANGParameter = mAKHACHHANG != null ?
                new ObjectParameter("MAKHACHHANG", mAKHACHHANG) :
                new ObjectParameter("MAKHACHHANG", typeof(string));
    
            var mADONMUAParameter = mADONMUA != null ?
                new ObjectParameter("MADONMUA", mADONMUA) :
                new ObjectParameter("MADONMUA", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN_Result>("PROC_GET_DATA_CHI_TIET_DON_MUA_DA_THANH_TOAN", mAKHACHHANGParameter, mADONMUAParameter);
        }
    
        public virtual ObjectResult<string> PROC_GET_DATA_DON_MUA(string mAKHACHHANG)
        {
            var mAKHACHHANGParameter = mAKHACHHANG != null ?
                new ObjectParameter("MAKHACHHANG", mAKHACHHANG) :
                new ObjectParameter("MAKHACHHANG", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("PROC_GET_DATA_DON_MUA", mAKHACHHANGParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<USP_CheckLogin_Cus_Result> USP_CheckLogin_Cus(string user, string pass)
        {
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            var passParameter = pass != null ?
                new ObjectParameter("Pass", pass) :
                new ObjectParameter("Pass", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_CheckLogin_Cus_Result>("USP_CheckLogin_Cus", userParameter, passParameter);
        }
    
        public virtual ObjectResult<USP_CheckLogin_Emp_Result> USP_CheckLogin_Emp(string user, string pass)
        {
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            var passParameter = pass != null ?
                new ObjectParameter("Pass", pass) :
                new ObjectParameter("Pass", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_CheckLogin_Emp_Result>("USP_CheckLogin_Emp", userParameter, passParameter);
        }
    
        public virtual ObjectResult<USP_LoadCart_Result> USP_LoadCart(string maKH)
        {
            var maKHParameter = maKH != null ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_LoadCart_Result>("USP_LoadCart", maKHParameter);
        }
    }
}
