using Microsoft.Net.Http.Headers;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using ModuloAsistenciasLibrary;
using System.Web;
using DashBoardLibrary;

namespace Gaolos.Controllers
{
    public class ModuloAsistenciasController : Controller
    {

        #region "Modulo Asistencias - DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias")]
        public async Task<IActionResult> AsistenciasDashboard(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDashBoard lis = new strDashBoard();

            try
            {
                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasdashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasdashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("AsistenciasDashboard", viewModel);
        }

        #endregion

        #region "Modulo Asistencias - Por asignar"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-asignar")]
        public async Task<IActionResult> AsistenciasPorAsignar(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasPorAsignarListado lis = new strAsistenciasAsistenciasPorAsignarListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 Anual = 0;
                try { Anual = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Anual")); } catch { }

                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Rel")); } catch { }
                string txtUrg = "";
                bool Urg = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "on") { txtUrg = "SI"; Urg = true; }
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "on") Man = true;

                string QS = "";
                string QN = "";

                if (Transporte.parameters.ParamsIsPost)
                {
                    if (Transporte.parameters.ParamsKeys != null)
                    {
                        for (Int32 t = 0; t < Transporte.parameters.ParamsKeys.Length; t++)
                        {
                            string[] part = Transporte.parameters.ParamsKeys[t].Split("_");
                            if (part.Length == 2)
                            {
                                switch (part[0])
                                {
                                    case "qs":
                                        if (QS != "") QS += "_";
                                        QS += part[1];
                                        break;
                                    case "qn":
                                        if (QN != "") QN += "_";
                                        QN += part[1];
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    QS = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QS");
                    QN = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QN");
                }



                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_ZonaSub"] = ID_ZonaSub;
                ViewData["ID_Tipo2"] = ID_Tipo2;
                ViewData["ID_Cli2Rel"] = ID_Cli2Rel;
                ViewData["Urg"] = Urg;
                ViewData["Man"] = Man;
                ViewData["QS"] = QS;
                ViewData["QN"] = QN;
                ViewData["Anual"] = Anual;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                strDatoS filtro = new strDatoS();
                filtro.datoI = ID_ZonaSub;
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                filtro.datoS1 = txtUrg;
                filtro.datoS2 = QS;
                filtro.datoS3 = QN;
                filtro.datoS4 = ID_Cli2Rel.ToString();
                filtro.datoS5 = Anual.ToString();
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporasignarlistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasPorAsignarListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporasignarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasPorAsignarListadoViewModel(lis);

            return View("AsistenciasPorAsignar", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-asignar/asignar")]
        public async Task<transportout> AsistenciasAsistenciasPorAsignarAsignar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Rel")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                Int32 Anual = 0;
                try { Anual = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Anual")); } catch { }
                string txtUrg = "";
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "true") { txtUrg = "SI"; }
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "true") Man = true;
                string ids = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ids");

                string QS = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QS");
                string QN = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QN");

                Int32 Num = 0;
                try { Num = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Num")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.id = ID_Us_Asi;
                bus.id2 = Num;
                strDatoS filtro = new strDatoS();
                filtro.datoI = ID_ZonaSub;
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                filtro.datoS1 = txtUrg;
                filtro.datoS2 = QS;
                filtro.datoS3 = QN;
                filtro.datoS4 = ids;

                strDatoS filtro2 = new strDatoS();
                filtro2.datoI = ID_Cli2Rel;
                filtro2.datoD = Convert.ToDecimal(Anual);

                //filtro.datoS5 = ID_Cli2Rel.ToString();
                filtro.datoS5 = JsonConvert.SerializeObject(filtro2);

                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporasignarasignar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporasignarasignar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-asignar/buscar-tecnico")]
        public async Task<strListaErr> AsistenciasPorAsignarBuscarTecnico(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporasignarbuscartecnico", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporasignarbuscartecnico-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-asignar/imprimir")]
        public async Task<IActionResult> AsistenciasAsistenciasPorAsignarImprimir(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Asis2;
                Transporte.obj = bus;

                //ModelsAndroid.strandroid_asistencia_nuevo_parte parte = new ModelsAndroid.strandroid_asistencia_nuevo_parte();
                //parte.id_us = 15775;
                //parte.token = "NOSE";
                //parte.offline = null;
                //parte.haypresup = false;
                //parte.obspresup = "";
                //parte.nofin = false;
                //parte.obsnofin = "";

                //parte.reso = "Reso";
                //parte.cont = "cont";
                //parte.tel = "tel";
                //parte.mail = "";

                //Transporte.obj = parte;

                //var client = new HttpClient();
                //client.BaseAddress = new Uri("http://localhost:60618");
                //var response = await client.PostAsJsonAsync("logincontroller/androidasistenciasasistenciapartenuevo", Transporte);
                //dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);

                //string a = "";

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporasignarimprimir", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
                }
                else
                {
                    strDatoS err500 = new strDatoS
                    {
                        datoS1 = dat.err.titulo,
                        datoS2 = dat.err.mensaje
                    };
                    Transporte.obj = err500;

                    dat = await Infrastructure.ReturnResults.GetResponseAsync("error/extraneterror500", Transporte, HttpContext);

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporasignarimprimir-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            //return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
            return File(lis.Fichero, mediaType.ToString());

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Asistencias - Rechazadas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-rechazadas")]
        public async Task<IActionResult> AsistenciasRechazadas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasPorAsignarListado lis = new strAsistenciasAsistenciasPorAsignarListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                string txtUrg = "";
                bool Urg = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "on") { txtUrg = "SI"; Urg = true; }
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "on") Man = true;

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_ZonaSub"] = ID_ZonaSub;
                ViewData["ID_Tipo2"] = ID_Tipo2;
                ViewData["Urg"] = Urg;
                ViewData["Man"] = Man;
                ViewData["QS"] = "";
                ViewData["QN"] = "";
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                strDatoS filtro = new strDatoS();
                filtro.datoI = ID_ZonaSub;
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                filtro.datoS1 = txtUrg;
                filtro.datoS2 = "";
                filtro.datoS3 = "";
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasrechazadaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasPorAsignarListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasrechazadaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasPorAsignarListadoViewModel(lis);

            return View("AsistenciasRechazadas", viewModel);
        }


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-rechazadas/reasignar")]
        public async Task<transportout> AsistenciasAsistenciasRechazadasReAsignar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string ids = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ids");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoS = ids;
                bus.datoI = ID_Us_Asi; ;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasrechazadasreasignar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasrechazadasreasignar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion


        #region "Modulo Asistencias - Nuevas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/nueva-asistencia")]
        public async Task<IActionResult> AsistenciasNuevaAsistencia(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasNuevaAsistencia lis = new strAsistenciasAsistenciasNuevaAsistencia();

            try
            {

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasnuevaasistencia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasNuevaAsistencia>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasnuevaasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasNuevaAsistenciaViewModel(lis);

            return View("AsistenciasAsistenciaNueva", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/nueva-asistencia/imprimir")]
        public async Task<IActionResult> AsistenciasAsistenciasNuevaAsistenciaImprimir(string paramsin) //FileResult
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Asis2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasnuevaasistenciaimprimir", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
                }
                else
                {
                    strDatoS err500 = new strDatoS
                    {
                        datoS1 = dat.err.titulo,
                        datoS2 = dat.err.mensaje
                    };
                    Transporte.obj = err500;

                    dat = await Infrastructure.ReturnResults.GetResponseAsync("error/extraneterror500", Transporte, HttpContext);

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasnuevaasistenciaimprimir-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
        }


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/nueva-asistencia/nueva")]
        public async Task<transportout> AsistenciasAsistenciasNuevaAsistenciaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string SoliPor = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "SoliPor");
                string TelCli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "TelCli");
                string MailCli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "MailCli");
                string RefCli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefCli");
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                bool Urg = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "true") { Urg = true; }
                bool CliParado = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CliParado");
                if (txt == "true") CliParado = true;
                Int32 ID_Centro = 0;
                try { ID_Centro = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro")); } catch { }
                string Breve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Breve");
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                string EmpAux = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                string UbiCont = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "UbiCont");
                string UbiTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "UbiTel");
                string Horario = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Horario");
                bool GuardarHor = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "GuardarHor");
                if (txt == "true") GuardarHor = true;
                string UbiObs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "UbiObs");

                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }

                string Dia = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia");
                string Hora = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Hora");
                string Comen = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Comen");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strAsistenciasAsistenciaNuevaGuardar bus = new strAsistenciasAsistenciaNuevaGuardar();
                bus.ID_Cli2 = ID_Cli2;
                bus.SolicitadoPor = SoliPor;
                bus.TelCli = TelCli;
                bus.MailCli = MailCli;
                bus.RefCli = RefCli;
                bus.ID_Tipo2 = ID_Tipo2;
                bus.Urg = Urg;
                bus.CliParado = CliParado;
                bus.ID_Centro = ID_Centro;
                bus.Breve = Breve;
                bus.Expo = Expo;
                bus.Obs = Obs;
                bus.EmpAux = EmpAux;
                bus.ID_Dir = ID_Dir;
                bus.UbiCont = UbiCont;
                bus.UbiTel = UbiTel;
                bus.Horario = Horario;
                bus.GuardarHor = GuardarHor;
                bus.UbiObs = UbiObs;
                bus.ID_Us_Asi = ID_Us_Asi;
                bus.Dia = Dia;
                bus.Hora = Hora;
                bus.Comen = Comen;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasnuevaasistenciaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasnuevaasistenciaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/nueva-asistencia/buscarcliente")]
        public async Task<strListaErr> AsistenciasAsistenciasNuevaAsistenciaBuscarCliente(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasnuevaasistenciabuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasnuevaasistenciabuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/nueva-asistencia/buscarusuario")]
        public async Task<strListaErr> AsistenciasAsistenciasNuevaAsistenciaBuscarUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasnuevaasistenciabuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasnuevaasistenciabuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/nueva-asistencia/buscardireccion")]
        public async Task<strAsistenciasAsistenciasNuevaSelCli> AsistenciasAsistenciasNuevaAsistenciaBuscarDireccion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasNuevaSelCli ret = new strAsistenciasAsistenciasNuevaSelCli();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasnuevaasistenciabuscardireccion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasNuevaSelCli>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasnuevaasistenciabuscardireccion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/nueva-asistencia/cambiodireccion")]
        public async Task<strDatoS> AsistenciasAsistenciasNuevaAsistenciaCambioDireccion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Dir);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasnuevaasistenciacambiodireccion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasnuevaasistenciacambiodireccion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Asistencias - En curso"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-en-curso")]
        public async Task<IActionResult> AsistenciasEnCurso(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasEnCursoListado lis = new strAsistenciasAsistenciasEnCursoListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Rel")); } catch { }
                string txtUrg = "";
                bool Urg = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "on") { txtUrg = "SI"; Urg = true; }
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "on") Man = true;

                string txtPartes = "";
                bool Partes = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Partes");
                if (txt == "on") { txtPartes = "SI"; Partes = true; }

                Int32 Anual = 0;
                try { Anual = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Anual")); } catch { }

                string QS = "";
                string QN = "";
                string OP = "";

                if (Transporte.parameters.ParamsIsPost)
                {
                    if (Transporte.parameters.ParamsKeys != null)
                    {
                        for (Int32 t = 0; t < Transporte.parameters.ParamsKeys.Length; t++)
                        {
                            string[] part = Transporte.parameters.ParamsKeys[t].Split("_");
                            if (part.Length == 2)
                            {
                                switch (part[0])
                                {
                                    case "qs":
                                        if (QS != "") QS += "_";
                                        QS += part[1];
                                        break;
                                    case "qn":
                                        if (QN != "") QN += "_";
                                        QN += part[1];
                                        break;
                                    case "ope":
                                        if (OP != "") OP += "_";
                                        OP += part[1];
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    QS = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QS");
                    QN = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QN");
                    OP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "OP");
                }



                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_ZonaSub"] = ID_ZonaSub;
                ViewData["ID_Tipo2"] = ID_Tipo2;
                ViewData["ID_Cli2Rel"] = ID_Cli2Rel;
                ViewData["Urg"] = Urg;
                ViewData["Man"] = Man;
                ViewData["Partes"] = Partes;
                ViewData["QS"] = QS;
                ViewData["QN"] = QN;
                ViewData["OP"] = OP;
                ViewData["Anual"] = Anual;

                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                bus.id2 = Anual;
                strDatoS filtro = new strDatoS();
                filtro.datoI = ID_ZonaSub;
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                filtro.datoS1 = txtUrg;
                filtro.datoS2 = QS;
                filtro.datoS3 = QN;
                filtro.datoS4 = OP;
                filtro.datoS5 = txtPartes + "|" + ID_Cli2Rel.ToString();
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasencursolistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasEnCursoListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasencursolistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasEnCursoListadoViewModel(lis);

            return View("AsistenciasEnCurso", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-en-curso/reasignar")]
        public async Task<transportout> AsistenciasAsistenciasEnCursoReasignar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                string txtUrg = "";
                bool Urg = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "true") { txtUrg = "SI"; Urg = true; }
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "true") Man = true;
                string ids = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ids");

                string QS = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QS");
                string QN = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QN");

                Int32 Num = 0;
                try { Num = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Num")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.id = ID_Us_Asi;
                bus.id2 = Num;
                strDatoS filtro = new strDatoS();
                filtro.datoI = ID_ZonaSub;
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                filtro.datoS1 = txtUrg;
                filtro.datoS2 = QS;
                filtro.datoS3 = QN;
                filtro.datoS4 = ids;
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasencursoreasignar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasencursoreasignar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-en-curso/replanificar")]
        public async Task<transportout> AsistenciasAsistenciasEnCursoReplanificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Comen = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Comen");
                string Dia = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia");
                string Hora = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Hora");
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Ope = 0;
                try { ID_Ope = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ope")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Asis2;
                bus.datoD = Convert.ToDecimal(ID_Ope);
                bus.datoS1 = Dia;
                bus.datoS2 = Hora;
                bus.datoS3 = Comen;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasencursoreplanificar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasencursoreplanificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-en-curso/imprimir")]
        public async Task<IActionResult> AsistenciasAsistenciasEnCursoImprimir(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Asis2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporasignarimprimir", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
                }
                else
                {
                    strDatoS err500 = new strDatoS
                    {
                        datoS1 = dat.err.titulo,
                        datoS2 = dat.err.mensaje
                    };
                    Transporte.obj = err500;

                    dat = await Infrastructure.ReturnResults.GetResponseAsync("error/extraneterror500", Transporte, HttpContext);

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporasignarimprimir-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            //return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
            return File(lis.Fichero, mediaType.ToString());

        }

        // Calendario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-en-curso/calendario-tecnico")]
        public async Task<strCalendarioTareas> AsistenciasAsistenciasEnCursoCalendarioTecnico(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCalendarioTareas ret = new strCalendarioTareas();

            try
            {
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 año = 0;
                try { año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "año")); } catch { }
                Int32 mes = 0;
                try { mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "mes")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Us_Asi;
                dato.datoS1 = año.ToString();
                dato.datoS2 = mes.ToString();
                dato.datoB = true;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasencursocalendariotecnico", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCalendarioTareas>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias-extranetasistenciasasistenciasencursocalendariotecnico-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-en-curso/carga-dia-tecnico")]
        public async Task<strAsistenciasAsistenciasEnCursoCargaDiaTecnico> AsistenciasAsistenciasEnCursoCargaDiaTecnico(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasEnCursoCargaDiaTecnico ret = new strAsistenciasAsistenciasEnCursoCargaDiaTecnico();

            try
            {
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string año = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "año");
                string mes = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "mes");
                string dia = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "dia");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Us_Asi;
                dato.datoS1 = dia;
                dato.datoS2 = mes;
                dato.datoS3 = año;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasencursocargadiatecnico", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasEnCursoCargaDiaTecnico>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias-extranetasistenciasasistenciasencursocargadiatecnico-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-en-curso/info-asistencia-planificar")]
        public async Task<strAsistenciasAsistenciasEnCursoInfoAsistenciaPlanificar> AsistenciasAsistenciasEnCursoInfoAsistenciaPlanificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasEnCursoInfoAsistenciaPlanificar ret = new strAsistenciasAsistenciasEnCursoInfoAsistenciaPlanificar();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Asis2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasencursoinfoasistenciaplanificar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasEnCursoInfoAsistenciaPlanificar>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias-extranetasistenciasasistenciasencursoinfoasistenciaplanificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Asistencias - Carga de trabajo"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/carga-de-trabajo")]
        public async Task<IActionResult> AsistenciasCargaDeTrabajo(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS lis = new strDatoS();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascargadetrabajoinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascargadetrabajoinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasCargaDeTrabajoInicioViewModel(lis);

            return View("AsistenciasCargaDeTrabajo", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/carga-de-trabajo/replanificar")]
        public async Task<transportout> AsistenciasAsistenciasCargaDeTrabajoSemanaReplanificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 ID_Us_Asin = 0;
                try { ID_Us_Asin = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asin")); } catch { }
                string Dia = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia");
                string Hora = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Hora");
                string Comen = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Comen");
                string ID_AsisPlan = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_AsisPlan");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Us_Asi;
                dato.datoD = Convert.ToDecimal(ID_Us_Asin);
                dato.datoS1 = Dia;
                dato.datoS2 = Hora;
                dato.datoS3 = Comen;
                dato.datoS4 = ID_AsisPlan;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciascargadetrabajosemanareplanificar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascargadetrabajosemanareplanificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/carga-de-trabajo/planificar")]
        public async Task<transportout> AsistenciasAsistenciasCargaDeTrabajoSemanaPlanificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string Dia = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia");
                string Hora = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Hora");
                string Comen = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Comen");
                string ID_Asis2s = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2s");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Us_Asi;
                dato.datoS1 = Dia;
                dato.datoS2 = Hora;
                dato.datoS3 = Comen;
                dato.datoS4 = ID_Asis2s;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciascargadetrabajosemanaplanificar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascargadetrabajosemanaplanificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/carga-de-trabajo/buscar-tecnico")]
        public async Task<strListaErr> AsistenciasAsistenciasCargaDeTrabajoBuscarTecnico(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascargadetrabajobuscartecnico", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascargadetrabajobuscartecnico-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/carga-de-trabajo/semana")]
        public async Task<strAsistenciasAsistenciasCargaDeTrabajo> AsistenciasAsistenciasCargaDeTrabajoSemana(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasCargaDeTrabajo ret = new strAsistenciasAsistenciasCargaDeTrabajo();

            try
            {
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string Fe_Ini = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ini");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = Fe_Ini;
                dato.datoI = ID_Us_Asi;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascargadetrabajosemana", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasCargaDeTrabajo>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascargadetrabajosemana-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/carga-de-trabajo/sin-planificar")]
        public async Task<strListadoConPaginador> AsistenciasAsistenciasCargaDeTrabajoSinPlanificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListadoConPaginador ret = new strListadoConPaginador();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");

                Int32 num = 30;


                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }

                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = "";
                dato.id = ID_Us_Asi;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                strDatoS filtro = new strDatoS();
                filtro.datoS1 = Pob;
                filtro.datoS2 = CP;
                dato.obj = filtro;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascargadetrabajosinplanificar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascargadetrabajosinplanificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        #endregion


        #endregion


        #endregion

        #region "Modulo Asistencias - Comisionar"

        #region "Vistas"


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar")]
        public async Task<IActionResult> AsistenciasComisionar(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasComisionarResumen lis = new strAsistenciasAsistenciasComisionarResumen();

            try
            {
                Int32 pag = 0;
                Int32 pagori = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pagori = pag; pag -= 1; } catch { }
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vopecom")); } catch { }
                string Fac = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["Fe_Fi"] = HttpUtility.UrlEncode(Fe_Fi);
                ViewData["ID_Us_Asi"] = ID_Us_Asi;
                ViewData["pag"] = pagori;
                ViewData["Fac"] = Fac;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Us_Asi;
                bus.buscar = Fe_Fi;
                bus.numReg = 20;
                bus.pagina = pag;
                strDatoS filtro = new strDatoS();
                filtro.datoS1 = Fac;
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasComisionarResumen>(dat.obj.ToString());
                    ViewData["Fe_Fi"] = HttpUtility.UrlEncode(lis.Fe_Fi);
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasComisionarInicioViewModel(lis);

            return View("AsistenciasComisionar", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/factura")]
        public async Task<IActionResult> AsistenciasComisionarFactura(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasComisionarFactura lis = new strAsistenciasAsistenciasComisionarFactura();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Fac"] = ID_Fac;
                ViewData["pag"] = pag;
                ViewData["Fe_Fi"] = Fe_Fi;
                ViewData["ID_Us_Asi"] = ID_Us_Asi;
                strDato bus = new strDato();
                bus.datoI = ID_Fac;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarfactura", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasComisionarFactura>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarfactura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasComisionarFacturaViewModel(lis);

            return View("AsistenciasComisionarFactura", viewModel);
        }


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/factura/detalle/addoperario")]
        public async Task<transportout> AsistenciasAsistenciasComisionarAddOperario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }

                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 ID_De_Al = 0;
                try { ID_De_Al = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Al")); } catch { }
                Int32 ID_Al2 = 0;
                try { ID_Al2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Al2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Fac;
                bus.datoD = Convert.ToDecimal(ID_Us_Asi);
                bus.datoS1 = ID_De_Al.ToString();
                bus.datoS2 = ID_Al2.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionaraddoperario", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionaraddoperario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/factura/detalle/comisionesguardar")]
        public async Task<transportout> AsistenciasAsistenciasComisionarFacturaDetallesComisionarGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_De_Al = 0;
                try { ID_De_Al = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Al")); } catch { }
                Int32 ID_Al2 = 0;
                try { ID_Al2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Al2")); } catch { }
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ids");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Fac;
                bus.datoS1 = ID_De_Al.ToString();
                bus.datoS2 = ID_Al2.ToString();
                bus.datoS3 = txt;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallescomisionesguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallescomisionesguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/factura/detalle/comisioneseliminar")]
        public async Task<transportout> AsistenciasAsistenciasComisionarFacturaDetallesComisionesEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_De_Al = 0;
                try { ID_De_Al = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Al")); } catch { }
                Int32 ID_Al2 = 0;
                try { ID_Al2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Al2")); } catch { }
                Int32 ID_Cmt = 0;
                try { ID_Cmt = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cmt")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Fac;
                bus.datoS1 = ID_De_Al.ToString();
                bus.datoS2 = ID_Al2.ToString();
                bus.datoS3 = ID_Cmt.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallescomisioneseliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallescomisioneseliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/factura/detalle/comisionescerrar")]
        public async Task<transportout> AsistenciasAsistenciasComisionarFacturaDetallesComisionesCerrar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Fac;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallescomisionescerrar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallescomisionescerrar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/factura/detalle")]
        public async Task<strAsistenciasAsistenciasComisionarFacturaAlbaranDetalle> AsistenciasAsistenciasComisionesFacturaDetalle(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasComisionarFacturaAlbaranDetalle ret = new strAsistenciasAsistenciasComisionarFacturaAlbaranDetalle();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_Al2 = 0;
                try { ID_Al2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Al2")); } catch { }
                Int32 ID_De_Al = 0;
                try { ID_De_Al = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Al")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Fac;
                dato.datoD = Convert.ToDecimal(ID_Al2);
                dato.datoS = ID_De_Al.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetalle", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasComisionarFacturaAlbaranDetalle>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetalle-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/factura/detalle/comisiones")]
        public async Task<strAsistenciasAsistenciasComisionarFacturaAlbaranDetalleComisones> AsistenciasAsistenciasComisionesFacturaDetalleComisiones(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasComisionarFacturaAlbaranDetalleComisones ret = new strAsistenciasAsistenciasComisionarFacturaAlbaranDetalleComisones();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_Al2 = 0;
                try { ID_Al2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Al2")); } catch { }
                Int32 ID_De_Al = 0;
                try { ID_De_Al = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Al")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Fac;
                dato.datoD = Convert.ToDecimal(ID_Al2);
                dato.datoS = ID_De_Al.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallecomisiones", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasComisionarFacturaAlbaranDetalleComisones>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarfacturadetallecomisiones-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisionar/buscar-tecnico")]
        public async Task<strListaErr> AsistenciasAsistenciasComisionarBuscarTecnico(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionarbuscartecnico", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionarbuscartecnico-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        #endregion


        #endregion


        #endregion

        #region "Modulo Asistencias - Comisiones"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisiones")]
        public async Task<IActionResult> AsistenciasComisiones(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasComisionesResumen lis = new strAsistenciasAsistenciasComisionesResumen();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 Tipo = 0;
                try { Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tipo")); } catch { }
                //Int32 ID_Us_Com = 0;
                //try { ID_Us_Com = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Com")); } catch { }
                //Int32 ID_Cli2_Com = 0;
                //try { ID_Cli2_Com = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2_Com")); } catch { }
                Int32 ID_Us_Pend = 0;
                try { ID_Us_Pend = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vopecompend")); } catch { }
                string topecompend = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "topecompend");
                Int32 ID_Cli2_Pend = 0;
                try { ID_Cli2_Pend = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vclicompend")); } catch { }
                string tclicompend = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "tclicompend");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;
                ViewData["Tipo"] = Tipo;


                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 40;
                bus.pagina = pag;
                strDatoS filtro = new strDatoS();
                filtro.datoI = Tipo;
                filtro.datoS1 = ID_Us_Pend.ToString(); //ID_Us_Com.ToString();
                filtro.datoS2 = ID_Cli2_Pend.ToString();
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionesinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasComisionesResumen>(dat.obj.ToString());
                    ViewData["vopecompend"] = lis.vopecompend;
                    ViewData["topecompend"] = lis.topecompend;
                    ViewData["vclicompend"] = lis.vclicompend;
                    ViewData["tclicompend"] = lis.tclicompend;
                    if (Fe_In!="" && Fe_Fi!="")
                    {
                        ViewData["Fe_In"] = Fe_In;
                        ViewData["Fe_Fi"] = Fe_Fi;
                    } else
                    {
                        ViewData["Fe_In"] = lis.Fe_In;
                        ViewData["Fe_Fi"] = lis.Fe_Fi;
                    }

                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionesinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasComisionesInicioViewModel(lis);

            return View("AsistenciasComisiones", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisiones/factura")]
        public async Task<IActionResult> AsistenciasComisionesFactura(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasComisionesFactura lis = new strAsistenciasAsistenciasComisionesFactura();

            try
            {
                Int32 ID_Com = 0;
                try { ID_Com = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Com")); } catch { }
                //Int32 pag = 0;
                //try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); } catch { }
                //Int32 ID_Us_Asi = 0;
                //try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                //string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["ID_Fac"] = ID_Fac;
                //ViewData["pag"] = pag;
                //ViewData["Fe_Fi"] = Fe_Fi;
                //ViewData["ID_Us_Asi"] = ID_Us_Asi;
                strDato bus = new strDato();
                bus.datoI = ID_Com;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionesfactura", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasComisionesFactura>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionesfactura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasComisionesFacturaViewModel(lis);

            return View("AsistenciasComisionesFactura", viewModel);
        }


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisiones/realizar-pago")]
        public async Task<transportout> AsistenciasAsistenciasComisionesDebeHaberRealizarPago(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2Com = 0;
                try { ID_Cli2Com = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Com")); } catch { }
                Int32 ID_Us_Com = 0;
                try { ID_Us_Com = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Com")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                decimal Imp = 0;
                try { Imp = Convert.ToDecimal(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp").Replace(".",",")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Us_Com;
                bus.datoD = Imp;
                bus.datoS1 = ID_Cli2Com.ToString();
                bus.datoS2 = Expo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionesdebehaberrealizarpago", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionesdebehaberrealizarpago-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisiones/comisionesdebehaber")]
        public async Task<strListadoConPaginador> AsistenciasAsistenciasComisionesDebeHaber(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListadoConPaginador ret = new strListadoConPaginador();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Int32 num = 40;


                Int32 ID_Us_Com = 0;
                try { ID_Us_Com = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Com")); } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.id = ID_Us_Com;
                dato.id2 = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                strDatoS filtro = new strDatoS();
                filtro.datoS1 = Fe_In;
                filtro.datoS2 = Fe_Fi;
                dato.obj = filtro;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionesdebehaber", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionesdebehaber-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisiones/pendientes/buscar-tecnico")]
        public async Task<strListaErr> AsistenciasAsistenciasComisionesPendientesBuscarTecnico(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionespendientesbuscartecnico", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionespendientesbuscartecnico-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/comisiones/pendientes/buscar-cliente")]
        public async Task<strListaErr> AsistenciasAsistenciasComisionesPendientesBuscarCliente(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciascomisionespendientesbuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascomisionespendientesbuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Asistencias - Produccion"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/produccion")]
        public async Task<IActionResult> AsistenciasProduccion(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasProduccionResumen lis = new strAsistenciasAsistenciasProduccionResumen();

            try
            {
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vprod-ope")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Us_Asi"] = ID_Us_Asi;
                ViewData["Fe_In"] = Fe_In;
                ViewData["Fe_Fi"] = Fe_Fi;

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Us_Asi;
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasproduccion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasProduccionResumen>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciascargadetrabajoinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasProduccionViewModel(lis);

            return View("AsistenciasProduccion", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/produccion/buscar-tecnico")]
        public async Task<strListaErr> AsistenciasAsistenciasProduccionBuscarTecnico(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasproduccionbuscartecnico", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasproduccionbuscartecnico-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Asistencias - Por facturar"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar")]
        public async Task<IActionResult> AsistenciasPorFacturar(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasPorFacturarListado lis = new strAsistenciasAsistenciasPorFacturarListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                string txt = "";
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "on") Man = true;

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_Tipo2"] = ID_Tipo2;
                ViewData["Man"] = Man;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                strDatoS filtro = new strDatoS();
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarlistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasPorFacturarListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasPorFacturarListadoViewModel(lis);

            return View("AsistenciasPorFacturar", viewModel);
        }

        // Eliminar recurso *******************************************************************************************************************************************************************
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/asistencia")]
        public async Task<IActionResult> AsistenciasPorFacturarAsistencia(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasPorAsignarListado lis = new strAsistenciasAsistenciasPorAsignarListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                string txtUrg = "";
                bool Urg = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "on") { txtUrg = "SI"; Urg = true; }
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "on") Man = true;

                string QS = "";
                string QN = "";

                if (Transporte.parameters.ParamsIsPost)
                {
                    if (Transporte.parameters.ParamsKeys != null)
                    {
                        for (Int32 t = 0; t < Transporte.parameters.ParamsKeys.Length; t++)
                        {
                            string[] part = Transporte.parameters.ParamsKeys[t].Split("_");
                            if (part.Length == 2)
                            {
                                switch (part[0])
                                {
                                    case "qs":
                                        if (QS != "") QS += "_";
                                        QS += part[1];
                                        break;
                                    case "qn":
                                        if (QN != "") QN += "_";
                                        QN += part[1];
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    QS = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QS");
                    QN = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "QN");
                }



                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_ZonaSub"] = ID_ZonaSub;
                ViewData["ID_Tipo2"] = ID_Tipo2;
                ViewData["Urg"] = Urg;
                ViewData["Man"] = Man;
                ViewData["QS"] = QS;
                ViewData["QN"] = QN;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                strDatoS filtro = new strDatoS();
                filtro.datoI = ID_ZonaSub;
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                filtro.datoS1 = txtUrg;
                filtro.datoS2 = QS;
                filtro.datoS3 = QN;
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporasignarlistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasPorAsignarListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporasignarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasPorAsignarListadoViewModel(lis);

            return View("AsistenciasPorFacturarAsistencia", viewModel);
        }
        // Eliminar recurso fin *******************************************************************************************************************************************************************

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar")]
        public async Task<IActionResult> AsistenciasAsistenciasPorFacturarFacturar(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasPorFacturarFacturar lis = new strAsistenciasAsistenciasPorFacturarFacturar();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                bus.datoI = ID_Asis2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasPorFacturarFacturar>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasPorFacturarFacturarViewModel(lis);

            return View("AsistenciasPorFacturarRevisar", viewModel);
        }


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/detalle-no-facturar")]
        public async Task<transportout> AsistenciasAsistenciasPorFacturarFacturarDetalleNoFacturarGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Parte2 = 0;
                try { ID_Parte2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Parte2")); } catch { }
                Int32 ID_De_Pa = 0;
                try { ID_De_Pa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Pa")); } catch { }
                bool NoFac = false;
                try { NoFac = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NoFac")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax bus = new strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax();
                bus.ID_Asis2 = ID_Asis2;
                bus.ID_Parte2 = ID_Parte2;
                bus.ID_De_Pa = ID_De_Pa;
                bus.NoFac = NoFac;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallenofacturarguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallenofacturarguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/no-facturar-todos")]
        public async Task<transportout> AsistenciasAsistenciasPorFacturarFacturarDetalleNoFacturarTodosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Parte2 = 0;
                try { ID_Parte2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Parte2")); } catch { }
                bool NoFac = false;
                try { NoFac = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NoFac")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax bus = new strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax();
                bus.ID_Asis2 = ID_Asis2;
                bus.ID_Parte2 = ID_Parte2;
                bus.NoFac = NoFac;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallenofacturartodosguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallenofacturartodosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/guardar-detalle")]
        public async Task<transportout> AsistenciasAsistenciasPorFacturarFacturarDetalleAjaxGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Parte2 = 0;
                try { ID_Parte2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Parte2")); } catch { }
                Int32 ID_De_Pa = 0;
                try { ID_De_Pa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Pa")); } catch { }
                string Codigo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Codigo");
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string Can = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Can");
                string PVP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PVP");
                bool NoFac = false;
                try { NoFac = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NoFac")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax bus = new strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax();
                bus.ID_Asis2 = ID_Asis2;
                bus.ID_Parte2 = ID_Parte2;
                bus.ID_De_Pa = ID_De_Pa;
                bus.Codigo = Codigo;
                bus.Serv = Expo;
                bus.Can = Can;
                bus.PVP = PVP;
                bus.NoFac = NoFac;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetalleajaxguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetalleajaxguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/procesar")]
        public async Task<transportout> AsistenciasAsistenciasPorFacturarFacturarDetallesProcesar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Pa");
                bool Fe_Alb = false;
                try { Fe_Alb = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Alb")); } catch { }

                Int32[] ID_De_Pa = null;

                if (txt!="")
                {
                    string[] txtID_De_Pa = txt.Split("_");
                    ID_De_Pa = Array.ConvertAll<string, Int32>(txtID_De_Pa, Int32.Parse);
                }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strAsistenciasAsistenciasPorFacturarProcesar bus = new strAsistenciasAsistenciasPorFacturarProcesar();
                bus.ID_Asis2 = ID_Asis2;
                bus.Fe_Alb = Fe_Alb;
                bus.ID_De_Pa = ID_De_Pa;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallesprocesar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallesprocesar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/obscli-guardar")]
        public async Task<transportout> AsistenciasAsistenciasPorFacturarFacturarObsCliGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Asis2);
                bus.datoS = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturarobscliguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturarobscliguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/obsclirel-guardar")]
        public async Task<transportout> AsistenciasAsistenciasPorFacturarFacturarObsCliRelGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Rel")); } catch { }

                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2Rel;
                bus.datoD = Convert.ToDecimal(ID_Asis2);
                bus.datoS = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturarobsclirelguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturarobsclirelguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/addservicio")]
        public async Task<transportout> AsistenciasAsistenciasPorFacturarDetallesAddServicio(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Parte2 = 0;
                try { ID_Parte2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Parte2")); } catch { }
                string Can = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Can");
                string ID_Serv2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serv2");
                string Precio = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precio");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Asis2;
                bus.datoD = Convert.ToDecimal(ID_Parte2);
                bus.datoS1 = Can;
                bus.datoS2 = ID_Serv2;
                bus.datoS3 = Precio;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturardetallesaddservicio", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturardetallesaddservicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/imprimir")]
        public async Task<IActionResult> AsistenciasAsistenciasPorFacturarImprimir(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Asis2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporasignarimprimir", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
                }
                else
                {
                    strDatoS err500 = new strDatoS
                    {
                        datoS1 = dat.err.titulo,
                        datoS2 = dat.err.mensaje
                    };
                    Transporte.obj = err500;

                    dat = await Infrastructure.ReturnResults.GetResponseAsync("error/extraneterror500", Transporte, HttpContext);

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporasignarimprimir-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            //return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
            return File(lis.Fichero, mediaType.ToString());

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/detalles")]
        public async Task<strAsistenciasAsistenciasPorFacturarFacturarAjax> AsistenciasAsistenciasPorFacturarFacturarDetallesAjax(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasPorFacturarFacturarAjax ret = new strAsistenciasAsistenciasPorFacturarFacturarAjax();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Asis2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallesajax", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasPorFacturarFacturarAjax>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetallesajax-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/detalle")]
        public async Task<strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax> AsistenciasAsistenciasPorFacturarFacturarDetalleAjax(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax ret = new strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }
                Int32 ID_Parte2 = 0;
                try { ID_Parte2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Parte2")); } catch { }
                Int32 ID_De_Pa = 0;
                try { ID_De_Pa = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_De_Pa")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Asis2;
                dato.datoD = Convert.ToDecimal(ID_Parte2);
                dato.datoS = ID_De_Pa.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetalleajax", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturardetalleajax-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/asistencias-por-facturar/facturar/buscarservicio")]
        public async Task<strServiciosServiciosListado> PresupuestosPresupuesto_BuscarServicio(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strServiciosServiciosListado ret = new strServiciosServiciosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.clase = clase;
                dato.numReg = 15;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasporfacturarfacturarbuscarservicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strServiciosServiciosListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasporfacturarfacturarbuscarservicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Asistencias - Historial"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/historial")]
        public async Task<IActionResult> AsistenciasHistorial(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasHistorialListado lis = new strAsistenciasAsistenciasHistorialListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                string txtUrg = "";
                bool Urg = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg");
                if (txt == "on") { txtUrg = "SI"; Urg = true; }
                bool Man = false;
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Man");
                if (txt == "on") Man = true;



                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_ZonaSub"] = ID_ZonaSub;
                ViewData["ID_Tipo2"] = ID_Tipo2;
                ViewData["Urg"] = Urg;
                ViewData["Man"] = Man;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                strDatoS filtro = new strDatoS();
                filtro.datoI = ID_ZonaSub;
                filtro.datoD = Convert.ToDecimal(ID_Tipo2);
                filtro.datoB = Man;
                filtro.datoS1 = txtUrg;
                bus.obj = filtro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciashistoriallistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasHistorialListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciashistoriallistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasHistorialListadoViewModel(lis);

            return View("AsistenciasHistorial", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/historial/asistencia")]
        public async Task<IActionResult> AsistenciasHistorialAsistencia(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasHistorialAsistencia lis = new strAsistenciasAsistenciasHistorialAsistencia();

            try
            {
                Int32 ID_Asis2 = 0;
                try { ID_Asis2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asis2")); } catch { }

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Asis2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciashistorialasistencia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasHistorialAsistencia>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciashistorialasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasHistorialAsistenciaViewModel(lis);

            return View("AsistenciasHistorialAsistencia", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"


        #endregion


        #region "Recuperar datos - Get"


        #endregion


        #endregion


        #endregion


        #region "Modulo Asistencias - Configuracion"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion")]
        public async Task<IActionResult> AsistenciasConfiguracion(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasZonasListado lis = new strAsistenciasAsistenciasZonasListado();

            try
            {
                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasZonasListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasZonasListadoViewModel(lis);

            return View("AsistenciasConfiguracion", viewModel);
        }

        #region "Modulo Asistencias - Configuracion - Zonas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas")]
        public async Task<IActionResult> AsistenciasZonas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasZonasListado lis = new strAsistenciasAsistenciasZonasListado();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string cps = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "cps");
                Int32 ID_Zona = 0;
                try { ID_Zona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Zona")); } catch { }


                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["cps"] = cps;
                ViewData["ID_Zona"] = ID_Zona;
                //ViewData["ID_Idi"] = ID_Idi;
                strDatoS bus = new strDatoS();
                bus.datoI = ID_Zona;
                bus.datoS1 = buscar;
                bus.datoS2 = cps;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasZonasListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasZonasListadoViewModel(lis);

            return View("AsistenciasConfiguracion-Zonas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/reportes")]
        public async Task<IActionResult> AsistenciasReportes(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasZonasListado lis = new strAsistenciasAsistenciasZonasListado();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string cps = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "cps");
                Int32 ID_Zona = 0;
                try { ID_Zona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Zona")); } catch { }


                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["cps"] = cps;
                ViewData["ID_Zona"] = ID_Zona;
                //ViewData["ID_Idi"] = ID_Idi;
                strDatoS bus = new strDatoS();
                bus.datoI = ID_Zona;
                bus.datoS1 = buscar;
                bus.datoS2 = cps;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasAsistenciasZonasListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasAsistenciasZonasListadoViewModel(lis);

            return View("AsistenciasReportes", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/zona-detalles-guardar")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasZonaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Zona = 0;
                try { ID_Zona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Zona")); } catch { }
                string Zona = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Zona");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Zona;
                bus.datoS1 = Zona;
                bus.datoS2 = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/zona-detalles-eliminar")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasZonaEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Zona = 0;
                try { ID_Zona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Zona")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Zona;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonaeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/zona-nueva")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasZonaNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Zona = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Zona");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoS = Zona;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonanueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonanueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/subzona-add-cps")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasSubZonaNuevoCPs(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_ZonaSub = 0;
                try { ID_ZonaSub = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ZonaSub")); } catch { }
                string CPs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CPs");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoS = CPs;
                bus.datoI = ID_ZonaSub;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonanuevocps", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonanuevocps-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/subzona-cp-eliminar")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasSubZonaEliminarCPs(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_SubZona = 0;
                try { ID_SubZona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_SubZona")); } catch { }
                Int32 ID_CP = 0;
                try { ID_CP = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CP")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoD = Convert.ToDecimal(ID_CP);
                bus.datoI = ID_SubZona;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonaeliminarcps", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonaeliminarcps-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/subzona-eliminar")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasSubZonaEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_SubZona = 0;
                try { ID_SubZona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_SubZona")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_SubZona;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonaeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/subzona-guardar")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasSubZonaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_SubZona = 0;
                try { ID_SubZona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_SubZona")); } catch { }
                Int32 ID_Zona = 0;
                try { ID_Zona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Zona")); } catch { }
                string Zona = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Zona");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_SubZona;
                bus.datoD = Convert.ToDecimal(ID_Zona);
                bus.datoS = Zona;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/subzona-nueva")]
        public async Task<transportout> AsistenciasAsistenciasConfiguracionZonasSubZonaNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Subzona = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Subzona");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoS = Subzona;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonanueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonanueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/zona-detalles")]
        public async Task<strDatoS> AsistenciasAsistenciasConfiguracionZonasZonaDetalle(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Zona = 0;
                try { ID_Zona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Zona")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Zona;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonadetalle", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonaszonadetalle-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/configuracion/zonas/subzona-detalles")]
        public async Task<strAsistenciasAsistenciasZonasSubZonaDetalles> AsistenciasAsistenciasConfiguracionZonasSubZonaDetalle(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasAsistenciasZonasSubZonaDetalles ret = new strAsistenciasAsistenciasZonasSubZonaDetalles();

            try
            {
                Int32 ID_SubZona = 0;
                try { ID_SubZona = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_SubZona")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_SubZona;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonadetalle", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasAsistenciasZonasSubZonaDetalles>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciasasistenciasconfiguracionzonassubzonadetalle-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #endregion

        #region "Modulo Asistencias - Partes de asistencia - Listado"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/partes-de-asistencia-por-cerrar")]
        public async Task<IActionResult> AsistenciasPartesDeTrabajoAbiertosListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasPartesListado lis = new strAsistenciasPartesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                //Int32 ID_Cli2 = 0;
                //try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.numReg = 50;
                bus.pagina = pag;
                //bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciaspartesdetrabajoabiertoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasPartesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciaspartesdetrabajoabiertoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasPartesListadoViewModel(lis);

            return View("AsistenciasPartesPorCerrarListado", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/partes-de-asistencia-por-cerrar/cerrar-parte")]
        public async Task<IActionResult> AsistenciasPartesCerrarParte(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasPartesListado lis = new strAsistenciasPartesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                //Int32 ID_Cli2 = 0;
                //try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.numReg = 50;
                bus.pagina = pag;
                //bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciaspartesdetrabajoabiertoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasPartesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciaspartesdetrabajoabiertoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasPartesListadoViewModel(lis);

            return View("AsistenciasPartesCerrarParte", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-asistencias/partes-de-asistencia-historial")]
        public async Task<IActionResult> AsistenciasPartesDeTrabajoHistorialListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasPartesListado lis = new strAsistenciasPartesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                //Int32 ID_Cli2 = 0;
                //try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.numReg = 50;
                bus.pagina = pag;
                //bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloasistencias/extranetasistenciaspartesdetrabajohistoriallistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strAsistenciasPartesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloasistencias/extranetasistenciaspartesdetrabajohistoriallistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloAsisnteicasPartesListadoViewModel(lis);

            return View("AsistenciasPartesHistorialListado", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"


        #endregion


        #region "Recuperar datos - Get"


        #endregion


        #endregion


        #endregion


    }
}
