$(document.body).on('click', '.comi-cerrar', function () {

    event.preventDefault(event);

    var ID_Fac = document.getElementById("ID_Fac").value;

    var Fe_Fi = document.getElementById("tmpFe_Fi").value;
    var pag = document.getElementById("tmppag").value;
    var ID_Us_Asi = document.getElementById("tmpID_Us_Asi").value;


    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/comisionar/factura/detalle/comisionescerrar",
                data: { ID_Fac: ID_Fac },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        var url = "/modulo-asistencias/comisionar/?Fe_Fi=" + encodeURI(Fe_Fi) + "&pag=" + pag + "&vopecom=" + ID_Us_Asi;
                        window.open(url, "_self");
                        return;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
});

$(document.body).on('click', '.eli-comi', function () {

    event.preventDefault(event);

    var ID_Fac = document.getElementById("ID_Fac").value;
    var ID_Al2 = document.getElementById("ID_Al2").value;
    var ID_De_Al = document.getElementById("ID_De_Al").value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cmt = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/comisionar/factura/detalle/comisioneseliminar",
                data: { ID_Fac: ID_Fac, ID_Al2: ID_Al2, ID_De_Al: ID_De_Al, ID_Cmt: ID_Cmt },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarTablaComisiones(ID_Fac, ID_Al2, ID_De_Al);
                        return;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
});

$(document.body).on('click', '.guardar-comi', function () {

    event.preventDefault(event);

    var ID_Fac = document.getElementById("ID_Fac").value;
    var ID_Al2 = document.getElementById("ID_Al2").value;
    var ID_De_Al = document.getElementById("ID_De_Al").value;

    var strid = "";
    var elements = document.getElementById('lis-com').querySelectorAll('input, radio');
    var eleAct
    for (var i = 0; i < elements.length; i++) {
        if (strid != "") strid += ";";
        strid += elements[i].id + "|" + elements[i].value;
    };

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/comisionar/factura/detalle/comisionesguardar",
                data: { ID_Fac: ID_Fac, ID_Al2: ID_Al2, ID_De_Al: ID_De_Al, ids: strid },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarTablaComisiones(ID_Fac, ID_Al2, ID_De_Al);
                        return;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
});


$(document.body).on('click', '.addopecom', function () {

    event.preventDefault(event);

    var ID_Fac = document.getElementById("ID_Fac").value;
    var ID_Al2 = document.getElementById("ID_Al2").value;
    var ID_De_Al = document.getElementById("ID_De_Al").value;
    var ID_Us_Asi = document.getElementById("vopecom").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/comisionar/factura/detalle/addoperario",
                data: { ID_Fac: ID_Fac, ID_Al2: ID_Al2, ID_De_Al: ID_De_Al, ID_Us_Asi: ID_Us_Asi },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                        ActualizarTablaComisiones(ID_Fac, ID_Al2, ID_De_Al);
                        return;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
});


function ActualizarTablaComisiones(ID_Fac, ID_Al2, ID_De_Al) {

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/comisionar/factura/detalle/comisiones",
                data: { ID_Fac: ID_Fac, ID_Al2: ID_Al2, ID_De_Al: ID_De_Al },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);
                        var txt = '';
                        var num = 0;
                        if (response.det != null) num = response.det.length;
                        for (t = 0; t < num; t++) {
                            txt += '<tr>';
                            txt += '<td class="">' + response.det[t].datoS1 + '</td>';
                            txt += '<td class="text-center">';
                            txt += '<div class="input-group input-group-sm">';
                            txt += '<input type="text" class="form-control form-control-sm" placeholder="" id="Be2_' + response.det[t].datoI + '" value="' + response.det[t].datoS2 + '">';
                            txt += '<div class="input-group-append">';
                            txt += '<span class="input-group-text input-group-text-auto">%</span>';
                            txt += '</div>';
                            txt += '</div>';
                            txt += '</td>';
                            txt += '<td class="text-center">';
                            txt += '<div class="input-group input-group-sm">';
                            txt += '<input type="text" class="form-control form-control-sm" placeholder="" id="Ba2_' + response.det[t].datoI + '" value="' + response.det[t].datoS3 + '">';
                            txt += '<div class="input-group-append">';
                            txt += '<span class="input-group-text input-group-text-auto">%</span>';
                            txt += '</div>';
                            txt += '</div>';
                            txt += '</td>';
                            txt += '<td class="text-center">' + response.det[t].datoS4 + '</td>';
                            txt += '<td class="text-center"><a href="#" class="eli-comi" id="elicom_' + response.det[t].datoI + '"><i class="fa fa-times text-danger"></i></a></td>';
                            txt += '</tr>';
                        }
                        if (txt == "") {
                            txt = '<tr><td colspan="5">No hay datos</td></tr>';
                        }

                        document.getElementById("comi").innerHTML = response.comi;
                        document.getElementById("benef").innerHTML = response.benef;

                        document.getElementById("lis-com").innerHTML = txt;

                        return;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });



}




$(document.body).on('click', '.seldet', function () {

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Al2 = strid[1];
    var ID_De_Al = strid[2];
    var ID_Fac = document.getElementById("ID_Fac").value;
    document.getElementById("ID_Al2").value = ID_Al2;
    document.getElementById("ID_De_Al").value = ID_De_Al;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-asistencias/comisionar/factura/detalle",
                data: { ID_Fac: ID_Fac, ID_Al2: ID_Al2, ID_De_Al: ID_De_Al },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        var num = 0;
                        var elems = document.getElementsByClassName('findtr');
                        if (elems != null) num = elems.length;
                        for (t = 0; t < num; t++) {
                            var elem = elems[t];
                            elem.classList.remove("tr-block");
                            var strid2 = elem.id.split('_');
                            if (strid2[1] == ID_De_Al) elem.classList.add("tr-block");
                        
                        }

                        document.getElementById("Expo").innerHTML = response.expo;
                        document.getElementById("Be2").innerHTML = response.benef2;
                        document.getElementById("Ba2").innerHTML = response.base2;

                        ActualizarTablaComisiones(ID_Fac, ID_Al2, ID_De_Al);

                        $("#sindatos").hide();
                        $("#verdet").show();
                        
                        return;

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });
    });
    
});



//// sin uso 

//$(document.body).on('keyup', '.cambiocomiemprel', function () {

//    var idd = this.id;
//    var strid = idd.split('_');

//    var PVPF = 0.0;
//    try {
//        PVPF = parseFloat(document.getElementById("pvpf_" + strid[1]).innerHTML.replace(",", "."));
//    } catch { }

//    var ComiPor = 0.0;
//    try {
//        ComiPor = parseFloat(document.getElementById("comporrel_" + strid[1]).value.replace(",", "."));
//    } catch { }

//    var valor = PVPF / 100.0 * ComiPor;

//    document.getElementById("comimprel_" + strid[1]).innerHTML = formatearNumero(Number(valor).toFixed(2)) + " €"; //Number(total).toFixed(2);

//});

//$(document.body).on('keyup', '.cambiocomiope', function () {

//    var idd = this.id;
//    var strid = idd.split('_');

//    document.getElementById("comope2_" + strid[1]).valor = "";

//    var CosteF = 0.0;
//    try {
//        CosteF = parseFloat(document.getElementById("costef_" + strid[1]).innerHTML.replace(",", "."));
//    } catch { }

//    var PVPF = 0.0;
//    try {
//        PVPF = parseFloat(document.getElementById("pvpf_" + strid[1]).innerHTML.replace(",", "."));
//    } catch { }

//    var ComiPor = 0.0;
//    try {
//        ComiPor = parseFloat(document.getElementById("comporrel_" + strid[1]).value.replace(",", "."));
//    } catch { }

//    var ComiPorOpe = 0.0;
//    try {
//        ComiPorOpe = parseFloat(document.getElementById("comope_" + strid[1]).value.replace(",", "."));
//    } catch { }

//    var valorComAdm = PVPF / 100.0 * ComiPor;

//    var valor = (PVPF - valorComAdm - CosteF) / 100.0 * ComiPorOpe;

//    document.getElementById("comopeval_" + strid[1]).innerHTML = formatearNumero(Number(valor).toFixed(2)) + " €"; //Number(total).toFixed(2);

//});

//$(document.body).on('keyup', '.cambiocomiope2', function () {

//    var idd = this.id;
//    var strid = idd.split('_');

//    document.getElementById("comope_" + strid[1]).valor = "";

//    var CosteF = 0.0;
//    try {
//        CosteF = parseFloat(document.getElementById("costef_" + strid[1]).innerHTML.replace(",", "."));
//    } catch { }

//    var PVPF = 0.0;
//    try {
//        PVPF = parseFloat(document.getElementById("pvpf_" + strid[1]).innerHTML.replace(",", "."));
//    } catch { }

//    var ComiPor = 0.0;
//    try {
//        ComiPor = parseFloat(document.getElementById("comporrel_" + strid[1]).value.replace(",", "."));
//    } catch { }

//    var ComiPorOpe = 0.0;
//    try {
//        ComiPorOpe = parseFloat(document.getElementById("comope2_" + strid[1]).value.replace(",", "."));
//    } catch { }

//    var valorComAdm = PVPF / 100.0 * ComiPor;

//    var valor = (PVPF - valorComAdm) / 100.0 * ComiPorOpe;

//    document.getElementById("comopeval_" + strid[1]).innerHTML = formatearNumero(Number(valor).toFixed(2)) + " €"; //Number(total).toFixed(2);

//});


$(function () {

    $("#opecom").keydown(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#opecom").blur(function () {
        if ($(this).val() == '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#opecom").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-asistencias/comisionar/buscar-tecnico",
                data: { buscar: encodeURI(document.getElementById('opecom').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror == true) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var result = [];

                        if (response.det != null) {
                            for (var i = 0; i < response.det.length; i++) {
                                var obj = {
                                    value: response.det[i].id,
                                    label: response.det[i].det
                                };
                                result.push(obj);
                            }
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
