using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using TaskMaster_Backend.Models;

namespace TaskMaster_Backend.Repositories
{
    public class TarefaRepository
    {
        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;

        public TarefaRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["taskmaster"].ConnectionString;
            _conn = new SqlConnection(connectionString);
            _cmd = _conn.CreateCommand();
        }

        public List<Tarefa> Select()
        {
            var tarefas = new List<Tarefa>();
            _conn.Open();
            _cmd.CommandText = "SELECT id, descricao, data_hora, prioridade, concluida FROM tarefa ORDER BY data_hora DESC";
            using (var dr = _cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    tarefas.Add(new Tarefa
                    {
                        Id = (int)dr["id"],
                        Descricao = dr["descricao"].ToString(),
                        DataHora = Convert.ToDateTime(dr["data_hora"]),
                        Prioridade = dr["prioridade"].ToString(),
                        Concluida = (bool)dr["concluida"]
                    });
                }
            }
            _conn.Close();
            return tarefas;
        }

        public List<Tarefa> SelectByDescricao(string descricao)
        {
            var tarefas = new List<Tarefa>();
            _conn.Open();
            _cmd.CommandText = "SELECT id, descricao, data_hora, prioridade, concluida FROM tarefa WHERE descricao LIKE @descricao ORDER BY data_hora DESC";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@descricao", $"%{descricao}%");
            using (var dr = _cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    tarefas.Add(new Tarefa
                    {
                        Id = (int)dr["id"],
                        Descricao = dr["descricao"].ToString(),
                        DataHora = Convert.ToDateTime(dr["data_hora"]),
                        Prioridade = dr["prioridade"].ToString(),
                        Concluida = (bool)dr["concluida"]
                    });
                }
            }
            _conn.Close();
            return tarefas;
        }

        public List<Tarefa> SelectByData(DateTime data)
        {
            var tarefas = new List<Tarefa>();
            _conn.Open();
            _cmd.CommandText = "SELECT id, descricao, data_hora, prioridade, concluida FROM tarefa WHERE CAST(data_hora AS DATE) = @data ORDER BY data_hora DESC";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@data", data.Date);
            using (var dr = _cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    tarefas.Add(new Tarefa
                    {
                        Id = (int)dr["id"],
                        Descricao = dr["descricao"].ToString(),
                        DataHora = Convert.ToDateTime(dr["data_hora"]),
                        Prioridade = dr["prioridade"].ToString(),
                        Concluida = (bool)dr["concluida"]
                    });
                }
            }
            _conn.Close();
            return tarefas;
        }

        public List<Tarefa> SelectByPrioridade(string prioridade)
        {
            var tarefas = new List<Tarefa>();
            _conn.Open();
            _cmd.CommandText = "SELECT id, descricao, data_hora, prioridade, concluida FROM tarefa WHERE prioridade = @prioridade ORDER BY data_hora DESC";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@prioridade", prioridade);
            using (var dr = _cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    tarefas.Add(new Tarefa
                    {
                        Id = (int)dr["id"],
                        Descricao = dr["descricao"].ToString(),
                        DataHora = Convert.ToDateTime(dr["data_hora"]),
                        Prioridade = dr["prioridade"].ToString(),
                        Concluida = (bool)dr["concluida"]
                    });
                }
            }
            _conn.Close();
            return tarefas;
        }

        public Tarefa SelectById(int id)
        {
            Tarefa tarefa = null;
            _conn.Open();
            _cmd.CommandText = "SELECT id, descricao, data_hora, prioridade, concluida FROM tarefa WHERE id = @id";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@id", id);
            using (var dr = _cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    tarefa = new Tarefa
                    {
                        Id = (int)dr["id"],
                        Descricao = dr["descricao"].ToString(),
                        DataHora = Convert.ToDateTime(dr["data_hora"]),
                        Prioridade = dr["prioridade"].ToString(),
                        Concluida = (bool)dr["concluida"]
                    };
                }
            }
            _conn.Close();
            return tarefa;
        }

        public bool Insert(Tarefa tarefa)
        {
            _conn.Open();
            _cmd.CommandText = "INSERT INTO tarefa (descricao, data_hora, prioridade, concluida) VALUES (@descricao, @data_hora, @prioridade, @concluida)";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@descricao", tarefa.Descricao);
            _cmd.Parameters.AddWithValue("@data_hora", tarefa.DataHora);
            _cmd.Parameters.AddWithValue("@prioridade", tarefa.Prioridade);
            _cmd.Parameters.AddWithValue("@concluida", tarefa.Concluida);
            int result = _cmd.ExecuteNonQuery();
            _conn.Close();
            return result > 0;
        }

        public bool Update(int id, Tarefa tarefa)
        {
            _conn.Open();
            _cmd.CommandText = "UPDATE tarefa SET descricao = @descricao, data_hora = @data_hora, prioridade = @prioridade, concluida = @concluida WHERE id = @id";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@descricao", tarefa.Descricao);
            _cmd.Parameters.AddWithValue("@data_hora", tarefa.DataHora);
            _cmd.Parameters.AddWithValue("@prioridade", tarefa.Prioridade);
            _cmd.Parameters.AddWithValue("@concluida", tarefa.Concluida);
            _cmd.Parameters.AddWithValue("@id", id);
            int result = _cmd.ExecuteNonQuery();
            _conn.Close();
            return result > 0;
        }

        public bool Delete(int id)
        {
            _conn.Open();
            _cmd.CommandText = "DELETE FROM tarefa WHERE id = @id";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@id", id);
            int result = _cmd.ExecuteNonQuery();
            _conn.Close();
            return result > 0;
        }

        public bool MarcarComoConcluida(int id)
        {
            _conn.Open();
            _cmd.CommandText = "UPDATE tarefa SET concluida = 1 WHERE id = @id";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@id", id);
            int result = _cmd.ExecuteNonQuery();
            _conn.Close();
            return result > 0;
        }
    }
}
