// CMS Gaolos

// ace editor #HTMLBody

var textareaHTMLBody = $('#HTMLBody');

var editorHTMLBody = ace.edit("HTMLBody");
editorHTMLBody.setTheme("ace/theme/monokai");
editorHTMLBody.getSession().setMode("ace/mode/html");
editorHTMLBody.getValue();

editorHTMLBody.getSession().on('change', function () {
    textareaHTMLBody.val(editorHTMLBody.getSession().getValue());
});

textareaHTMLBody.val(editorHTMLBody.getSession().getValue());

// Ordenar elementos

$(function () {
    $(".sortable").sortable();
    $(".sortable").disableSelection();
});

// Funciones card-cms-row : Clonar, Ver código, Opciones

$('.cms-cont-clone').click(function () {
    $(this).parents('.card-cms').clone('.card-cms').appendTo('.card-cms-main');
    console.log('yo te clono');
});

$('.cms-cont-del').click(function () {
    $(this).parents('.card-cms').remove();
});

$('.cms-cont-code').click(function () {

});

$('.cms-cont-options').click(function () {

});

// Funciones card-cms-row : Clonar, Ver código, Opciones

$('.cms-row-clone').click(function () {
    $(this).parents('.card-cms-row').clone('.card-cms-row').appendTo('.card-cms-content');
    console.log('yo te clono');
});

$('.cms-row-del').click(function () {
    $(this).parents('.card-cms-row').remove();
});

$('.cms-row-code').click(function () {

});

$('.cms-row-options').click(function () {

});

// Funciones añadir columnas

$(document.body).on('change', '#layout-01', function() {
    $(this).click(function () {
        console.log('he sido clicado');
    });
});

// Insertar columna

$('.cms-add-col').click(function () {
    console.log('inserto columna');
    var dialog = bootbox.dialog({
        message: '<p class="text-center">El nombre de usuario y la contraseña que ingresaste no coinciden con nuestros registros. Por favor, revisa e inténtalo de nuevo.</p>',
        closeButton: true,
        size: 'large'
    });
    dialog.init(function () {
        dialog.find('.bootbox-body').html("\
            <div class='container cms-layout'>\
            <div class='row'>\
            <div class='col-4 text-center'><a href='#' id='layout-01'><div class='row layout-row'><div class='col-12 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-6 layout-col'></div><div class='col-6 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-4 layout-col'></div><div class='col-4 layout-col'></div><div class='col-4 layout-col'></div></div></a></div>\
            </div>\
            <div class='row'>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-3 layout-col'></div><div class='col-3 layout-col'></div><div class='col-3 layout-col'></div><div class='col-3 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-9 layout-col'></div><div class='col-3 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-3 layout-col'></div><div class='col-9 layout-col'></div></div></a></div>\
            </div>\
            <div class='row'>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-2 layout-col'></div><div class='col-10 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-10 layout-col'></div><div class='col-2 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-6 layout-col'></div><div class='col-3 layout-col'></div><div class='col-3 layout-col'></div></div></a></div>\
            </div>\
            <div class='row'>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-3 layout-col'></div><div class='col-3 layout-col'></div><div class='col-6 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'><a href='#' id=''><div class='row layout-row'><div class='col-3 layout-col'></div><div class='col-6 layout-col'></div><div class='col-3 layout-col'></div></div></a></div>\
            <div class='col-4 text-center'></div>\
            </div>\
            </div>");
    });
});

// Guardar documento

$(document.body).on('click', '.save-html', function () {

    //var sHeader = $('#headerarea').data('contentbuilder').html(); // Obtenemos header
    var sContent = $('#contentarea').data('contentbuilder').html(); // Obtenemos content

    var id1 = document.getElementById("nodo_id_tv").value;
    var id2 = document.getElementById("nodo_id_nodo").value;
    var id3 = document.getElementById("ID_Idi").value;

    editorHTMLBody.setValue(textareaHTMLBody.val());

    //var id4 = $('#headerarea').data('contentbuilder').html(); // Obtenemos header
    var id5 = sContent;

    var id6 = document.getElementById("HTMLBody").value;

    //var dataJson = JSON.stringify({ ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, Html: id5 });
    //  console.log(id5);

    //var data = new FormData();
    //data.append("ID_Tv", id1);
    //data.append("ID_Nodo", id2);
    //data.append("ID_Idi", id3);
    //data.append("Html", id5);

    $.ajax(
        {
            type: "POST",
            url: "/cms/webnodoinformacionhtmlguardar",
            data: { ID_Tv: id1, ID_Nodo: id2, ID_Idi: id3, Html: id6 },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.eserror == true) {
                    $.jGrowl(response.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    $.jGrowl(response.mensaje, $optionsMessageOK);
                    return true;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
});


jQuery(document).ready(function ($) {
    
    $("#headerarea, #contentarea").contentbuilder({
        zoom: 0.85,
        snippetFile: '/lib/contentbuilder/snippets-bootstrap.html',
        snippetOpen: true,
        toolbar: 'top',
        iconselect: '/lib/contentbuilder/selecticon.html',
        axis: 'y'
    });
});