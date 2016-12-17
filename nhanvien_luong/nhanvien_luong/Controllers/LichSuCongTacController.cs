using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nhanvien_luong.DTO;
using MyEntity;

namespace nhanvien_luong.Controllers
{
    public class LichSuCongTacController : Controller
    {
        //
        // GET: /LichSuCongTac/

        salaryContext db = new salaryContext();
        public ActionResult Index(string id)
        {
            
            try
            {
                int id_nhanvien = Int32.Parse(id);
                //
                var query = from b in db.nhanvien
                            where b.id == id_nhanvien
                            select b;
                nhanvien a = query.FirstOrDefault<nhanvien>();
                ViewBag.ma_nhanvien = a.ma;
                ViewBag.ten_nhanvien = a.ten;
                ViewBag.ngayvaolam_nhanvien = a.ngay_vao_lam;
                //
                var query2 = (from b in db.nhanvien_ngach
                              join c in db.ngach on b.id_ngach equals c.id
                              where b.id_nhanvien == id_nhanvien
                              select new lichsungach()
                              {
                                  ten = c.ngach1,
                                  bac = b.bac,
                                  ngay = (DateTime)b.ngay
                              }).OrderBy(x => x.ngay);
                ViewBag.ngachs = query2;
                //

                var query3 = (from b in db.nhanvien_chucvu
                             join c in db.chucvu on b.id_chucvu equals c.id
                             where b.id_nhanvien == id_nhanvien
                             select new lichsuchucvu
                             {
                                 ten = c.chuc_vu,
                                 ngay = (DateTime)b.ngay
                             }).OrderBy(x => x.ngay);
                ViewBag.chucvus = query3;
                 
                return View();

            }catch(Exception ex){
                return Redirect("/Home");
            }
            
            
            
        }
	}
}