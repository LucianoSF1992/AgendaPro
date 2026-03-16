# 📅 AgendaPro

![.NET](https://img.shields.io/badge/.NET-9-blue)
![ASP.NET](https://img.shields.io/badge/ASP.NET-Core-purple)
![C#](https://img.shields.io/badge/C%23-Backend-green)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red)
![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

Sistema web para **gerenciamento de agendamentos de serviços**, desenvolvido com **ASP.NET Core MVC, C# e SQL Server**.

O sistema permite que empresas ou profissionais organizem seus atendimentos, gerenciem clientes, serviços e acompanhem o status de cada agendamento de forma simples e eficiente.

---

# 🖥️ Preview do Sistema

*(Em breve imagens da aplicação)*

Exemplo de funcionalidades:

- Dashboard de atendimentos
- Cadastro de clientes
- Cadastro de serviços
- Cadastro de profissionais
- Agendamento de serviços
- Controle de status do atendimento

---

# 🎯 Objetivo do Projeto

Permitir que empresas ou profissionais organizem seus agendamentos de serviços, sabendo:

- quem será atendido
- em qual horário
- por qual profissional
- qual o serviço
- qual o status do atendimento

---

# 🧰 Tecnologias Utilizadas

## Backend
- **.NET 9**
- **ASP.NET Core MVC**
- **C#**
- **Entity Framework Core**

## Banco de Dados
- **SQL Server**

## Frontend
- **Razor Views**
- **Bootstrap 5**
- **HTML / CSS**

## Ferramentas
- **Git**
- **GitHub**
- **Visual Studio / VS Code**

---

# 👥 Tipos de Usuários

## 👑 Admin
- Gerencia clientes
- Gerencia serviços
- Gerencia profissionais
- Cria e edita agendamentos
- Visualiza toda a agenda

## 👨‍🔧 Profissional
- Visualiza seus próprios agendamentos
- Atualiza status dos atendimentos

---

# 📋 Funcionalidades do MVP

- Autenticação de usuários com Identity
- Cadastro de clientes
- Cadastro de serviços
- Cadastro de profissionais
- Criação de agendamentos
- Controle de status
- Dashboard de agenda
- Histórico de atendimentos
- Layout responsivo

---

# 🏗️ Arquitetura do Projeto

O projeto segue o padrão **ASP.NET Core MVC**.

```text
AgendaPro
│
├── Controllers
├── Models
├── ViewModels
├── Data
│   ├── ApplicationDbContext
│   └── Migrations
├── Views
├── wwwroot
└── Program.cs

```

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

O sistema será executado em uma URL local, como por exemplo:

http://localhost:5139

📊 Status do Projeto

🚧 Em desenvolvimento

O projeto está sendo desenvolvido de forma incremental, com novas funcionalidades sendo adicionadas por etapas.


# 📌 Roadmap do Projeto

## 🚧 Etapa 1 — Estrutura inicial
- [x] Estrutura inicial do projeto
- [x] Configuração do banco de dados
- [x] Sistema de autenticação

## 📋 Etapa 2 — Cadastros
- [x] Cadastro de clientes
- [x] Cadastro de serviços
- [x] Cadastro de profissionais

## 📅 Etapa 3 — Agendamentos
- [x] Criação de agendamentos
- [x] Controle de conflito de horários
- [x] Atualização de status

## 🎨 Etapa 4 — Interface e experiência
- [x] Agrupamento de agendamentos por data
- [x] Exibição visual de status com badges
- [x] Destaque para agendamentos passados
- [x] Personalização do layout global
- [x] Personalização das páginas do ASP.NET Identity

## 📊 Etapa 5 — Dashboard
- [ ] Visualização diária
- [ ] Filtro por profissional
- [ ] Estatísticas de atendimentos

## 🕘 Etapa 6 — Histórico
- [ ] Histórico de agendamentos
- [ ] Filtros por cliente
- [ ] Filtros por profissional

## 🚀 Etapa 7 — Deploy
- [ ] Deploy na Hostinger
- [ ] Configuração de subdomínio
- [ ] Configuração de banco em produção 


🌐 Deploy

O sistema será publicado em:

agendapro.lucianoferreiradev.com


# 📚 Aprendizados com o Projeto

Durante o desenvolvimento deste projeto foram aplicados conceitos importantes como:

- Arquitetura MVC  
- ASP.NET Core Identity  
- Entity Framework Core  
- Relacionamentos entre entidades  
- ViewModels  
- Boas práticas de organização de código  
- Controle de versionamento com Git  

---

# 👨‍💻 Autor

**Luciano Silva Ferreira**

Desenvolvedor Full Stack com foco em **.NET e ASP.NET Core**

---

# 🔗 Contato

🌐 **Portfólio**  
https://lucianoferreiradev.com  

💼 **LinkedIn**  
https://www.linkedin.com/in/lucianoferreira92/  

💻 **GitHub**  
https://github.com/LucianoSF1992
