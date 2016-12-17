using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEntity;

namespace nhanvien_luong.Controllers
{
    public class NgachController : Controller
    {
        //
        // GET: /Ngach/
        salaryContext db = new salaryContext();
        public ActionResult Index()
        {
            ViewBag.ngach = db.ngach.ToList();
            return View();
        }
	}
}