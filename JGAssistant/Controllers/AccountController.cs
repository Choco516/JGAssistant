using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.DataAccess.Client;
using JGAssistant.Models;

namespace JGAssistant.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private string nombre;
        private string contrasena;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario = "", string contrasena = "")
        {
            string conString;
            this.nombre = usuario;
            this.contrasena = contrasena;
            conString = ("Data Source=" + ("LOGISTIC" + (";User Id="
                        + (nombre.ToString() + (";Password="
                        + (contrasena.ToString() + ";Connection Lifetime=0;Connection Timeout=120;"))))));

            baseDatos db = new baseDatos(conString);
            try
            {
                db.Open_Connection();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");    
            
            }
            Session["conString"] = conString;
            Session["nombre"] = this.nombre.ToString();
            Session["contrasena"] = contrasena.ToString();
            Session["BD"] = "LOGISTIC";
            Session.Timeout = 120;
            db.Close_Connection();
            return RedirectToAction("Index", "Home");
        }

    }
}
