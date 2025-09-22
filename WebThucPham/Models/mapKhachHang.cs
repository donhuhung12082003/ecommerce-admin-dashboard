using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThucPham.Models
{
    public class mapKhachHang
    {
        WebThucPhamEntities db=new WebThucPhamEntities();
        public List<KhachHang> DanhSachKhachHang()
        {
            return db.KhachHangs.ToList();
        }
        public KhachHang ChiTietKhachHang(int id)
        {
            return db.KhachHangs.FirstOrDefault(kh => kh.ID == id);
        }
        public void ThemKhachHang(KhachHang khachhang)
        {
            db.KhachHangs.Add(khachhang);
            db.SaveChanges();
        } 
        public void SuaKhachHang(KhachHang khachhang)
        {
            var khachhangcu = db.KhachHangs.FirstOrDefault(kh => kh.ID == khachhang.ID);
            if (khachhangcu != null)
            {
                khachhangcu.TenKhachHang = khachhang.TenKhachHang;
                khachhangcu.SoDienThoai = khachhang.SoDienThoai;
                khachhangcu.DiaChi = khachhang.DiaChi;
                db.SaveChanges();
            }
        }
        public void XoaKhachHang(int id)
        {
            var khachhang = db.KhachHangs.FirstOrDefault(kh => kh.ID == id);
            if (khachhang != null)
            {
                db.KhachHangs.Remove(khachhang);
                db.SaveChanges();
            }
        }
    }
}