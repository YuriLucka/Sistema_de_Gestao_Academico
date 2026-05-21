# Layout Moderno — Sidebar + Header Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Implementar sidebar dark expandida/mini + header branco com accent verde, CSS 100% custom, sem AdminLTE.

**Architecture:** CSS em `site.css` controla toda a aparência via classe `sidebar-expanded` no `<body>`. JS em `_Scripts.cshtml` gerencia toggle, acordeão, auto-active e breadcrumb. HTMLs das partials recebem os atributos e elementos necessários para os seletores CSS/JS funcionarem.

**Tech Stack:** CSS custom (sem framework UI), Bootstrap 5 (grid/utilities apenas), Bootstrap Icons CDN, vanilla JS, ASP.NET Core 9 Razor.

**Build:** `dotnet build CAA/CAA.csproj` — deve retornar zero erros após cada task.

---

## File Map

| Arquivo | Responsabilidade |
|---|---|
| `CAA/wwwroot/css/site.css` | Tokens, layout skeleton, sidebar, header, mobile |
| `CAA/Views/Shared/AdminLTE/_MainNavigation.cshtml` | HTML da sidebar: brand-mini, title tooltips, menu-label, section labels |
| `CAA/Views/Shared/AdminLTE/_TopNavigation.cshtml` | HTML do header: breadcrumb nav |
| `CAA/Views/Shared/AdminLTE/_Layout.cshtml` | Classe `sidebar-expanded` no `<body>` |
| `CAA/Views/Shared/AdminLTE/_Scripts.cshtml` | Todo o JS: toggle, acordeão, active, breadcrumb, localStorage |

---

## Task 1: CSS — site.css completo

**Files:**
- Rewrite: `CAA/wwwroot/css/site.css`

- [ ] **Substituir todo o conteúdo de `site.css` pelo seguinte:**

```css
/* ═══════════════════════════════════════════════════════
   CAA — Design Tokens
═══════════════════════════════════════════════════════ */
:root {
    --green:          #25D366;
    --green-dark:     #128C7E;
    --green-darker:   #064e44;
    --sidebar-width:  260px;
    --sidebar-mini:    64px;
    --topbar-height:   56px;
    --transition:      0.3s ease;
}

/* ═══════════════════════════════════════════════════════
   Base
═══════════════════════════════════════════════════════ */
html, body { height: 100%; }
body {
    font-family: 'Inter', 'Segoe UI', system-ui, -apple-system, sans-serif;
    background: #f4f6f8;
    overflow-x: hidden;
}

/* ═══════════════════════════════════════════════════════
   Sidebar — container
═══════════════════════════════════════════════════════ */
#sidebar {
    position: fixed;
    top: 0; left: 0;
    height: 100vh;
    width: var(--sidebar-width);
    background: linear-gradient(180deg, #050f0a 0%, #071a14 60%, #050f0a 100%);
    border-right: 1px solid rgba(37, 211, 102, .15);
    overflow-y: auto;
    overflow-x: hidden;
    z-index: 100;
    transition: width var(--transition);
    display: flex;
    flex-direction: column;
}

/* decorative glow */
#sidebar::after {
    content: '';
    position: absolute;
    top: 0; right: 0; bottom: 0;
    width: 60%;
    background: radial-gradient(ellipse at 100% 25%, rgba(37,211,102,.07) 0%, transparent 65%);
    pointer-events: none;
}

/* scrollbar */
#sidebar::-webkit-scrollbar { width: 4px; }
#sidebar::-webkit-scrollbar-thumb { background: rgba(37,211,102,.3); border-radius: 2px; }
#sidebar::-webkit-scrollbar-track { background: transparent; }

/* ═══════════════════════════════════════════════════════
   Sidebar — brand
═══════════════════════════════════════════════════════ */
#sidebar-brand {
    padding: 18px 14px;
    border-bottom: 1px solid rgba(37,211,102,.12);
    display: flex;
    align-items: center;
    justify-content: center;
    flex-shrink: 0;
    min-height: 72px;
    overflow: hidden;
}

.brand-full {
    display: flex;
    align-items: center;
    text-decoration: none;
    transition: opacity var(--transition);
}
.brand-full img {
    max-height: 42px;
    max-width: 200px;
    object-fit: contain;
}

.brand-mini {
    display: none;
    width: 36px; height: 36px;
    background: linear-gradient(135deg, var(--green), var(--green-dark));
    border-radius: 8px;
    align-items: center;
    justify-content: center;
    font-weight: 700;
    font-size: 1rem;
    color: #fff;
    text-decoration: none;
    box-shadow: 0 2px 10px rgba(37,211,102,.3);
    flex-shrink: 0;
}

/* ═══════════════════════════════════════════════════════
   Sidebar — nav
═══════════════════════════════════════════════════════ */
#sidebar-nav { flex: 1; padding: 10px 0; }

.sidebar-menu {
    list-style: none;
    padding: 0 8px;
    margin: 0;
    display: flex;
    flex-direction: column;
    gap: 1px;
}

/* Section labels */
.sidebar-section-label {
    font-size: .6rem;
    font-weight: 600;
    text-transform: uppercase;
    letter-spacing: .08em;
    color: rgba(255,255,255,.22);
    padding: 14px 8px 4px;
    white-space: nowrap;
    overflow: hidden;
}

/* All nav links */
.sidebar-menu a,
.sidebar-menu .menu-parent {
    display: flex;
    align-items: center;
    gap: 10px;
    padding: 10px 8px;
    border-radius: 8px;
    font-size: .875rem;
    font-weight: 500;
    color: rgba(255,255,255,.5);
    text-decoration: none;
    cursor: pointer;
    transition: all .2s ease;
    white-space: nowrap;
    overflow: hidden;
    background: none;
    border: none;
    width: 100%;
    text-align: left;
    font-family: inherit;
}

.sidebar-menu a:hover,
.sidebar-menu .menu-parent:hover {
    color: rgba(255,255,255,.9);
    background: rgba(37,211,102,.08);
    transform: translateX(2px);
}

/* Active direct link */
.sidebar-menu li:not(.has-submenu) > a.active {
    color: #fff;
    background: rgba(37,211,102,.15);
    border-left: 3px solid var(--green);
    padding-left: 5px;
}

/* Open parent style */
.has-submenu.open > .menu-parent {
    color: rgba(255,255,255,.85);
}

/* Icons */
.menu-icon {
    font-size: 1rem;
    color: var(--green);
    flex-shrink: 0;
    width: 20px;
    text-align: center;
    transition: transform .2s;
}
.sidebar-menu a:hover .menu-icon,
.sidebar-menu .menu-parent:hover .menu-icon { transform: scale(1.1); }

/* Label */
.menu-label {
    flex: 1;
    overflow: hidden;
    text-overflow: ellipsis;
}

/* Arrow */
.menu-arrow {
    font-size: .7rem;
    color: rgba(255,255,255,.25);
    transition: transform .25s ease;
    flex-shrink: 0;
}
.has-submenu.open > .menu-parent .menu-arrow {
    transform: rotate(90deg);
    color: var(--green);
}

/* ═══════════════════════════════════════════════════════
   Sidebar — submenus
═══════════════════════════════════════════════════════ */
.submenu {
    list-style: none;
    padding: 2px 0 2px 28px;
    margin: 0 0 0 16px;
    max-height: 0;
    overflow: hidden;
    transition: max-height .3s ease;
    border-left: 2px solid rgba(37,211,102,.2);
}
.has-submenu.open > .submenu { max-height: 500px; }

.submenu li > a {
    display: flex;
    align-items: center;
    gap: 8px;
    padding: 7px 8px;
    border-radius: 6px;
    font-size: .82rem;
    font-weight: 400;
    color: rgba(255,255,255,.42);
    transform: none;
}
.submenu li > a::before {
    content: '';
    width: 5px; height: 5px;
    border-radius: 50%;
    background: rgba(37,211,102,.35);
    flex-shrink: 0;
    transition: background .2s;
}
.submenu li > a:hover { color: rgba(255,255,255,.88); }
.submenu li > a:hover::before,
.submenu li > a.active::before { background: var(--green); }
.submenu li > a.active { color: #fff; font-weight: 600; }

/* ═══════════════════════════════════════════════════════
   Sidebar — MINI state  (body without .sidebar-expanded)
═══════════════════════════════════════════════════════ */
body:not(.sidebar-expanded) #sidebar { width: var(--sidebar-mini); }

body:not(.sidebar-expanded) .brand-full { display: none !important; }
body:not(.sidebar-expanded) .brand-mini { display: flex !important; }

body:not(.sidebar-expanded) .sidebar-section-label,
body:not(.sidebar-expanded) .menu-label,
body:not(.sidebar-expanded) .menu-arrow { display: none; }

body:not(.sidebar-expanded) .sidebar-menu { padding: 0 6px; }

body:not(.sidebar-expanded) .sidebar-menu a,
body:not(.sidebar-expanded) .sidebar-menu .menu-parent {
    justify-content: center;
    padding: 10px;
    gap: 0;
}

body:not(.sidebar-expanded) .sidebar-menu li:not(.has-submenu) > a.active {
    border-left: none;
    padding-left: 10px;
    border-radius: 8px;
}

body:not(.sidebar-expanded) .submenu { max-height: 0 !important; }

/* ═══════════════════════════════════════════════════════
   Main wrapper
═══════════════════════════════════════════════════════ */
#main-wrapper {
    margin-left: var(--sidebar-mini);
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    transition: margin-left var(--transition);
}
body.sidebar-expanded #main-wrapper { margin-left: var(--sidebar-width); }

/* ═══════════════════════════════════════════════════════
   Topbar
═══════════════════════════════════════════════════════ */
#topbar {
    height: var(--topbar-height);
    position: sticky;
    top: 0;
    z-index: 90;
    background: #fff;
    border-bottom: 2px solid transparent;
    border-image: linear-gradient(90deg, var(--green), var(--green-dark)) 1;
    box-shadow: 0 2px 12px rgba(37,211,102,.1);
    display: flex;
    align-items: center;
    padding: 0 20px;
    gap: 12px;
    flex-shrink: 0;
}

#topbar-left {
    display: flex;
    align-items: center;
    gap: 12px;
    flex: 1;
    min-width: 0;
}

#topbar-right {
    display: flex;
    align-items: center;
    gap: 8px;
    flex-shrink: 0;
}

/* Hamburger */
#sidebar-toggle-btn {
    width: 36px; height: 36px;
    border-radius: 8px;
    border: 1px solid #e5e7eb;
    background: #f9fafb;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #6b7280;
    font-size: 1.15rem;
    cursor: pointer;
    transition: all .2s;
    flex-shrink: 0;
    padding: 0;
}
#sidebar-toggle-btn:hover {
    background: rgba(37,211,102,.08);
    border-color: rgba(37,211,102,.3);
    color: var(--green-dark);
}

/* Breadcrumb */
#topbar-breadcrumb {
    display: flex;
    align-items: center;
    gap: 6px;
    font-size: .78rem;
    overflow: hidden;
}
#topbar-breadcrumb .bc-home {
    color: #9ca3af;
    text-decoration: none;
    display: flex;
    align-items: center;
    gap: 4px;
    white-space: nowrap;
    transition: color .2s;
}
#topbar-breadcrumb .bc-home:hover { color: var(--green-dark); }
#topbar-breadcrumb .bc-sep {
    color: #d1d5db;
    font-size: .62rem;
    flex-shrink: 0;
}
#topbar-breadcrumb .bc-section {
    color: #9ca3af;
    white-space: nowrap;
}
#topbar-breadcrumb .bc-current {
    font-weight: 600;
    color: #1a2332;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

/* Fullscreen button */
#fullscreen-btn {
    width: 32px; height: 32px;
    border-radius: 7px;
    border: 1px solid #e5e7eb;
    background: transparent;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #9ca3af;
    font-size: .85rem;
    cursor: pointer;
    transition: all .2s;
    padding: 0;
}
#fullscreen-btn:hover { background: #f4f6f8; color: #374151; }

/* ═══════════════════════════════════════════════════════
   Page content
═══════════════════════════════════════════════════════ */
#page-content { flex: 1; padding: 1.5rem; }

/* Compatibilidade com views existentes que usam .content-header */
.content-header { padding: 0 0 1rem 0; }
.content-header h1,
.content-header h2,
.content-header h3 {
    margin: 0;
    font-weight: 700;
    color: #1a2332;
    font-size: 1.25rem;
}

/* ═══════════════════════════════════════════════════════
   Utilities
═══════════════════════════════════════════════════════ */
.btn-caa,
.btn-caa:visited { background: var(--green) !important; color: #fff !important; border: none !important; }
.btn-caa:hover,
.btn-caa:focus,
.btn-caa:active { background: var(--green-dark) !important; color: #fff !important; }
.btn-caa:disabled { opacity: .65; pointer-events: none; }

.btn-outline-caa { color: var(--green) !important; background: transparent !important; border: 1px solid var(--green) !important; transition: all .2s; }
.btn-outline-caa:hover,
.btn-outline-caa:focus,
.btn-outline-caa:active { color: #fff !important; background: var(--green) !important; }

.input-caa { border: 2px solid var(--green); border-radius: .5rem; transition: border-color .2s; }
.input-caa:focus { border-color: var(--green-dark); box-shadow: 0 0 0 .2rem rgba(18,140,126,.15); }

.text-caa { color: var(--green) !important; }
.bg-caa-green { background: var(--green) !important; color: #fff !important; }
.bg-caa-green-gradient { background: linear-gradient(90deg, var(--green), var(--green-dark)); color: #fff !important; }
.card-caa-border { border: 2px solid var(--green) !important; }
.no-wrap { white-space: nowrap; }

.form-check-input[type="checkbox"] {
    width: 1.2em; height: 1.2em;
    border: 2px solid var(--green);
    border-radius: .25em;
}
.form-check-input[type="checkbox"]:checked { background-color: var(--green); border-color: var(--green); }
.form-check-input[type="checkbox"]:focus { box-shadow: 0 0 0 .2rem rgba(18,140,126,.25); border-color: var(--green-dark); }

/* ═══════════════════════════════════════════════════════
   Mobile  (≤ 992px)
═══════════════════════════════════════════════════════ */
@media (max-width: 992px) {
    #sidebar {
        width: var(--sidebar-width) !important;
        transform: translateX(-100%);
        transition: transform var(--transition);
    }

    body.sidebar-expanded #sidebar { transform: translateX(0); }

    #main-wrapper { margin-left: 0 !important; }

    /* Backdrop */
    body.sidebar-expanded::before {
        content: '';
        position: fixed;
        inset: 0;
        background: rgba(0,0,0,.45);
        z-index: 99;
    }
}
```

- [ ] **Verificar build**

```powershell
dotnet build CAA/CAA.csproj --no-restore 2>&1 | Select-String "error\s"
```

Saída esperada: nenhuma linha com "error" (warnings são normais).

- [ ] **Commit**

```powershell
git add CAA/wwwroot/css/site.css
git commit -m "feat(layout): adicionar CSS completo sidebar + header"
```

---

## Task 2: HTML — _MainNavigation.cshtml

**Files:**
- Rewrite: `CAA/Views/Shared/AdminLTE/_MainNavigation.cshtml`

**Mudanças necessárias em relação ao atual:**
1. `#sidebar-brand` recebe `.brand-full` e `.brand-mini`
2. `<span>` nos menu-parents vira `<span class="menu-label">`
3. Cada `.menu-parent` recebe `title="Nome da seção"` (tooltip no modo mini)
4. `<li class="sidebar-section-label">` adicionados antes de cada grupo

- [ ] **Substituir todo o conteúdo de `_MainNavigation.cshtml`:**

```html
<aside id="sidebar">
    <div id="sidebar-brand">
        <a href="/" class="brand-full">
            <img src="/img/marca_uam_campus_athon_branca_horizontal.png" alt="CAA" />
        </a>
        <a href="/" class="brand-mini" title="CAA Acadêmico">A</a>
    </div>
    <nav id="sidebar-nav">
        <ul class="sidebar-menu">

            @if (User.IsInRole("Colaboradores") || User.IsInRole("Parametros") || User.IsInRole("Admin"))
            {
                <li class="sidebar-section-label">Gestão</li>
                <li class="has-submenu">
                    <a href="#" class="menu-parent" title="Administração">
                        <i class="bi bi-key menu-icon"></i>
                        <span class="menu-label">Administração</span>
                        <i class="bi bi-chevron-right menu-arrow"></i>
                    </a>
                    <ul class="submenu">
                        @if (User.IsInRole("Colaboradores") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Colaborador" asp-action="Index">Colaboradores</a></li>
                        }
                        @if (User.IsInRole("Parametros") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Parametros" asp-action="Index">Parâmetros</a></li>
                        }
                    </ul>
                </li>
            }

            @if (User.IsInRole("Central de Contatos") || User.IsInRole("Fichas Médicas") || User.IsInRole("Recados") || User.IsInRole("Admin"))
            {
                <li class="sidebar-section-label">Atendimento</li>
                <li class="has-submenu">
                    <a href="#" class="menu-parent" title="Atendimento">
                        <i class="bi bi-people menu-icon"></i>
                        <span class="menu-label">Atendimento</span>
                        <i class="bi bi-chevron-right menu-arrow"></i>
                    </a>
                    <ul class="submenu">
                        @if (User.IsInRole("Recados") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Chat" asp-action="Index">Chat</a></li>
                        }
                        @if (User.IsInRole("Fichas Médicas") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="FichaMedica" asp-action="Index">Fichas Médicas</a></li>
                        }
                        @if (User.IsInRole("Central de Contatos") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Contato" asp-action="Index">Contatos</a></li>
                        }
                    </ul>
                </li>
            }

            @if (User.IsInRole("Cursos") || User.IsInRole("Matrículas") || User.IsInRole("Estágios") || User.IsInRole("Documentos Institucionais") || User.IsInRole("Admin"))
            {
                <li class="sidebar-section-label">Acadêmico</li>
                <li class="has-submenu">
                    <a href="#" class="menu-parent" title="Acadêmico">
                        <i class="bi bi-journal-bookmark menu-icon"></i>
                        <span class="menu-label">Acadêmico</span>
                        <i class="bi bi-chevron-right menu-arrow"></i>
                    </a>
                    <ul class="submenu">
                        @if (User.IsInRole("Cursos") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Curso" asp-action="Index">Cursos</a></li>
                        }
                        @if (User.IsInRole("Matrículas") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Matricula" asp-action="Index">Matrículas</a></li>
                        }
                        @if (User.IsInRole("Estágios") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Estagio" asp-action="Index">Estágios</a></li>
                        }
                        @if (User.IsInRole("Documentos Institucionais") || User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="Documento" asp-action="Index">Documentos</a></li>
                        }
                    </ul>
                </li>
            }

            @if (User.IsInRole("ProUni") || User.IsInRole("Admin"))
            {
                <li class="sidebar-section-label">Programas</li>
                <li class="has-submenu">
                    <a href="#" class="menu-parent" title="ProUni">
                        <i class="bi bi-mortarboard menu-icon"></i>
                        <span class="menu-label">ProUni</span>
                        <i class="bi bi-chevron-right menu-arrow"></i>
                    </a>
                    <ul class="submenu">
                        <li><a asp-controller="Prouni" asp-action="Analise">Análise de Documentos</a></li>
                        @if (User.IsInRole("Admin"))
                        {
                            <li><a asp-controller="ProuniCampo" asp-action="Index">Campos de Documentos</a></li>
                        }
                    </ul>
                </li>
            }

            @if (User.IsInRole("Links Úteis") || User.IsInRole("Admin"))
            {
                <li class="sidebar-section-label">Outros</li>
                <li>
                    <a asp-controller="Link" asp-action="Index" title="Links Úteis">
                        <i class="bi bi-link-45deg menu-icon"></i>
                        <span class="menu-label">Links Úteis</span>
                    </a>
                </li>
            }

        </ul>
    </nav>
</aside>
```

- [ ] **Verificar build**

```powershell
dotnet build CAA/CAA.csproj --no-restore 2>&1 | Select-String "error\s"
```

Saída esperada: nenhuma linha com "error".

- [ ] **Commit**

```powershell
git add "CAA/Views/Shared/AdminLTE/_MainNavigation.cshtml"
git commit -m "feat(layout): atualizar HTML sidebar com brand-mini, menu-label e section labels"
```

---

## Task 3: HTML — _TopNavigation.cshtml + _Layout.cshtml

**Files:**
- Rewrite: `CAA/Views/Shared/AdminLTE/_TopNavigation.cshtml`
- Modify: `CAA/Views/Shared/AdminLTE/_Layout.cshtml` (adicionar `sidebar-expanded` no `<body>`)

- [ ] **Substituir todo o conteúdo de `_TopNavigation.cshtml`:**

```html
<header id="topbar">
    <div id="topbar-left">
        <button type="button" id="sidebar-toggle-btn" data-toggle="sidebar" aria-label="Toggle menu">
            <i class="bi bi-list"></i>
        </button>
        <nav id="topbar-breadcrumb" aria-label="Breadcrumb">
            <a href="/" class="bc-home">
                <i class="bi bi-house-door"></i> Home
            </a>
        </nav>
    </div>
    <div id="topbar-right">
        <button type="button" id="fullscreen-btn" data-toggle="fullscreen" aria-label="Fullscreen">
            <i class="bi bi-arrows-fullscreen"></i>
        </button>
        @await Component.InvokeAsync("UserProfile")
    </div>
</header>
```

- [ ] **Em `_Layout.cshtml`, alterar a tag `<body>` de:**

```html
<body>
```

**Para:**

```html
<body class="sidebar-expanded">
```

- [ ] **Verificar build**

```powershell
dotnet build CAA/CAA.csproj --no-restore 2>&1 | Select-String "error\s"
```

Saída esperada: nenhuma linha com "error".

- [ ] **Commit**

```powershell
git add "CAA/Views/Shared/AdminLTE/_TopNavigation.cshtml" "CAA/Views/Shared/AdminLTE/_Layout.cshtml"
git commit -m "feat(layout): atualizar HTML header com breadcrumb e body class sidebar-expanded"
```

---

## Task 4: JS — _Scripts.cshtml completo

**Files:**
- Rewrite: `CAA/Views/Shared/AdminLTE/_Scripts.cshtml`

O script gerencia: restauração de estado (localStorage), toggle sidebar, acordeão de submenus, marcação de link ativo, abertura automática do pai ativo, breadcrumb dinâmico e fullscreen.

- [ ] **Substituir todo o conteúdo de `_Scripts.cshtml`:**

```html
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>

<script>
(function () {
    'use strict';

    var STORAGE_KEY = 'caa-sidebar';

    function isMobile() { return window.innerWidth <= 992; }

    /* ── Restaurar estado salvo (antes do DOMContentLoaded para evitar flash) ── */
    if (!isMobile()) {
        if (localStorage.getItem(STORAGE_KEY) === 'mini') {
            document.body.classList.remove('sidebar-expanded');
        }
    } else {
        document.body.classList.remove('sidebar-expanded');
    }

    /* ── DOMContentLoaded ────────────────────────────────────────────────────── */
    document.addEventListener('DOMContentLoaded', function () {

        /* Marcar link ativo */
        var url = window.location.href;

        document.querySelectorAll('#sidebar a:not(.menu-parent):not(.brand-full):not(.brand-mini)').forEach(function (a) {
            if (a.href === url) {
                a.classList.add('active');
            }
        });

        /* Abrir submenu pai do item ativo */
        var activeSubLink = document.querySelector('#sidebar .submenu a.active');
        if (activeSubLink) {
            var parentItem = activeSubLink.closest('.has-submenu');
            if (parentItem) parentItem.classList.add('open');
        }

        /* Breadcrumb */
        buildBreadcrumb();

        /* Resize: ajustar estado no redimensionamento */
        window.addEventListener('resize', function () {
            if (isMobile()) {
                document.body.classList.remove('sidebar-expanded');
            }
        });
    });

    /* ── Toggle sidebar ─────────────────────────────────────────────────────── */
    document.querySelectorAll('[data-toggle="sidebar"]').forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            document.body.classList.toggle('sidebar-expanded');
            if (!isMobile()) {
                localStorage.setItem(STORAGE_KEY,
                    document.body.classList.contains('sidebar-expanded') ? 'expanded' : 'mini'
                );
            }
        });
    });

    /* Fechar sidebar no mobile ao clicar no backdrop */
    document.addEventListener('click', function (e) {
        if (isMobile() && document.body.classList.contains('sidebar-expanded')) {
            var sidebar = document.getElementById('sidebar');
            if (sidebar && !sidebar.contains(e.target) && !e.target.closest('[data-toggle="sidebar"]')) {
                document.body.classList.remove('sidebar-expanded');
            }
        }
    });

    /* ── Acordeão de submenus ───────────────────────────────────────────────── */
    document.querySelectorAll('.menu-parent').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();

            /* No estado mini (desktop), não abrir submenus */
            if (!isMobile() && !document.body.classList.contains('sidebar-expanded')) return;

            var item = this.closest('.has-submenu');
            var isOpen = item.classList.contains('open');

            /* Fechar todos */
            document.querySelectorAll('.has-submenu.open').forEach(function (li) {
                li.classList.remove('open');
            });

            if (!isOpen) item.classList.add('open');
        });
    });

    /* ── Breadcrumb dinâmico ────────────────────────────────────────────────── */
    function buildBreadcrumb() {
        var bc = document.getElementById('topbar-breadcrumb');
        if (!bc) return;

        var activeLink = document.querySelector('#sidebar a.active:not(.menu-parent):not(.brand-full):not(.brand-mini)');
        var sectionName = '';
        var pageName = '';

        if (activeLink) {
            pageName = activeLink.textContent.trim();

            var submenu = activeLink.closest('.submenu');
            if (submenu) {
                var parentItem = submenu.closest('.has-submenu');
                if (parentItem) {
                    var parentLabel = parentItem.querySelector('.menu-label');
                    sectionName = parentLabel ? parentLabel.textContent.trim() : '';
                }
            }
        }

        var html = '<a href="/" class="bc-home"><i class="bi bi-house-door"></i> Home</a>';

        if (sectionName) {
            html += '<i class="bi bi-chevron-right bc-sep"></i>';
            html += '<span class="bc-section">' + sectionName + '</span>';
        }

        if (pageName && pageName !== 'Home') {
            html += '<i class="bi bi-chevron-right bc-sep"></i>';
            html += '<span class="bc-current">' + pageName + '</span>';
        }

        bc.innerHTML = html;
    }

    /* ── Fullscreen ─────────────────────────────────────────────────────────── */
    document.querySelectorAll('[data-toggle="fullscreen"]').forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            var icon = this.querySelector('i');
            if (!document.fullscreenElement) {
                document.documentElement.requestFullscreen();
                if (icon) icon.className = 'bi bi-fullscreen-exit';
            } else {
                document.exitFullscreen();
                if (icon) icon.className = 'bi bi-arrows-fullscreen';
            }
        });
    });

})();
</script>
```

- [ ] **Verificar build**

```powershell
dotnet build CAA/CAA.csproj --no-restore 2>&1 | Select-String "error\s"
```

Saída esperada: nenhuma linha com "error".

- [ ] **Commit**

```powershell
git add "CAA/Views/Shared/AdminLTE/_Scripts.cshtml"
git commit -m "feat(layout): implementar JS sidebar toggle, acordeão, active link e breadcrumb"
```

---

## Task 5: Verificação final

**Files:** nenhum arquivo adicional.

- [ ] **Build limpo**

```powershell
dotnet build CAA/CAA.csproj 2>&1 | Select-String "Build succeeded|FAILED|error\s"
```

Saída esperada: `Build succeeded`.

- [ ] **Checklist visual — abrir o app no browser e verificar cada item:**

| Item | Como verificar |
|---|---|
| Sidebar dark 260px com logo, seções, itens | Qualquer página autenticada |
| Colapsa para mini 64px ao clicar ☰ | Clicar no botão hambúrguer |
| Animação suave na transição | Observar `width` e `margin-left` animando |
| Estado mini persiste após reload | Colapsar → F5 → deve continuar mini |
| Submenu abre com animação `max-height` | Clicar em "Administração" / "Atendimento" |
| Apenas um submenu aberto por vez | Abrir dois em sequência |
| Item ativo destacado (borda esquerda verde) | Navegar para qualquer página |
| Submenu pai auto-abre quando subitem ativo | Navegar para Colaboradores, recarregar |
| Header branco + borda gradiente verde | Visível em todas as páginas |
| Breadcrumb mostra `Home / Seção / Página` | Navegar para qualquer subitem |
| Mobile: sidebar overlay com backdrop | Redimensionar janela < 992px → clicar ☰ |
| Views existentes não quebram | Navegar para Matrículas, Estágios, ProUni |

- [ ] **Commit final se houver ajustes**

```powershell
git add -A
git commit -m "feat(layout): ajustes pós-verificação visual"
```
