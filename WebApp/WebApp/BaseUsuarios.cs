using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public static class BaseUsuarios
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario { Nome = "Admin", Senha = "admin",
                    Funcoes = new string[] { Funcao.Administrador } },

                new Usuario { Nome = "Professor", Senha = "professor",
                    Funcoes = new string[] { Funcao.Professor } },

                new Usuario { Nome = "Aluno", Senha = "aluno",
                    Funcoes = new string[] { Funcao.Aluno } },

                new Usuario { Nome = "Coordenador", Senha = "coordenador",
                    Funcoes = new string[] { Funcao.Professor, Funcao.Administrador } },
            };
        }
    }

    public class Usuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string[] Funcoes { get; set; }
    }

    // Funções
    public class Funcao
    {
        public const string Aluno = "Aluno";
        public const string Professor = "Professor";
        public const string Administrador = "Administrador";
    }
}