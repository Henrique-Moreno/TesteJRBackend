using apiToDo.DTO;
using apiToDo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        // Instância da classe Tarefas para gerenciar as tarefas
        private readonly Tarefas _tarefasService;

        // Construtor para inicializar a instância de Tarefas
        public TarefasController()
        {
            _tarefasService = new Tarefas();
        }

        // Método para retornar a lista de tarefas
        [HttpGet("lstTarefas")]
        public IActionResult lstTarefas()
        {
            try
            {
                var tarefas = _tarefasService.lstTarefas();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                return StatusCode(500, new { msg = "Erro interno ao buscar tarefas" });
            }
        }

        // Método para inserir uma nova tarefa
        [HttpPost("InserirTarefas")]
        public IActionResult InserirTarefas([FromBody] TarefaDTO novaTarefa)
        {
            try
            {
                // Verifica se a tarefa já existe antes de inserir
                if (!_tarefasService.lstTarefas().Any(t => t.ID_TAREFA == novaTarefa.ID_TAREFA))
                {
                    _tarefasService.InserirTarefa(novaTarefa);
                }
                return Ok(_tarefasService.lstTarefas());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                return StatusCode(500, new { msg = "Erro interno ao inserir tarefa" });
            }
        }

        // Método para deletar uma tarefa
        [HttpDelete("DeletarTarefa")]
        public IActionResult DeletarTarefa([FromQuery] int ID_TAREFA)
        {
            try
            {
                // Deleta a tarefa pelo ID
                _tarefasService.DeletarTarefa(ID_TAREFA);

                // Retorna a lista atualizada com status 200 OK
                return Ok(_tarefasService.lstTarefas());
            }
            catch (KeyNotFoundException ex)
            {
                // Retorna erro 404 se a tarefa não for encontrada
                return NotFound(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                return StatusCode(500, new { msg = "Erro interno ao deletar tarefa" });
            }
        }

        // Método para atualizar uma tarefa
        [HttpPut("AtualizarTarefa")]
        public IActionResult AtualizarTarefa([FromBody] TarefaDTO tarefaAtualizada)
        {
            try
            {
                var tarefas = _tarefasService.lstTarefas();
                var tarefa = tarefas.FirstOrDefault(t => t.ID_TAREFA == tarefaAtualizada.ID_TAREFA);
                if (tarefa != null)
                {
                    tarefa.DS_TAREFA = tarefaAtualizada.DS_TAREFA;
                    return Ok(_tarefasService.lstTarefas());
                }
                else
                {
                    return NotFound("Tarefa não encontrada");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                return StatusCode(500, new { msg = "Erro interno ao atualizar tarefa" });
            }
        }

        // Método para pegar uma tarefa por ID
        [HttpGet("GetTarefaPorId/{id}")]
        public IActionResult GetTarefaPorId(int id)
        {
            try
            {
                var tarefas = _tarefasService.lstTarefas();
                var tarefa = tarefas.FirstOrDefault(t => t.ID_TAREFA == id);
                if (tarefa != null)
                {
                    return Ok(tarefa);
                }
                else
                {
                    return NotFound("Tarefa não encontrada");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                return StatusCode(500, new { msg = "Erro interno ao buscar tarefa" });
            }
        }
    }
}
