﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}