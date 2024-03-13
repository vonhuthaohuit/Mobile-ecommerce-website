using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_LTW.Models
{
    public class ViewModel
    {
        public IEnumerable<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }
        public IEnumerable<LOAISANPHAM> LOAISANPHAMs { get; set; }
    }
}