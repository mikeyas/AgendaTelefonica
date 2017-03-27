﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaTelefonica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Adicione um novo contato à agenda.";

            return View();
        }

        [Authorize]
        public ActionResult EditContact()
        {
            ViewBag.Message = "Edite um contato da agenda.";

            return View();
        }


        public ActionResult DetailsContact()
        {
            ViewBag.Message = "Exibe todos os dados do contato";

            return View();
        }
    }
}