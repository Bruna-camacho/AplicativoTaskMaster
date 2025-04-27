using System;
using System.Web.Http;
using TaskMaster_Backend.Repositories;
using TaskMaster_Backend.Models;

namespace TaskMaster_Backend.Controllers
{
    [RoutePrefix("api/tarefas")]
    public class TarefasController : ApiController
    {
        private readonly TarefaRepository _repositorioTarefas;

        public TarefasController()
        {
            _repositorioTarefas = new TarefaRepository();
        }
                
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var tarefas = _repositorioTarefas.Select();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        [HttpGet]
        [Route("descricao")]
        public IHttpActionResult GetByDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao) || descricao.Length < 3)
                return BadRequest("A descrição deve conter no mínimo 3 caracteres.");

            try
            {
                var tarefas = _repositorioTarefas.SelectByDescricao(descricao);
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                
        [HttpGet]
        [Route("data")]
        public IHttpActionResult GetByData(string data)
        {
            if (!DateTime.TryParse(data, out DateTime dataConvertida))
                return BadRequest("Data inválida.");

            try
            {
                var tarefas = _repositorioTarefas.SelectByData(dataConvertida);
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                
        [HttpGet]
        [Route("prioridade")]
        public IHttpActionResult GetByPrioridade(string prioridade)
        {
            if (string.IsNullOrWhiteSpace(prioridade))
                return BadRequest("Prioridade inválida.");

            try
            {
                var tarefas = _repositorioTarefas.SelectByPrioridade(prioridade);
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var tarefa = _repositorioTarefas.SelectById(id);
                if (tarefa == null)
                    return NotFound();
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
                return BadRequest("Tarefa inválida.");

            try
            {
                var sucesso = _repositorioTarefas.Insert(tarefa);
                if (sucesso)
                    return Ok("Tarefa criada com sucesso.");
                else
                    return BadRequest("Erro ao criar a tarefa.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
                return BadRequest("Tarefa inválida.");

            try
            {
                var sucesso = _repositorioTarefas.Update(id, tarefa);
                if (sucesso)
                    return Ok("Tarefa atualizada com sucesso.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var sucesso = _repositorioTarefas.Delete(id);
                if (sucesso)
                    return Ok("Tarefa excluída com sucesso.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                
        [HttpPatch]
        [Route("{id:int}/concluir")]
        public IHttpActionResult ConcluirTarefa(int id)
        {
            try
            {
                var sucesso = _repositorioTarefas.MarcarComoConcluida(id);
                if (sucesso)
                    return Ok("Tarefa marcada como concluída.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
