﻿using System;
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

        public List<AlunoDTO> ListarAlunosDB(int? id = null)
        {
            var listaAlunos = new List<AlunoDTO>();
            try
            {                
                IDbCommand selectCmd = conexao.CreateCommand();
                if (id == null)
                {
                    selectCmd.CommandText = "select * from Alunos";
                }
                else
                {
                    selectCmd.CommandText = $"select * from Alunos where id = {id}";
                }
                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var al = new AlunoDTO // injeta diretamente
                    {
                        id = Convert.ToInt32(resultado["Id"]),
                        nome = Convert.ToString(resultado["nome"]),
                        sobrenome = Convert.ToString(resultado["sobrenome"]),
                        telefone = Convert.ToString(resultado["telefone"]),
                        ra = Convert.ToInt32(resultado["ra"]),
                };                  
                    listaAlunos.Add(al);
                }
                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }

        public void InserirAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "insert into Alunos (nome, sobrenome, telefone, ra) values (@nome, @sobrenome, @telefone, @ra)";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);                
                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);                
                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);                
                IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);

                insertCmd.Parameters.Add(paramNome);
                insertCmd.Parameters.Add(paramSobrenome);
                insertCmd.Parameters.Add(paramTelefone);
                insertCmd.Parameters.Add(paramRa);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }         
        }

        public void AtualizarAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "update Alunos set nome = @nome, sobrenome = @sobrenome, telefone = @telefone, ra = @ra where id = @id";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);

                updateCmd.Parameters.Add(paramNome);
                updateCmd.Parameters.Add(paramSobrenome);
                updateCmd.Parameters.Add(paramTelefone);
                updateCmd.Parameters.Add(paramRa);

                IDbDataParameter paramID = new SqlParameter("id", aluno.id);
                updateCmd.Parameters.Add(paramID);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }            
        }

        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "delete from Alunos where id = @id";

                IDbDataParameter paramID = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramID);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }           
        }
    }
}