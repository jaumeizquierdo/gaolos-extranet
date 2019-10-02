
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using ModuloFacturacionLibrary;
using DashBoardLibrary;

namespace Gaolos.Controllers
{
    public class ModuloFacturacionController : Controller
    {

        #region "Modulo Facturacion - Facturas - Vencimientos factura"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturas/vencimientos-factura")]
        public async Task<IActionResult> FacturacionVencimientosFactura(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionVencimientosFactura lis = new strFacturacionVencimientosFactura();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                //string go = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "go");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Fac"] = ID_Fac;

                strDato bus = new strDato();
                bus.datoI = ID_Fac;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionvencimientosfactura", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionVencimientosFactura>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionvencimientosfactura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionVencimientosFacturaViewModel(lis);

            return View("Factura-Vencimientos", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturas/vencimientos-factura/vencimientos")]
        public async Task<strFacturacionVencimientosFacturaVencimientos> FacturacionVencimientosFacturaVencimientos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionVencimientosFacturaVencimientos ret = new strFacturacionVencimientosFacturaVencimientos();

            try
            {
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                //string Fe_Fa = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fa");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Fac;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionvencimientosfacturavencimientos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionVencimientosFacturaVencimientos>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionvencimientosfacturavencimientos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion")]
        public async Task<IActionResult> FacturacionDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturaciondashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturaciondashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("FacturacionDashboard", viewModel);
        }

        #endregion

        #region "Pupurri"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/series-de-facturacion")]
        public async Task<IActionResult> FacturacionConfiguracionSeriesFacturacion(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionConfiguracionSeriesDeFacturacion lis = new strFacturacionConfiguracionSeriesDeFacturacion();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguracionseriesdefacturacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionConfiguracionSeriesDeFacturacion>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguracionseriesdefacturacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionConfiguracionSeriesDeFacturacionViewModel(lis);

            return View("Configuracion-SeriesFacturacion", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/series-de-facturacion/serie")]
        public async Task<strFacturacionConfiguracionSerieDeFacturacion> FacturacionConfiguracionSeriesFacturacionSerie(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionConfiguracionSerieDeFacturacion ret = new strFacturacionConfiguracionSerieDeFacturacion();

            try
            {
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Serie;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguracionseriesdefacturacionserie", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionConfiguracionSerieDeFacturacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguracionseriesdefacturacionserie-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/series-de-facturacion/serie/guardar")]
        public async Task<strDato> FacturacionConfiguracionSeriesDeFacturacionSerieGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }
                Int32 Prio = 0;
                try { Prio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prio")); } catch { }
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                bool Vis = false;
                try { Vis = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Vis")); } catch { }
                bool NoMail = false;
                try { NoMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NoMail")); } catch { }

                string Serie = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Serie");
                string Num = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Num");
                string Fe_Ul = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ul");
                string EsEdit = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EsEdit");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionSerieDeFacturacion dato = new strFacturacionConfiguracionSerieDeFacturacion();
                dato.ID_Serie = ID_Serie;
                dato.Prio = Prio;
                dato.Obs = Obs;
                dato.Visible = Vis;
                dato.NoMail = NoMail;
                dato.Serie = Serie;
                dato.Num = Num;
                dato.Fe_Ul = Fe_Ul;
                dato.EsEdit = EsEdit;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionseriesdefacturacionserieguardar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionseriesdefacturacionserieguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/series-de-facturacion/serie-nueva/guardar")]
        public async Task<strDato> FacturacionConfiguracionSeriesDeFacturacionSerieNuevaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Serie = 0;
                Int32 Prio = 0;
                try { Prio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prio")); } catch { }
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                bool Vis = false;
                try { Vis = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Vis")); } catch { }
                bool NoMail = false;
                try { NoMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NoMail")); } catch { }

                string Serie = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Serie");
                string Num = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Num");
                string Fe_Ul = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ul");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionSerieDeFacturacion dato = new strFacturacionConfiguracionSerieDeFacturacion();
                dato.ID_Serie = ID_Serie;
                dato.Prio = Prio;
                dato.Obs = Obs;
                dato.Visible = Vis;
                dato.NoMail = NoMail;
                dato.Serie = Serie;
                dato.Num = Num;
                dato.Fe_Ul = Fe_Ul;
                dato.EsEdit = "";
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionseriesdefacturacionserienuevaguardar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionseriesdefacturacionserienuevaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/series-de-facturacion/serie/eliminar")]
        public async Task<strDato> FacturacionConfiguracionSeriesDeFacturacionSerieEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionSerieDeFacturacion dato = new strFacturacionConfiguracionSerieDeFacturacion();
                dato.ID_Serie = ID_Serie;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionseriesdefacturacionserieeliminar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionseriesdefacturacionserieeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion

        #region "Modulo Facturación - Comisiones"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/comisiones")]
        public async Task<IActionResult> FacturacionComisionesListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionComisionesListado lis = new strFacturacionComisionesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbuscarcli")); } catch { }
                Int32 ID_Us = 0;
                try { ID_Us = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbuscarusu")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");


                strDatoS filtros = new strDatoS();
                filtros.datoI = ID_Cli2;
                filtros.datoD = Convert.ToDecimal(ID_Us);
                filtros.datoS1 = Fe_In;
                filtros.datoS2 = Fe_Fi;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["vbuscarcli"] = ID_Cli2;
                ViewData["vbuscarusu"] = ID_Us;
                ViewData["Fe_In"] = Fe_In;
                ViewData["Fe_Fi"] = Fe_Fi;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncomisioneslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionComisionesListado>(dat.obj.ToString());
                    ViewData["buscarcli"] = lis.Cli;
                    ViewData["buscarusu"] = lis.Usu;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacioncomisioneslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionComisionesListadoViewModel(lis);

            return View("Comisiones", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/comisiones/buscarcliente")]
        public async Task<strListaErr> FacturacionComisionesBuscarCliente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncomisionesbuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacioncomisionesbuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/comisiones/buscarusuario")]
        public async Task<strListaErr> FacturacionComisionesBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncomisionesbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacioncomisionesbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/comisiones/exportar")]
        public async Task<IActionResult> FacturacionComisionesListadoExportar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFichero ret = new strFacturacionInformacionFichero();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbuscarcli")); } catch { }
                Int32 ID_Us = 0;
                try { ID_Us = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbuscarusu")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");


                strDatoS filtros = new strDatoS();
                filtros.datoI = ID_Cli2;
                filtros.datoD = Convert.ToDecimal(ID_Us);
                filtros.datoS1 = Fe_In;
                filtros.datoS2 = Fe_Fi;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncomisioneslistadoexportar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
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

                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(ret.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioncomisioneslistadoexportar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            if (ret.Err.eserror) { return null; }

            Response.Headers.Add("Content-Disposition", "attachment; filename=" + ret.Nombre);

            return File(ret.Fichero, ret.TipoMime);

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Facturación - Facturación"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturas")]
        public async Task<IActionResult> FacturacionFacturasListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionFacturasListado lis = new strFacturacionFacturasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string ID_Al2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Al2");
                string Fac = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string Mes = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes");
                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }


                strDatoS filtros = new strDatoS();
                filtros.datoS1 = ID_Al2;
                filtros.datoS2 = Fac;
                filtros.datoI = ID_Serie;
                filtros.datoS3 = Fe_In;
                filtros.datoS4 = Fe_Fi;
                filtros.datoS5 = Mes;
                filtros.datoD = Convert.ToDecimal(Año);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_Al2"] = ID_Al2;
                ViewData["Fac"] = Fac;
                ViewData["ID_Serie"] = ID_Serie;
                ViewData["Fe_In"] = Fe_In;
                ViewData["Fe_Fi"] = Fe_Fi;
                ViewData["Mes"] = Mes;
                ViewData["Año"] = Año;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionfacturaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionFacturasListado>(dat.obj.ToString());

                    ViewData["Mes"] = lis.Mes;
                    ViewData["Año"] = lis.Año;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionfacturaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionFacturasListadoViewModel(lis);

            return View("Facturas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/informacion-factura/descargar")]
        public async Task<IActionResult> FacturacionInformacionFacturaDescargar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFichero ret = new strFacturacionInformacionFichero();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                string Tipo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tipo");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_Fac,
                    datoS = Tipo
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioninformacionfacturafichero", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    switch (ret.Tipo)
                    {
                        case "pdf2":
                        case "pdf":
                            byte[] arr = ret.Fichero;

                            HttpContext.Response.Headers.Clear();
                            HttpContext.Response.Headers.Add("Content-Length", arr.Length.ToString());

                            return File(arr, ret.TipoMime);
                        default:

                            HttpContext.Response.ContentType = "text/html;charset=utf-8";
                            ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(ret.Fichero);

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

                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(ret.Fichero);

                    return View("html");

                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioninformacionfacturafichero-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return null;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/informacion-factura/recibo/descargar")]
        public async Task<IActionResult> FacturacionInformacionFacturaReciboFichero(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFichero ret = new strFacturacionInformacionFichero();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }
                string Tipo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tipo");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_Fac,
                    datoD = Convert.ToDecimal(ID_Fra),
                    datoS = Tipo
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioninformacionfacturarecibofichero", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    switch (ret.Tipo)
                    {
                        case "pdf2":
                        case "pdf":
                            byte[] arr = ret.Fichero;

                            HttpContext.Response.Headers.Clear();
                            HttpContext.Response.Headers.Add("Content-Length", arr.Length.ToString());

                            return File(arr, ret.TipoMime);
                        default:

                            HttpContext.Response.ContentType = "text/html;charset=utf-8";
                            ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(ret.Fichero);

                            return View("html");
                    }
                } else
                {
                    strDatoS err500 = new strDatoS
                    {
                        datoS1 = dat.err.titulo,
                        datoS2 = dat.err.mensaje
                    };
                    Transporte.obj = err500;

                    dat = await Infrastructure.ReturnResults.GetResponseAsync("error/extraneterror500", Transporte, HttpContext);

                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(ret.Fichero);

                    return View("html");

                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioninformacionfacturarecibofichero-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return null;

        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacionnuevasolicitud")]
        public async Task<strDato> FacturacionSolicitudNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strSolicitudNueva dato = new strSolicitudNueva();
                dato.ID_Cli2 = ID_Fac;
                dato.ID_Cont2 = 0;
                dato.Expo = Expo;
                dato.ID_Us_Asi = ID_Us_Asi;
                dato.Fe_Prev_Ini = Fe_In;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionnuevasolicitud", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionnuevasolicitud-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacionfacturaeliminar")]
        public async Task<strDato> FacturacionFacturaEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                string ObsEli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsEli");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Fac;
                dato.datoS = ObsEli;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionfacturaeliminar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionfacturaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacionfacturacambiarobservaciones")]
        public async Task<strDato> FacturacionFacturaCambiarObservaciones(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Fac;
                dato.datoS = Obs;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionfacturacambiarobservaciones", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionfacturacambiarobservaciones-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacionfacturaenviarpormail")]
        public async Task<strDato> FacturacionFacturaEnviarPorMail(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac");
                Int32 ID_Fac = 0;
                bool EnMi = false;
                string[] val = txt.Split("_");
                try { ID_Fac = Convert.ToInt32(val[1]); } catch { }
                try
                {
                    if (val[2].ToLower() == "si") EnMi = true;
                } catch { }
                string MailAsunto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "MailAsunto");
                string MailTexto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "MailTexto");
                if (MailAsunto==null) { MailAsunto = ""; }
                if (MailTexto == null) { MailTexto = ""; }

                if (MailTexto != "") EnMi = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Fac;
                dato.datoB = EnMi;
                dato.datoS1 = MailAsunto;
                dato.datoS2 = MailTexto;
                Transporte.obj = dato;

                //var response = await client.PostAsJsonAsync("modulofacturacion/extranetfacturacionfacturaenviarpormail", Transporte);
                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionfacturaenviarpormail", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionfacturaenviarpormail-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/solicitud-buscar-usuario")]
        public async Task<strDato> FacturacionSolicitudBuscarTrabajador(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionfacturassolicitudbuscartrabajador", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionfacturassolicitudbuscartrabajador-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Obtener datos de la factura
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/informacion-factura")]
        public async Task<strFacturacionInformacionFactura> FacturacionInformacionFactura(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFactura ret = new strFacturacionInformacionFactura();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_Fac
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioninformacionfactura", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFactura>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioninformacionfactura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Obtener datos de la factura
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/obtener-plantilla-mail")]
        public async Task<strDato> FacturacionPlantillaMail(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strDato ret = new strDato();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_TM2 = 0;
                try { ID_TM2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TM2")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_TM2
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionplantillamail", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionplantillamail-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion

        #endregion

        #region "Modulo Facturación - Abonos"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/abonos")]
        public async Task<IActionResult> FacturacionAbonosListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionFacturasListado lis = new strFacturacionFacturasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string ID_Al2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Al2");
                string Fac = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string Mes = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes");
                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }


                strDatoS filtros = new strDatoS();
                filtros.datoS1 = ID_Al2;
                filtros.datoS2 = Fac;
                filtros.datoI = ID_Serie;
                filtros.datoS3 = Fe_In;
                filtros.datoS4 = Fe_Fi;
                filtros.datoS5 = Mes;
                filtros.datoD = Convert.ToDecimal(Año);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["ID_Al2"] = ID_Al2;
                ViewData["Fac"] = Fac;
                ViewData["ID_Serie"] = ID_Serie;
                ViewData["Fe_In"] = Fe_In;
                ViewData["Fe_Fi"] = Fe_Fi;
                ViewData["Mes"] = Mes;
                ViewData["Año"] = Año;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionabonoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionFacturasListado>(dat.obj.ToString());

                    ViewData["Mes"] = lis.Mes;
                    ViewData["Año"] = lis.Año;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionabonoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionFacturasListadoViewModel(lis);

            return View("Abonos", viewModel);
        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"


        #endregion


        #endregion

        #endregion

        #region "Modulo Facturación - Cobros"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/cobros")]
        public async Task<IActionResult> Cobros(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionCobrosListado lis = new strFacturacionCobrosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }
                Int32 Mes = 0;
                try { Mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes")); } catch { }
                string Fac = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");
                string Fe_Fa_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fa_In");
                string Fe_Fa_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fa_Fi");

                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }
                Int32 ID_For = 0;
                try { ID_For = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_For")); } catch { }
                Int32 ID_CueNeg = 0;
                try { ID_CueNeg = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CueNeg")); } catch { }
                string Fe_Ve_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ve_In");
                string Fe_Ve_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ve_Fi");
                string Fe_Cob_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Cob_In");
                string Fe_Cob_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Cob_Fi");
                string Imp_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp_In");
                string Imp_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp_Fi");


                strFacturacionBuscar filtros = new strFacturacionBuscar();
                filtros.Año = Año;
                filtros.Fac = Fac;
                filtros.Fe_Cob_Fi = Fe_Cob_Fi;
                filtros.Fe_Cob_In = Fe_Cob_In;
                filtros.Fe_Fa_Fi = Fe_Fa_Fi;
                filtros.Fe_Fa_In = Fe_Fa_In;
                filtros.Fe_Ve_Fi = Fe_Ve_Fi;
                filtros.Fe_Ve_In = Fe_Ve_In;
                filtros.ID_For = ID_For;
                filtros.ID_Tipo = ID_Tipo;
                filtros.Imp_Fi = Imp_Fi;
                filtros.Imp_In = Imp_In;
                filtros.Mes = Mes;
                filtros.ID_CueNeg = ID_CueNeg;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["Año"] = Año;
                ViewData["Fac"] = Fac;
                ViewData["Fe_Cob_Fi"] = Fe_Cob_Fi;
                ViewData["Fe_Cob_In"] = Fe_Cob_In;

                ViewData["Fe_Fa_Fi"] = Fe_Fa_Fi;
                ViewData["Fe_Fa_In"] = Fe_Fa_In;
                ViewData["Fe_Ve_Fi"] = Fe_Ve_Fi;
                ViewData["Fe_Ve_In"] = Fe_Ve_In;
                ViewData["ID_For"] = ID_For;
                ViewData["ID_Tipo"] = ID_Tipo;
                ViewData["ID_CueNeg"] = ID_CueNeg;

                ViewData["Imp_Fi"] = Imp_Fi;
                ViewData["Imp_In"] = Imp_In;
                ViewData["Mes"] = Mes;
                ViewData["buscar"] = buscar;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncobroslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionCobrosListado>(dat.obj.ToString());

                    ViewData["ID_Tipo"] = lis.ID_Tipo;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacioncobroslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionCobrosListadoViewModel(lis);

            //return View("CobrosEnEspera", viewModel);
            return View("Cobros", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/cobrosenespera")]
        public async Task<IActionResult> CobrosEnEspera(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionCobrosListado lis = new strFacturacionCobrosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }
                Int32 Mes = 0;
                try { Mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes")); } catch { }
                string Fac = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");
                string Fe_Fa_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fa_In");
                string Fe_Fa_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fa_Fi");

                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }
                Int32 ID_For = 0;
                try { ID_For = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_For")); } catch { }
                Int32 ID_CueNeg = 0;
                try { ID_CueNeg = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CueNeg")); } catch { }
                string Fe_Ve_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ve_In");
                string Fe_Ve_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ve_Fi");
                string Fe_Cob_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Cob_In");
                string Fe_Cob_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Cob_Fi");
                string Imp_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp_In");
                string Imp_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp_Fi");


                strFacturacionBuscar filtros = new strFacturacionBuscar();
                filtros.Año = Año;
                filtros.Fac = Fac;
                filtros.Fe_Cob_Fi = Fe_Cob_Fi;
                filtros.Fe_Cob_In = Fe_Cob_In;
                filtros.Fe_Fa_Fi = Fe_Fa_Fi;
                filtros.Fe_Fa_In = Fe_Fa_In;
                filtros.Fe_Ve_Fi = Fe_Ve_Fi;
                filtros.Fe_Ve_In = Fe_Ve_In;
                filtros.ID_For = ID_For;
                filtros.ID_Tipo = ID_Tipo;
                filtros.Imp_Fi = Imp_Fi;
                filtros.Imp_In = Imp_In;
                filtros.Mes = Mes;
                filtros.ID_CueNeg = ID_CueNeg;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["Año"] = Año;
                ViewData["Fac"] = Fac;
                ViewData["Fe_Cob_Fi"] = Fe_Cob_Fi;
                ViewData["Fe_Cob_In"] = Fe_Cob_In;

                ViewData["Fe_Fa_Fi"] = Fe_Fa_Fi;
                ViewData["Fe_Fa_In"] = Fe_Fa_In;
                ViewData["Fe_Ve_Fi"] = Fe_Ve_Fi;
                ViewData["Fe_Ve_In"] = Fe_Ve_In;
                ViewData["ID_For"] = ID_For;
                ViewData["ID_Tipo"] = ID_Tipo;
                ViewData["ID_CueNeg"] = ID_CueNeg;

                ViewData["Imp_Fi"] = Imp_Fi;
                ViewData["Imp_In"] = Imp_In;
                ViewData["Mes"] = Mes;
                ViewData["buscar"] = buscar;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncobroslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionCobrosListado>(dat.obj.ToString());

                    ViewData["ID_Tipo"] = lis.ID_Tipo;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacioncobroslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionCobrosListadoViewModel(lis);

            return View("CobrosEnEspera", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/cobros/recibo/descargar")]
        public async Task<IActionResult> FacturacionCobrosReciboFichero(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFichero ret = new strFacturacionInformacionFichero();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }
                string Tipo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tipo");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_Fac,
                    datoD = Convert.ToDecimal(ID_Fra),
                    datoS = Tipo
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncobrosrecibofichero", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    switch (ret.Tipo)
                    {
                        case "pdf2":
                        case "pdf":
                            byte[] arr = ret.Fichero;

                            HttpContext.Response.Headers.Clear();
                            HttpContext.Response.Headers.Add("Content-Length", arr.Length.ToString());

                            return File(arr, ret.TipoMime);
                        default:

                            HttpContext.Response.ContentType = "text/html;charset=utf-8";
                            ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(ret.Fichero);

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

                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(ret.Fichero);

                    return View("html");

                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioncobrosrecibofichero-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return null;

        }


        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacionreciboanularcobro")]
        public async Task<strDato> FacturacionRecbioAnularCobro(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                string ObsEli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsEli");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Fra;
                dato.datoD = Convert.ToDecimal(ID_Fac);
                dato.datoS = ObsEli;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionreciboanularcobro", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionreciboanularcobro-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacionrecibomarcarincobrable")]
        public async Task<strDato> FacturacionCobroMarcarIncobrable(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                string ObsInco = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsInco");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Fra;
                dato.datoD = Convert.ToDecimal(ID_Fac);
                dato.datoS = ObsInco;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacioncobromarcarincobrable", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioncobromarcarincobrable-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacionrecibomarcarcomocobrados")]
        public async Task<strDato> FacturacionCobroMarcarComoCobrado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_For = 0;
                try { ID_For = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_For")); } catch { }
                Int32 ID_CueNeg = 0;
                try { ID_CueNeg = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CueNeg")); } catch { }
                string Fe_Cob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Cob");
                string sel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "sel");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_For;
                dato.datoD = Convert.ToDecimal(ID_CueNeg);
                dato.datoS1 = Fe_Cob;
                dato.datoS2 = sel;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacioncobromarcarcomocobrado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioncobromarcarcomocobrado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/facturacioncobrodevolucioncobro")]
        public async Task<strDato> FacturacionCobroDevolucionCobro(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string Fe_Dev = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Dev");
                string Gastos = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Gastos");
                string ObsDev = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsDev");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Fra;
                dato.datoD = Convert.ToDecimal(ID_Fac);
                dato.datoS1 = Fe_Dev;
                dato.datoS2 = Gastos;
                dato.datoS3 = ID_Us_Asi.ToString();
                dato.datoS4 = ObsDev;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacioncobrodevolucioncobro", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioncobrodevolucioncobro-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion

        #region "Recuperar datos - Get"

        // Obtener datos de la factura
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/informacion-cobro")]
        public async Task<strFacturacionInformacionCobro> FacturacionInformacionCobro(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionCobro ret = new strFacturacionInformacionCobro();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }
                Int32 ID_Fac = 0;
                try { ID_Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fac")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_Fra,
                    datoD=Convert.ToDecimal(ID_Fac)
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioninformacioncobro", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionInformacionCobro>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioninformacioncobro-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #endregion


        #endregion

        #region "Modulo Facturacion - Cobros a negociar"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/cobros-a-negociar")]
        public async Task<IActionResult> FacturacionCobrosANegociarListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionCobrosANegociarListado lis = new strFacturacionCobrosANegociarListado();

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
                bus.numReg = 20;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacioncobrosanegociarlistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionCobrosANegociarListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacioncobrosanegociarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionCobrosANegociarListadoViewModel(lis);

            return View("CobrosANegociar", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/cobros-a-negociar/revisar")]
        public async Task<strDato> FacturacionCobrosANegociarSolicitudGenerar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_PCEE = 0;
                try { ID_PCEE = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PCEE")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_PCEE;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacioncobrosanegociarsolicitudgenerar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacioncobrosanegociarsolicitudgenerar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion


        #region "Modulo Facturación - Recibos"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos")]
        public async Task<IActionResult> FacturacionRecibosInicio(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionRecibosInicio lis = new strFacturacionRecibosInicio();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionrecibosinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionRecibosInicio>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionrecibosinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionRecibosInicioViewModel(lis);

            return View("Recibos", viewModel);
        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/add-recibos")]
        public async Task<strDato> FacturacionRecibosRecibosDisponiblesAdd(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Fra = 0;
                try { ID_Fra = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Fra")); } catch { }
                string Fe_Ve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ve");
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                Fe_Ve = Fe_Ve.Trim();
                if (Fe_Ve.Length != 6) Fe_Ve = "";
                if (Fe_Ve.Length == 6) Fe_Ve = Funciones.Funciones.Left(Fe_Ve, 2) + "/" + Funciones.Funciones.Right(Funciones.Funciones.Left(Fe_Ve, 4), 2) + "/" + Funciones.Funciones.Right(Fe_Ve, 2);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Cli2;
                dato.datoS1 = buscar;
                dato.datoS2 = Fe_Ve;
                dato.datoD = Convert.ToDecimal(ID_Fra);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosdisponiblesadd", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionrecibosrecibosdisponiblesadd-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/del-recibos")]
        public async Task<strDato> FacturacionRecibosRecibosSeleccionadosDel(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_RRR = 0;
                try { ID_RRR = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_RRR")); } catch { }
                string Fe_Ve = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Ve");
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                Fe_Ve = Fe_Ve.Trim();
                if (Fe_Ve.Length != 6) Fe_Ve = "";
                if (Fe_Ve.Length == 6) Fe_Ve = Funciones.Funciones.Left(Fe_Ve, 2) + "/" + Funciones.Funciones.Right(Funciones.Funciones.Left(Fe_Ve, 4), 2) + "/" + Funciones.Funciones.Right(Fe_Ve, 2);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Cli2;
                dato.datoS1 = buscar;
                dato.datoS2 = Fe_Ve;
                dato.datoD = Convert.ToDecimal(ID_RRR);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosseleccionadosdel", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionrecibosrecibosseleccionadosdel-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/generar-remesa")]
        public async Task<strDato> FacturacionRecibosRecibosGenerarRemesa(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }
                Int32 ID_Norma = 0;
                try { ID_Norma = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Norma")); } catch { }
                string Fe_Pres = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Pres");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Cue;
                dato.datoD = Convert.ToDecimal(ID_Tipo);
                dato.datoS1 = ID_Norma.ToString();
                dato.datoS2 = Fe_Pres;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosgenerarremesa", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionrecibosrecibosgenerarremesa-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/quitar-recibos")]
        public async Task<strDato> FacturacionRecibosRecibosQuitarRecibos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosquitarrecibos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionrecibosrecibosquitarrecibos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/eliminar-remesa")]
        public async Task<strDato> FacturacionRecibosRecibosEliminarRemesa(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Rem = 0;
                try { ID_Rem = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rem")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Rem;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionrecibosreciboseliminarremesa", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionrecibosreciboseliminarremesa-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion

        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/resumen-recibos-disponibles")]
        public async Task<strListadoConPaginador> FacturacionRecibosRecibosDisponiblesResumen(string paramsin)
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

                Int32 num = 20;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosdisponiblesresumen", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionrecibosrecibosdisponiblesresumen-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/recibos-disponibles")]
        public async Task<strListadoConPaginador> FacturacionRecibosRecibosDisponiblesFracciones(string paramsin)
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

                Int32 num = 20;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosdisponiblesfracciones", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionrecibosrecibosdisponiblesfracciones-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/resumen-recibos-seleccionados")]
        public async Task<strListadoConPaginador> FacturacionRecibosRecibosSeleccionadosResumen(string paramsin)
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

                Int32 num = 20;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosseleccionadosresumen", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionrecibosrecibosseleccionadosresumen-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/recibos-seleccionados")]
        public async Task<strListadoConPaginador> FacturacionRecibosRecibosSeleccionadosFracciones(string paramsin)
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

                Int32 num = 20;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosseleccionadosfracciones", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionrecibosrecibosseleccionadosfracciones-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/recibos/remesas-realizadas-hoy")]
        public async Task<strFacturacionRecibosResumenHoy> FacturacionRecibosRecibosRemesasRealizadasHoy(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionRecibosResumenHoy ret = new strFacturacionRecibosResumenHoy();

            try
            {
                //Int32 pag = 0;
                //try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                //string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");

                //Int32 num = 20;


                //Int32 ID_Cli2 = 0;
                //try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fac");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionrecibosrecibosremesasrealizadashoy", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strFacturacionRecibosResumenHoy>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionrecibosrecibosremesasrealizadashoy-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #endregion


        #endregion

        #region "Modulo Facturación - Remesas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/remesas")]
        public async Task<IActionResult> FacturacionRemesasListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionRemesasListado lis = new strFacturacionRemesasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string Año = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Añp");
                string Mes = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes");
                string Fe_Pres = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Pres");
                Int32 ID_Rem = 0;
                try { ID_Rem = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rem")); } catch { }

                strDatoS filtros = new strDatoS();
                filtros.datoI = ID_Rem;
                filtros.datoS1 = Año;
                filtros.datoS2 = Mes;
                filtros.datoS3 = Fe_Pres;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["Fe_Pres"] = Fe_Pres;
                if (ID_Rem>0)
                {
                    ViewData["ID_Rem"] = ID_Rem;
                } else
                {
                    ViewData["ID_Rem"] = "";
                }


                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionremesaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionRemesasListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionremesaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionFacturacionRemesasListadoViewModel(lis);

            return View("Remesas", viewModel);
        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/remesas/eliminar-remesa")]
        public async Task<strDato> FacturacionRemesasRecibosEliminarRemesa(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {

                Int32 ID_Rem = 0;
                try { ID_Rem = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rem")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Rem;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionremesasreciboseliminarremesa", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionremesasreciboseliminarremesa-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion

        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/remesas/remesa")]
        public async Task<strListadoConPaginador> FacturacionRemesasRemesaDetalles(string paramsin)
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

                Int32 num = 20;


                Int32 ID_Rem = 0;
                try { ID_Rem = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rem")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = "";
                dato.id = ID_Rem;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionremesasremesadetalles", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionremesasremesadetalles-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #endregion


        #endregion

        #region "Modulo Facturacion - Configuración"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion")]
        public async Task<IActionResult> FacturacionConfiguracion(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguracion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguracion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("Configuracion", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion


        #region "Modulo Facturacion - Configuracion - Abono de Facturas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/abono-de-facturas")]
        public async Task<IActionResult> FacturacionConfiguracionAbonoFacturas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionConfiguracionAbonoFacturas lis = new strFacturacionConfiguracionAbonoFacturas();

            try
            {
                Int32 ID_Idi = 0;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguracionabonofacturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionConfiguracionAbonoFacturas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguracionabonofacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionConfiguracionAbonoFacturasViewModel(lis);

            return View("Configuracion-AbonoFacturas", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/abono-facturas/abonos-guardar")]
        public async Task<strDato> FacturacionConfiguracionAbonoFacturasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Serie = 0;
                try { ID_Serie = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Serie")); } catch { }

                bool AboFacSoloPred = false;
                try { AboFacSoloPred = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "AboFacSoloPred")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionAbonoFacturas dato = new strFacturacionConfiguracionAbonoFacturas();
                dato.ID_Serie = ID_Serie;
                dato.AboFacSoloPred = AboFacSoloPred;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionabonofacturasguardar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionabonofacturasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Facturacion - Configuración - Recibos y Remesas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/recibos-y-remesas")]
        public async Task<IActionResult> FacturacionConfiguracionRecibosRemesas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionConfiguracionRecibosRemesas lis = new strFacturacionConfiguracionRecibosRemesas();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguracionrecibosremesas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionConfiguracionRecibosRemesas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguracionrecibosremesas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionConfiguracionRecibosRemesasViewModel(lis);

            return View("Configuracion-RemesasRecibos", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/recibos-y-remesas/recibos-guardar")]
        public async Task<strDato> FacturacionConfiguracionRecibosRemesasRecibosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                Int32 ID_Norma = 0;
                try { ID_Norma = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Norma")); } catch { }
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }

                bool RecRemPropDia = false;
                try { RecRemPropDia = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RecRemPropDia")); } catch { }
                bool RecRemNoBor = false;
                try { RecRemNoBor = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RecRemNoBor")); } catch { }

                Int32 RecRemMargenDias = 0;
                try { RecRemMargenDias = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RecRemMargenDias")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionRecibosRemesas dato = new strFacturacionConfiguracionRecibosRemesas();
                dato.ID_Cue = ID_Cue;
                dato.ID_Norma = ID_Norma;
                dato.ID_Tipo = ID_Tipo;
                dato.RecRemPropDia = RecRemPropDia;
                dato.RecRemNoBor = RecRemNoBor;
                dato.RecRemMargenDias = RecRemMargenDias;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionrecibosremesasrecibosguardar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionrecibosremesasrecibosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/recibos-y-remesas/remesas-guardar")]
        public async Task<strDato> FacturacionConfiguracionRecibosRemesasRemesasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                bool RecRemEnvMail = false;
                try { RecRemEnvMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RecRemEnvMail")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionRecibosRemesas dato = new strFacturacionConfiguracionRecibosRemesas();
                dato.ID_Mail = ID_Mail;
                dato.RecRemEnvMail = RecRemEnvMail;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionrecibosremesasremesasguardar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionrecibosremesasremesasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/recibos-y-remesas/probar-mail")]
        public async Task<strDato> FacturacionConfiguracionRecibosRemesasRemesasProbarMail(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Mail;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionrecibosremesasremesasprobarmail", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionrecibosremesasremesasprobarmail-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Facturacion - Configuracion - Envío de facturas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/envio-de-facturas")]
        public async Task<IActionResult> FacturacionConfiguracionEnvioFacturas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionConfiguracionEnvioFacturas lis = new strFacturacionConfiguracionEnvioFacturas();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguracionenviofacturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionConfiguracionEnvioFacturas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguracionenviofacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionConfiguracionEnvioFacturasViewModel(lis);

            return View("Configuracion-EnvioFacturas", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/envio-facturas/envio-guardar")]
        public async Task<strDato> FacturacionConfiguracionEnvioFacturasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                bool EnvFacPorMail = false;
                try { EnvFacPorMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvFacPorMail")); } catch { }

                bool EnvFacCopia = false;
                try { EnvFacCopia = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvFacCopia")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionEnvioFacturas dato = new strFacturacionConfiguracionEnvioFacturas();
                dato.ID_Mail = ID_Mail;
                dato.EnvFacPorMail = EnvFacPorMail;
                dato.EnvFacCopia = EnvFacCopia;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionenviofacturasguardar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionenviofacturasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/envio-facturas/probar-mail")]
        public async Task<strDato> FacturacionConfiguracionEnvioFacturasProbarMail(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Mail;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguracionenviofacturasprobarmail", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguracionenviofacturasprobarmail-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Facturacion - Configuracion - Series de facturación"

        #region "Vistas"


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Facturacion - Configuración - Devoluciones"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/devoluciones")]
        public async Task<IActionResult> FacturacionConfiguracionDevoluciones(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionConfiguracionDevoluciones lis = new strFacturacionConfiguracionDevoluciones();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguraciondevoluciones", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionConfiguracionDevoluciones>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguraciondevoluciones-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloFacturacionConfiguracionDevolucionesViewModel(lis);

            return View("Configuracion-Devoluciones", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-facturacion/configuracion/devoluciones/guardar")]
        public async Task<strDato> FacturacionConfiguracionDevolucionesGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                bool CobDevoSoli = false;
                try { CobDevoSoli = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CobDevoSoli")); } catch { }
                bool CobDevoCobNego = false;
                try { CobDevoCobNego = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CobDevoCobNego")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strFacturacionConfiguracionDevoluciones dato = new strFacturacionConfiguracionDevoluciones();
                dato.CobDevoSoli = CobDevoSoli;
                dato.CobDevoCobNego = CobDevoCobNego;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulofacturacion/extranetfacturacionconfiguraciondevolucionesguardar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion-extranetfacturacionconfiguraciondevolucionesguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion


        #region "Modulo Albaranes - Facturar [TEMPORAL]"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-albaranes/facturar")]
        public async Task<IActionResult> AlbaranesFacturar(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulofacturacion/extranetfacturacionconfiguracion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulofacturacion/extranetfacturacionconfiguracion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("AlbaranesFacturar", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        // Pendiente

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("/modulo-facturacion/configuracion-crm")]
        public async Task<IActionResult> ConfiguracionCRM(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                //Int32 ID_Ubi2 = 0;
                //try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Ubi2")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //strDato bus = new strDato();
                //bus.datoI = ID_Ubi2;
                //Transporte.Obj = bus;

                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cursos/cursos/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                //if (!dat.Err.EsError)
                //{
                //    lis = JsonConvert.DeserializeObject<strCursosUbicacionDetallesEditar>(dat.Obj.ToString());
                //}

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/cursos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            //if (lis.Err == null) { lis.Err = dat.Err; }

            //var viewModel = new CursosUbicacioDetallesViewModel(lis);

            return View("ConfiguracionCRM", null);
        }



    }
}
