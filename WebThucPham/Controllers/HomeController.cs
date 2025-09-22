using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucPham.Models;

namespace WebThucPham.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<SanPham> dssp = new List<SanPham>() { 
                new SanPham{TenSanPham="rau xanh 1",DonGia=1000},
                new SanPham{TenSanPham="rau xanh 2",DonGia=2000},
                new SanPham{TenSanPham="rau xanh 3",DonGia=3000},
                new SanPham{TenSanPham="rau xanh 4",DonGia=4000},
                new SanPham{TenSanPham="rau xanh 5",DonGia=5000},
                 new SanPham{TenSanPham="rau xanh 6",DonGia=6000},
                  new SanPham{TenSanPham="rau xanh 4",DonGia=4000},
                new SanPham{TenSanPham="rau xanh 5",DonGia=5000},
                

            };

            return View(dssp);
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult Shop_Details()
        {
            

            return View();
        }
    }
}