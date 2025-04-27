using System;
using System.ComponentModel.DataAnnotations;

namespace TaskMaster_Backend.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]        
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        public DateTime DataHora { get; set; }

        private string _prioridade;

        [Required(ErrorMessage = "A prioridade é obrigatória.")]
        public string Prioridade
        {
            get => _prioridade;
            set
            {
                var valoresValidos = new[] { "baixa", "média", "alta" };
                if (!Array.Exists(valoresValidos, p => p.Equals(value, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Prioridade inválida. Valores permitidos: baixa, média, alta.");

                _prioridade = value;
            }
        }

        public bool Concluida { get; set; }
    }
}