
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using LogsBbdds;
using MiEmpresaLibrary;
using ModuloEmpresasRelacionadasLibrary;
using DashBoardLibrary;

namespace Gaolos.Controllers.EmpresasRelacionadas
{
    public class ModuloEmpresasRelacionadasController : Controller
    {

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-empresas-relacionadas")]
        public async Task<IActionResult> EmpresasRelacionadasDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloempresasrelacionadas/extranetempresasrelacionadasdashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloempresasrelacionadas/extranetempresasrelacionadasdashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("EmpresasRelacionadasDashboard", viewModel);
        }

        #endregion

        #region "Vistas"

        /* Empresas relacionadas listado */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-empresas-relacionadas/listado")]
        public async Task<IActionResult> EmpresasRelacionadasListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strEmpresasRelacionadasListado lis = new strEmpresasRelacionadasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

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

                ViewData["buscar"] = buscar;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 50,
                    pagina = pag
                };
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/moduloempresasrelacionadas/extranetempresasrelacionadaslistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strEmpresasRelacionadasListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloempresasrelacionadas/extranetempresasrelacionadaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloEmpresasRelacionadasListadoViewModel(lis);

            return View("EmpresasRelacionadasListado", viewModel);
        }

        /* Empresas relacionadas listado */
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-empresas-relacionadas/por-agentes")]
        public async Task<IActionResult> EmpresasRelacionadasDelAgenteListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strEmpresasRelacionadasResumenAgenteListado lis = new strEmpresasRelacionadasResumenAgenteListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string Cobro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cobro");
                string Presup = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Presup");
                string Soli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Soli");
                string Asis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Asis");
                string Precios = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios");
                string Tipos = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tipos");
                Int32 ID_Agente = 0;
                try { ID_Agente = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Agente")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

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

                strEmpresasRelacionadasResumenAgenteListadoRelacionadosDetalles obj = new strEmpresasRelacionadasResumenAgenteListadoRelacionadosDetalles();
                obj.NumAsis = Asis;
                obj.NumCob = Cobro;
                obj.NumPres = Presup;
                obj.NumSoli = Soli;
                obj.NumAsis = Asis;
                obj.NumPrecios = Precios;
                obj.Num = Tipos;

                ViewData["buscar"] = buscar;
                ViewData["Cobro"] = Cobro;
                ViewData["Presup"] = Presup;
                ViewData["Soli"] = Soli;
                ViewData["Asis"] = Asis;
                ViewData["Precios"] = Precios;
                ViewData["Tipos"] = Tipos;
                ViewData["ID_Agente"] = ID_Agente;


                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 50,
                    pagina = pag,
                    id = ID_Agente,
                    obj = obj
                };
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/moduloempresasrelacionadas/extranetEmpresasrelacionadasdelagentelistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strEmpresasRelacionadasResumenAgenteListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloempresasrelacionadas/extranetEmpresasrelacionadasdelagentelistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloEmpresasRelacionadasResumenAgenteListadoViewModel(lis);

            return View("EmpresasRelacionadasPorAgenteListado", viewModel);
        }

        #endregion

        #region "Ajax - Empresas Relacionadas"

        #region "Guardar - Post"

        #endregion

        #region "Recuperar datos - Get"

        #endregion

        #endregion

    }
}
