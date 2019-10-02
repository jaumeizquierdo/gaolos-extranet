// GAOLOS 2017 v1.0

/*
Plugins para valorar:

    jQuery File Upload:
    https://github.com/blueimp/jQuery-File-Upload
    demo: https://blueimp.github.io/jQuery-File-Upload/
    File Upload widget with multiple file selection, drag&drop support, progress bar, validation and preview images, audio and video for jQuery. Supports cross-domain, chunked and resumable file uploads.
    Works with any server-side platform (Google App Engine, PHP, Python, Ruby on Rails, Java, etc.) that supports standard HTML form file uploads.
    
    jQuery Form
    https://github.com/jquery-form/form
    The jQuery Form Plugin allows you to easily and unobtrusively upgrade HTML forms to use AJAX. The main methods, ajaxForm and ajaxSubmit,
    gather information from the form element to determine how to manage the submit process. Both of these methods support numerous options which allows you to have full control over how the data is submitted.
    No special markup is needed, just a normal form. Submitting a form with AJAX doesn't get any easier than this!    

    https://www.psd2html.com/js-custom-forms/
    
*/

// Menu lateral. Evento lanzado cuando el menú ha sido abierto



// Dropdown: Añadimos fade al abrir menú

/*
$('.dropdown').on('show.bs.dropdown', function () {
    $(this).find('.dropdown-menu').first().stop(true, true).fadeIn();
});
*/
// Dropdown: Eliminamos fade al cerrar menú
/*
$('.dropdown').on('hide.bs.dropdown', function () {
    $(this).find('.dropdown-menu').first().stop(true, true).fadeOut();
});
*/

// Refrescar View Component desde botón

$("#cargar-menu").click(function () {
    //$("#menu-lateral").empty();
    var container = $("#MenuGaolosComponent");
    console.log("botón cargar menú clicado");
    $.get("/Home/MenuGaolosComponent", function (data) {
        container.html(data);
    });
    console.log(container);
});

$("#cargar-footer").click(function () {
    //$("#viewcomponent-wrapper").empty();
    var container = $("#MenuFooterComponent");
    console.log("botón cargar footer clicado");
    $.get("/Home/MenuFooterComponent", function (data) {
        container.html(data);
    });
    console.log(container);
});

$("#cargar-usuario").click(function () {
    //$("#viewcomponent-wrapper").empty();
    var container = $("#MenuUsuarioComponent");
    console.log("botón cargar usuario clicado");
    $.get("/Home/MenuUsuarioComponent", function (data) {
        container.html(data);
    });
    console.log(container);
});

$("#cargar-notificaciones").click(function () {
    //$("#viewcomponent-wrapper").empty();
    var container = $("#MenuNotificacionesComponent");
    console.log("botón cargar notificaciones clicado");
    $.get("/Home/MenuNotificacionesComponent", function (data) {
        container.html(data);
    });
    console.log(container);
});

// Refrescar View Component automáticamente con un delay

$("#cargar-footer-autorefresh").click(function () {
    var container = $("#viewcomponent-wrapper-refresh");
    var refreshComponent = function () {
        $.get("/Home/MenuFooterComponent", function (data) { container.html(data); });
    };

    $(function () { window.setInterval(refreshComponent, 3000); });
    console.log(container);
});