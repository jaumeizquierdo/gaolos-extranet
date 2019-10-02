$(document.body).on('change', '.cambio-pais', function () {

    var id_Pai = this.id;
    var strid = id_Pai.split('_');
    var id_CP = "UbiCP_" + strid[1];
    var id_Pro = "UbiPro_" + strid[1];
    var id_Pob = "UbiPob_" + strid[1];

    var Pai = document.getElementById(id_Pai).value;

    document.getElementById(id_Pro).innerHTML = "";
    document.getElementById(id_CP).innerHTML = "";
    document.getElementById(id_Pob).innerHTML = "";

    document.getElementById(id_Pro).disabled = true;
    document.getElementById(id_CP).disabled = true;
    document.getElementById(id_Pob).disabled = true;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/comun/direccioncambiopais",
                data: { Pai: Pai },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById(id_Pro);
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                var valor = response.det[t].det;
                                if (t === 0) valor = "";
                                txt += "<option value='" + valor + "'>" + response.det[t].det + "</option>";
                            }
                            elem.innerHTML = txt;
                            document.getElementById(id_Pro).disabled = false;
                        } else {
                            elem.innerHTML = "";
                            document.getElementById(id_Pro).disabled = true;
                        }
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

$(document.body).on('change', '.cambio-provincia', function () {

    var id_Pro = this.id;
    var strid = id_Pro.split('_');
    var id_Pai = "UbiPai_" + strid[1];
    var id_CP = "UbiCP_" + strid[1];
    var id_Pob = "UbiPob_" + strid[1];

    var Pai = document.getElementById(id_Pai).value;
    var Pro = document.getElementById(id_Pro).value;

    document.getElementById(id_CP).innerHTML = "";
    document.getElementById(id_Pob).innerHTML = "";

    document.getElementById(id_CP).disabled = true;
    document.getElementById(id_Pob).disabled = true;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/comun/direccioncambioprovincia",
                data: { Pai: Pai, Pro: Pro },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById(id_CP);
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                var valor = response.det[t].det;
                                if (t === 0) valor = "";
                                txt += "<option value='" + valor + "'>" + response.det[t].det + "</option>";
                            }
                            elem.innerHTML = txt;
                            document.getElementById(id_CP).disabled = false;
                        } else {
                            elem.innerHTML = "";
                            document.getElementById(id_CP).disabled = true;
                        }
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

$(document.body).on('change', '.cambio-cp', function () {

    var id_CP = this.id;
    var strid = id_CP.split('_');
    var id_Pai = "UbiPai_" + strid[1];
    var id_Pro = "UbiPro_" + strid[1];
    var id_Pob = "UbiPob_" + strid[1];

    var Pai = document.getElementById(id_Pai).value;
    var Pro = document.getElementById(id_Pro).value;
    var CP = document.getElementById(id_CP).value;

    document.getElementById(id_Pob).innerHTML = "";

    document.getElementById(id_Pob).disabled = true;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/comun/direccioncambiocp",
                data: { Pai: Pai, Pro: Pro, CP: CP },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var elem = document.getElementById(id_Pob);
                        var txt = "";
                        if (response.det !== null) {
                            for (t = 0; t < response.det.length; t++) {
                                var valor = response.det[t].det;
                                if (t === 0) valor = "";
                                if (response.det.length === 2 && t === 1) {
                                    txt += "<option value='" + valor + "' selected>" + response.det[t].det + "</option>";
                                } else {
                                    txt += "<option value='" + valor + "'>" + response.det[t].det + "</option>";
                                }
                            }
                            elem.innerHTML = txt;
                            document.getElementById(id_Pob).disabled = false;
                        } else {
                            elem.innerHTML = "";
                            document.getElementById(id_Pob).disabled = true;
                        }
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
