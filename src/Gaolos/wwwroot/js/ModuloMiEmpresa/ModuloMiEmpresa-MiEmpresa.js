
function hideAll() {
    $("#sindatos").hide();
    $("#verdir").hide();
    $("#vermail").hide();
    $("#verweb").hide();
    $("#vercentro").hide();
    $("#vertel").hide();
}

$(document.body).on('click', '.cliente-centro-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este centro de coste?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Centro2 = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/eliminar-centro",
                data: { ID_Centro2: ID_Centro2 },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

$(document.body).on('click', '.mi-empresa-centro-guardar', function () {

    event.preventDefault(event);

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
                    url: "/modulo-mi-empresa/mi-empresa/nuevo-centro",
                    data: { Centro: Centro, ObsCentro: ObsCentro, Referencia: Referencia },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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
                    url: "/modulo-mi-empresa/mi-empresa/guardar-centro",
                    data: { ID_Centro2: ID_Centro2, Centro: Centro, ObsCentro: ObsCentro, Referencia: Referencia },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

function CargarCentro(ID_Centro2) {

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-mi-empresa/mi-empresa/centro",
                data: { ID_Centro2: ID_Centro2 },
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
                        document.getElementById("AltaCentro").innerText = response.datoS3 + " " + response.datoS4;
                        document.getElementById("Referencia").value = response.datoS5;
                        document.getElementById("ID_Centro2").innerText = response.datoI;
                        document.getElementById("altaFieldCentro").style.display = 'flex';

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

$(document.body).on('click', '.nuevo-centro', function () {

    event.preventDefault(event);

    document.getElementById("centro-titulo").innerHTML = "Nuevo centro de coste";
    document.getElementById("ID_CentroMod").value = "0";
    document.getElementById("Centro").value = "";
    document.getElementById("ObsCentro").value = "";
    document.getElementById("altaFieldCentro").style.display = 'none';

    hideAll();
    $("#vercentro").show();
    $("#vercentro").addClass("animated fadeIn");

});

$(document.body).on('click', '.mi-empresa-web-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta web?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Web = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/eliminar-web",
                data: { ID_Web: ID_Web },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

$(document.body).on('click', '.mi-empresa-web-guardar', function () {

    event.preventDefault(event);

    var ID_Web = document.getElementById("ID_Web").value;
    var Web = document.getElementById('Web').value;
    var ObsWeb = document.getElementById('ObsWeb').value;

    Pace.restart();

    if (ID_Web === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-mi-empresa/mi-empresa/nueva-web",
                    data: { Web: Web, ObsWeb: ObsWeb },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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
                    url: "/modulo-mi-empresa/mi-empresa/guardar-web",
                    data: { ID_Web: ID_Web, Web: Web, ObsWeb: ObsWeb },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

$(document.body).on('click', '.sel-web-pred', function (event) {

    event.preventDefault(event);


    var idd = this.id;
    var strid = idd.split('_');

    var ID_Web = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/sel-web-pred",
                data: { ID_Web: ID_Web },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        event.target.checked = true;
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


$(document.body).on('click', '.sel-web', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Web = strid[1];

    CargarWeb(ID_Web);

});

function CargarWeb(ID_Web) {

    
    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-mi-empresa/mi-empresa/web",
                data: { ID_Web: ID_Web },
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
                        document.getElementById("AltaWeb").innerText = `${response.datoS3} - ${response.datoS4}`;
                        document.getElementById("altaFieldWeb").style.display = 'flex';
                        
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

$(document.body).on('click', '.nueva-web', function () {

    event.preventDefault(event);

    document.getElementById("web-titulo").innerHTML = "Nueva web";
    document.getElementById("ID_Web").value = "0";
    document.getElementById("Web").value = "";
    document.getElementById("ObsWeb").value = "";
    document.getElementById("altaFieldWeb").style.display = 'none';

    hideAll();
    $("#verweb").show();
    $("#verweb").addClass("animated fadeIn");

});

$(document.body).on('click', '.sel-mail-fac', function (event) {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];
    var Sel = this.checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/sel-mail-facturas",
                data: { ID_Mail: ID_Mail, Sel: Sel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        event.target.checked = true;
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

$(document.body).on('click', '.sel-mail-com', function (event) {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];
    var Sel = this.checked;

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/sel-mail-pred",
                data: { ID_Mail: ID_Mail, Sel: Sel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        event.target.checked = true;
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

$(document.body).on('click', '.cliente-mail-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este mail?")) return false;

    
    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/eliminar-mail",
                data: { ID_Mail: ID_Mail },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

$(document.body).on('click', '.mi-empresa-mail-guardar', function () {

    event.preventDefault(event);

    var ID_Mail = document.getElementById("ID_Mail").value;
    var Mail = document.getElementById('Mail').value;
    var ObsMail = document.getElementById('ObsMail').value;
    var Smtp = document.getElementById('Smtp').value;
    var PuertoSmtp = document.getElementById('PuertoSmtp').value;
    var Us = document.getElementById('Us').value;
    var Pass = document.getElementById('Pass').value;
    var SSL = document.getElementById('SSL').checked;

    Pace.restart();

    if (ID_Mail === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-mi-empresa/mi-empresa/nuevo-mail",
                    data: { Mail: Mail, ObsMail: ObsMail, Smtp: Smtp, PuertoSmtp: PuertoSmtp, SSL: SSL, Us: Us, Pass: Pass },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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
                    url: "/modulo-mi-empresa/mi-empresa/guardar-mail",
                    data: { ID_Mail: ID_Mail, Mail: Mail, ObsMail: ObsMail, Smtp: Smtp, PuertoSmtp: PuertoSmtp, SSL: SSL,  Us: Us, Pass: Pass },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

$(document.body).on('click', '.sel-mail', function () {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Mail = strid[1];

    CargarMail(ID_Mail);

});

function CargarMail(ID_Mail) {

    
    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-mi-empresa/mi-empresa/mail",
                data: { ID_Mail: ID_Mail },
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
                        document.getElementById("Mail").value = response.mail;
                        document.getElementById("ObsMail").value = response.obsMail;
                        document.getElementById("AltaMail").innerText = response.fe_Al + " - " + response.usu;
                        document.getElementById("Smtp").value = response.smtp;
                        document.getElementById("PuertoSmtp").value = response.puertoSmtp;
                        document.getElementById("SSL").checked = response.ssl;
                        document.getElementById("Us").value = response.us;
                        document.getElementById("Pass").value = response.pass;
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

$(document.body).on('click', '.nuevo-mail', function () {

    event.preventDefault(event);

    document.getElementById("mail-titulo").innerHTML = "Nuevo mail";
    document.getElementById("ID_Mail").value = "0";
    document.getElementById("Mail").value = "";
    document.getElementById("ObsMail").value = "";
    document.getElementById("Smtp").value = "";
    document.getElementById("PuertoSmtp").value = "";
    document.getElementById("SSL").value = false;
    document.getElementById("Us").value = "";
    document.getElementById("Pass").value = "";
    document.getElementById("altaFieldMail").style.display = 'none';

    hideAll();
    $("#vermail").show();
    $("#vermail").addClass("animated fadeIn");
});


$(document.body).on('click', '.cliente-tel-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar este teléfono?")) return false;

    
    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/eliminar-tel",
                data: {  ID_Tel: ID_Tel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

$(document.body).on('click', '.mi-empresa-tel-pred', function (element) {

    event.preventDefault(event);


    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/pred-tel",
                data: { ID_Tel: ID_Tel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        element.target.checked=true;
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

$(document.body).on('click', '.mi-empresa-fax-pred', function (event) {

    event.preventDefault(event);


    var idd = this.id;
    var strid = idd.split('_');

    var ID_Tel = strid[1];

    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/pred-fax",
                data: { ID_Tel: ID_Tel },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        event.target.checked = true;
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

$(document.body).on('click', '.mi-empresa-tel-guardar', function () {

    event.preventDefault(event);

    var ID_Tel = document.getElementById("ID_Tel").value;
    var Tel = document.getElementById('Tel').value;
    var ObsTel = document.getElementById('ObsTel').value;

    Pace.restart();

    if (ID_Tel === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-mi-empresa/mi-empresa/nuevo-tel",
                    data: { Tel: Tel, ObsTel: ObsTel },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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
                    url: "/modulo-mi-empresa/mi-empresa/guardar-tel",
                    data: { ID_Tel: ID_Tel, Tel: Tel, ObsTel: ObsTel },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return false;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

function CargarTelefono(ID_Tel) {

    //var ID_Cli2 = document.getElementById("ID_Cli2").value;

    Pace.track(function () {
        $.ajax(
            {
                type: "GET",
                url: "/modulo-mi-empresa/mi-empresa/telefono",
                data: { ID_Tel: ID_Tel },
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
                        //document.getElementById("Alta").innerText = response.datoS3 + " - " + response.datoS4;
                        document.getElementById("altaFieldTelefono").style.display = 'flex';

                        
                        hideAll();
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

$(document.body).on('click', '.mi-empresa-dir-eliminar', function () {

    event.preventDefault(event);

    if (!confirm("¿Desea eliminar esta dirección?")) return false;

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];
    
    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/direccion-eliminar",
                data: { ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        window.open("/modulo-mi-empresa/mi-empresa", "_self");
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

$(document.body).on('click', '.sel-dir-com', function (event) {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];
    
    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/cambiar-direccion-comercial",
                data: { ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        event.target.checked = true;
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

$(document.body).on('click', '.sel-dir-fis', function (event) {

    event.preventDefault(event);

    var idd = this.id;
    var strid = idd.split('_');

    var ID_Dir = strid[1];
    
    Pace.track(function () {
        $.ajax(
            {
                type: "POST",
                url: "/modulo-mi-empresa/mi-empresa/cambiar-direccion-fiscal",
                data: { ID_Dir: ID_Dir },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return;
                    } else {
                        $.jGrowl(response.err.mensaje, $optionsMessageOK);
                        event.target.checked =true;
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


$(document.body).on('click', '.mi-empresa-direccion-guardar', function () {

    event.preventDefault(event);

    var ID_Dir = document.getElementById('ID_Dir').value;
    var Dir = document.getElementById('Dir').value;
    var Pai = document.getElementById('UbiPai_1').value;
    var Pro = document.getElementById('UbiPro_1').value;
    var CP = document.getElementById('UbiCP_1').value;
    var Pob = document.getElementById('UbiPob_1').value;
    var Obs = document.getElementById('Obs').value;

    if (ID_Dir === "0") {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-mi-empresa/mi-empresa/nueva-direccion",
                    data: {  Dir: Dir, Pai: Pai, Pro: Pro, CP: CP, Pob: Pob, Obs: Obs },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
                            return;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return;
                    }
                });
        });
    } else {
        Pace.track(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "/modulo-mi-empresa/mi-empresa/guardar-direccion",
                    data: { ID_Dir: ID_Dir, Dir: Dir, Pai: Pai, Pro: Pro, CP: CP, Pob: Pob, Obs: Obs },
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    cache: false,
                    datatype: "json",
                    success: function (response, textStatus, jqXHR) {
                        if (response.err.eserror) {
                            $.jGrowl(response.err.mensaje, $optionsMessageKO);
                            return;
                        } else {
                            $.jGrowl(response.err.mensaje, $optionsMessageOK);
                            window.open("/modulo-mi-empresa/mi-empresa", "_self");
                            return;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $.jGrowl(errorThrown, $optionsMessageKO);
                        return;
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

$(document.body).on('click', '.nueva-dir', function () {

    event.preventDefault(event);

    CargarDireccion(0);

});

function CargarDireccion(ID_Dir) {

    
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
                url: "/modulo-mi-empresa/mi-empresa/direccion", 
                data: { ID_Dir: ID_Dir },
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
                            document.getElementById("altaFieldDir").style.display='none';
                        } else {
                            document.getElementById("dir-titulo").innerHTML = "Modificar dirección";
                            document.getElementById("altaFieldDir").style.display = 'flex';
                        }

                        document.getElementById("ID_Dir").value = response.iD_Dir;
                        document.getElementById("Dir").value = response.dir;
                        document.getElementById("Obs").value = response.obs;
                        document.getElementById("AltaDir").innerText = response.fe_Al + " " + response.usu;

                        if (response.pais === null) {
                            txt = '<p>No hay paises definidos</p>';
                        } else {
                            txt = '<select class="custom-select cambio-pais-mi-empresa-dir" id="UbiPai_' + numDir + '">';
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
                            txt = '<select class="custom-select cambio-provincia-mi-empresa-dir" id="UbiPro_' + numDir + '">';
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
                            txt = '<select class="custom-select cambio-cp-mi-empresa-dir" id="UbiCP_' + numDir + '">';
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


                        hideAll();
                        $("#verdir").show();
                        $("#verdir").addClass("animated fadeIn");

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

$(document.body).on('change', '.cambio-pais-mi-empresa-dir', function () {

    var id_Pai = this.id;
    var strid = id_Pai.split('_');
    var numDir = strid[1];

    var Pai = document.getElementById("UbiPai_" + numDir).value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-mi-empresa/mi-empresa/direccioncambiopais",
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
                            txt = '<select class="custom-select cambio-provincia-mi-empresa-dir" id="UbiPro_' + numDir + '">';
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

$(document.body).on('change', '.cambio-provincia-mi-empresa-dir', function () {

    var id_Pro = this.id;
    var strid = id_Pro.split('_');
    var numDir = strid[1];

    var Pai = document.getElementById("UbiPai_" + numDir).value;
    var Pro = document.getElementById("UbiPro_" + numDir).value;

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/modulo-mi-empresa/mi-empresa/direccioncambioprovincia",
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
                            txt = '<select class="custom-select cambio-cp-mi-empresa-dir" id="UbiCP_' + numDir + '">';
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

$(document.body).on('change', '.cambio-cp-mi-empresa-dir', function () {

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
                url: "/modulo-mi-empresa/mi-empresa/direccioncambiocp",
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

