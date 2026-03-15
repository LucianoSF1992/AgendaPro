📅 AgendaPro












Sistema web para gerenciamento de agendamentos de serviços, desenvolvido com ASP.NET Core MVC, C# e SQL Server.

O sistema permite que empresas ou profissionais organizem seus atendimentos, gerenciem clientes, serviços e acompanhem o status de cada agendamento de forma simples e eficiente.

🖥️ Preview do Sistema

(Em breve imagens da aplicação)

Exemplo de funcionalidades:

Dashboard de atendimentos

Cadastro de clientes

Cadastro de serviços

Cadastro de profissionais

Agendamento de serviços

Controle de status do atendimento

🎯 Objetivo do Projeto

Permitir que empresas ou profissionais organizem seus agendamentos de serviços, sabendo:

quem será atendido

em qual horário

por qual profissional

qual o serviço

qual o status do atendimento

🧰 Tecnologias Utilizadas
Backend

.NET 9

ASP.NET Core MVC

C#

Entity Framework Core

Banco de Dados

SQL Server

Frontend

Razor Views

Bootstrap 5

HTML / CSS

Ferramentas

Git

GitHub

Visual Studio / VS Code

👥 Tipos de Usuários
👑 Admin

Gerencia clientes

Gerencia serviços

Gerencia profissionais

Cria e edita agendamentos

Visualiza toda a agenda

👨‍🔧 Profissional

Visualiza seus próprios agendamentos

Atualiza status dos atendimentos

📋 Funcionalidades do MVP

Autenticação de usuários com Identity

Cadastro de clientes

Cadastro de serviços

Cadastro de profissionais

Criação de agendamentos

Controle de status

Dashboard de agenda

Histórico de atendimentos

Layout responsivo

🏗️ Arquitetura do Projeto

O projeto segue o padrão ASP.NET Core MVC.

AgendaPro
│
├── Controllers
├── Models
├── ViewModels
├── Data
│ ├── ApplicationDbContext
│ └── Migrations
├── Views
├── wwwroot
└── Program.cs

🚀 Como executar o projeto
1️⃣ Clonar o repositório

git clone https://github.com/LucianoSF1992/AgendaPro.git

2️⃣ Entrar na pasta do projeto

cd AgendaPro

3️⃣ Restaurar dependências

dotnet restore

4️⃣ Aplicar migrations

dotnet ef database update

5️⃣ Executar o projeto

dotnet run

O sistema será executado em uma URL local como:

http://localhost:5139

📊 Status do Projeto

🚧 Em desenvolvimento

O projeto está sendo desenvolvido de forma incremental, com novas funcionalidades sendo adicionadas a cada etapa.

📌 Roadmap do Projeto
🚧 Etapa 1 — Estrutura inicial

 Estrutura inicial do projeto

 Configuração do banco de dados

 Sistema de autenticação

📋 Etapa 2 — Cadastros

 Cadastro de clientes

 Cadastro de serviços

 Cadastro de profissionais

📅 Etapa 3 — Agendamentos

 Criação de agendamentos

 Controle de conflito de horários

 Atualização de status

📊 Etapa 4 — Dashboard

 Visualização diária

 Filtro por profissional

 Estatísticas de atendimentos

🕘 Etapa 5 — Histórico

 Histórico de agendamentos

 Filtros por cliente

 Filtros por profissional

🚀 Etapa 6 — Deploy

 Deploy na Hostinger

 Configuração de subdomínio

 Configuração de banco em produção

🌐 Deploy

O sistema será publicado em:

agendapro.lucianoferreiradev.com

📚 Aprendizados com o Projeto

Durante o desenvolvimento deste projeto foram aplicados conceitos importantes como:

Arquitetura MVC

ASP.NET Core Identity

Entity Framework Core

Relacionamentos entre entidades

ViewModels

Boas práticas de organização de código

Controle de versionamento com Git

👨‍💻 Autor

Luciano Silva Ferreira

Desenvolvedor Full Stack com foco em .NET e ASP.NET Core

🔗 Contato

🌐 Portfólio
https://lucianoferreiradev.com

💼 LinkedIn
https://www.linkedin.com/in/lucianoferreira92/

💻 GitHub
https://github.com/LucianoSF1992