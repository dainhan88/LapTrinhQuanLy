using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhQuanLy.Areas.Admins.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admins/HomeAdmin
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}