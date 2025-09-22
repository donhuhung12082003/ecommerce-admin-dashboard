using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebThucPham.App_Start;
using WebThucPham.Models;

namespace WebThucPham.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        mapKhachHang mapkhachhang = new mapKhachHang();
        // GET: Admin/KhachHang
        [QuyenNhanVien(Roles = "5")]
        public ActionResult DanhSach()
        {
            if (TempData["ketqua"] != null)
            {
                var ketqua = TempData["ketqua"] as List<KhachHang>;
                return View(ketqua);
            }

            return View(mapkhachhang.DanhSachKhachHang());
        }
        [QuyenNhanVien(Roles = "5")]
        public ActionResult TimKiem(string thongtin)
        {
            if(string.IsNullOrEmpty(thongtin)==false ) thongtin= thongtin.Trim();
            TempData["thongtin"] = thongtin;
            thongtin=thongtin.ToLower();
            TempData["ketqua"]=mapkhachhang.DanhSachKhachHang().Where(kh => 
                kh.TenKhachHang.ToLower().Contains(thongtin)
            ||  kh.SoDienThoai.ToLower().Contains(thongtin)
            ||  kh.DiaChi.ToLower().Contains(thongtin)
            || string.IsNullOrEmpty(thongtin)
             ).ToList();
            return RedirectToAction("DanhSach");   
        }
        [QuyenNhanVien(Roles = "6")]
        public ActionResult Them()
        {
           return View();   
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "6")]
        public ActionResult Them(KhachHang khachhang)
        {
            if (string.IsNullOrEmpty(khachhang.TenKhachHang))
            {
                ModelState.AddModelError("TenKhachHang", "Tên khách hàng không được để trống");
                return View(khachhang);
            }
            if (string.IsNullOrEmpty(khachhang.SoDienThoai))
            {
                ModelState.AddModelError("SoDienThoai", "Số điện thoại không được để trống");
                return View(khachhang);
            }   
            if (string.IsNullOrEmpty(khachhang.DiaChi))
            {
                ModelState.AddModelError("DiaChi", "Địa chỉ không được để trống");
                return View(khachhang);
            }
            mapkhachhang.ThemKhachHang(khachhang);
            return RedirectToAction("DanhSach");
        }
        [QuyenNhanVien(Roles = "7")]
        public ActionResult Sua(int id)
        {
            var khachhang = mapkhachhang.ChiTietKhachHang(id);  
            return View(khachhang);
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "7")]
        public ActionResult Sua(KhachHang khachhang)
        {
            if (string.IsNullOrEmpty(khachhang.TenKhachHang))
            {
                ModelState.AddModelError("TenKhachHang", "Tên khách hàng không được để trống");
                return View(khachhang);
            }
            if (string.IsNullOrEmpty(khachhang.SoDienThoai))
            {
                ModelState.AddModelError("SoDienThoai", "Số điện thoại không được để trống");
                return View(khachhang);
            }
            if (string.IsNullOrEmpty(khachhang.DiaChi))
            {
                ModelState.AddModelError("DiaChi", "Địa chỉ không được để trống");
                return View(khachhang);
            }
            mapkhachhang.SuaKhachHang(khachhang);
            return RedirectToAction("DanhSach");
        }
        [QuyenNhanVien(Roles = "8")]
        public ActionResult Xoa(int id)
        {
             mapkhachhang.XoaKhachHang(id);
            return RedirectToAction("DanhSach");
        }   
    }
}