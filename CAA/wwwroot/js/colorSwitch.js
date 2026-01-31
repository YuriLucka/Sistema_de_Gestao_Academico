// Força sempre o tema claro
(() => {
    "use strict";
    document.documentElement.setAttribute("data-bs-theme", "light");
    // Remove qualquer tema salvo anteriormente
    localStorage.removeItem("theme");
})();