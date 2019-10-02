using System;
using System.Collections.Generic;
using Models;

namespace ModelsAndroid
{

    #region "Modulo - Android"

    public class strandroid_asistencia_nuevo_parte
    {
        public Int32 id_us { get; set; }
        public string token { get; set; }
        public strandroid_asistencia_offline offline { get; set; }

        public bool haypresup { get; set; }
        public string obspresup { get; set; }

        public bool nofin { get; set; }
        public string obsnofin { get; set; }

        public string reso { get; set; }
        public byte[] firma_cli { get; set; }
        public byte[] firma_ope { get; set; }

        public string cont { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
    }


    public class strandroid_operarios_listado
    {
        public strandroid_dato[] tecnicos { get; set; }
        public strandroid_dato[] agentes { get; set; }
        public strandroid_error err { get; set; }
    }

    public class strandroid_app_ini
    {
        public strandroid_servicios_listado[] serv { get; set; }
        public strandroid_mantenimientopreguntas_listado[] carac { get; set; }
        public strandroid_mantenimientopreguntas_listado[] man { get; set; }
        public strandroid_valorespreguntas_listado[] val { get; set; }
        public strandroid_marcas_listado[] marcas { get; set; }
        public strandroid_tipos_elementos_listado[] tipos { get; set; }
        public strandroid_tiposrelservicios_listado[] tiposrelserv { get; set; }
        public strandroid_tipos_subelementos_listado[] tipossubelem { get; set; }
        public strandroid_tipos_rel_subelementos_listado[] tiposrelsubelem { get; set; }
        public strandroid_tipos_subelementos_caracteristicas_listado[] tipossubelemcarac { get; set; }
        public strandroid_tipos_subelementos_mantenimiento_listado[] tipossubelemman { get; set; }
        public strandroid_error err { get; set; }
    }

    public class strandroid_servicios_listado
    {
        public Int32 id_serv2 { get; set; }
        public string codigo { get; set; }
        public string serv { get; set; }
        public decimal preciomin { get; set; }
        public decimal precio { get; set; }
    }

    public class strandroid_mantenimientopreguntas_listado
    {
        public Int32 id_caredef { get; set; }
        public string carac { get; set; }
        public bool libre { get; set; }
        public bool esaviso { get; set; }
        public Int32 id_planelem { get; set; }
        public bool esman { get; set; }
    }

    public class strandroid_valorespreguntas_listado
    {
        public Int32 id_valdef { get; set; }
        public Int32 id_caredef { get; set; }
        public string valor { get; set; }
        public bool valor_ok { get; set; }
    }

    public class strandroid_marcas_listado
    {
        public Int32 id_marca { get; set; }
        public Int32 id_planelem { get; set; }
        public string marca { get; set; }
    }

    public class strandroid_tipos_elementos_listado
    {
        public Int32 id_tipo2 { get; set; }
        public string tipo { get; set; }
        public bool subelemdisp { get; set; }
        public Int32 id_planelem { get; set; }
    }

    public class strandroid_tiposrelservicios_listado
    {
        public Int32 id_tipo2 { get; set; }
        public Int32 id_serv2 { get; set; }
        public string serv { get; set; }
    }

    public class strandroid_tipos_subelementos_listado
    {
        public Int32 id_sube { get; set; }
        public string subelem { get; set; }
    }

    public class strandroid_tipos_rel_subelementos_listado
    {
        public Int32 id_tipo2 { get; set; }
        public Int32 id_sube { get; set; }
    }

    public class strandroid_tipos_subelementos_caracteristicas_listado
    {
        public Int32 id_sube { get; set; }
        public Int32 id_caredef { get; set; }
    }

    public class strandroid_tipos_subelementos_mantenimiento_listado
    {
        public Int32 id_sube { get; set; }
        public Int32 id_caredef { get; set; }
        public bool esaviso { get; set; }
    }

    public class strandroid_asistencia_offline
    {
        public string fe_in { get; set; }
        // Info planificación 
        public Int32 tiempo { get; set; }
        public string usuconf { get; set; }
        public string fe_conf { get; set; }
        public string obs_conf { get; set; }

        // Info asistencia y dirección asistencia
        public bool man { get; set; }
        public string solicitadopor { get; set; }
        public Int32 id_asis2 { get; set; }
        public string asis_breve { get; set; }
        public string asis_expo { get; set; }
        public string asis_emp { get; set; }
        public string asis_dir { get; set; }
        public string asis_pob { get; set; }
        public string asis_pro { get; set; }
        public string asis_cp { get; set; }
        public string asis_pai { get; set; }
        public string asis_cont { get; set; }
        public string asis_tel { get; set; }
        public string asis_obs { get; set; }
        public string asis_horario { get; set; }
        public string asis_lat { get; set; }
        public string asis_lon { get; set; }
        public string asis_cat { get; set; }

        // datos cliente
        public Int32 cli_id_cli2 { get; set; }
        public string cli_cli { get; set; }
        public strandroid_dato[] cli_mails { get; set; }
        public strandroid_dato[] cli_tels { get; set; }

        // datos empresa relacionada
        public Int32 clirel_id_cli2 { get; set; }
        public string clirel_cli { get; set; }
        public string clirel_dir { get; set; }
        public string clirel_pro { get; set; }
        public string clirel_cp { get; set; }
        public string clirel_pob { get; set; }
        public string clirel_pai { get; set; }
        public string clirel_dirobs { get; set; }
        public string clirel_diremp { get; set; }
        public string clirel_dirhor { get; set; }
        public string clirel_dirlat { get; set; }
        public string clirel_dirlon { get; set; }
        public strandroid_dato[] clirel_mails { get; set; }
        public strandroid_dato[] clirel_tels { get; set; }

        // Precios especiales
        public strandroid_dato[] cli_precios { get; set; }

        // Mantenimiento
        public Int32 id_man { get; set; }
        public Int32 id_man_plan { get; set; }
        public Int32 id_man_plan2 { get; set; }
        public bool maninicial { get; set; }
        public string usumanini { get; set; }
        public string fe_man_ini { get; set; }
        public Int32 perifac { get; set; }
        public Int32 perivis { get; set; }
        public string manobs { get; set; }
        public string manrefcli { get; set; }
        public strandroid_asistencia_man_det[] mandet { get; set; }
        public strandroid_asistencia_man_det_sub[] mandetsub { get; set; }

        public strandroid_asistencias_historial[] his { get; set; }

        public strandroid_servicios_listado[] servs { get; set; }

        public strandroid_error err { get; set; }
    }

    public class strandroid_asistencia_man_det
    {
        public Int32 id_planelem { get; set; }
        public Int32 id_manplandet { get; set; }
        public Int32 id_mandet { get; set; }

        public Int32 id_elem { get; set; }
        public Int32 id_tipo2 { get; set; }
        public string elem_tipo { get; set; }
        public string elem_refcli { get; set; }
        public string elem_ubi { get; set; }
        public string elem_bastidor { get; set; }
        public Int32 elem_año { get; set; }
        public Int32 elem_id_marca2 { get; set; }
        public bool fin { get; set; }
        public Int32 num { get; set; }
        public bool haysubelem { get; set; }
        public Int32 id_serv2 { get; set; }
    }

    public class strandroid_asistencia_man_det_sub
    {
        public Int32 id_subelem { get; set; }
        public Int32 id_sube { get; set; }
        public Int32 id_elem { get; set; }
        public Int32 num { get; set; }
        public Int32 id_marca { get; set; }
        public bool fin { get; set; }
    }

    public class strandroid_asistencias_historial
    {
        public Int32 id_asis2 { get; set; }
        public string cat { get; set; }
        public string fe_cerr { get; set; }
        public string usu_cerr { get; set; }
        public string reso { get; set; }
        public bool man { get; set; }
    }

    public class strandroid_asistencias_operario
    {
        public strandroid_asistencias_operario_detalles[] asis { get; set; }
        //public strDatoS[] Dias { get; set; }
        public strandroid_error err { get; set; }

    }

    public class strandroid_asistencias_operario_detalles
    {
        public Int32 id_asis2 { get; set; }
        public string dir { get; set; }
        public string emp { get; set; }
        public string pob { get; set; }
        public string cp { get; set; }
        public string adm { get; set; }
        public string tipo { get; set; }
        public bool urg { get; set; }
        public bool man { get; set; }
        public string cont { get; set; }
        public string tel { get; set; }
        public string obs { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string expo { get; set; }
        public Int32 id_asisconf { get; set; }
    }



    public class strandroid_lista
    {
        public Int32 id { get; set; }
        public string det { get; set; }
    }

    public class strandroid_dato
    {
        public Int32 dato_i { get; set; }
        public string dato_s1 { get; set; }
        public string dato_s2 { get; set; }
        public string dato_s3 { get; set; }
        public decimal dato_d { get; set; }
    }

    public class strandroid_error
    {
        public string mensaje { get; set; }
        public string titulo { get; set; }
        public bool eserror { get; set; }
        public bool salir { get; set; }
    }

    #endregion
}
