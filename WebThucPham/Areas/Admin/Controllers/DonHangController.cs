using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucPham.App_Start;
using WebThucPham.Models;

namespace WebThucPham.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        mapDonHang map = new mapDonHang();
        // GET: Admin/DonHang
        [QuyenNhanVien(Roles = "9")]
        public ActionResult DanhSach()
        {
            if (((int)(TempData["sl"] ?? 0) > 0) || TempData["ngay"] != null || TempData["idkhachhang"] != null || TempData["nguoinhan"] != null)
            {
                var kq= TempData["ketqua"] as List<spDanhSachDonHang_Result>;
                return View(kq);
            }
            return View(map.DanhSachDonHang(null, null, null));
        }
        [QuyenNhanVien(Roles = "9")]
        public ActionResult TimKiem(DateTime? ngay, int? idkhachhang, string nguoinhan)
        {
            TempData["ngay"] = ngay;
            TempData["idkhachhang"] = idkhachhang;
            TempData["nguoinhan"] = nguoinhan;
            TempData["ketqua"]=map.DanhSachDonHang(ngay, idkhachhang, nguoinhan);
            int sl= (TempData["ketqua"] as List<spDanhSachDonHang_Result>).Count;
            TempData["sl"] = sl;
            return RedirectToAction("DanhSach");
        }
        [QuyenNhanVien(Roles = "10")]
        public ActionResult ChiTiet(int iddonhang)
        {
            return View(map.ChiTietDonHang(iddonhang));
        }
        [QuyenNhanVien(Roles = "11")]
        public ActionResult Sua(int id)
        {
            return View(map.ChiTietDonHang(id));
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "11")]
        public ActionResult Sua(DonHang donhang)
        {
            map.Sua(donhang);
            return RedirectToAction("ChiTiet", new { iddonhang = donhang.ID});
        }
        [QuyenNhanVien(Roles = "12")]
        public ActionResult ThemSanPhamVaoChiTietDonHang(int id)
        {
            var dh = new ChiTietDonHang();
            dh.idDonHang = id;
            return View(dh);
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "12")]
        public ActionResult ThemSanPhamVaoChiTietDonHang(ChiTietDonHang dh)
        {
            if (string.IsNullOrEmpty(dh.DonViTinh))
            {
                ModelState.AddModelError("DonViTinh", "Đơn vị tính không được để trống.");
                return View(dh);
            }
            if (dh.DonGia == null || dh.DonGia <=0)
            {   
                ModelState.AddModelError("DonGia", "Đơn giá phải lớn hơn 0 và không được để trống");
                return View(dh);
            }
            if (dh.SoLuong == null || dh.SoLuong <= 0)
     
            {
                ModelState.AddModelError("SoLuong", "Số lượng phải lớn hơn 0 và không được để trống");
                return View(dh);
            }
            new mapChiTietDonHang().Them(dh);
            return RedirectToAction("ChiTiet", new { iddonhang = dh.idDonHang });
        }
        [QuyenNhanVien(Roles = "13")]
        public ActionResult SuaChiTietDonHang(int id)
        {
            var dh = new mapChiTietDonHang().ChiTietDonHang(id);
            return View(dh);
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "13")]
        public ActionResult SuaChiTietDonHang(ChiTietDonHang dh)
        {
            if (string.IsNullOrEmpty(dh.DonViTinh))
            {
                ModelState.AddModelError("DonViTinh", "Đơn vị tính không được để trống.");
                return View(dh);
            }
            if (dh.DonGia == null || dh.DonGia <= 0)
            {
                
                ModelState.AddModelError("DonGia", "Đơn giá phải lớn hơn 0 và không được để trống");
                return View(dh);
            }
            if (dh.SoLuong == null || dh.SoLuong <= 0)
      
            {
                ModelState.AddModelError("SoLuong", "Số lượng phải lớn hơn 0 và không được để trống");
                return View(dh);
            }
            new mapChiTietDonHang().Sua(dh);
            return RedirectToAction("ChiTiet", new { iddonhang = dh.idDonHang });
        }
        [QuyenNhanVien(Roles = "14")]
        public ActionResult XoaChiTietDonHang(int id)
        {
            var dh = new mapChiTietDonHang().ChiTietDonHang(id);
            new mapChiTietDonHang().Xoa(id);
            return RedirectToAction("ChiTiet", new { iddonhang = dh.idDonHang });
        }
    }
}