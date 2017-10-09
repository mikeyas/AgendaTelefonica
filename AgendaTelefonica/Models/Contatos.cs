using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Models
{
    public class Contatos
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Telefone { get; set; }
            public string Lembranca { get; set; }
        }
}