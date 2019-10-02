
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using MyGaolosLibrary;

namespace Gaolos.Controllers
{
    public class MyGaolosController : Controller
    {

        #region "Vistas"

        /* Bandeja de entrada */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("bandeja-de-entrada")]
        public async Task<IActionResult> TareasPendientesBandejaDeEntrada(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloUsuarioTareasPendientesListado lis = new strModuloUsuarioTareasPendientesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                //Transporte.Params.Path = HttpContext.Request.Path.Value;
                //Transporte.Params.PathParams = "";

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = "",
                    numReg = 50,
                    pagina = pag
                };
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendientesbandejadeentrada", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranettareaspendientesbandejadeentrada/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloUsuarioTareasPendientesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranettareaspendientesbandejadeentrada-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MyGaolosTareasPendienesListadoViewModel(lis);

            return View("BandejaDeEntrada", viewModel);

        }

        /* Listado de tareas pendientes */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("tareas-pendientes")]
        public async Task<IActionResult> TareasPendientesListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloUsuarioTareasPendientesListado lis = new strModuloUsuarioTareasPendientesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                //Transporte.Params.Path = HttpContext.Request.Path.Value;
                //Transporte.Params.PathParams = "";
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 50,
                    pagina = pag
                };
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendienteslistado", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranettareaspendienteslistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloUsuarioTareasPendientesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranettareaspendienteslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MyGaolosTareasPendienesListadoViewModel(lis);

            return View("TareasPendientes", viewModel);
        }

        /* Detalles de la Tarea Pendiente [ficha] */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("tareas-pendientes/tarea-pendiente")]
        public async Task<IActionResult> TareaPendiente(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloUsuarioTareaPendiente lis = new strModuloUsuarioTareaPendiente();

            try
            {
                Int32 ID_Twit = 0;
                try { ID_Twit = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Twit")); } catch { }
                Int32 ID_Soli2 = 0;
                try { ID_Soli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Soli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = "",
                    id = ID_Twit,
                    id2 = ID_Soli2,
                    ID_Idi = ID_IdiPlataforma
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendientestareapendiente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloUsuarioTareaPendiente>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranettareaspendientestareapendiente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MyGaolosTareasPendienesTareaPendienteViewModel(lis);

            return View("TareaPendiente", viewModel);
        }

        /* My Gaolos */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-perfil")]
        public async Task<IActionResult> MiPerfil(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strMiPerfil lis = new strMiPerfil();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                //Transporte.Params.Path = HttpContext.Request.Path.Value;
                //Transporte.Params.PathParams = "";
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = "",
                    numReg = 50,
                    pagina = pag
                };
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranetmiperfil", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranetmiperfil/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strMiPerfil>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranetmiperfil-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MyGaolosMiPerfilViewModel(lis);

            return View("MiPerfil", viewModel);

        }

        /* Bandeja de entrada */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("Calendario")]
        public async Task<IActionResult> CalendarioInicio(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDatoS lis = new strDatoS();

            try
            {
                Int32 año = 0;
                try { año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "año")); } catch { }
                Int32 mes = 0;
                try { mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "mes")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato
                {
                    datoD = Convert.ToDecimal(mes),
                    datoI = año
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranetcalendarioinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                    ViewData["año"] = lis.datoS2;
                    ViewData["mes"] = lis.datoS1;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranetcalendarioinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new MyGaolosCalendarioInicioViewModel(lis);

            return View("Calendario", viewModel);

        }


        #endregion


        #region "Ajax - Modulo Usuarios"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-nueva")]
        public async Task<transportout> TareaNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string RefNeg = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefNeg");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Ho_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Ho_In");
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                Int32 Prio = 0;
                try { Prio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prio")); } catch { }
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CtrlLec");
                bool CtrlLec = false;
                if (txt.ToLower() == "true") { CtrlLec = true; }
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CtrlFin");
                bool CtrlFin = false;
                if (txt.ToLower() == "true") { CtrlFin = true; }

                Int32 ID_Emp = 0;
                bool EsCli = false;
                try
                {
                    string id1 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id1");
                    string[] txt2 = id1.Split("_");
                    ID_Emp = Convert.ToInt32(txt2[0]);
                    if (txt2[1] == "c") { EsCli = true; }
                }
                catch { }


                string Fecha = Fe_In + " " + Ho_In;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strTareaPendienteNueva dato = new strTareaPendienteNueva();
                dato.Acuse = CtrlLec;
                dato.CtrlFin = CtrlFin;
                dato.Expo = Expo;
                dato.Fe_Prev_Ini = Fecha;
                dato.Fe_Prev_Fin = "";
                dato.ID_TwitIni = 0;
                dato.ID_TwitPadre = 0;
                dato.ID_Us_Asi = ID_Us_Asi;
                dato.Prioridad = Prio;
                dato.Privado = false;
                dato.RefNeg = RefNeg;
                dato.ID_Emp = ID_Emp;
                dato.EsCli = EsCli;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientestareapendientenueva", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranettareaspendientestareapendientenueva", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientestareapendientenueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-replanificar")]
        public async Task<transportout> TareaReplanificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Twit = 0;
                try { ID_Twit = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Twit")); } catch { }
                Int32 ID_TwitIni = 0;
                try { ID_TwitIni = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TwitIni")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string Fe_Apa = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Apa");
                Int32 Min = 0; ;
                try { Min = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Min")); } catch { }
                string RefNeg = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefNeg");
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 Prio = 0;
                try { Prio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prio")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strTareaPendienteReplanificar dato = new strTareaPendienteReplanificar
                {
                    ID_Twit = ID_Twit,
                    ID_TwitIni = ID_TwitIni,
                    Expo = Expo,
                    AplazarFecha = Fe_Apa,
                    AplazarRapido = Min,
                    RefNeg = RefNeg,
                    ID_Us_Asi = ID_Us_Asi,
                    Prioridad = Prio
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientestareapendientereplanificar", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranettareaspendientestareapendientereplanificar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientestareapendientereplanificar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-reabrir")]
        public async Task<transportout> TareasPendientesTareaPendienteReAbrir(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Twit = 0;
                try { ID_Twit = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Twit")); } catch { }
                Int32 ID_TwitIni = 0;
                try { ID_TwitIni = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TwitIni")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string Fe_Apa = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Apa");
                Int32 Min = 0; ;
                try { Min = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Min")); } catch { }
                string RefNeg = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefNeg");
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 Prio = 0;
                try { Prio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prio")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strTareaPendienteReplanificar dato = new strTareaPendienteReplanificar
                {
                    ID_Twit = ID_Twit,
                    ID_TwitIni = ID_TwitIni,
                    Expo = Expo,
                    AplazarFecha = Fe_Apa,
                    AplazarRapido = Min,
                    RefNeg = RefNeg,
                    ID_Us_Asi = ID_Us_Asi,
                    Prioridad = Prio
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientestareapendientereabrir", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientestareapendientereabrir-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-cerrar")]
        public async Task<transportout> TareaCerrar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Twit = 0;
                try { ID_Twit = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Twit")); } catch { }
                Int32 ID_TwitIni = 0;
                try { ID_TwitIni = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TwitIni")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strTareaPendienteReplanificar dato = new strTareaPendienteReplanificar();
                dato.ID_Twit = ID_Twit;
                dato.ID_TwitIni = ID_TwitIni;
                dato.Expo = Expo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientestareapendientecerrar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientestareapendientecerrar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-inscripcioncursocambiarcurso")]
        public async Task<transportout> TareaInscripcionCursoCambiarCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Soli2 = 0;
                try { ID_Soli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Soli2")); } catch { }
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Soli2;
                dato.datoD = Convert.ToDecimal(ID_Curso2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareapendientesolicitudinscripcioncursocambiarcurso", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranettareapendientesolicitudinscripcioncursocambiarcurso", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareapendientesolicitudinscripcioncursocambiarcurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-inscripcion-curso")]
        public async Task<transportout> TareaPendienteTareaPendienteSolicitudInscripcionCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Soli2 = 0;
                try { ID_Soli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Soli2")); } catch { }
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Precio = 0;
                try { ID_Precio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Precio")); } catch { }
                string valbool = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvCom");
                bool EnvCom = false;
                if (valbool.ToLower() == "true") { EnvCom = true; }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Soli2;
                dato.datoD = Convert.ToDecimal(ID_Curso2);
                dato.datoS = ID_Precio.ToString();
                dato.datoB = EnvCom;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientestareapendientesolicitudinscripcioncurso", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientestareapendientesolicitudinscripcioncurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-curso-enviar-mail-info")]
        public async Task<transportout> TareaPendienteSolicitudCambiarMails(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Soli2 = 0;
                try { ID_Soli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Soli2")); } catch { }
                string valbool = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvCom");
                bool EnvCom = false;
                if (valbool.ToLower() == "true") { EnvCom = true; }
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "nuevoMail");
                string MailOri = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "MailOri");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Soli2;
                dato.datoS1 = Mail;
                dato.datoS2 = MailOri;
                dato.datoB = EnvCom;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientestareapendientesolicitudcambiarmails", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientestareapendientesolicitudcambiarmails-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-inscripcioncursoeliminar")]
        public async Task<transportout> TareaInscripcionCursoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Soli2 = 0;
                try { ID_Soli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Soli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Soli2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareapendientesolicitudinscripcioncursoeliminar", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranettareapendientesolicitudinscripcioncursoeliminar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareapendientesolicitudinscripcioncursoeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/tarea-pendiente-cerrar-solicitud-sin-tarea-pendiente")]
        public async Task<transportout> TareasPendientesTareaPendienteCerrarSolicitudSinTareaAbierta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Soli2 = 0;
                try { ID_Soli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Soli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                //Transporte.Params.Path = HttpContext.Request.Path.Value;
                //Transporte.Params.PathParams = "";
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoI = ID_Soli2
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientestareapendientecerrarsolicitudsintareaabierta", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranettareaspendientestareapendientecerrarsolicitudsintareaabierta", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientestareapendientecerrarsolicitudsintareaabierta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-perfil/menucambiarorden")]
        public async Task<transportout> MiPerfilNegocioMenuCambiarOrden(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                string idd = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "idd");
                string RefNeg = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefNeg");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = idd;
                dato.datoS2 = RefNeg;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranetmiperfilnegociomenucambiarorden", Transporte, HttpContext);

                HttpContext.Session.SetString("reload_labelsmenugaolos", "s");

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranetmiperfilnegociomenucambiarorden-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-perfil/menucambiarordendraganddrop")]
        public async Task<transportout> MiPerfilNegocioMenuCambiarOrdenDragAndDrop(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                string indexo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "indexo");
                string indexd = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "indexd");
                string RefNeg = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefNeg");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = RefNeg;
                dato.datoS2 = indexo;
                dato.datoS3 = indexd;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranetmiperfilnegociomenucambiarordendraganddrop", Transporte, HttpContext);

                HttpContext.Session.SetString("reload_labelsmenugaolos", "s");

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranetmiperfilnegociomenucambiarordendraganddrop-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-perfil/cambiarclave")]
        public async Task<transportout> MiPerfilCambiarClave(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                string PassA = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PassA");
                string PassN = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PassN");
                string PassR = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PassR");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = PassA;
                dato.datoS2 = PassN;
                dato.datoS3 = PassR;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranetmiperfilcambiarclave", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranetmiperfilcambiarclave", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranetmiperfilcambiarclave-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mi-perfil/cambiarnombre")]
        public async Task<transportout> MiPerfilCambiarNombre(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                string Nom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Nom");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = Nom;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranetmiperfilcambiarnombre", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranetmiperfilcambiarnombre", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranetmiperfilcambiarnombre-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/vincularempresa")]
        public async Task<transportout> TareasPendientesVincularEmpresa(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Twit = 0;
                try { ID_Twit = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Twit")); } catch { }
                string id1 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id1");
                string[] txt = id1.Split("_");
                Int32 ID_Emp = 0;
                try { ID_Emp = Convert.ToInt32(txt[0]); } catch { }
                bool EsCli = false;
                if (txt[1]=="c") { EsCli = true; }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                //Transporte.Params.Path = HttpContext.Request.Path.Value;
                //Transporte.Params.PathParams = "";
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Twit;
                dato.datoD = Convert.ToDecimal(ID_Emp);
                dato.datoB = EsCli;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientesvincularempresa", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranettareaspendientesvincularempresa", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientesvincularempresa-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/vincularempresaeliminar")]
        public async Task<transportout> TareasPendientesVincularEmpresaEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Twit = 0;
                try { ID_Twit = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Twit")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //Transporte.Params.ID_Idi = ID_IdiPlataforma;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Twit;
                dato.datoD = Convert.ToDecimal(ID_Rel);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("mygaolos/extranettareaspendientesvincularempresaeliminar", Transporte, HttpContext);
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                //var response = await client.PostAsJsonAsync("mygaolos/extranettareaspendientesvincularempresaeliminar", Transporte);
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientesvincularempresaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }



        #endregion

        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/buscar-usuario")]
        public async Task<strDato> TareaPendienteBuscarTrabajador(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string RefNeg = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefNeg");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar.Replace("|", "") + "|" + RefNeg.Replace("|", "");
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendientesusuarioslistado", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranettareaspendientesusuarioslistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientesusuarioslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de cursos abiertos
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/buscar-curso")]
        public async Task<strDato> TareaPendienteBuscarCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendientescursolistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranettareaspendientescursolistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de precios del curso
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/listado-precios-cursos")]
        public async Task<strListaErr> TareasPendientesBuscarCursoObtenerPreciosCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendientesvuscarcursoobtenerprecioscurso", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/modulosolicitudes/extranetsolicitudesobtenerprecioscurso/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranettareaspendientesvuscarcursoobtenerprecioscurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Menu del negocio seleccionado del usuario
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mygaolos/miperfilnegociomenu")]
        public async Task<contenedorMenuLateral> MiPerfilNegocioMenu(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            contenedorMenuLateral ret = new contenedorMenuLateral();

            try
            {
                string RefNeg = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefNeg");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = RefNeg;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranetmiperfilnegociomenu", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranetmiperfilnegociomenu/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<contenedorMenuLateral>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranetmiperfilnegociomenu-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Calendario tareas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/calendario")]
        public async Task<strCalendarioTareas> TareasPendientesCalendario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCalendarioTareas ret = new strCalendarioTareas();

            try
            {

                Int32 año = 0;
                try { año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "año")); } catch { }
                Int32 mes = 0;
                try { mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "mes")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCalendarioTareas dato = new strCalendarioTareas();
                dato.Año = año;
                dato.numMes = mes;
                dato.verFeAnt = false;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendientescalendario", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranettareaspendientescalendario/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCalendarioTareas>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranettareaspendientescalendario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de empresas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("tareas-pendientes/listado-empresas-buscar")]
        public async Task<strDato> TareasPendientesEmpresasListadoBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                //if (Transporte.Params == null) { Transporte.Params = new Param(); }
                //Transporte.Params.NIC = Ses.NIC;
                //Transporte.Params.RefNeg = Ses.RefNeg;
                //Transporte.Params.ID_Ses = Ses.ID_Ses;
                //Transporte.Params.ClaveSesion = Ses.ClaveSesion;
                //string IP = "";
                //try { IP = Transporte.Params.Tracert.Tracert[Transporte.Params.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                //Transporte.Params.IP = IP;
                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranettareaspendientesbuscarempresaslistado", Transporte, HttpContext);
                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranettareaspendientesbuscarempresaslistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranettareaspendientesbuscarempresaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Calendario tareas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("calendario/cargar")]
        public async Task<strCalendario> Calendario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCalendario ret = new strCalendario();

            try
            {

                Int32 año = 0;
                try { año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "año")); } catch { }
                Int32 mes = 0;
                try { mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "mes")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.pagina = año;
                dato.numReg = mes;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranetcalendario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCalendario>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos-extranetcalendario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #endregion







    }
}