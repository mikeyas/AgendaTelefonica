using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AgendaTelefonica.DAO
{
    public class AdministradorDAO : UsuariosDAO
    {
        public Boolean CriarUsuario(string nome, string senha, string email)
        {
            DBContatosEntities conexao = new DBContatosEntities();
            if (nome == "administrador"
                || (from Usuario in conexao.Usuarios where Usuario.Email == email select Usuario).First().Email == email
                || (from Usuario in conexao.Usuarios where Usuario.Nome == nome select Usuario).First().Nome == nome)
            {
                return false;
            }
            else
            {

                try
                {

                    Usuario usuario = new Usuario() { Nome = nome, Senha = senha, Email = email };
                    conexao.Usuarios.Add(usuario);
                    conexao.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        public Boolean AlterarUsuario(int id, string nome, string email)
        {
            if (nome != "administrador")
            {
                try
                {
                    DBContatosEntities conexao = new DBContatosEntities();
                    Usuario user = (from Usuario in conexao.Usuarios where Usuario.Id == id select Usuario).First();
                    user.Nome = nome;
                    user.Email = email;
                    conexao.SaveChanges();
                    return true;

                }
                catch
                {
                    return false;
                }
            }
            else
                return false;

        }

        public string RersetarSenha(int id)
        {

            try
            {
                DBContatosEntities conexao = new DBContatosEntities();
                Usuario user = (from Usuario in conexao.Usuarios where Usuario.Id == id select Usuario).First();
                Random random = new Random();

                string senha = Convert.ToString(random.Next());
                Models.algoritmoMD5 MD5 = new Models.algoritmoMD5();
                string senhaMD5 = MD5.GetMD5("#$%" + user.Nome + senha);
                if (user.Nome != "administrador")
                {
                    user.Senha = senhaMD5;
                    conexao.SaveChanges();
                    return senha;
                }
                else
                {
                    return "(tente novamente)";
                }
            }
            catch
            {
            return "(tente novamente)";
            }
        }

        public Boolean RemoverUsuario(int id)
        {
            
                try
                {
                    DBContatosEntities conexao = new DBContatosEntities();
                    Usuario user = (from Usuario in conexao.Usuarios where Usuario.Id == id select Usuario).First();
                    if (user.Nome != "administrador")
                    {
                        conexao.Usuarios.Remove(user);
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