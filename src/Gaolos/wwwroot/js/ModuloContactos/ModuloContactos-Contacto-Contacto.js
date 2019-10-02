function hideAll() {
    $("#sindatos").hide();
    $("#verdir").hide();
    $("#vermail").hide();
    $("#verweb").hide();
    $("#vercentro").hide();
    $("#vertel").hide();
    $("#ver-maps").hide();
}


$(document.body).on('click', '.contacto-web-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta web?")) return false;

    var ID_Cont2 = document.getElementById('ID_Cont2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Web = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/eliminar-web",
                data: { ID_Cont2: ID_Cont2, ID_Web: ID_Web },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.contacto-web-guardar', function () {

    event.preventDefault(event);

    var ID_Cont2 = document.getElementById('ID_Cont2').value;
    var ID_Web = document.getElementById("ID_Web").value;
    var Web = document.getElementById('Web').value;
    var ObsWeb = document.getElementById('ObsWeb').value;

    Pace.restart();

    if (ID_Web === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/nueva-web",
                    data: { ID_Cont2: ID_Cont2, Web: Web, ObsWeb: ObsWeb },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });

    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/guardar-web",
                    data: { ID_Cont2: ID_Cont2, ID_Web: ID_Web, Web: Web, ObsWeb: ObsWeb },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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


});

$(document.body).on('click', '.sel-web', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Web = strid[1];

    CargarWeb(ID_Web);

});

$(document.body).on('click', '.nueva-web', function () {

    event.preventDefault(event);

    document.getElementById("web-titulo").innerHTML = "Nueva web";
    document.getElementById("ID_Web").value = "0";
    document.getElementById("Web").value = "";
    document.getElementById("ObsWeb").value = "";

    hideAll();
    $("#verweb").show();
    $("#verweb").addClass("animated fadeIn");

});

function CargarWeb(ID_Web) {

    var ID_Cont2 = document.getElementById("ID_Cont2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/web",
                data: { ID_Cont2: ID_Cont2, ID_Web: ID_Web },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("web-titulo").innerHTML = "Modificar web";
                        document.getElementById("ID_Web").value = ID_Web;
                        document.getElementById("Web").value = response.datoS1;
                        document.getElementById("ObsWeb").value = response.datoS2;

                        hideAll();
                        $("#verweb").show();
                        $("#vercentro").addClass("animated fadeIn");

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

$(document.body).on('click', '.contacto-centro-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este centro de coste?")) return false;

    var ID_Cont2 = document.getElementById('ID_Cont2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Centro2 = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/eliminar-centro",
                data: { ID_Cont2: ID_Cont2, ID_Centro2: ID_Centro2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.contacto-centro-guardar', function () {

    event.preventDefault(event);

    var ID_Cont2 = document.getElementById("ID_Cont2").value;
    var ID_Centro2 = document.getElementById("ID_CentroMod").value;
    var Centro = document.getElementById('Centro').value;
    var ObsCentro = document.getElementById('ObsCentro').value;
    var Referencia = document.getElementById('Referencia').value;

    Pace.restart();

    if (ID_Centro2 === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/nuevo-centro",
                    data: { ID_Cont2: ID_Cont2,Centro: Centro, ObsCentro: ObsCentro, Referencia: Referencia },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });

    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/guardar-centro",
                    data: { ID_Cont2: ID_Cont2, ID_Centro2: ID_Centro2, Centro: Centro, ObsCentro: ObsCentro, Referencia: Referencia },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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


});

$(document.body).on('click', '.sel-centro', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Centro2 = strid[1];

    CargarCentro(ID_Centro2);

});

$(document.body).on('click', '.nuevo-centro', function () {

    event.preventDefault(event);

    document.getElementById("centro-titulo").innerHTML = "Nuevo centro de coste";
    document.getElementById("ID_CentroMod").value = "0";
    document.getElementById("Centro").value = "";
    document.getElementById("ObsCentro").value = "";
    document.getElementById("Referencia").value = "";
    document.getElementById("altaFieldCentro").style.display = 'none';

    hideAll();
    $("#vercentro").show();
    $("#vercentro").addClass("animated fadeIn");

});

function CargarCentro(ID_Centro2) {

    var ID_Cont2 = document.getElementById("ID_Cont2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/centro",
                data: { ID_Cont2: ID_Cont2, ID_Centro2: ID_Centro2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {


                        document.getElementById("centro-titulo").innerHTML = "Modificar centro de coste";
                        document.getElementById("ID_CentroMod").value = ID_Centro2;
                        document.getElementById("Centro").value = response.datoS1;
                        document.getElementById("ObsCentro").value = response.datoS2;

                        hideAll();
                        $("#vercentro").show();
                        $("#vercentro").addClass("animated fadeIn");

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

$(document.body).on('click', '.sel-mail-fac', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];
    var ID_Cont2 = document.getElementById("ID_Cont2").value;
    var Sel = this.checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/sel-mail-facturas",
                data: { ID_Cont2: ID_Cont2, ID_Mail: ID_Mail, Sel: Sel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.sel-mail-com', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];
    var ID_Cont2 = document.getElementById("ID_Cont2").value;
    var Sel = this.checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/sel-mail-comercial",
                data: { ID_Cont2: ID_Cont2, ID_Mail: ID_Mail, Sel: Sel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.contacto-mail-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este mail?")) return false;

    var ID_Cont2 = document.getElementById('ID_Cont2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/eliminar-mail",
                data: { ID_Cont2: ID_Cont2, ID_Mail: ID_Mail },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.contacto-mail-guardar', function () {

    event.preventDefault(event);

    var ID_Cont2 = document.getElementById('ID_Cont2').value;
    var ID_Mail = document.getElementById("ID_Mail").value;
    var Mail = document.getElementById('Mail').value;
    var ObsMail = document.getElementById('ObsMail').value;

    Pace.restart();

    if (ID_Mail === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/nuevo-mail",
                    data: { ID_Cont2: ID_Cont2, Mail: Mail, ObsMail: ObsMail },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });

    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/guardar-mail",
                    data: { ID_Cont2: ID_Cont2, ID_Mail: ID_Mail, Mail: Mail, ObsMail: ObsMail },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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


});

function CargarMail(ID_Mail) {

    var ID_Cont2 = document.getElementById("ID_Cont2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/mail",
                data: { ID_Cont2: ID_Cont2, ID_Mail: ID_Mail },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("mail-titulo").innerHTML = "Modificar mail";
                        document.getElementById("ID_Mail").value = ID_Mail;
                        document.getElementById("Mail").value = response.datoS1;
                        document.getElementById("ObsMail").value = response.datoS2;
                        document.getElementById("AltaMail").innerText = response.datoS3 + " - " + response.datoS4;
                        document.getElementById("altaFieldMail").style.display = 'flex';

                        hideAll();
                        $("#vermail").show();
                        $("#vermail").addClass("animated fadeIn");

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

$(document.body).on('click', '.sel-mail', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];

    CargarMail(ID_Mail);

});

$(document.body).on('click', '.nuevo-mail', function () {

    event.preventDefault(event);

    document.getElementById("mail-titulo").innerHTML = "Nuevo mail";
    document.getElementById("ID_Mail").value = "0";
    document.getElementById("Mail").value = "";
    document.getElementById("ObsMail").value = "";
    document.getElementById("altaFieldMail").style.display = 'none';

    hideAll();
    $("#vermail").show();
    $("#vermail").addClass("animated fadeIn");
});

function CargarTelefono(ID_Tel) {

    var ID_Cont2 = document.getElementById("ID_Cont2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/telefono",
                data: { ID_Cont2: ID_Cont2, ID_Tel: ID_Tel },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("tel-titulo").innerHTML = "Modificar teléfono";
                        document.getElementById("ID_Tel").value = ID_Tel;
                        document.getElementById("Tel").value = response.datoS1;
                        document.getElementById("ObsTel").value = response.datoS2;
                        document.getElementById("AltaTelefono").innerText = `${response.datoS3} - ${response.datoS4}`;
                        document.getElementById("altaFieldTelefono").style.display = 'flex';


                        hideAll()
                        $("#vertel").show();
                        $("#vertel").addClass("animated fadeIn");

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

$(document.body).on('click', '.contacto-tel-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este teléfono?")) return false;

    var ID_Cont2 = document.getElementById('ID_Cont2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/eliminar-tel",
                data: { ID_Cont2: ID_Cont2, ID_Tel: ID_Tel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.cliente-tel-pred', function () {

    event.preventDefault(event);

    var ID_Cont2 = document.getElementById('ID_Cont2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/pred-tel",
                data: { ID_Cont2: ID_Cont2, ID_Tel: ID_Tel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.contacto-tel-guardar', function () {

    event.preventDefault(event);

    var ID_Cont2 = document.getElementById('ID_Cont2').value;
    var ID_Tel = document.getElementById("ID_Tel").value;
    var Tel = document.getElementById('Tel').value;
    var ObsTel = document.getElementById('ObsTel').value;

    Pace.restart();

    if (ID_Tel === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/nuevo-tel",
                    data: { ID_Cont2: ID_Cont2, Tel: Tel, ObsTel: ObsTel },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });

    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/guardar-tel",
                    data: { ID_Cont2: ID_Cont2, ID_Tel: ID_Tel, Tel: Tel, ObsTel: ObsTel },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
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


});

$(document.body).on('click', '.sel-tel', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tel = strid[1];

    CargarTelefono(ID_Tel);

});

$(document.body).on('click', '.nuevo-tel', function () {

    event.preventDefault(event);

    document.getElementById("tel-titulo").innerHTML = "Nuevo teléfono";
    document.getElementById("ID_Tel").value = "0";
    document.getElementById("Tel").value = "";
    document.getElementById("ObsTel").value = "";
    document.getElementById("altaFieldTelefono").style.display = 'none';

    hideAll();
    $("#vertel").show();
    $("#vertel").addClass("animated fadeIn");

});

// Direcciones

function CargarDireccion(ID_Dir) {

    var ID_Cont2 = document.getElementById("ID_Cont2").value;

    var numDir = "1";

    //var id_Pai = "UbiPai_1";
    //var id_CP = "UbiCP_1";
    //var id_Pro = "UbiPro_1";
    //var id_Pob = "UbiPob_1";

    //document.getElementById(id_Pro).innerHTML = "";
    //document.getElementById(id_CP).innerHTML = "";
    //document.getElementById(id_Pob).innerHTML = "";

    //document.getElementById(id_Pro).disabled = true;
    //document.getElementById(id_CP).disabled = true;
    //document.getElementById(id_Pob).disabled = true;

    //document.getElementById("verdir").style.display = "none";

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/direccion",
                data: { ID_Cont2: ID_Cont2, ID_Dir: ID_Dir },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        if (response.iD_Dir === 0) {
                            document.getElementById("dir-titulo").innerHTML = "Nueva dirección";
                        } else {
                            document.getElementById("dir-titulo").innerHTML = "Modificar dirección";
                        }

                        document.getElementById("ID_Dir").value = response.iD_Dir;
                        document.getElementById("Dir").value = response.dir;
                        document.getElementById("Obs").value = response.obs;
                        document.getElementById("EmpAux").value = response.empAux;
                        document.getElementById("Horario").value = response.horario;
                        document.getElementById("Lat").value = response.latitud;
                        document.getElementById("Lon").value = response.longitud;

                        var elem = document.getElementById('ID_Centro');
                        var txt = "";
                        if (response.centros !== null) {
                            if (response.centros.length > 1) {
                                for (t = 0; t < response.centros.length; t++) {
                                    if (response.iD_Centro === response.centros[t].det) {
                                        txt += "<option value='" + response.centros[t].id + "' selected>" + response.centros[t].det + "</option>";
                                    } else {
                                        txt += "<option value='" + response.centros[t].id + "'>" + response.centros[t].det + "</option>";
                                    }
                                }
                                elem.innerHTML = txt;
                                elem.disabled = false;
                            } else {
                                elem.innerHTML = "";
                                elem.disabled = true;
                            }
                        } else {
                            elem.innerHTML = "";
                            elem.disabled = true;
                        }

                        if (response.pais === null) {
                            txt = '<p>No hay paises definidos</p>';
                        } else {
                            txt = '<select class="custom-select cambio-pais-cli-dir" id="UbiPai_' + numDir + '">';
                            for (t = 0; t < response.pais.length; t++) {
                                if (response.pai === response.pais[t].datoS1 && response.pais[t].datoS1 !== "") {
                                    txt += "<option value='" + response.pais[t].datoS1 + "' selected>" + response.pais[t].datoS2 + "</option>";
                                } else {
                                    txt += "<option value='" + response.pais[t].datoS1 + "'>" + response.pais[t].datoS2 + "</option>";
                                }
                            }
                            txt += '</select>';
                        }
                        document.getElementById("DirPai" + numDir).innerHTML = txt;


                        if (response.pros === null) {
                            if (response.pai === "") {
                                txt = '';
                            } else {
                                txt = '<input type="text" class="form-control" id="UbiPro_' + numDir + '" aria-describedby="" value="' + response.pro + '" maxlength="75">';
                            }
                        } else {
                            txt = '<select class="custom-select cambio-provincia-cli-dir" id="UbiPro_' + numDir + '">';
                            if (response.pros !== null) {
                                for (t = 0; t < response.pros.length; t++) {
                                    if (response.pro === response.pros[t].datoS1 && response.pros[t].datoS1 !== "") {
                                        txt += "<option value='" + response.pros[t].datoS1 + "' selected>" + response.pros[t].datoS2 + "</option>";
                                    } else {
                                        txt += "<option value='" + response.pros[t].datoS1 + "'>" + response.pros[t].datoS2 + "</option>";
                                    }
                                }
                            }
                            txt += '</select>';
                        }
                        document.getElementById("DirPro" + numDir).innerHTML = txt;


                        if (response.cPs === null) {
                            if (response.pai === "") {
                                txt = '';
                            } else {
                                txt = '<input type="text" class="form-control" id="UbiCP_' + numDir + '" aria-describedby="" value="' + response.cp + '" maxlength="75">';
                            }
                        } else {
                            txt = '<select class="custom-select cambio-cp-cli-dir" id="UbiCP_' + numDir + '">';
                            if (response.cPs !== null) {
                                for (t = 0; t < response.cPs.length; t++) {
                                    if (response.cp === response.cPs[t].datoS1 && response.cPs[t].datoS1 !== "") {
                                        txt += "<option value='" + response.cPs[t].datoS1 + "' selected>" + response.cPs[t].datoS2 + "</option>";
                                    } else {
                                        txt += "<option value='" + response.cPs[t].datoS1 + "'>" + response.cPs[t].datoS2 + "</option>";
                                    }
                                }
                            }
                            txt += '</select>';
                        }
                        document.getElementById("DirCP" + numDir).innerHTML = txt;

                        if (response.pobs === null) {
                            if (response.pai === "") {
                                txt = '';
                            } else {
                                txt = '<input type="text" class="form-control" id="UbiPob_' + numDir + '" aria-describedby="" value="' + response.pob + '" maxlength="75">';
                            }
                        } else {
                            txt = '<select class="custom-select" id="UbiPob_' + numDir + '">';
                            if (response.pobs !== null) {
                                for (t = 0; t < response.pobs.length; t++) {
                                    if (response.pob === response.pobs[t].datoS1 && response.pobs[t].datoS1 !== "") {
                                        txt += "<option value='" + response.pobs[t].datoS1 + "' selected>" + response.pobs[t].datoS2 + "</option>";
                                    } else {
                                        txt += "<option value='" + response.pobs[t].datoS1 + "'>" + response.pobs[t].datoS2 + "</option>";
                                    }
                                }
                            }
                            txt += '</select>';
                        }
                        document.getElementById("DirPob" + numDir).innerHTML = txt;

                        document.getElementById("Lat").value = response.latitud;
                        document.getElementById("Lon").value = response.longitud;

                        var map;
                        var marker;
                        var point;

                        if (response.latitud !== "" && response.longitud !== "") {
                            var lat = parseFloat(response.latitud);
                            var lng = parseFloat(response.longitud);

                            point = { lat: lat, lng: lng };
                            map = new google.maps.Map(document.getElementById('clientes-general-map'), {
                                zoom: 16,
                                center: point
                            });
                            marker = new google.maps.Marker({
                                position: point,
                                map: map
                            });
                        } else {
                            point = {};
                            map = new google.maps.Map(document.getElementById('clientes-general-map'), {
                                zoom: 16,
                                center: point
                            });
                            marker = new google.maps.Marker({
                                position: point,
                                map: map
                            });
                        }

                        hideAll();
                        $("#verdir").show();
                        $("#ver-maps").show();
                        $("#verdir").addClass("animated fadeIn");
                        $("#ver-maps").addClass("animated fadeIn");

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

$(document.body).on('click', '.nueva-dir', function () {

    event.preventDefault(event);

    CargarDireccion(0);

});

$(document.body).on('click', '.dir-geo-save', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Dir = document.getElementById('ID_Dir').value;
    var Lat = document.getElementById('Lat').value;
    var Lon = document.getElementById('Lon').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/direccion-geo-save",
                data: { ID_Cli2: ID_Cli2, ID_Dir: ID_Dir, Lat: Lat, Lon: Lon },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return;
                }
            });
    });
});

$(document.body).on('click', '.dir-geo', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Dir = document.getElementById('ID_Dir').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/cliente/cliente/direccion-geo",
                data: { ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("Lat").value = response.datoS1;
                        document.getElementById("Lon").value = response.datoS2;

                        var lat = parseFloat(response.datoS1);
                        var lng = parseFloat(response.datoS2);

                        var point = { lat: lat, lng: lng };
                        var map = new google.maps.Map(document.getElementById('clientes-general-map'), {
                            zoom: 16,
                            center: point
                        });
                        var marker = new google.maps.Marker({
                            position: point,
                            map: map
                        });

                        return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return;
                }
            });
    });


});

$(document.body).on('click', '.contacto-dir-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta dirección?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];
    var ID_Cont2 = document.getElementById("ID_Cont2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-contactos/contactos/contacto/direccion-eliminar",
                data: { ID_Cont2: ID_Cont2, ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-contactos/contactos/contacto?ID_Cli2=" + ID_Cont2, "_self");
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

$(document.body).on('click', '.sel-dir-docu', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];
    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var Sel = this.checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cambiar-direccion-documentacion",
                data: { ID_Cli2: ID_Cli2, ID_Dir: ID_Dir, Sel: Sel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.sel-dir-com', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];
    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cambiar-direccion-comercial",
                data: { ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.sel-dir-fis', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];
    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cambiar-direccion-fiscal",
                data: { ID_Cli2: ID_Cli2, ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente?ID_Cli2=" + ID_Cli2, "_self");
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


$(document.body).on('click', '.contacto-direccion-guardar', function () {

    event.preventDefault(event);

    var ID_Cont2 = document.getElementById('ID_Cont2').value;
    var ID_Dir = document.getElementById('ID_Dir').value;
    var Dir = document.getElementById('Dir').value;
    var Pai = document.getElementById('UbiPai_1').value;
    var Pro = document.getElementById('UbiPro_1').value;
    var CP = document.getElementById('UbiCP_1').value;
    var Pob = document.getElementById('UbiPob_1').value;
    var Obs = document.getElementById('Obs').value;
    var ID_Centro = document.getElementById('ID_Centro').value;
    var EmpAux = document.getElementById('EmpAux').value;
    var Horario = document.getElementById('Horario').value;

    if (ID_Dir === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/nueva-direccion",
                    data: { ID_Cont2: ID_Cont2, Dir: Dir, Pai: Pai, Pro: Pro, CP: CP, Pob: Pob, Obs: Obs, ID_Centro: ID_Centro, EmpAux: EmpAux, Horario: Horario },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-contactos/contactos/contacto?ID_Cont2=" + ID_Cont2, "_self");
                            return true;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return false;
                    }
                });
        });
    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-contactos/contactos/contacto/guardar-direccion",
                    data: { ID_Cont2: ID_Cont2, ID_Dir: ID_Dir, Dir: Dir, Pai: Pai, Pro: Pro, CP: CP, Pob: Pob, Obs: Obs, ID_Centro: ID_Centro, EmpAux: EmpAux, Horario: Horario },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            document.getElementById('dir_' + ID_Dir).innerHTML = Dir;
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


});

$(document.body).on('click', '.sel-dir', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];

    CargarDireccion(ID_Dir);

});

$(document.body).on('change', '.cambio-pais-cli-dir', function () {

    var id_Pai = this.id;
    var strid = id_Pai.split('_');
    var numDir = strid[1];

    var Pai = document.getElementById("UbiPai_" + numDir).value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/direccioncambiopais",
                data: { Pai: Pai },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        if (response.det === null) {
                            if (Pai === "") {
                                txt = '';
                                document.getElementById("DirPro" + numDir).innerHTML = txt;
                            } else {
                                txt = '<input type="text" class="form-control" id="UbiPro_' + numDir + '" aria-describedby="" value="" maxlength="75">';
                                document.getElementById("DirPro" + numDir).innerHTML = txt;
                                txt = '<input type="text" class="form-control" id="UbiCP_' + numDir + '" aria-describedby="" value="" maxlength="75">';
                                document.getElementById("DirCP" + numDir).innerHTML = txt;
                                txt = '<input type="text" class="form-control" id="UbiPob_' + numDir + '" aria-describedby="" value="" maxlength="75">';
                                document.getElementById("DirPob" + numDir).innerHTML = txt;
                            }
                        } else {
                            txt = '<select class="custom-select cambio-provincia-cli-dir" id="UbiPro_' + numDir + '">';
                            for (t = 0; t < response.det.length; t++) {
                                txt += "<option value='" + response.det[t].datoS1 + "'>" + response.det[t].datoS2 + "</option>";
                            }
                            txt += '</select>';
                            document.getElementById("DirPro" + numDir).innerHTML = txt;
                            document.getElementById("DirCP" + numDir).innerHTML = "";
                            document.getElementById("DirPob" + numDir).innerHTML = "";
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

$(document.body).on('change', '.cambio-provincia-cli-dir', function () {

    var id_Pro = this.id;
    var strid = id_Pro.split('_');
    var numDir = strid[1];

    var Pai = document.getElementById("UbiPai_" + numDir).value;
    var Pro = document.getElementById("UbiPro_" + numDir).value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/direccioncambioprovincia",
                data: { Pai: Pai, Pro: Pro },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        if (response.det === null) {
                            txt = '<input type="text" class="form-control" id="UbiCP_' + numDir + '" aria-describedby="" value="" maxlength="75">';
                            document.getElementById("DirCP" + numDir).innerHTML = txt;
                        } else {
                            txt = '<select class="custom-select cambio-cp-cli-dir" id="UbiCP_' + numDir + '">';
                            for (t = 0; t < response.det.length; t++) {
                                txt += "<option value='" + response.det[t].datoS1 + "'>" + response.det[t].datoS2 + "</option>";
                            }
                            txt += '</select>';
                            document.getElementById("DirCP" + numDir).innerHTML = txt;
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

$(document.body).on('change', '.cambio-cp-cli-dir', function () {

    var id_CP = this.id;
    var strid = id_CP.split('_');
    var numDir = strid[1];

    var Pai = document.getElementById("UbiPai_" + numDir).value;
    var Pro = document.getElementById("UbiPro_" + numDir).value;
    var CP = document.getElementById("UbiCP_" + numDir).value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-contactos/contactos/contacto/direccioncambiocp",
                data: { Pai: Pai, Pro: Pro, CP: CP },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        if (response.det === null) {
                            txt = '<input type="text" class="form-control" id="UbiPob_' + numDir + '" aria-describedby="" value="" maxlength="75">';
                            document.getElementById("DirPob" + numDir).innerHTML = txt;
                        } else {
                            txt = '<select class="custom-select" id="UbiPob_' + numDir + '">';
                            for (t = 0; t < response.det.length; t++) {
                                txt += "<option value='" + response.det[t].datoS1 + "'>" + response.det[t].datoS2 + "</option>";
                            }
                            txt += '</select>';
                            document.getElementById("DirPob" + numDir).innerHTML = txt;
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


// Direcciones fin





// Módulo Clientes > Cliente > relacionado con - guardar relacion

$(document.body).on('click', '.relacion-guardar', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Rel = strid[1];

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var CobEnEspera = document.getElementById("CobEnEspera_" + ID_Rel).checked;
    var EnvFac = document.getElementById("EnvFac_" + ID_Rel).checked;
    var Facturar = document.getElementById("Facturar_" + ID_Rel).checked;
    var ForPag = document.getElementById("ForPag_" + ID_Rel).checked;
    var Precios = document.getElementById("Precios_" + ID_Rel).checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cliente-relacionado-con/guardar-vinculacion",
                data: { ID_Cli2: ID_Cli2, ID_Rel: ID_Rel, CobEnEspera: CobEnEspera, EnvFac: EnvFac, Facturar: Facturar, ForPag: ForPag, Precios: Precios },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cliente-emprelvin-del', function () {

    event.preventDefault(event);


    var idd = this.id;
    var strid = idd.split('_');

    document.getElementById("ID_RelEli").value = strid[1];
    document.getElementById("MotivoEli").value = "";

    $("#eliminarContacto").modal({ show: true });

});

$(document.body).on('click', '.cliente-emprelvin-del-confirmar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var ID_Rel = document.getElementById("ID_RelEli").value;
    var Motivo = document.getElementById("MotivoEli").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cliente-relacionado-con/eliminar-vinculacion-_Falta_Contacto",
                data: { ID_Cli2: ID_Cli2, ID_Rel: ID_Rel, Motivo: Motivo },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $("#eliminarContacto").modal({ show: false });
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/cliente-relacionado-con?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-emprelvin', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var ID_Cli2Rel = document.getElementById("vemprelvin").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cliente-relacionado-con/vincular",
                data: { ID_Cli2: ID_Cli2, ID_Cli2Rel: ID_Cli2Rel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/cliente-relacionado-con?ID_Cli2=" + ID_Cli2, "_self");
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

// Módulo Clientes > Cliente > Relacionado con > Buscar cliente

$(function () {

    $("#emprelvin").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
            return false;
        }
    });

    $("#emprelvin").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            ActualizarCalendario();
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#emprelvin").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/cliente-relacionado-con/buscar-cliente",
                data: { buscar: encodeURI(document.getElementById('emprelvin').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var result = [];

                        if (response.det !== null) {
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

// Módulo Clientes > Cliente > Clientes relacionados - Herencia

$(document.body).on('click', '.herencia-guardar', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var Propagar = false;
    if (strid[1] === "1") Propagar = true;

    if (Propagar) if (!confirm("¿Desea propagar estos cambios a todos los clientes relacionados?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var CobEnEspera = document.getElementById("CobEnEspera").checked;
    var EnvFac = document.getElementById("EnvFac").checked;
    var Facturar = document.getElementById("Facturar").checked;
    var ForPag = document.getElementById("ForPag").checked;
    var Precios = document.getElementById("Precios").checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/clientes-relacionados/herencia-guardar",
                data: { ID_Cli2: ID_Cli2, Propagar: Propagar, CobEnEspera: CobEnEspera, EnvFac: EnvFac, Facturar: Facturar, ForPag: ForPag, Precios: Precios },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        var msg = response.err.mensaje;
                        if (response.obj.datoB) {
                            if (response.obj.datoI === 0) {
                                msg += ". Se han localizado clientes relacionados.";
                            } else if (response.obj.datoI === 1) {
                                msg += ". Se ha actualizado " + response.obj.datoI + " cliente.";
                            } else {
                                msg += ". Se han actualizado " + response.obj.datoI + " clientes.";
                            }
                        }
                        $.jGrowl(msg, $optionsMessageOK);

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

// Módulo Clientes > Cliente > Bloquear Cliente

$(document.body).on('click', '.cliente-bloquear', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea bloquear este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var MotBaj = document.getElementById("MotBaj").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/bloqueo/cliente-bloquear",
                data: { ID_Cli2: ID_Cli2, MotBaj: MotBaj},
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/bloqueo?ID_Cli2=" + ID_Cli2, "_self");
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

// Módulo Clientes > Cliente > Desbloquear Cliente

$(document.body).on('click', '.cliente-desbloquear', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea desbloquear este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/bloqueo/cliente-desbloquear",
                data: { ID_Cli2: ID_Cli2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/bloqueo?ID_Cli2=" + ID_Cli2, "_self");
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

// Módulo Clientes > Cliente > Baja Cliente

$(document.body).on('click', '.cliente-baja', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea dar de baja definitiva este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/bloqueo/cliente-baja",
                data: { ID_Cli2: ID_Cli2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/", "_self");
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

$(document.body).on('click', '.cliente-moneda-guardar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar la moneda de este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Mon = document.getElementById("ID_Mon").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/moneda-guardar",
                data: { ID_Cli2: ID_Cli2, ID_Mon: ID_Mon },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cliente-req-guardar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar el tipo de REQ de este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var REQ = document.getElementById("REQ").checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/req-guardar",
                data: { ID_Cli2: ID_Cli2, REQ: REQ },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        document.getElementById("REQ").checked = response.obj.datoB;
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

$(document.body).on('click', '.cliente-tipoiva-guardar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar el tipo de IVA de este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_TipoIVA = document.getElementById("ID_TipoIVA").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/tipoiva-guardar",
                data: { ID_Cli2: ID_Cli2, ID_TipoIVA: ID_TipoIVA },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.sel-pres', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Pres2 = strid[1];

    CargarPresupuesto(ID_Pres2);

});

function CargarPresupuesto(ID_Pres2) {

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/clientes/cliente/situacion/presupuestos/presupuesto",
                data: { ID_Cli2: ID_Cli2, ID_Pres2: ID_Pres2 },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        document.getElementById("numpres").innerHTML = response.iD_Pres2;
                        
                        var txt = '';
                        txt += '<p class="text-left mb-0"><span class="fw-5">Fecha: </span> ' + response.fe_Al + ' - ' + response.usuAl + '</p>';
                        if (response.usuAsig !== "") txt += '<p class="text-left mb-0"><span class="fw-5">Asignado a: </span> ' + response.usuAsig + '</p>';
                        if (response.usuCom !== "") txt += '<p class="text-left mb-0"><span class="fw-5">Comercial: </span> ' + response.usuCom + '</p>';
                        if (response.breve !== "") txt += '<p class="text-left mb-0"><span class="fw-5">Breve: </span> ' + response.breve + '</p>';
                        txt += '<p class="text-left mb-0"><span class="fw-5">Total: </span> ' + response.total + '</p>';
                        if (response.obs !== "") txt += '<p class="text-left mb-0"><span class="fw-5">Observaciones: </span> ' + response.obs + '</p>';
                        if (response.obsPriv !== "") txt += '<p class="text-left mb-0"><span class="fw-5">Observaciones privadas: </span> ' + response.obsPriv + '</p>';
                        if (response.att !== "") txt += '<p class="text-left mb-0"><span class="fw-5">A la atención de: </span> ' + response.att + '</p>';
                        if (response.tel !== "") txt += '<p class="text-left mb-0"><span class="fw-5">Teléfono contacto: </span> ' + response.tel + '</p>';
                        if (response.mail !== "") txt += '<p class="text-left mb-0"><span class="fw-5">Mail contacto: </span> ' + response.mail + '</p>';
                        txt += '<div class="text-center"><a href="/modulo-presupuestos/presupuestos/presupuesto?ID_Pres2=' + response.iD_Pres2 + '" class="btn btn-sm btn-primary" target="_blank">Ir al presupuesto</a></div>';
                        
                        txt += '<hr>';
                        var num = 0;
                        if (response.det !== null) num = response.det.length;
                        for (t = 0; t < num; t++) {
                            txt += '<p class="text-left mb-1 fs-0-8">' + response.det[t].datoS1 + ' x ' + response.det[t].datoS2 + ' - ' + response.det[t].datoS3 + ' - ' + response.det[t].datoS4 + '</p>';
                        }

                        document.getElementById("pres_content").innerHTML = txt;

                        hideAll();
                        $("#verpres").show();
                        $("#verpres").addClass("animated fadeIn");

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

$(document.body).on('click', '.cliente-facturacion-guardar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var AxDias = document.getElementById("AxDias").value;
    var Frac = document.getElementById("Frac").value;
    var PostPago = document.getElementById("PostPago").checked;
    var FacDif = document.getElementById("FacDif").checked;
    var FacEMail = document.getElementById("FacEMail").checked;
    var ID_Us_MailEnNom = document.getElementById('vEnviarEn').value;
    var ObsAdm = document.getElementById("ObsAdm").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/facturacion-guardar",
                data: { ID_Cli2: ID_Cli2, AxDias: AxDias, Frac: Frac, PostPago: PostPago, FacDif: FacDif, FacEMail: FacEMail, ID_Us_MailEnNom: ID_Us_MailEnNom, ObsAdm: ObsAdm},
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cliente-formas-guardar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var elems = document.getElementsByName("ID_For");
    var num = 0;
    if (elems !== null) num = elems.length;
    var id_forpred = "0";
    var id_for = "";
    var perm = "";
    var id_cue = "";
    for (t = 0; t < num; t++) {
        var txt = elems[t].id.split("_");
        if (id_for !== "") {
            id_for += "_";
            perm += "_";
            id_cue += "_";
        }
        id_for += txt[1];
        if (elems[t].checked) id_forpred = txt[1];
        var id = "Per_" + txt[1];
        perm += document.getElementById(id).checked;
        id = "ID-Cue-Neg_" + txt[1];
        var elem = document.getElementById(id);
        if (elem === null) {
            id_cue += "0";
        } else {
            id_cue += document.getElementById(id).value;
        }
    }

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/formas-guardar",
                data: { ID_Cli2: ID_Cli2, id_for: id_for, id_forpred: id_forpred, perm: perm, id_cue: id_cue },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cliente-cccli-guardar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar el número de cuenta contable de este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var CCCli = document.getElementById("CCCli").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/cccli-guardar",
                data: { ID_Cli2: ID_Cli2, CCCli: CCCli },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-razon-guardar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea cambiar la razón fiscal de este cliente?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var Emp = document.getElementById("Emp").value;
    var Razon = document.getElementById("Razon").value;
    var NIF = document.getElementById("NIF").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/razon-guardar",
                data: { ID_Cli2: ID_Cli2, Razon: Razon, NIF: NIF, Emp: Emp },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-riesgo-guardar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var RieMax = document.getElementById("RieMax").value;
    var Poliza = document.getElementById('Poliza').value;
    var Asegurado = document.getElementById('Asegurado').checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/riesgo-guardar",
                data: { ID_Cli2: ID_Cli2, RieMax: RieMax, Poliza: Poliza, Asegurado: Asegurado },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-dia-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este dia de pago?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var Dia = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/dia-eliminar",
                data: { ID_Cli2: ID_Cli2, Dia: Dia },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-dia-nuevo', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var Dia = document.getElementById("Dia").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/dia-nuevo",
                data: { ID_Cli2: ID_Cli2, Dia: Dia },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-cuenta-guardar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Cue = document.getElementById("ID_Cue").value;
    var Cue = document.getElementById('Cue').value;
    var ObsBan = document.getElementById('ObsBan').value;
    var DirBan = document.getElementById('DirBan').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/cuenta-guardar",
                data: { ID_Cli2: ID_Cli2, ID_Cue: ID_Cue, Cue: Cue, ObsBan: ObsBan, DirBan: DirBan },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.sel-cue', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cue = strid[1];

    CargarCuenta(ID_Cue);

});

function CargarCuenta(ID_Cue) {

    var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-clientes/cliente/cliente/fiscal/cuenta",
                data: { ID_Cli2: ID_Cli2, ID_Cue: ID_Cue },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {

                        document.getElementById("ID_Cue").value = ID_Cue;
                        document.getElementById("Cue").value = response.datoS1;
                        document.getElementById("Banco").innerHTML = response.datoS2;
                        document.getElementById("DirBan").value = response.datoS3;
                        document.getElementById("ObsBan").value = response.datoS4;

                        var elem = document.getElementById("reccont");
                        var txt = "";
                        if (response.datoI === 0) {
                            txt += '<p class="fw-5 mb-0 text-center">No hay recibos pendientes</p>';
                            txt += "";
                            txt += "";
                        } else {
                            if (response.datoI === 1) {
                                txt += '<p class="fw-5 mb-0 text-center">Hay <span class="text-danger">1</span> recibo pendiente</p>';
                            } else {
                                txt += '<p class="fw-5 mb-0 text-center">Hay <span class="text-danger">' + response.datoI + '</span> recibos pendientes</p>';
                            }
                            txt += '<div class="d-flex justify-content-center mb-2 mt-2"><button type="button" class="btn btn-sm btn-primary">Pasar recibos pendientes por esta cuenta</button></div>';
                        }

                        elem.innerHTML = txt;

                        hideAll();
                        $("#vercue").show();
                        $("#verrec").show();
                        $("#vercue").addClass("animated fadeIn");
                        $("#verrec").addClass("animated fadeIn");

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

$(document.body).on('click', '.cliente-cuenta-pred', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cue = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/cuenta-pred",
                data: { ID_Cli2: ID_Cli2, ID_Cue: ID_Cue },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-cuenta-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta cuenta bancaria?")) return false;

    var ID_Cli2 = document.getElementById('ID_Cli2').value;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Cue = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/cuenta-eli",
                data: { ID_Cli2: ID_Cli2, ID_Cue: ID_Cue },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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

$(document.body).on('click', '.cliente-cuenta-nueva', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var Cue = document.getElementById('CueNew').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/cuenta-nueva",
                data: { ID_Cli2: ID_Cli2, Cue: Cue },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/fiscal?ID_Cli2=" + ID_Cli2, "_self");
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



// Buscador de usuarios

$(function () {

    $("#EnviarEn").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#EnviarEn").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#EnviarEn").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/fiscal/buscarusuario",
                data: { buscar: encodeURI(document.getElementById('EnviarEn').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var result = [];

                        if (response.det !== null) {
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


$(document.body).on('click', '.cliente-comercial-guardar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var Emp = document.getElementById("Emp").value;
    var ID_Us_Agente = document.getElementById("vAgente").value;
    var ID_TipCli2 = document.getElementById("ID_TipCli2").value;
    var Fe_Na = document.getElementById("Fe_Na").value;
    var Mailing = document.getElementById("Mailing").checked;
    var ID_Tari2 = document.getElementById("ID_Tari2").value;
    var Act = document.getElementById("Act").value;
    var Obs = document.getElementById("Obs").value;
    var Dto = document.getElementById("Dto").value;
    var Comi = document.getElementById("ComiPor").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/comercial/guardar-comercial",
                data: { ID_Cli2: ID_Cli2, Emp: Emp, ID_Us_Agente: ID_Us_Agente, ID_TipCli2: ID_TipCli2, Fe_Na: Fe_Na, Mailing: Mailing, ID_Tari2: ID_Tari2, Act: Act, Obs: Obs, Dto: Dto, Comi: Comi },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

$(document.body).on('click', '.cliente-comercial-programa-guardar', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById("ID_Cli2").value;
    var CopiasAlb = document.getElementById("CopiasAlb").value;
    var CopiasFac = document.getElementById("CopiasFac").value;
    var AlbNoVal = document.getElementById("AlbNoVal").checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/comercial/guardar-programa-comercial",
                data: { ID_Cli2: ID_Cli2, CopiasAlb: CopiasAlb, CopiasFac: CopiasFac, AlbNoVal: AlbNoVal },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
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

// Buscador de agentes

$(function () {

    $("#Agente").keydown(function (e) {
        if (e.keyCode === 46) {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
            return false;
        }
    });

    $("#Agente").blur(function () {
        if ($(this).val() === '') {
            $(this).val("");
            document.getElementById('t' + $(this)[0].id).value = "";
            document.getElementById('v' + $(this)[0].id).value = "0";
        }
        else { $(this).val(document.getElementById('t' + $(this)[0].id).value); }
        return false;
    });
    $("#Agente").autocomplete({
        delay: 0,
        position: { collision: "flipfit" },
        source: function (request, responsefunc) {
            $.ajax({
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/comercial/buscaragente",
                data: { buscar: encodeURI(document.getElementById('Agente').value).replace('&', '%26') },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        // validado

                        var result = [];

                        if (response.det !== null) {
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

// Módulo Cliente > Cliente > Acceso Web > Nuevo Usuario

$(document.body).on('click', '.ver-nuevo-usuario', function () {

    event.preventDefault(event);

    hideAll();
    $("#nuevoUsuario").show();
    $("#nuevoUsuario").addClass("animated fadeIn");

});

// Generar usuario
$(document.body).on('click', '.generar-usuario', function () {

    event.preventDefault(event);

    var ID_Cli2 = document.getElementById('ID_Cli2').value;
    var ID_Mail = document.getElementById('ID_Mail').value;
    var ID_Tv = document.getElementById('ID_Tv').value;
    var Nom = document.getElementById('Nom').value;
    var NIC = document.getElementById('NIC').value;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-clientes/clientes/cliente/acceso-web/nuevo-usuario",
                data: { ID_Cli2: ID_Cli2, ID_Mail: ID_Mail, Nom: Nom, NIC: NIC, ID_Tv: ID_Tv },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-clientes/clientes/cliente/acceso-web?ID_Cli2=" + ID_Cli2, "_self");
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
