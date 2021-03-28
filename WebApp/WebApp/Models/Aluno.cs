using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Aluno
    {

        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public string data { get; set; }
        public int ra { get; set; }

        public List<Aluno> ListarAlunos()
        {
            try
            {
                var alunoDB = new AlunoDAO();
                return alunoDB.ListarAlunosDB();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Alunos: Erro => {ex.Message}");
            }
        }

        public bool RescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
            return true;
        }

        public void Inserir(Aluno aluno)
        {
            /*
            var listaAlunos = this.ListarAlunos();
            var maxId = listaAlunos.Max(aluno => aluno.id);
            listaAlunos.Add(Aluno);

            RescreverArquivo(listaAlunos);
            return Aluno;
            */

            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.InserirAlunoDB(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir Aluno: Erro => {ex.Message}");
            }
        }

        public Aluno Atualizar(int id, Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var itemIndex = listaAlunos.FindIndex(p => p.id == id);

            if (itemIndex >= 0)
            {
                Aluno.id = id;
                listaAlunos[itemIndex] = Aluno;
            }
            else
            {
                return null;
            }

            RescreverArquivo(listaAlunos);
            return Aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAlunos();
            var itemIdex = listaAlunos.FindIndex(p => p.id == id);

            if (itemIdex >= 0)
            {
                listaAlunos.RemoveAt(itemIdex);
            }
            else
            {
                return false;
            }
            RescreverArquivo(listaAlunos);
            return true;
        }
    }
}