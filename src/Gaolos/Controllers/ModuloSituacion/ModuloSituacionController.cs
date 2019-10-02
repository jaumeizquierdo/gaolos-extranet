using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using SituacionLibrary;
using DashBoardLibrary;

namespace Gaolos.Controllers.Situacion
{

    public class ModuloSituacionController : Controller
    {

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-situacion")]
        public async Task<IActionResult> SituacionDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosituacion/extranetsituaciondashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosituacion/extranetsituaciondashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("SituacionDashboard", viewModel);
        }

        #endregion

        #region "Vistas"

        /* Situacion / Clientes */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-situacion/clientes")]
        public async Task<IActionResult> Clientes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strSituacionClientes lis = new strSituacionClientes();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                if (Ses == null) { return Redirect(Class.Globales.UrlSesEnd(HttpContext)); }

                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosituacion/extranetclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strSituacionClientes>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosituacion/extranetclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new SituacionClientesViewModel(lis);

            return View("Clientes", viewModel);
        }

        /* Situacion / Usuarios */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-situacion/usuarios")]
        public async Task<IActionResult> Usuarios(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strSituacionUsuarios lis = new strSituacionUsuarios();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                if (Ses == null) { return Redirect(Class.Globales.UrlSesEnd(HttpContext)); }

                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDatoS bus = new strDatoS
                {
                    datoS1 = Fe_In,
                    datoS2 = Fe_Fi
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosituacion/extranetusuarios", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strSituacionUsuarios>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosituacion/extranetusuarios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new SituacionUsuariosViewModel(lis);

            return View("Usuarios", viewModel);
        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-situacion/extranetusuarioresumenpresupuestos")]
        public async Task<strSituacionUsuarios> SituacionUsuarioResumenPresupuestos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strSituacionUsuarios ret = new strSituacionUsuarios();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                Int32 ID_Ope = 0;
                Int32.TryParse(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID"), out ID_Ope);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                bus.datoI = ID_Ope;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosituacion/extranetusuarioresumenpresupuestos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strSituacionUsuarios>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosituacion/extranetusuarioresumenpresupuestos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            return ret;

        }








        /* Situacion / DashBoard */
        //[Infrastructure.SessionControl]
        //[Infrastructure.HttpRequest]
        //[Route("modulo-situacion")]
        //public async Task<IActionResult> SituacionDashBoard(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout { err = new strerror() };

        //    try
        //    {
        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        if (Ses == null) { return Redirect(Class.Globales.UrlSesEnd(HttpContext)); }

        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);
        //        string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
        //        string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
        //        ViewData["Migas"] = Migas.datoS1;
        //        ViewData["Form"] = Migas.datoS2;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosituacion/extranetusuarios-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return View("SituacionDashboard", null);
        //}


        /* Situacion / DashBoard */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-situacion/empresa")]
        public async Task<IActionResult> SituacionEmpresa(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strSituacionEmpresa lis = new strSituacionEmpresa();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                if (Ses == null) { return Redirect(Class.Globales.UrlSesEnd(HttpContext)); }

                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosituacion/extranetempresa", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strSituacionEmpresa>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosituacion/extranetempresa-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new SituacionEmpresaViewModel(lis);

            return View("SituacionEmpresa", viewModel);
        }


        #endregion







    }

}
