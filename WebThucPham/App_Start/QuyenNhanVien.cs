using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucPham.Models;
namespace WebThucPham.App_Start
{
    public class QuyenNhanVien : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.Session["user"] as TaiKhoan;
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Admin/HomeAdmin/DangNhap");
                return;
            }
            var db= new Models.WebThucPhamEntities();
            if (db.PhanQuyenChucNangs.Any(pq=>pq.Username==user.Username && pq.idChucNang.ToString() == Roles)==false)
            {
                filterContext.Result = new RedirectResult("/Admin/HomeAdmin/KhongCoQuyen");
            }
            return;
        }
    }
}