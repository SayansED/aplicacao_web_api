using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AlunoDAO
    {
        //string stringConexao = ConfigurationManager.AppSettings["ConnectionString"];
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<Aluno> ListarAlunosDB()
        {
            var listaAlunos = new List<Aluno>();

            IDbCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from Alunos";

            IDataReader resultado = selectCmd.ExecuteReader();
            while (resultado.Read())
            {
                var al = new Aluno();
                al.id = Convert.ToInt32(resultado["Id"]);
                al.nome = Convert.ToString(resultado["nome"]);
                al.sobrenome = Convert.ToString(resultado["sobrenome"]);
                al.telefone = Convert.ToString(resultado["telefone"]);
                al.ra = Convert.ToInt32(resultado["ra"]);

                listaAlunos.Add(al);
            }
            conexao.Close();
            return listaAlunos;
        }

        public void InserirAlunoDB(Aluno aluno)
        {
            IDbCommand insertCmd = conexao.CreateCommand();
            insertCmd.CommandText = "insert into Aluno (nome, sobrenome, telefone, ra) values (@nome, @sobrenome, @telefone, @ra)";

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
            insertCmd.Parameters.Add(paramNome);

            IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
            insertCmd.Parameters.Add(paramSobrenome);

            IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
            insertCmd.Parameters.Add(paramTelefone);

            IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);
            insertCmd.Parameters.Add(paramRa);

            insertCmd.ExecuteNonQuery();
        }
    }
}