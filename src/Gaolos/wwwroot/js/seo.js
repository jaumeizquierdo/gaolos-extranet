// GAOLOS 2017 v1.0
// File: Seo.js
// Desc: Eventos para el módulo de SEO

// Habilitamos datepicker el buscador de la sección SEO

$(function () {
    $('#seo-desde input').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});

$(function () {
    $('#seo-hasta input').datepicker({
        format: 'dd/mm/yy',
        language: 'es',
        autoclose: 'true',
        todayHighlight: 'true'
    });
});