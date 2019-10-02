
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using Models;
using Newtonsoft.Json;
using System.Net;
using Gaolos.Session;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Class;

namespace Infrastructure
{
    public class SessionControl : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            transportin Transporte = null;

            if (context.ActionArguments.Count > 0)
            {
                try { Transporte = (transportin)context.ActionArguments["transporteIn"]; } catch { }
                try { Transporte = JsonConvert.DeserializeObject<transportin>(context.ActionArguments["paramsin"].ToString()); } catch { }
            }
            if (Transporte == null) { Transporte = new transportin(); }

            sesionExtranet Ses = context.HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
            if (Ses == null)
            {

                try
                {
                    //Leer cookie
                    string IdiLast = context.HttpContext.Request.Cookies["gaolosextranet_idi"];
                    string SesOld = context.HttpContext.Request.Cookies["gaolosextranet_id"];
                    string Fe_Al = context.HttpContext.Request.Cookies["gaolosextranet_fe"];

                    string iniUrl = "";
                    foreach (var Items in context.RouteData.Values)
                    {
                        if (iniUrl != "") { iniUrl += "/"; }
                        iniUrl += Items.Value;
                    }

                    if (iniUrl != "Login/Login")
                    {
                        context.HttpContext.Session.SetObjectAsJson("iniUrl", iniUrl);
                        //Sesion caducada
                    }

                    Int32 ID_Idi = 1;
                    if (IdiLast!=null) { Int32.TryParse(IdiLast, out ID_Idi); }

                    //Obtenemos parametros iniciales
                    strSolicitarSesion dati = new strSolicitarSesion();
                    dati.iniUrl = iniUrl;
                    dati.Cookie_ID_SesOld = 0;
                    if (SesOld != null) { try { dati.Cookie_ID_SesOld = Convert.ToInt32(SesOld); } catch { } }
                    dati.Cookie_ID_Idi = 0;
                    if (IdiLast != null) { try { dati.Cookie_ID_Idi = Convert.ToInt32(IdiLast); } catch { } }
                    dati.Cookie_Fe_Al = Fe_Al;
                    dati.Host = context.HttpContext.Request.Host.Host; //res += "RemoteIpAddress: " + context.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress + "<br>";
                    dati.Port = context.HttpContext.Request.Host.Port; //res += "RemoteIpAddress: " + context.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress + "<br>";
                    dati.RemoteIP = context.HttpContext.Connection.RemoteIpAddress.ToString(); //var remoteIpAddress2 = HttpContext.Connection.RemoteIpAddress.ToString();
                    dati.LocalIP = context.HttpContext.Connection.LocalIpAddress.ToString(); //var LocalIpAddress = HttpContext.Connection.LocalIpAddress.ToString();
                    //dati.Language = ((Microsoft.AspNetCore.Server.Kestrel.Internal.Http.FrameRequestHeaders)context.HttpContext.Request.Headers).HeaderAcceptLanguage;
                    //dati.UserAgent = ((Microsoft.AspNetCore.Server.Kestrel.Internal.Http.FrameRequestHeaders)context.HttpContext.Request.Headers).HeaderUserAgent;
                    //dati.Referer = ((Microsoft.AspNetCore.Server.Kestrel.Internal.Http.FrameRequestHeaders)context.HttpContext.Request.Headers).HeaderReferer;
                    dati.Language = ((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpRequestHeaders)context.HttpContext.Request.Headers).HeaderAcceptLanguage; // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.FrameRequestHeaders
                    dati.UserAgent = ((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpRequestHeaders)context.HttpContext.Request.Headers).HeaderUserAgent; // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.FrameRequestHeaders
                    dati.Referer = ((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpRequestHeaders)context.HttpContext.Request.Headers).HeaderReferer; // Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.FrameRequestHeaders
                    dati.sessionID = context.HttpContext.Session.Id;
                    dati.Dom = UrlApis.webUrl;

                    //Solicitamos inicio de sesion

                    try
                    {
                        Transporte.obj = dati;

                        //var client = new HttpClient();
                        //var response = await client.GetStringAsync(UrlApis.Url + "/logincontroller/solicitarsesion/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                        //TransportOut dat = JsonConvert.DeserializeObject<TransportOut>(response);
                        //strDato ret = JsonConvert.DeserializeObject<strDato>(dat.Obj.ToString()); // (strDato)dat.Obj; // JsonConvert.DeserializeObject<strDato>(response);
                        //if (ret.Err.EsError) { return; }

                        var client = new HttpClient();
                        HttpResponseMessage response = client.PostAsJsonAsync(UrlApis.Url + "/logincontroller/solicitarsesion/", Transporte).Result;
                        transportout dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
                        //try
                        //{
                        //    string txt = response.Content.ReadAsStringAsync().Result; //JsonConvert.SerializeObject(dat);
                        //    LogBbdd.Write(new logPartial { nombreFichero = "postaa-", logInfo = new logInterno { strSql = txt } });
                        //}
                        //catch
                        //{ }
                        strDato ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                        if (ret.Err.eserror) { return; }

                        Int32 ID_Ses = ret.datoI;
                        Int32 ID_SesAbi = Convert.ToInt32(ret.datoD);
                        string ClaveSesion = ret.datoS;                            

                        //Escribir cookie
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddYears(1);
                        context.HttpContext.Response.Cookies.Append("gaolosextranet_idi", ID_Idi.ToString(), options);
                        context.HttpContext.Response.Cookies.Append("gaolosextranet_id", ID_Ses.ToString(), options);
                        context.HttpContext.Response.Cookies.Append("gaolosextranet_fe", DateTime.Now.ToString(), options);
                        context.HttpContext.Response.Cookies.Append("gaolosextranet_ini", iniUrl, options);

                        Ses = new sesionExtranet();
                        Ses.ID_Idi = ID_Idi;
                        Ses.ClaveSesion = ClaveSesion;
                        Ses.ID_Ses = ID_Ses;
                        //Ses.ID_SesAbi = ID_SesAbi;


                        context.HttpContext.Session.SetObjectAsJson("SesExtranet", Ses);

                    }
                    catch (Exception ex)
                    {
                        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-logincontroller/solicitarsesion-", logInfo = new logInterno { strSql = "", ex = ex } });
                        //dat.Err.EsError = true;
                        //dat.Err.Mensaje = ex.Message;
                    }

                }
                catch (Exception ex)
                {
                    LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-OnActionExecuting-", logInfo = new logInterno { strSql = "", ex = ex } });
                    //dat.Err.EsError = true;
                    //dat.Err.Mensaje = ex.Message;
                }

            }

        }
    }
}
