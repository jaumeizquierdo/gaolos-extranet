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
using ModuloPresupuestosLibrary;
using DashBoardLibrary;

namespace Gaolos.Controllers
{
    public class ModuloPresupuestosController : Controller
    {

        #region "Modulo Presupuestos - DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos")]
        public async Task<IActionResult> PresupuestosDashboard(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDashBoard lis = new strDashBoard();

            try
            {
                //Int32 ID_Idi = 0;
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosdashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosdashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("PresupuestosDashboard", viewModel);
        }

        #endregion

        #region "Modulo Presupuestos - Presupuestos"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos")]
        public async Task<IActionResult> PresupuestosListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosListado lis = new strPresupuestosPresupuestosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 Seg = 0;
                try { Seg = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Seg")); } catch { }
                Int32 Borrador = 0;
                try { Borrador = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Borrador")); } catch { }

                Int32 vbusPresUsu = 0;
                try { vbusPresUsu = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbusPresUsu")); } catch { }
                string tbusPresUsu = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "tbusPresUsu");

                //string ID_Al2 = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Al2");
                //string Fac = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Fac");
                //string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Fe_In");
                //string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Fe_Fi");
                //string Mes = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Mes");
                //Int32 Año = 0;
                //try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Año")); } catch { }


                strDatoS filtros = new strDatoS();
                //filtros.datoS1 = ID_Al2;
                //filtros.datoS2 = Fac;
                filtros.datoI = vbusPresUsu;
                //filtros.datoS3 = Fe_In;
                //filtros.datoS4 = Fe_Fi;
                //filtros.datoS5 = Mes;
                //filtros.datoD = Convert.ToDecimal(Año);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["Seg"] = Seg;
                ViewData["Borrador"] = Borrador;

                //ViewData["ID_Al2"] = ID_Al2;
                //ViewData["Fac"] = Fac;
                //ViewData["ID_Serie"] = ID_Serie;
                //ViewData["Fe_In"] = Fe_In;
                //ViewData["Fe_Fi"] = Fe_Fi;
                //ViewData["Mes"] = Mes;
                //ViewData["Año"] = Año;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.id = Seg;
                bus.id2 = Borrador;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strPresupuestosPresupuestosListado>(dat.obj.ToString());
                    ViewData["vbusPresUsu"] = lis.ID_Us_Agente;
                    ViewData["tbusPresUsu"] = lis.Agente;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloPresupuestosPresupuestosListadoViewModel(lis);

            return View("Presupuestos", viewModel);
        }

        #endregion


        #region "Ajax - Modulo Usuarios"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/buscar-usuario")]
        public async Task<strListaErr> PresupuestosPresupuestosBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestosbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestosbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Presupuestos - Presupuesto"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto")]
        public async Task<IActionResult> Presupuesto(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosPresupuesto lis = new strPresupuestosPresupuestosPresupuesto();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuesto", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strPresupuestosPresupuestosPresupuesto>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuesto-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloPresupuestosPresupuestosPresupuestoViewModel(lis);

            return View("Presupuesto", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/imprimir-historial-envio")]
        public async Task<IActionResult> PresupuestosPresupuestosPresupuestoHistorialEnviosImprimir(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_Env = 0;
                try { ID_Env = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Env")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                bus.datoD = Convert.ToDecimal(ID_Env);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestohistorialenviosimprimir", Transporte, HttpContext);
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
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestohistorialenviosimprimir-", logInfo = new logInterno { strSql = "", ex = ex } });
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
        [Route("modulo-presupuestos/presupuestos/presupuesto/imprimir")]
        public async Task<IActionResult> PresupuestosPresupuestosPresupuestoImprimir(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                string Tipo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tipo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                bus.datoS = Tipo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoimprimir", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    switch (lis.Tipo)
                    {
                        case "pdf":
                            byte[] arr = lis.Fichero;

                            HttpContext.Response.Headers.Clear();
                            HttpContext.Response.Headers.Add("Content-Length", arr.Length.ToString());

                            return File(arr, lis.TipoMime);
                        default:

                            HttpContext.Response.ContentType = "text/html;charset=utf-8";
                            ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                            return View("html");
                    }
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
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoimprimir-", logInfo = new logInterno { strSql = "", ex = ex } });
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


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/addservicio")]
        public async Task<transportout> PresupuestosPresupuestoAddServicio(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                string Can = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Can");
                string ID_Serv2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serv2");
                string Precio = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precio");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Pres2;
                bus.datoS1 = Can;
                bus.datoS2 = ID_Serv2;
                bus.datoS3 = Precio;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddservicio", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddservicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/add-servicio-libre")]
        public async Task<transportout> PresupuestosPresupuestoAddServicioLibre(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string Codigo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Codigo");
                string txtCan = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "txtCan");
                string txtCoste = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "txtCoste");
                string txtPVP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "txtPVP");
                //string txtDto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dto");
                decimal Dto = 0;
                //try { Dto = Convert.ToDecimal(txtDto); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Pres2;
                bus.datoS1 = Expo;
                bus.datoS2 = Codigo;
                bus.datoS3 = txtCan;
                bus.datoS4 = txtCoste;
                bus.datoS5 = txtPVP;
                bus.datoD = Dto;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddserviciolibre", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddserviciolibre-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/detalle-guardar")]
        public async Task<transportout> Presupuestos_Presupuesto_DetalleGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_NPCD = 0;
                try { ID_NPCD = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_NPCD")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string Codigo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Codigo");
                string txtCan = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Can");
                string txtPVP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PVP");
                string txtCoste = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Coste");
                string txtIVA = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "IVA");
                string txtIRPF = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "IRPF");
                string txtDto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dto");
                string txtDtoE = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "DtoE");
                
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strPresupuestosPresupuestoDetalleAjax bus = new strPresupuestosPresupuestoDetalleAjax();
                bus.ID_Pres2 = ID_Pres2;
                bus.ID_NPCD = ID_NPCD;
                bus.Expo = Expo;
                bus.Codigo = Codigo;
                bus.Can = txtCan;
                bus.PVP = txtPVP;
                bus.Coste = txtCoste;
                bus.IVA = txtIVA;
                bus.IRPF = txtIRPF;
                bus.Dto = txtDto;
                bus.DtoE = txtDtoE;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoguardardetalle", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoguardardetalle-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/detalle-eliminar")]
        public async Task<transportout> PresupuestosPresupuestoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_NPCD = 0;
                try { ID_NPCD = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_NPCD")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                bus.datoD = Convert.ToDecimal(ID_NPCD);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestodetalleeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestodetalleeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/guardar-presupuesto")]
        public async Task<transportout> Presupuestos_Presupuesto_GuardarPresupuesto(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_TipoID = 0;
                try { ID_TipoID = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoID")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 ID_Us_Com = 0;
                try { ID_Us_Com = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Com")); } catch { }
                string Breve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Breve");
                string ExpoPres = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ExpoPres");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                string Att = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Att");
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string ObsPriv = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsPriv");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strPresupuestosPresupuestosPresupuestoEditar bus = new strPresupuestosPresupuestosPresupuestoEditar();
                bus.ID_Pres2 = ID_Pres2;
                bus.ID_TipoID = ID_TipoID;
                bus.ID_Us_Asi = ID_Us_Asi;
                bus.ID_Us_Com = ID_Us_Com;
                bus.Breve = Breve;
                bus.ExpoPres = ExpoPres;
                bus.Obs = Obs;
                bus.Att = Att;
                bus.Mail = Mail;
                bus.Tel = Tel;
                bus.ObsPriv = ObsPriv;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoeditarguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoeditarguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/aceptar")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoAceptar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                string Motivo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Motivo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                bus.datoS= Motivo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoaceptar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoaceptar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/rechazar")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoRechazar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                string Motivo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Motivo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                bus.datoS = Motivo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestorechazar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestorechazar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/enviar")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoEnviar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                string Mails = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mails");
                string Asunto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Asunto");
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strPresupuestosPresupuestoEnviar bus = new strPresupuestosPresupuestoEnviar();
                bus.ID_Pres2 = ID_Pres2;
                bus.Mails = Mails;
                bus.Asunto = Asunto;
                bus.Expo = Expo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoenviar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoenviar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/seguimiento/generar")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoSeguimientoGenerar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string txt_Fe_Prev = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "txt_Fe_Prev");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Pres2;
                bus.datoD = ID_Us_Asi;
                bus.datoS1 = Expo;
                bus.datoS2 = txt_Fe_Prev;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddsolicitud", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddsolicitud-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/cambiar-clientecontacto")]
        public async Task<transportout> PresupuestosPresupuestoCambiarClienteContacto(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cont2 = 0;
                try { ID_Cont2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cont2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Pres2;
                bus.datoS1 = ID_Cli2.ToString();
                bus.datoS2 = ID_Cont2.ToString();
                bus.datoS3 = ID_Dir.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestocambiarclientecontacto", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestocambiarclientecontacto-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/duplicar")]
        public async Task<transportout> PresupuestosPresupuestoDuplicar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cont2 = 0;
                try { ID_Cont2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cont2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Pres2;
                bus.datoS1 = ID_Cli2.ToString();
                bus.datoS2 = ID_Cont2.ToString();
                bus.datoS3 = ID_Dir.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoduplicar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoduplicar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/forma-de-pago-guardar")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoFormaDePagoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_For = 0;
                try { ID_For = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_For")); } catch { }
                Int32 ID_CueNeg = 0;
                try { ID_CueNeg = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CueNeg")); } catch { }
                string ObsForma = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsForma");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Pres2;
                bus.datoD = Convert.ToDecimal(ID_For);
                bus.datoS1 = ID_CueNeg.ToString();
                bus.datoS2 = ObsForma;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoformadepagoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoformadepagoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/quitar-borrador")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoBorradorQuitar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoborradorquitar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoborradorquitar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/marcar-borrador")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoBorradorMarcar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Pres2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoborradormarcar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoborradormarcar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/addarticulo")]
        public async Task<transportout> PresupuestosPresupuestosPresupuestoAddArticulo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                string Can = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Can");
                string ID_Art2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Art2");
                string Precio = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precio");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Pres2;
                bus.datoS1 = Can;
                bus.datoS2 = ID_Art2;
                bus.datoS3 = Precio;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddarticulo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoaddarticulo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/buscarservicio")]
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscarservicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strServiciosServiciosListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscarservicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/buscararticulo")]
        public async Task<strArticulosArticulosListado> PresupuestosPresupuesto_BuscarArticulo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strArticulosArticulosListado ret = new strArticulosArticulosListado();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscararticulo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strArticulosArticulosListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscararticulo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/detalles")]
        public async Task<strPresupuestosPresupuestoAjax> PresupuestosPresupuestosPresupuestoDetallesAjax(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestoAjax ret = new strPresupuestosPresupuestoAjax();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Pres2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestodetallesajax", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestoAjax>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestodetallesajax-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/detalle")]
        public async Task<strPresupuestosPresupuestoDetalleAjax> PresupuestosPresupuestosPresupuestoDetalleAjax(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestoDetalleAjax ret = new strPresupuestosPresupuestoDetalleAjax();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_NPCD = 0;
                try { ID_NPCD = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_NPCD")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Pres2;
                dato.datoD = Convert.ToDecimal(ID_NPCD);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestodetalleajax", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestoDetalleAjax>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestodetalleajax-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/editar-presupuesto")]
        public async Task<strPresupuestosPresupuestosPresupuestoEditar> PresupuestosPresupuestosPresupuestoEditar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosPresupuestoEditar ret = new strPresupuestosPresupuestosPresupuestoEditar();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato(); // ida
                dato.datoI = ID_Pres2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoeditar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestosPresupuestoEditar>(dat.obj.ToString()); // vuelta
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoeditar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/buscar-usuario")]
        public async Task<strListaErr> PresupuestosPresupuestosPresupuestoEditarBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoeditarbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoeditarbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/seguimiento/asignar-usuario")]
        public async Task<strListaErr> PresupuestosPresupuestosPresupuestoSeguimientoAsignarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestosolicitudbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestosolicitudbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/iraenviar")]
        public async Task<strPresupuestosPresupuestoIrAEnviar> PresupuestosPresupuestosPresupuestoIrAEnviar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestoIrAEnviar ret = new strPresupuestosPresupuestoIrAEnviar();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Pres2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoiraenviar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestoIrAEnviar>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoiraenviar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/iraenviarobtenerplantilla")]
        public async Task<strDato> PresupuestosPresupuestosPresupuestoIrAEnviarObtenerPlantilla(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                Int32 ID_TM2 = 0;
                try { ID_TM2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TM2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_TM2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoiraenviarobtenerplantilla", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoiraenviarobtenerplantilla-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/historialenvios")]
        public async Task<strListaDatoSErr> PresupuestosPresupuestosPresupuestoHistorialEnvios(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Pres2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestohistorialenvios", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestohistorialenvios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/buscar-cliente")]
        public async Task<strListaErr> PresupuestosPresupuestosPresupuestoBuscarCliente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/buscar-contacto")]
        public async Task<strListaErr> PresupuestosPresupuestosPresupuestoBuscarContacto(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscarcontacto", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestobuscarcontacto-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/obtener-datos-clicont")]
        public async Task<strPresupuestosPresupuestosDatosClienteContacto> PresupuestosPresupuestosPresupuestoObtenerDatosClienteContacto(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosDatosClienteContacto ret = new strPresupuestosPresupuestosDatosClienteContacto();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cont2 = 0;
                try { ID_Cont2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cont2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Cont2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoobtenerdatosclientecontacto", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestosDatosClienteContacto>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoobtenerdatosclientecontacto-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/forma-de-pago")]
        public async Task<strPresupuestosPresupuestosPresupuestoFormaDePago> PresupuestosPresupuestosPresupuestoFormaDePago(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosPresupuestoFormaDePago ret = new strPresupuestosPresupuestosPresupuestoFormaDePago();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato(); // ida
                dato.datoI = ID_Pres2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestoformadepago", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestosPresupuestoFormaDePago>(dat.obj.ToString()); // vuelta
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestoformadepago-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Presupuestos - Presupuestos Aceptados"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados")]
        public async Task<IActionResult> PresupuestosAceptadosListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosAceptadosListado lis = new strPresupuestosPresupuestosAceptadosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                //string ID_Al2 = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Al2");
                //string Fac = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Fac");
                //Int32 ID_Serie = 0;
                //try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Serie")); } catch { }
                //string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Fe_In");
                //string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Fe_Fi");
                //string Mes = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Mes");
                //Int32 Año = 0;
                //try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Año")); } catch { }


                strDatoS filtros = new strDatoS();
                //filtros.datoS1 = ID_Al2;
                //filtros.datoS2 = Fac;
                //filtros.datoI = ID_Serie;
                //filtros.datoS3 = Fe_In;
                //filtros.datoS4 = Fe_Fi;
                //filtros.datoS5 = Mes;
                //filtros.datoD = Convert.ToDecimal(Año);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                //ViewData["ID_Al2"] = ID_Al2;
                //ViewData["Fac"] = Fac;
                //ViewData["ID_Serie"] = ID_Serie;
                //ViewData["Fe_In"] = Fe_In;
                //ViewData["Fe_Fi"] = Fe_Fi;
                //ViewData["Mes"] = Mes;
                //ViewData["Año"] = Año;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestosaceptadoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strPresupuestosPresupuestosAceptadosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestosaceptadoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloPresupuestosPresupuestosAceptadosListadoViewModel(lis);

            return View("PresupuestosAceptados", viewModel);
        }

        #endregion


        #region "Ajax - Modulo Usuarios"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/validar-contacto")]
        public async Task<transportout> PresupuestosPresupuestoAceptadoValidarContacto(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }
                string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");
                string Emp_Fis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp_Fis");
                string IBAN = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "IBAN");
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                string ObsAdm = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsAdm");
                string NIF = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIF");
                Int32 ID_For = 0;
                try { ID_For = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_For")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strPresupuestosPresupuestoAceptadoValidarContacto bus = new strPresupuestosPresupuestoAceptadoValidarContacto();
                bus.ID_PresAcep = ID_PresAcep;
                bus.Dir = Dir;
                bus.Emp = Emp;
                bus.Emp_Fis = Emp_Fis;
                bus.IBAN = IBAN;
                bus.ID_For = ID_For;
                bus.NIF = NIF;
                bus.Obs = Obs;
                bus.ObsAdm = ObsAdm;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadovalidarcontacto", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadovalidarcontacto-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/generar-asistencia")]
        public async Task<transportout> PresupuestosPresupuestoAceptadoAccionAsistenciaGenerar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }
                string RefCli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefCli");
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                bool Urg = false;
                try { Urg = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Urg")); } catch { }
                bool CliParado = false;
                try { CliParado = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CliParado")); } catch { }
                string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                string Horario = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Horario");
                bool GuaHor = false;
                try { GuaHor = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "GuaHor")); } catch { }
                string Cont = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cont");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strPresupuestosPresupuestoAceptadoAccionAsistenciaGenerar bus = new strPresupuestosPresupuestoAceptadoAccionAsistenciaGenerar();
                bus.ID_PresAcep = ID_PresAcep;
                bus.RefCli = RefCli;
                bus.ID_Tipo = ID_Tipo;
                bus.Expo = Expo;
                bus.Urg = Urg;
                bus.CliParado = CliParado;
                bus.Emp = Emp;
                bus.ID_Dir = ID_Dir;
                bus.Horario = Horario;
                bus.GuaHor = GuaHor;
                bus.Cont = Cont;
                bus.Tel = Tel;
                bus.Obs = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionasistenciagenerar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionasistenciagenerar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/generar-mantenimiento")]
        public async Task<transportout> PresupuestosPresupuestoAceptadoAccionMantenimientoGenerar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }
                string RefCli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RefCli");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                Int32 PeriVis = 0;
                try { PeriVis = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PeriVis")); } catch { }
                Int32 PeriFac = 0;
                try { PeriFac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PeriFac")); } catch { }
                string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                string Horario = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Horario");
                string Cont = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cont");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string ObsUbi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsUbi");
                string ObsPriv = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsPriv");
                string ObsPub = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsPub");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strPresupuestosPresupuestoAceptadoAccionMantenimientoGenerar bus = new strPresupuestosPresupuestoAceptadoAccionMantenimientoGenerar();
                bus.ID_PresAcep = ID_PresAcep;
                bus.RefCli = RefCli;
                bus.Fe_In = Fe_In;
                bus.PeriVis = PeriVis;
                bus.PeriFac = PeriFac;
                bus.Emp = Emp;
                bus.ID_Dir = ID_Dir;
                bus.Horario = Horario;
                bus.Cont = Cont;
                bus.Tel = Tel;
                bus.ObsUbi = ObsUbi;
                bus.ObsPriv = ObsPriv;
                bus.ObsPub = ObsPub;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionmantenimientogenerar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionmantenimientogenerar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/marcar-como-revisado")]
        public async Task<transportout> PresupuestosPresupuestoAceptadoAccionMarcarComoRevisadoGenerar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_PresAcep;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionmarcarcomorevisadogenerar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionmarcarcomorevisadogenerar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/pasar-a-albaran")]
        public async Task<transportout> PresupuestosPresupuestoAceptadoAccionPasarAAlbaranGenerar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_PresAcep;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionpasaraalbarangenerar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionpasaraalbarangenerar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/anular-aceptacion")]
        public async Task<transportout> PresupuestosPresupuestoAceptadoAnularAceptacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_PresAcep;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadoanularaceptacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadoanularaceptacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/estado")]
        public async Task<strPresupuestosPresupuestoAceptadoEstado> PresupuestosPresupuestosAceptadosEstado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestoAceptadoEstado ret = new strPresupuestosPresupuestoAceptadoEstado();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_PresAcep;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestosaceptadosestado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestoAceptadoEstado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestosaceptadosestado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/accion-asistencia")]
        public async Task<strPresupuestosPresupuestoAceptadoAccionAsistencia> PresupuestosPresupuestoAceptadoAccionAsistencia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestoAceptadoAccionAsistencia ret = new strPresupuestosPresupuestoAceptadoAccionAsistencia();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_PresAcep;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionasistencia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestoAceptadoAccionAsistencia>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos-aceptados/accion-mantenimiento")]
        public async Task<strPresupuestosPresupuestoAceptadoAccionMantenimiento> PresupuestosPresupuestoAceptadoAccionMantenimiento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestoAceptadoAccionMantenimiento ret = new strPresupuestosPresupuestoAceptadoAccionMantenimiento();

            try
            {
                Int32 ID_PresAcep = 0;
                try { ID_PresAcep = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PresAcep")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_PresAcep;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionmantenimiento", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestoAceptadoAccionMantenimiento>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoaceptadoaccionmantenimiento-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion



        #endregion


        #endregion

        #region "Modulo Presupuestos - Consultas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/consultas")]
        public async Task<IActionResult> PresupuestosConsultas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDashBoard lis = new strDashBoard();

            try
            {
                //Int32 ID_Idi = 0;
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosconsultas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosconsultas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("PresupuestosConsultas", viewModel);
        }

        #endregion

        #region "Modulo Presupuestos - Consultas - Presupuestos aceptados por usuario"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/consultas/presupuestos-aceptados-por-usuario")]
        public async Task<IActionResult> PresupuestosConsultasPresupuestosAceptadosPorUsuario(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosCPAPUListado lis = new strPresupuestosPresupuestosCPAPUListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Us = 0;
                try { ID_Us = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vmodPresUsuAsis")); } catch { }

                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                strDatoS filtros = new strDatoS();
                filtros.datoS1 = Fe_In;
                filtros.datoS2 = Fe_Fi;
                filtros.datoI = ID_Us;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosconsultaspresupuestosaceptadosporusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strPresupuestosPresupuestosCPAPUListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosconsultaspresupuestosaceptadosporusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloPresupuestosPresupuestosCPAPUListadoViewModel(lis);

            return View("PresupuestosConsultas-AceptadosPorUsuario", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/consultas/presupuestos-aceptados-por-usuario/buscar-usuario")]
        public async Task<strListaErr> PresupuestosConsultasPresupuestosAceptadosPorUsuarioBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosconsultaspresupuestosaceptadosporusuariobuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosconsultaspresupuestosaceptadosporusuariobuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Presupuestos - Consultas - Presupuestos aceptados con alta de cliente"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-presupuestos/consultas/presupuestos-aceptados-con-alta-de-cliente")]
        public async Task<IActionResult> PresupuestosConsultasPresupuestosAceptadosConAltaDeCliente(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strPresupuestosPresupuestosAceptadosConAltaDeClienteListado lis = new strPresupuestosPresupuestosAceptadosConAltaDeClienteListado();

            try
            {
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                //if (Ses == null) { return Redirect(Class.Globales.UrlSesEnd(HttpContext)); }
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosconsultaspresupuestosaceptadosconaltadecliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strPresupuestosPresupuestosAceptadosConAltaDeClienteListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosconsultaspresupuestosaceptadosconaltadecliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloPresupuestosPresupuestosAceptadosConAltaDeClienteListadoViewModel(lis);

            return View("PresupuestosConsultas-AceptadosConAltaCliente", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"


        #endregion


        #endregion


        #endregion


        #region "Modulo Presupuestos - Nuevo"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/nuevo")]
        public async Task<IActionResult> PresupuestosNuevo(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDashBoard lis = new strDashBoard();

            try
            {
                //Int32 ID_Idi = 0;
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosnuevo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosnuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("PresupuestosNuevo", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/presupuestos/presupuesto/nuevo-presupuesto")]
        public async Task<transportout> Presupuestos_Presupuesto_NuevoPresupuesto(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cont2 = 0;
                try { ID_Cont2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cont2")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                string Breve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Breve");
                string Att = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Att");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string ObsPriv = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsPriv");
                string txtFe_Prev = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Prev");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strPresupuestosPresupuestoNuevo bus = new strPresupuestosPresupuestoNuevo();
                bus.ID_Cli2 = ID_Cli2;
                bus.ID_Cont2 = ID_Cont2;
                bus.ID_Us_Asi = ID_Us_Asi;
                bus.Breve = Breve;
                bus.Att = Att;
                bus.Tel = Tel;
                bus.Mail = Mail;
                bus.ObsPriv = ObsPriv;
                bus.Fe_Prev = "";
                bus.ID_Dir = ID_Dir;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulopresupuestos/extranetpresupuestospresupuestospresupuestonuevogenerar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestospresupuestonuevogenerar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/nuevo/buscar-cliente")]
        public async Task<strListaErr> PresupuestosNuevoBuscarCliente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosnuevobuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosnuevobuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/nuevo/buscar-contacto")]
        public async Task<strListaErr> PresupuestosNuevoBuscarContacto(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosnuevobuscarcontacto", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosnuevobuscarcontacto-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/nuevo/buscar-usuario")]
        public async Task<strListaErr> PresupuestosNuevoBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosnuevobuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosnuevobuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/nuevo/obtener-datos-clicont")]
        public async Task<strPresupuestosPresupuestosDatosClienteContacto> PresupuestosNuevoObtenerDatosClienteContacto(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosDatosClienteContacto ret = new strPresupuestosPresupuestosDatosClienteContacto();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cont2 = 0;
                try { ID_Cont2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cont2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Cont2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestosnuevoobtenerdatosclientecontacto", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strPresupuestosPresupuestosDatosClienteContacto>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestosnuevoobtenerdatosclientecontacto-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Presupuestos - Borradores"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/borradores")]
        public async Task<IActionResult> PresupuestosPresupuestosBorradoresListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosListado lis = new strPresupuestosPresupuestosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                strDatoS filtros = new strDatoS();

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestosborradoreslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strPresupuestosPresupuestosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestosborradoreslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloPresupuestosPresupuestosListadoViewModel(lis);

            return View("PresupuestosBorrador", viewModel);
        }

        #endregion


        #region "Ajax - Modulo Usuarios"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Presupuestos - Historial"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/historial")]
        public async Task<IActionResult> PresupuestosPresupuestosHistorialListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strPresupuestosPresupuestosListado lis = new strPresupuestosPresupuestosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 vbusHisPresUsu = 0;
                try { vbusHisPresUsu = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbusHisPresUsu")); } catch { }
                string tbusHisPresUsu = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "tbusHisPresUsu");

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                strDatoS filtros = new strDatoS();
                filtros.datoI = vbusHisPresUsu;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                //if (Ses == null) { return Redirect(Class.Globales.UrlSesEnd(HttpContext)); }
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoshistoriallistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strPresupuestosPresupuestosListado>(dat.obj.ToString());
                    ViewData["vbusHisPresUsu"] = lis.ID_Us_Agente;
                    ViewData["tbusHisPresUsu"] = lis.Agente;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoshistoriallistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloPresupuestosPresupuestosListadoViewModel(lis);

            return View("PresupuestosHistorial", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-presupuestos/historial/buscar-usuario")]
        public async Task<strListaErr> PresupuestosPresupuestosHistorialBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulopresupuestos/extranetpresupuestospresupuestoshistorialbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulopresupuestos/extranetpresupuestospresupuestoshistorialbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

    }

}