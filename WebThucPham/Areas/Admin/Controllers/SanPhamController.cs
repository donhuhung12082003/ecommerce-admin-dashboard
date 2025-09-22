using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucPham.App_Start;
using WebThucPham.Models;

namespace WebThucPham.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        mapSanPham mapsanpham = new mapSanPham();
        // GET: Admin/HomeAdmin
        #region Danh sách sản phẩm
        [QuyenNhanVien(Roles = "1")]
        public ActionResult DanhSach()
        {
            if (TempData["ketquatimkiem"] != null)
            {
                var ketquatimkiem = TempData["ketquatimkiem"] as List<SanPham>;
                return View(ketquatimkiem);
            }
            //if (TempData["ketquatimkiem1"] != null)
            //{
            //    var ketquatimkiem = TempData["ketquatimkiem1"] as List<sp_DanhSachSanPham_Result>;
            //    return View(ketquatimkiem);
            //}
            return View(mapsanpham.DanhSachSanPham());
        }

        #endregion

        #region Tìm kiếm sản phẩm
        [QuyenNhanVien(Roles = "1")]
        [HttpGet]
        public ActionResult TimKiem(int? namsanxuat, double? dongiatu, double? dongiaden, string tensp)
        {
            TempData["namsanxuat"] = namsanxuat;
            TempData["dongiatu"] = dongiatu;
            TempData["dongiaden"] = dongiaden;
            TempData["tensp"] = tensp;

            TempData["ketquatimkiem"] = mapsanpham.DanhSachSanPham().Where(sp =>
                (sp.NamSanXuat == namsanxuat || namsanxuat == null)
                && (sp.DonGia >= dongiatu || dongiatu == null)
                && (sp.DonGia <= dongiaden || dongiaden == null)
                && (sp.TenSanPham.ToLower().Contains(tensp??"".ToLower()) || string.IsNullOrEmpty(tensp)==true)
            ).ToList();
           // TempData["ketquatimkiem1"] = new WebThucPhamEntities().sp_DanhSachSanPham(namsanxuat, dongiatu, dongiaden, tensp).ToList();

            return RedirectToAction("DanhSach");
        }
        #endregion

        #region Thêm sản phẩm
        [QuyenNhanVien(Roles = "2")]
        [HttpGet]
        public ActionResult Them()
        {
            SanPham sp= new SanPham();
           sp.NamSanXuat = DateTime.Now.Year;
            sp.DonGia = 0;
            sp.ThoiGianTao = DateTime.Now;
            return View(sp);
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "2")]
        public ActionResult Them(SanPham sp, HttpPostedFileBase file)
        {
            if(string.IsNullOrEmpty(sp.TenSanPham))
            {
                ModelState.AddModelError("TenSanPham", "Tên sản phẩm không được để trống");
                return View(sp);
            }
            if(sp.DonGia <= 0 || sp.DonGia==null)
            {
                ModelState.AddModelError("DonGia", "Đơn giá phải lớn hơn hoặc bằng 0");
                return View(sp);
            }
            if(sp.NamSanXuat < 1900 || sp.NamSanXuat > DateTime.Now.Year || sp.NamSanXuat==null)
            {
                ModelState.AddModelError("NamSanXuat", "Năm sản xuất không hợp lệ");
                return View(sp);
            }
            if(file!=null && file.ContentLength > 0)
            {
                string path = "/Data";
                string filename= file.FileName;
                string rootpath = Server.MapPath(path);
                if (System.IO.Directory.Exists(rootpath) == false)
                {
                    System.IO.Directory.CreateDirectory(rootpath);
                }
                string filepath = rootpath + "/" + filename;
                file.SaveAs(filepath);
                sp.HinhAnh = path + "/" + filename; ;
            }
            mapsanpham.ThemSanPham(sp);
            return RedirectToAction("DanhSach");
        }
        #endregion

        [HttpGet]
        [QuyenNhanVien(Roles = "3")]
        public ActionResult Sua(int id)
        {
            var sp = mapsanpham.ChiTietSanPham(id);
            if(string.IsNullOrEmpty(sp.SoDienThoaiNCC)==false) sp.SoDienThoaiNCC = sp.SoDienThoaiNCC.Trim();
            return View(sp);
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "3")]
        public ActionResult Sua(SanPham sp, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(sp.TenSanPham))
            {
                ModelState.AddModelError("TenSanPham", "Tên sản phẩm không được để trống");
                return View(sp);
            }
            if (sp.DonGia <= 0 || sp.DonGia == null)
            {
                ModelState.AddModelError("DonGia", "Đơn giá phải lớn hơn hoặc bằng 0");
                return View(sp);
            }
            if (sp.NamSanXuat < 1900 || sp.NamSanXuat > DateTime.Now.Year || sp.NamSanXuat == null)
            {
                ModelState.AddModelError("NamSanXuat", "Năm sản xuất không hợp lệ");
                return View(sp);
            }
            if (file != null && file.ContentLength > 0)
            {
                string path = "/Data";
                string filename = file.FileName;
                string rootpath = Server.MapPath(path);
                if (System.IO.Directory.Exists(rootpath) == false)
                {
                    System.IO.Directory.CreateDirectory(rootpath);
                }
                string filepath = rootpath + "/" + filename;
                file.SaveAs(filepath);
                sp.HinhAnh = path + "/" + filename; ;
            }
            mapsanpham.SuaSanPham(sp);
            return RedirectToAction("DanhSach");
        }
        [HttpPost]
        [QuyenNhanVien(Roles = "4")]
        public JsonResult XoaAnh(int id)
        {
            WebThucPhamEntities db= new WebThucPhamEntities();
            var sp = db.SanPhams.Find(id);
            if(string.IsNullOrEmpty(sp.HinhAnh)==false) System.IO.File.Delete(Server.MapPath(sp.HinhAnh));
            sp.HinhAnh=null;
            db.SaveChanges();
            return Json(new {ketqua="ok"});
        }
        [QuyenNhanVien(Roles = "4")]
        public ActionResult Xoa(int id)
        {
            mapsanpham.XoaSanPham(id);
            return RedirectToAction("DanhSach");
        }
        public ActionResult ThongKeSanPhamSanXuatTheoNam()
        {
            return View();  
        }
    }
}