use taskmaster;

insert into tarefa (descricao, data_hora, prioridade, concluida)
values 
('Estudar Angular', CONVERT(datetime, '2025-04-25 14:00', 120), 'Alta', 0),
('Revisar API .NET', CONVERT(datetime, '2025-04-25 16:30', 120), 'Média', 0),
('Enviar relatório final', CONVERT(datetime, '2025-04-26 09:00', 120), 'Baixa', 1);

select * from tarefa;