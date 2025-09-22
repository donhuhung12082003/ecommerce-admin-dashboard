using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucPham.Models;

namespace WebThucPham.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        mapTaiKhoan map= new mapTaiKhoan();
        // GET: Admin/Home
        public ActionResult TrangChuAdmin()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            if (map.CheckDangNhap(username, password))
            {
                var user = map.ChiTietTaiKhoan(username);
                Session["user"] = user;
                return RedirectToAction("TrangChuAdmin");
            }
            ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu";
            return View();
        }
        [HttpPost]
        public ActionResult DangNhapAjax(string username, string password)
        {
            if (map.CheckDangNhap(username, password))
            {
                var user = map.ChiTietTaiKhoan(username);
                Session["user"] = user;
                return Json(new { success = true});
            }
            return Json(new { success = false,thongbao = "Sai tài khoản hoặc mật khẩu" });
        }
        public ActionResult DangXuat()
        {
            Session["user"] = null;
            return RedirectToAction("DangNhap");
        }
        public ActionResult KhongCoQuyen()
        {
            return View();
        }
    }
}