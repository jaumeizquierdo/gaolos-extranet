function CalendarioMensual(mes,año) {

    Pace.track(function () {

        $.ajax(
            {
                type: "POST",
                url: "/calendario/cargar",
                data: { mes: mes, año: año },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        //$.jGrowl(response.err.mensaje, $optionsMessageOK);

                        //$("#sininfo").hide();
                        //$("#semana").show();

                        var txt = '';

                        if (response.dia !== null) {

                            var dias = response.diaSemana.length;
                            for (d = 0; d < dias; d++) {
                                var bloquedia = '';
                                bloquedia += '<div class="card mb-3 mr-2 w-20">';
                                bloquedia += '<div class="card-header card-header-options">';
                                bloquedia += '<span><small class="fw-5">' + response.diaSemana[d] + '</small></span>';
                                bloquedia += '</div>';
                                bloquedia += '<div class="card-block card-table p-0">';
                                bloquedia += '<div class="d-flex">';
                                bloquedia += '';
                                bloquedia += '<table class="table table-striped p-0 mb-0">';
                                bloquedia += '<tbody>';

                                var semanas = response.dia.length;

                                for (b = 0; b < semanas; b++) {

                                    var bloqueasis = '';
                                    bloqueasis += '<tr>';
                                    if (response.dia[b][d].esOtroMes) {
                                        bloqueasis += '<td class="p-0" style="height:100px;vertical-align:top;" ><div class="carga carga-en-curso m-1 p-1">';
                                    } else {
                                        bloqueasis += '<td class="p-0" style="height:100px;vertical-align:top;" ><div class="carga carga-concluida m-1 p-1">';
                                    }

                                    bloqueasis += '<div class="carga-header"><p class="mb-0"><strong>' + response.dia[b][d].nDia + '</strong> <span class=""><a href="#" id="' + response.dia[b][d].nDia + '" class="fw-5" >+</a></span></p></div>';

                                    // Contenido
                                    var nc = 0;
                                    if (response.dia[b][d].objDiaCal !== null) nc = response.dia[b][d].objDiaCal.length;
                                    if (nc > 0) {
                                        bloqueasis += '<div class="carga-info mt-1 mb-1 d-flex justify-content-between align-items-center">';
                                        for (t = 0; t < nc; t++) {
                                            //bloqueasis += '<div class="round-item mdc-bg-red-A400">A</div>';
                                            bloqueasis += '<p>' + response.dia[b][d].objDiaCal[t].ho_In + '</p>';
                                            bloqueasis += '<p>' + response.dia[b][d].objDiaCal[t].asunto + '</p>';
                                        }
                                        bloqueasis += '</div>';
                                    }


                                    //bloqueasis += '<p class="mb-0">' + 'emp' + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha del cliente">(' + ')</a></p>';
                                    //bloqueasis += '<p class="mb-0">' + 'pob' + '</p>';

                                    //bloqueasis += '<p class="mb-0">' + 'breve' + '</p>';
                                    //bloqueasis += '<p class="mb-0"><strong>Partes:</strong> ' + '1' + '</p>';

                                    //var num3 = 0;
                                    //if (response.det[t].asis[b].elems != null) num3 = response.det[t].asis[b].elems.length;
                                    //for (x = 0; x < num3; x++) {
                                    //    bloqueasis += '<p class="mb-0">' + '<i class="fa fa-check"></i> ' + response.det[t].asis[b].elems[x].datoI + ' x ' + response.det[t].asis[b].elems[x].datoS + '</p>';
                                    //}

                                    bloqueasis += '</div></td>';
                                    bloqueasis += '</tr>';


                                    bloquedia += bloqueasis;
                                }



                                bloquedia += '</tbody>';
                                bloquedia += '</table>';
                                bloquedia += '</div>';
                                bloquedia += '</div>';
                                bloquedia += '</div>';

                                txt += bloquedia;

                            }

                        }

                        document.getElementById("content").innerHTML = txt;

                        document.getElementById("nombre_mes").innerHTML = response.mes + " - " + response.año;

                        var elem = document.getElementsByName("prev");
                        if (elem !== null) elem[0].id = "f_" + response.antMes + "_" + response.antAño;

                        elem = document.getElementsByName("sig");
                        if (elem !== null) elem[0].id = "f_" + response.sigMes + "_" + response.sigAño;

                        $("#semana").show();

                        return;





                        //var num = 0;

                        //if (response.det !== null) num = response.det.length;
                        //if (num > 5) num = 5;
                        //for (t = 0; t < num; t++) {

                        //    var bloquedia = '';
                        //    bloquedia += '<div class="card mb-3 mr-2 w-20">';
                        //    bloquedia += '<div class="card-header card-header-options">';
                        //    bloquedia += '<span><small class="fw-5">' + response.det[t].diaTxt + '</small></span><span><small class="fw-5"><a href="#" class="diasemsel_' + response.det[t].diaSem + '">' + response.det[t].dia + '</a></small></span>';
                        //    bloquedia += '</div>';
                        //    bloquedia += '<div class="card-block card-table p-0">';
                        //    bloquedia += '<div class="d-flex">';
                        //    bloquedia += '';
                        //    bloquedia += '<table class="table table-striped p-0 mb-0">';
                        //    bloquedia += '<tbody>';

                        //    var num2 = 0;
                        //    if (response.det[t].asis != null) num2 = response.det[t].asis.length;
                        //    for (b = 0; b < num2; b++) {

                        //        var bloqueasis = '';
                        //        bloqueasis += '<tr>';
                        //        if (response.det[t].asis[b].atraso) {
                        //            bloqueasis += '<td class="p-0"><div class="carga carga-urgente m-1 p-1">';
                        //        } else {
                        //            if (response.det[t].asis[b].fe_Fin == "") {
                        //                bloqueasis += '<td class="p-0"><div class="carga carga-en-curso m-1 p-1">';
                        //            } else {
                        //                bloqueasis += '<td class="p-0"><div class="carga carga-concluida m-1 p-1">';
                        //            }
                        //        }
                        //        bloqueasis += '<div class="carga-header"><p class="mb-0"><strong>' + response.det[t].asis[b].emp + '</strong> <a href="/modulo-asistencias/historial/asistencia?ID_Asis2=' + response.det[t].asis[b].iD_Asis2 + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver detalles asistencia">(' + response.det[t].asis[b].iD_Asis2 + ')</a></p></div>';
                        //        bloqueasis += '<div class="carga-info mt-1 mb-1 d-flex justify-content-between align-items-center">';
                        //        if (response.det[t].asis[b].esMan) {
                        //            if (response.det[t].asis[b].anual) {
                        //                bloqueasis += '<div class="round-item mdc-bg-teal-A700"><a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].asis[b].iD_Man2 + '" target="_blank" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver detalles del mantenimiento">M*</a></div>';
                        //            } else {
                        //                bloqueasis += '<div class="round-item mdc-bg-teal-A700"><a href="/modulo-mantenimientos/mantenimientos/mantenimiento?ID_Man2=' + response.det[t].asis[b].iD_Man2 + '" target="_blank" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ver detalles del mantenimiento">M</a></div>';
                        //            }
                        //        } else {
                        //            bloqueasis += '<div class="round-item mdc-bg-red-A400">A</div>';
                        //        }
                        //        if (response.det[t].asis[b].fe_Fin == "") {
                        //            bloqueasis += '<input type="checkbox" class="diasem_' + response.det[t].diaSem + ' sel-check" id="sel-rep_' + response.det[t].asis[b].iD_AsisPlan + '">';
                        //        }
                        //        bloqueasis += '</div>';
                        //        bloqueasis += '<p class="mb-0">' + response.det[t].asis[b].emp + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].asis[b].iD_Cli2 + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha del cliente">(' + response.det[t].asis[b].iD_Cli2 + ')</a></p>';
                        //        bloqueasis += '<p class="mb-0">' + response.det[t].asis[b].pob + '</p>';
                        //        if (response.det[t].asis[b].iD_Cli2Rel != "") {
                        //            if (response.det[t].asis[b].empRelTipo == "") {
                        //                bloqueasis += '<p class="mb-0"><strong>' + response.det[t].asis[b].empRelTipo + ':</strong> ' + response.det[t].asis[b].empRel + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].asis[b].iD_Cli2Rel + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha del cliente relacionado">(' + response.det[t].asis[b].iD_Cli2Rel + ')</a></p>';
                        //            } else {
                        //                bloqueasis += '<p class="mb-0"><strong>' + response.det[t].asis[b].empRelTipo + ':</strong> ' + response.det[t].asis[b].empRel + ' <a href="/modulo-clientes/clientes/cliente?ID_Cli2=' + response.det[t].asis[b].iD_Cli2Rel + '" target="_blank" class="fw-5" data-toggle="tooltip" data-placement="top" title="" data-original-title="Ir a la ficha de la/el ' + response.det[t].asis[b].empRelTipo + '">(' + response.det[t].asis[b].iD_Cli2Rel + ')</a></p>';
                        //            }
                        //        }

                        //        bloqueasis += '<p class="mb-0">' + response.det[t].asis[b].breve + '</p>';
                        //        if (response.det[t].asis[b].comen != "") bloqueasis += '<p class="mb-0"><a href="#" class="asistencia-sel fw-5">' + response.det[t].asis[b].comen + '</a></p>';
                        //        if (response.det[t].asis[b].numPartes == 0) {
                        //            bloqueasis += '<p class="mb-0 text-danger fw-5">Sin partes</p>';

                        //        } else {
                        //            bloqueasis += '<p class="mb-0"><strong>Partes:</strong> ' + response.det[t].asis[b].numPartes + '</p>';
                        //        }
                        //        var num3 = 0;
                        //        if (response.det[t].asis[b].elems != null) num3 = response.det[t].asis[b].elems.length;
                        //        for (x = 0; x < num3; x++) {
                        //            bloqueasis += '<p class="mb-0">' + '<i class="fa fa-check"></i> ' + response.det[t].asis[b].elems[x].datoI + ' x ' + response.det[t].asis[b].elems[x].datoS + '</p>';
                        //        }

                        //        bloqueasis += '</div></td>';
                        //        bloqueasis += '</tr>';


                        //        bloquedia += bloqueasis;
                        //    }


                        //    bloquedia += '</tbody>';
                        //    bloquedia += '</table>';
                        //    bloquedia += '</div>';
                        //    bloquedia += '</div>';
                        //    bloquedia += '</div>';

                        //    txt += bloquedia;

                        //}

                        //document.getElementById("content").innerHTML = txt;

                        //return;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $.jGrowl(errorThrown, $optionsMessageKO);
                    return false;
                }
            });

    });

}


function Calendario(mes,año,id) {

    Pace.track(function () {

        $.ajax(
            {
                type: "GET",
                url: "/tareas-pendientes/calendario",
                data: { mes: mes, año: año },
                contentType: "application/json;charset=utf-8",
                cache: false,
                datatype: "json",
                success: function (response, textStatus, jqXHR) {
                    if (response.err.eserror) {
                        $.jGrowl(response.err.mensaje, $optionsMessageKO);
                        return false;
                    } else {
                        mes = response.numMes;
                        año = response.año;
                        var mesp = mes + 1;
                        var añop = año;
                        if (mesp > 12) {
                            mesp = 1;
                            añop += 1;
                        }
                        var mesa = mes - 1;
                        var añoa = año;
                        if (mesa < 1) {
                            mesa = 12;
                            añoa -= 1;
                        }

                        var txt = '';
                        txt += '<thead><tr><th colspan="7" class="p-0"><div class="d-flex justify-content-between align-items-center px-3 py-2">';
                        txt += '<button type="button" id="' + mesa + '_' + añoa + '_' + id + '" class="btn btn-sm btn-primary btn-calendario" value=""><i class="fa fa-chevron-left"></i></button><span class="fw-5">' + response.mes + ' ' + response.año + '</span><button type="button" class="btn btn-sm btn-primary btn-calendario" id="' + mesp + '_' + añop + '_' + id + '" value=""><i class="fa fa-chevron-right"></i></button></div></th></tr><tr>';
                        txt += '<th class="text-center fw-5">' + response.diasSemama[0] + '</th>';
                        txt += '<th class="text-center fw-5">' + response.diasSemama[1] + '</th>';
                        txt += '<th class="text-center fw-5">' + response.diasSemama[2] + '</th>';
                        txt += '<th class="text-center fw-5">' + response.diasSemama[3] + '</th>';
                        txt += '<th class="text-center fw-5">' + response.diasSemama[4] + '</th>';
                        txt += '<th class="text-center fw-5">' + response.diasSemama[5] + '</th>';
                        txt += '<th class="text-center fw-5">' + response.diasSemama[6] + '</th>';
                        txt += '</tr></thead>';
                        txt += '<tbody>';
                        var col = 0;
                        for (t = 0; t < response.dia.length; t++) {
                            if ((t % 7) === 0) { txt += '<tr>'; }
                            var clss = '';
                            var clsstd = '';
                            if (response.dia[t].esOtroMes) { clss = ' text-muted '; }
                            if (response.dia[t].hoy) { clsstd = ' tarea-dia-en-curso'; }
                            if (response.dia[t].vacaciones) { clsstd = ' tarea-vacaciones'; }
                            else if (response.dia[t].festivo) { clsstd = ' tarea-festivo'; }
                            else if (response.dia[t].descanso) { clsstd = ' tarea-no-laborable'; }
                            //else { }

                            var clssnum = 'tarea-empty';
                            switch (response.dia[t].nivel) {
                                case 1:
                                    clssnum = 'tarea-baja';
                                    break;
                                case 2:
                                    clssnum = 'tarea-media';
                                    break;
                                case 3:
                                    clssnum = 'tarea-alta';
                                    break;
                            }
                            txt += '<td class="text-center fw-5' + clsstd + '">';
                            txt += '<div>';
                            txt += '<p class="mb-0 text-left"><small class="fw-5' + clss + '">' + response.dia[t].dia + '</small></p>';
                            txt += '<span class="' + clssnum + '">';
                            if (response.dia[t].num > 0) { txt += response.dia[t].num; }
                            txt += '</span>';
                            txt += '</div>';
                            txt += '</td>';
                            if (((t + 1) % 7) === 0) { txt += '</tr>'; }
                        }
                        txt += '</tbody>';

                        document.getElementById(id).innerHTML = txt;

                        bta = mesa + '_' + añoa + '_' + id;
                        document.getElementById(bta).addEventListener('click', function () {
                            Calendario(mesa, añoa, id);
                        });
                        btp = mesp + '_' + añop + '_' + id;
                        document.getElementById(btp).addEventListener('click', function () {
                            Calendario(mesp, añop, id);
                        });

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
