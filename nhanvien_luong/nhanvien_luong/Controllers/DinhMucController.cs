using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEntity;

namespace nhanvien_luong.Controllers
{
    public class DinhMucController : Controller
    {
        //
        // GET: /DinhMuc/
        salaryContext db = new salaryContext();
        public ActionResult Index()
        {
            ViewBag.dinhmuc = db.luong.FirstOrDefault<luong>();
            return View();
        }
	}
}