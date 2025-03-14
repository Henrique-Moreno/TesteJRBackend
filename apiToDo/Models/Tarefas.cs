using apiToDo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Models
{
    public class Tarefas
    {
        // // **Mudança** = Lista estática para persistir dados
        private static List<TarefaDTO> _tarefas = new List<TarefaDTO>();

        static Tarefas()
        {
            _tarefas.Add(new TarefaDTO { ID_TAREFA = 1, DS_TAREFA = "Fazer Compras" });
            _tarefas.Add(new TarefaDTO { ID_TAREFA = 2, DS_TAREFA = "Fazer Atividade Faculdade" });
            _tarefas.Add(new TarefaDTO { ID_TAREFA = 3, DS_TAREFA = "Subir Projeto de Teste no GitHub" });
        }

        // **Mudança** =  Construtor vazio, pois a inicialização estática já adiciona as tarefas iniciais
        public Tarefas() { }


        //  Retorna a lista estática em vez de criar uma nova lista a cada chamada
        public List<TarefaDTO> lstTarefas()
        {
            return _tarefas;
        }

        public void InserirTarefa(TarefaDTO Request)
        {
            if (!_tarefas.Any(t => t.ID_TAREFA == Request.ID_TAREFA))
            {
                _tarefas.Add(Request);
            }
        }

        //  Remove a tarefa diretamente da lista estática
        public void DeletarTarefa(int ID_TAREFA)
        {
            var tarefa = _tarefas.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);
            if (tarefa != null)
            {
                _tarefas.Remove(tarefa);
            }
        }
    }
}
