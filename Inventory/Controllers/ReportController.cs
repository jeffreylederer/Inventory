using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportViewer()
        {
            ViewData["reportUrl"] = "../Report/View/local/ClosingInventory/";

            return View();
        }
    }
}