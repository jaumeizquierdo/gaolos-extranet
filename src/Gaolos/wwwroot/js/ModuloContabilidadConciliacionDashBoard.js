function DashBoardConciliacionFraCli(label) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliaciondashboardfraccionesclientes",
            data: { },
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                    document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                    document.getElementById(label + "_num").innerHTML = "n/d";
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<p class="mb-1 fw-5"><span class="text-info d-inline-block w-10 mr-2">' + response.det[t].id + '</span>' + response.det[t].det + '</p>';
                        }
                    }
                    document.getElementById(label + "_text").innerHTML = txt;
                    document.getElementById(label + "_num").innerHTML = response.num;
                    if (response.sema == 1) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else if (response.sema == 2) { document.getElementById(label + "_sema").src = "/images/semaforo-amarillo.svg"; }
                    else if (response.sema == 0) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else { document.getElementById(label + "_sema").src = "/images/semaforo-rojo.svg"; }
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                document.getElementById(label + "_num").innerHTML = "n/d";
                return false;
            }
        });
}

function DashBoardConciliacionFraProv(label) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliaciondashboardfraccionesproveedores",
            data: {},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                    document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                    document.getElementById(label + "_num").innerHTML = "n/d";
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<p class="mb-1 fw-5"><span class="text-info d-inline-block w-10 mr-2">' + response.det[t].id + '</span>' + response.det[t].det + '</p>';
                        }
                    }
                    document.getElementById(label + "_text").innerHTML = txt;
                    document.getElementById(label + "_num").innerHTML = response.num;
                    if (response.sema == 1) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else if (response.sema == 2) { document.getElementById(label + "_sema").src = "/images/semaforo-amarillo.svg"; }
                    else if (response.sema == 0) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else { document.getElementById(label + "_sema").src = "/images/semaforo-rojo.svg"; }
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                document.getElementById(label + "_num").innerHTML = "n/d";
                return false;
            }
        });
}

function DashBoardConciliacionRem(label) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliaciondashboardremesas",
            data: {},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                    document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                    document.getElementById(label + "_num").innerHTML = "n/d";
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<p class="mb-1 fw-5"><span class="text-info d-inline-block w-10 mr-2">' + response.det[t].id + '</span>' + response.det[t].det + '</p>';
                        }
                    }
                    document.getElementById(label + "_text").innerHTML = txt;
                    document.getElementById(label + "_num").innerHTML = response.num;
                    if (response.sema == 1) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else if (response.sema == 2) { document.getElementById(label + "_sema").src = "/images/semaforo-amarillo.svg"; }
                    else if (response.sema == 0) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else { document.getElementById(label + "_sema").src = "/images/semaforo-rojo.svg"; }
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                document.getElementById(label + "_num").innerHTML = "n/d";
                return false;
            }
        });
}

function DashBoardConciliacionMovCue(label) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliaciondashboardmovimientoscuentas",
            data: {},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                    document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                    document.getElementById(label + "_num").innerHTML = "n/d";
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<p class="mb-1 fw-5"><span class="text-info d-inline-block w-10 mr-2">' + response.det[t].id + '</span>' + response.det[t].det + '</p>';
                        }
                    }
                    document.getElementById(label + "_text").innerHTML = txt;
                    document.getElementById(label + "_num").innerHTML = response.num;
                    if (response.sema == 1) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else if (response.sema == 2) { document.getElementById(label + "_sema").src = "/images/semaforo-amarillo.svg"; }
                    else if (response.sema == 0) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else { document.getElementById(label + "_sema").src = "/images/semaforo-rojo.svg"; }
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                document.getElementById(label + "_num").innerHTML = "n/d";
                return false;
            }
        });
}

function DashBoardConciliacionMovTar(label) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliaciondashboardmovimientostarjetas",
            data: {},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                    document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                    document.getElementById(label + "_num").innerHTML = "n/d";
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<p class="mb-1 fw-5"><span class="text-info d-inline-block w-10 mr-2">' + response.det[t].id + '</span>' + response.det[t].det + '</p>';
                        }
                    }
                    document.getElementById(label + "_text").innerHTML = txt;
                    document.getElementById(label + "_num").innerHTML = response.num;
                    if (response.sema == 1) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else if (response.sema == 2) { document.getElementById(label + "_sema").src = "/images/semaforo-amarillo.svg"; }
                    else if (response.sema == 0) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else { document.getElementById(label + "_sema").src = "/images/semaforo-rojo.svg"; }
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                document.getElementById(label + "_num").innerHTML = "n/d";
                return false;
            }
        });
}

function DashBoardConciliacionPasaBan(label) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliaciondashboardpasarelasbancos",
            data: {},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                    document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                    document.getElementById(label + "_num").innerHTML = "n/d";
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<p class="mb-1 fw-5"><span class="text-info d-inline-block w-10 mr-2">' + response.det[t].id + '</span>' + response.det[t].det + '</p>';
                        }
                    }
                    document.getElementById(label + "_text").innerHTML = txt;
                    document.getElementById(label + "_num").innerHTML = response.num;
                    if (response.sema == 1) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else if (response.sema == 2) { document.getElementById(label + "_sema").src = "/images/semaforo-amarillo.svg"; }
                    else if (response.sema == 0) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else { document.getElementById(label + "_sema").src = "/images/semaforo-rojo.svg"; }
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                document.getElementById(label + "_num").innerHTML = "n/d";
                return false;
            }
        });
}

function DashBoardConciliacionPasaCli(label) {

    $.ajax(
        {
            type: "GET",
            url: "/modulo-contabilidad/conciliaciondashboardpasarelasclientes",
            data: {},
            contentType: "application/json;charset=utf-8",
            cache: false,
            datatype: "json",
            success: function (response, textStatus, jqXHR) {
                if (response.err.eserror == true) {
                    $.jGrowl(response.err.mensaje, $optionsMessageKO);
                    document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                    document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                    document.getElementById(label + "_num").innerHTML = "n/d";
                    return false;
                } else {
                    var txt = "";
                    if (response.det != null) {
                        for (t = 0; t < response.det.length; t++) {
                            txt += '<p class="mb-1 fw-5"><span class="text-info d-inline-block w-10 mr-2">' + response.det[t].id + '</span>' + response.det[t].det + '</p>';
                        }
                    }
                    document.getElementById(label + "_text").innerHTML = txt;
                    document.getElementById(label + "_num").innerHTML = response.num;
                    if (response.sema == 1) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else if (response.sema == 2) { document.getElementById(label + "_sema").src = "/images/semaforo-amarillo.svg"; }
                    else if (response.sema == 0) { document.getElementById(label + "_sema").src = "/images/semaforo-verde.svg"; }
                    else { document.getElementById(label + "_sema").src = "/images/semaforo-rojo.svg"; }
                    return;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.jGrowl(errorThrown, $optionsMessageKO);
                document.getElementById(label + "_sema").src = "/images/semaforo-apagado.svg";
                document.getElementById(label + "_text").innerHTML = "<img src='https://image.flaticon.com/icons/svg/291/291202.svg' style='width: 30%'>";
                document.getElementById(label + "_num").innerHTML = "n/d";
                return false;
            }
        });
}
