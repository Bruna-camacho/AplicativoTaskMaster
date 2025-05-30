Aplicativo de Gerenciamento de Tarefas

## Tecnologias Utilizadas

- **Backend**: ASP.NET Web API (.NET Framework)
- **Frontend**: Angular 17
- **Banco de Dados**: SQL Server

## Funcionalidades

✅ Cadastro, edição, exclusão e conclusão de tarefas.
✅ Filtros por descrição, data e prioridade.
✅ Modo Claro e Escuro (alternância via botão).
✅ Validação de campos obrigatórios no formulário.
✅ Mensagens de erro amigáveis para o usuário.

## Observações

É necessário ter instalado:

- Node.js
- Angular CLI
- Visual Studio 2022 ou superior
- SQL Server e SQL Server Management Studio

## Instruções de Execução

### 1. Banco de Dados
- Rodar o script `Banco_De_Dados/DDL.sql` para criar o banco de dados.
- Rodar o script `Banco_De_Dados/DML.sql` para inserir dados iniciais.

### 2. Backend
- Abrir a solução `TaskMaster.sln` no Visual Studio.
- Restaurar os pacotes NuGet (Microsoft.AspNet.WebApi.Core, Microsoft.AspNet.WebApi.Client, Microsoft.AspNet.WebApi.Cors...).
- Antes de executar, configure a conexão com o banco (vá em Web.config e mude a connection string conforme seu ambiente local)
- Executar o projeto `TaskMaster_Backend` (irá subir na porta `https://localhost:44339`).

### 3. Frontend
- Acessar a pasta `TaskMaster_Frontend`.
- Executar: npm install.
- Vá no arquivo tarefa.service.ts e cole sua URL que rodou o backend.
- Rode o projeto angular ng serve --open

## Demonstração do Projeto

### Tela Inicial
![Tela Inicial](./home.png)

### Tarefas Cadastradas
![Tarefas](./tarefas.png)

### Modo Escuro Ativado
![Modo Escuro](./modo-escuro.png)
