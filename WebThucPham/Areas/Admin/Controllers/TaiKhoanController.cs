using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucPham.App_Start;
using WebThucPham.Models;

namespace WebThucPham.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        mapTaiKhoan map= new mapTaiKhoan();
        // GET: Admin/TaiKhoan
        [QuyenNhanVien(Roles = "15")]
        public ActionResult DanhSach()
        {

            return View(map.DanhSachTaiKhoan());
        }
        [QuyenNhanVien(Roles = "16")]
        public ActionResult ChiTiet(string username)
        {
            return View(map.ChiTietTaiKhoan(username));
        }
        [QuyenNhanVien(Roles = "17")]
        public ActionResult LuuPhanQuyen(string username, int idChucNang)
        {
            map.PhanQuyen(username, idChucNang);
            return RedirectToAction("ChiTiet", new { username = username });
        }
    }
}