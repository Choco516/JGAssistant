using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JGAssistant.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/

        public ActionResult NewBooking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewBooking(string usuario = "", string contrasena = "")
        {
            return View();
        }
    }
}
