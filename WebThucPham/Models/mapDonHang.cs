using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThucPham.Models
{
    public class mapDonHang
    {
        WebThucPhamEntities db = new WebThucPhamEntities();
        public List<spDanhSachDonHang_Result> DanhSachDonHang(DateTime? ngay, int? idkhachhang, string nguoinhan)
        {
            return db.spDanhSachDonHang(ngay, idkhachhang, nguoinhan).ToList();
        }
        public DonHang ChiTietDonHang(int id)
        {
            return db.DonHangs.Find(id);
        }
        public void Sua(DonHang dh)
        {
            var existingOrder = db.DonHangs.Find(dh.ID);
            if (existingOrder != null)
            {
                existingOrder.idKhachHang = dh.idKhachHang;
                existingOrder.NguoiNhan = dh.NguoiNhan;
                existingOrder.DiaChiNhan = dh.DiaChiNhan;
                existingOrder.SoDienThoai = dh.SoDienThoai;
                existingOrder.TrangThai = dh.TrangThai;
                db.SaveChanges();
            }
        }
    }
}