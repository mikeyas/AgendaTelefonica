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

        [HttpPost]
        public ActionResult AddContato(string id, string nome, string telefone, string lembranca)
        {
            try
            {
                int _id = Convert.ToInt32(id);
                DBContatosEntities conexao = new DBContatosEntities();
                Contato contato = (from contatos in conexao.Contatos where contatos.Id == _id select contatos).First();
                contato.Nome = nome;
                contato.Lembranca = lembranca;
                contato.Telefone = telefone;
                conexao.SaveChanges();
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
                int _id = Convert.ToInt32(id);
                DBContatosEntities conexao = new DBContatosEntities();
                Contato contato = (from contatos in conexao.Contatos where contatos.Id == _id select contatos).First();
                conexao.Contatos.Remove(contato);
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