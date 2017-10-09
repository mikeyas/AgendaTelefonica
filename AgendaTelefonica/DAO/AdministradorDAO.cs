using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.DAO
{
    public class AdministradorDAO : UsuariosDAO
    {
        public Boolean CriarUsuario(string nome, string senha, string email)
        {
            if (nome != "administrador")
            {
                try
                {
                    DBContatosEntities conexao = new DBContatosEntities();
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
            else
            {
                return false;
            }
        }

        public Boolean AlterarUsuario(string nome, string senha, string email)
        {
            if (nome != "administrador")
            {
                try
                {
                    DBContatosEntities conexao = new DBContatosEntities();
                    Usuario user = (from Usuario in conexao.Usuarios where Usuario.Nome == nome select Usuario).First();
                    user.Nome = nome;
                    user.Senha = senha;
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

        public Boolean RemoverUsuario(string nome)
        {
            if (nome != "administrador")
            {
                try
                {
                    DBContatosEntities conexao = new DBContatosEntities();
                    Usuario user = (from Usuario in conexao.Usuarios where Usuario.Nome == nome select Usuario).First();
                    conexao.Usuarios.Remove(user);
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

    }
}