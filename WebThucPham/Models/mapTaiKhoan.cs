using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThucPham.Models
{
    public class mapTaiKhoan
    {
        WebThucPhamEntities db = new WebThucPhamEntities();
        public List<TaiKhoan> DanhSachTaiKhoan()
        {
            return db.TaiKhoans.ToList();
        }
        public TaiKhoan ChiTietTaiKhoan(string username)
        {
            return db.TaiKhoans.Find(username);
        }
        public void PhanQuyen(string username, int idchucnang)
        {
            var pq = db.PhanQuyenChucNangs.Where(x => x.Username == username && x.idChucNang == idchucnang).FirstOrDefault();
            if (pq == null)
            {
                PhanQuyenChucNang pqcn = new PhanQuyenChucNang();
                pqcn.Username = username;
                pqcn.idChucNang = idchucnang;
                db.PhanQuyenChucNangs.Add(pqcn);
                
            }
            else
            {
                db.PhanQuyenChucNangs.Remove(pq);
            }
            db.SaveChanges();
        }
        public bool CheckDangNhap(string username, string password)
        {
            var tk = db.TaiKhoans.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            if (tk == null)
            {
                return false;
            }
            return true;
        }
    }
}