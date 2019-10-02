using Microsoft.Net.Http.Headers;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Models;
using System.Net.Http;
using LogsBbdds;
using Gaolos.Session;
using Microsoft.AspNetCore.Http;
using CursosLibrary;
using System.IO;
using DashBoardLibrary;

namespace Gaolos.Controllers
{
    public class ModuloCursosController : Controller
    {

        #region "DashBoard"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos")]
        public async Task<IActionResult> CursosDashboard(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("modulocursos/extranetcursosdashboard", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strDashBoard>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-modulocursos/extranetcursosdashboard-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new DashBoardViewModel(lis);

            return View("CursosDashboard", viewModel);
        }

        #endregion

        #region "Modulo Curso - Cursos"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos")]
        public async Task<IActionResult> CursosListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();


            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }
                Int32 ID_Agente = 0;
                try { ID_Agente = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Agente")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                Int32 ID_TipoCurso = 0;
                try { ID_TipoCurso = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoCurso")); } catch { }
                string Ubi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Ubi");

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

                ViewData["buscar"] = buscar;
                ViewData["ID_Agente"] = ID_Agente;
                ViewData["ID_Tipo"] = ID_Tipo;
                ViewData["Fe_In"] = Fe_In;
                ViewData["Fe_Fi"] = Fe_Fi;
                ViewData["ID_TipoCurso"] = ID_TipoCurso;
                ViewData["Ubi"] = Ubi;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 20;
                bus.pagina = pag;
                bus.id = ID_Tipo;
                bus.id2 = ID_Agente;
                strDatoS filtro = new strDatoS();
                filtro.datoS1 = Fe_In;
                filtro.datoS2 = Fe_Fi;
                filtro.datoS3 = Ubi;
                filtro.datoS4 = ID_TipoCurso.ToString();
                bus.obj = filtro;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());

                    ViewData["ID_Tipo"] = lis.ID_Tipo;

                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("Cursos", viewModel);
        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/listado-deben")]
        public async Task<strListadoConPaginador> CursosCursosDebenListado(string paramsin)
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

                string buscarcob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarcob");
                string buscarimp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscarimp");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                ViewData["buscarcob"] = buscarcob;
                ViewData["buscarimp"] = buscarimp;

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscarcob;
                dato.clase = clase;
                dato.numReg = num;
                dato.pagina = pag;
                strDatoS filtro = new strDatoS();
                filtro.datoS1 = buscarimp;
                dato.obj = filtro;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursosdebenlistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursosdebenlistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/duplicar")]
        public async Task<transportout> CursosDuplicarCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursosduplicarcurso", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosduplicarcurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Modulo Cursos - Curso"

        #region "Vistas"


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-general")]
        public async Task<IActionResult> CursosCurso_General(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_General lis = new strCursosCursoDetallesEditar_General();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesgeneral", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_General>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgeneral-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_GeneralViewModel(lis);

            return View("Curso-General", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-asignaturas")]
        public async Task<IActionResult> CursosCurso_Asignaturas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Asignaturas lis = new strCursosCursoDetallesEditar_Asignaturas();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesasignaturas", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Asignaturas>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasignaturas-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_AsignaturasViewModel(lis);

            return View("Curso-Asignaturas", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-calendario")]
        public async Task<IActionResult> CursosCurso_Calendario(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Calendario lis = new strCursosCursoDetallesEditar_Calendario();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallescalendario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Calendario>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_CalendarioViewModel(lis);

            return View("Curso-Calendario", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-comunicacion")]
        public async Task<IActionResult> CursosCurso_Comunicacion(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Comunicacion lis = new strCursosCursoDetallesEditar_Comunicacion();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallescomunicacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Comunicacion>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescomunicacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_ComunicacionViewModel(lis);

            return View("Curso-Comunicacion", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-alumnos")]
        public async Task<IActionResult> CursosCurso_Alumnos(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Alumnos lis = new strCursosCursoDetallesEditar_Alumnos();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Estado = 0;
                try { ID_Estado = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Estado")); } catch { }
                string verPlaza = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Plaza");
                bool Plaza = false;
                if (verPlaza!="") Plaza = true;
                string verCob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Cob");

                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

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
                ViewData["ID_Ocu2"] = "";
                if (ID_Ocu2 > 0) ViewData["ID_Ocu2"] = ID_Ocu2.ToString();

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                bus.datoD = Convert.ToDecimal(ID_Estado);
                bus.datoB = Plaza;
                bus.datoS = verCob;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                ViewData["ID_Estado"] = ID_Estado;
                ViewData["Plaza"] = verPlaza;
                ViewData["Cob"] = verCob;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesalumnos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Alumnos>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_AlumnosViewModel(lis);

            return View("Curso-Alumnos", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-grupos")]
        public async Task<IActionResult> CursosCurso_Grupos(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Grupos lis = new strCursosCursoDetallesEditar_Grupos();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesgrupos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Grupos>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgrupos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_GruposViewModel(lis);

            return View("Curso-Grupos", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-asistencia")]
        public async Task<IActionResult> CursosCurso_Asistencia(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Asistencia lis = new strCursosCursoDetallesEditar_Asistencia();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesasistencia", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Asistencia>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_AsistenciaViewModel(lis);

            return View("Curso-Asistencia", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-evaluacion")]
        public async Task<IActionResult> CursosCurso_Evaluacion(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Evaluacion lis = new strCursosCursoDetallesEditar_Evaluacion();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesevaluacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Evaluacion>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesevaluacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_EvaluacionViewModel(lis);

            return View("Curso-Evaluacion", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-documentacion")]
        public async Task<IActionResult> CursosCurso_Documentacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Documentacion lis = new strCursosCursoDetallesEditar_Documentacion();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesdocumentacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Documentacion>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesdocumentacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_DocumentacionViewModel(lis);

            return View("Curso-Documentacion", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-formadores")]
        public async Task<IActionResult> CursosCurso_Formadores(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Formadores lis = new strCursosCursoDetallesEditar_Formadores();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesformadores", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Formadores>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesformadores-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_FormadoresViewModel(lis);

            return View("Curso-Formadores", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso-cierre")]
        public async Task<IActionResult> CursosCurso_Cierre(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Cierre lis = new strCursosCursoDetallesEditar_Cierre();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

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

                //ViewData["ID_Idi"] = ID_Idi;
                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallescierre", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Cierre>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescierre-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursoDetallesEditar_CierreViewModel(lis);

            return View("Curso-Cierre", viewModel);
        }

        #endregion


        #region "Ajax"

        #region "Guardar - Post"

        #region "Guardar - Post - Curso - General"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/generalguardar")]
        public async Task<transportout> CursosCursoDetalles_General_CursoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string Curso = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Curso");
                Int32 ID_TipoCurso = 0;
                try { ID_TipoCurso = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoCurso")); } catch { }
                string Fe_In_Pub = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In_Pub");
                string Fe_Act = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Act");
                Int32 ID_Nivel = 0;
                try { ID_Nivel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Nivel")); } catch { }
                string Fe_Fi_Pub = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi_Pub");
                Int32 Plazas = 0;
                try { Plazas = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Plazas")); } catch { }
                bool online = false;
                string txtOnLine = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "online");
                if (txtOnLine == "true") { online = true; }
                string Fe_In_Ins = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In_Ins");
                string IVA = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "IVA");
                Int32 ID_CentroCoste = 0;
                try { ID_CentroCoste = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CentroCoste")); } catch { }
                string Fe_Fi_Ins = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi_Ins");
                Int32 Prioridad = 0;
                try { Prioridad = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Prioridad")); } catch { }
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                Int32 ID_Us_Agente = 0;
                try { ID_Us_Agente = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Us_Agente")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosCursoDetallesEditar_General_CursoGuardar dato = new strCursosCursoDetallesEditar_General_CursoGuardar();
                dato.Curso = Curso;
                dato.Fe_Act = Fe_Act;
                dato.Fe_Fi_Ins = Fe_Fi_Ins;
                dato.Fe_Fi_Pub = Fe_Fi_Pub;
                dato.Fe_In_Ins = Fe_In_Ins;
                dato.Fe_In_Pub = Fe_In_Pub;
                dato.ID_CentroCoste = ID_CentroCoste;
                dato.ID_Curso2 = ID_Curso2;
                dato.ID_Nivel = ID_Nivel;
                dato.ID_TipoCurso = ID_TipoCurso;
                dato.ID_Us_Agente = ID_Us_Agente;
                dato.IVA = IVA;
                dato.Obs = Obs;
                dato.online = online;
                dato.Plazas = Plazas;
                dato.Prioridad = Prioridad;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgeneralcursoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgeneralcursoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/generalpreciosguardar")]
        public async Task<transportout> CursosCursoDetalles_General_CursoPreciosGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string Precios = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precios");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoS = Precios;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgeneralcursopreciosguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgeneralcursopreciosguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/generaledadguardar")]
        public async Task<transportout> CursosCursoDetalles_General_CursoEdadGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string Nom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Nom");
                string De = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "De");
                string A = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "A");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoS1 = Nom;
                dato.datoS2 = De;
                dato.datoS3 = A;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgeneralcursoedadguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgeneralcursoedadguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/generaledadeliminar")]
        public async Task<transportout> CursosCursoDetalles_General_CursoEdadEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Edad = 0;
                try { ID_Edad = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Edad")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Edad);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgeneralcursoedadeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgeneralcursoedadeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Guardar - Post - Curso - Asignaturas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/solo-formador")]
        public async Task<transportout> CursosCursoDetalles_Asignaturas_SoloFormadorGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 NumSoloForm = 0;
                try { NumSoloForm = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NumSoloForm")); } catch { }
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "SoloForm");
                bool SoloForm = false;
                if (txt == "true") SoloForm = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(NumSoloForm);
                dato.datoB = SoloForm;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasignaturassoloformadorguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasignaturassoloformadorguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/asignaturas-add-asignatura")]
        public async Task<transportout> CursosCursoDetalles_Asignaturas_AñadirAsignatura(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Asig2 = 0;
                try { ID_Asig2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asig2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Asig2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasignaturasaddasignatura", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasignaturasaddasignatura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/asignaturas-del-asignatura")]
        public async Task<transportout> CursosCursoDetalles_Asignaturas_EliminarAsignatura(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Rel);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasignaturasdelasignatura", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasignaturasdelasignatura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/del-evaluacion")]
        public async Task<transportout> CursosCursoDetalles_Asignaturas_EliminarEvaluacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetCursoscursodetallesasignaturaseliminarevaluacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetCursoscursodetallesasignaturaseliminarevaluacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/del-evaluacion-asistencia")]
        public async Task<transportout> CursosCursoDetalles_Asignaturas_EliminarEvaluacionYAsistencia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetCursoscursodetallesasignaturaseliminarevaluacionyasistencia", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetCursoscursodetallesasignaturaseliminarevaluacionyasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/del-asistencia")]
        public async Task<transportout> CursosCursoDetalles_Asignaturas_EliminarAsistencia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetCursoscursodetallesasignaturaseliminarasistencia", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetCursoscursodetallesasignaturaseliminarasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/re-abrir-asistencia")]
        public async Task<transportout> CursosExtranetController_CursosCursoDetalles_Asignaturas_ReAbrirAsistencia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasignaturasreabrirasistencia", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasignaturasreabrirasistencia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Guardar - Post - Curso - Calendario"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/calendario-guardar")]
        public async Task<transportout> CursosCursoDetalles_Calendario_CursoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Cale = 0;
                try { ID_Cale = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cale")); } catch { }
                Int32 ID_Asig2 = 0;
                try { ID_Asig2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asig2")); } catch { }
                Int32 ID_Temario2 = 0;
                try { ID_Temario2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Temario2")); } catch { }
                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }
                Int32 ID_TipoH = 0;
                try { ID_TipoH = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoH")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string TotalMin = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "TotalMin");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosCursoDetallesEditar_Calendario_CursoGuardar dato = new strCursosCursoDetallesEditar_Calendario_CursoGuardar();
                dato.ID_Curso2 = ID_Curso2;
                dato.ID_Cale = ID_Cale;
                dato.ID_Asig2 = ID_Asig2;
                dato.ID_Temario2 = ID_Temario2;
                dato.ID_TipoH = ID_TipoH;
                dato.ID_Ubi2 = ID_Ubi2;
                dato.Fe_In = Fe_In;
                dato.Fe_Fi = Fe_Fi;
                dato.TotalMin = TotalMin;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallescalendariocursoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendariocursoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/calendario-nuevo")]
        public async Task<transportout> CursosCursoDetalles_Calendario_CursoNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Cale = 0;
                Int32 ID_Asig2 = 0;
                try { ID_Asig2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asig2")); } catch { }
                Int32 ID_Temario2 = 0;
                try { ID_Temario2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Temario2")); } catch { }
                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }
                Int32 ID_TipoH = 0;
                try { ID_TipoH = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoH")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string TotalMin = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "TotalMin");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosCursoDetallesEditar_Calendario_CursoGuardar dato = new strCursosCursoDetallesEditar_Calendario_CursoGuardar();
                dato.ID_Curso2 = ID_Curso2;
                dato.ID_Cale = ID_Cale;
                dato.ID_Asig2 = ID_Asig2;
                dato.ID_Temario2 = ID_Temario2;
                dato.ID_TipoH = ID_TipoH;
                dato.ID_Ubi2 = ID_Ubi2;
                dato.Fe_In = Fe_In;
                dato.Fe_Fi = Fe_Fi;
                dato.TotalMin = TotalMin;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallescalendariocursonuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendariocursonuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/calendario-del")]
        public async Task<transportout> CursosCursoDetalles_Calendario_CursoDel(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Cale = 0;
                try { ID_Cale = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cale")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Cale);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallescalendariocursodel", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendariocursodel-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/calendario-quitar-formador")]
        public async Task<transportout> CursosCursoDetalles_Calendario_DelProfesor(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Cale = 0;
                try { ID_Cale = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cale")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Cale);
                dato.datoS = ID_Rel.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallescalendariodelprofesor", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendariodelprofesor-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/calendario-add-formador")]
        public async Task<transportout> CursosCursoDetalles_Calendario_AddProfesor(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Cale = 0;
                try { ID_Cale = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cale")); } catch { }
                Int32 ID_Prof2 = 0;
                try { ID_Prof2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Prof2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Cale);
                dato.datoS = ID_Prof2.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallescalendarioaddprofesor", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendarioaddprofesor-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Guardar - Post - Curso - Alumnos"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-eliminar")]
        public async Task<transportout> CursosCursoDetalles_Alumno_EliminarInscripcion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string MotEli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "MotEli");
                string ID_Ocu2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2");
                string txtCerrarSoli = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CerrarSoli");
                bool CerrarSoli = false;
                if (txtCerrarSoli != "") CerrarSoli = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoB = CerrarSoli;
                dato.datoS1 = ID_Ocu2;
                dato.datoS2 = MotEli;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoeliminarinscripcion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoeliminarinscripcion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-rapida")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionRapida(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string Come = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Come");
                Int32 NumAlu = 0;
                try { NumAlu = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NumAlu")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(NumAlu);
                dato.datoS = Come;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcionrapida", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcionrapida-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-obs-guardar")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionObsGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                string Obs2 = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs2");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS1 = Obs;
                dato.datoS2 = Obs2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcionobsguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcionobsguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-contacto-guardar")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionContactoGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                string Nom = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Nom");
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string NIF = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIF");
                string Tel = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Tel");
                string Mail = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mail");
                string Fe_Na = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Na");
                string txtSexo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "sm");
                bool Sexo = false;
                if (txtSexo != "") Sexo = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosCursoDetallesEditar_Alumnos_OcupacionContacto dato = new strCursosCursoDetallesEditar_Alumnos_OcupacionContacto();
                dato.ID_Curso2 = ID_Curso2;
                dato.ID_Ocu2 = ID_Ocu2;
                dato.Nom = Nom;
                dato.Dir = Dir;
                dato.Pai = Pai;
                dato.Pro = Pro;
                dato.CP = CP;
                dato.Pob = Pob;
                dato.NIF = NIF;
                dato.Tel = Tel;
                dato.Mail = Mail;
                dato.Fe_Na = Fe_Na;
                dato.Sexo = Sexo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncontactoguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncontactoguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-grupo")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarGrupo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS = ID_Grupo.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiargrupo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiargrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-curso")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                Int32 ID_Curso2Des = 0;
                try { ID_Curso2Des = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2Des")); } catch { }
                Int32 ID_PrecioDes = 0;
                try { ID_PrecioDes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_PrecioDes")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS1 = ID_Curso2Des.ToString();
                dato.datoS2 = ID_PrecioDes.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiarcurso", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiarcurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-estado")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarEstado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                Int32 ID_Estado = 0;
                try { ID_Estado = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Estado")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS = ID_Estado.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiarestado", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiarestado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-a-interesado")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarAInteresado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                Int32 ID_Estado = 1;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS = ID_Estado.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiarestado", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiarestado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-plaza-asignar")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarPlazaAsignar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                //dato.datoS = ID_Estado.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiarplazaasignar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiarplazaasignar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-plaza-quitar")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarPlazaQuitar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                //dato.datoS = ID_Estado.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiarplazaquitar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiarplazaquitar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-tarifa")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarTarifa(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                Int32 ID_Precio = 0;
                try { ID_Precio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Precio")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS = ID_Precio.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiartarifa", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiartarifa-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-cambiar-precio")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCambiarPrecio(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                string Precio = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precio");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS = Precio;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncambiarprecio", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncambiarprecio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/listado-alumnos")]
        public async Task<IActionResult> CursosCursoDetalles_Alumno_ListadoAlumnos(string paramsin) //FileResult
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                //string buscar = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "buscar");

                Int32 ID_Idi = 0;
                //try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Idi")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesalumnoslistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
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

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            //return File(lis.Fichero, lis.TipoMime + "; charset=windows-1254", lis.Nombre);
            return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
        }

        [HttpPost]
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-importar-csv")]
        public async Task<transportout> Upload_ListadoAlumnos_CSV(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout { err = new strerror() };

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string csv = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "csv");

                Int32 ID_Idi = 1;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out int ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato
                {
                    datoI = ID_Curso2,
                    datoS = csv
                };
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoslistadoimportarcsv", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos-extranetcursoscursodetallesalumnoslistadoimportarcsv-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-eliminar-foto")]
        public async Task<transportout> CursosCursoDetalles_Alumno_EliminarFotografia(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoeliminarfotografia", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoeliminarfotografia-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-marcar-cobro")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionCobro(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                Int32 ID_CueNeg = 0;
                try { ID_CueNeg = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_CueNeg")); } catch { }
                string Fe_Cob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Cob");
                string Imp = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Imp");
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "envNoti");
                bool envNoti = false;
                if (txt.ToLower() == "true") envNoti = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS1 = ID_CueNeg.ToString();
                dato.datoS2 = Fe_Cob;
                dato.datoS3 = Imp;
                dato.datoB = envNoti;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioncobro", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioncobro-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/inscripcion-nueva")]
        public async Task<transportout> CursosCursoDetalles_Alumno_InscripcionInscribir(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                Int32 ID_Precio = 0;
                try { ID_Precio = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Precio")); } catch { }
                string Precio = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Precio");
                string Fe_Repla = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Repla");
                string NIC = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "NIC");
                string txtEnv = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnvIns");
                bool EnvIns = false;
                if (txtEnv == "true") EnvIns = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS1 = ID_Precio.ToString();
                dato.datoS2 = NIC;
                dato.datoS3 = Precio;
                dato.datoS4 = Fe_Repla;
                dato.datoB = EnvIns;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoinscripcioninscribir", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoinscripcioninscribir-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/ocupacion/enviar-comunicacion")]
        public async Task<transportout> CursosCursoDetalles_Alumno_OcupacionEnviarComunicacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesalumnoocupacionenviarcomunicacion", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoocupacionenviarcomunicacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Guardar - Post - Curso - Grupos"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/grupos-nuevo-grupo")]
        public async Task<transportout> CursosCursoDetalles_Grupos_NuevoGrupo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string Grupo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Grupo");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoS = Grupo;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgruposnuevogrupo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposnuevogrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/grupos-eliminar-grupo")]
        public async Task<transportout> CursosCursoDetalles_Grupos_EliminarGrupo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Grupo);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgruposeliminargrupo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposeliminargrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/grupos-guardar-grupo")]
        public async Task<transportout> CursosCursoDetalles_Grupos_GuardarGrupo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                Int32 ID_Cli2Col = 0;
                try { ID_Cli2Col = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Col")); } catch { }
                Int32 ID_Cli2Fac = 0;
                try { ID_Cli2Fac = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cli2Fac")); } catch { }
                string ObsPub = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ObsPub");
                string Mails = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mails");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosCursoDetallesEditar_Grupo dato = new strCursosCursoDetallesEditar_Grupo();
                dato.ID_Curso2 = ID_Curso2;
                dato.ID_Grupo = ID_Grupo;

                dato.ID_Cli2Col = ID_Cli2Col;
                dato.ID_Cli2Fac = ID_Cli2Fac;
                dato.ObsPub = ObsPub;
                dato.Mails = Mails;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgruposguardargrupo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposguardargrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/grupos-add-plantilla")]
        public async Task<transportout> CursosCursoDetalles_Grupos_AddPlantilla(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                Int32 ID_TPGD2 = 0;
                try { ID_TPGD2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TPGD2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToInt32(ID_Grupo);
                dato.datoS = ID_TPGD2.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgruposaddplantilla", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposaddplantilla-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/grupos-grupo-cambiar-doc")]
        public async Task<transportout> CursosCursoDetalles_Grupos_GrupoCambiarDoc(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                Int32 ID_TPGDE = 0;
                try { ID_TPGDE = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TPGDE")); } catch { }
                Int32 ID_GD = 0;
                try { ID_GD = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_GD")); } catch { }
                Int32 ID_Doc2 = 0;
                try { ID_Doc2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Doc2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToInt32(ID_Grupo);
                dato.datoS1 = ID_GD.ToString();
                dato.datoS2 = ID_TPGDE.ToString();
                dato.datoS3 = ID_Doc2.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesgruposgrupocambiardoc", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposgrupocambiardoc-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Guardar - Post - Curso - Asistencia"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-asistencia-guardar")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_ControlAsistenciaGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string strid = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "strid");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoS = strid;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciacontrolasistenciaguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciacontrolasistenciaguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-asistencia-cerrar")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_ControlAsistenciaCerrar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Curso2Des = 0;
                try { ID_Curso2Des = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2Des")); } catch { }

                string Fe_Repla = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Repla");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Curso2Des);
                dato.datoS = Fe_Repla;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciacontrolasistenciacerrar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciacontrolasistenciacerrar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/curso-anular")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_AnularCurso(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string ExpoAnu = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ExpoAnu");
                Int32 ID_Curso2Des = 0;
                try { ID_Curso2Des = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2Des")); } catch { }

                string Fe_Repla = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Repla");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Curso2Des);
                dato.datoS2 = Fe_Repla;
                dato.datoS1 = ExpoAnu;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciaanularcurso", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciaanularcurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/reabrir-curso-anulado")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_ReabrirCursoAnulado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciareabrircursoanulado", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciareabrircursoanulado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/reabrir-curso-cerrado")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_ReabrirCursoCerrado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciareabrircursocerrado", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciareabrircursocerrado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/asistencia-mover-interesados")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_InscripcionMoverInteresados(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Curso2Des = 0;
                try { ID_Curso2Des = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2Des")); } catch { }

                string Fe_Repla = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Repla");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Curso2Des);
                dato.datoS = Fe_Repla;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciainscripcionmoverinteresados", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciainscripcionmoverinteresados-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/ocupacion-guardar-asig")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_AsignaturasGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }
                string id = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id");
                string val = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "val");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                dato.datoS1 = id;
                dato.datoS2 = val;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciaasignaturasguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciaasignaturasguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/listado-alumnos-asistencia")]
        public async Task<IActionResult> CursosCursoDetalles_Asistencia_ListadoAlumnos(string paramsin) //FileResult
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();


            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }

                Int32 ID_Idi = 0;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                if (ID_Idi == 0) { ID_Idi = ID_IdiPlataforma; }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Curso2;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesasistencialistadoalumnos", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
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

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistencialistadoalumnos-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            //return File(lis.Fichero, lis.TipoMime + "; charset=windows-1254", lis.Nombre);
            return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/asistencia-alumno-eliminar")]
        public async Task<transportout> CursosCursoDetalles_Asistencia_ControlAsistenciaEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesasistenciacontrolasistenciaeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciacontrolasistenciaeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #region "Guardar - Post - Curso - Evaluacion"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-evaluacion-guardar")]
        public async Task<transportout> CursosCursoDetalles_Evaluacion_ControlEvaluacionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string c = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "c");
                string a = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "a");
                string n = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "n");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoI = ID_Curso2;
                dato.datoS1 = c;
                dato.datoS2 = a;
                dato.datoS3 = n;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesevaluacioncontrolevaluacionguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesevaluacioncontrolevaluacionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-evaluacion-cerrar")]
        public async Task<transportout> CursosCursoDetalles_Evaluacion_ControlEvaluacionCerrar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                //Int32 ID_Curso2Des = 0;
                //try { ID_Curso2Des = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Curso2Des")); } catch { }

                //string Fe_Repla = Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "Fe_Repla");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                //dato.datoD = Convert.ToDecimal(ID_Curso2Des);
                //dato.datoS = Fe_Repla;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesevaluacioncontrolevaluacioncerrar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesevaluacioncontrolevaluacioncerrar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-evaluacion-eliminar")]
        public async Task<transportout> CursosCursoDetalles_Evaluacion_ControlEvaluacionEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursoscursodetallesevaluacioncontrolevaluacioneliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursoscursodetallesevaluacioncontrolevaluacioneliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Guardar - Post - Curso - Documentacion"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-documentacion-guardar")]
        public async Task<transportout> CursosCursoDetalles_Documentacion_ControlDocumentacionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string strid = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "strid");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoS = strid;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesdocumentacioncontroldocumentacionguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesdocumentacioncontroldocumentacionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Guardar - Post - Curso - Formadores"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-formador-guardar")]
        public async Task<transportout> CursosCursoDetalles_Formadores_ControlEvaluacionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Prof2 = 0;
                try { ID_Prof2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Prof2")); } catch { }
                Int32 ID_NFC = 0;
                try { ID_NFC = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_NFC")); } catch { }
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ids");
                Int32[] ids= JsonConvert.DeserializeObject<Int32[]>(txt);
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "come");
                string[] come = JsonConvert.DeserializeObject<string[]>(txt);
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "val");
                string[] val = JsonConvert.DeserializeObject<string[]>(txt);
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "np");
                bool[] np = JsonConvert.DeserializeObject<bool[]>(txt);
                txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "id");
                Int32[] ID_NFCD = JsonConvert.DeserializeObject<Int32[]>(txt);

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosCursoDetallesEditar_FormadoresEvaluacionGuardar dato = new strCursosCursoDetallesEditar_FormadoresEvaluacionGuardar();
                dato.ID_Curso2 = ID_Curso2;
                dato.ID_Prof2 = ID_Prof2;
                dato.ID_NFC = ID_NFC;
                dato.ID_Camps = ids;
                dato.NPs = np;
                dato.Comes = come;
                dato.Valores = val;
                dato.ID_NFCDs = ID_NFCD;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallesformadoresevaluaciondetallesguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesformadoresevaluaciondetallesguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Guardar - Post - Curso - Cierre"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/control-cierre-guardar")]
        public async Task<transportout> CursosCursoDetalles_Cierre_CursoCerrar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Curso2Des = 0;
                try { ID_Curso2Des = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2Des")); } catch { }

                string Fe_Repla = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Repla");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Curso2Des);
                dato.datoS = Fe_Repla;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursodetallescierrecursocerrar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescierrecursocerrar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion

        #region "Guardar - Post - Curso - Comunicacion"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/comunicacion/documentoadd")]
        public async Task<transportout> CursosCursoComunicacion_DocumentoAdd(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_DocOri = 0;
                try { ID_DocOri = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_DocOri")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_DocOri);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursocomunicaciondocumentoadd", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursocomunicaciondocumentoadd-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/comunicacion/documentodel")]
        public async Task<transportout> CursosCursoComunicacion_DocumentoDel(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Rel);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursocomunicaciondocumentodel", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursocomunicaciondocumentodel-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/comunicacion/diplomaadd")]
        public async Task<transportout> CursosCursoComunicacion_DisplomaAdd(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Doc2 = 0;
                try { ID_Doc2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Doc2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Doc2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursoscursocomunicaciondisplomaadd", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursocomunicaciondisplomaadd-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion

        #endregion

        #region "Recuperar datos - Get"

        #region "Recuperar datos - Get - Asignaturas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/buscar-asignatura")]
        public async Task<strDato> CursosCursoDetalles_AsignaturasBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesasignaturasbuscarasignatura", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasignaturasbuscarasignatura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/ocupacion-listado-asig")]
        public async Task<strListado> CursosCursoDetalles_AsignaturasOcupacionListado(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesasistenciaocuasiglistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesasistenciaocuasiglistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #region "Recuperar datos - Get - Calendario"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/buscar-ubicacion")]
        public async Task<strDato> CursosCursoDetalles_Calendario_BuscarUbicacion(string paramsin)
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

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallescalendariobuscarubicacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendariobuscarubicacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/temario-de-la-asignatura")]
        public async Task<strListaErr> CursosCursoDetalles_Calendario_TemarioDeLaAsignatura(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaErr ret = new strListaErr();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Asig2 = 0;
                try { ID_Asig2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Asig2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Asig2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallescalendariotameriodelaasignatura", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendariotameriodelaasignatura-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/obtener-fecha-calendario")]
        public async Task<strCursosCursoDetallesEditar_CalendarioObtenerFecha> CursosCursoDetalles_Calendario_ObtenerCalendario(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_CalendarioObtenerFecha ret = new strCursosCursoDetallesEditar_CalendarioObtenerFecha();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Cale = 0;
                try { ID_Cale = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Cale")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Cale);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallescalendarioobtenercalendario", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_CalendarioObtenerFecha>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendarioobtenercalendario-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/buscar-profesor")]
        public async Task<strDato> CursosCursoDetalles_Calendario_BuscarProfesor(string paramsin)
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

                strDato dato = new strDato();
                dato.datoS = buscar;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallescalendariobuscarprofesor", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallescalendariobuscarprofesor-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #region "Recuperar datos - Get - Alumnos"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/obtener-inscripcion")]
        public async Task<strCursosCursoDetallesEditar_Alumnos_ObtenerOcupacion> CursosCursoDetalles_Alumno_ObtenerInscripcion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Alumnos_ObtenerOcupacion ret = new strCursosCursoDetallesEditar_Alumnos_ObtenerOcupacion();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Ocu2 = 0;
                try { ID_Ocu2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ocu2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Ocu2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesalumnoobtenerinscripcion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Alumnos_ObtenerOcupacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesalumnoobtenerinscripcion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de cursos abiertos
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/listado-cursos-buscar")]
        public async Task<strDato> CursosCursoListadoBuscar(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosbuscarcursolistado", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosbuscarcursolistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de precios del curso
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/listado-precios-cursos")]
        public async Task<strListaErr> CursosCursoListadoBuscarObtenerPreciosCurso(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursobuscarcursoobtenerprecioscurso", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursobuscarcursoobtenerprecioscurso-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #region "Recuperar datos - Get - Grupos"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/grupos-obteber-grupo")]
        public async Task<strCursosCursoDetallesEditar_Grupo> CursosCursoDetalles_Grupos_ObtenerGrupo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_Grupo ret = new strCursosCursoDetallesEditar_Grupo();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Grupo);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesgruposobtenergrupo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_Grupo>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposobtenergrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/grupos-obteber-grupo-doc")]
        public async Task<strListado> CursosCursoDetalles_Grupos_ObtenerGrupoDoc(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Grupo = 0;
                try { ID_Grupo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Grupo")); } catch { }
                Int32 ID_GD = 0;
                try { ID_GD = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_GD")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Grupo);
                dato.datoS = ID_GD.ToString();
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesgruposobtenergrupodoc", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposobtenergrupodoc-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        // Listado de empresas
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/listado-clientes-buscar")]
        public async Task<strListaSErr> CursosCursoDetalles_Grupos_BuscarCliente(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaSErr ret = new strListaSErr();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesgruposbuscarcliente", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposbuscarcliente-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/listado-docs-plantillas-buscar")]
        public async Task<strListaSErr> CursosCursoDetalles_Grupos_BuscarDocPlantillaGrupo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListaSErr ret = new strListaSErr();

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesgruposbuscardocplantillagrupo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaSErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesgruposbuscardocplantillagrupo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/documentosprivadosbuscarlista")]
        public async Task<strListaErr> CursosCursoDetalles_Grupos_ObtenerGrupoDocumentosPrivadosBuscarLista(string paramsin)
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

                strbuscarlistado dato = new strbuscarlistado();
                dato.buscar = buscar;
                dato.ID_Idi = ID_Idi;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesgruposobtenergrupodocdocspriv", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos-extranetcursoscursodetallesgruposobtenergrupodocdocspriv-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion

        #region "Recuperar datos - Get - Formadores"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/cursos/curso/formadores-obteber-detalles")]
        public async Task<strCursosCursoDetallesEditar_FormadoresEvaluacion> CursosCursoDetalles_FormadorEvaluacionDetalles(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursoDetallesEditar_FormadoresEvaluacion ret = new strCursosCursoDetallesEditar_FormadoresEvaluacion();

            try
            {
                Int32 ID_Curso2 = 0;
                try { ID_Curso2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Curso2")); } catch { }
                Int32 ID_Prof2 = 0;
                try { ID_Prof2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Prof2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Curso2;
                dato.datoD = Convert.ToDecimal(ID_Prof2);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursodetallesformadoresevaluaciondetalles", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCursosCursoDetallesEditar_FormadoresEvaluacion>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursodetallesformadoresevaluaciondetalles-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        #endregion

        #region "Recuperar datos - Get - Comunicacion"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/curso/comunicacion/buscardocpriv")]
        public async Task<strListaErr> CursosCursoComunicacion_DocumentosPrivadosBuscarLista(string paramsin)
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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursoscursocomunicaciondocumentosprivadosbuscarlista", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListaErr>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursoscursocomunicaciondocumentosprivadosbuscarlista-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }


        #endregion

        #endregion


        #endregion


        #endregion

        #region "Modulo Cursos - Exportar Datos"

        /* Exportacion datos */
        [Infrastructure.SessionControl]
        [Infrastructure.HttpRequest]
        [Route("modulo-cursos/exportar-datos")]
        public async Task<IActionResult> ExportacionDatos(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strModuloCursosExportarDatosInicio lis = new strModuloCursosExportarDatosInicio();

            try
            {
                Int32 ID_Idi = 0;
                try { ID_Idi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Idi")); } catch { }

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

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosexportardatosocupacionexportardatosinicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strModuloCursosExportarDatosInicio>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosexportardatosocupacionexportardatosinicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosExportarDatosInicioViewModel(lis);

            return View("CursosExportacionDatos", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/exportar-datos/ocupacion-buscar")]
        public async Task<strDato> CursosExportarDatos_OcupacionExportarDatosBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                Int32 ID_Estado = 0;
                try { ID_Estado = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Estado")); } catch { }
                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }
                Int32 Mes = 0;
                try { Mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string EnWeb = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnWeb");
                bool Plaza = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Plaza");
                if (txt == "true") Plaza = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Estado;
                bus.datoD = Convert.ToDecimal(Año);
                bus.datoS1 = Mes.ToString();
                bus.datoS2 = Fe_In;
                bus.datoS3 = Fe_Fi;
                bus.datoS4 = EnWeb;
                bus.datoB = Plaza;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosexportardatosocupacionexportardatosbuscar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos-extranetcursosexportardatosocupacionexportardatosbuscar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/exportar-datos/exportar/ocupacion")]
        public async Task<IActionResult> CursosExportarDatos_OcupacionCursosExportar(string paramsin) //FileResult
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();


            try
            {
                Int32 ID_Estado = 0;
                try { ID_Estado = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Estado")); } catch { }
                Int32 Año = 0;
                try { Año = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Año")); } catch { }
                Int32 Mes = 0;
                try { Mes = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Mes")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string EnWeb = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnWeb");
                bool Plaza = false;
                string txt = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Plaza");
                if (txt == "true") Plaza = true;

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Estado;
                bus.datoD = Convert.ToDecimal(Año);
                bus.datoS1 = Mes.ToString();
                bus.datoS2 = Fe_In;
                bus.datoS3 = Fe_Fi;
                bus.datoS4 = EnWeb;
                bus.datoB = Plaza;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursosexportardatosocupacioncursosexportar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
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

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosexportardatosocupacioncursosexportar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/exportar-datos/inscritos-buscar")]
        public async Task<strDato> CursosExportarDatos_InscritosExportarDatosBuscar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDato ret = new strDato();

            try
            {
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string Curso = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Curso");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Tipo;
                bus.datoS1 = Curso;
                bus.datoS2 = Fe_In;
                bus.datoS3 = Fe_Fi;
                bus.datoS4 = Pro;
                bus.datoS5 = Pob;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosexportardatosinscritosexportardatosbuscar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos-extranetcursosexportardatosinscritosexportardatosbuscar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/exportar-datos/exportar/inscritos")]
        public async Task<IActionResult> CursosExportarDatos_InscritosCursosExportar(string paramsin) //FileResult
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strFacturacionInformacionFichero lis = new strFacturacionInformacionFichero();


            try
            {
                Int32 ID_Tipo = 0;
                try { ID_Tipo = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo")); } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");
                string Curso = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Curso");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS bus = new strDatoS();
                bus.datoI = ID_Tipo;
                bus.datoS1 = Curso;
                bus.datoS2 = Fe_In;
                bus.datoS3 = Fe_Fi;
                bus.datoS4 = Pro;
                bus.datoS5 = Pob;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursosexportardatosinscritoscursosexportar", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());
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

                    lis = JsonConvert.DeserializeObject<strFacturacionInformacionFichero>(dat.obj.ToString());

                    HttpContext.Response.ContentType = "text/html;charset=utf-8";
                    ViewData["Html"] = Funciones.Funciones.ByteArrayToStringUtf8(lis.Fichero);

                    return View("html");
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosexportardatosinscritoscursosexportar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }



            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Content-Length", lis.Fichero.Length.ToString());

            var mediaType = new MediaTypeHeaderValue(lis.TipoMime);
            mediaType.Encoding = System.Text.Encoding.UTF8;

            return File(lis.Fichero, mediaType.ToString(), lis.Nombre);
        }

        #endregion


        #region "Modulo Cursos - Configuracion"

        // Cursos Configuracion
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion")]
        public async Task<IActionResult> CursosConfiguracion(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosConfiguracion", viewModel);
        }

        
        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/temarios")]
        public async Task<IActionResult> CursosTemarios(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosConfiguracionTemariosListado lis = new strCursosConfiguracionTemariosListado();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

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
                bus.numReg = 20;
                bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguraciontemarioslistados", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosConfiguracionTemariosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguraciontemarioslistados-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosConfiguracionTemariosListadoViewModel(lis);

            return View("CursosTemarios", viewModel);
        }


        #region "Ajax"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/temarios/temario/guardar")]
        public async Task<transportout> CursosConfiguracion_TemariosTemarioGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Temario2 = 0;
                try { ID_Temario2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Temario2")); } catch { }
                Int32 ID_Tipo2 = 0;
                try { ID_Tipo2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Tipo2")); } catch { }
                string Temario = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Temario");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosConfiguracionTemariosTemario dato = new strCursosConfiguracionTemariosTemario();
                dato.ID_Temario2 = ID_Temario2;
                dato.ID_Tipo2 = ID_Tipo2;
                dato.Temario = Temario;
                dato.Obs = Obs;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursosconfiguraciontemariostemarioguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguraciontemariostemarioguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/temarios/temario/nuevo")]
        public async Task<transportout> CursosConfiguracion_TemariosTemarioNuevo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Temario = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Temario");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = Temario;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetcursosconfiguraciontemariostemarionuevo", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguraciontemariostemarionuevo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/temarios/temario")]
        public async Task<strCursosConfiguracionTemariosTemario> CursosConfiguracion_TemariosTemarioDetalles(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosConfiguracionTemariosTemario ret = new strCursosConfiguracionTemariosTemario();

            try
            {
                Int32 ID_Temario2 = 0;
                try { ID_Temario2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Temario2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Temario2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguraciontemariostemariodetalles", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strCursosConfiguracionTemariosTemario>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguraciontemariostemariodetalles-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        #endregion


        #endregion








        // Cursos Configuracion - Asignaturas

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/asignaturas")]
        public async Task<IActionResult> CursosAsignaturas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosAsignaturas", viewModel);
        }

        // Cursos Configuracion - Titulaciones

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/titulaciones")]
        public async Task<IActionResult> CursosTitulaciones(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTitulaciones", viewModel);
        }

        // Cursos Configuracion - Tipos familias

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/familias-tipos-curso")]
        public async Task<IActionResult> CursosTiposFamilias(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTiposFamilias", viewModel);
        }

        // Cursos Configuracion - Tipos

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/tipos-curso")]
        public async Task<IActionResult> CursosTipos(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTipos", viewModel);
        }

        // Cursos Configuracion - Tipos asignaturas

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/tipos-asignaturas")]
        public async Task<IActionResult> CursosTiposAsignaturas(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTiposAsignaturas", viewModel);
        }

        // Cursos Configuracion - Tipos temarios

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/tipos-temarios")]
        public async Task<IActionResult> CursosTiposTemarios(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTiposTemarios", viewModel);
        }

        // Cursos Configuracion - Tipos titulaciones

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/tipos-titulaciones")]
        public async Task<IActionResult> CursosTiposTitulaciones(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTiposTitulaciones", viewModel);
        }

        // Cursos Configuracion - Tipos ubicaciones

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/tipos-ubicaciones")]
        public async Task<IActionResult> CursosTiposUbicaciones(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTiposUbicaciones", viewModel);
        }

        // Cursos Configuracion - Tipos horas

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/tipos-horas")]
        public async Task<IActionResult> CursosTiposHoras(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosTiposHoras", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/configuracion/formadores")]
        public async Task<IActionResult> CursosFormadores(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosCursosListado lis = new strCursosCursosListado();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);


                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                strDato bus = new strDato();
                Transporte.obj = bus;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosconfiguracioninicio", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosCursosListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosconfiguracioninicio-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosCursosListadoViewModel(lis);

            return View("CursosFormadores", viewModel);
        }

        // Cursos Configuracion - Plantillas


        #endregion

        #region "Modulo Cursos - Transacciones"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/transacciones")]
        public async Task<IActionResult> CursosTransaccionesListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListadoConPaginador lis = new strListadoConPaginador();

            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string Fe_In = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_In");
                string Fe_Fi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Fe_Fi");

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

                ViewData["Fe_In"] = Fe_In;
                ViewData["Fe_Fi"] = Fe_Fi;
                ViewData["det"] = null;
                //ViewData["ID_Idi"] = ID_Idi;

                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = "";
                bus.numReg = 50;
                bus.pagina = pag;
                strDatoS filtro = new strDatoS();
                filtro.datoS1 = Fe_In;
                filtro.datoS2 = Fe_Fi;
                bus.obj = filtro;
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cursos/extranetcursostransaccioneslistado/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strListadoConPaginador>(dat.obj.ToString());
                    filtro= JsonConvert.DeserializeObject<strDatoS>(lis.Filtros.ToString());
                    ViewData["Fe_In"] = filtro.datoS1;
                    ViewData["Fe_Fi"] = filtro.datoS2;
                    strCursosTransaccionesListadoDetalles[] det = null;
                    if (lis.det!=null)
                    {
                        for (Int32 t = 0; t < lis.det.Length; t++)
                        {
                            if (det == null) { det = new strCursosTransaccionesListadoDetalles[1]; }
                            else { Array.Resize(ref det, det.Length + 1); }
                            det[det.Length - 1] = JsonConvert.DeserializeObject<strCursosTransaccionesListadoDetalles>(lis.det[t].ToString());
                        }
                    }

                    ViewData["det"] = det;
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursostransaccioneslistado-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosTransaccionesListadoViewModel(lis);

            return View("CursosTransacciones", viewModel);
        }

        #endregion


        #region "Modulo Cursos - Ubicaciones"

        #region "Vistas"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/ubicaciones")]
        public async Task<IActionResult> CursosUbicacionesListado(string paramsin)
        {

            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosUbicacionesListado lis = new strCursosUbicacionesListado();


            try
            {
                Int32 pag = 0;
                try { pag = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "pag")); pag -= 1; } catch { }
                string buscar = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "buscar");

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

                ViewData["buscar"] = buscar;
                //ViewData["ID_Idi"] = ID_Idi;
                strbuscarlistado bus = new strbuscarlistado();
                bus.buscar = buscar;
                bus.numReg = 20;
                bus.pagina = pag;
                //bus.ID_Idi = ID_Idi;
                Transporte.obj = bus;

                var client = new HttpClient();
                var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cursos/extranetubicaciones/?paramsin=" + JsonConvert.SerializeObject(Transporte));
                dat = JsonConvert.DeserializeObject<transportout>(response);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosUbicacionesListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicaciones-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosUbicacionesListadoViewModel(lis);

            return View("CursosUbicacionesListado", viewModel);
        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("modulo-cursos/ubicaciones/ubicacion")]
        public async Task<IActionResult> CursosUbicacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strCursosUbicacionDetallesEditar lis = new strCursosUbicacionDetallesEditar();

            try
            {
                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato bus = new strDato();
                bus.datoI = ID_Ubi2;
                Transporte.obj = bus;

                strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
                ViewData["Migas"] = Migas.datoS1;
                ViewData["Form"] = Migas.datoS2;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetubicacion", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    lis = JsonConvert.DeserializeObject<strCursosUbicacionDetallesEditar>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicacion-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (lis.Err == null) { lis.Err = dat.err; }

            var viewModel = new ModuloCursosUbicacioDetallesViewModel(lis);

            return View("CursosUbicacion", viewModel);
        }

        // Cursos Dashboard
        //[Infrastructure.HttpRequest]
        //[Infrastructure.SessionControl]
        //[Route("modulo-cursos")]
        //public async Task<IActionResult> CursosDashboard(string paramsin)
        //{

        //    transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

        //    transportout dat = new transportout();
        //    dat.err = new strerror();

        //    strDato ret = new strDato();

        //    try
        //    {
        //        sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
        //        Int32 ID_Idi = 1;
        //        Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

        //        //Int32 ID_Ubi2 = 0;
        //        //try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.Params.ParamsKeys, Transporte.Params.ParamsValues, "ID_Ubi2")); } catch { }

        //        Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

        //        strDatoS Migas = Funciones.Funciones.FormatoMigas(Transporte);
        //        ViewData["Migas"] = Migas.datoS1;
        //        ViewData["Form"] = Migas.datoS2;

        //        //strDato bus = new strDato();
        //        //bus.datoI = ID_Ubi2;
        //        //Transporte.Obj = bus;

        //        //var client = new HttpClient();
        //        //var response = await client.GetStringAsync(Gaolos.Class.UrlApis.Url + "/cursos/cursos/?paramsin=" + JsonConvert.SerializeObject(Transporte));
        //        //dat = JsonConvert.DeserializeObject<TransportOut>(response);
        //        //if (!dat.Err.EsError)
        //        //{
        //        //    lis = JsonConvert.DeserializeObject<strCursosUbicacionDetallesEditar>(dat.Obj.ToString());
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/cursos-", logInfo = new logInterno { strSql = "", ex = ex } });
        //        dat.err.eserror = true;
        //        dat.err.mensaje = ex.Message;
        //    }

        //    //if (lis.Err == null) { lis.Err = dat.Err; }

        //    //var viewModel = new CursosUbicacioDetallesViewModel(lis);

        //    return View("CursosDashboard", null);
        //}


        #endregion


        #region "Ajax - Modulo Usuarios"

        #region "Guardar - Post"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/ubicacionnueva")]
        public async Task<transportout> CursosNuevaUbicacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                string Ubi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Ubi");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoS = Ubi;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetubicacionnueva", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicacionnueva-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/extranetubicacionguardar")]
        public async Task<transportout> CursoUbicacionGuardar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }
                string Ubi = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Ubi");
                string Obs = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Obs");
                Int32 ID_TipoUbi = 0;
                try { ID_TipoUbi = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_TipoUbi")); } catch { }
                string Codigo = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Codigo");
                string Contacto = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Contacto");
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Lat = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Lat");
                string Lng = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Lng");
                bool EnWeb = false;
                string txtEnWeb = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "EnWeb");
                if (txtEnWeb == "true") { EnWeb = true; }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strCursosUbicacionDetallesEditar dato = new strCursosUbicacionDetallesEditar();
                dato.ID_Ubi2 = ID_Ubi2;
                dato.Ubi = Ubi;
                dato.Obs = Obs;
                dato.ID_TipoUbi = ID_TipoUbi;
                dato.Codigo = Codigo;
                dato.Contacto = Contacto;
                dato.Dir = Dir;
                dato.Pob = Pob;
                dato.Pro = Pro;
                dato.CP = CP;
                dato.Pai = Pai;
                dato.Latitud = Lat;
                dato.Longitud = Lng;
                dato.EnWeb = EnWeb;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetubicacionguardar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicacionguardar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/ubicacioneliminar")]
        public async Task<transportout> CursosEliminarUbicacion(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Ubi2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetubicacioneliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicacioneliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/ubicacionvinculardocumento")]
        public async Task<transportout> CursosUbicacionVincularDocumento(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }
                Int32 ID_Doc = 0;
                try { ID_Doc = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Doc")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Ubi2;
                dato.datoD = Convert.ToDecimal(ID_Doc);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetubicacionvinculardocumento", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicacionvinculardocumento-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/ubicacionvinculardocumentoeliminar")]
        public async Task<transportout> CursosUbicacionVincularDocumentoEliminar(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            try
            {
                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }
                Int32 ID_Rel = 0;
                try { ID_Rel = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Rel")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDato dato = new strDato();
                dato.datoI = ID_Ubi2;
                dato.datoD = Convert.ToDecimal(ID_Rel);
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.PostResponseAsync("cursos/extranetubicacionvinculardocumentoeliminar", Transporte, HttpContext);
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicacionvinculardocumentoeliminar-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            return dat;

        }


        #endregion


        #region "Recuperar datos - Get"

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/extranetubicacioncursosubicaciongeo")]
        public async Task<strDatoS> CursosUbicacionesGeo(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strDatoS ret = new strDatoS();

            try
            {
                string Dir = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Dir");
                string Pai = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pai");
                string Pro = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pro");
                string CP = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "CP");
                string Pob = Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "Pob");

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_Idi = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_Idi);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strDatoS dato = new strDatoS();
                dato.datoS1 = Dir;
                dato.datoS2 = Pob;
                dato.datoS3 = CP;
                dato.datoS4 = Pro;
                dato.datoS5 = Pai;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetubicacioncursosubicaciongeo", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDatoS>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetubicacioncursosubicaciongeo-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/ubicacionlistadodocumentosrelacionados")]
        public async Task<strListado> CursosUbicacionListadoDocumentosRelacionados(string paramsin)
        {
            transportin Transporte = Funciones.Funciones.TransportIn(paramsin, HttpContext);

            transportout dat = new transportout();
            dat.err = new strerror();

            strListado ret = new strListado();

            try
            {
                Int32 ID_Ubi2 = 0;
                try { ID_Ubi2 = Convert.ToInt32(Funciones.Funciones.ObtenerKey(Transporte.parameters.ParamsKeys, Transporte.parameters.ParamsValues, "ID_Ubi2")); } catch { }

                sesionExtranet Ses = HttpContext.Session.GetObjectFromJson<sesionExtranet>("SesExtranet");
                Int32 ID_IdiPlataforma = 1;
                Int32.TryParse(HttpContext.Session.GetString("ID_IdiExtranet"), out ID_IdiPlataforma);

                Infrastructure.ReturnResults.PrepareTransport(ref Transporte, ref Ses, HttpContext);

                strbuscarlistado dato = new strbuscarlistado();
                dato.id = ID_Ubi2;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosubicacionlistadodocumentosrelacionados", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strListado>(dat.obj.ToString());
                }

            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos/extranetcursosubicacionlistadodocumentosrelacionados-", logInfo = new logInterno { strSql = "", ex = ex } });
                dat.err.eserror = true;
                dat.err.mensaje = ex.Message;
            }

            if (ret.Err == null) { ret.Err = dat.err; }

            return ret;

        }

        [Infrastructure.HttpRequest]
        [Infrastructure.SessionControl]
        [Route("cursos/documentospublicosbuscarlista")]
        public async Task<strDato> CursosDocumentosPublicosBuscar(string paramsin)
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
                dato.ID_Idi = ID_Idi;
                Transporte.obj = dato;

                dat = await Infrastructure.ReturnResults.GetResponseAsync("cursos/extranetcursosdocumentospublicosbuscarlista", Transporte, HttpContext);
                if (!dat.err.eserror)
                {
                    ret = JsonConvert.DeserializeObject<strDato>(dat.obj.ToString());
                }
            }
            catch (Exception ex)
            {
                LogBbdd.Write(new logPartial { nombreFichero = "Try-Ap-cursos-extranetcursosdocumentospublicosbuscarlista-", logInfo = new logInterno { strSql = "", ex = ex } });
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
