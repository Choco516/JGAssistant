using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.DataAccess.Client;
using JGAssistant.Models;

namespace JGAssistant.Controllers
{
    public class ProjectionController : Controller
    {
        //
        // GET: /Projection/

        public ActionResult Projection()
        {
            return View();
        }

        public ActionResult ActualProjection()
        {
            return View("Projection");
        }

    }
}
