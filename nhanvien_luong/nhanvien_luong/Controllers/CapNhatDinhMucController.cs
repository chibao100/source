using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEntity;

namespace nhanvien_luong.Controllers
{
    public class CapNhatDinhMucController : Controller
    {
        //
        // GET: /CapNhatDinhMuc/

        salaryContext db = new salaryContext();
        public ActionResult Index()
        {
            ViewBag.dinhmuc = db.luong.FirstOrDefault<luong>();
            return View();
        }
        [HttpPost]
        public ActionResult CapNhat()
        {
            var dinhmuc = Request.Form["dinhmuc"];
            var query = from b in db.luong
                        where b.id == 1
                        select b;
            luong a = query.FirstOrDefault<luong>();
            if (a != null)
            {
                a.dinhmuc = Int32.Parse(dinhmuc);
                db.SaveChanges();
            }
            return Redirect("/DinhMuc");
        }
	}
}