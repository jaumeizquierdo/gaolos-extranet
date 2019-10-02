// GAOLOS 2017 v1.0
// File: Core.js
// Desc: Eventos básicos una vez hemos inicializado la aplicación

// Variables Globales

// Hacemos que Pace.js aparezca siempre que hay una llamada AJAX

$(document).ajaxStart(function () {
    Pace.restart();
});

function messageDatosOK() {
    var dialog = bootbox.dialog({
        message: "<div class='message-ko'><i class='fa fa-times'></i> " + response.mensaje + "</div>",
        closeButton: false
    });
    setTimeout(function () {
        dialog.modal('hide');
    }, 3000);
}

function messageDatosKO() {
    var dialog = bootbox.dialog({
        message: "<div class='message-ok'><i class='fa fa-check'></i> " + response.mensaje + "</div>",
        closeButton: false
    });
    setTimeout(function () {
        dialog.modal('hide');
    }, 3000);
}

// Logout

$('#btnlogout').click(function (e) {
    $.ajax({
        type: "GET",
        url: "/login/ajaxlogout",
        data: "",
        contentType: "application/json;charset=utf-8",
        cache: false,
        datatype: "json",
        success: function (response, textStatus, jqXHR) {
            if (response.err.eserror == true) {
                $.jGrowl(response.err.mensaje, $optionsMessageKO);
                //var dialog = bootbox.dialog({
                //    message: 'No se ha podido cerrar la sesión',
                //    //closeButton: false
                //});
                window.open('/', '_self');
                return false;
            } else {
                // validado
                window.open('/', '_self');
                return true;
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $.jGrowl(errorThrown, $optionsMessageKO);
            return false;
        }
    });
});

// Selección de filtro de menú rápido

$('.filtro').click(function (e) {
    $.ajax({
        type: "GET",
        url: "/login/AjaxFiltroUsuario",
        data: (e.currentTarget).id,
        contentType: "application/json;charset=utf-8",
        cache: false,
        datatype: "json",
        success: function (response, textStatus, jqXHR) {
            if (response.err.eserror == true) {
                $.jGrowl(response.err.mensaje, $optionsMessageKO);
                //var dialog = bootbox.dialog({
                //    message: 'No se ha podido cambiar el filtro',
                //    //closeButton: false
                //});
                return false;
            } else {
                // validado
                location.reload();
                return true;
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $.jGrowl(errorThrown, $optionsMessageKO);
            return false;
        }
    });
});

// Cambio de negocio

$('.refneg').click(function (e) {
    $.ajax({
        type: "GET",
        url: "/login/AjaxCambioNegocio",
        data: (e.currentTarget).id,
        contentType: "application/json;charset=utf-8",
        cache: false,
        datatype: "json",
        success: function (response, textStatus, jqXHR) {
            if (response.eserror == true) {
                var dialog = bootbox.dialog({
                    message: 'No se ha podido cambiar el filtro',
                    //closeButton: false
                });
                return false;
            } else {
                // validado
                location.reload();
                return true;
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
            return false;
        }
    });
});

// Toggle Sidebar y Notificaciones

$(".toggle-menu").click(function () {
    $('.layout__nav').toggleClass('collapsed-sidebar');
    $('i', this).toggleClass('fa-angle-right fa-angle-left');
});

$(".toggle-notificaciones").click(function () {
    $('.layout__aside').toggleClass('collapsed-aside');
    $('i', this).toggleClass('fa-angle-left fa-angle-right');
});

$(".menu-item-container").tooltip("disable");

$(".toggle-expandir-sidebars").on('click', function (i, e) {

    var $arrow = $(".toggle-expandir-sidebars .material-icons").text();

    if ($arrow == 'keyboard_arrow_left') {
        $(this).html("<i class='material-icons'>keyboard_arrow_right</i>");
    } else {
        $(this).html("<i class='material-icons'>keyboard_arrow_left</i>");
    }

    $('.layout__nav').toggleClass("collapsed-sidebar");

    $('.modulo-titulo').toggle();
    $('.colapsar-menu').toggle();
    $('.copyright').toggle();

    $('.logo').toggleClass("d-none");
    $('.logo-ico').toggle();

    $('.menu-item-container').tooltip('toggleEnabled');

    $(".toggle-expandir-sidebars .material-icons").toggleClass("animated flash");

    var $cierraCollapse = $(".contentCollapse").each(function (i, e) {
        $(this).attr("id", "id_" + i);
    });

    $(".menu-item-container").on("mouseenter", function () {
        $(".tooltip").css({ "margin-left": "40px", "margin-top": "5px", "pointer-events": "none", "position": "fixed" });
        $(".tooltip .arrow").css("display", "none");
    });

    $(".menu-item-container").on("mouseleave", function () {
    });

    $cierraCollapse.toggle();
    
});

// PreventDefaults

$(".pagination .active a").click(function () {
    event.preventDefault();
});

$('.card-actions .handle').click(function () {
    event.preventDefault();
});

// Mensajes Globales para jGrowl

$icoMessageOK = '<i class="fa fa-check"></i> ';
$icoMessageKO = '<i class="fa fa-exclamation-triangle"></i> ';
$icoMessageInfo = '<i class="fa fa-check"></i> ';

$optionsMessageOK = {
    position: 'center',
    header: '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 130.2 130.2" style="width: 80px; display: block; margin: 20px auto 0 auto;"><circle class="path circle" fill="none" stroke="#00BFA5" stroke-width="6" stroke-miterlimit="10" cx="65.1" cy="65.1" r="62.1" /><polyline class="path check" fill="none" stroke="#00BFA5" stroke-width="6" stroke-linecap="round" stroke-miterlimit="10" points="100.2,40.2 51.5,88.8 29.8,67.5 " /></svg>',
    theme: 'bg-success-jgrowl',
    size: 'large'
};

$optionsMessageKO = {
    position: 'center',
    header: '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 130.2 130.2" style="width: 80px; display: block; margin: 10px auto 0 auto;"><circle class= "path circle" fill="none" stroke="#FF1744" stroke-width="6" stroke-miterlimit="10" cx="65.1" cy="65.1" r="62.1" /><line class="path line" fill="none" stroke="#FF1744" stroke-width="6" stroke-linecap="round" stroke-miterlimit="10" x1="34.4" y1="37.9" x2="95.8" y2="92.3" /><line class="path line" fill="none" stroke="#FF1744" stroke-width="6" stroke-linecap="round" stroke-miterlimit="10" x1="95.8" y1="38" x2="34.4" y2="92.2" /></svg>',
    theme: 'bg-danger-jgrowl',
    size: 'large'
};

$optionsMessageInfo = {
    position: 'center',
    //header: '<i class="fa fa-times"></i> Algo salió mal',
    theme: 'bg-info',
    size: 'large'
};

// Habilitamos efecto Tooltip en toda la web

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

// Escondemos el tooltip cuando hacemos click en otro objeto

$('[data-toggle="tooltip"]').tooltip({ trigger: "hover" });

// Refresco de contadores generales

function RefreshCounters() {

    var elems = document.getElementsByClassName('counter-class');
    for (t = 0; t < elems.length; t++) {
        elems[t].style.display = "none";
    }

    //ActualizarContadores
    $.ajax(
        {
            type: "GET",
            url: "/menus-counter",
            data: { },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response != null) {
                    for (t = 0; t < response.length; t++) {
                        var elem = document.getElementById(response[t].id);
                        var elemli = document.getElementById(response[t].id+"_li");
                        if (response[t].val != "0") {
                            elem.style.display = "inline-flex";
                            elem.innerHTML = response[t].val;
                            if (elemli != null) elemli.style.display = "flex";
                        } else {
                            elem.style.display = "none";
                            elem.innerHTML = "";
                            if (elemli != null) elemli.style.display = "none";
                        }
                    }
                }
                return true;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

// Responsive scripts

$("#show-menu-desktop").click(function () {
    console.log("hola");
    //$("#MenuGaolosComponent").toggleClass("d-none d-md-block");
    $("#MenuGaolosComponent").toggleClass("d-none");
});

// Función para formatear números

function formatearNumero(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    return x1 + x2;
}

/* Función para fijar el usuario en la parte superior [sticky top menu] */

$(document).ready(function () {
    $("#MenuUsuarioComponent").sticky({ topSpacing: 0 });
});

/* Función para copiar en el portapapeles [librería clipboard.js] */

$(document).ready(function () {

    var $copyElement;

    $copyElement = $("<a href='#' class='copy-info ml-2'><i class='fa fa-clipboard'></i></a>")

    function hoverCopy() {        
        $(this).append($copyElement);
        var $texto = $(this).find("span").text();       
    }

    $(".copy-data p").on("mouseenter", function () {

        $(this).each(function (i, e) {

            $(this).append($copyElement);

            //$(".copy-info", this).attr("id", "id_" + i);

            $(this).find(".copy-info").on("click", function () {

                event.preventDefault();

                console.log("click funcion mouseover");

                if (clipboard) {
                    clipboard.destroy();
                }

                var clipboard = new ClipboardJS(".copy-info", {
                    text: function (trigger) {
                        return $(trigger).parent().find('span').text();
                    }
                });

                clipboard.on('success', function (e) {
                    console.info('Action:', e.action);
                    console.info('Text:', e.text);
                    console.info('Trigger:', e.trigger);
                    e.clearSelection();
                });

                $.jGrowl("Copiado al portapapeles", $optionsMessageOK);

            });
        
        });

    });

    $(".copy-data p").on("mouseleave", function () {
        $copyElement.remove();
    });

});

// Función para obtener el tiempo

$(document).ready(function () {
    var city = "Barcelona";
    var searchtext = "select item.condition from weather.forecast where woeid in (select woeid from geo.places(1) where text='" + city + "') and u='c'"
    //change city variable dynamically as required
    $.getJSON("https://query.yahooapis.com/v1/public/yql?q=" + searchtext + "&format=json").success(function (data) {
        console.log(data);
        $('#weather').html("La temperatura de hoy en " + city + " es de " + data.query.results.channel.item.condition.temp + "°C");
    });
});

////////// ANIMACIONES

// Card "Sin Datos"

//$("#sindatos img").addClass("animation-delay-200 animated fadeIn");
$("#sindatos").show();
$("#sindatos img").addClass("animated fadeIn");
$("#sindatos").addClass("animated fadeIn");
$("#sindatos p").addClass("animated fadeIn");
//$("#sindatos p").addClass("animation-delay-300 animated fadeIn");

//$("#versininfo img").addClass("animation-delay-200 animated fadeIn");
$("#versininfo").show();
$("#versininfo img").addClass("animated fadeIn");
$("#versininfo").addClass("animated fadeIn");
$("#versininfo p").addClass("animated fadeIn");
//$("#versininfo p").addClass("animation-delay-300 animated fadeIn");

//$("#sindatos").mouseover(function () {
//    $(this).find("img").addClass("animated infinite bounce");
//});
//$("#sindatos").mouseout(function () {
//    $(this).find("img").removeClass("animated infinite bounce");
//});

// Bloques dashboard y configuración de módulos

$(".col-conf figure").on("mouseenter", function () {
    $(this).css("transition", "all ease-in-out .3s");
    $(this).css("transform", "scale(1.2)");
});

$(".col-conf figure").on("mouseleave", function () {
    $(this).css("transition", "all ease-in-out .3s");
    $(this).css("transform", "scale(1)");
});

$(".badge-dashboard").on("mouseenter", function () {
    $(this).css("transition", "all ease-in-out .3s");
    $(this).css("transform", "scale(1.1)");
});

$(".badge-dashboard").on("mouseleave", function () {
    $(this).css("transition", "all ease-in-out .3s");
    $(this).css("transform", "scale(1)");
});

// Animación login

$(".bg-login").bgswitcher({
    images: ["https://public.gaolos.com/cdn/public/extranet/img/gaolos-login-bg-2.jpg", "https://images.pexels.com/photos/7374/startup-photos.jpg", "https://images.pexels.com/photos/7375/startup-photos.jpg", "https://images.pexels.com/photos/933964/pexels-photo-933964.jpeg", "https://images.pexels.com/photos/630839/pexels-photo-630839.jpeg"],
    effect: "fade"
});

// Table Row - Success

if ($('tr').hasClass('tr-success')) {
    $(this).parent('tr').css('border-bottom', '1px solid red');
}