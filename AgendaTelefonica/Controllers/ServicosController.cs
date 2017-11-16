using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgendaTelefonica.DAO;
using AgendaTelefonica.Models;

namespace AgendaTelefonica.Controllers
{
    public class ServicosController : Controller
    {

        [HttpPost]
        public ActionResult SalvarContato(string nome, string telefone, string lembranca)
        {
            try
            {
                ContatosDAO contatoDAO = new ContatosDAO();
                Contato contato = new Contato() { Nome = nome, Telefone = telefone, Lembranca = lembranca };
                contatoDAO.SalvarContato(contato);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult AddContato(string id, string nome, string telefone, string lembranca)
        {
            try
            {
                ContatosDAO contatoDAO = new ContatosDAO();
                contatoDAO.EditarContato(id, nome, telefone, lembranca);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }

        }

        [HttpPost]
        public ActionResult RemoveContato(string id)
        {
            try
            {
                ContatosDAO contatoDAO = new ContatosDAO();
                contatoDAO.RemoverContato(id);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }

        }

        [HttpGet]
        public ActionResult Contatos()
        {
            ContatosDAO contatoDAO = new ContatosDAO();
            return Json(contatoDAO.ListarContatos(), JsonRequestBehavior.AllowGet);
        }
    }
}