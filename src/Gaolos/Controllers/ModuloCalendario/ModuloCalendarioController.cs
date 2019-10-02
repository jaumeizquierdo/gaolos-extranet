using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using System.Web;
using ModuloCalendarioLibrary;

namespace Gaolos.Controllers
{
    public class ModuloCalendarioController : Controller
    {

        #region "Modulo Calendario - Grupos"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-calendario/grupos")]
        public async Task<IActionResult> CalendarioGruposInicio(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato lis = new strDato();

            try
            {
                //Int32 pag = 0;
                //try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocalendario/extranetcalendariogruposinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocalendario/extranetcalendariogruposinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCalendarioInicioViewModel(lis);

            return View("CalendarioGrupos", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-calendario/grupos/add-grupo")]
        public async Task<transportout> CalendarioGruposAddGrupo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                Int32 ID_Us_Auto = 0;
                try { ID_Us_Auto = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Auto")); } catch { }
                bool CtrlTotal = false;
                try { CtrlTotal = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CtrlTotal")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Grupo;
                dato.datoD = Convert.ToDecimal(ID_Us_Auto);
                dato.datoB = CtrlTotal;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulocalendario/extranetcalendariogruposaddgrupo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocalendario/extranetcalendariogruposaddgrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-calendario/grupos/buscar-grupo")]
        public async Task<strListaErr> CalendarioGruposBuscarGrupo(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocalendario/extranetcalendariogruposbuscargrupo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocalendario/extranetcalendariogruposbuscargrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-calendario/grupos/buscar-usuario")]
        public async Task<strListaErr> CalendarioGruposBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocalendario/extranetcalendariogruposbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocalendario/extranetcalendariogruposbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
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
