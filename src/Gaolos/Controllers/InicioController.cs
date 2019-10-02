using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using System.Xml;
using MyGaolosLibrary;

namespace Gaolos.Controllers
{
    public class InicioController : Controller
    {
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Infrastructure.UserControl]
        [Route("inicio")]
        [Route("start")]
        public async Task<IActionResult> Inicio(string paramsin) 
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloInicio ret = new strModuloInicio();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                //Int32 ID_Ubi2 = 0;
                //try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Ubi2")); } catch { }

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_Idi;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;
                Transporte.parameters.Path = HttpContext.Request.Path.Value;
                Transporte.parameters.PathParams = "";

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                //bus.datoI = ID_Ubi2;
                Transporte.obj = bus;


                dat = await Infrastructure.ReturnResults.GetResponseAsync("mygaolos/extranetinicio", Transporte, HttpContext);

                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/mygaolos/extranetinicio/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strModuloInicio>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-mygaolos/extranetinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            var viewModel = new MyGaolosInicioExtranetViewModel(ret);

            return View("Index", viewModel); 
        }


        [Infrastructure.SessionControl]
        [Route("menus-counter")]
        public async Task<menusCounter[]> GetMenusCounter()
        {
            menusCounter[] mc = null;
            try
            {
                mc= JsonConvert.DeserializeObject<menusCounter[]>(HttpContext.Session.GetString("menus_counter"));
            }
            catch { }
            return mc;
        }

        // Cursos Dashboard
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("404")]
        public async Task<IActionResult> NotFound(string paramsin)
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

            return View("NotFound", null);
        }


        #region "Comun - Direccion"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("comun/direccionpaises")]
        public async Task<strListaErr> DireccionPaises(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("comun/extranetdireccionpaises", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-comun-extranetdireccionpaises-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("comun/direccioncambiopais")]
        public async Task<strListaErr> DireccionCambioPais(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/comun/extranetdireccioncambiopais/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-comun-extranetdireccioncambiopais-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("comun/direccioncambioprovincia")]
        public async Task<strListaErr> DireccionCambioProvincia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/comun/extranetdireccioncambioprovincia/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-comun-extranetdireccioncambioprovincia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("comun/direccioncambiocp")]
        public async Task<strListaErr> DireccionCambioCP(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                dato.datoS3 = CP;
                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/comun/extranetdireccioncambiocp/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-comun-extranetdireccioncambiocp-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion



    }
}
