/*** add active class and stay opened when selected ***/
var url = window.location;

// for sidebar menu entirely but not cover treeview
$('ul.sidebar-menu a').filter(function () {
    return this.href == url;
}).addClass('active');

// for the treeview
$('ul.nav-treeview a').filter(function () {
    return this.href == url;
}).parentsUntil(".sidebar-menu > .nav-treeview").addClass('menu-open').prev('a').addClass('active');