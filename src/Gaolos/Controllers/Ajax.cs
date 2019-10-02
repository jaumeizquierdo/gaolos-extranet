using Microsoft.AspNetCore.Http;
using Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Features;
using LogsBbdds;
using Infrastructure;
using Gaolos.Session;

namespace Gaolos.Controllers
{

    public class Ajax : Controller
    {

        [SessionControl]
        [UserControl]
        [Route("ajax/MenuGaolosDecolapse")]
        public bool MenuGaolosDecolapse(string id)
        {
            try
            {
                string[] ids = null;
                string str = HttpContext.Session.GetString("menudeco");
                if (str != null) { ids = JsonConvert.DeserializeObject<string[]>(str); }

                if (ids == null)
                {
                    ids = new string[1];
                    ids[0] = id;
                }
                else
                {

                    for (int r = 0; r < ids.Length; r++)
                    {
                        if (ids[r] == id) { return false; }
                    }

                    Array.Resize(ref ids, ids.Length + 1);
                    ids[ids.Length - 1] = id;
                }
                str = JsonConvert.SerializeObject(ids);
                HttpContext.Session.SetString("menudeco", str);
            }
            catch { }

            return true;
        }

        [SessionControl]
        [UserControl]
        [Route("ajax/MenuGaolosColapse")]
        public bool MenuGaolosColapse(string id)
        {
            try
            {
                string[] ids = null;
                string str = HttpContext.Session.GetString("menudeco");
                if (str != null) { ids = JsonConvert.DeserializeObject<string[]>(str); }

                if (ids != null)
                {
                    for (int r = 0; r < ids.Length; r++)
                    {
                        if (ids[r] == id)
                        {
                            Funciones.Funciones.RemoveAt(ref ids, r);
                            str = JsonConvert.SerializeObject(ids);
                            HttpContext.Session.SetString("menudeco", str);
                            return true;
                        }
                    }

                }
            }
            catch { }

            return true;
        }



        [SessionControl]
        [UserControl]
        [Infrastructure.HttpRequest]
        [Route("ajax/nodomodificar")]
        public async Task<strDato> NodoModificar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato dato = new strDato();

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

                string[] val = Transporte.parameters.ParamsKeys[0].ToString().Split('_');

                dato.datoI = Convert.ToInt32(val[0]);
                dato.datoS = val[1];

                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/arbolnodos/arbolnodomodificar/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    Nodo nodo = JsonConvert.DeserializeObject<Nodo>(dat.obj.ToString());
                    Int32 nivel = 0;
                    string txt = "";
                    Funciones.Funciones.LeerNodo(ref txt, nodo, ref nivel);
                    dato.datoS = txt;

                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-arbolnodos/arbolnodos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            dato.Err = dat.err;

            return dato;
        }

        [SessionControl]
        [UserControl]
        [Infrastructure.HttpRequest]
        [Route("ajax/nodonuevo")]
        public async Task<strDato> NodoNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato dato = new strDato();

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

                string[] val = Transporte.parameters.ParamsKeys[0].ToString().Split('_');

                dato.datoI = Convert.ToInt32(val[0]);
                dato.datoS = val[1];

                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/arbolnodos/arbolnodonuevo/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    Nodo nodo = JsonConvert.DeserializeObject<Nodo>(dat.obj.ToString());
                    Int32 nivel = 0;
                    string txt = "";
                    Funciones.Funciones.LeerNodo(ref txt, nodo, ref nivel);
                    dato.datoS = txt;

                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-arbolnodos/arbolnodos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            dato.Err = dat.err;

            return dato;
        }

        [SessionControl]
        [UserControl]
        [Infrastructure.HttpRequest]
        [Route("ajax/nodoeliminar")]
        public async Task<strDato> NodoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato dato = new strDato();

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

                dato.datoI = Convert.ToInt32(Transporte.parameters.ParamsKeys[0].ToString());
                dato.datoS = "";

                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/arbolnodos/arbolnodoeliminar/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    Nodo nodo = JsonConvert.DeserializeObject<Nodo>(dat.obj.ToString());
                    Int32 nivel = 0;
                    string txt = "";
                    Funciones.Funciones.LeerNodo(ref txt, nodo, ref nivel);
                    dato.datoS = txt;

                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-arbolnodos/arbolnodos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            dato.Err = dat.err;

            return dato;
        }

    }
}


//[Infrastructure.HttpRequest]
//public async Task<Error> Login(string paramsin)
//{

//    TransportIn Transporte = null;
//    if (paramsin == null) { Transporte = new TransportIn(); }
//    else { Transporte = JsonConvert.DeserializeObject<TransportIn>(paramsin); }

//    var client = new HttpClient();
//    TransportOut TransOut = null;

//    Param param = Transporte.Params;
//    //Sesion.Sesion.revisarSesion(ref param, HttpContext);

//    string NIC = "";
//    string Pass = "";
//    bool Recordar = false;



//    int index = Array.FindIndex(param.ParamsKeys, row => row.Contains("username"));
//    try { NIC = param.ParamsValues[index]; } catch { }
//    index = Array.FindIndex(param.ParamsKeys, row => row.Contains("password"));
//    try { Pass = param.ParamsValues[index]; } catch { }
//    index = Array.FindIndex(param.ParamsKeys, row => row.Contains("rememberMe"));
//    try
//    {
//        string esp = param.ParamsValues[index];
//        Recordar = true;
//    } catch { }


//    // Filtros caracteres válidos

//    param.NIC = NIC;
//    param.RefNeg = Pass;


//    try
//    {
//        string response = await client.GetStringAsync(Class.UrlApis.Url + "/logincontroller/gaolosloginextranet/?paramsin=" + JsonConvert.SerializeObject(Transporte));
//        TransOut = JsonConvert.DeserializeObject<TransportOut>(response);
//    }
//    catch (Exception ex)
//    {
//        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-logincontroller/gaolosloginextranet-", logInfo = new logInterno { strSql = "", ex = ex } });
//        TransOut.Err.EsError = true;
//        TransOut.Err.Mensaje = ex.Message;
//    }

//    return TransOut.Err;

//}

//[Infrastructure.HttpRequest]
//public async Task<contenedorMenuLateral> MenuLateral(string paramsin)
//{

//    TransportIn Transporte = null;
//    if (paramsin == null) { Transporte = new TransportIn(); }
//    else { Transporte = JsonConvert.DeserializeObject<TransportIn>(paramsin); }

//    var client = new HttpClient();
//    TransportOut TransOut = null;

//    Param param = Transporte.Params;

//    try
//    {
//        string response = await client.GetStringAsync(Class.UrlApis.Url + "/menuscontroller/menugaolos/?paramsin=" + JsonConvert.SerializeObject(Transporte));
//        TransOut = JsonConvert.DeserializeObject<TransportOut>(response);
//    }
//    catch (Exception ex)
//    {
//        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-menuscontroller/menugaolos-", logInfo = new logInterno { strSql = "", ex = ex } });
//        TransOut.Err.EsError = true;
//        TransOut.Err.Mensaje = ex.Message;
//    }
//    contenedorMenuLateral ss= (contenedorMenuLateral)TransOut.Obj;

//    return ss;

//}

