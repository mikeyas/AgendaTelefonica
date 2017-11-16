using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaTelefonica.DAO
{
    public class ContatosDAO
    {
        public List<Contato> ListarContatos()
        {
            DBContatosEntities conexao = new DBContatosEntities();
            List<Contato> contatos = new List<Contato>();
            contatos = conexao.Contatos.ToList();
            return contatos;
        }
        public Boolean SalvarContato(Contato contato)
        {
            try
            {
                DBContatosEntities conexao = new DBContatosEntities();
                conexao.Contatos.Add(contato);
                conexao.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Boolean EditarContato(string id, string nome, string telefone, string lembranca)
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
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Boolean RemoverContato(string id)
        {
            try
            {
                int _id = Convert.ToInt32(id);
                DBContatosEntities conexao = new DBContatosEntities();
                Contato contato = (from contatos in conexao.Contatos where contatos.Id == _id select contatos).First();
                conexao.Contatos.Remove(contato);
                conexao.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }


}