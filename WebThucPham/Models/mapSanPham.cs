using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebThucPham.Models
{
    public class mapSanPham
    {
        WebThucPhamEntities db= new WebThucPhamEntities();
        public List<SanPham> DanhSachSanPham()
        {
            return db.SanPhams.ToList();
        }
        public SanPham ChiTietSanPham(int id)
        {
            return db.SanPhams.FirstOrDefault(sp => sp.ID == id);
        }
        public void ThemSanPham(SanPham sp)
        {
            if (string.IsNullOrEmpty(sp.SoDienThoaiNCC) == false) sp.SoDienThoaiNCC = sp.SoDienThoaiNCC.Trim();
            db.SanPhams.Add(sp);
            db.SaveChanges();
        } 
        public void SuaSanPham(SanPham sp)
        {
            var sanpham = db.SanPhams.FirstOrDefault(s => s.ID == sp.ID);
            if (sanpham != null)
            {
                sanpham.TenSanPham = sp.TenSanPham;
                sanpham.NamSanXuat = sp.NamSanXuat;
                sanpham.DonGia = sp.DonGia;
                sanpham.MoTa = sp.MoTa;
                sanpham.ThoiGianTao = sp.ThoiGianTao;
                sanpham.MauSac = sp.MauSac;
                sanpham.PhanLoai = sp.PhanLoai;
                sanpham.HetHang = sp.HetHang;
                sanpham.EmailNCC = sp.EmailNCC;
                sanpham.SoDienThoaiNCC = sp.SoDienThoaiNCC;
                if(string.IsNullOrEmpty(sanpham.HinhAnh)==true && string.IsNullOrEmpty(sp.HinhAnh) == false)
                {
                    sanpham.HinhAnh = sp.HinhAnh;
                }
                else if(string.IsNullOrEmpty(sanpham.HinhAnh) == false && string.IsNullOrEmpty(sp.HinhAnh) == false && sanpham.HinhAnh != sp.HinhAnh)
                {
                    File.Delete(HttpContext.Current.Server.MapPath(sanpham.HinhAnh));
                    sanpham.HinhAnh = sp.HinhAnh;
                }
                db.SaveChanges();
            }
        }
        public void XoaSanPham(int id)
        {
            var sp = db.SanPhams.FirstOrDefault(s => s.ID == id);
            if (sp != null)
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
            }
        }
    }
}