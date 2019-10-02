using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using SeoLibrary;

using DashBoardLibrary;

namespace Gaolos.Controllers.SEO
{
    public class SEOController : Controller
    {

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-seo")]
        public async Task<IActionResult> SeoDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("seo/extranetseodashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-seo/extranetseodashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("SeoDashboard", viewModel);
        }

        #endregion

        /* SEO */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-seo/analytics")]
        public async Task<IActionResult> Analytics(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strSeoAnalyticsInicio lis = new strSeoAnalyticsInicio();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

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

                //strbuscarlistado bus = new strbuscarlistado();
                //bus.buscar = "";
                //bus.numReg = 20;
                //bus.pagina = 0;
                //Transporte.Obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/seo/extranetanalyticinicio/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strSeoAnalyticsInicio>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-seo/extranetanalyticinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new SeoAnalyticsInicioViewModel(lis);

            return View("Analytics", viewModel);
        }

        /* SEO - DashBoard */
        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("seo")]
        //public async Task<IActionResult> SEODashBoard(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout { err = new strerror() };

        //    try
        //    {
        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_Idi = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

        //        if (Transporte.parameters == null) { Transporte.parameters = new param(); }
        //        Transporte.parameters.NIC = Ses.NIC;
        //        Transporte.parameters.RefNeg = Ses.RefNeg;
        //        Transporte.parameters.ID_Ses = Ses.ID_Ses;
        //        Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
        //        Transporte.parameters.ID_Idi = ID_Idi;
        //        string IP = "";
        //        try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
        //        Transporte.parameters.IP = IP;
        //        Transporte.parameters.Path = HttpContext.Request.Path.Value;
        //        Transporte.parameters.PathParams = "";

        //        strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
        //        ViewData["Migas"] = Migas.datoS1;
        //        ViewData["Form"] = Migas.datoS2;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-seo/extranetanalyticinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return View("SEODashboard", null);
        //}

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("seo/analyticquery")]
        public async Task<strGoogleAnalyticsReturn> AnalyticQuery(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strGoogleAnalyticsReturn ret = new strGoogleAnalyticsReturn();

            try
            {
                Int32 ID_Dom = 0;
                try { ID_Dom = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dom")); } catch { }
                Int32 ID_Query = 0;
                try { ID_Query = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Query")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strGoogleAnaliticsQuery bus = new strGoogleAnaliticsQuery();
                bus.ID_Dom = ID_Dom; //326
                bus.ID_Query = ID_Query; //1
                bus.Fe_In = Fe_In; //"01/03/17";
                bus.Fe_Fi = Fe_Fi;  //"01/04/17";

                if (bus.Fe_In=="" || bus.Fe_Fi=="")
                {
                    ret.Err.eserror = true;
                    ret.Err.mensaje = "Fechas no válidas";
                    return ret;
                }
                if (ID_Dom==0)
                {
                    ret.Err.eserror = true;
                    ret.Err.mensaje = "Debe indicar un dominio";
                    return ret;
                }
                if (ID_Query == 0)
                {
                    ret.Err.eserror = true;
                    ret.Err.mensaje = "Debe indicar una consulta";
                    return ret;
                }
                Transporte.obj = bus;

                //var tjson = System.IO.File.ReadAllText("c:\\json.txt");

                var client = new HttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(Gaolos.Class.UrlApis.Url + "/seo/extranetanalyticvisitas/", Transporte).Result;
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
                if (!dat.err.eserror)
                {
                    //System.IO.File.WriteAllText("c:\\json.txt", dat.Obj.ToString());
                    ret = JsonConvert.DeserializeObject<strGoogleAnalyticsReturn>(dat.obj.ToString());
                    //ret = JsonConvert.DeserializeObject<strGoogleAnalyticsReturn>(tjson);
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-seo/extranetanalyticvisitas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err==null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("google/extranetanalyticvisitas")]
        public async Task<strGoogleAnalyticsReturn> AñadirPermisoAlNodo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strGoogleAnalyticsReturn lis = null;

            try
            {
                //Int32 ID_Nodo = 0;
                //try { ID_Nodo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Nodo")); } catch { }
                //string Modulos = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Modulos");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (Transporte.parameters == null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_Idi;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                strGoogleAnaliticsQuery bus = new strGoogleAnaliticsQuery();
                bus.ID_Dom = 326;
                bus.ID_Query = 1;
                bus.Fe_In = "01/03/17";
                bus.Fe_Fi = "01/04/17";
                Transporte.obj = bus;

                var tjson = System.IO.File.ReadAllText("c:\\json.txt");

                //var client = new HttpClient();
                //HttpResponseMessage response = client.PostAsJsonAsync(Gaolos.Class.UrlApis.Url + "/google/extranetanalyticvisitas/", Transporte).Result;
                //dat = JsonConvert.DeserializeObject<TransportOut>(response.Content.ReadAsStringAsync().Result);


                if (!dat.err.eserror)
                {
                    //System.IO.File.WriteAllText("c:\\json.txt", dat.Obj.ToString());
                    //lis = JsonConvert.DeserializeObject<strGoogleAnalyticsReturn>(dat.Obj.ToString());
                    lis = JsonConvert.DeserializeObject<strGoogleAnalyticsReturn>(tjson);
                }
                lis.Err = dat.err;
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-google/extranetanalyticvisitas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            lis.Err = dat.err;

            return lis;
        }

    }
}
