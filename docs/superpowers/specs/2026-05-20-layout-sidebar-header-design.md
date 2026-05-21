# Spec — Layout Moderno: Sidebar + Header

## Objetivo

Implementar um layout completo, moderno e elegante para o app autenticado do sistema CAA, com:
- Sidebar dark expandida (260px) que colapsa para mini (64px, só ícones) ao clicar no hambúrguer
- Header branco com borda inferior em gradiente verde e sombra verde sutil
- Sem footer
- CSS 100% customizado (sem AdminLTE ou outro framework de UI)
- Identidade visual consistente com a tela de login já implementada

## Contexto

A estrutura HTML já foi limpa em sessão anterior: partials sem CSS inline, HTML semântico com IDs `#sidebar`, `#main-wrapper`, `#topbar`, `#page-content`. O `site.css` contém apenas tokens de cor e skeleton estrutural. AdminLTE foi removido completamente. Agora o objetivo é estilizar do zero com design profissional.

## Área do sistema

UI / Layout

## Layout afetado

App autenticado — `Views/Shared/AdminLTE/_Layout.cshtml` e suas partials

## Arquivos a criar ou modificar

| Arquivo | Tipo de mudança |
|---|---|
| `CAA/wwwroot/css/site.css` | Reescrever — todo CSS do layout |
| `CAA/Views/Shared/AdminLTE/_MainNavigation.cshtml` | HTML da sidebar + tooltip no mini |
| `CAA/Views/Shared/AdminLTE/_TopNavigation.cshtml` | HTML do header + breadcrumb |
| `CAA/Views/Shared/AdminLTE/_Layout.cshtml` | Classe `sidebar-expanded` no `<body>` |
| `CAA/Views/Shared/AdminLTE/_Scripts.cshtml` | JS do toggle mini/expand e acordeão |

## Solução proposta

### Estrutura HTML

```
<body class="sidebar-expanded">
  <aside id="sidebar">
    <div id="sidebar-brand">          ← logo expandido / ícone mini
    <nav id="sidebar-nav">
      <ul class="sidebar-menu">
        <li class="has-submenu">
          <a class="menu-parent" title="Seção"> ← title = tooltip no mini
            <i class="menu-icon bi bi-..."></i>
            <span class="menu-label">Texto</span>
            <i class="menu-arrow bi bi-chevron-right"></i>
          </a>
          <ul class="submenu">
            <li><a>Item</a></li>
          </ul>
        </li>
      </ul>
  </aside>
  <div id="main-wrapper">
    <header id="topbar">
      <div id="topbar-left">
        <button data-toggle="sidebar">   ← hambúrguer
        <nav id="breadcrumb">            ← Home / Página atual
      </div>
      <div id="topbar-right">
        <button data-toggle="fullscreen">
        UserProfile ViewComponent
      </div>
    </header>
    <main id="page-content">
      <div class="container-fluid">@RenderBody()</div>
    </main>
  </div>
</body>
```

### CSS — Sidebar

**Tokens usados:** `--green: #25D366`, `--green-dark: #128C7E`, `--green-darker: #064e44`

**Layout:**
- `position: fixed; top: 0; left: 0; height: 100vh; z-index: 100`
- Expandida: `width: 260px` | Mini: `width: 64px`
- `transition: width 0.3s ease`
- Background: `linear-gradient(180deg, #050f0a 0%, #071a14 60%, #050f0a 100%)`
- Borda direita: `1px solid rgba(37,211,102,0.15)`
- Glow decorativo: `::after` com `radial-gradient` verde, `pointer-events: none`

**Brand:**
- Expandida: logo PNG `max-height: 44px`
- Mini: quadrado verde 36×36 com "A" branco
- `overflow: hidden` no container para ocultar texto no mini

**Itens de menu:**
- Normal: `color: rgba(255,255,255,0.5)`, `padding: 10px 16px`, `border-radius: 8px`
- Hover: `rgba(255,255,255,0.85)` + `background: rgba(37,211,102,0.08)` + `translateX(2px)`
- Ativo: `background: rgba(37,211,102,0.15)` + `border-left: 3px solid #25D366` + `color: #fff`
- Ícone: `color: #25D366; font-size: 1rem`

**Estado mini — texto oculto via:**
```css
body:not(.sidebar-expanded) .menu-label,
body:not(.sidebar-expanded) .menu-arrow,
body:not(.sidebar-expanded) .sidebar-section-label { display: none; }
body:not(.sidebar-expanded) #sidebar-brand .brand-full { display: none; }
body:not(.sidebar-expanded) #sidebar-brand .brand-mini { display: flex; }
```
- Tooltip nativo via `title` no `<a>`

**Submenus:**
- Animação via `max-height: 0 → max-height: 500px` + `overflow: hidden` + `transition: max-height 0.3s ease`
- Linha vertical: `border-left: 2px solid rgba(37,211,102,0.2)`, `margin-left: 28px`
- Seta: `rotate(90deg)` quando pai tem `.open`
- Mini: submenus desabilitados — clique no item pai abre a rota direta (se houver) ou faz nada

**Scrollbar:**
```css
#sidebar::-webkit-scrollbar { width: 4px; }
#sidebar::-webkit-scrollbar-thumb { background: rgba(37,211,102,0.3); border-radius: 2px; }
```

### CSS — Main Wrapper e Header

**Main wrapper:**
- `margin-left: 260px` quando `.sidebar-expanded`
- `margin-left: 64px` quando não `.sidebar-expanded`
- `transition: margin-left 0.3s ease`

**Header:**
- `height: 56px; position: sticky; top: 0; z-index: 90`
- `background: #ffffff`
- `border-bottom: 2px solid transparent`
- `border-image: linear-gradient(90deg, #25D366, #128C7E) 1`
- `box-shadow: 0 2px 12px rgba(37,211,102,0.1)`

**Hambúrguer:**
- `width: 36px; height: 36px; border-radius: 8px; border: 1px solid #e5e7eb`
- Hover: `background: rgba(37,211,102,0.08); border-color: rgba(37,211,102,0.3); color: #128C7E`

**Breadcrumb:**
- `font-size: 0.78rem; color: #9ca3af`
- Item atual: `color: #1a2332; font-weight: 600`
- Separador: `bi-chevron-right` tamanho 0.6rem

**UserProfile:**
- Avatar circular com sombra verde: `box-shadow: 0 2px 8px rgba(37,211,102,0.2)`
- Nome + cargo em `font-size: 0.78rem`

### CSS — Mobile (<992px)

```css
@media (max-width: 992px) {
  #sidebar { transform: translateX(-260px); width: 260px; }
  #main-wrapper { margin-left: 0; }
  body.sidebar-open #sidebar { transform: translateX(0); }
  body.sidebar-open::after { /* backdrop */ }
}
```

### JS — Comportamento

**Toggle sidebar (desktop):**
```js
// Alterna .sidebar-expanded no body
// Salva estado em localStorage('caa-sidebar')
// Lê localStorage no DOMContentLoaded para restaurar
```

**Toggle sidebar (mobile):**
```js
// Alterna .sidebar-open no body
// Clique no backdrop remove .sidebar-open
```

**Acordeão submenu:**
```js
// Clique em .menu-parent → toggle .open no .has-submenu pai
// Fecha outros abertos (accordion)
// No estado mini: não abre submenu (deixa browser navegar pelo title/href)
```

**Auto-active:**
```js
// Compara href dos links com window.location.href
// Marca .active + abre pai se em submenu
```

**Breadcrumb:**
- Gerado dinamicamente via JS lendo o link ativo do sidebar

## Critérios de aceitação

- [ ] Sidebar dark (260px) com logo, seções, itens com ícone + texto e seta
- [ ] Sidebar colapsa para mini (64px, só ícones) ao clicar hambúrguer — animação suave
- [ ] Estado mini/expandido persiste após reload (localStorage)
- [ ] Submenus abrem/fecham com animação `max-height` — um por vez (accordion)
- [ ] Item ativo destacado (borda esquerda verde + fundo) ao carregar página
- [ ] Submenu pai aberto automaticamente quando subitem está ativo
- [ ] Header branco com borda inferior gradiente verde + sombra verde
- [ ] Breadcrumb mostra `Home / Seção atual` baseado no item ativo
- [ ] Mobile: sidebar some, abre como overlay com backdrop ao clicar hambúrguer
- [ ] `dotnet build CAA/CAA.csproj` — zero erros
- [ ] Nenhuma view existente quebra com o novo layout

## Fora do escopo

- Redesign das views internas (cards, tabelas, formulários)
- Dark mode para o conteúdo principal
- Animações complexas além das especificadas
- Notificações / badges no sidebar
- Menu de contexto no mini sidebar (fly-out panels)
