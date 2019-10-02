using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using AsistenciasLibrary;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gaolos.Controllers.Asistencias
{
    public class AsistenciasController : Controller
    {
        // Asistencias - Rutas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("mantenimientos/planificar")]
        public async Task<IActionResult> MantenimientosPlanificar(string paramsin)
        {
            return View("MantenimientosPlanificar", null);
        }


        // Asistencias - Rutas
        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("asistencias/rutas")]
        //public async Task<IActionResult> Rutas(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strAsistenciasRutaAsignarValoresInicio lis = new strAsistenciasRutaAsignarValoresInicio();

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

        //        strbuscarlistado bus = new strbuscarlistado();
        //        bus.buscar = "";
        //        bus.numReg = 20;
        //        bus.pagina = 0;
        //        Transporte.obj = bus;

        //        var client = new HttpClient();
        //        var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/asistencias/extranetasistenciasrutasplanificar/?paramsin=" + JsonConvert.SerializeObject(Transporte));
        //        dat = JsonConvert.DeserializeObject<transportout>(response);
        //        if (!dat.err.eserror)
        //        {
        //            lis = JsonConvert.DeserializeObject<strAsistenciasRutaAsignarValoresInicio>(dat.obj.ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-asistencias/extranetasistenciasrutasplanificar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    if (lis.Err==null) { lis.Err = dat.err; }

        //    var viewModel = new AsistenciasRutasPlanificarViewModel(lis);

        //    return View("Rutas", viewModel);

        //}

        //// Asistencias - Rutas
        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("asistencias/rutasconfirmar")]
        //public async Task<IActionResult> RutasConfirmar(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strAsistenciasRutaConfirmarValoresInicio lis = new strAsistenciasRutaConfirmarValoresInicio();

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

        //        strbuscarlistado bus = new strbuscarlistado();
        //        bus.buscar = "";
        //        bus.numReg = 20;
        //        bus.pagina = 0;
        //        Transporte.obj = bus;

        //        var client = new HttpClient();
        //        var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/asistencias/extranetasistenciasrutasporconfirmar/?paramsin=" + JsonConvert.SerializeObject(Transporte));
        //        dat = JsonConvert.DeserializeObject<transportout>(response);
        //        if (!dat.err.eserror)
        //        {
        //            lis = JsonConvert.DeserializeObject<strAsistenciasRutaConfirmarValoresInicio>(dat.obj.ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-asistencias/extranetasistenciasrutasporconfirmar-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    if (lis.Err==null) { lis.Err = dat.err; }

        //    var viewModel = new AsistenciasRutasConfirmarViewModel(lis);

        //    return View("RutasConfirmar", viewModel);

        //}

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("asistencias/extranetasistenciasrutasplanificarcargatrabajo")]
        public async Task<strAsistenciasRutasPlanificarCargaTrabajoCarga> CargaDeTrabajo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strAsistenciasRutasPlanificarCargaTrabajoCarga ret = new strAsistenciasRutasPlanificarCargaTrabajoCarga();

            try
            {
                string Fecha="";
                try { Fecha = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fecha"); } catch { }

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

                strDato dato = new strDato();
                dato.datoS = Fecha;
                Transporte.obj = dato;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/asistencias/extranetasistenciasrutasplanificarcargatrabajo/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strAsistenciasRutasPlanificarCargaTrabajoCarga>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-asistencias/extranetasistenciasrutasplanificarcargatrabajo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err==null) { ret.Err = dat.err;}

            return ret;

        }

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("asistencias/extranetasistenciasrutasasignartrabajo")]
        //public async Task<transportout> AsistenciasRutasAsignarTrabajador(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    try
        //    {
        //        Int32 ID_Us_Asi = 0;
        //        try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
        //        Int32 ID_Us_Conf = 0;
        //        try { ID_Us_Conf = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Conf")); } catch { }
        //        string Modulos = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Modulos");
        //        string Fecha = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fecha");

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

        //        strDatoS dato = new strDatoS();
        //        dato.datoI = ID_Us_Asi;
        //        dato.datoD = ID_Us_Conf;
        //        dato.datoS1 = Modulos;
        //        dato.datoS2 = Fecha;
        //        Transporte.obj = dato;

        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri(Gaolos.Class.UrlApis.Url);
        //        var response = await client.PostAsJsonAsync("asistencias/extranetasistenciasrutasasignartrabajo", Transporte);
        //        dat = JsonConvert.DeserializeObject<transportout>(response.Content.ReadAsStringAsync().Result);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-asistencias/extranetasistenciasrutasasignartrabajo-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    return dat;

        //}


        // Asistencias - Rutas
        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("asistencias/rutasoperario")]
        //public async Task<IActionResult> RutasOperario(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strAsistenciasRutaConfirmarValoresInicio lis = new strAsistenciasRutaConfirmarValoresInicio();

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

        //        strbuscarlistado bus = new strbuscarlistado();
        //        bus.buscar = "";
        //        bus.numReg = 20;
        //        bus.pagina = 0;
        //        Transporte.obj = bus;

        //        var client = new HttpClient();
        //        var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/asistencias/extranetasistenciasrutasoperario/?paramsin=" + JsonConvert.SerializeObject(Transporte));
        //        dat = JsonConvert.DeserializeObject<transportout>(response);
        //        if (!dat.err.eserror)
        //        {
        //            lis = JsonConvert.DeserializeObject<strAsistenciasRutaConfirmarValoresInicio>(dat.obj.ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-asistencias/extranetasistenciasrutasoperario-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    if (lis.Err==null) { lis.Err = dat.err; }

        //    var viewModel = new AsistenciasRutasConfirmarViewModel(lis);

        //    return View("RutasOperario", viewModel);

        //}

        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("asistencias/extranetasistenciasrutasoperarioinfoasistencia")]
        //public async Task<strAsistenciasRutasOperarioInfoAsistencia> OperarioInfoAsistencia(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strAsistenciasRutasOperarioInfoAsistencia ret = new strAsistenciasRutasOperarioInfoAsistencia();

        //    try
        //    {
        //        Int32 ID_NAR = 0;
        //        try { ID_NAR = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_NAR")); } catch { }

        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_Idi = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

        //        if (Transporte.parameters == null) { Transporte.parameters = new param(); }
        //        Transporte.parameters.NIC = Ses.NIC;
        //        Transporte.parameters.RefNeg = Ses.RefNeg;
        //        Transporte.parameters.ID_Ses = Ses.ID_Ses;
        //        Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
        //        string IP = "";
        //        try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
        //        Transporte.parameters.IP = IP;

        //        strDato dato = new strDato();
        //        dato.datoI = ID_NAR;
        //        Transporte.obj = dato;

        //        var client = new HttpClient();
        //        var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/asistencias/extranetasistenciasrutasoperarioinfoasistencia/?paramsin=" + JsonConvert.SerializeObject(Transporte));
        //        dat = JsonConvert.DeserializeObject<transportout>(response);
        //        if (!dat.err.eserror)
        //        {
        //            ret = JsonConvert.DeserializeObject<strAsistenciasRutasOperarioInfoAsistencia>(dat.obj.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-asistencias/extranetasistenciasrutasoperarioinfoasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    if (ret.Err == null) { ret.Err = dat.err; }

        //    return ret;

        //}


        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("asistencias/extranetasistenciasrutasoperarioinfoasistenciamandet")]
        //public async Task<strMantenimientoDetalleAsistencia> OperarioInfoAsistenciaManDet(string paramsin)
        //{
        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strMantenimientoDetalleAsistencia ret = new strMantenimientoDetalleAsistencia();

        //    try
        //    {
        //        Int32 ID_Man2 = 0;
        //        try { ID_Man2= Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Man2")); } catch { }
        //        Int32 ID_ManPlanDet = 0;
        //        try { ID_ManPlanDet = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_ManPlanDet")); } catch { }


        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_Idi = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

        //        if (Transporte.parameters == null) { Transporte.parameters = new param(); }
        //        Transporte.parameters.NIC = Ses.NIC;
        //        Transporte.parameters.RefNeg = Ses.RefNeg;
        //        Transporte.parameters.ID_Ses = Ses.ID_Ses;
        //        Transporte.parameters.ClaveSesion = Ses.ClaveSesion;
        //        string IP = "";
        //        try { IP = Transporte.parameters.Tracert.Tracert[Transporte.parameters.Tracert.Tracert.Length - 1].RemoteIp; } catch { }
        //        Transporte.parameters.IP = IP;

        //        strDato dato = new strDato();
        //        dato.datoI = ID_Man2;
        //        dato.datoD = Convert.ToDecimal(ID_ManPlanDet);
        //        Transporte.obj = dato;

        //        var client = new HttpClient();
        //        var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/asistencias/extranetasistenciasrutasoperarioinfoasistenciamandet/?paramsin=" + JsonConvert.SerializeObject(Transporte));
        //        dat = JsonConvert.DeserializeObject<transportout>(response);
        //        if (!dat.err.eserror)
        //        {
        //            ret = JsonConvert.DeserializeObject<strMantenimientoDetalleAsistencia>(dat.obj.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-asistencias/extranetasistenciasrutasoperarioinfoasistenciamandet-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    if (ret.Err == null) { ret.Err = dat.err; }

        //    return ret;

        //}

    }
}
