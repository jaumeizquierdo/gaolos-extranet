
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using ModuloSolicitudesLibrary;

namespace Gaolos.Controllers
{
    public class ModuloSolicitudesController : Controller
    {

        #region "Vistas"

        // Solicitudes Nueva
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/nueva")]
        public async Task<IActionResult> SolicitudesNueva(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strSolicitudesNuevaSolicitudIni lis = new strSolicitudesNuevaSolicitudIni();

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

                //ViewData["ID_Idi"] = ID_Idi;
                //ViewData["buscar"] = buscar;
                strDato bus = new strDato
                {
                    //buscar = buscar,
                    //id = ID_Us_Asi,
                    //numReg = 50,
                    //pagina = pag
                };
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesnuevasolicitudini", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strSolicitudesNuevaSolicitudIni>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesnuevasolicitudini-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloSolicitudesNuevaSolicitudIniViewModel(lis);

            return View("SolicitudesNueva", viewModel);
        }

        // Solicitudes por asignar
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/por-asignar")]
        public async Task<IActionResult> SolicitudesPorAsignar(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloSolicitudesListado lis = new strModuloSolicitudesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["ID_Idi"] = ID_Idi;
                ViewData["buscar"] = buscar;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    numReg = 50,
                    pagina = pag
                };
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesporasignarlistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloSolicitudesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesporasignarlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            HttpContext.Session.SetString("reload_labelsmenunotificaciones", "s");

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloSolicitudesAbiertasListadoViewModel(lis);

            return View("SolicitudesPorAsignar", viewModel);

        }

        // Solicitudes Abiertas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/abiertas")]
        public async Task<IActionResult> SolicitudesAbiertas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloSolicitudesListado lis = new strModuloSolicitudesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["ID_Idi"] = ID_Idi;
                ViewData["buscar"] = buscar;
                ViewData["ID_Us_Asi"] = ID_Us_Asi;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    id = ID_Us_Asi,
                    numReg = 50,
                    pagina = pag
                };
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesabiertaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloSolicitudesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesabiertaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloSolicitudesAbiertasListadoViewModel(lis);

            return View("SolicitudesAbiertas", viewModel);

        }

        // Solicitudes cerradas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/historial")]
        public async Task<IActionResult> SolicitudesCerradas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            strModuloSolicitudesListado lis = new strModuloSolicitudesListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                //ViewData["ID_Idi"] = ID_Idi;
                ViewData["buscar"] = buscar;
                ViewData["ID_Us_Asi"] = ID_Us_Asi;
                strbuscarlistado bus = new strbuscarlistado
                {
                    buscar = buscar,
                    id = ID_Us_Asi,
                    numReg = 50,
                    pagina = pag
                };
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudescerradaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloSolicitudesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudescerradaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloSolicitudesAbiertasListadoViewModel(lis);

            return View("SolicitudesHistorial", viewModel);

        }

        // Solicitudes Dashboard
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes")]
        public async Task<IActionResult> SolicitudesDashboardInicio(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strSolicitudesDashBoardInicio lis = new strSolicitudesDashBoardInicio();

            try
            {
                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesdashboardinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strSolicitudesDashBoardInicio>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesdashboardinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloSolicitudesDashBoardInicioViewModel(lis);

            return View("SolicitudesDashboard", viewModel);
        }

        #endregion


        #region "Ajax - Modulo Usuarios"

        #region "Guardar - Post"

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/solicitud-nueva")]
        public async Task<strDato> SolicitudNueva(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {

                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cont2 = 0;
                try { ID_Cont2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cont2")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                string Emp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Emp");
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string Expo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Expo");

                //  Vinculaciones
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string valbool = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvCom");
                bool EnvCom = false;
                if (valbool.ToLower() == "true") { EnvCom = true; }



                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strSolicitudNueva dato = new strSolicitudNueva();
                dato.ID_Cli2 = ID_Cli2;
                dato.ID_Cont2 = ID_Cont2;
                dato.Expo = Expo;
                dato.ID_Us_Asi = ID_Us_Asi;

                strSolicitudAux Aux = null;
                try
                {
                    if (Emp.Length>0 || Mail.Length>0 || Tel.Length>0 || Dir.Length>0 || Pai.Length>0 || Pro.Length>0 || CP.Length>0 || Pob.Length>0)
                    {
                        Aux = new strSolicitudAux();
                        Aux.Cont = "";
                        Aux.CP = CP;
                        Aux.Dir = Dir;
                        Aux.DNI = "";
                        Aux.Emp = Emp;
                        Aux.EsMujer = false;
                        Aux.Fe_Na = "";
                        Aux.Mail = Mail;
                        Aux.Pai = Pai;
                        Aux.Pob = Pob;
                        Aux.Pro = Pro;
                        Aux.Tel = Tel;
                    }
                } catch { }

                dato.Aux = Aux;

                strSolicitudVinculado[] Vin = null;

                if (ID_Curso2>0)
                {
                    if (Vin == null) { Vin = new strSolicitudVinculado[1]; }
                    else { Array.Resize(ref Vin, Vin.Length + 1); }
                    Vin[Vin.Length - 1] = new strSolicitudVinculado();
                    Vin[Vin.Length - 1].ID_Tipo = 15; // Cursos - Ocupacion
                    Vin[Vin.Length - 1].ID_ID = ID_Curso2;

                    strDato Obj = new strDato();
                    Obj.datoB = EnvCom;
                    Vin[Vin.Length - 1].Obj = Obj;
                }

                dato.Vinc = Vin;


                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulosolicitudes/extranetsolicitudnueva", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudnueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (dat.err.eserror) ret.Err = dat.err;

            return ret;

        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/asignar-solicitud")]
        public async Task<transportout> SolicitudAsignar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Soli2 = 0;
                try { ID_Soli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Soli2")); } catch { }
                Int32 ID_Us_Asi = 0;
                try { ID_Us_Asi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Asi")); } catch { }
                Int32 Prio = 0;
                try { Prio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prio")); } catch { }
                string Nota = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Nota");


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Soli2;
                dato.datoD = Convert.ToDecimal(ID_Us_Asi);
                dato.datoS1 = Prio.ToString();
                dato.datoS2 = Nota;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("modulosolicitudes/extranetsolicitudessolicitudasignar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudessolicitudasignar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/nuevainfocontactoempresa")]
        public async Task<strListado> SolicitudesNuevaInfoContactoEmpresa(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                Int32 ID_Cli2 = 0;
                try { ID_Cli2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2")); } catch { }
                Int32 ID_Cont2 = 0;
                try { ID_Cont2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cont2")); } catch { }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Cli2;
                bus.datoD = Convert.ToDecimal(ID_Cont2);
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesnuevainfocontactoempresa", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudesnuevainfocontactoempresa-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/nuevabuscarcontactolistado")]
        public async Task<strListadoConPaginador> SolicitudesNuevaBuscarContactoListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListadoConPaginador ret = new strListadoConPaginador();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscarTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarTel");
                string buscarMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarMail");
                string buscarNom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarNom");
                string buscarDNI = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarDNI");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");

                Int32 num = 8;

                Int32 ID_Tipo = 0;
                string buscar = "";

                if (buscarTel!="")
                {
                    ID_Tipo = 1;
                    buscar = buscarTel;
                } else if (buscarMail!="")
                {
                    ID_Tipo = 2;
                    buscar = buscarMail;
                }
                else if (buscarNom != "")
                {
                    ID_Tipo = 3;
                    buscar = buscarNom;
                }
                else if (buscarDNI != "")
                {
                    ID_Tipo = 4;
                    buscar = buscarDNI;
                }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Tipo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesnuevabuscarcontactolistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudesnuevabuscarcontactolistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/nuevabuscarclientelistado")]
        public async Task<strListadoConPaginador> SolicitudesNuevaBuscarClienteListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListadoConPaginador ret = new strListadoConPaginador();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscarTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarTel");
                string buscarMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarMail");
                string buscarNom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarNom");
                string buscarDNI = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarDNI");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");

                Int32 num = 8;

                Int32 ID_Tipo = 0;
                string buscar = "";

                if (buscarTel != "")
                {
                    ID_Tipo = 1;
                    buscar = buscarTel;
                }
                else if (buscarMail != "")
                {
                    ID_Tipo = 2;
                    buscar = buscarMail;
                }
                else if (buscarNom != "")
                {
                    ID_Tipo = 3;
                    buscar = buscarNom;
                }
                else if (buscarDNI != "")
                {
                    ID_Tipo = 4;
                    buscar = buscarDNI;
                }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Tipo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesnuevabuscarclientelistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudesnuevabuscarclientelistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/nuevabuscarsolicitudabiertalistado")]
        public async Task<strListadoConPaginador> SolicitudesNuevaBuscarSolicitudeAbiertaListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListadoConPaginador ret = new strListadoConPaginador();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscarTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarTel");
                string buscarMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarMail");
                string buscarNom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarNom");
                string buscarDNI = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarDNI");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");

                Int32 num = 8;

                Int32 ID_Tipo = 0;
                string buscar = "";

                if (buscarTel != "")
                {
                    ID_Tipo = 1;
                    buscar = buscarTel;
                }
                else if (buscarMail != "")
                {
                    ID_Tipo = 2;
                    buscar = buscarMail;
                }
                else if (buscarNom != "")
                {
                    ID_Tipo = 3;
                    buscar = buscarNom;
                }
                else if (buscarDNI != "")
                {
                    ID_Tipo = 4;
                    buscar = buscarDNI;
                }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Tipo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesnuevabuscarsolicitudabiertalistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudesnuevabuscarsolicitudabiertalistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/nuevabuscarocupacioncontactolistado")]
        public async Task<strListadoConPaginador> SolicitudesNuevaBuscarOcupacionContactoListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListadoConPaginador ret = new strListadoConPaginador();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscarTel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarTel");
                string buscarMail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarMail");
                string buscarNom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarNom");
                string buscarDNI = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarDNI");
                string clase = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Clas");

                Int32 num = 8;

                Int32 ID_Tipo = 0;
                string buscar = "";

                if (buscarTel != "")
                {
                    ID_Tipo = 1;
                    buscar = buscarTel;
                }
                else if (buscarMail != "")
                {
                    ID_Tipo = 2;
                    buscar = buscarMail;
                }
                else if (buscarNom != "")
                {
                    ID_Tipo = 3;
                    buscar = buscarNom;
                }
                else if (buscarDNI != "")
                {
                    ID_Tipo = 4;
                    buscar = buscarDNI;
                }


                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.clase = clase;
                bus.numReg = num;
                bus.pagina = pag;
                bus.id = ID_Tipo;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesnuevabuscarocupacioncontactolistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudesnuevabuscarocupacioncontactolistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/buscar-usuario")]
        public async Task<strDato> SolicitudesBuscarTrabajador(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesbuscarusuariolistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes-extranetsolicitudesbuscarusuariolistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de empresas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/listado-empresas-buscar")]
        public async Task<strDato> SolicitudesEmpresasListadoBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesbuscarempresaslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesbuscarempresaslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de cursos abiertos
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/listado-cursos-buscar")]
        public async Task<strDato> SolicitudesCursosListadoBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesbuscarcursolistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesbuscarcursolistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de precios del curso
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/listado-precios-cursos")]
        public async Task<strListaErr> SolicitudesCursosListaDePrecios(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesobtenerprecioscurso", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesobtenerprecioscurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/nic-ocupado-curso")]
        public async Task<strDato> SolicitudesNICOcupadoCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = NIC;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesrevisarniccurso", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesrevisarniccurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion



        #endregion

        #region "Ajax - DashBoard"

        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/dashboardaltasporusuario")]
        public async Task<strListado> SolicitudesDashboardAltasPorUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesdashboardaltasporusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesdashboardaltasporusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            if (dat.err.eserror) ret.Err = dat.err;

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/dashboardaltaspordia")]
        public async Task<strListado> SolicitudesDashboardAltasPorDia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesdashboardaltaspordia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesdashboardaltaspordia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            if (dat.err.eserror) ret.Err = dat.err;

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/dashboardcargadetrabajoporusuario")]
        public async Task<strListado> SolicitudesDashboardCargarDeTrabajoPorUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesdashboardcargardetrabajoporusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesdashboardcargardetrabajoporusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            if (dat.err.eserror) ret.Err = dat.err;

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/dashboardpromediotiempo")]
        public async Task<strListado> SolicitudesDashboardPromedioTiempo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesdashboardpromediotiempo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesdashboardpromediotiempo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            if (dat.err.eserror) ret.Err = dat.err;

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/dashboardcierresporusuario")]
        public async Task<strListado> SolicitudesDashboardCierresPorUsuario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesdashboardcierresporusuario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesdashboardcierresporusuario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            if (dat.err.eserror) ret.Err = dat.err;

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("solicitudes/dashboardcierrespordia")]
        public async Task<strListado> SolicitudesDashboardCierresPorDia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoS1 = Fe_In;
                bus.datoS2 = Fe_Fi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulosolicitudes/extranetsolicitudesdashboardcierrepordia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulosolicitudes/extranetsolicitudesdashboardcierrepordia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
                ret.Err = dat.err;
            }

            if (dat.err.eserror) ret.Err = dat.err;

            return ret;

        }

        #endregion

        #endregion




    }
}