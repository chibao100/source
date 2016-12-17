using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEntity;

namespace nhanvien_luong.Controllers
{
    public class ChucVuController : Controller
    {
        //
        // GET: /ChucVu/
        salaryContext db = new salaryContext();
        public ActionResult Index()
        {
            ViewBag.chucvu = db.chucvu.ToList();
            return View();
        }
	}
}