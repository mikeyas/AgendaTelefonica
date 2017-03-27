using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;


namespace AgendaTelefonica.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string usuario, string senha, string returnUrl)
        {
            if (usuario == "mikeyas" && senha == "123456")
            {
                FormsAuthentication.SetAuthCookie(usuario, false);
                //return Redirect(returnUrl);
                return Json(true);
            }
            // return View();
            return Json(false);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("Home/Index");
        }
    }
}