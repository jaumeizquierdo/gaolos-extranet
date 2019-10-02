using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using ModuloClientesLibrary;

using DashBoardLibrary;

namespace Gaolos.Controllers
{
    public class ModuloClientesController : Controller
    {
        #region "Modulo Clientes - Consultas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/consultas")]
        public async Task<IActionResult> ClientesConsultas(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesconsultas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesconsultas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("ClientesConsultas", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Consultas - Resumen Empresas Relacionadas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/consultas/resumen-empresas-relacionadas")]
        public async Task<IActionResult> ClientesConsultasResumenEmpresasRelacionadas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesConsultasResumenEmpresasRelacionadasListado lis = new strClientesConsultasResumenEmpresasRelacionadasListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string Fe_In1 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In1");
                string Fe_Fi1 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi1");
                string Fe_In2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In2");
                string Fe_Fi2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi2");
                Int32 ID_Us_Agente = 0;
                try { ID_Us_Agente = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vAgente")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vemprelvin")); } catch { }

                strDatoS filtros = new strDatoS();
                filtros.datoS1 = Fe_In1;
                filtros.datoS2 = Fe_Fi1;
                filtros.datoS3 = Fe_In2;
                filtros.datoS4 = Fe_Fi2;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["Fe_In1"] = Fe_In1;
                ViewData["Fe_Fi1"] = Fe_Fi1;
                ViewData["Fe_In2"] = Fe_In2;
                ViewData["Fe_Fi2"] = Fe_Fi2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Us_Agente;
                bus.id2 = ID_Cli2Rel;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesconsultasresumenempresasrelacionadas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesConsultasResumenEmpresasRelacionadasListado>(dat.obj.ToString());

                    ViewData["Fe_In1"] = lis.Fe_In1;
                    ViewData["Fe_Fi1"] = lis.Fe_Fi1;
                    ViewData["Fe_In2"] = lis.Fe_In2;
                    ViewData["Fe_Fi2"] = lis.Fe_Fi2;

                    ViewData["ID_Us_Agente"] = lis.ID_Us_Agente;
                    ViewData["Agente"] = lis.Agente;
                    ViewData["ID_Cli2Rel"] = lis.ID_Cli2Rel;
                    ViewData["EmpRel"] = lis.EmpRel;


                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesconsultasresumenempresasrelacionadas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesConsultasResumenEmpresasRelacionadasListadoViewModel(lis);

            return View("ClientesConsultas-ResumenEmpresasRelacionadas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/consultas/resumen-empresas-relacionadas/exportar")]
        public async Task<IActionResult> ClientesConsultasResumenEmpresasRelacionadasExportar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFichero ret = new strFacturacionInformacionFichero();

            try
            {
                string Fe_In1 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In1");
                string Fe_Fi1 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi1");
                string Fe_In2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In2");
                string Fe_Fi2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi2");
                Int32 ID_Us_Agente = 0;
                try { ID_Us_Agente = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vAgente")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vemprelvin")); } catch { }

                strDatoS filtros = new strDatoS();
                filtros.datoS1 = Fe_In1;
                filtros.datoS2 = Fe_Fi1;
                filtros.datoS3 = Fe_In2;
                filtros.datoS4 = Fe_Fi2;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Us_Agente;
                bus.id2 = ID_Cli2Rel;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesconsultasresumenempresasrelacionadasexportar", Transporte, HttpContext);
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
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesconsultasresumenempresasrelacionadasexportar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            if (ret.Err.eserror) { return null; }

            Response.Headers.Add("Content-Disposition", "attachment; filename=" + ret.Nombre);

            return File(ret.Fichero, ret.TipoMime);

        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/consultas/resumen-empresas-relacionadas/buscar-agente")]
        public async Task<strListaErr> ClientesConsultasResumenEmpresasRelacionadasBuscarAgente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesconsultasresumenempresasrelacionadasbuscaragente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesconsultasresumenempresasrelacionadasbuscaragente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/consultas/resumen-empresas-relacionadas/buscar-empresa-relacionada")]
        public async Task<strListaErr> ClientesConsultasResumenEmpresasRelacionadasBuscarEmpresaRelacionada(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesconsultasresumenempresasrelacionadasbuscarempresarelacionada", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesconsultasresumenempresasrelacionadasbuscarempresarelacionada-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Consultas - Resumen Empresas Relacionadas por Agente"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/consultas/resumen-empresas-relacionadas-por-agente")]
        public async Task<IActionResult> ClientesConsultasResumenEmpresasRelacionadasPorAgente(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesConsultasResumenEmpresasRelacionadasPorAgenteListado lis = new strClientesConsultasResumenEmpresasRelacionadasPorAgenteListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                //string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                //string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                //string Cont = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cont");
                //Int32 ID_Tipo = 0;
                //try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }

                //string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                //string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                //string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");

                //string NIC = "";

                strDatoS filtros = new strDatoS();
                //filtros.datoI = ID_Tipo;
                //filtros.datoS1 = Mail;
                //filtros.datoS2 = Tel;
                //filtros.datoS3 = Cont;
                //filtros.datoS4 = NIC;

                //strDatoS masdatos = new strDatoS();
                //masdatos.datoS1 = Dir;
                //masdatos.datoS2 = Pob;
                //masdatos.datoS3 = Pro;
                //filtros.datoS5 = JsonConvert.SerializeObject(masdatos);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                //ViewData["Mail"] = Mail;
                //ViewData["Tel"] = Tel;
                //ViewData["Cont"] = Cont;
                //ViewData["ID_Tipo"] = ID_Tipo;

                //ViewData["Dir"] = Dir;
                //ViewData["Pro"] = Pro;
                //ViewData["Pob"] = Pob;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesconsultasresumenempresasrelacionadasporagente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesConsultasResumenEmpresasRelacionadasPorAgenteListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesconsultasresumenempresasrelacionadasporagente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesConsultasResumenEmpresasRelacionadasPorAgenteListadoViewModel(lis);

            return View("ClientesConsultas-ResumenEmpresasRelacionadasPorAgente", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/consultas/resumen-empresas-relacionadas-por-agente/exportar")]
        public async Task<IActionResult> ClientesConsultasResumenEmpresasRelacionadasPorAgenteExportar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFichero ret = new strFacturacionInformacionFichero();

            try
            {
                //Int32 ID_Cli2 = 0;
                //try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbuscarcli")); } catch { }
                //Int32 ID_Us = 0;
                //try { ID_Us = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "vbuscarusu")); } catch { }
                //string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                //string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");


                strDatoS filtros = new strDatoS();
                //filtros.datoI = ID_Cli2;
                //filtros.datoD = Convert.ToDecimal(ID_Us);
                //filtros.datoS1 = Fe_In;
                //filtros.datoS2 = Fe_Fi;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesconsultasresumenempresasrelacionadasporagenteexportar", Transporte, HttpContext);
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
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesconsultasresumenempresasrelacionadasporagenteexportar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            if (ret.Err.eserror) { return null; }

            Response.Headers.Add("Content-Disposition", "attachment; filename=" + ret.Nombre);

            return File(ret.Fichero, ret.TipoMime);

        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes")]
        public async Task<IActionResult> ClientesDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesdashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesdashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("ClientesDashboard", viewModel);
        }

        #endregion

        #region "Modulo Clienes - Por revisar"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/por-revisar")]
        public async Task<IActionResult> ClientesClientesPorRevisarListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesClientesPorRevisarListado lis = new strClientesClientesPorRevisarListado();

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
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesporrevisarlistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesClientesPorRevisarListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesporrevisarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClientesPorRevisarListadoViewModel(lis);

            return View("ClientesPorRevisar", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/por-revisar/revisado")]
        public async Task<transportout> ClientesClientesPorRevisarRevisado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Rev = 0;
                try { ID_Rev = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rev")); } catch { }
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Rev;
                bus.datoS = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesporrevisarrevisado", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesporrevisarrevisado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion


        #region "Modulo Clientes - Clientes"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes")]
        public async Task<IActionResult> ClientesListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesClientesListado lis = new strClientesClientesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string Cont = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cont");
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }

                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");

                string NIC = "";

                strDatoS filtros = new strDatoS();
                filtros.datoI = ID_Tipo;
                filtros.datoS1 = Mail;
                filtros.datoS2 = Tel;
                filtros.datoS3 = Cont;
                filtros.datoS4 = NIC;

                strDatoS masdatos = new strDatoS();
                masdatos.datoS1 = Dir;
                masdatos.datoS2 = Pob;
                masdatos.datoS3 = Pro;
                filtros.datoS5 = JsonConvert.SerializeObject(masdatos);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["buscar"] = buscar;
                ViewData["Mail"] = Mail;
                ViewData["Tel"] = Tel;
                ViewData["Cont"] = Cont;
                ViewData["ID_Tipo"] = ID_Tipo;

                ViewData["Dir"] = Dir;
                ViewData["Pro"] = Pro;
                ViewData["Pob"] = Pob;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 50;
                bus.pagina = pag;
                bus.obj = filtros;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesClientesListado>(dat.obj.ToString());
               }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClientesListadoViewModel(lis);

            return View("Clientes", viewModel);
        }


        #endregion




        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Nuevo cliente"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/nuevo-cliente")]
        public async Task<IActionResult> ClientesNuevo(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesClienteNuevo lis = new strClientesClienteNuevo();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientenuevo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesClienteNuevo>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientenuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteNuevoViewModel(lis);

            return View("Cliente-Nuevo", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/nuevo-cliente/generar")]
        public async Task<transportout> ClientesNuevo_Generar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");
                string EmpFis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EmpFis");
                string DirCom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "DirCom");
                string PaiCom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PaiCom");
                string ProCom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ProCom");
                string CPCom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CPCom");
                string PobCom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PobCom");
                bool MismaDirDis = false;
                try { MismaDirDis = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "MismaDirDis")); } catch { }
                string DirFis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "DirFis");
                string PaiFis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PaiFis");
                string ProFis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ProFis");
                string CPFis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CPFis");
                string PobFis = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PobFis");
                string NIF = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIF");
                bool REQ = false;
                try { REQ = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "REQ")); } catch { }
                bool Mailing = false;
                try { Mailing = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mailing")); } catch { }
                Int32 ID_Us_Agente = 0;
                try { ID_Us_Agente = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Agente")); } catch { }
                string Actividad = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Actividad");
                bool FacViaMail = false;
                try { FacViaMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "FacViaMail")); } catch { }
                string Conctacto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Conctacto");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string Movil = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Movil");
                string Mail1 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail1");
                string Mail2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail2");
                string Web = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Web");
                Int32 ID_TipCli = 0;
                try { ID_TipCli = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipCli")); } catch { }
                Int32 AxDias = 0;
                try { AxDias = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipCli")); } catch { }
                Int32 Dia = 0;
                try { Dia = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Rel")); } catch { }
                Int32 ID_For = 0;
                try { ID_For = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_For")); } catch { }
                string Cue = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cue");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                string ObsAdm = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsAdm");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesClienteNuevoGenerar bus = new strClientesClienteNuevoGenerar();
                bus.Emp = Emp;
                bus.EmpFis = EmpFis;
                bus.DirCom = DirCom;
                bus.PaiCom = PaiCom;
                bus.ProCom = ProCom;
                bus.CPCom = CPCom;
                bus.PobCom = PobCom;
                bus.DifDirDis = !MismaDirDis;
                bus.DirFis = DirFis;
                bus.PaiFis = PaiFis;
                bus.ProFis = ProFis;
                bus.CPFis = CPFis;
                bus.PobFis = PobFis;
                bus.NIF = NIF;
                bus.REQ = REQ;
                bus.Mailing = Mailing;
                bus.ID_Us_Agente = ID_Us_Agente;
                bus.Actividad = Actividad;
                bus.FacViaMail = FacViaMail;
                bus.Conctacto = Conctacto;
                bus.Tel = Tel;
                bus.Movil = Movil;
                bus.Mail1 = Mail1;
                bus.Mail2 = Mail2;
                bus.Web = Web;
                bus.ID_TipCli = ID_TipCli;
                bus.AxDias = AxDias;
                bus.Dia = Dia;
                bus.ID_Cli2Rel = ID_Cli2Rel;
                bus.ID_For = ID_For;
                bus.Cue = Cue;
                bus.Obs = Obs;
                bus.ObsAdm = ObsAdm;

                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientenuevogenerar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientenuevogenerar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/nuevo-cliente/buscaragente")]
        public async Task<strListaErr> ClientesClienteNuevolBuscarAgente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientenuevobuscaragente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientenuevobuscaragente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/nuevo-cliente/buscarcliente")]
        public async Task<strListaErr> ClientesClienteNuevolBuscarCliente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientenuevobuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientenuevobuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/nuevo-cliente/direccioncambiopais")]
        public async Task<strListaDatoSErr> ClientesClienteNuevoDireccionCambioPais(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientenuevodireccioncambiopais", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
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
        [Route("modulo-clientes/nuevo-cliente/direccioncambioprovincia")]
        public async Task<strListaDatoSErr> ClientesClienteNuevoDireccionCambioProvincia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientenuevodireccioncambioprovincia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientenuevodireccioncambioprovincia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/nuevo-cliente/direccioncambiocp")]
        public async Task<strListaDatoSErr> DireccionCambioCP(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                dato.datoS3 = CP;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientenuevodireccioncambiocp", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientenuevodireccioncambiocp-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Bloqueo"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/bloqueo")]
        public async Task<IActionResult> ClienteBloqueo(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_Bloqueo lis = new strClientesCliente_Bloqueo();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientebloqueo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_Bloqueo>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientebloqueo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteBloqueoViewModel(lis);

            return View("Cliente-Bloqueo", viewModel);
        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/bloqueo/cliente-bloquear")]
        public async Task<transportout> ClientesCliente_Bloqueo_ClienteBloquear(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string MotBaj = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "MotBaj");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoS = MotBaj;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientebloqueobloquear", Transporte, HttpContext);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientebloqueobloquear-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/bloqueo/cliente-desbloquear")]
        public async Task<transportout> ClientesCliente_Bloqueo_ClienteDesbloquear(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientebloqueodesbloquear", Transporte, HttpContext);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientebloqueodesbloquear-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/bloqueo/cliente-baja")]
        public async Task<transportout> ClientesCliente_Bloqueo_ClienteBaja(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientebloqueobaja", Transporte, HttpContext);

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientebloqueobaja-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion

        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Correspondencia"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/correspondencia")]
        public async Task<IActionResult> ClientesCliente_Correspondencia(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_Correspondencia lis = new strClientesCliente_Correspondencia();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientecorrespondencia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_Correspondencia>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientecorrespondencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteCorrespondenciaViewModel(lis);

            return View("Cliente-Correspondencia", viewModel);
        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/correspondencia/guardar")]
        public async Task<transportout> ClientesCliente_CorrespondenciaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                bool FacEMail = false;
                try { FacEMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "FacEMail")); } catch { }
                bool CorrFac = false;
                try { CorrFac = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CorrFac")); } catch { }
                string MRC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ops");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesCliente_Correspondencia bus = new strClientesCliente_Correspondencia();
                bus.CorrFac = CorrFac;
                bus.FacEMail = FacEMail;
                strDatoS[] fil = new strDatoS[1];
                fil[0] = new strDatoS();
                fil[0].datoI = ID_Cli2;
                fil[0].datoS1 = MRC;
                bus.Facs = fil;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientecorrespondenciaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientecorrespondenciaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"


        #endregion


        #endregion

        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Tags"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/tags")]
        public async Task<IActionResult> ClientesCliente_Tags(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_Tags lis = new strClientesCliente_Tags();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoS = buscar;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientetags", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_Tags>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientetags-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteTagsViewModel(lis);

            return View("Cliente-Tags", viewModel);
        }

        #endregion

        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/tags/nuevo-tag")]
        public async Task<transportout> ModuloClientesExtranetController_ClientesCliente_TagsAddTag(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Tag = 0;
                try { ID_Tag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tag")); } catch { }
                string Tag = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tag");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoS1 = Tag;
                bus.datoS2 = ID_Tag.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientetagsaddtag", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientetagsaddtag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/tags/del-tag")]
        public async Task<transportout> ClientesCliente_TagsDelTag(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_RelT = 0;
                try { ID_RelT = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_RelT")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoS = ID_RelT.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientetagsdeltag", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientetagsdeltag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/tags/buscartag")]
        public async Task<strListaErr> ClientesCliente_Tags_BuscarTag(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientetagsbuscartag", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientetagsbuscartag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion

        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Fiscal"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal")]
        public async Task<IActionResult> ClienteFiscal(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_Fiscal lis = new strClientesCliente_Fiscal();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientefiscal", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_Fiscal>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscal-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteFiscalViewModel(lis);

            return View("Cliente-Fiscal", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/cuenta-nueva")]
        public async Task<transportout> ClientesCliente_Fiscal_CuentaNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Cue = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cue");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoS = Cue;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentanueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentanueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/cuenta-eli")]
        public async Task<transportout> ClientesCliente_Fiscal_CuentaEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Cue);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentaeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/cuenta-pred")]
        public async Task<transportout> ClientesCliente_Fiscal_CuentaPred(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Cue);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentapred", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentapred-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/cuenta-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_CuentaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }
                string Cue = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cue");
                string DirBan = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "DirBan");
                string ObsBan = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsBan");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Cue);
                bus.datoS1 = Cue;
                bus.datoS2 = DirBan;
                bus.datoS3 = ObsBan;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/cambio-cuenta")]
        public async Task<transportout> ClientesCliente_Fiscal_CuentaRecibosCambioCuenta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Cue);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcuentareciboscambiocuenta", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuentareciboscambiocuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/dia-nuevo")]
        public async Task<transportout> ClientesCliente_Fiscal_DiaNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 Dia = 0;
                try { Dia = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(Dia);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscaldianuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscaldianuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/dia-eliminar")]
        public async Task<transportout> ClientesCliente_Fiscal_DiaEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 Dia = 0;
                try { Dia = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dia")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(Dia);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscaldiaeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscaldiaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/riesgo-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_RiesgoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string RieMax = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "RieMax");
                string Poliza = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Poliza");
                bool Asegurado = false;
                try { Asegurado = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Asegurado")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoS1 = RieMax;
                bus.datoS2 = Poliza;
                bus.datoB = Asegurado;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalriesgoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalriesgoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/razon-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_RazonGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Razon = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Razon");
                string NIF = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIF");
                string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoS1 = Razon;
                bus.datoS2 = Emp;
                bus.datoS3 = NIF;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalrazonguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalrazonguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/cccli-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_CCCliGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string CCCli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CCCli");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoS = CCCli;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalcccliguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcccliguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/formas-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_FormasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string id_for = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id_for");
                Int32 id_forpred = 0;
                try { id_forpred = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id_forpred")); } catch { }
                string perm = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "perm");
                string id_cue = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id_cue");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(id_forpred);
                bus.datoS1 = id_for;
                bus.datoS2 = perm;
                bus.datoS3 = id_cue;

                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalformasguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalformasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/facturacion-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_FacturacionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 AxDias = 0;
                try { AxDias = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "AxDias")); } catch { }
                Int32 Frac = 0;
                try { Frac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Frac")); } catch { }
                Int32 ID_Us_MailEnNom = 0;
                try { ID_Us_MailEnNom = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_MailEnNom")); } catch { }
                bool PostPago = false;
                try { PostPago = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "PostPago")); } catch { }
                bool FacDif = false;
                try { FacDif = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "FacDif")); } catch { }
                bool FacEMail = false;
                try { FacEMail = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "FacEMail")); } catch { }
                string ObsAdm = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsAdm");
                Int32 ID_Us_Gest = 0;
                try { ID_Us_Gest = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Gest")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesCliente_Fiscal_Facturacion_Guardar bus = new strClientesCliente_Fiscal_Facturacion_Guardar();
                bus.ID_Cli2 = ID_Cli2;
                bus.AxDias = AxDias;
                bus.Frac = Frac;
                bus.ID_Us_MailEnNom = ID_Us_MailEnNom;
                bus.PostPago = PostPago;
                bus.FacDif = FacDif;
                bus.FacEMail = FacEMail;
                bus.ObsAdm = ObsAdm;
                bus.ID_Us_Gest = ID_Us_Gest;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalfacturacionguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalfacturacionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/tipoiva-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_TipoIVAGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_TipoIVA = 0;
                try { ID_TipoIVA = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoIVA")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_TipoIVA);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscaltipoivaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscaltipoivaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/req-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_REQGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                bool REQ = false;
                try { REQ = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "REQ")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoB = REQ;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalreqguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalreqguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/moneda-guardar")]
        public async Task<transportout> ClientesCliente_Fiscal_MonedaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Mon = 0;
                try { ID_Mon = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mon")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Mon);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientefiscalmonedaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalmonedaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/fiscal/buscarusuario")]
        public async Task<strListaErr> ClientesCliente_Fiscal_FiscalBuscarUsuario(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientefiscalbuscarusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalbuscarusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/fiscal/cuenta")]
        public async Task<strDatoS> ClientesCliente_Fiscal_Cuenta(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cue = 0;
                try { ID_Cue = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cue")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Cue);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientefiscalcuenta", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscalcuenta-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Clientes relacionados - Cobros pendientes"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/cobros-pendientes")]
        public async Task<IActionResult> ClienteClientesRelacionadosCobros(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_CobrosPendientes lis = new strClientesCliente_ClientesRelacionados_CobrosPendientes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadoscobrospendientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_CobrosPendientes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadoscobrospendientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosCobrosPendientesViewModel(lis);

            return View("Cliente-ClientesRelacionados-Cobros", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/cobros-pendientes/guardar")]
        public async Task<transportout> ClienteClientesRelacionadosCobrosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Ops = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ops");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoS = Ops;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadoscobrospendientesguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadoscobrospendientesguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Clientes relacionados - Herencia"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/herencia")]
        public async Task<IActionResult> ClienteClientesRelacionadosHerencia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Herencia lis = new strClientesCliente_ClientesRelacionados_Herencia();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosherencia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Herencia>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosherencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosHerenciaViewModel(lis);

            return View("Cliente-ClientesRelacionados-Herencia", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/herencia-guardar")]
        public async Task<transportout> ClientesCliente_ClientesRelacionados_HerenciaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                bool CobEnEspera = false;
                try { CobEnEspera = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CobEnEspera")); } catch { }
                bool EnvFac = false;
                try { EnvFac = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvFac")); } catch { }
                bool Facturar = false;
                try { Facturar = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Facturar")); } catch { }
                bool ForPag = false;
                try { ForPag = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ForPag")); } catch { }
                bool Precios = false;
                try { Precios = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios")); } catch { }
                bool Propagar = false;
                try { Propagar = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Propagar")); } catch { }
                bool Tags = false;
                try { Tags = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tags")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesCliente_ClientesRelacionados_Herencia bus = new strClientesCliente_ClientesRelacionados_Herencia();
                bus.ID_Cli2 = ID_Cli2;
                bus.CobEnEspera = CobEnEspera;
                bus.EnvFac = EnvFac;
                bus.Facturar = Facturar;
                bus.ForPag = ForPag;
                bus.Precios = Precios;
                bus.Propagar = Propagar;
                bus.Tags = Tags;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteclientesRelacionadosherenciaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesRelacionadosherenciaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Relacionado con"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cliente-relacionado-con")]
        public async Task<IActionResult> ClientesCliente_RelacionadoCon(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_RelacionadoCon lis = new strClientesCliente_RelacionadoCon();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienterelacionadocon", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_RelacionadoCon>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienterelacionadocon-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteRelacionadoConViewModel(lis);

            return View("Cliente-ClienteRelacionadoCon", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cliente-relacionado-con/vincular")]
        public async Task<transportout> ClientesCliente_RelacionadoConVincular(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cli2Rel = 0;
                try { ID_Cli2Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Rel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Cli2Rel);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienterelacionadoconvincular", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienterelacionadoconvincular-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cliente-relacionado-con/eliminar-vinculacion")]
        public async Task<transportout> ClientesCliente_RelacionadoConEliminarVinculacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }
                string Motivo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Motivo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Rel);
                bus.datoS = Motivo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienterelacionadoeliminarvinculacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienterelacionadoeliminarvinculacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cliente-relacionado-con/guardar-vinculacion")]
        public async Task<transportout> ClientesCliente_RelacionadoConGuardarVinculacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }
                bool CobEnEspera = false;
                try { CobEnEspera = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CobEnEspera")); } catch { }
                bool EnvFac = false;
                try { EnvFac = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvFac")); } catch { }
                bool Facturar = false;
                try { Facturar = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Facturar")); } catch { }
                bool ForPag = false;
                try { ForPag = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ForPag")); } catch { }
                bool Precios = false;
                try { Precios = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesCliente_ClientesRelacionados_Herencia bus = new strClientesCliente_ClientesRelacionados_Herencia();
                bus.ID_Cli2 = ID_Cli2;
                bus.CobEnEspera = CobEnEspera;
                bus.EnvFac = EnvFac;
                bus.Facturar = Facturar;
                bus.ForPag = ForPag;
                bus.Precios = Precios;
                bus.ID_Rel = ID_Rel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienterelacionadoguardarvinculacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienterelacionadoguardarvinculacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cliente-relacionado-con/buscar-cliente")]
        public async Task<strListaErr> ClientesCliente_RelacionadoConBuscarCliente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienterelacionadoconbuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienterelacionadoconbuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cliente-relacionado-con/buscartag")]
        public async Task<strListaErr> ClientesCliente_ClientesRelacionados_Clientes_BuscarTagXX(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclientebuscartag", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclientebuscartag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Módulo Clientes - Configuración"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/configuracion")]
        public async Task<IActionResult> ClientesConfiguracion(string paramsin)
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

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/configuracion/preferencias")]
        public async Task<IActionResult> ClientesConfiguracionPreferencias(string paramsin)
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

            return View("Configuracion-Preferencias", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Acceso Web"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/acceso-web")]
        public async Task<IActionResult> ClienteAccesoWeb(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_AccesoWebListado lis = new strClientesCliente_AccesoWebListado();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteaccesoweb", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_AccesoWebListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteaccesoweb-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteAccesoWebViewModel(lis);

            return View("Cliente-AccesoWeb", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/acceso-web/nuevo-usuario")]
        public async Task<transportout> ClientesCliente_AccesoWeb_NuevoUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }
                Int32 ID_Tv = 0;
                try { ID_Tv = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tv")); } catch { }
                string Nom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Nom");
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Mail);
                bus.datoS1 = Nom;
                bus.datoS2 = NIC;
                bus.datoS3 = ID_Tv.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteaccesowebnuevousuario", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteaccesowebnuevousuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion




        #region "Modulo Clientes - Clientes - Cliente - Precios especiales (Resume)"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales")]
        public async Task<IActionResult> ClientePreciosEspeciales(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_PreciosEspeciales lis = new strClientesCliente_PreciosEspeciales();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                //var client = new HttpClient();
                //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/moduloclientes/extranetclientesclientepreciosespeciales/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                //dat = JsonConvert.DeserializeObject<TransportOut>(response);
                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespeciales", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_PreciosEspeciales>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespeciales-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClientePreciosEspecialesViewModel(lis);

            return View("Cliente-PreciosEspeciales", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion

        #region "Recuperar datos - Get"


        #endregion

        #endregion

        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Precios especiales - Servicios"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/servicios")]
        public async Task<IActionResult> ClientePreciosEspecialesServicios(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_PreciosEspeciales lis = new strClientesCliente_PreciosEspeciales();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialesservicios", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_PreciosEspeciales>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialesservicios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClientePreciosEspecialesViewModel(lis);

            return View("Cliente-PreciosEspeciales-Servicios", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/servicios/guardar")]
        public async Task<transportout> ClientePreciosEspecialesGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Precios = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoS = Precios;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosserviciosguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientepreciosespecialespreciosserviciosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/servicios/listado-precios")]
        public async Task<strListadoConPaginador> ClientePreciosEspecialesListadoPrecios(string paramsin)
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

                Int32 num = 30;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscarserv = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarserv");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscarserv;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosservicioslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialespreciosservicioslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Precios especiales - Articulos"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos")]
        public async Task<IActionResult> ClientesCliente_PreciosEspecialesArticulos(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_PreciosEspeciales lis = new strClientesCliente_PreciosEspeciales();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialesarticulos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_PreciosEspeciales>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialesarticulos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClientePreciosEspecialesViewModel(lis);

            return View("Cliente-PreciosEspeciales-Articulos", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos/guardar")]
        public async Task<transportout> ClientesClientePreciosEspecialesArticulosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Precios = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoS = Precios;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosarticulosguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientepreciosespecialespreciosarticulosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos/listado-precios")]
        public async Task<strListadoConPaginador> ClientesClientePreciosEspecialesPreciosArticulosListado(string paramsin)
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

                Int32 num = 30;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscarart = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarart");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscarart;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosarticuloslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialespreciosarticuloslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Precios especiales - Articulos Categorias"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos-categorias")]
        public async Task<IActionResult> ClientesCliente_PreciosEspecialesArticulosCategorias(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_PreciosEspeciales lis = new strClientesCliente_PreciosEspeciales();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialesarticuloscategorias", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_PreciosEspeciales>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialesarticuloscategorias-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClientePreciosEspecialesViewModel(lis);

            return View("Cliente-PreciosEspeciales-ArticulosCategorias", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos-categorias/guardar")]
        public async Task<transportout> ClientesClientePreciosEspecialesArticulosCategoriasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Precios = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoS = Precios;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosarticuloscategoriasguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientepreciosespecialespreciosarticuloscategoriasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos-categorias/listado-precios")]
        public async Task<strListadoConPaginador> ClientesClientePreciosEspecialesPreciosArticulosCategoriasListado(string paramsin)
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

                Int32 num = 30;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscarartcat = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarartcat");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscarartcat;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosarticuloscategoriaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialespreciosarticuloscategoriaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Clientes - Cliente - Precios especiales - Articulos Familias"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos-familias")]
        public async Task<IActionResult> ClientesCliente_PreciosEspecialesArticulosFamilias(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_PreciosEspeciales lis = new strClientesCliente_PreciosEspeciales();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialesarticulosfamilias", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_PreciosEspeciales>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialesarticulosfamilias-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClientePreciosEspecialesViewModel(lis);

            return View("Cliente-PreciosEspeciales-ArticulosFamilias", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos-familias/guardar")]
        public async Task<transportout> ClientesClientePreciosEspecialesArticulosFamiliasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Precios = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoS = Precios;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosarticulosfamiliasguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientepreciosespecialespreciosarticulosfamiliasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/precios-especiales/articulos-familias/listado-precios")]
        public async Task<strListadoConPaginador> ClientesClientePreciosEspecialesPreciosArticulosFamiliasListado(string paramsin)
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

                Int32 num = 30;


                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                string buscarartfam = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarartfam");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscarartfam;
                dato.id = ID_Cli2;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientepreciosespecialespreciosarticulosfamiliaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientepreciosespecialespreciosarticulosfamiliaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Cliente"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente")]
        public async Task<IActionResult> ClienteGeneral(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_General lis = new strClientesCliente_General();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneral", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_General>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneral-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteGeneralViewModel(lis);

            return View("Cliente-General", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/imprimir-direccion")]
        public async Task<IActionResult> ClientesCliente_GeneralDireccionFichero(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strFacturacionInformacionFichero ret = new strFacturacionInformacionFichero();

            try
            {

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_Idi);

                string Tipo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tipo");
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato
                {
                    datoI = ID_Dir,
                    datoS = Tipo
                };
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneraldireccionfichero", Transporte, HttpContext);
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
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientegeneraldireccionfichero-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return null;

        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/nueva-direccion")]
        public async Task<transportout> ClientesCliente_General_DireccionNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                Int32 ID_Centro = 0;
                try { ID_Centro = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro")); } catch { }
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                string Horario = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Horario");
                string EmpAux = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EmpAux");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesClienteDireccionDetalle bus = new strClientesClienteDireccionDetalle();
                bus.ID_Cli2 = ID_Cli2;
                bus.ID_Dir = ID_Dir;
                bus.ID_Centro = ID_Centro;
                bus.Dir = Dir;
                bus.Pai = Pai;
                bus.Pro = Pro;
                bus.CP = CP;
                bus.Pob = Pob;
                bus.Obs = Obs;
                bus.Horario = Horario;
                bus.EmpAux = EmpAux;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraldireccionnueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldireccionnueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/guardar-direccion")]
        public async Task<transportout> ClientesCliente_General_DireccionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                Int32 ID_Centro = 0;
                try { ID_Centro = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro")); } catch { }
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                string Horario = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Horario");
                string EmpAux = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EmpAux");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesClienteDireccionDetalle bus = new strClientesClienteDireccionDetalle();
                bus.ID_Cli2 = ID_Cli2;
                bus.ID_Dir = ID_Dir;
                bus.ID_Centro = ID_Centro;
                bus.Dir = Dir;
                bus.Pai = Pai;
                bus.Pro = Pro;
                bus.CP = CP;
                bus.Pob = Pob;
                bus.Obs = Obs;
                bus.Horario = Horario;
                bus.EmpAux = EmpAux;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraldireccionguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldireccionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/direccion-geo-save")]
        public async Task<transportout> ClientesCliente_General_DireccionGeoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                string Lat = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Lat");
                string Lon = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Lon");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Dir);
                bus.datoS1 = Lat;
                bus.datoS2 = Lon;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraldirecciongeoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldirecciongeoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cambiar-direccion-fiscal")]
        public async Task<transportout> ClientesCliente_General_DireccionFiscalCambiar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Dir);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraldireccionfiscalcambiar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldireccionfiscalcambiar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cambiar-direccion-comercial")]
        public async Task<transportout> ClientesCliente_General_DireccionComercialCambiar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Dir);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraldireccioncomercialcambiar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldireccioncomercialcambiar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/cambiar-direccion-documentacion")]
        public async Task<transportout> ClientesCliente_General_DireccionDocumentosCambiar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }
                bool Sel = false;
                try { Sel = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Sel")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Dir);
                bus.datoB = Sel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraldirecciondocumentoscambiar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldirecciondocumentoscambiar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/direccion-eliminar")]
        public async Task<transportout> ClientesCliente_General_DireccionEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Dir);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraldireccioneliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldireccioneliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/nuevo-tel")]
        public async Task<transportout> ClientesCliente_General_TelefonoNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string ObsTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsTel");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoS1 = Tel;
                bus.datoS2 = ObsTel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraltelefononuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraltelefononuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/guardar-tel")]
        public async Task<transportout> ClientesCliente_General_TelefonoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string ObsTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsTel");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Tel);
                bus.datoS1 = Tel;
                bus.datoS2 = ObsTel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraltelefonoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraltelefonoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/eliminar-tel")]
        public async Task<transportout> ClientesCliente_General_TelefonoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Tel);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraltelefonoeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraltelefonoeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/pred-tel")]
        public async Task<transportout> ClientesCliente_General_TelefonoPred(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Tel);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneraltelefonopred", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraltelefonopred-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/nuevo-mail")]
        public async Task<transportout> ClientesCliente_General_MailNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string ObsMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsMail");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoS1 = Mail;
                bus.datoS2 = ObsMail;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralmailnuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralmailnuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/guardar-mail")]
        public async Task<transportout> ClientesCliente_General_MailGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string ObsMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsMail");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Mail);
                bus.datoS1 = Mail;
                bus.datoS2 = ObsMail;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralmailguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralmailguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/eliminar-mail")]
        public async Task<transportout> ClientesCliente_General_MailEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Mail);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralmaileliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralmaileliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/sel-mail-comercial")]
        public async Task<transportout> ClientesCliente_General_MailComercial(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }
                bool Sel = false;
                try { Sel = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Sel")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Mail);
                bus.datoB = Sel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralmailcomercial", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralmailcomercial-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/sel-mail-facturas")]
        public async Task<transportout> ClientesCliente_General_MailFacturas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }
                bool Sel = false;
                try { Sel = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Sel")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Mail);
                bus.datoB = Sel;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralmailfacturas", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralmailfacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/nueva-web")]
        public async Task<transportout> ClientesCliente_General_WebNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Web = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Web");
                string ObsWeb = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsWeb");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoS1 = Web;
                bus.datoS2 = ObsWeb;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralwebnueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralwebnueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/guardar-web")]
        public async Task<transportout> ClientesCliente_General_WebGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Web = 0;
                try { ID_Web = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Web")); } catch { }
                string Web = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Web");
                string ObsWeb = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsWeb");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Web);
                bus.datoS1 = Web;
                bus.datoS2 = ObsWeb;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralwebguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralwebguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/eliminar-web")]
        public async Task<transportout> ClientesCliente_General_WebEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Web = 0;
                try { ID_Web = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Web")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Web);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralwebeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralwebeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/nuevo-centro")]
        public async Task<transportout> ClientesCliente_General_CentroNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Centro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Centro");
                string ObsCentro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsCentro");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoS1 = Centro;
                bus.datoS2 = ObsCentro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralcentronuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralcentronuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/guardar-centro")]
        public async Task<transportout> ClientesCliente_General_CentroGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Centro = 0;
                try { ID_Centro = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro")); } catch { }
                string Centro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Centro");
                string ObsCentro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsCentro");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Centro);
                bus.datoS1 = Centro;
                bus.datoS2 = ObsCentro;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralcentroguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralcentroguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/eliminar-centro")]
        public async Task<transportout> ClientesCliente_General_CentroEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Centro = 0;
                try { ID_Centro = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Centro);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientegeneralcentroeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralcentroeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion



        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/direccion")]
        public async Task<strClientesClienteDireccionDetalle> ClientesCliente_General_Direccion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesClienteDireccionDetalle ret = new strClientesClienteDireccionDetalle();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Dir);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneraldireccion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strClientesClienteDireccionDetalle>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldireccion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/direccion-geo")]
        public async Task<strDatoS> ClientesCliente_General_DireccionGeo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Dir = 0;
                try { ID_Dir = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Dir")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Dir);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneraldirecciongeo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraldirecciongeo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/telefono")]
        public async Task<strDatoS> ClientesCliente_General_Telefono(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Tel = 0;
                try { ID_Tel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Tel);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneraltelefono", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneraltelefono-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/mail")]
        public async Task<strDatoS> ClientesCliente_General_Mail(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Mail = 0;
                try { ID_Mail = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Mail")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Mail);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneralmail", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralmail-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/web")]
        public async Task<strDatoS> ClientesCliente_General_Web(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Web = 0;
                try { ID_Web = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Web")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Web);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneralweb", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralweb-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/centro")]
        public async Task<strDatoS> ClientesCliente_General_Centro(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Centro = 0;
                try { ID_Centro = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Centro")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Centro);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneralcentro", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientegeneralcentro-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/direccioncambiopais")]
        public async Task<strListaDatoSErr> ClientesCliente_General_DireccionCambioPais(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneraldireccioncambiopais", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-comun-extranetclientesclientegeneraldireccioncambiopais-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/direccioncambioprovincia")]
        public async Task<strListaDatoSErr> ClientesCliente_General_DireccionCambioProvincia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneraldireccioncambioprovincia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientegeneraldireccioncambioprovincia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/cliente/cliente/direccioncambiocp")]
        public async Task<strListaDatoSErr> ClientesCliente_General_DireccionCambioCP(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Pai;
                dato.datoS2 = Pro;
                dato.datoS3 = CP;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientegeneraldireccioncambiocp", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes-extranetclientesclientegeneraldireccioncambiocp-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        
        #endregion


        #endregion


        #endregion



        // **********************************+


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/contactos")]
        public async Task<IActionResult> ClienteContactos(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_Fiscal lis = new strClientesCliente_Fiscal();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientefiscal", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_Fiscal>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscal-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteFiscalViewModel(lis);

            return View("Cliente-Contactos", viewModel);
        }


        #region "Ajax - Relacionado con"

        #region "Guardar - Post"



        #endregion


        #region "Recuperar datos - Get"



        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Situacion"

        #region "Modulo Clientes - Cliente - Situacion - Resumen"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion")]
        public async Task<IActionResult> ClienteSituacion(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_Fiscal lis = new strClientesCliente_Fiscal();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientefiscal", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_Fiscal>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientefiscal-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteFiscalViewModel(lis);

            return View("Cliente-Situacion", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Situacion - Cobros Pendientes"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/cobros-pendientes")]
        public async Task<IActionResult> ClienteSituacion_CobrosPendientes(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_CobrosPendientes lis = new strClientesCliente_ClientesSituacion_CobrosPendientes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacioncobrospendientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_CobrosPendientes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacioncobrospendientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionCobrosPendientesViewModel(lis);

            return View("Cliente-Situacion-Cobros-Pendientes", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Situacion - Cobros"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/cobros")]
        public async Task<IActionResult> ClienteSituacion_Cobros(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_Cobros lis = new strClientesCliente_ClientesSituacion_Cobros();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacioncobros", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_Cobros>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacioncobros-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionCobrosViewModel(lis);

            return View("Cliente-Situacion-Cobros", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Situacion - Presupuestos"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/presupuestos")]
        public async Task<IActionResult> ClienteSituacion_Presupuestos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_Presupuestos lis = new strClientesCliente_ClientesSituacion_Presupuestos();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacionpresupuestos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_Presupuestos>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacionpresupuestos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionPresupuestosViewModel(lis);

            return View("Cliente-Situacion-Presupuestos", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/presupuestos/presupuesto")]
        public async Task<strClientesCliente_ClientesSituacion_PresupuestosPresupuetoDetalles> ClientesCliente_ClientesSituacion_PresupuestosPresupuestoDetalles(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_PresupuestosPresupuetoDetalles ret = new strClientesCliente_ClientesSituacion_PresupuestosPresupuetoDetalles();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Pres2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacionpresupuestospresupuestodetalles", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_PresupuestosPresupuetoDetalles>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacionpresupuestospresupuestodetalles-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        #endregion


        #endregion


        #endregion


        #region "Modulo Clientes - Cliente - Situacion - Solicitudes"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/solicitudes")]
        public async Task<IActionResult> ClienteSituacion_Solicitudes(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_Solicitudes lis = new strClientesCliente_ClientesSituacion_Solicitudes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacionsolicitudes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_Solicitudes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacionsolicitudes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionSolicitudesViewModel(lis);

            return View("Cliente-Situacion-Solicitudes", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion


        #region "Modulo Clientes - Cliente - Situacion - Asistencias"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/asistencias")]
        public async Task<IActionResult> ClienteSituacion_Asistencias(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_Asistencias lis = new strClientesCliente_ClientesSituacion_Asistencias();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacionasistencias", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_Asistencias>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacionasistencias-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionAsistenciasViewModel(lis);

            return View("Cliente-Situacion-Asistencias", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Situacion - Mantenimientos"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/mantenimientos")]
        public async Task<IActionResult> ClienteSituacion_Mantenimientos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_Mantenimientos lis = new strClientesCliente_ClientesSituacion_Mantenimientos();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacionmantenimientos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_Mantenimientos>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacionmantenimientos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionMantenimientosViewModel(lis);

            return View("Cliente-Situacion-Mantenimientos", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Situacion - Facturas"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/facturas")]
        public async Task<IActionResult> ClienteSituacion_Facturas(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_Facturas lis = new strClientesCliente_ClientesSituacion_Facturas();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacionfacturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_Facturas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacionfacturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionFacturasViewModel(lis);

            return View("Cliente-Situacion-Facturas", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion

        #region "Modulo Clientes - Cliente - Situacion - Albaranes"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/situacion/albaranes")]
        public async Task<IActionResult> ClienteSituacion_Albaranes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesSituacion_Albaranes lis = new strClientesCliente_ClientesSituacion_Albaranes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.numReg = 50;
                bus.pagina = pag;
                bus.id = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientessituacionalbaranes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesSituacion_Albaranes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientessituacionalbaranes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesSituacionAlbaranesViewModel(lis);

            return View("Cliente-Situacion-Albaranes", viewModel);
        }

        #endregion


        #endregion

        #endregion

        #region "Modulo Clientes - Cliente - Comercial"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/comercial")]
        public async Task<IActionResult> ClienteComercial(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_Comercial lis = new strClientesCliente_Comercial();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientecomercial", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_Comercial>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientecomercial-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteComercialViewModel(lis);

            return View("Cliente-Comercial", viewModel);
        }


        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/comercial/guardar-comercial")]
        public async Task<transportout> ClientesCliente_Comercial_ComercialGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");
                Int32 ID_Agente = 0;
                try { ID_Agente = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Agente")); } catch { }
                Int32 ID_TipCli2 = 0;
                try { ID_TipCli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipCli2")); } catch { }
                string Fe_Na = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Na");
                bool Mailing = false;
                try { Mailing = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mailing")); } catch { }
                Int32 ID_Tari2 = 0;
                try { ID_Tari2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tari2")); } catch { }
                string Dto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dto");
                string Comi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Comi");
                string Act = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Act");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesCliente_Comercial_Comercial bus = new strClientesCliente_Comercial_Comercial();
                bus.ID_Cli2 = ID_Cli2;
                bus.Emp = Emp;
                bus.ID_Agente = ID_Agente;
                bus.ID_TipCli2 = ID_TipCli2;
                bus.Fe_Na = Fe_Na;
                bus.Mailing = Mailing;
                bus.ID_Tari2 = ID_Tari2;
                bus.Dto = Dto;
                bus.Comi = Comi;
                bus.Act = Act;
                bus.Obs = Obs;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientecomercialcomercialguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientecomercialcomercialguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/comercial/guardar-programa-comercial")]
        public async Task<transportout> ClientesCliente_Comercial_ProgramasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 CopiasAlb = 0;
                try { CopiasAlb = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CopiasAlb")); } catch { }
                Int32 CopiasFac = 0;
                try { CopiasFac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CopiasFac")); } catch { }
                bool AlbNoVal = false;
                try { AlbNoVal = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "AlbNoVal")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(CopiasAlb);
                bus.datoS = CopiasFac.ToString();
                bus.datoB = AlbNoVal;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclientecomercialprogramasguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientecomercialprogramasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/comercial/buscaragente")]
        public async Task<strListaErr> ClientesCliente_Comercial_ComercialBuscarAgente(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclientecomercialbuscaragente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclientecomercialbuscaragente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Clientes - Clientes - Cliente - Clientes relacionados"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados")]
        public async Task<IActionResult> ClienteClientesRelacionadosResumen(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_CobrosPendientes lis = new strClientesCliente_ClientesRelacionados_CobrosPendientes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadoscobrospendientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_CobrosPendientes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadoscobrospendientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosCobrosPendientesViewModel(lis);

            return View("Cliente-ClientesRelacionados-Resumen", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"


        #endregion


        #endregion


        #endregion


        #region "Modulo Clientes - Clientes - Cliente - Clientes relacionados - Clientes"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/clientes")]
        public async Task<IActionResult> ClienteClientesRelacionadosClientes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Clientes lis = new strClientesCliente_ClientesRelacionados_Clientes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Clientes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosClientesViewModel(lis);

            return View("Cliente-ClientesRelacionados-Clientes", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/nuevo-tag")]
        public async Task<transportout> ClientesCliente_ClientesRelacionados_Cliente_AddTag(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }
                Int32 ID_Tag = 0;
                try { ID_Tag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tag")); } catch { }
                string Tag = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tag");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Rel);
                bus.datoS1 = Tag;
                bus.datoS2 = ID_Tag.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclienteaddtag", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclienteaddtag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/guardar-vinculacion")]
        public async Task<transportout> ClientesCliente_ClientesRelacionados_Cliente_GuardarRelacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }
                bool CobEnEspera = false;
                try { CobEnEspera = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CobEnEspera")); } catch { }
                bool EnvFac = false;
                try { EnvFac = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvFac")); } catch { }
                bool Facturar = false;
                try { Facturar = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Facturar")); } catch { }
                bool ForPag = false;
                try { ForPag = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ForPag")); } catch { }
                bool Precios = false;
                try { Precios = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios")); } catch { }
                bool Tags = false;
                try { Tags = Convert.ToBoolean(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tags")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strClientesCliente_ClientesRelacionados_Herencia bus = new strClientesCliente_ClientesRelacionados_Herencia();
                bus.ID_Cli2 = ID_Cli2;
                bus.CobEnEspera = CobEnEspera;
                bus.EnvFac = EnvFac;
                bus.Facturar = Facturar;
                bus.ForPag = ForPag;
                bus.Precios = Precios;
                bus.ID_Rel = ID_Rel;
                bus.Tags = Tags;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclienteaguardarrelacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclienteaguardarrelacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/eliminar-vinculacion")]
        public async Task<transportout> ClientesCliente_ClientesRelacionados_Cliente_EliminarRelacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }
                string Motivo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Motivo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Rel);
                bus.datoS = Motivo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclienteaeliminarrelacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclienteaeliminarrelacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/del-tag")]
        public async Task<transportout> ClientesCliente_ClientesRelacionados_Cliente_DelTag(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }
                Int32 ID_RelT = 0;
                try { ID_RelT = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_RelT")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Rel);
                bus.datoS = ID_RelT.ToString();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclientedeltag", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclientedeltag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/buscartag")]
        public async Task<strListaErr> ClientesCliente_ClientesRelacionados_Clientes_BuscarTag(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclientebuscartag", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclientebuscartag-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/clientes/cliente/relacionado")]
        public async Task<strClientesCliente_ClientesRelacionados_Cliente_RelacionadoCon> ClientesCliente_ClientesRelacionados_Cliente_RelacionadoCon(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Cliente_RelacionadoCon ret = new strClientesCliente_ClientesRelacionados_Cliente_RelacionadoCon();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Cli2;
                dato.datoD = Convert.ToDecimal(ID_Rel);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclienterelacionadocon", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Cliente_RelacionadoCon>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclienterelacionadocon-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion


        #region "Modulo Clientes - Clientes - Cliente - Clientes relacionados - Presupuestos"

        #region "Vistas"

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/presupuestos")]
        public async Task<IActionResult> ClienteClientesRelacionadosPresupuestos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Presupuestos lis = new strClientesCliente_ClientesRelacionados_Presupuestos();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadospresupuestos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Presupuestos>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadospresupuestos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosPresupuestosViewModel(lis);

            return View("Cliente-ClientesRelacionados-Presupuestos", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/presupuestos/guardar")]
        public async Task<transportout> ClienteClientesRelacionadosPresupuestosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                string Ops = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ops");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoS = Ops;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadospresupuestosguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadospresupuestosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/presupuestos/historialenvios")]
        public async Task<strListaDatoSErr> PresupuestosPresupuestosPresupuestoHistorialEnvios(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaDatoSErr ret = new strListaDatoSErr();

            try
            {
                Int32 ID_Pres2 = 0;
                try { ID_Pres2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Pres2")); } catch { }
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Pres2;
                dato.datoD = Convert.ToDecimal(ID_Cli2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadospresupuestoshistorialenvios", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaDatoSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadospresupuestoshistorialenvios-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion


        #endregion






        #region "Modulo Clientes - Clientes relacionados"

        #region "Vistas"



        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/contactos")]
        public async Task<IActionResult> ClienteClientesRelacionadosContactos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Clientes lis = new strClientesCliente_ClientesRelacionados_Clientes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosclientes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Clientes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosclientes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosClientesViewModel(lis);

            return View("Cliente-ClientesRelacionados-Contactos", viewModel);
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/solicitudes")]
        public async Task<IActionResult> ClienteClientesRelacionadosSolicitudes(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Solicitudes lis = new strClientesCliente_ClientesRelacionados_Solicitudes();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadossolicitudes", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Solicitudes>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadossolicitudes-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosSolicitudesViewModel(lis);

            return View("Cliente-ClientesRelacionados-Solicitudes", viewModel);
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/presupuestos-contactos")]
        public async Task<IActionResult> ClienteClientesRelacionadosPresupuestosContactos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Presupuestos lis = new strClientesCliente_ClientesRelacionados_Presupuestos();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadospresupuestos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Presupuestos>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadospresupuestos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosPresupuestosViewModel(lis);

            return View("Cliente-ClientesRelacionados-PresupuestosContactos", viewModel);
        }

        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-clientes/clientes/cliente/clientes-relacionados/asistencias")]
        public async Task<IActionResult> ClienteClientesRelacionadosAsistencias(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strClientesCliente_ClientesRelacionados_Asistencias lis = new strClientesCliente_ClientesRelacionados_Asistencias();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                ViewData["ID_Cli2"] = ID_Cli2;

                strbuscarlistado bus = new strbuscarlistado();
                bus.id = ID_Cli2;
                bus.numReg = 50;
                bus.pagina = pag;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("moduloclientes/extranetclientesclienteclientesrelacionadosasistencias", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strClientesCliente_ClientesRelacionados_Asistencias>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-moduloclientes/extranetclientesclienteclientesrelacionadosasistencias-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloClientesClienteClientesRelacionadosAsistenciasViewModel(lis);

            return View("Cliente-ClientesRelacionados-Asistencias", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #endregion


        #region "Recuperar datos - Get"

        #endregion


        #endregion


        #endregion


    }

}
