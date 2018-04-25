using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiSchool.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			ViewBag.Title = "Home Page";

			return View();
		}

		public ActionResult BookX() {
			ViewBag.Title = "Home Page - book-x";

			return View("~/Views/Home/BooksX.cshtml");
		}
	}
}
