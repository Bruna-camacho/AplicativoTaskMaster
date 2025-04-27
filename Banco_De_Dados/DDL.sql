create database taskmaster;
use taskmaster;create database taskmaster;
use taskmaster;

create table tarefa (
  id int identity(1,1),
  descricao varchar(200) not null,
  data_hora datetime not null,
  prioridade varchar(10) not null,
  concluida bit default 0,
  constraint pk_tarefa primary key (id)
);


ALTER TABLE tarefa
ADD CONSTRAINT uq_tarefa_unica UNIQUE (descricao, data_hora, prioridade);

