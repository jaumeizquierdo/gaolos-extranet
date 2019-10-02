


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
            url: "/modulo-contabilidad/conciliacion/pasarelas/movimientos",
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
                            txt += '<td><p class="mb-0 fw-5">' + response.det[t].conc + '</p></td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].pago + '</p></td>';
                            txt += '<td class="text-right"><a href="#" class="fw-5 sel-importe-mov" id="sel-importe-mov_' + response.det[t].iD_Mov + '">' + response.det[t].ingreso + '</a></td>';
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
            url: "/modulo-contabilidad/conciliacion/pasarelas/detalles",
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
                            txt += '<tr class="vis-fra" id="vis-fra_' + response.det[t].iD_Mov + '">';
                            txt += '<td><p class="mb-0 fw-5">' + response.det[t].fe_Val + '</p></td>';
                            txt += '<td>' + response.det[t].conc + '</td>';
                            txt += '<td class="text-right"><p class="mb-0 fw-5">' + response.det[t].dev + '</p></td>';
                            txt += '<td class="text-right"><a href="#" class="fw-5 sel-importe-fra" id="sel-importe-fra_' + response.det[t].iD_Mov + '">' + response.det[t].imp + '</a></td>';
                            txt += '<td class="text-center"><div class="custom-control custom-checkbox mb-0 ml-3 mt-0"><input type="checkbox" class="custom-control-input sel-sel-fra" id="sel-sel-importe-fra_' + response.det[t].iD_Rem + '"><label class="custom-control-label" for="sel-sel-importe-fra_' + response.det[t].iD_Rem + '"></label></div></td>';
                            txt += '</tr>';
                        }
                        document.getElementById("fracciones-facturas").innerHTML = txt;
                        document.getElementById("fracciones-facturas-paginador").innerHTML = response.paginador;
                    } else {
                        txt = "<tr><td colspan='5'><p>No hay movimientos</p></td></tr>";
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

    var ID_Cue = document.getElementById("ID_Pasa").value;

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

    var ID_Mov = strid[1];
    var ID_Pasa = document.getElementById("ID_Pasa").value;

    Pace.restart();

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliacion/pasarelas/movimiento",
            data: { ID_Mov: ID_Mov, ID_Pasa: ID_Pasa },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    return false;
                } else {

                    document.getElementById("info-fe-val").innerHTML = response.fe_Val;

                    document.getElementById("info-fe-al").innerHTML = response.fe_Al;
                    document.getElementById("info-terminal").innerHTML = response.terminal;
                    document.getElementById("info-tipoope").innerHTML = response.tipoOpe;
                    document.getElementById("info-numauto").innerHTML = response.numAuto;
                    document.getElementById("info-id-transelec2").innerHTML = response.iD_TransElec2;
                    document.getElementById("info-tipopago").innerHTML = response.tipoPago;
                    document.getElementById("info-imp").innerHTML = response.imp;
                    document.getElementById("info-moneda-imp").innerHTML = response.monedaImp;
                    document.getElementById("info-dev").innerHTML = response.dev;
                    document.getElementById("info-moneda-dev").innerHTML = response.monedaDev;
                    document.getElementById("info-numtarj").innerHTML = response.numTarj;

                    document.getElementById("info-paitar").innerHTML = response.paiTar;
                    document.getElementById("info-escredito").innerHTML = response.esCredito;
                    document.getElementById("info-usu").innerHTML = response.usuario;
                    document.getElementById("info-entrada").innerHTML = response.entrada;
                    document.getElementById("info-ip").innerHTML = response.ip;
                    document.getElementById("info-pais").innerHTML = response.pais;
                    document.getElementById("info-codpai").innerHTML = response.codPai;

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

    var ID_Cue = document.getElementById("ID_Cue").value;

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
            url: "/modulo-contabilidad/conciliacion/pasarelas/conciliacion",
            data: { ID_Cue: ID_Cue, ID_Mov: ID_Mov, ID_Fra: ID_Fra },
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

                    reloadfracciones(PagFra);
                    reloadmovimientos(ID_Cue, PagMov);

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