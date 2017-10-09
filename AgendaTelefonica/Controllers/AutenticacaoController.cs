using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using AgendaTelefonica.DAO;
using AgendaTelefonica.Models;

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
        public ActionResult LogOn(FormCollection usuario, string ReturnUrl)
        {
            UsuariosDAO usuarioDAO = new UsuariosDAO();
            algoritmoMD5 MD5 = new algoritmoMD5();
            string login = usuario["login"];
            //string senha = usuario["senha"];
            string senha = MD5.GetMD5("#$%" + login + usuario["senha"]);

            if (usuarioDAO.VerificaLogin(login, senha))
                    {
                        FormsAuthentication.SetAuthCookie(login, false);
                        return Redirect("~/Home/Index");
                    }
                else
                    {
                    Response.Write("<script>alert('Usuário ou Senha incorretos');</script>");
                    return View();
                    }
        }

        [HttpGet]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(FormCollection usuario)
        {
            string login = User.Identity.Name;
            UsuariosDAO usuarioDAO = new UsuariosDAO();
            algoritmoMD5 MD5 = new algoritmoMD5();
            //string senha = usuario["senha atual"];
            //string novasenha = usuario["nova senha"];
            string novasenha = MD5.GetMD5("#$%"+login+usuario["nova senha"]);
            string senha = MD5.GetMD5("#$%"+login+usuario["senha atual"]);

            if (usuario["nova senha"] == usuario["repetir nova senha"])
            {
                if (usuarioDAO.AlterarSenha(login, senha, novasenha))
                {
                    Response.Write("<script>alert('Senha alterada com sucesso!');</script>");
                    return Redirect("~/Home/Index");
                }
                else
                {
                    Response.Write("<script>alert('Usuário ou Senha incorretos');</script>");
                    return View();
                }
            }else
            {
                Response.Write("<script>alert('A nova senha não coincide. Repita a operação!');</script>");
                return View();
            }
        }

        [HttpGet]
        public ActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarUsuario(FormCollection usuario)
        {
            if (User.Identity.Name == "administrador")
            {
                algoritmoMD5 MD5 = new algoritmoMD5();
                string login = usuario["login"];
                string senha = MD5.GetMD5("#$%" + login + usuario["senha"]);
                string email = usuario["email"];

                AdministradorDAO admin = new AdministradorDAO();
                try
                {
                    admin.CriarUsuario(login, senha, email);
                    Response.Write("<script>alert('Usuário criado com sucesso!');</script>");
                    return Redirect("~/Home/Index");
                   
                }
                catch
                {
                    Response.Write("<script>alert('O usuário não pode ser criado.');</script>");
                    return View();
                }
            }
            else
            {
                Response.Write("<script>alert('Você não é ADMINISTRADOR!');</script>");
                    return View();
            }
        }

        [HttpGet]
        public ActionResult GerenciarUsuarios()
        {
            DBContatosEntities conexao = new DBContatosEntities();

            return Json(conexao.Usuarios.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Home/Index");
        }
    }
}