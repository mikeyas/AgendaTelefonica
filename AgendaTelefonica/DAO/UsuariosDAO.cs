using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AgendaTelefonica.DAO
{
    public class UsuariosDAO
    {
        public Boolean VerificaLogin(string login, string senha)
        {
            try
            {
                DBContatosEntities conexao = new DBContatosEntities();
                Usuario user = (from Usuario in conexao.Usuarios where Usuario.Nome == login select Usuario).First();
                if (senha == user.Senha)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        public Boolean AlterarSenha(string login, string senha, string novasenha)
        {
            try
            {
                DBContatosEntities conexao = new DBContatosEntities();
                Usuario user = (from Usuario in conexao.Usuarios where Usuario.Nome == login select Usuario).First();
                if (senha == user.Senha)
                {
                    user.Senha = novasenha;
                    conexao.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }



    }
}