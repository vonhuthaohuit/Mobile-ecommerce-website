using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_LTW.Models
{
    public class View_CTSP
    {
        public CHITIETSANPHAM DetailProduct { get; set; }
        public List<CHITIETSANPHAM> RelatedProducts { get; set; }
        public List<DANHGIA> Reviews { get; set; }
    }
}