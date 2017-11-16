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
            ViewBag.Title = "Login do sistema";
            ViewBag.Message = "Insira seu nome de usuário e senha:";
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
                if (login == "administrador")
                {
                    return Redirect("~/autenticacao/gerenciarusuarios");
                }
                else
                {
                    return Redirect("~/home/index");
                }
                        
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
            ViewBag.Title = "Alteração de senha do usuários";
            ViewBag.Message = "Preencha o formulário para alterar sua senha de usuário:";
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
        public ActionResult CadastrarUsuario()
        {
            ViewBag.Title = "Cadastro de Usuários";
            ViewBag.Message = "Adicione ou edite usuários do sistema:";

            DBContatosEntities conexao = new DBContatosEntities();
            List<Usuario> usuarios = conexao.Usuarios.ToList();
            ViewBag.usuarios = usuarios;
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(FormCollection usuario)
        {
            if (User.Identity.Name == "administrador")
            {
                string login = usuario["login"];
                string email = usuario["email"];
                AdministradorDAO admin = new AdministradorDAO();
                if (usuario["id"] != "")
                {                                       
                    try
                    {
                        int id = Convert.ToInt32(usuario["id"]);
                        if(admin.AlterarUsuario(id, login, email))
                        {
                            return Content("<script>alert('Usuário alterado com sucesso!');window.location='gerenciarusuarios'</script>");
                        }
                        else
                        {
                            return Content("<script>alert('O usuário não pôde ser alterado!');window.location='gerenciarusuarios'</script>");
                        }

                    }
                    catch
                    {
                        return Content("<script>alert('O usuário não pôde ser alterado!');window.location='gerenciarusuarios'</script>");
                    } 
                }
                else
                {
                    if(usuario["senha"]!= usuario["senha2"])
                    {
                        return Content("<script>alert('As senhas digitadas não coincidem!');window.location='cadastrarusuario'</script>");
                    }

                    algoritmoMD5 MD5 = new algoritmoMD5();
                    string senha = MD5.GetMD5("#$%" + login + usuario["senha"]);
                    
                    try
                    {
                        if (admin.CriarUsuario(login, senha, email))
                        {
                            return Content("<script>alert('Usuário criado com sucesso!');window.location='gerenciarusuarios'</script>");
                        }
                        else
                        {
                            return Content("<script>alert('O usuário ou e-mail já estão cadastrados no sistema');window.location='gerenciarusuarios'</script>");
                        }
                    }
                    catch
                    {
                        return Content("<script>alert('O usuário não pôde ser criado!');window.location='gerenciarusuarios'</script>");
                    }
                }
            }
            else
            {
                return Content("<script>alert('Você não é ADMINISTRADOR!');window.location='gerenciarusuarios'</script>");
            }
        }

        [HttpGet]
        public ActionResult GerenciarUsuarios()
        {
            ViewBag.Title = "Gerenciar Usuários";
            ViewBag.Message = "Gerencie usuários do sistema:";
            DBContatosEntities conexao = new DBContatosEntities();
            List<Usuario> usuarios = conexao.Usuarios.ToList();
            ViewBag.usuarios = usuarios;
            return View();
        }

        public ActionResult RemoverUsuario()
        {
            AdministradorDAO admin = new AdministradorDAO();
            int id = Convert.ToInt32(Request.QueryString["id"]);

            try
            {
                if (admin.RemoverUsuario(id))
                {
                    return Content("<script>alert('Usuário removido com sucesso!');window.location='gerenciarusuarios'</script>");
                    //return Redirect("~/autenticacao/gerenciarusuarios");
                }
                else
                {
                    return Content("<script>alert('O usuário não pôde ser removido!');window.location='gerenciarusuarios'</script>");
                }
            }
            catch
            {
                return Content("<script>alert('O usuário não pôde ser removido!');window.location='gerenciarusuarios'</script>");
            }
        }

        public ActionResult ResetarSenha()
        {
            AdministradorDAO admin = new AdministradorDAO();
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string user = Request.QueryString["user"];

            try
            {
                string resetsenha = admin.RersetarSenha(id);
                if(resetsenha.Equals("tente novamente")){
                    return Content("<script>alert('A senha não pôde ser resetada!');window.location='gerenciarusuarios'</script>");
                }
                else
                {
                    string resultado = string.Format("<script>alert('Senha resetada com sucesso! Usuário: {0} Nova senha: {1}');window.location='gerenciarusuarios'</script>", user, resetsenha);
                    return Content(resultado);
                }
                //return Content("<script>alert('Senha resetada com sucesso! A nova senha é:');window.location='gerenciarusuarios'</script>");
                //return Redirect("~/autenticacao/gerenciarusuarios");

            }
            catch
            {
                return Content("<script>alert('A senha não pôde ser resetada!');window.location='gerenciarusuarios'</script>");
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Home/Index");
        }
    }
}