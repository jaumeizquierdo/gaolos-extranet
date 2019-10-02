// Cambio mes calendario
$(document.body).on('click', '.btn-calendario-mes', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    CalendarioMensual(strid[1], strid[2]);

});


// Nuevo tag

$(document.body).on('click', '.nuevo-tag', function () {

    event.preventDefault(event);

    $('#modalNuevoTag').modal({
        show: true
    });

});


// Calendario
$(document).ready(function () {
    $(function () {
        $('#Fe_In').datepicker({
            format: 'dd/mm/yyyy',
            language: 'es',
            autoclose: 'true',
            todayHighlight: 'true'
        });
    });

    $(function () {
        $('#Fe_Apa').datepicker({
            format: 'dd/mm/yyyy',
            language: 'es',
            autoclose: 'true',
            todayHighlight: 'true'
        });
    });
});

// Buscador usuario

$(function () {

    $("#findcarac1").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#findcarac1").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#findcarac1").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/tareas-pendientes/buscar-usuario",
                data: { buscar: encodeURI(document.getElementById('findcarac1').value).replace('&', '%26'), RefNeg: document.getElementById('RefNeg').value },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
                        }
                        responsefunc(result);

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
        },
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $(this).val(ui.item.label);
            document.getElementById('v' + $(this)[0].id).value = ui.item.value;
            document.getElementById('t' + $(this)[0].id).value = ui.item.label;
            return false;
        }
    });
});

$(document.body).on('change', '.cambio-neg', function () {

    document.getElementById("vfindcarac1").value = "";
    document.getElementById("tfindcarac1").value = "";
    document.getElementById("findcarac1").value = "";
});

$(document.body).on('click', '.nueva-tarea', function () {

    event.preventDefault(event);

    var Fe_In = document.getElementById('Fe_In').value;
    var Ho_In = document.getElementById('Ho_In').value;
    var Expo = document.getElementById('Expo').value;
    var RefNeg = document.getElementById('RefNeg').value;
    var ID_Us_Asi = document.getElementById('vfindcarac1').value;
    var Prio = document.getElementById('Prio').value;
    var CtrlLec = document.getElementById('CtrlLec').checked;
    var CtrlFin = document.getElementById('CtrlFin').checked;
    var id1 = document.getElementById('vempresa').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-nueva",
                data: { Fe_In: Fe_In, Ho_In: Ho_In, Expo: Expo, RefNeg: RefNeg, ID_Us_Asi: ID_Us_Asi, Prio: Prio, CtrlLec: CtrlLec, CtrlFin: CtrlFin, id1: id1 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes", "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.replanificar-tarea', function () {

    event.preventDefault(event);

    var ID_Twit = document.getElementById('ID_Twit').value;
    var ID_TwitIni = document.getElementById('ID_TwitIni').value;
    var Expo = document.getElementById('Expo').value;
    var Fe_Apa = document.getElementById('Fe_Apa').value;
    var Min = document.getElementById('Min').value;
    var RefNeg = document.getElementById('RefNeg').value;
    var ID_Us_Asi = document.getElementById('vfindcarac1').value;
    var Prio = document.getElementById('Prio').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-replanificar",
                data: { ID_Twit: ID_Twit, ID_TwitIni: ID_TwitIni, Expo: Expo, Fe_Apa: Fe_Apa, Min: Min, RefNeg: RefNeg, ID_Us_Asi: ID_Us_Asi, Prio: Prio },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Twit=" + ID_TwitIni, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.reabrir-tarea', function () {

    event.preventDefault(event);

    var ID_Twit = document.getElementById('rID_Twit').value;
    var ID_TwitIni = document.getElementById('rID_TwitIni').value;
    var Expo = document.getElementById('rExpo').value;
    var Fe_Apa = ""; //document.getElementById('Fe_Apa').value;
    var Min = ""; //document.getElementById('Min').value;
    var RefNeg = ""; //document.getElementById('rRefNeg').value;
    var ID_Us_Asi = 0; // document.getElementById('vfindcarac1').value;
    var Prio = ""; //document.getElementById('Prio').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-reabrir",
                data: { ID_Twit: ID_Twit, ID_TwitIni: ID_TwitIni, Expo: Expo, Fe_Apa: Fe_Apa, Min: Min, RefNeg: RefNeg, ID_Us_Asi: ID_Us_Asi, Prio: Prio },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Twit=" + ID_TwitIni, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.cerrar-tarea', function () {

    event.preventDefault(event);

    var ID_Twit = document.getElementById('ID_Twit').value;
    var ID_TwitIni = document.getElementById('ID_TwitIni').value;
    var Expo = document.getElementById('ExpoFin').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-cerrar",
                data: { ID_Twit: ID_Twit, ID_TwitIni: ID_TwitIni, Expo: Expo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Twit=" + ID_TwitIni, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.cerrar-solicitud-sin-tarea-pendiente', function () {

    event.preventDefault(event);

    var ID_Soli2 = document.getElementById('ID_Soli2').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-cerrar-solicitud-sin-tarea-pendiente",
                data: { ID_Soli2: ID_Soli2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Soli2=" + ID_Soli2, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.cambiar-curso-inscripcion-curso-tarea', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar la inscripción de curso?")) {
        return false;
    }

    var ID_Soli2 = document.getElementById('ID_Soli2').value;
    var ID_Curso2 = document.getElementById('ID_Curso2Nuevo').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-inscripcioncursocambiarcurso",
                data: { ID_Soli2: ID_Soli2, ID_Curso2: ID_Curso2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Soli2=" + ID_Soli2, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.eliminar-inscripcion-curso-tarea', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar la inscripción?")) {
        return false;
    }

    var ID_Soli2 = document.getElementById('ID_Soli2').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-inscripcioncursoeliminar",
                data: { ID_Soli2: ID_Soli2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Soli2=" + ID_Soli2, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.mi-perfil-sel-negocio', function () {

    event.preventDefault(event);
    //event.stopPropagation();
    //Pace.restart();

    var idd = this.id;
    var strid = idd.split('_');
    var RefNeg = strid[1];

     SelMenuActualizarMenuNegocio(RefNeg);

});

function SelMenuActualizarMenuNegocio(RefNeg) {

    event.stopPropagation();

    // html5sortable
    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/mygaolos/miperfilnegociomenu",
                data: { RefNeg: RefNeg },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {

                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        document.getElementById("detalles-menu").style.display = "none";
                        document.getElementById("RefNeg").value = "";
                        return false;
                    } else {

                        document.getElementById("RefNeg").value = RefNeg;
                        document.getElementById("detalles-menu-neg").innerHTML = response.deco;

                        var ul = document.createElement("UL");
                        ul.className = "js-sortable-connected list flex flex-column";
                        ul.id = "accordion";
                        ul.setAttribute("aria-dropeffect", "move");

                        var txt = "";
                        txt += '';
                        for (t = 0; t < response._menus.length; t++) {

                            var nodep = document.createElement("LI");
                            nodep.className = "main-item fw-5 px-3 py-2";
                            nodep.setAttribute("role", "option");
                            nodep.setAttribute("aria-grabbed", "false");
                            nodep.id = "m_" + response._menus[t].id + "_0";


                            var div = document.createElement("DIV");
                            div.innerHTML = '<span><i class="fa fa-arrows moverItem mdc-text-blue-500 mr-2" draggable="true"></i><span id="">' + response._menus[t].orden + '</span>. ' + response._menus[t].titulo + '</span><a href="#" class="open-menu" data-toggle="collapse" data-target="#collapse_' + response._menus[t].id + "_0" + '" aria-expanded="true" aria-controls="collapse_' + response._menus[t].id + '"_0"><i class="fa fa-chevron-down"></i></a>';
                            div.className = "d-flex justify-content-between mb-0 fw-5";
                            div.id = "heading_" + response._menus[t].id + "_0";
                            nodep.appendChild(div);

                            var x = document.createElement("UL");
                            x.className = "js-sortable-inner-connected list flex flex-column mb-0 mt-1 ml-3 collapse";
                            x.id = "collapse_" + response._menus[t].id + "_0";
                            x.setAttribute("aria-dropeffect", "move");

                            for (p = 0; p < response._menus[t]._menudetalle.length; p++) {
                                var node = document.createElement("LI");                 // Create a <li> node
                                node.innerHTML = '<i class="fa fa-arrows moverSubItem mdc-text-blue-500 mr-2 mt-1" draggable="true"></i>' + '<span class="mdc-text-blue-grey-400">' + response._menus[t]._menudetalle[p].subTitulo + '</span>';
                                node.className = "item fw-5";
                                node.setAttribute("role", "option");
                                node.setAttribute("aria-grabbed", "false");
                                x.appendChild(node);
                            }
                            nodep.appendChild(x);

                            ul.appendChild(nodep);

                        }

                        var cont = document.getElementById("ordenarMenu");
                        while (cont.firstChild) {
                            cont.removeChild(cont.firstChild);
                        }

                        document.getElementById("ordenarMenu").appendChild(ul);
                        document.getElementById("detalles-menu").style.display = "block";

                        $(function () {
                            sortable('.js-sortable-connected', {
                                forcePlaceholderSize: true,
                                connectWith: '.js-connected',
                                handle: '.moverItem',
                                items: 'li',
                                placeholderClass: 'sortable-ghost'
                            });
                            sortable.bind('sortupdate', function (e, ui) {
                                $item = ui;
                                $itemDragged = ui.item;
                                origin = ui.startparent[0].id;
                                destination = this.id;
                                $("#form_to_complete").show('fade');
                            });
                            sortable('.js-sortable-inner-connected', {
                                forcePlaceholderSize: true,
                                connectWith: 'js-inner-connected',
                                handle: '.moverSubItem',
                                items: '.item',
                                maxItems: 3,
                                placeholderClass: 'sortable-ghost'
                            });
                            document.querySelector('.js-sortable-connected').addEventListener('sortupdate', function (e) {
                                //console.log('Sortupdate: ', e.detail);
                                //console.log('Container: ', e.detail.origin.container, ' -> ', e.detail.destination.container);
                                //console.log('Index: ' + e.detail.origin.index + ' -> ' + e.detail.destination.index);
                                //console.log('Element Index: ' + e.detail.origin.elementIndex + ' -> ' + e.detail.destination.elementIndex);
                                CambiarOrdenMenu(e.detail.origin.elementIndex, e.detail.destination.elementIndex);
                            });
                            document.querySelector('.js-sortable-connected').addEventListener('sortstart', function (e) {
                                //console.log('Sortstart: ', e.detail);
                                //console.log('Inicio el drop');
                            });
                            document.querySelector('.js-sortable-connected').addEventListener('sortstop', function (e) {
                                //console.log('Sortstop: ', e.detail);
                                //console.log('he realizado el drop');
                            });

                        });

                        $(".open-menu").click(function () {
                            console.log('click');
                            $(this)
                                .find('i.fa')
                                .toggleClass('fa-chevron-down fa-chevron-up');
                        });

                        var targetOffset = $('#detalles-menu').offset().top;
                        $('html, body').animate({ scrollTop: targetOffset }, 1000);


                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    document.getElementById("ordenarMenus").style.display = "none";
                    document.getElementById("RefNeg").value = "";
                    return false;
                }
            });
    });
}

function CambiarOrdenMenu(indexo, indexd) {

    var RefNeg = document.getElementById("RefNeg").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/mi-perfil/menucambiarordendraganddrop",
                data: { RefNeg: RefNeg, indexo: indexo, indexd: indexd },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        SelMenuActualizarMenuNegocio(RefNeg);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        //SelMenuActualizarMenuNegocio(RefNeg);
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });

}


$(document.body).on('click', '.detalles-menu-mover-menu', function () {

    event.preventDefault(event);

    var idd = this.id;
    var RefNeg = document.getElementById("RefNeg").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/mi-perfil/menucambiarorden",
                data: { idd: idd, RefNeg: RefNeg },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        SelMenuActualizarMenuNegocio(RefNeg);
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.mi-perfil-cambiar-clave', function () {

    event.preventDefault(event);

    var PassA = document.getElementById("PassA").value;
    var PassN = document.getElementById("PassN").value;
    var PassR = document.getElementById("PassR").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/mi-perfil/cambiarclave",
                data: { PassA: PassA, PassN: PassN, PassR: PassR },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("PassA").value = "";
                        document.getElementById("PassN").value = "";
                        document.getElementById("PassR").value = "";
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.mi-perfil-cambiar-nombre', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar su nombre en su perfil?")) {
        return false;
    }

    var Nom = document.getElementById("Nom").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/mi-perfil/cambiarnombre",
                data: { Nom: Nom },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/mi-perfil", "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

//Buscador empresas

$(function () {

    $("#empresa").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#empresa").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#empresa").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/tareas-pendientes/listado-empresas-buscar",
                data: { buscar: encodeURI(document.getElementById('empresa').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
                        }
                        responsefunc(result);

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
        },
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $(this).val(ui.item.label);
            document.getElementById('v' + $(this)[0].id).value = ui.item.value;
            document.getElementById('t' + $(this)[0].id).value = ui.item.label;
            return false;
        }
    });
});

$(document.body).on('click', '.relacionar-empresa', function () {

    event.preventDefault(event);

    var id1 = document.getElementById('vempresa').value;
    var ID_Twit = document.getElementById("ID_Twit").value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/vincularempresa",
                data: { id1: id1, ID_Twit: ID_Twit },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Twit=" + ID_Twit, "_self");
                        //document.getElementById("vempresa").value = "";
                        //document.getElementById("empresa").value = "";
                        //document.getElementById("tempresa").value = "";
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.eliminar-empresa-rel', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Twit = document.getElementById("ID_Twit").value;
    var ID_Rel = strid[1];

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/vincularempresaeliminar",
                data: { ID_Rel: ID_Rel, ID_Twit: ID_Twit },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Twit=" + ID_Twit, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

// Buscador curso

$(function () {

    $("#findcurso").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarPrecios();
            return false;
        }
    });

    $("#findcurso").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        ActualizarPrecios();
        return false;
    });
    $("#findcurso").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/tareas-pendientes/buscar-curso",
                data: { buscar: encodeURI(document.getElementById('findcurso').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var arr = response.datoS.split("|");
                        var num = arr.length;
                        var result = [];

                        for (var i = 0; i < num; i++) {
                            var dat = arr[i].split(",");
                            var obj = {
                                value: dat[0],
                                label: dat[1]
                            };
                            result.push(obj);
                        }
                        responsefunc(result);

                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
        },
        focus: function (event, ui) {
            $(this).val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $(this).val(ui.item.label);
            document.getElementById('v' + $(this)[0].id).value = ui.item.value;
            document.getElementById('t' + $(this)[0].id).value = ui.item.label;
            ActualizarPrecios();
            return false;
        }
    });
});

function ActualizarPrecios() {

    var ID_Curso2 = document.getElementById('vfindcurso').value;
    var elem = document.getElementById('ID_Precio');

    if (elem === null) { return; }

    if (ID_Curso2 === "" || ID_Curso2 === "0") {
        elem.innerHTML = "";
        elem.disabled = true;
        return;
    }

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/listado-precios-cursos",
                data: { ID_Curso2: ID_Curso2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var txt = "";
                        if (response.det !== null) {
                            for (var t = 0; t < response.det.length; t++) {
                                txt += '<option value="' + response.det[t].id + '">' + response.det[t].det + '</option>';
                            }
                            txt += '</select>';
                            elem.disabled = false;
                        } else {
                            elem.disabled = true;
                        }
                        elem.innerHTML = txt;
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

}

$(document.body).on('click', '.solicitud-inscripcion-curso', function () {

    event.preventDefault(event);

    var ID_Soli2 = document.getElementById('ID_Soli2').value;
    var ID_Curso2 = document.getElementById('vfindcurso').value;
    var EnvCom = document.getElementById('EnvCom').checked;
    var ID_Precio = document.getElementById('ID_Precio').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-inscripcion-curso",
                data: { ID_Soli2: ID_Soli2, ID_Curso2: ID_Curso2, ID_Precio: ID_Precio, EnvCom: EnvCom },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Soli2=" + ID_Soli2, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});

$(document.body).on('click', '.solicitud-enviar-mail-info', function () {

    event.preventDefault(event);

    var ID_Soli2 = document.getElementById('ID_Soli2').value;
    var EnvCom = document.getElementById('EnvComAux').checked;
    var nuevoMail = document.getElementById('nuevoMail').value;
    var MailOri = document.getElementById('MailOri').value;

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/tareas-pendientes/tarea-pendiente-curso-enviar-mail-info",
                data: { ID_Soli2: ID_Soli2, EnvCom: EnvCom, nuevoMail: nuevoMail, MailOri: MailOri },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/tareas-pendientes/tarea-pendiente?ID_Soli2=" + ID_Soli2, "_self");
                        return true;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

});