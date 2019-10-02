// GAOLOS 2017 v1.0
// File: cursos.js
// Desc: Eventos para el módulo de cursos

$(".tag-del").click(function () {
    event.preventDefault(event);
    $(this).parent()
        .removeClass('badge-default')
        .addClass('badge-danger')
        .fadeOut();
});

/* Calendarios */

$(function () {
    $('#fechaIniPubli').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});

$(function () {
    $('#fechaFinPubli').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});

$(function () {
    $('#fechaIniIns').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});

$(function () {
    $('#fechaFinIns').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});


