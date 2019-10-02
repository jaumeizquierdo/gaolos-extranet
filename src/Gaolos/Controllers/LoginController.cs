using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using Infrastructure;
using MenuGaolosLibrary;


namespace Gaolos.Controllers
{
    public class LoginController : Controller
    {
        [Infrastructure.HttpRequest]
        [SessionControl]
        public async Task<IActionResult> Login(string paramsin)
        {
            transportin Transporte = null;
            if (paramsin == null) { Transporte = new transportin(); }
            else { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                if (Ses != null) { if (Ses.NIC!="" && Ses.NIC!=null) { return Redirect("/inicio"); } }


                //ViewData["sesMe"] = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ses");

                var client = new HttpClient();
                var response = await client.GetStringAsync(Class.UrlApis.Url + "/logincontroller/homeloginlabel/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror) {
                    for (int i = 0; i < dat.labels.Length; i += 1)
                    {
                        ViewData[dat.labels[i].Alias] = dat.labels[i].Mensaje;
                    }

                    string usernamecookie = Request.Cookies["usernamecookie"];
                    if (usernamecookie == null) usernamecookie = "";
                    string remembermecookie = Request.Cookies["remembermecookie"];
                    if (remembermecookie == null) remembermecookie = "";
                    ViewData["usernamecookie"] = usernamecookie;
                    ViewData["remembermecookie"] = remembermecookie;
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-logincontroller/homeloginlabel-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return View(dat.labels);
        }




        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("login/ajaxlogin")]
        public async Task<transportout> AjaxLogin(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "username");
                string Pass = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "password");
                bool Recordar = false;
                string rememberMe = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "rememberMe");
                if (rememberMe == "true") Recordar = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                //if (Ses == null)
                //{
                //    dat.Err.EsError = true;
                //    dat.Err.Mensaje = "No hay sesion";
                //    return dat.Err;
                //}
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

                sesionInicio dato = new sesionInicio();
                dato.NIC = NIC;
                dato.Clave = Pass;
                dato.ID_Ses = Ses.ID_Ses;
                dato.ClaveSesion = Ses.ClaveSesion;
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("/logincontroller/gaolosloginextranet", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
                if (!dat.err.eserror)
                {
                    sesionIniciada ret = JsonConvert.DeserializeObject<sesionIniciada>(dat.obj.ToString());
                    Ses.Nom = ret.Nom;
                    Ses.Emp = ret.Emp;
                    Ses.NIC = NIC;
                    Ses.RefNeg = ret.RefNeg;
                    //Ses.MenuGaolos = ret.MenuGaolos;
                    //Ses.MenuUsuario = ret.MenuUsuario;
                    //Ses.MenuEmpresas = ret.MenuEmpresas;
                    //Ses.MenuUsuarioOpciones = ret.MenuUsuarioOpciones;
                    //Ses.ID_MenuSel = ret.ID_MenuSel;

                    HttpContext.Session.SetString("menugaolosID_MenuSel", ret.MenuGaolos.ID_MenuSel.ToString());
                    HttpContext.Session.SetString("ID_IdiExtranet", ret.ID_Idi.ToString());
                    HttpContext.Session.SetObjectAsJson("menuempresas", ret.MenuEmpresas);
                    HttpContext.Session.SetObjectAsJson("menuusuarioopciones", ret.MenuUsuarioOpciones);
                    HttpContext.Session.SetObjectAsJson("menuusuario", ret.MenuUsuario);
                    HttpContext.Session.SetObjectAsJson("menugaolos", ret.MenuGaolos);
                    HttpContext.Session.SetString("menudeco", ret.MenuGaolos.Deco);

                    if (Recordar)
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddMinutes(60 * 24 * 30);
                        //option.Expires = DateTime.Now.AddMilliseconds(10);
                        Response.Cookies.Append("usernamecookie", NIC, option);
                        Response.Cookies.Append("remembermecookie", rememberMe, option);
                    }

                } else
                {
                    Ses.NIC = "";
                }
                HttpContext.Session.SetObjectAsJson("SesExtranet", Ses);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-logincontroller/gaolosloginextranet-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }



        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("login/ajaxlogout")]
        public async Task<transportout> AjaxLogout(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

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

                Int32 ID_MenuSel = 0;
                Int32.TryParse(HttpContext.Session.GetString("menugaolosID_MenuSel"), out ID_MenuSel);

                strDato dato = new strDato();
                dato.datoI = ID_MenuSel;
                dato.datoS = HttpContext.Session.GetString("menudeco");
                Transporte.obj = dato;

                var client = new HttpClient();
                client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
                var response = await client.PostAsJsonAsync("/logincontroller/gaolosloguutextranet", Transporte);
                dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
                if (!dat.err.eserror)
                {
                    Ses.NIC = "";
                    Ses.Emp = "";
                    Ses.EsGlo = false;
                    Ses.Nom = "";
                    Ses.RefNeg = "";

                    HttpContext.Session.SetString("menugaolosID_MenuSel", "");
                    HttpContext.Session.SetObjectAsJson("menugaolos", null);
                    HttpContext.Session.SetString("menudeco", "");
                    HttpContext.Session.SetObjectAsJson("menuusuario", null);
                    HttpContext.Session.SetObjectAsJson("menuusuarioopciones", null);
                    HttpContext.Session.SetObjectAsJson("menuempresas", null);
                }
                HttpContext.Session.SetObjectAsJson("SesExtranet", Ses);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-logincontroller/gaolosloginextranet-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [SessionControl]
        public async Task<strerror> AjaxFiltroUsuario(string paramsin)
        {

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                transportin Transporte = null;
                if (paramsin == null) { Transporte = new transportin(); }
                else { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                if (Transporte.parameters==null) { Transporte.parameters = new param(); }
                Transporte.parameters.NIC = Ses.NIC;
                Transporte.parameters.RefNeg = Ses.RefNeg;
                Transporte.parameters.ID_Ses = Ses.ID_Ses;
                Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
                Transporte.parameters.ID_Idi = ID_Idi;
                string IP = "";
                try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
                Transporte.parameters.IP = IP;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/menuscontroller/menugaolos/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (dat.err.eserror) { return dat.err; }

                contenedorMenuLateral ss = JsonConvert.DeserializeObject<contenedorMenuLateral>(dat.obj.ToString());
                HttpContext.Session.SetString("menugaolosID_MenuSel", ss.ID_MenuSel.ToString());
                HttpContext.Session.SetObjectAsJson("menugaolos", ss);
                HttpContext.Session.SetString("menudeco", "");
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuscontroller/menugaolos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }


        [Infrastructure.HttpRequest]
        [SessionControl]
        public async Task<strerror> AjaxCambioNegocio(string paramsin)
        {

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                transportin Transporte = null;
                if (paramsin == null) { Transporte = new transportin(); }
                else { Transporte = JsonConvert.DeserializeObject<transportin>(paramsin); }

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

                strDato dati = new strDato();

                string NegRef = "";
                try
                {
                    for (Int32 t = 0; t < Transporte.parameters.ParamsKeys.Length; t++)
                    {
                        if (Transporte.parameters.ParamsKeys[t].Replace("refneg_", "") != Transporte.parameters.ParamsKeys[t])
                        {
                            NegRef = Transporte.parameters.ParamsKeys[t].Replace("refneg_", "");
                            break;
                        }
                    }
                } catch { }

                dati.datoS = NegRef;

                Transporte.obj = dati;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/logincontroller/gaoloscambionegocioextranet/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (dat.err.eserror) { return dat.err; }

                sesionIniciada ret = JsonConvert.DeserializeObject<sesionIniciada>(dat.obj.ToString());
                Ses.Nom = ret.Nom;
                Ses.Emp = ret.Emp;
                //Ses.NIC = NIC; // No varia
                Ses.RefNeg = ret.RefNeg;

                HttpContext.Session.SetString("menugaolosID_MenuSel", ret.MenuGaolos.ID_MenuSel.ToString());
                HttpContext.Session.SetString("ID_IdiExtranet", ret.ID_Idi.ToString());
                HttpContext.Session.SetObjectAsJson("menuempresas", ret.MenuEmpresas);
                HttpContext.Session.SetObjectAsJson("menuusuarioopciones", ret.MenuUsuarioOpciones);
                HttpContext.Session.SetObjectAsJson("menuusuario", ret.MenuUsuario);
                HttpContext.Session.SetObjectAsJson("menugaolos", ret.MenuGaolos);
                HttpContext.Session.SetString("menudeco", ret.MenuGaolos.Deco);

                HttpContext.Session.SetObjectAsJson("SesExtranet", Ses);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuscontroller/menugaolos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat.err;

        }


    }
}
