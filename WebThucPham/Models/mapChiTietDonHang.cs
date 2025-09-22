using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThucPham.Models
{
    public class mapChiTietDonHang
    {
        WebThucPhamEntities db = new WebThucPhamEntities();
        public ChiTietDonHang ChiTietDonHang(int id)
        {
            return db.ChiTietDonHangs.Find(id);
        }
        public void Them(ChiTietDonHang dh)
        {
            dh.TenSanPham=db.SanPhams.Find(dh.idSanPham)?.TenSanPham;
            db.ChiTietDonHangs.Add(dh);
            db.SaveChanges();
        }
        public void Sua(ChiTietDonHang dh)
        {
            var existingDetail = db.ChiTietDonHangs.Find(dh.ID);
            if (existingDetail != null)
            {
                existingDetail.idSanPham = dh.idSanPham;
                existingDetail.TenSanPham = db.SanPhams.Find(dh.idSanPham)?.TenSanPham;
                existingDetail.SoLuong = dh.SoLuong;
                existingDetail.DonGia = dh.DonGia;
                existingDetail.DonViTinh= dh.DonViTinh;
                existingDetail.MucThueVAT = dh.MucThueVAT;
                db.SaveChanges();
            }
        }
        public void Xoa(int id)
        {
            var detail = db.ChiTietDonHangs.Find(id);
            if (detail != null)
            {
                db.ChiTietDonHangs.Remove(detail);
                db.SaveChanges();
            }
        }
    }
}