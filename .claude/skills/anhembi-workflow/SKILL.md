---
name: anhembi-workflow
description: >
  Workflow completo de desenvolvimento para o Sistema de Gestão Acadêmico (CAA) da Anhembi Sorocaba.
  O usuário descreve o que quer fazer — feature, bug, melhoria, refatoração — e o workflow conduz
  brainstorm, spec, plano, execução com subagents, build, revisão e PR.
  Ative com: "implementar", "corrigir", "criar", "adicionar", "novo desenvolvimento", "quero fazer",
  "quero adicionar", "quero corrigir", "vamos desenvolver", "começar tarefa", "nova feature".
  Na dúvida, sempre invoque — pular leva a commits em branches erradas e código sem spec aprovada.
---

# Anhembi — Workflow de Desenvolvimento (CAA)

Siga as fases em ordem. Não pule fases. Não escreva código antes da aprovação do spec.

---

## Fase 1 — Entender a Tarefa

### 1.1 — Classificar a tarefa
Com base na descrição do usuário, classifique em uma das áreas do sistema:

| Área | Quando usar |
|---|---|
| **Matrículas** | Matrículas, alunos, cursos, turmas, disciplinas |
| **Financeiro** | Planos financeiros, descontos, pagamentos, inadimplência |
| **Estágios** | Estágios, contratos de estágio, supervisores, relatórios |
| **Identidade / Acesso** | Login, registro, perfil, usuários, roles, permissões |
| **Chat / Comunicação** | Chat em tempo real, mensagens, SignalR, ChatHub |
| **Administrativo** | Configurações gerais, seed de dados, dashboard |
| **UI / Layout** | Componentes visuais, layouts, Tailwind (login) ou AdminLTE (app) |

Se ambíguo, pergunte ao usuário por mais contexto antes de avançar.

### 1.2 — Definir branch de trabalho
Pergunte ao usuário:
> "Deseja trabalhar em uma **branch existente** ou **criar uma nova**?"

**Se branch existente:**
Pergunte o nome da branch e execute:
```powershell
git checkout {nome-da-branch}
git pull
```
Confirme: `"Trabalhando na branch: {nome-da-branch}"`. Pule para **Fase 2**.

**Se branch nova:** Continue para 1.3.

### 1.3 — Classificar e gerar nome de branch
Com base no tipo de tarefa:
- **Bug / Correção** → prefixo `fix/`
- **Feature / Nova funcionalidade** → prefixo `feat/`
- **Refatoração / Melhoria técnica** → prefixo `refactor/`
- **Melhoria de UX/UI** → prefixo `feat/`

Gere um slug curto (2–4 palavras em kebab-case) descrevendo a tarefa.

Exemplos:
- `feat/matricula-historico-aluno`
- `fix/financeiro-calculo-desconto`
- `feat/chat-notificacao-mensagem`
- `refactor/estagio-controller`
- `feat/identity-confirmacao-email`
- `feat/dashboard-graficos-apexcharts`

### 1.4 — Criar branch a partir de `main`
**Antes de qualquer artifact (spec, plano ou código)**, crie a branch:

```powershell
git checkout main
git pull
git checkout -b {nome-da-branch}
```

Confirme: `"Branch criada: {nome-da-branch}"`

---

## Fase 2 — Brainstorm e Spec

### 2.1 — Invocar skill de brainstorming
Invoque `superpowers:brainstorming` com:
- A descrição da tarefa
- A área identificada
- O contexto técnico do projeto:

**Stack CAA (Sistema de Gestão Acadêmico):**
- **ASP.NET Core 9 MVC + Razor Pages**, EF Core Code-First, SQL Server, ASP.NET Core Identity, SignalR
- **Projeto único:** `CAA/CAA.csproj` — sem solution multi-projeto
- **Build:** `dotnet build CAA/CAA.csproj`
- **Run:** `dotnet run --project CAA/CAA.csproj`
- **Migrations:** `dotnet ef migrations add <NomeMigration> --project CAA/CAA.csproj`
- **Publish:** `dotnet publish CAA/CAA.csproj -c Release`

**Dual layout — crítico ao editar qualquer View:**

| Área | Layout | CSS |
|---|---|---|
| Login / Identity (`Areas/Identity/`) | `Views/Shared/_Layout.cshtml` | Tailwind CDN, Lucide Icons, Inter font, paleta verde `#25D366`/`#128C7E` |
| App autenticado (`Views/`) | `Views/Shared/AdminLTE/_Layout.cshtml` | AdminLTE 4 + Bootstrap (local em `wwwroot/lib/admin-lte/`) |

Nunca misturar os dois sistemas de CSS na mesma view.

**Autenticação & Autorização:**
- `Usuario` estende `IdentityUser` com: `Nome`, `Sobrenome`, `Cargo`, `Departamento`, `FotoPerfil`, `Ativo`
- Login exige `EmailConfirmed = true` (enforced em `Program.cs`)
- Roles: `"Admin"`, `"Matrículas"`, `"Estágios"` — controllers usam `[Authorize(Roles = "...")]`
- Sidebar em `_MainNavigation.cshtml` renderiza itens condicionalmente por role
- Admin padrão: `admin@anhembisorocaba.com.br` / `Admin@123`
- Seeding via `SeedDataBase.SeedAll()` — roda em todo startup, idempotente

**Controllers:**
- Maioria estende `BaseController` → injeta `ApplicationDbContext` + `UserManager<Usuario>`
- `MatriculaController` e similares que não precisam de `UserManager` injetam só `ApplicationDbContext`
- `HomeController` é landing pós-login (`/`), exige `[Authorize]`

**Data Layer:**
- `ApplicationDbContext` estende `IdentityDbContext<Usuario>`
- Cascade delete global desabilitado — FKs usam `DeleteBehavior.Restrict`
- `decimal(18,2)` configurado explicitamente para `Desconto.Valor`, `PlanoFinanceiro.Valor`, `TipoDesconto.ValorPadrao`

**Real-time (SignalR):**
- `ChatHub` em `/chathub`
- Layout principal injeta conexão SignalR global → SweetAlert2 toasts para mensagens novas fora de `/chat`

**Email:**
- `EmailSender` (SMTP Gmail) registrado como `IEmailSender`
- Config SMTP em `appsettings.json` na chave `"Smtp"`

**Localização:** `pt-BR` fixo (datas, números)

**Assets estáticos (todos vendored em `wwwroot/`):**
AdminLTE, Bootstrap, Bootstrap Icons, DataTables, ApexCharts, OverlayScrollbars, jsvectormap

### 2.2 — Escrever spec
Após o brainstorming, escreva o spec em `docs/specs/{nome-da-branch}.md`:

```markdown
# Spec — {nome-da-branch}

## Objetivo
{o que será implementado ou corrigido}

## Contexto
{por que essa tarefa existe, qual problema resolve}

## Área do sistema
{Matrículas / Financeiro / Estágios / Identidade / Chat / Administrativo / UI}

## Layout afetado
{Identity (_Layout.cshtml com Tailwind) ou App (AdminLTE) — ou ambos}

## Arquivos a criar ou modificar
{lista com caminho completo de cada arquivo}

## Solução proposta
{abordagem técnica detalhada}

## Critérios de aceitação
- [ ] {critério 1}
- [ ] {critério 2}
- [ ] ...

## Fora do escopo
{o que explicitamente NÃO será feito nesta tarefa}
```

### 2.3 — Aprovação obrigatória ⛔
**PARE AQUI.** Não avance para Fase 3 sem aprovação explícita.

Apresente o spec e pergunte:
> "O spec está correto? Posso avançar para o plano de implementação?"

Se pedir ajustes, revise e pergunte novamente.

### 2.4 — Commitar spec
Após aprovação:
```powershell
git add docs/specs/{nome-da-branch}.md
git commit -m "docs(spec): adicionar spec {nome-da-branch}"
```

---

## Fase 3 — Plano de Implementação

### 3.1 — Invocar skill de planejamento
Invoque `superpowers:writing-plans` com o spec aprovado.

O plano deve incluir:
- Tarefas ordenadas com caminhos de arquivo exatos
- Quais tarefas são paralelas vs. sequenciais
- Comando de build para verificação após cada tarefa: `dotnet build CAA/CAA.csproj`
- Mensagem de commit convencional sugerida para cada tarefa

**Pipeline padrão para feature que cruza camadas:**
`Modelo/Entidade` → `DbContext/Fluent API` → `Migration` → `Controller` → `View` → `Revisão`

Features isoladas (ex: só ajuste de View) podem pular etapas desnecessárias.

Salve em `docs/superpowers/plans/{nome-da-branch}.md`.

### 3.2 — Commitar plano
```powershell
git add docs/superpowers/plans/{nome-da-branch}.md
git commit -m "docs(plan): adicionar plano {nome-da-branch}"
```

---

## Fase 4 — Execução

### 4.1 — Executar com subagents
Invoque `superpowers:subagent-driven-development` com o plano.

Cada subagent deve:
1. Implementar sua tarefa
2. Verificar build: `dotnet build CAA/CAA.csproj` — zero erros
3. Commitar no formato convencional: `tipo(escopo): descrição em português`

Exemplos de commits:
- `feat(matricula): adicionar histórico de matrículas do aluno`
- `fix(financeiro): corrigir cálculo de desconto acumulado`
- `feat(chat): exibir toast para mensagem recebida fora da rota /chat`
- `chore(db): migration AdicionaCampoAtivoUsuario`
- `feat(estagio): implementar listagem de contratos por supervisor`
- `refactor(identity): extrair validação de role para BaseController`

**Regras de layout obrigatórias:**
- Mudanças em `Areas/Identity/` → usar Tailwind CDN, paleta verde `#25D366`/`#128C7E`, sem Bootstrap
- Mudanças em `Views/` → usar classes Bootstrap/AdminLTE, assets locais de `wwwroot/lib/`
- Para novos componentes interativos na area do app: checar se DataTables, ApexCharts ou outro lib já vendored em `wwwroot/` atende antes de adicionar dependência nova

### 4.2 — Revisão de código
Invoque `pr-review-toolkit:code-reviewer` nos arquivos modificados.
Corrija problemas antes de avançar.

### 4.3 — Verificação contra o spec
Abra `docs/specs/{nome-da-branch}.md` e percorra o checklist de **Critérios de aceitação**:

- ✅ Atendido → segue
- ❌ Não atendido → retorne para 4.1, corrija, recompile, recomite

**Só avance para Fase 5 quando todos os critérios estiverem ✅.**

---

## Fase 5 — Push

### 5.1 — Verificar estado
```powershell
git status
dotnet build CAA/CAA.csproj
git log --oneline main..HEAD
```

Resolva qualquer problema antes de avançar.

### 5.2 — Push
```powershell
git push -u origin {nome-da-branch}
```

---

## Fase 6 — Pull Request

### 6.1 — Perguntar ao usuário
> "Deseja abrir o PR agora?"
> - **Sim** → PR pronto para revisão
> - **Rascunho** → Draft PR
> - **Não** → encerrar aqui

### 6.2 — Criar PR
Use `gh pr create` com:
- `--base main`
- `--title`: `{tipo}: {descrição curta da tarefa}`
- `--draft` se usuário escolheu "Rascunho"
- `--body`: conforme template abaixo

```markdown
## O que foi feito
{resumo do spec em bullets}

## Área do sistema
{Matrículas / Financeiro / Estágios / Identidade / Chat / Administrativo / UI}

## Layout afetado
{Identity (Tailwind) / App (AdminLTE) / Ambos}

## Como testar
{critérios de aceitação em formato checklist markdown}

## Arquivos principais
{lista dos arquivos criados ou modificados}

🤖 Gerado com [Claude Code](https://claude.ai/code)
```

### 6.3 — Resumo final
```
✅ Branch:  {nome-da-branch}
✅ PR:      {link}
✅ Build:   passou (dotnet build CAA/CAA.csproj)
✅ Critérios de aceitação: todos atendidos
```
