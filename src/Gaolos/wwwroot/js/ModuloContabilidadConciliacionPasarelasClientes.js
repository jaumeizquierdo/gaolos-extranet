


function AddDaysToDate(sDate, iAddDays, sSeperator) {
    var date = new Date(sDate);
    date.setDate(date.getDate() + parseInt(iAddDays));
    var sEndDate = LPad(date.getMonth() + 1, 2) + sSeperator + LPad(date.getDate(), 2) + sSeperator + date.getFullYear();
    return sEndDate;
}
function LPad(sValue, iPadBy) {
    sValue = sValue.toString();
    return sValue.length < iPadBy ? LPad("0" + sValue, iPadBy) : sValue;
}


// Conciliacion 


$(document.body).on('change', '.cuenta', function () {

    var ID_Pasa = document.getElementById("ID_Pasa").value;

    reloadmovimientos(ID_Pasa, 1);
    reloadfracciones(ID_Pasa,1);


});

$(document.body).on('click', '.paginadormov', function () {


    var idd = this.id;
    var strid = idd.split('_');

    reloadmovimientos(strid[2], strid[1]);

});



function reloadmovimientos(ID_Pasa, ID_Pag) {

    var buscar_mov = document.getElementById("buscar-mov").value;
    var importe_mov = document.getElementById("importe-mov").value;
    var Fe_In = document.getElementById("fe-in-mov").value;
    var Fe_Fi = document.getElementById("fe-fi-mov").value;
    var num = document.getElementById("reg-mov-cue").value;

    var Clas = 'paginadormov';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliacion/pasarelas-facturas/detalles",
            data: { ID_Pasa: ID_Pasa, pag: ID_Pag, Clas: Clas, buscar: buscar_mov, Imp: importe_mov, Fe_In: Fe_In, Fe_Fi: Fe_Fi, num: num },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr>';
                            txt += '<td><a href="#" class="mb-0 fw-5 sel-fecha-mov">' + response.det[t].fe_Val + '</a></td>';
                            txt += '<td>' + response.det[t].conc + '</td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].dev + '</p></td>';
                            txt += '<td class="text-right"><a href="#" class="fw-5 sel-importe-mov" id="sel-importe-mov_' + response.det[t].iD_Mov + '">' + response.det[t].imp + '</a></td>';
                            txt += '<td class="text-center"><div class="custom-control custom-checkbox mb-0 ml-3 mt-0"><input type="checkbox" class="custom-control-input sel-sel" id="sel-sel-importe-mov_' + response.det[t].iD_Mov + '"><label class="custom-control-label" for="sel-sel-importe-mov_' + response.det[t].iD_Mov + '"></label></div></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("cuenta-movimientos").innerHTML = txt;
                        document.getElementById("movimientos-cuenta-paginador").innerHTML = response.paginador;
                    } else {
                        txt = "<tr><td colspan='5'><p class='fw-5 text-danger mb-0'><i class='fa fa-times mr-1'></i> No hay movimientos</p></td></tr>";
                        document.getElementById("cuenta-movimientos").innerHTML = txt;
                        document.getElementById("movimientos-cuenta-paginador").innerHTML = "";
                    }

                    actualizarcontadormov();

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

$(document.body).on('click', '.ref-mov', function () {
    var ID_Pasa = document.getElementById("ID_Pasa").value;

    reloadmovimientos(ID_Pasa, 1);
});


$(document.body).on('click', '.ref-fra', function () {
    var ID_Pasa = document.getElementById("ID_Pasa").value;

    reloadfracciones(ID_Pasa,1);
});

$(document.body).on('click', '.paginadorfra', function () {
    var ID_Pasa = document.getElementById("ID_Pasa").value;

    var idd = this.id;
    var strid = idd.split('_');

    reloadfracciones(ID_Pasa,strid[1]);

});

function reloadfracciones(ID_Pasa, ID_Pag) {

    var buscar_fra = document.getElementById("buscar-fra").value;
    var importe_fra = document.getElementById("importe-fra").value;
    var Fe_In = document.getElementById("fe-in-fra").value;
    var Fe_Fi = document.getElementById("fe-fi-fra").value;
    var num = document.getElementById("reg-rem").value;

    var Clas = 'paginadorfra';

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliacion/pasarelas/fracciones",
            data: { pag: ID_Pag, Clas: Clas, buscar: buscar_fra, Imp: importe_fra, Fe_In: Fe_In, Fe_Fi: Fe_Fi, num: num, ID_Pasa: ID_Pasa },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<tr class="vis-fra" id="vis-fra_' + response.det[t].iD_Fra + '">';
                            txt += '<td><a href="#" class="mb-0 fw-5 sel-fecha-fra">' + response.det[t].fe_Ve + '</a></td>';
                            txt += '<td><a href="#" class="mb-0 fw-5 sel-fac-fra" id="sel-fac-fra_' + response.det[t].fac + '">' + response.det[t].conc + '</a></td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].forma + '</p></td>';
                            txt += '<td class="text-right"><a href="#" class="fw-5 sel-importe-fra" id="sel-importe-fra_' + response.det[t].iD_Fra + '">' + response.det[t].imp + '</a></td>';
                            txt += '<td class="text-center"><div class="custom-control custom-checkbox mb-0 ml-3 mt-0"><input type="checkbox" class="custom-control-input sel-sel-fra" id="sel-sel-importe-fra_' + response.det[t].iD_Fra + '"><label class="custom-control-label" for="sel-sel-importe-fra_' + response.det[t].iD_Fra + '"></label></div></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("fracciones-facturas").innerHTML = txt;
                        document.getElementById("fracciones-facturas-paginador").innerHTML = response.paginador;
                    } else {
                        txt = "<tr><td colspan='5'><p class='fw-5 text-danger mb-0'><i class='fa fa-times mr-1'></i> No hay movimientos</p></td></tr>";
                        document.getElementById("fracciones-facturas").innerHTML = txt;
                        document.getElementById("fracciones-facturas-paginador").innerHTML = "";
                    }

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });
}

$(document.body).on('click', '.sel-fecha-fra', function () {

    var elem = this;
    var fecha = elem.innerHTML;
    var fc = fecha.split('/');
    fecha = fc[1] + "/" + fc[0] + "/" + fc[2];

    var Fe_In = AddDaysToDate(fecha, -2, "/");
    var Fe_Fi = AddDaysToDate(fecha, +2, "/");

    fc = Fe_In.split('/');
    document.getElementById("fe-in-mov").value = (fc[1] + '/' + fc[0] + '/' + fc[2]);

    fc = Fe_Fi.split('/');
    document.getElementById("fe-fi-mov").value = (fc[1] + '/' + fc[0] + '/' + fc[2]);

    var ID_Pasa = document.getElementById("ID_Pasa").value;

    reloadmovimientos(ID_Pasa,1);

});

$(document.body).on('click', '.sel-importe-fra', function () {

    event.preventDefault();

    var ID_Pasa = document.getElementById("ID_Pasa").value;

    var elem = this;
    document.getElementById("importe-mov").value = elem.innerHTML;

    reloadmovimientos(ID_Pasa, 1);
});

$(document.body).on('click', '.sel-importe-mov', function () {

    var ID_Pasa = document.getElementById("ID_Pasa").value;

    var elem = this;
    document.getElementById("importe-fra").value = elem.innerHTML;

    reloadfracciones(ID_Pasa,1);

});

$(document.body).on('click', '.sel-fecha-mov', function () {

    var elem = this;
    var fecha = elem.innerHTML;
    var fc = fecha.split('/');
    fecha = fc[1] + "/" + fc[0] + "/" + fc[2];

    var Fe_In = AddDaysToDate(fecha, -15, "/");
    var Fe_Fi = AddDaysToDate(fecha, +15, "/");

    fc = Fe_In.split('/');
    document.getElementById("fe-in-fra").value = (fc[1] + '/' + fc[0] + '/' + fc[2]);

    fc = Fe_Fi.split('/');
    document.getElementById("fe-fi-fra").value = (fc[1] + '/' + fc[0] + '/' + fc[2]);

    var ID_Pasa = document.getElementById("ID_Pasa").value;

    reloadfracciones(ID_Pasa,1);

});

$(document.body).on('click', '.sel-fac-fra', function () {

    event.preventDefault();

    var ID_Pasa = document.getElementById("ID_Pasa").value;

    var idd = this.id;
    var strid = idd.split('_');

    document.getElementById("buscar-mov").value = strid[1];

    reloadmovimientos(ID_Pasa, 1);
});


$(document.body).on('change', '.sel-sel', function () {

    actualizarcontadormov();
});

function actualizarcontadormov() {

    var elems = document.getElementsByClassName('sel-sel');
    if (elems.length == 0) {
        // no hay elementos seleccionados
        document.getElementById("total-mov").innerHTML = "0,00 €";
        return;
    }

    var total = 0.0;

    for (t = 0; t < elems.length; t++) {
        var elem = elems[t];
        if (elem.checked) {
            var id = elem.id;
            var strid = id.split('_');
            var num = document.getElementById("sel-importe-mov_" + strid[1]).innerHTML;
            num = num.replace(".", "").replace(",", ".");
            total += parseFloat(num);
        }
    }

    document.getElementById("total-mov").innerHTML = formatearNumero(Number(total).toFixed(2)) + " €"; //Number(total).toFixed(2);
}

$(document.body).on('change', '.sel-sel-fra', function () {

    actualizarcontadorfra();
});

function actualizarcontadorfra() {

    var elems = document.getElementsByClassName('sel-sel-fra');
    if (elems.length == 0) {
        // no hay elementos seleccionados
        document.getElementById("total-fra").innerHTML = "0,00 €";
        return;
    }

    var total = 0.0;

    for (t = 0; t < elems.length; t++) {
        var elem = elems[t];
        if (elem.checked) {
            var id = elem.id;
            var strid = id.split('_');
            var num = document.getElementById("sel-importe-fra_" + strid[1]).innerHTML;
            num = num.replace(".", "").replace(",", ".");
            total += parseFloat(num);
        }
    }

    document.getElementById("total-fra").innerHTML = formatearNumero(Number(total).toFixed(2)) + " €"; //Number(total).toFixed(2);
}

$(document.body).on('click', '.vis-fra', function () {

    var id = this.id;
    var strid = id.split('_');

    var ID_Fra = strid[1];

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliacion/pasarelas/fraccion",
            data: { ID_Fra: ID_Fra },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    document.getElementById("info-fe-ve").innerHTML = response.fe_Ve;
                    document.getElementById("info-fac").innerHTML = response.fac;
                    document.getElementById("info-imp").innerHTML = response.imp;
                    document.getElementById("info-forma").innerHTML = response.forma;
                    if (response.cue != "") {
                        document.getElementById("info-cue").innerHTML = response.cue;
                        document.getElementById("ver-cue").style = "display:block;";
                    } else {
                        document.getElementById("ver-cue").style = "display:none !important;";
                    }

                    if (response.cueNeg != "") {
                        document.getElementById("info-cueneg").innerHTML = response.cueNeg;
                        document.getElementById("ver-cueneg").style = "display:block;";
                    } else {
                        document.getElementById("ver-cueneg").style = "display:none !important;";
                    }


                    if (response.incobreble) {
                        document.getElementById("ver-incobrable").style = "display:block;";
                    } else {
                        document.getElementById("ver-incobrable").style = "display:none;";
                    }

                    if (response.numFraT > 1) {
                        document.getElementById("info-numfra").innerHTML = response.numFra;
                        document.getElementById("info-numfrat").innerHTML = response.numFraT;
                        document.getElementById("info-total").innerHTML = response.total;

                        var elems = document.getElementsByClassName('ver-fra');
                        for (t = 0; t < elems.length; t++) {
                            elems[t].style = "display:block;";
                        }
                    } else {
                        var elems = document.getElementsByClassName('ver-fra');
                        for (t = 0; t < elems.length; t++) {
                            elems[t].style = "display:none !important;";
                        }
                    }

                    document.getElementById("info-fe-fa").innerHTML = response.fe_Fa;
                    document.getElementById("info-cli").innerHTML = response.emp;
                    document.getElementById("info-id-cli2").innerHTML = response.iD_Cli2;

                    if (response.cob) {
                        document.getElementById("info-fe-cob").innerHTML = response.fe_Cob;
                        document.getElementById("ver-fe-cob").style = "display:block;";
                    } else {
                        document.getElementById("ver-fe-cob").style = "display:none !important;";
                    }

                    document.getElementById("info-fra").style = "display:block;";

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


});

$(document.body).on('click', '.conciliar', function () {

    var ID_Pasa = document.getElementById("ID_Pasa").value;

    var elems = document.getElementsByClassName('sel-sel');
    if (elems.length == 0) {
        alert("No hay movimientos seleccionados");
        return;
    }

    var ID_Mov = "";

    var totalsel = 0.0;
    for (t = 0; t < elems.length; t++) {
        var elem = elems[t];
        if (elem.checked) {
            var id = elem.id;
            var strid = id.split('_');
            if (ID_Mov != "") { ID_Mov += "_"; }
            ID_Mov += strid[1];
            var num = document.getElementById("sel-importe-mov_" + strid[1]).innerHTML;
            num = num.replace(".", "").replace(",", ".");
            totalsel += parseFloat(num);
        }
    }

    if (ID_Mov == "") {
        alert("No hay movimientos seleccionados");
        return;
    }

    var ID_Fra = "";

    var elems = document.getElementsByClassName('sel-sel-fra');
    if (elems.length == 0) {
        alert("No hay fracciones seleccionadas");
        return;
    }

    var totalfra = 0.0;
    for (t = 0; t < elems.length; t++) {
        var elem = elems[t];
        if (elem.checked) {
            var id = elem.id;
            var strid = id.split('_');
            if (ID_Fra != "") { ID_Fra += "_"; }
            ID_Fra += strid[1];
            var num = document.getElementById("sel-importe-fra_" + strid[1]).innerHTML;
            num = num.replace(".", "").replace(",", ".");
            totalfra += parseFloat(num);
        }
    }

    if (ID_Fra == "") {
        alert("No hay fracciones seleccionadas");
        return;
    }

    totalfra = parseFloat(totalfra).toFixed(2);
    totalsel = parseFloat(totalsel).toFixed(2);

    if (totalfra != totalsel) {
        alert("El importe no está cuadrado.");
        return;
    }


    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliacion/pasarelas/conciliacion-clientes",
            data: { ID_Pasa: ID_Pasa, ID_Mov: ID_Mov, ID_Fra: ID_Fra },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {
                    document.getElementById("info-fra").style = "display:none;";

                    $.jGrowl(response.err.mensaje, $optionsMessageOK);

                    var PagMov = document.getElementById("movimientos-cuenta-paginador").getElementsByClassName("active")[0].firstChild.innerHTML;
                    var PagFra = document.getElementById("fracciones-facturas-paginador").getElementsByClassName("active")[0].firstChild.innerHTML;

                    reloadfracciones(ID_Pasa, PagFra);
                    reloadmovimientos(ID_Pasa, PagMov);

                    document.getElementById("total-mov").innerHTML = "0,00 €";
                    document.getElementById("total-fra").innerHTML = "0,00 €";

                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                return false;
            }
        });


});

// fin conciliacion