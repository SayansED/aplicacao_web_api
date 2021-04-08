using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route("RecuperarPorId/{id:int}")]
        public Aluno RecuperarPorId(int id)
        {
            Aluno aluno = new Aluno();
            return aluno.ListarAlunos(id).FirstOrDefault();
        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult RecuperarPorDataNome(string data, string nome)
        {
            try
            {
                Aluno aluno = new Aluno();
                IEnumerable<Aluno> alunos = aluno.ListarAlunos().Where(x => x.data == data || x.nome == nome);
                if (!alunos.Any())
                    return NotFound(); //404

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
         
        }

        // POST: api/Aluno
        [HttpPost]
        public IHttpActionResult Post(Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
                _aluno.Inserir(aluno);
                return Ok(_aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        // PUT: api/Aluno/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
                aluno.id = id;
                _aluno.Atualizar(aluno);
                return Ok(_aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/aluno/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Aluno _aluno = new Aluno();
                _aluno.Deletar(id);
                return Ok("Deletado com sucesso");
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }      
        }
    }
}
