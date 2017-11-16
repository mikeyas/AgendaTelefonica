using System;
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
            ViewBag.Title = "Sobre o Sistema de Gerenciamento de Contatos Telefônicos";
            ViewBag.Message = "Conheça o SGCT ou entre em contato conosco:";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Title = "Cadastrar novo Contato.";
            ViewBag.Message = "Adicione um novo contato à agenda:";

            return View();
        }

        [Authorize]
        public ActionResult EditContact()
        {
            ViewBag.Title = "Edição de Contato";
            ViewBag.Message = "Edite um contato da agenda.";

            return View();
        }


        public ActionResult DetailsContact()
        {
            ViewBag.Title = "Exibição de Contato";
            ViewBag.Message = "Visualize     todos os dados do contato";

            return View();
        }
    }
}