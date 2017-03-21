using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaTelefonica.Controllers
{
    public class ServicosController : Controller
    {

        [HttpPost]
        public ActionResult SalvarContato(string nome, string telefone, string lembranca)
        {
            try
            {
                DBContatosEntities conexao = new DBContatosEntities();

                Contato contato = new Contato() { Nome = nome, Telefone = telefone, Lembranca = lembranca };
                conexao.Contatos.Add(contato);
                conexao.SaveChanges();
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
            DBContatosEntities conexao = new DBContatosEntities();

            return Json(conexao.Contatos.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}