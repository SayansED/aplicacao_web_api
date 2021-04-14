using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AlunoDTO 
    {
        public int id { get; set; }

        [Required(ErrorMessage = "O nome é de Preenchimento Obrigatório")]
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public string data { get; set; }

        [Required(ErrorMessage = "O RA é de Preenchimento Obrigatório OBS: Int")]
        public int? ra { get; set; } // aceita null
    }
}