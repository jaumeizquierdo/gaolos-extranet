using System;
using System.Collections.Generic;

namespace Models
{

    #region "Modulo Correspondencia"

    public class strCorrespondenciaCorrespondenciaListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strCorrespondenciaCorrespondenciaListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strCorrespondenciaCorrespondenciaListadoDetalles
    {
        public Int32 ID_Corr { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Fac { get; set; }
        public string ID_Fac { get; set; }
        public string Fe_Fa { get; set; }
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string ID_Dir { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
        public string Motivo { get; set; }
        public strDatoS[] ops { get; set; }
    }

    #endregion

    #region "Modulo Articulos"

    #region "Modulo Artículos - Articulos - Articulo"

    public class strArticulosArticuloCabecera
    {
        public string ID_Art2 { get; set; }
        public string Art { get; set; }
        public string Codigo { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
    }

    public class strArticulosArticulo_General
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public Int32 ID_Art2 { get; set; }
        public string Art { get; set; }
        public string Codigo { get; set; }
        public string Sub { get; set; }
        public string Sub2 { get; set; }
        public Int32 ID_Cat2 { get; set; }
        public Int32 ID_Fab2 { get; set; }
        public Int32 ID_Ref2 { get; set; }
        public string Co_Ba { get; set; }
        public Int32 ID_Uni { get; set; }
        public string Obs { get; set; }
        public strLista[] Cats { get; set; }
        public strLista[] Fabs { get; set; }
        public strLista[] Refs { get; set; }
        public strLista[] Unids { get; set; }

        public strerror Err { get; set; }
    }

    #region "Modulo Artículos - Articulos - Articulo - Precios"

    public class strArticulosArticulo_Precios
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public string Precio { get; set; }
        public string PrecioMin { get; set; }
        public string IVA { get; set; }
        public Int32 numTafi { get; set; }
        public strDatoS[] Tarifas { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Artículos - Articulos - Articulo - Proveedores"

    public class strArticulosArticulo_Proveedores
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public Int32 numProv { get; set; }
        public strArticulosArticulo_ProveedoresDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strArticulosArticulo_ProveedoresDetalles
    {
        public Int32 ID_SusA { get; set; }
        public string Fe_Val { get; set; }
        public string ID_Prov2 { get; set; }
        public string Prov { get; set; }
        public string CodProv { get; set; }
        public string PVRProv { get; set; }
        public string StockProv { get; set; }
        public string CosteProv { get; set; }
        public bool Coti { get; set; }

        public string UsuVal { get; set; }
        public string ArtProv { get; set; }
        public string Fe_UlComp { get; set; }
        public string Coste_UlComp { get; set; }
        public string ObsProv { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Artículos - Articulos - Articulo - Control"

    public class strArticulosArticulo_Control
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public bool CtrlSN { get; set; }
        public bool SN_Lock { get; set; }
        public bool CtrlLote { get; set; }
        public bool Lote_Lock { get; set; }
        public string Garan { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Artículos - Articulos - Articulo - Coste"

    public class strArticulosArticulo_Coste
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public string Coste { get; set; }
        public string CosteE { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Artículos - Articulos - Articulo - Transporte"

    public class strArticulosArticulo_Transporte
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public string Peso { get; set; }
        public bool Fragil { get; set; }
        public bool ADR { get; set; }
        public string Ancho { get; set; }
        public string Alto { get; set; }
        public string Largo { get; set; }
        public bool NoPortesGratis { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Artículos - Articulos - Articulo - Stock"

    public class strArticulosArticulo_Stock
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public string Can { get; set; }
        public string Stock { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Artículos - Articulos - Articulo - Almacenes"

    public class strArticulosArticulo_Almacenes
    {
        public strArticulosArticuloCabecera Cab { get; set; }

        public Int32 numAlm { get; set; }
        public strArticulosArticulo_AlmacenesDetalles[] Alms { get; set; }

        public strerror Err { get; set; }
    }

    public class strArticulosArticulo_AlmacenesDetalles
    {
        public Int32 ID_EAS { get; set; }
        public Int32 ID_Alm { get; set; }
        public string Alm { get; set; }

        public string Loc { get; set; }
        public bool NoImpEti { get; set; }
        public string Can { get; set; }
    }

    #endregion

    #endregion


    #region "Modulo Articulos - Articulos" 

    public class strArticulosArticulosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strArticulosArticulosListadoDetalles[] det { get; set; }
        public strLista[] Cat { get; set; }
        public strLista[] Fam { get; set; }
        public strerror Err { get; set; }
    }
    public class strArticulosArticulosListadoDetalles
    {
        public Int32 ID_Art2 { get; set; }
        public string Art { get; set; }
        public string Codigo { get; set; }
        public string Cat { get; set; }
        public string Fam { get; set; }
        public string Stock { get; set; }
        public string Precio { get; set; }
    }

    #endregion

    #region "Modulo Articulos - Configuracion - Categorias

    public class strArticulosArticulosCategoria
    {
        public Int32 ID_Cat2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Cat { get; set; }
        public Int32 ID_Fam2 { get; set; }
        public string Obs { get; set; }
        public strLista[] det { get; set; }
        public string NoEli { get; set; }
        public string WarnEli { get; set; }
        public strerror Err { get; set; }
    }

    public class strArticulosArticulosCategoriasListadoDetalles
    {
        public Int32 ID_Cat2 { get; set; }
        public string Cat { get; set; }
        public string Obs { get; set; }
        public string Fam { get; set; }
        public string numArt { get; set; }
    }

    #endregion

    #region "Modulo Articulos - Configuración - Familias"

    public class strArticulosArticulosFamiliasListadoDetalles
    {
        public Int32 ID_Fam2 { get; set; }
        public string Fam { get; set; }
        public string Obs { get; set; }
        public string numCat { get; set; }
        public string numArt { get; set; }
    }

    public class strArticulosArticulosFamilia
    {
        public Int32 ID_Fam2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Fam { get; set; }
        public string Obs { get; set; }
        public string NoEli { get; set; }
        public string WarnEli { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Articulos - Configuración - Fabricantes"

    public class strArticulosArticulosFabricantesListadoDetalles
    {
        public Int32 ID_Fab2 { get; set; }
        public string Fab { get; set; }
        public string Obs { get; set; }
        public string numCat { get; set; }
        public string numArt { get; set; }
    }

    public class strArticulosArticulosFabricante
    {
        public Int32 ID_Fab2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Fab { get; set; }
        public string Obs { get; set; }
        public string NoEli { get; set; }
        public string WarnEli { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #endregion


    #region "Modulo Pedidos proveedores"

    #region "Modulo Pedidos proveedores - Borradores"

    public class strPedidosProveedoresBorradoresListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strPedidosProveedoresBorradoresListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strPedidosProveedoresBorradoresListadoDetalles
    {
        public Int32 ID_Ped2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string ID_Prov2 { get; set; }
        public string Prov { get; set; }
        public string Breve { get; set; }
        public string Obs { get; set; }
        public string Total { get; set; }
        public bool Env { get; set; }
        public string ID_Soli2 { get; set; }
        public bool Blo { get; set; }
    }

    #endregion

    #endregion


    #region "Modulo Proveedores"

    #region "Modulo Proveedores - Proveedores"

    public class strProveedoresProveedoresListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strProveedoresProveedoresListadoDetalles[] det { get; set; }
        public strLista[] TiposProv { get; set; }

        public strerror Err { get; set; }
    }
    public class strProveedoresProveedoresListadoDetalles
    {
        public string ID_Prov2 { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pai { get; set; }
        public string NIF { get; set; }
        public string Tel { get; set; }
        public bool Blo { get; set; }
    }

    #endregion

    #region "Modulo Proveedores - Facturas proveedores"

    public class strProveedoresProveedoresFacturasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strLista[] Años { get; set; }
        public strLista[] Meses { get; set; }
        public strProveedoresProveedoresFacturasListadoDetalles[] det { get; set; }
        public Int32 ID_Prov2 { get; set; }
        public string Prov { get; set; }

        public strerror Err { get; set; }
    }
    public class strProveedoresProveedoresFacturasListadoDetalles
    {
        public Int32 ID_FacRec2 { get; set; }
        public string NumFac { get; set; }
        public string Fe_Fa { get; set; }
        public string Emp { get; set; }
        public string NIF { get; set; }
        public string ID_Prov2 { get; set; }
        public string CCProv { get; set; }
        public string ID_Doc { get; set; }
        public bool Conta { get; set; }
        public string Total { get; set; }
    }

    public class strProveedoresProveedoresFacturasInformacionFactura
    {
        public Int32 ID_FacRec2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }

        public string Fe_Fa { get; set; }
        public string NumFac { get; set; }
        public string Emp { get; set; }
        public string Emp_Fis { get; set; }
        public string Dir { get; set; }
        public string Pai { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pob { get; set; }



        public string Total { get; set; }
        public string ObsFac { get; set; }
        public bool SiConta { get; set; }
        public string NIF { get; set; }
        public string CCProv { get; set; }
        public string ID_Prov2 { get; set; }
        public bool HayDoc { get; set; }
        public string ID_Doc { get; set; }

        public strProveedoresProveedoresFacturasInformacionFacturaFracciones[] Fra { get; set; }

        public Int32 ID_Soli2 { get; set; }

        public strerror Err { get; set; }
    }

    public class strProveedoresProveedoresFacturasInformacionFacturaFracciones
    {
        public Int32 ID_FacDet { get; set; }
        public string Forma { get; set; }
        public string Cue { get; set; }
        public string Fe_Ve { get; set; }
        public string Imp { get; set; }
        public string Obs { get; set; }
        public string Fe_Pag { get; set; }
        public bool EsConta { get; set; }
        public bool EsConc { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo Pedidos Proveedores"

    #region "Modulo Pedidos proveedores - Compras"

    public class strPedidosProveedoresComprasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strPedidosProveedoresComprasListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strPedidosProveedoresComprasListadoDetalles
    {
        public Int32 ID_Art2 { get; set; }
        public string Articulo { get; set; }
        public string Codigo { get; set; }
        public string Disp { get; set; }
        public string Min { get; set; }
        public string PedCli { get; set; }
        public string PedProv { get; set; }
        public string Faltan { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo Control Horario"

    #region "Modulo Control Horario - Hoy"

    public class strControlHorarioHoyListado
    {
        public strDatoS[] Hoy { get; set; }
        public strDatoS[] Res { get; set; }
        public strDatoS[] Inci { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Control Horario - Consultas - Resume horas"

    public class strControlHorarioResumenHorasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strControlHorarioResumenHorasListadoDetalles[] det { get; set; }

        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public Int32 ID_Us_Alta { get; set; }
        public string UsuAlta { get; set; }

        public strDatoS[] res { get; set; }

        public strerror Err { get; set; }
    }
    public class strControlHorarioResumenHorasListadoDetalles
    {
        public string Dia { get; set; }

        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public string Usu { get; set; }
        public string Horas { get; set; }
        public string Min { get; set; }
    }

    #endregion

    #endregion



    #region "Modulo Taller"

    #region "Modulo Taller - Taller

    public class strTallerTallerListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTallerTallerListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strTallerTallerListadoDetalles
    {
        public Int32 ID_Ent2 { get; set; }
        public string ID_Cli2 { get; set; }
        public string Fe_En { get; set; }
        public string Emp { get; set; }
        public string Mat { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
        public string Obs { get; set; }
        public string Km { get; set; }
        public string ID_Al2 { get; set; }
    }

    #endregion


    #endregion


    #region "Modulo Transporte"

    #region "Modulo Transporte - Tipo de envío

    public class strTransporteTransporteTipoEnvioListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTransporteTransporteTipoEnvioListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strTransporteTransporteTipoEnvioListadoDetalles
    {
        public string ID_Tipo { get; set; }
        public string Tipo { get; set; }
        public string Obs { get; set; }
        public string HoraCorte { get; set; }
        public string ImpGratis { get; set; }
        public string NumMod { get; set; }
    }

    public class strTransporteTransporteTipoEnvioDetalles
    {
        public Int32 ID_Tipo { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Tipo { get; set; }
        public string Obs { get; set; }
        public string HoraCorte { get; set; }
        public string ImpGratis { get; set; }
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strDatoS[] det { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Transporte - Modalidades

    public class strTransporteTransporteModalidadesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTransporteTransporteModalidadesListadoDetalles[] det { get; set; }
        public string Pai { get; set; }
        public Int32 ID_Pai { get; set; }

        public strerror Err { get; set; }
    }
    public class strTransporteTransporteModalidadesListadoDetalles
    {
        public Int32 ID_Mod { get; set; }
        public string Trans { get; set; }
        public string Tipo { get; set; }
        public string InfoCli { get; set; }
        public string InfoPriv { get; set; }
        public string DuradaH { get; set; }
        public string NumMod { get; set; }
    }

    public class strTransporteTransporteModalidadesModalidad
    {
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public Int32 ID_Tipo { get; set; }
        public string Tipo { get; set; }
        public Int32 DuradaH { get; set; }
        public string InfoCli { get; set; }
        public string InfoPriv { get; set; }
        public Int32 ID_Prov2 { get; set; }
        public string Trans { get; set; }
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTransporteTransporteModalidadesModalidadDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strTransporteTransporteModalidadesModalidadDetalles
    {
        public Int32 ID_Det { get; set; }
        public string Trans { get; set; }
        public string Tipo { get; set; }
        public string Pai_Ori { get; set; }
        public string Pai_Des { get; set; }
        public string Est_Ori { get; set; }
        public string Est_Des { get; set; }
        public string Reg_Ori { get; set; }
        public string Reg_Des { get; set; }
        public string Pro_Ori { get; set; }
        public string Pro_Des { get; set; }
        public string CP_Ori { get; set; }
        public string CP_Des { get; set; }
        public string Pob_Ori { get; set; }
        public string Pob_Des { get; set; }
        public bool EstIgual { get; set; }
        public bool RegIgual { get; set; }
        public bool ProIgual { get; set; }
        public string Nota { get; set; }
        public string Tarifa { get; set; }
    }

    public class strTransporteTransporteModalidadesModalidadLocalizacion
    {
        public Int32 ID_Mod { get; set; }
        public Int32 ID_Tarifa { get; set; }
        public Int32 ID_PaiOri { get; set; }
        public Int32 ID_EstOri { get; set; }
        public Int32 ID_RegOri { get; set; }
        public Int32 ID_ProOri { get; set; }
        public Int32 ID_PobOri { get; set; }
        public string CPOri { get; set; }
        public Int32 ID_PaiDes { get; set; }
        public Int32 ID_EstDes { get; set; }
        public Int32 ID_RegDes { get; set; }
        public Int32 ID_ProDes { get; set; }
        public Int32 ID_PobDes { get; set; }
        public string CPDes { get; set; }
        public bool EstIgual { get; set; }
        public bool RegIgual { get; set; }
        public bool ProIgual { get; set; }
        public string Nota { get; set; }
    }

    #endregion

    #region "Modulo Transporte - Localizaciones

    public class strTransporteTransporteLocalizacionesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTransporteTransporteLocalizacionesListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strTransporteTransporteLocalizacionesListadoDetalles
    {
        public Int32 ID_Det { get; set; }
        public string Trans { get; set; }
        public string Tipo { get; set; }
        public string Pai_Ori { get; set; }
        public string Pai_Des { get; set; }
        public string Est_Ori { get; set; }
        public string Est_Des { get; set; }
        public string Reg_Ori { get; set; }
        public string Reg_Des { get; set; }
        public string Pro_Ori { get; set; }
        public string Pro_Des { get; set; }
        public string CP_Ori { get; set; }
        public string CP_Des { get; set; }
        public string Pob_Ori { get; set; }
        public string Pob_Des { get; set; }
        public bool EstIgual { get; set; }
        public bool RegIgual { get; set; }
        public bool ProIgual { get; set; }
        public string Nota { get; set; }
    }

    #endregion

    #region "Modulo Transporte - Tarifas

    public class strTransporteTransporteTarifasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTransporteTransporteTarifasListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strTransporteTransporteTarifasListadoDetalles
    {
        public Int32 ID_Tar { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Trans { get; set; }
        public string Tarifa { get; set; }
        public string Obs { get; set; }
        public string Num { get; set; }
    }

    public class strTransporteTransporteTarifasTarifaPrecios
    {
        public Int32 ID_Tarifa { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Tarifa { get; set; }
        public string Obs { get; set; }
        public string ID_Prov2 { get; set; }
        public string Trans { get; set; }
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strDatoS[] det { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #endregion


    #region "Modulo Firewall"

    public class strFireWallIPsListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strFireWallIPsListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strFireWallIPsListadoDetalles
    {
        public Int32 ID_IPs { get; set; }
        public string IP { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Expo { get; set; }
        public bool White { get; set; }
        public bool Black { get; set; }
    }

    public class strFireWallAppListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strFireWallAppListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strFireWallAppListadoDetalles
    {
        public Int32 ID_IPa { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string App { get; set; }
        public string ExpoA { get; set; }
        public string IP { get; set; }
        public string Expo { get; set; }
        public bool Black { get; set; }
        public bool Act { get; set; }
    }

    public class strFireWallAppNuevo
    {
        public strLista[] IPs { get; set; }
        public strLista[] App { get; set; }

        public strerror Err { get; set; }
    }

    public class strFireWallRulesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strFireWallRulesListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strFireWallRulesListadoDetalles
    {
        public Int32 ID_Rule { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string App { get; set; }
        public string IP { get; set; }
        public string ExpoIP { get; set; }
        public string ExpoR { get; set; }
        public bool Blo { get; set; }
    }

    public class strFireWallGruposListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strFireWallGruposListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strFireWallGruposListadoDetalles
    {
        public Int32 ID_Grupo { get; set; }
        public string Grupo { get; set; }
        public string Obs { get; set; }
        public string numUsus { get; set; }
        public string numRules { get; set; }
    }

    public class strFireWallGruposGrupoInfo
    {
        public strDatoS[] RulesGrupo { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Gaolos Mantenimiento - Modulos"

    public class strGaolosMantenimientoModulosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strGaolosMantenimientoModulosListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strGaolosMantenimientoModulosListadoDetalles
    {
        public string ID_Modulo { get; set; }
        public string Fe_Al { get; set; }
        public string Icono { get; set; }
        public string Modulo { get; set; }
        public bool EsGlobal { get; set; }
        public bool EsGaolos { get; set; }
        public bool Base { get; set; }
        public bool Comun { get; set; }
        public bool Oculto { get; set; }
        public string Num { get; set; }
        public string Num2 { get; set; }
    }

    public class strGaolosMantenimientoModulosModulo
    {
        public Int32 ID_Modulo { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Modulo { get; set; }
        public string Vista { get; set; }
        public string Icono { get; set; }
        public bool EsGlobal { get; set; }
        public bool EsGaolos { get; set; }
        public bool Base { get; set; }
        public bool Comun { get; set; }
        public bool Oculto { get; set; }
        public string Expo { get; set; }
        public string Obs { get; set; }
        public string App { get; set; }
        public string IconoSvg { get; set; }
        public strGaolosMantenimientoModulosModuloDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strGaolosMantenimientoModulosModuloDetalles
    {
        public string ID_Apa { get; set; }
        public string Fe_Al { get; set; }
        public string Apa { get; set; }
        public string Llamada { get; set; }
        public string Orden { get; set; }
        public bool Oculto { get; set; }
        public string Num { get; set; }
        public bool DSA { get; set; }
    }

    public class strGaolosMantenimientoModulosModuloApartado
    {
        public Int32 ID_Apa { get; set; }
        public Int32 ID_Modulo { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Modulo { get; set; }
        public string Apa { get; set; }
        public string Expo { get; set; }
        public string Obs { get; set; }
        public bool Oculto { get; set; }
        public string Llamada { get; set; }
        public string IconoSvg { get; set; }
        public bool DSA { get; set; }
        public strGaolosMantenimientoModulosModuloApartadoDetalles[] det { get; set; }
        public Int32 numR { get; set; }
        public Int32 numW { get; set; }

        public strerror Err { get; set; }
    }
    public class strGaolosMantenimientoModulosModuloApartadoDetalles
    {
        public string ID_Aci { get; set; }
        public string Fe_Al { get; set; }
        public string Rest { get; set; }
        public string Obs { get; set; }
        public bool EsSet { get; set; }
    }


    #endregion

    #region "Dashboard"

    public class strDashBoard
    {
        public string Modulo { get; set; }
        public string Obs { get; set; }
        public string Icono { get; set; }
        public string IconoSvg { get; set; }
        public strDashBoardApartados[] det { get; set; }
        public object Aux { get; set; }

        public strerror Err { get; set; }
    }
    public class strDashBoardApartados
    {
        public string Apartado { get; set; }
        public string Llamada { get; set; }
        public string Obs { get; set; }
        public string IconoSvg { get; set; }
    }

    #endregion

    #region "Modulo - Android"

    public class strAndroidDispositivosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAndroidDispositivosListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strAndroidDispositivosListadoDetalles
    {
        public Int32 ID_Token { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Tipo { get; set; }
        public string Obs { get; set; }
        public string Fe_Auto { get; set; }
        public string Usu_Auto { get; set; }
        public string Fe_Blo { get; set; }
        public string Usu_Blo { get; set; }
        public string MotBlo { get; set; }
        public string Fe_Ul_Acc { get; set; }
        public string Num_Acc { get; set; }
        public string UsuUlAcc { get; set; }
        public bool SinUso { get; set; }
    }

    #endregion

    #region "Modulo Presupuestos"

    #region "Modulo Presupuestos - Presupuestos"

    public class strPresupuestosPresupuestosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public Int32 ID_Tipo { get; set; }
        public Int32 ID_Us_Agente { get; set; }
        public string Agente { get; set; }
        public strPresupuestosPresupuestosListadoDetalles[] det { get; set; }

        public strLista[] Meses { get; set; }
        public Int32 Mes { get; set; }
        public strLista[] Años { get; set; }
        public Int32 Año { get; set; }

        public strerror Err { get; set; }
    }
    public class strPresupuestosPresupuestosListadoDetalles
    {
        public string ID_Pres2 { get; set; }

        public string Fe_Al { get; set; }
        public string Agente { get; set; }
        public string Tipo { get; set; }
        public string Breve { get; set; }

        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }

        public string Total { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }

        public bool Baja { get; set; }
        public bool Blo { get; set; }
        public bool HayCob { get; set; }
        public bool HayDev { get; set; }
        public string NumEnv { get; set; }

        public string Procesado { get; set; }
        public bool Aceptado { get; set; }

        public string ID_Soli2 { get; set; }
        public bool SoliCerr { get; set; }

        public bool Borrador { get; set; }

        public string Sep { get; set; }
    }

    #endregion

    #region "Modulo Presupuestos - Aceptados"

    public class strPresupuestosPresupuestosAceptadosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strPresupuestosPresupuestosAceptadosListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strPresupuestosPresupuestosAceptadosListadoDetalles
    {
        public string ID_PresAcep { get; set; }
        public string ID_Pres2 { get; set; }

        public string Fe_Al { get; set; }
        public string Emp { get; set; }
        public string EmpRel { get; set; }
        public string Usu { get; set; }

        public bool ViaWeb { get; set; }
        public bool EsCli { get; set; }
        public string Total { get; set; }
        public string Breve { get; set; }

        public string ID_Soli2 { get; set; }
        public bool SoliCerr { get; set; }

        public string Sep { get; set; }
    }

    public class strPresupuestosPresupuestoAceptadoEstado
    {
        public string select_opcion_txt { get; set; }

        public Int32 ID_Pres2 { get; set; }
        public bool Validar { get; set; }
        public Int32 ID_Cont2 { get; set; }
        public string Emp { get; set; }
        public string Emp_Fis { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }
        public string NIF { get; set; }
        public string Obs { get; set; }
        public string Agente { get; set; }
        public Int32 ID_For { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }
        public strDatoS[] Webs { get; set; }
        public strDatoS[] Usus { get; set; }
        public strDatoS[] EmpRels { get; set; }
        public bool Man { get; set; }

        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestoAceptadoValidarContacto
    {
        public Int32 ID_PresAcep { get; set; }
        public string Emp { get; set; }
        public string Emp_Fis { get; set; }
        public string Dir { get; set; }
        public string NIF { get; set; }
        public string Obs { get; set; }
        public string ObsAdm { get; set; }
        public Int32 ID_For { get; set; }
        public string IBAN { get; set; }
    }

    public class strPresupuestosPresupuestoAceptadoAccionAsistencia
    {
        public Int32 ID_Cli2 { get; set; }
        public Int32 ID_Pres2 { get; set; }
        public string Emp { get; set; }
        public string Obs { get; set; }

        public strDatoS[] Tels { get; set; }
        public strLista[] Dirs { get; set; }
        public strLista[] Tipos { get; set; }

        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestoAceptadoAccionAsistenciaGenerar
    {
        public Int32 ID_PresAcep { get; set; }
        public string RefCli { get; set; }
        public Int32 ID_Tipo { get; set; }
        public string Expo { get; set; }

        public bool Urg { get; set; }
        public bool CliParado { get; set; }
        public string Emp { get; set; }
        public Int32 ID_Dir { get; set; }
        public string Horario { get; set; }
        public bool GuaHor { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
        public string Obs { get; set; }
    }

    public class strPresupuestosPresupuestoAceptadoAccionMantenimiento
    {
        public Int32 ID_Pres2 { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public Int32 ID_Dir { get; set; }
        public Int32 ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
        public string TipoEmpRel { get; set; }
        public string Fe_In { get; set; }

        public string ObsPriv { get; set; }

        public strLista[] PeriVis { get; set; }
        public strLista[] PeriFac { get; set; }
        public strLista[] Dirs { get; set; }
        public strDatoS[] Tels { get; set; }


        public strDato[] Man { get; set; }
        public strDato[] Otros { get; set; }

        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestoAceptadoAccionMantenimientoGenerar
    {
        public Int32 ID_PresAcep { get; set; }
        public string RefCli { get; set; }
        public string Fe_In { get; set; }
        public Int32 PeriVis { get; set; }
        public Int32 PeriFac { get; set; }
        public string Emp { get; set; }
        public Int32 ID_Dir { get; set; }
        public string Horario { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
        public string ObsUbi { get; set; }
        public string ObsPriv { get; set; }
        public string ObsPub { get; set; }
    }

    #endregion

    #region "Modulo Presupuestos - Presupuestos - Presupuesto"

    public class strPresupuestosPresupuestosPresupuesto
    {
        public Int32 ID_Pres2 { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string UsuAsi { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string Cli2Rel { get; set; }
        public string Cli2RelTipo { get; set; }
        public bool IVAInc { get; set; }
        public string ExpoPres { get; set; }
        public string UsuCom { get; set; }
        public bool HayTiposPres { get; set; }
        public string TipoPres { get; set; }
        public string Breve { get; set; }
        public string Emp { get; set; }
        public string Att { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string Dir { get; set; }
        public string Pai { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pob { get; set; }
        public string TipoIVA { get; set; }
        public string ObsPriv { get; set; }
        public string Obs { get; set; }
        public string Base { get; set; }
        public string Total { get; set; }
        public string Gan { get; set; }

        public string NumPresPend { get; set; }
        public string NumPresTotal { get; set; }
        public Int32 NumEnv { get; set; }

        public bool EsBorrador { get; set; }

        public strPresupuestosPresupuestosPresupuestoSolicitud Soli { get; set; }

        public string Fe_Cerr { get; set; }
        public bool Aceptado { get; set; }
        public string UsuPro { get; set; }
        public string ExpoRes { get; set; }
        public string PVPEspciales { get; set; }

        public string ID_Al { get; set; }
        public string ID_Al2 { get; set; }
        public string TotalAlb { get; set; }
        public string ID_Fac { get; set; }
        public string Fac { get; set; }

        public strPresupuestosPresupuestosPresupuestoProduccion Prod { get; set; }

        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestosPresupuestoSolicitud
    {
        // Solicitud
        public Int32 ID_Soli2 { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string Url { get; set; }
        public string NumTwits { get; set; }
        public string Expo { get; set; }
        public string Fe_Cerr { get; set; }
        public string UsuCerr { get; set; }

        // Twit
        public string Fe_Ul { get; set; }
        public string Fe_Prev { get; set; }
        public string UsuUl { get; set; }
        public string UsuAsi { get; set; }
        public string ExpoUl { get; set; }
    }

    public class strPresupuestosPresupuestosPresupuestoProduccion
    {
        public string[] ID_Ord2 { get; set; }
        public strDatoS[] Albs { get; set; }
        public strDatoS[] Res { get; set; }

    }

    public class strPresupuestosPresupuestoAjax
    {
        public string Base { get; set; }
        public string Total { get; set; }
        public string Gan { get; set; }
        public string Bruto { get; set; }
        public string ImpDto { get; set; }
        public bool Cerrado { get; set; }

        public strPresupuestosPresupuestoDetallesAjax[] det { get; set; }

        public strDatoS[] Imp { get; set; }

        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestoDetallesAjax
    {
        public Int32 ID_NPCD { get; set; }
        public string Can { get; set; }
        public string Codigo { get; set; }
        public string Expo { get; set; }
        public string Dto { get; set; }
        public string PVP { get; set; }
        public string PVPF { get; set; }
        public bool EsAlgo { get; set; }
        public bool HayPrecio { get; set; }
        public bool PrecioDif { get; set; }
    }

    public class strPresupuestosPresupuestoDetalleAjax
    {
        public Int32 ID_Pres2 { get; set; }
        public Int32 ID_NPCD { get; set; }
        public string Can { get; set; }
        public string Codigo { get; set; }
        public string Expo { get; set; }
        public string Dto { get; set; }
        public string DtoE { get; set; }
        public string PVP { get; set; }
        public string Coste { get; set; }
        public string IVA { get; set; }
        public string REQ { get; set; }
        public string IRPF { get; set; }

        public bool EsArt { get; set; }
        public bool EsServ { get; set; }
        public bool EsLibre { get; set; }

        public bool EditArt { get; set; }
        public bool EditServ { get; set; }

        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestosPresupuestoEditar
    {
        public Int32 ID_Pres2 { get; set; }
        public Int32 ID_Us_Asi { get; set; }
        public string UsuAsi { get; set; }
        public Int32 ID_Us_Com { get; set; }
        public string UsuCom { get; set; }
        public string Breve { get; set; }
        public string ExpoPres { get; set; }
        public Int32 ID_TipoID { get; set; }
        public string Att { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string ObsPriv { get; set; }
        public string Obs { get; set; }

        public strLista[] Tipos { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }

        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestoEnviar
    {
        public Int32 ID_Pres2 { get; set; }
        public string Mails { get; set; }
        public string Asunto { get; set; }
        public string Expo { get; set; }
    }

    public class strPresupuestosPresupuestoIrAEnviar
    {
        public strDatoS[] Mails { get; set; }
        public strLista[] PlanTextos { get; set; }
        public string Asunto { get; set; }
        public strerror Err { get; set; }
    }

    public class strPresupuestosPresupuestosPresupuestoFormaDePago
    {
        public Int32 ID_For { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public string ObsForma { get; set; }

        public strLista[] Formas { get; set; }
        public strLista[] CueNegs { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Presupuestos - Presupuestos - Nuevo"

    public class strPresupuestosPresupuestoNuevo
    {
        public Int32 ID_Cli2 { get; set; }
        public Int32 ID_Cont2 { get; set; }
        public Int32 ID_Dir { get; set; }
        public string Breve { get; set; }
        public string Att { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string ObsPriv { get; set; }
        public Int32 ID_Us_Asi { get; set; }
        public string Fe_Prev { get; set; }
    }

    #endregion

    #region "Modulo Presupuestos - Consultas - Presupuestos aceptados por usuario"

    public class strPresupuestosPresupuestosCPAPUListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strPresupuestosPresupuestosCPAPUListadoDetalles[] det { get; set; }

        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public Int32 ID_Us_Alta { get; set; }
        public string UsuAlta { get; set; }
        public Int32 NumRea { get; set; }
        public Int32 NumAce { get; set; }
        public string ValRea { get; set; }
        public string ValAce { get; set; }
        public string ValFac { get; set; }

        public strDatoS[] res { get; set; }

        public strerror Err { get; set; }
    }
    public class strPresupuestosPresupuestosCPAPUListadoDetalles
    {
        public string ID_Pres2 { get; set; }

        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string BasePres { get; set; }
        public string Tipo { get; set; }
        public string ID_Al2 { get; set; }
        public string Fac { get; set; }
        public string BaseFac { get; set; }
        public string ID_Fac { get; set; }
        public string ID_Cli2 { get; set; }
    }

    public class strPresupuestosPresupuestosDatosClienteContacto
    {
        public strLista[] Dirs { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }

        public strerror Err { get; set; }
    }


    #endregion

    #region "Modulo Presupuestos - Consultas - Presupuestos aceptados con alta de cliente"

    public class strPresupuestosPresupuestosAceptadosConAltaDeClienteListado
    {
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }

        public strDatoS[] Altas { get; set; }
        public Int32 Total { get; set; }
        public string Titulo { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo - Mantenimientos"

    public class strMantenimientosMantenimientosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strMantenimientosMantenimientosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strMantenimientosMantenimientosListadoDetalles
    {
        public Int32 ID_Man2 { get; set; }
        public string Fe_Al { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string RefCli { get; set; }
        public string PeriVis { get; set; }
        public string Fe_Prox { get; set; }
        public string Fe_Fin { get; set; }
        public bool Baja { get; set; }
        public string UsuBaj { get; set; }
        public string Fe_Ba { get; set; }
        public string ExpoBaj { get; set; }
    }


    public class strMantenimientosMantenimientoCabecera
    {
        public string ID_Man2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string RefCli { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }

        public string ID_Cli2 { get; set; }
        public string ImportCli { get; set; }
    }


    public class strMantenimientosMantenimientoDetalles
    {
        public strMantenimientosMantenimientoCabecera Cab { get; set; }
        public string Usu { get; set; }
        public string Fe_Al { get; set; }
        public string PeriFac { get; set; }
        public Int32 PeriVis { get; set; }
        public string Fe_Prox { get; set; }
        public string Fe_Prox_Ant { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public string Mes { get; set; }
        public Int32 ID_Cli2Fac { get; set; }
        public string EmpFac { get; set; }
        public string AxDias { get; set; }
        public string Dia1 { get; set; }
        public string Dia2 { get; set; }
        public bool AutoFac { get; set; }
        public Int32 ID_For { get; set; }
        public Int32 ID_Cue { get; set; }
        public Int32 ID_CueNeg { get; set; }

        public string Total { get; set; }

        public string RefCli { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

        public string Horario { get; set; }
        public string UbiCont { get; set; }
        public string UbiTel { get; set; }
        public string UbiObs { get; set; }

        public string ObsPriv { get; set; }
        public string ObsPub { get; set; }

        public string UsuVali { get; set; }
        public string Fe_Vali { get; set; }
        public string Kms { get; set; }

        public strMantenimientosMantenimientoDetallesElementos[] det { get; set; }
        public strMantenimientosMantenimientoDetallesHistorial[] his { get; set; }
        public strLista[] CueNegs { get; set; }
        public strLista[] Cues { get; set; }
        public strLista[] Formas { get; set; }
        public strLista[] Peri { get; set; }
        public strDatoS[] Vis { get; set; }

        public string ID_Soli2 { get; set; }
        public bool SoliCerr { get; set; }

        public string ID_ManN2 { get; set; }
        public strerror Err { get; set; }
    }

    public class strMantenimientosMantenimientoDetallesElementos
    {
        public Int32 ID_ManDet { get; set; }
        public string Breve { get; set; }
        public string Bastidor { get; set; }
        public string Ubi { get; set; }
        public string RefInt { get; set; }
        public string Año { get; set; }
        public Int32 ID_Elem2 { get; set; }
    }

    public class strMantenimientosMantenimientoDireccionesCliente
    {
        public strDato[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strMantenimientosMantenimientoNuevoSelCli
    {
        public strLista[] PeriFac { get; set; }
        public strLista[] PeriVis { get; set; }
        public string Fe_In { get; set; }
        public string AxDias { get; set; }
        public string Dia1 { get; set; }
        public string Dia2 { get; set; }
        public strLista[] Formas { get; set; }
        public Int32 ID_For { get; set; }
        public strLista[] Cues { get; set; }
        public Int32 ID_Cue { get; set; }
        public strLista[] CueNegs { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public strLista[] Dirs { get; set; }
        public strDatoS[] Tels { get; set; }
        public string Emp { get; set; }
        public string Horario { get; set; }
        public string UbiObs { get; set; }
        public strDatoS[] Mans { get; set; }

        public strerror Err { get; set; }
    }

    public class strMantenimientosMantenimientoNuevo
    {
        public Int32 ID_Cli2 { get; set; }
        public string RefCli { get; set; }
        public Int32 PeriFac { get; set; }
        public Int32 PeriVis { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public bool AutoFac { get; set; }
        public Int32 AxDias { get; set; }
        public Int32 Dia1 { get; set; }
        public Int32 Dia2 { get; set; }
        public Int32 ID_For { get; set; }
        public Int32 ID_Cue { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public string ObsPriv { get; set; }
        public string ObsPub { get; set; }
        public Int32 ID_Dir { get; set; }
        public string Emp { get; set; }
        public string Horario { get; set; }
        public string UbiObs { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
    }

    public class strMantenimientosMantenimientoDetallesHistorial
    {
        public Int32 ID_ManPlan2 { get; set; }
        public string Fe_Prox { get; set; }
        public string UsuFin { get; set; }
        public string Fe_Fin { get; set; }
        public string Obs { get; set; }
    }


    public class strMantenimientosMantenimientoHistorial
    {
        public strMantenimientosMantenimientoCabecera Cab { get; set; }

        public strMantenimientosMantenimientoHistorialDetalles[] his { get; set; }

        public string ID_Man2Old { get; set; }

        public strerror Err { get; set; }
    }

    public class strMantenimientosMantenimientoHistorialDetalles
    {
        public Int32 ID_ManPlan2 { get; set; }
        public string Fe_Prox { get; set; }
        public string UsuIni { get; set; }
        public string Fe_Ini { get; set; }
        public string UsuFin { get; set; }
        public string Fe_Fin { get; set; }
        public string Obs { get; set; }
        public bool HayAsis { get; set; }
        public string ID_Asis2 { get; set; }
        public bool InfoDisables { get; set; }
        public bool SiEli { get; set; }
    }

    #endregion

    #region "Modulo - Mantenimientos - Consultas"

    public class strMantenimientosConsultasAltasYBajas
    {
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }

        public strLista[] AltasRes { get; set; }
        public Int32 AltasTotal { get; set; }
        public strLista[] AltasDet { get; set; }
        public string AltasTitulo { get; set; }

        public strLista[] BajasRes { get; set; }
        public Int32 BajasTotal { get; set; }
        public strLista[] BajasDet { get; set; }
        public string BajasTitulo { get; set; }

        public strLista[] RecRes { get; set; }
        public Int32 RecTotal { get; set; }
        public strLista[] RecDet { get; set; }
        public string RecTitulo { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Asisntencias"

    #region "Asistencias"

    public class strAsistenciasAsistenciasPorAsignarListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAsistenciasAsistenciasPorAsignarListadoDetalles[] det { get; set; }

        public strZonas[] Zonas { get; set; }
        public strLista[] Tipos { get; set; }
        public Int32 Urg { get; set; }
        public Int32 Man { get; set; }
        public strDato[] TiposElem { get; set; }
        public strLista[] EmpRels { get; set; }

        public strerror Err { get; set; }
    }
    public class strAsistenciasAsistenciasPorAsignarListadoDetalles
    {
        public Int32 ID_Asis2 { get; set; }
        public string Fe_Al { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string SoliPor { get; set; }
        public string Cat { get; set; }
        public bool Urg { get; set; }
        public bool CliParado { get; set; }
        public bool Man { get; set; }
        public string Web { get; set; }
        public string Expo { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public bool ManAnual { get; set; }
        public string ID_Man2 { get; set; }
        public string ID_Cli2 { get; set; }
        public string NumPartes { get; set; }
        public string Fe_Ul_Parte { get; set; }
        public string Fe_Plan { get; set; }
        public string Usu { get; set; }
        public string[] Tags { get; set; }
    }

    public class strAsistenciasAsistenciasNuevaAsistencia
    {
        public strLista[] Tipos { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasNuevaSelCli
    {
        public string Horario { get; set; }
        public string UbiObs { get; set; }
        public strLista[] Dirs { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }
        public strDatoS[] Asis { get; set; }
        public strLista[] Centros { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciaNuevaGuardar
    {
        public Int32 ID_Cli2 { get; set; }
        public string SolicitadoPor { get; set; }
        public string TelCli { get; set; }
        public string MailCli { get; set; }
        public string RefCli { get; set; }
        public Int32 ID_Tipo2 { get; set; }
        public bool Urg { get; set; }
        public bool CliParado { get; set; }
        public Int32 ID_Centro { get; set; }
        public string Breve { get; set; }
        public string Expo { get; set; }
        public string Obs { get; set; }
        public string EmpAux { get; set; }
        public Int32 ID_Dir { get; set; }
        public string UbiCont { get; set; }
        public string UbiTel { get; set; }
        public string Horario { get; set; }
        public bool GuardarHor { get; set; }
        public string UbiObs { get; set; }
        public Int32 ID_Us_Asi { get; set; }
        public string Dia { get; set; }
        public string Hora { get; set; }
        public string Comen { get; set; }
    }

    public class strAsistenciasAsistenciasEnCursoListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAsistenciasAsistenciasEnCursoListadoDetalles[] det { get; set; }

        public strZonas[] Zonas { get; set; }
        public strLista[] Tipos { get; set; }
        public Int32 Urg { get; set; }
        public Int32 Man { get; set; }
        public Int32 Partes { get; set; }
        public strDato[] TiposElem { get; set; }
        public strDato[] Opes { get; set; }
        public strLista[] EmpRels { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasEnCursoListadoDetalles
    {
        public Int32 ID_Asis2 { get; set; }
        public string Fe_Al { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string SoliPor { get; set; }
        public string Cat { get; set; }
        public bool Urg { get; set; }
        public bool CliParado { get; set; }
        public bool Man { get; set; }
        public string Web { get; set; }
        public string Expo { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string NumPartes { get; set; }
        public string Fe_Plan { get; set; }
        public bool ManAnual { get; set; }
        public string ID_Man2 { get; set; }
        public string ID_Cli2 { get; set; }
        public string Fe_Ul_Parte { get; set; }
        public string Fe_Fin { get; set; }
        public string Fe_Cerr { get; set; }
        public string Fe_Prox { get; set; }
    }

    public class strAsistenciasAsistenciasEnCursoCargaDiaTecnico
    {
        public string Dia { get; set; }
        public strAsistenciasAsistenciasEnCursoCargaDiaTecnicoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasEnCursoCargaDiaTecnicoDetalles
    {
        public Int32 ID_Asis2 { get; set; }
        public bool Man { get; set; }
        public string Fe_Plan { get; set; }
        public string Breve { get; set; }
        public string Comen { get; set; }
        public string NumPartes { get; set; }
        public string Cliente { get; set; }
        public string ID_Cli2 { get; set; }
        public string Pob { get; set; }
        public string CliRel { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string Horario { get; set; }
        public string ID_Man2 { get; set; }

        public strLista[] ManDet { get; set; }
    }

    public class strAsistenciasAsistenciasEnCursoInfoAsistenciaPlanificar
    {
        public Int32 ID_Asis2 { get; set; }
        public bool Man { get; set; }
        public string UsuAsi { get; set; }
        public string Dia { get; set; }
        public string Hora { get; set; }
        public string Breve { get; set; }
        public string Comen { get; set; }
        public string NumPartes { get; set; }
        public strLista[] ManDet { get; set; }
        public strerror Err { get; set; }
    }


    public class strAsistenciasAsistenciasCargaDeTrabajo
    {
        public strAsistenciasAsistenciasCargaDeTrabajoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasCargaDeTrabajoDetalles
    {
        public string DiaTxt { get; set; }
        public string Dia { get; set; }
        public Int32 DiaSem { get; set; }

        public strAsistenciasAsistenciasCargaDeTrabajoAsistenciasDetalles[] Asis { get; set; }
    }

    public class strAsistenciasAsistenciasCargaDeTrabajoAsistenciasDetalles
    {
        public Int32 ID_AsisPlan { get; set; }
        public string Fe_Plan { get; set; }
        public string Ho_Plan { get; set; }
        public Int32 Dia { get; set; }
        public string Comen { get; set; }
        public string ID_Asis2 { get; set; }
        public bool EsMan { get; set; }
        public string Breve { get; set; }
        public string Emp { get; set; }
        public string Pob { get; set; }
        public Int32 NumPartes { get; set; }
        public bool EnPause { get; set; }
        public string Fe_Fin { get; set; }
        public string ID_Man2 { get; set; }
        public bool Anual { get; set; }
        public string ID_Cli2 { get; set; }
        public string EmpRel { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string EmpRelTipo { get; set; }
        public bool Atraso { get; set; }
        public strDato[] Elems { get; set; }
    }

    public class strAsistenciasAsistenciasCargaDeTrabajoSinPlanificar
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strDatoS filtro { get; set; }
        public strAsistenciasAsistenciasCargaDeTrabajoAsistenciasDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasProduccionResumen
    {
        public bool SiPost { get; set; }
        public strAsistenciasAsistenciasProduccionResumenDetalles[] det { get; set; }

        public string UsuAsi { get; set; }
        public strDatoS[] Tipos { get; set; }
        public string TiposNumPartes { get; set; }
        public string TiposTiempos { get; set; }
        public strDatoS[] Facs { get; set; }
        public string FacsCan { get; set; }
        public string FacsCoste { get; set; }
        public string FacsTotal { get; set; }
        public strDatoS Partes { get; set; }
        public strDatoS[] PartesExce { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasProduccionResumenDetalles
    {
        public Int32 ID_Asis2 { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string CP { get; set; }
        public string Tipo { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public string Durada { get; set; }

        public strDato[] Tipos { get; set; }
        public strDato[] Facs { get; set; }

        public strerror Err { get; set; }
    }


    public class strAsistenciasAsistenciasComisionarResumen
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAsistenciasAsistenciasComisionarResumenDetalles[] det { get; set; }

        public string Fe_Fi { get; set; }
        public string UsuAsi { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasComisionarResumenDetalles
    {
        public string ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
        public decimal ComPorRel { get; set; }
        public Int32 ID_Parte2 { get; set; }
        public Int32 ID_Asis2 { get; set; }
        public string Cat { get; set; }
        public bool Man { get; set; }
        public Int32 ID_Al2 { get; set; }
        public string Fac { get; set; }
        public Int32 ID_Fac { get; set; }
        public string Fe_Fa { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Total { get; set; }
        public string Usu { get; set; }
    }

    public class strAsistenciasAsistenciasComisionarFactura
    {
        public string Fac { get; set; }
        public string Fe_Fa { get; set; }
        public string Total { get; set; }
        public string Emp { get; set; }
        public string ID_Cli2 { get; set; }
        public bool CliNuevo { get; set; }
        public bool Man { get; set; }

        public strDatoS[] Venci { get; set; }

        public string EmpRel { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string ComPorRel { get; set; }
        public string EmpRelTipo { get; set; }

        public strAsistenciasAsistenciasComisionarFacturaAlbaranes[] Alb { get; set; }

        public strerror Err { get; set; }

    }

    public class strAsistenciasAsistenciasComisionarFacturaAlbaranes
    {
        public string ID_Al2 { get; set; }
        public string AlbTotal { get; set; }

        public string ID_Parte2 { get; set; }
        public string ID_Asis2 { get; set; }
        public string AsisTipo { get; set; }
        public string AsisDir { get; set; }
        public string AsisPrimUsu { get; set; }
        public bool AsisMan { get; set; }

        public string ID_Man2 { get; set; }
        public string ID_ManPlan2 { get; set; }

        public string ID_Pres2 { get; set; }
        public string Pres2Rea { get; set; }
        public string Pres2Asi { get; set; }
        public string Pres2Com { get; set; }
        public string Pres2Acep { get; set; }
        public string Pres2Total { get; set; }
        public bool Pres2CliNuevo { get; set; }

        public strAsistenciasAsistenciasComisionarFacturaAlbaranDetalles[] det { get; set; }
    }

    public class strAsistenciasAsistenciasComisionarFacturaAlbaranDetalles
    {
        public Int32 ID_De_Al { get; set; }
        public string Expo { get; set; }
        public string Codigo { get; set; }
        public decimal Can { get; set; }
        public decimal Coste { get; set; }
        public decimal CosteF { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioF { get; set; }
        public decimal Benef { get; set; }
        public decimal ComiRel { get; set; }
        public decimal Benef2 { get; set; }
        public decimal Be2u { get; set; }
        public decimal Base2 { get; set; }
        public string ComOpe { get; set; }
        public bool Lock { get; set; }
    }

    public class strAsistenciasAsistenciasComisionarFacturaAlbaranDetalle
    {
        public string Expo { get; set; }
        public decimal Benef2 { get; set; }
        public decimal Base2 { get; set; }

        public strerror Err { get; set; }

    }

    public class strAsistenciasAsistenciasComisionarFacturaAlbaranDetalleComisones
    {
        public string comi { get; set; }
        public string benef { get; set; }
        public strDatoS[] det { get; set; }

        public strerror Err { get; set; }

    }

    public class strAsistenciasAsistenciasComisionesResumen
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }

        public Int32 vopecompend { get; set; }
        public string topecompend { get; set; }
        public Int32 vclicompend { get; set; }
        public string tclicompend { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }

        public strAsistenciasAsistenciasComisionesResumenDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasComisionesResumenDetalles
    {
        public Int32 ID_Com { get; set; }
        public Int32 ID_Us_Com { get; set; }
        public Int32 ID_Cli2 { get; set; }

        public string Tipo { get; set; }
        public string Nom { get; set; }
        public string Saldo { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasComisionesDebeHader
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strDatoS filtro { get; set; }
        public strAsistenciasAsistenciasComisionesDebeHaderDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasComisionesDebeHaderDetalles
    {
        public Int32 ID_Com { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Expo { get; set; }
        public string Ingreso { get; set; }
        public string Pago { get; set; }
        public string Saldo { get; set; }
    }

    public class strAsistenciasAsistenciasComisionesFactura
    {
        public string Fac { get; set; }
        public string Fe_Fa { get; set; }
        public string Total { get; set; }
        public string Emp { get; set; }
        public string ID_Cli2 { get; set; }
        public bool CliNuevo { get; set; }
        public bool Man { get; set; }

        public strDatoS[] Venci { get; set; }

        public string EmpRel { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string ComPorRel { get; set; }
        public string EmpRelTipo { get; set; }

        public strAsistenciasAsistenciasComisionesFacturaAlbaranes[] Alb { get; set; }

        public strerror Err { get; set; }

    }

    public class strAsistenciasAsistenciasComisionesFacturaAlbaranes
    {
        public string ID_Al2 { get; set; }
        public string AlbTotal { get; set; }

        public string ID_Parte2 { get; set; }
        public string ID_Asis2 { get; set; }
        public string AsisTipo { get; set; }
        public string AsisDir { get; set; }
        public string AsisPrimUsu { get; set; }
        public bool AsisMan { get; set; }

        public string ID_Man2 { get; set; }
        public string ID_ManPlan2 { get; set; }

        public string ID_Pres2 { get; set; }
        public string Pres2Rea { get; set; }
        public string Pres2Asi { get; set; }
        public string Pres2Com { get; set; }
        public string Pres2Acep { get; set; }
        public string Pres2Total { get; set; }
        public bool Pres2CliNuevo { get; set; }

        public strAsistenciasAsistenciasComisionesFacturaAlbaranDetalles[] det { get; set; }
    }

    public class strAsistenciasAsistenciasComisionesFacturaAlbaranDetalles
    {
        public Int32 ID_De_Al { get; set; }
        public string Expo { get; set; }
        public string Codigo { get; set; }
        public decimal Can { get; set; }
        public decimal Coste { get; set; }
        public decimal CosteF { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioF { get; set; }
        public decimal Benef { get; set; }
        public decimal ComiRel { get; set; }
        public decimal Benef2 { get; set; }
        public decimal Be2u { get; set; }
        public decimal Base2 { get; set; }
        public string ComOpe { get; set; }
        public bool Lock { get; set; }
        public strAsistenciasAsistenciasComisionesFacturaAlbaranDetallesComision[] Comi { get; set; }
    }

    public class strAsistenciasAsistenciasComisionesFacturaAlbaranDetallesComision
    {
        public string EmpUsu { get; set; }
        public decimal Base { get; set; }
        public decimal ComiPor { get; set; }
        public decimal Imp { get; set; }
        public bool EsOpe { get; set; }
    }

    public class strAsistenciasAsistenciasHistorialListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAsistenciasAsistenciasHistorialListadoDetalles[] det { get; set; }

        public strZonas[] Zonas { get; set; }
        public strLista[] Tipos { get; set; }
        public Int32 Urg { get; set; }
        public Int32 Man { get; set; }
        public Int32 Partes { get; set; }
        public strDato[] TiposElem { get; set; }
        public strDato[] Opes { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasHistorialListadoDetalles
    {
        public Int32 ID_Asis2 { get; set; }
        public string Fe_Al { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string SoliPor { get; set; }
        public string Cat { get; set; }
        public bool Urg { get; set; }
        public bool CliParado { get; set; }
        public bool Man { get; set; }
        public string Web { get; set; }
        public string Expo { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public Int32 Partes { get; set; }
        public string UsuAsi { get; set; }
    }

    public class strAsistenciasAsistenciasHistorialAsistencia
    {

        public Int32 ID_Asis2 { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string Cat { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public string Cli { get; set; }
        public string SoliPor { get; set; }
        public string Expo { get; set; }
        public string Breve { get; set; }
        public string ObsInt { get; set; }
        public string RefCli { get; set; }
        public string Dom { get; set; }
        public string TelCli { get; set; }
        public string MailCli { get; set; }
        public bool Mantenimiento { get; set; }
        public bool Urg { get; set; }
        public bool CliParado { get; set; }
        public string Centro { get; set; }
        public string UsuAsig { get; set; }
        public string NumPartes { get; set; }
        public bool EnPause { get; set; }
        public string UsuFin { get; set; }
        public string Fe_Fin { get; set; }
        public string UsuCerr { get; set; }
        public string Fe_Cerr { get; set; }
        public string Reso { get; set; }
        public string ObsCerr { get; set; }

        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
        public string Obs { get; set; }
        public string Horario { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }


        public strAsistenciasAsistenciasHistorialAsistenciaPartes[] det { get; set; }

        public Int32 ID_Man2 { get; set; }
        public Int32 ID_ManPlan2 { get; set; }
        public string Man_Fe_Fin { get; set; }
        public string Man_UsuFin { get; set; }

        public strAsistenciasAsistenciasHistorialMantenimientoPlanificacion[] ManDet { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasHistorialAsistenciaPartes
    {
        public Int32 ID_Parte2 { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public bool EsPause { get; set; }
        public string NumParte { get; set; }
        public bool MalUso { get; set; }
        public string Fe_Cerr { get; set; }
        public string UsuCerr { get; set; }
        public string Reso { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public string ObsNoFin { get; set; }
        public string ObsInt { get; set; }
        public bool CerrAsis { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string SigCliJpg { get; set; }
        public string SigTecJpg { get; set; }
    }

    public class strAsistenciasAsistenciasHistorialMantenimientoPlanificacion
    {
        public string Tipo { get; set; }
        public string Ubi { get; set; }
        public string RefCli { get; set; }
        public string Bastidor { get; set; }
        public string UsuFin { get; set; }
        public string Fe_Fin { get; set; }
        public string ValorOK { get; set; }
        public bool Eli { get; set; }
    }

    public class strAsistenciasAsistenciasZonasListado
    {
        public Int32 numZonas { get; set; }
        public Int32 numSubZonas { get; set; }
        public Int32 numCPs { get; set; }
        public strDatoS[] Zonas { get; set; }
        public strDatoS[] SubZonas { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasZonasSubZonaDetalles
    {
        public Int32 ID_ZonaSub { get; set; }
        public string ZonaSub { get; set; }
        public Int32 ID_Zona { get; set; }
        public strDato[] CPs { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Asistencias - Por facturar"

    public class strAsistenciasAsistenciasPorFacturarListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAsistenciasAsistenciasPorFacturarListadoDetalles[] det { get; set; }

        public strLista[] Tipos { get; set; }
        public Int32 Man { get; set; }

        public strerror Err { get; set; }
    }
    public class strAsistenciasAsistenciasPorFacturarListadoDetalles
    {
        public Int32 ID_Asis2 { get; set; }
        public string Fe_Al { get; set; }
        public Int32 ID_Parte2 { get; set; }
        public string UsuRea { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Cat { get; set; }
        public bool Man { get; set; }
        public string ID_Cli2 { get; set; }
    }

    public class strAsistenciasAsistenciasPorFacturarFacturar
    {
        public string ID_Asis2 { get; set; }
        public string Fe_Fin { get; set; }
        public string UsuFin { get; set; }
        public string Cat { get; set; }
        public string Emp { get; set; }
        public bool CliNuevo { get; set; }
        public string ID_Cli2 { get; set; }
        public string ObsCli { get; set; }
        public string EmpTipo { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string ObsDir { get; set; }
        public bool HayEmpObs { get; set; }
        public Int32 PreciosCli { get; set; }
        public string Breve { get; set; }
        public bool EsMan { get; set; }

        public string EmpRel { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string ObsCliRel { get; set; }
        public string EmpRelTipo { get; set; }
        public bool HayEmpRelObs { get; set; }
        public Int32 PreciosCliRel { get; set; }

        public string ID_Man2 { get; set; }
        public string ManUsu { get; set; }
        public string ManFe_Fin { get; set; }
        public bool ManAnual { get; set; }
        public string ManObsTec { get; set; }
        public string ManPeri { get; set; }
        public string ManVis { get; set; }

        public string ID_Pres2 { get; set; }
        public string PresFe_Al { get; set; }
        public string PresUsu { get; set; }
        public string PresBreve { get; set; }
        public string PresFe_Rev { get; set; }
        public string PresUsuRev { get; set; }
        public strDatoS[] detPres { get; set; }

        public strDatoS[] Albs { get; set; }
        public strDatoS[] Cobs { get; set; }
        public strDatoS[] Facs { get; set; }

        public strDatoS partes { get; set; }

        public strerror Err { get; set; }

    }

    public class strAsistenciasAsistenciasPorFacturarFacturarAjax
    {
        public string Total { get; set; }
        public bool Cerrado { get; set; }

        public strAsistenciasAsistenciasPorFacturarFacturarDetallesAjax[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasPorFacturarFacturarDetallesAjax
    {
        public Int32 ID_De_Pa { get; set; }
        public Int32 ID_Parte2 { get; set; }
        public string Codigo { get; set; }
        public string Expo { get; set; }
        public string Can { get; set; }
        public string PVP { get; set; }
        public string PVPF { get; set; }
        public bool NoFac { get; set; }
        public bool Rev { get; set; }
        public string Fe_Procesado { get; set; }
        public bool PrecioDef { get; set; }
        public Int32 NumCache { get; set; }
    }

    public class strAsistenciasAsistenciasPorFacturarFacturarDetalleAjax
    {
        public Int32 ID_Asis2 { get; set; }
        public Int32 ID_Parte2 { get; set; }
        public Int32 ID_De_Pa { get; set; }
        public string ServOri { get; set; }
        public string CodigoOri { get; set; }
        public string ExpoOri { get; set; }
        public string CanOri { get; set; }
        public string PVPOri { get; set; }
        public string PVPFOri { get; set; }
        public bool NoFac { get; set; }

        public string Serv { get; set; }
        public string Codigo { get; set; }
        public string Can { get; set; }
        public string PVP { get; set; }

        public strerror Err { get; set; }
    }

    public class strAsistenciasAsistenciasPorFacturarProcesar
    {
        public Int32 ID_Asis2 { get; set; }
        public Int32[] ID_De_Pa { get; set; }
        public bool Fe_Alb { get; set; }
    }

    #endregion


    #region "Partes de asistencia"

    public class strAsistenciasPartesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAsistenciasPartesListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strAsistenciasPartesListadoDetalles
    {
        public Int32 ID_Parte2 { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAlta { get; set; }
        public Int32 NumParte { get; set; }
        public Int32 NumPartes { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public Int32 ID_Asis2 { get; set; }
        public bool Cerrado { get; set; }

        public string UsuCerr { get; set; }
        public string Fe_Cerr { get; set; }
        public string Reso { get; set; }
        public bool MalUso { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public string ObsNoFin { get; set; }
        public string Cont { get; set; }
        public string ContTel { get; set; }
        public string ContMail { get; set; }
        public bool HayFirmaTec { get; set; }
        public bool HayFirmaCli { get; set; }
        public bool AsisCerr { get; set; }
    }

    #endregion


    #endregion

    #region "Modulo - Contactos"

    public class strContactosContactosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strContactosContactosListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strContactosContactosListadoDetalles
    {
        public string ID_Cont2 { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string NIF { get; set; }
        public string Tel { get; set; }
    }

    public class strContactosContactosCabecera
    {
        public string ID_Cont2 { get; set; }
        public string Emp { get; set; }
        public string EmpFis { get; set; }
        public string Tipo { get; set; }
        public string NIF { get; set; }
        public string NIFWar { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
    }

    public class strContactosContactos_General
    {
        public strContactosContactosCabecera Cab { get; set; }
        public strClientesClienteDireccion[] Dirs { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }
        public strDatoS[] Centros { get; set; }
        public strDatoS[] Webs { get; set; }

        public strerror Err { get; set; }
    }

    public class strContactosContactos_AccesoWebListado
    {
        public strContactosContactosCabecera Cab { get; set; }

        public strContactosContactos_AccesoWebListadoDetalles[] det { get; set; }
        public strLista[] Mails { get; set; }
        public strLista[] Webs { get; set; }

        public strerror Err { get; set; }
    }

    public class strContactosContactos_AccesoWebListadoDetalles
    {
        public string NIC { get; set; }
        public string Nom { get; set; }
        public string Mail { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string Fe_Ul_Acc { get; set; }
        public string NumAcc { get; set; }
        public bool Blo { get; set; }
        public string UsuBlo { get; set; }
        public string Fe_Blo { get; set; }
        public string ExpoBlo { get; set; }
        public bool EsAdm { get; set; }
    }

    public class strContactosContactoDireccionDetalle
    {
        public Int32 ID_Cont2 { get; set; }
        public Int32 ID_Dir { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }

        public string Obs { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public Int32 ID_Centro { get; set; }
        public string EmpAux { get; set; }
        public string Horario { get; set; }
        public string UsuVali { get; set; }
        public string Fe_Vali { get; set; }

        public strLista[] Centros { get; set; }
        public strDatoS[] Pais { get; set; }
        public strDatoS[] Pros { get; set; }
        public strDatoS[] CPs { get; set; }
        public strDatoS[] Pobs { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Clientes"

    #region "Modulo Clientes - Por revisar"

    public class strClientesClientesPorRevisarListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strClientesClientesPorRevisarListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strClientesClientesPorRevisarListadoDetalles
    {
        public Int32 ID_Rev { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Obs { get; set; }
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
    }

    #endregion

    #region "Modulo Clientes - Nuevo cliente"

    public class strClientesClienteNuevo
    {
        public strLista[] TiposCli { get; set; }
        public strLista[] ForsPag { get; set; }

        public string Pai { get; set; }
        public string Pro { get; set; }

        public strLista[] Pais { get; set; }
        public strLista[] Pros { get; set; }
        public strLista[] CPs { get; set; }
        public strLista[] Pobs { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesClienteNuevoGenerar
    {
        public string Emp { get; set; }
        public string EmpFis { get; set; }
        public string DirCom { get; set; }
        public string PaiCom { get; set; }
        public string ProCom { get; set; }
        public string CPCom { get; set; }
        public string PobCom { get; set; }
        public bool DifDirDis { get; set; }
        public string DirFis { get; set; }
        public string PaiFis { get; set; }
        public string ProFis { get; set; }
        public string CPFis { get; set; }
        public string PobFis { get; set; }
        public string NIF { get; set; }
        public bool REQ { get; set; }
        public bool Mailing { get; set; }
        public Int32 ID_Us_Agente { get; set; }
        public string Actividad { get; set; }
        public bool FacViaMail { get; set; }
        public string Conctacto { get; set; }
        public string Tel { get; set; }
        public string Movil { get; set; }
        public string Mail1 { get; set; }
        public string Mail2 { get; set; }
        public string Web { get; set; }
        public Int32 ID_TipCli { get; set; }
        public Int32 AxDias { get; set; }
        public Int32 Dia { get; set; }
        public Int32 ID_Cli2Rel { get; set; }
        public Int32 ID_For { get; set; }
        public string Cue { get; set; }
        public string Obs { get; set; }
        public string ObsAdm { get; set; }
    }

    #endregion

    #region "Modulo Clientes - Clientes"

    public class strClientesClientesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strClientesClientesListadoDetalles[] det { get; set; }
        public strLista[] TiposCli { get; set; }

        public strerror Err { get; set; }
    }
    public class strClientesClientesListadoDetalles
    {
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string NIF { get; set; }
        public string Tel { get; set; }
        public bool Blo { get; set; }
    }

    #endregion

    #region "Modulo Clientes - Consultas"

    #region "Modulo Clientes - Consultas - Resumen Empresas Relacionadas"

    public class strClientesConsultasResumenEmpresasRelacionadasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public string Fe_In1 { get; set; }
        public string Fe_Fi1 { get; set; }
        public string Fe_In2 { get; set; }
        public string Fe_Fi2 { get; set; }
        public Int32 ID_Us_Agente { get; set; }
        public string Agente { get; set; }
        public Int32 ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
        public strClientesConsultasResumenEmpresasRelacionadasListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strClientesConsultasResumenEmpresasRelacionadasListadoDetalles
    {
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string TipCli { get; set; }
        public string Agente { get; set; }
        public string Pob { get; set; }

        public string NumCli { get; set; }
        public string NumPrecios { get; set; }

        public string NumCobPend { get; set; }
        public string CobPend { get; set; }
        public string TieCobPend { get; set; }

        public string NumPres { get; set; }
        public string BasePres { get; set; }
        public string ValMedBasePres { get; set; }
        public string NumPresAbi { get; set; }
        public string BasePresAbi { get; set; }
        public string ValMedBasePresAbi { get; set; }
        public string BasePresAcep { get; set; }
        public string WR { get; set; }
        public string BaseFac { get; set; }

        public string NumPres2 { get; set; }
        public string BasePres2 { get; set; }
        public string ValMedBasePres2 { get; set; }
        public string NumPresAbi2 { get; set; }
        public string BasePresAbi2 { get; set; }
        public string ValMedBasePresAbi2 { get; set; }
        public string BasePresAcep2 { get; set; }
        public string WR2 { get; set; }
        public string BaseFac2 { get; set; }
    }

    #endregion

    #region "Modulo Clientes - Consultas - Resumen Empresas Relacionadas por Agente"

    public class strClientesConsultasResumenEmpresasRelacionadasPorAgenteListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strClientesConsultasResumenEmpresasRelacionadasPorAgenteListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strClientesConsultasResumenEmpresasRelacionadasPorAgenteListadoDetalles
    {
        public string Agente { get; set; }
        public string NumCli { get; set; }
        public string NumPresAbi { get; set; }
        public string BasePresAbi { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo Clientes - Clientes - Tags"

    public class strClientesCliente_Tags
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strLista[] Tags { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Clientes - Clientes - Cliente"

    public class strClientesClienteCabecera
    {
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string EmpFis { get; set; }
        public string Tipo { get; set; }
        public string NIF { get; set; }
        public string NIFWar { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public bool Blo { get; set; }
        public string BloWar { get; set; }
        public bool Devo { get; set; }

        public string ImportCli { get; set; }
    }

    public class strClientesClienteDireccion
    {
        public string ID_Dir { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }
        public string Obs { get; set; }
        public bool EsDocu { get; set; }
        public bool Vali { get; set; }
        public string CentroCoste { get; set; }
        public string EmpAux { get; set; }
        public string Horario { get; set; }
        public bool EsCom { get; set; }
        public bool EsFis { get; set; }
    }

    public class strClientesCliente_General
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strClientesClienteDireccion[] Dirs { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }
        public strDatoS[] Centros { get; set; }
        public strDatoS[] Webs { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesClienteDireccionDetalle
    {
        public Int32 ID_Cli2 { get; set; }
        public Int32 ID_Dir { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }

        public string Obs { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public Int32 ID_Centro { get; set; }
        public string EmpAux { get; set; }
        public string Horario { get; set; }
        public string UsuVali { get; set; }
        public string Fe_Vali { get; set; }

        public strLista[] Centros { get; set; }
        public strDatoS[] Pais { get; set; }
        public strDatoS[] Pros { get; set; }
        public strDatoS[] CPs { get; set; }
        public strDatoS[] Pobs { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_Comercial
    {
        public strClientesClienteCabecera Cab { get; set; }

        public string Emp { get; set; }
        public string EmpFis { get; set; }
        public Int32 ID_Agente { get; set; }
        public string Agente { get; set; }
        public Int32 ID_TipCli { get; set; }
        public string Fe_Na { get; set; }
        public bool Mailing { get; set; }
        public Int32 ID_Tari2 { get; set; }
        public string Dto { get; set; }
        public string Comi { get; set; }
        public string Act { get; set; }
        public string Obs { get; set; }

        public strLista[] TipClis { get; set; }
        public strLista[] Tarifas { get; set; }


        public Int32 CopiasAlb { get; set; }
        public Int32 CopiasFac { get; set; }
        public bool AlbNoVal { get; set; }

        public bool HayModuloPedido { get; set; }
        public bool HayProgramasLocales { get; set; }
        public bool HayModuloTags { get; set; }
        public bool FacPedCli { get; set; }
        public Int32 ID_TipEnvGratis { get; set; }
        public Int32 ID_Portes { get; set; }
        public string ObsPortes { get; set; }

        public strLista[] TiposEnv { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_Comercial_Comercial
    {
        public Int32 ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public Int32 ID_Agente { get; set; }
        public Int32 ID_TipCli2 { get; set; }
        public string Fe_Na { get; set; }
        public bool Mailing { get; set; }
        public Int32 ID_Tari2 { get; set; }
        public string Dto { get; set; }
        public string Comi { get; set; }
        public string Act { get; set; }
        public string Obs { get; set; }
    }

    public class strClientesCliente_Fiscal
    {
        public strClientesClienteCabecera Cab { get; set; }

        public string Emp { get; set; }
        public string EmpFis { get; set; }
        public string NIF { get; set; }
        public string CCCli { get; set; }
        public Int32 ID_TipoIVA { get; set; }
        public Int32 ID_Mon { get; set; }
        public bool REQ { get; set; }
        public string ID_Prov2 { get; set; }

        public string AxDias { get; set; }
        public Int32 NumFra { get; set; }
        public bool PostPago { get; set; }
        public bool FacDif { get; set; }
        public bool FacEMail { get; set; }
        public Int32 ID_Us_MailEnNom { get; set; }
        public string Us_MailEnNom { get; set; }
        public string ObsAdm { get; set; }
        public Int32 ID_Us_Gest { get; set; }
        public string Us_Gest { get; set; }

        public Int32 ID_For { get; set; }

        public string RieMax { get; set; }
        public string RieMaxStr { get; set; }
        public bool Asegurado { get; set; }
        public string Poliza { get; set; }
        public string RieAct { get; set; }
        public string RieDis { get; set; }
        public strDatoS[] Riesgo { get; set; }

        public strLista[] TiposIVAs { get; set; }
        public strLista[] Monedas { get; set; }
        public strLista[] CueNegs { get; set; }
        public strDatoS[] Cues { get; set; }
        public strDatoS[] Formas { get; set; }
        public strLista[] Dias { get; set; }

        // Cuentas empresa

        public strerror Err { get; set; }
    }

    public class strClientesCliente_Fiscal_Facturacion_Guardar
    {
        public Int32 ID_Cli2 { get; set; }
        public Int32 AxDias { get; set; }
        public Int32 Frac { get; set; }
        public bool PostPago { get; set; }
        public bool FacDif { get; set; }
        public bool FacEMail { get; set; }
        public Int32 ID_Us_MailEnNom { get; set; }
        public string ObsAdm { get; set; }
        public Int32 ID_Us_Gest { get; set; }
    }

    public class strClientesCliente_Bloqueo
    {
        public strClientesClienteCabecera Cab { get; set; }

        public bool Blo { get; set; }
        public string Fe_Blo { get; set; }
        public string UsuBlo { get; set; }
        public string MotBlo { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_AccesoWebListado
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strClientesCliente_AccesoWebListadoDetalles[] det { get; set; }
        public strLista[] Mails { get; set; }
        public strLista[] Webs { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_AccesoWebListadoDetalles
    {
        public string NIC { get; set; }
        public string Nom { get; set; }
        public string Mail { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string Fe_Ul_Acc { get; set; }
        public string NumAcc { get; set; }
        public bool Blo { get; set; }
        public string UsuBlo { get; set; }
        public string Fe_Blo { get; set; }
        public string ExpoBlo { get; set; }
        public bool EsAdm { get; set; }
    }

    public class strClientesCliente_PreciosEspeciales
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strDato[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_PreciosEspeciales_Detalles
    {
        public Int32 ID_Det2 { get; set; }
        public string Det { get; set; }
        public string Cat { get; set; }
        public string Codigo { get; set; }
        public string PrecioO { get; set; }
        public string PrecioE { get; set; }
        public string Dto { get; set; }
        public string DtoE { get; set; }
        public string DtoO { get; set; }
    }

    public class strClientesCliente_Correspondencia
    {
        public strClientesClienteCabecera Cab { get; set; }

        public bool FacEMail { get; set; }

        public bool CorrDisp { get; set; }
        public bool CorrFac { get; set; } // ID_Modulo=17
        public strDatoS[] Facs { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_RelacionadoCon
    {
        public strClientesClienteCabecera Cab { get; set; }


        public strClientesCliente_RelacionadoConDetalles[] det { get; set; }


        public strerror Err { get; set; }
    }

    public class strClientesCliente_RelacionadoConDetalles
    {
        public Int32 ID_Cli2 { get; set; }
        public Int32 ID_Rel { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Emp { get; set; }
        public string TipCli { get; set; }
        public string NIF { get; set; }
        public bool EnvFac { get; set; }
        public bool ForPag { get; set; }
        public bool CobEnEsp { get; set; }
        public bool Precios { get; set; }
        public bool FacAEmpRel { get; set; }

        public bool Baja { get; set; }
        public string Fe_Baj { get; set; }
        public string UsuBaj { get; set; }
        public string ObsBaj { get; set; }

        public strLista[] Tags { get; set; }
        public strClientesCliente_RelacionadoConDetallesContactos[] Conts { get; set; }
    }

    public class strClientesCliente_RelacionadoConDetallesContactos
    {
        public Int32 ID_RelC { get; set; }
        public Int32 ID_Cnt2 { get; set; }
        public string Nom { get; set; }
        public string Cargo { get; set; }
        public bool EnvFac { get; set; }
        public string Obs { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_Clientes
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }

        public strClientesClienteCabecera Cab { get; set; }

        public strClientesCliente_ClientesRelacionados_ClientesDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_ClientesDetalles
    {
        public Int32 ID_Rel { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string Tipo { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string NIF { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_Cliente_RelacionadoCon
    {
        public Int32 ID_Rel { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Emp { get; set; }
        public bool EnvFac { get; set; }
        public bool ForPag { get; set; }
        public bool CobEnEsp { get; set; }
        public bool Precios { get; set; }
        public bool FacAEmpRel { get; set; }
        public bool TagsHere { get; set; }

        public strLista[] Tags { get; set; }

        public strerror Err { get; set; }
    }


    public class strClientesCliente_ClientesRelacionados_CobrosPendientes
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }

        public strClientesClienteCabecera Cab { get; set; }

        public strFacturacionCobrosListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_Presupuestos
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }

        public strClientesClienteCabecera Cab { get; set; }

        public strClientesCliente_ClientesRelacionados_PresupuestosDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_PresupuestosDetalles
    {
        public Int32 ID_Pres2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string UsuAsi { get; set; }
        public string UsuCom { get; set; }
        public string TipoPres { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }
        public string Emp { get; set; }
        public string NIF { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Breve { get; set; }
        public string Total { get; set; }

        public Int32 EnvPresMail { get; set; }
        public Int32 EnvPresCorr { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public strModuloUsuarioTareaPendienteDetalles[] Sol { get; set; }
        public bool Soli_Atraso { get; set; }
        public string Soli_Fe_Prev { get; set; }
        public string Soli_Usu_Prev { get; set; }
        public bool SoliCerr { get; set; }
        public bool SoliTwitHoy { get; set; }

        public bool Atraso { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_Asistencias
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }

        public strClientesClienteCabecera Cab { get; set; }

        public strAsistenciasAsistenciasEnCursoListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_Solicitudes
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }

        public strClientesClienteCabecera Cab { get; set; }

        public strModuloSolicitudesListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesRelacionados_Herencia
    {
        public Int32 ID_Cli2 { get; set; }
        public bool EnvFac { get; set; }
        public bool ForPag { get; set; }
        public bool CobEnEspera { get; set; }
        public bool Precios { get; set; }
        public bool Facturar { get; set; }
        public bool Propagar { get; set; }
        public bool Tags { get; set; }

        public Int32 ID_Rel { get; set; } // Solo para modulo-clientes/clientes/cliente/cliente-relacionado-con/relacion-guardar

        public strClientesClienteCabecera Cab { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Clientes - Clientes - Cliente - Situación"

    public class strClientesCliente_ClientesSituacion_CobrosPendientes
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strFacturacionCobrosListado lis { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesSituacion_Cobros
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strClientesClienteCabecera Cab { get; set; }

        public strClientesCliente_ClientesSituacion_CobrosDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesSituacion_CobrosDetalles
    {
        public Int32 ID_Fra { get; set; }
        public Int32 ID_Fac { get; set; }
        public string Fe_Ve { get; set; }
        public string Fe_Fa { get; set; }
        public string Fac { get; set; }
        public string Imp { get; set; }
        public string Forma { get; set; }
        public string Fe_Cob { get; set; }
        public bool Inco { get; set; }
        public string Fe_EnEspera { get; set; }
        public string Cuenta { get; set; }
        public string Concepto { get; set; }
        public string NumReme { get; set; }
        public string PresReme { get; set; }
        public string CueReme { get; set; }
        public string TipoReme { get; set; }
        public string Fe_Dev { get; set; }
        public bool Alarma { get; set; }
    }


    public class strClientesCliente_ClientesSituacion_Presupuestos
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strPresupuestosPresupuestosListado lis { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesSituacion_PresupuestosPresupuetoDetalles
    {
        public Int32 ID_NPC { get; set; }
        public Int32 ID_Pres2 { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string UsuAsig { get; set; }
        public string UsuCom { get; set; }
        public string Expo { get; set; }
        public string Breve { get; set; }
        public string Total { get; set; }
        public string Obs { get; set; }
        public string ObsPriv { get; set; }
        public string Att { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }

        public strDatoS[] det { get; set; }

        public strerror Err { get; set; }
    }


    public class strClientesCliente_ClientesSituacion_Asistencias
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strAsistenciasAsistenciasEnCursoListado lis { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesSituacion_Mantenimientos
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strMantenimientosMantenimientosListado lis { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesSituacion_Facturas
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strFacturacionFacturasListado lis { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesSituacion_Albaranes
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strFacturacionAlbaranesListado lis { get; set; }

        public strerror Err { get; set; }
    }

    public class strClientesCliente_ClientesSituacion_Solicitudes
    {
        public strClientesClienteCabecera Cab { get; set; }

        public strModuloSolicitudesListado lis { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo Albaranes"

    #region "Modulo Albaranes - Nuevo"

    public class strAlbaranesAlbaranesAlbaranNuevoDatosCliente
    {
        public Int32 ID_Dir { get; set; }
        public strLista[] Dirs { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Albaranes - Albaranes"

    public class strAlbaranesAlbaranesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strAlbaranesAlbaranesListadoDetalles[] det { get; set; }

        public string Cli { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public string Año { get; set; }

        public strerror Err { get; set; }
    }
    public class strAlbaranesAlbaranesListadoDetalles
    {
        public string ID_Al { get; set; }
        public string ID_Al2 { get; set; }
        public string Fe_Al { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string CP { get; set; }
        public string Pro { get; set; }
        public string NIF { get; set; }
        public string Obs { get; set; }
        public string Total { get; set; }
        public string ID_Cli2 { get; set; }
        public string Fac { get; set; }
        public string ID_Fac { get; set; }
        public string Sep { get; set; }
    }

    #endregion

    #region "Modulo Albaranes - Albaranes - Albaran"

    public class strAlbaranesAlbaranesAlbaran
    {
        public string ID_Al { get; set; }
        public Int32 ID_Al2 { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string Cli2Rel { get; set; }
        public string Cli2RelTipo { get; set; }
        public bool IVAInc { get; set; }
        public string Obs { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pai { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pob { get; set; }
        public string TipoIVA { get; set; }
        public string Base { get; set; }
        public string Total { get; set; }
        public string Gan { get; set; }

        //public strPresupuestosPresupuestosPresupuestoSolicitud Soli { get; set; }

        public string Factura { get; set; }

        public strerror Err { get; set; }
    }

    public class strAlbaranesAlbaranAjax
    {
        public string Base { get; set; }
        public string Total { get; set; }
        public string Gan { get; set; }
        public string Bruto { get; set; }
        public string ImpDto { get; set; }
        public string Factura { get; set; }

        public strAlbaranesAlbaranDetallesAjax[] det { get; set; }

        public strDatoS[] Imp { get; set; }

        public strerror Err { get; set; }
    }

    public class strAlbaranesAlbaranDetallesAjax
    {
        public Int32 ID_De_Al { get; set; }
        public string Can { get; set; }
        public string Codigo { get; set; }
        public string Expo { get; set; }
        public string Dto { get; set; }
        public string PVP { get; set; }
        public string PVPF { get; set; }
        //public bool EsAlgo { get; set; }
        //public bool HayPrecio { get; set; }
        //public bool PrecioDif { get; set; }
    }

    public class strAlbaranesAlbaranDetalleAjax
    {
        public Int32 ID_Al2 { get; set; }
        public Int32 ID_De_Al { get; set; }
        public string Can { get; set; }
        public string Codigo { get; set; }
        public string Expo { get; set; }
        public string Dto { get; set; }
        public string DtoE { get; set; }
        public string PVP { get; set; }
        public string Coste { get; set; }
        public string IVA { get; set; }
        public string REQ { get; set; }
        public string IRPF { get; set; }

        public bool EsArt { get; set; }
        public bool EsServ { get; set; }
        public bool EsLibre { get; set; }

        public bool EditArt { get; set; }
        public bool EditServ { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Albaranes - Albaranes - Facturar albaranes"

    public class strAlbaranesFacturarAlbaranesInicio
    {
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string Emp_Fis { get; set; }
        public string Dir { get; set; }
        public string Pai { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pob { get; set; }
        public string NIF { get; set; }
        public string TipCli { get; set; }
        public string CCCli { get; set; }
        public string Forma { get; set; }
        public string TipoIVA { get; set; }
        public string AxDias { get; set; }
        public string AxDiasCli { get; set; }
        public Int32 NumFrac { get; set; }
        public string Dia { get; set; }
        public string Dias { get; set; }
        public string DiasCli { get; set; }
        public string Obs { get; set; }
        public bool EnvMail { get; set; }
        public string Mails { get; set; }

        public string EmpRel { get; set; }
        public string TipEmpRel { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string ObsRel { get; set; }
        public string AxDiasRel { get; set; }
        public string DiaRel { get; set; }
        public string DiasRel { get; set; }
        public bool EnvMailRel { get; set; }
        public string MailsRel { get; set; }
        public bool HeredaForPag { get; set; }
        public bool CobEnEspera { get; set; }


        public strDatoS[] Impuestos { get; set; }
        public string Bruto { get; set; }
        public string ImpDto { get; set; }
        public string Base { get; set; }
        public string Total { get; set; }
        public string lTotal { get; set; }
        public Int32 numAlbs { get; set; }
        public strDatoS[] Albs { get; set; }

        public strDatoS[] Series { get; set; }

        public string Fac { get; set; }
        public string Fe_Fa { get; set; }
        public Int32 ID_Serie { get; set; }
        public Int32 ID_Eli { get; set; }
        public string Fe_Fra { get; set; }

        public string Cob_Inco { get; set; }
        public string Cob_Dev { get; set; }
        public string Cob_Atraso { get; set; }
        public string Cob_Atraso_esp { get; set; }
        public string Cob_NoVenci { get; set; }
        public string Cob_NoVenci_esp { get; set; }
        public string TotalDebe { get; set; }

        public bool RevCli { get; set; }

        public strerror Err { get; set; }
    }

    public class strAlbaranesFacturarAlbaranesVencimientos
    {
        public string TotalFra { get; set; }
        public string Descuadre { get; set; }
        public Int32 ID_For { get; set; }
        public strLista[] Formas { get; set; }
        public Int32 ID_Cue { get; set; }
        public strLista[] Cues { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public strLista[] CueNeg { get; set; }
        public strAlbaranesFacturarAlbaranesVencimientosDetalles[] Venci { get; set; }

        public strerror Err { get; set; }
    }

    public class strAlbaranesFacturarAlbaranesVencimientosDetalles
    {
        public Int32 NumFra { get; set; }
        public string Fe_Ve { get; set; }
        public Int32 ID_For { get; set; }
        public Int32 ID_Cue { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public string Imp { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo - Facturación "

    public class strFacturacionBuscar
    {
        public string buscar { get; set; }
        public Int32 ID_Al2 { get; set; }
        public string Fac { get; set; }
        public Int32 Año { get; set; }
        public Int32 Mes { get; set; }
        public Int32 ID_Serie { get; set; }
        public string Fe_Fa_In { get; set; }
        public string Fe_Fa_Fi { get; set; }

        public string Fe_Ve_In { get; set; }
        public string Fe_Ve_Fi { get; set; }
        public Int32 ID_Tipo { get; set; }
        public Int32 ID_For { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public string Fe_Cob_In { get; set; }
        public string Fe_Cob_Fi { get; set; }

        public string Imp_In { get; set; }
        public string Imp_Fi { get; set; }
    }

    #region "Modulo Facturacion - Facturas - Vencimientos factura

    public class strFacturacionVencimientosFactura
    {
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Fac { get; set; }
        public string Fe_Fa { get; set; }
        public string Emp { get; set; }
        public string NIF { get; set; }
        public string Dir { get; set; }
        public string Pai { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pob { get; set; }
        public string Base { get; set; }
        public string Total { get; set; }
        public string Obs { get; set; }
        public bool Conta { get; set; }
        public string ID_Cli2 { get; set; }
        public string CCCli { get; set; }
        public strerror Err { get; set; }
    }

    public class strFacturacionVencimientosFacturaVencimientos
    {
        public string TotalFac { get; set; }
        public string TotalFra { get; set; }
        public string Descuadre { get; set; }
        public Int32 ID_For { get; set; }
        public strLista[] Formas { get; set; }
        public Int32 ID_Cue { get; set; }
        public strLista[] Cues { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public strLista[] CueNeg { get; set; }
        public strFacturacionVencimientosFacturaVencimientosDetalles[] Venci { get; set; }

        public strerror Err { get; set; }
    }

    public class strFacturacionVencimientosFacturaVencimientosDetalles
    {
        public Int32 ID_FFM { get; set; }
        public Int32 ID_Fra { get; set; }
        public Int32 NumFra { get; set; }
        public string Fe_Exp { get; set; }
        public string Fe_Ve { get; set; }
        public Int32 ID_For { get; set; }
        public Int32 ID_Cue { get; set; }
        public Int32 ID_CueNeg { get; set; }
        public string Imp { get; set; }
        public string EnEspera { get; set; }
        public bool Cob { get; set; }
        public bool Conta { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Cobros"

    public class strFacturacionCobrosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strLista[] Años { get; set; }
        public strLista[] Meses { get; set; }
        public Int32 ID_Tipo { get; set; }
        public strLista[] Formas { get; set; }
        public strLista[] Cuentas { get; set; }
        public strFacturacionCobrosListadoDetalles[] det { get; set; }
        public bool EnMiNombre { get; set; }

        public strerror Err { get; set; }
    }
    public class strFacturacionCobrosListadoDetalles
    {
        public Int32 ID_Fra { get; set; }
        public Int32 ID_Fac { get; set; }
        public string Fe_Ve { get; set; }
        public string Fe_Fa { get; set; }
        public string Fac { get; set; }
        public string ObsFac { get; set; }
        public string NumFra { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string NIF { get; set; }
        public string Imp { get; set; }
        public Int32 ID_For { get; set; }
        public string Forma { get; set; }
        public string Fe_Cob { get; set; }
        public string Fe_Dev { get; set; }
        public bool Inco { get; set; }
        public bool EnEspera { get; set; }
        public bool SiConta { get; set; }

        public string Cue { get; set; }
        public string WarCue { get; set; }
        public string CueNeg { get; set; }
        public string WarCueNeg { get; set; }
        public string Banco { get; set; }


        public string CCCli { get; set; }
        public string ID_Cli2 { get; set; }

        public Int32 EnvFacOK { get; set; }
        public Int32 EnvFacKO { get; set; }
        public Int32 NumCorr { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public strModuloUsuarioTareaPendienteDetalles[] Sol { get; set; }
        public bool Soli_Atraso { get; set; }
        public string Soli_Fe_Prev { get; set; }
        public string Soli_Usu_Prev { get; set; }
        public bool SoliCerr { get; set; }
        public bool SoliTwitHoy { get; set; }

        public bool Atraso { get; set; }
    }

    public class strFacturacionInformacionCobro
    {
        public Int32 ID_Fra { get; set; }
        public Int32 ID_Fac { get; set; }
        public string NumFra { get; set; }

        public string Fe_Exp { get; set; }
        public string Fe_Ve { get; set; }
        public string Forma { get; set; }
        public Int32 ID_For { get; set; }
        public string Cue { get; set; }
        public string WarCue { get; set; }
        public string CueNeg { get; set; }
        public string Imp { get; set; }

        public string Fe_Cob { get; set; }
        public string Fe_Al_Cob { get; set; }
        public string Us_Al_Cob { get; set; }

        public string Fe_Dev { get; set; }
        public strFacturacionInformacionCobroDevoluciones[] Dev { get; set; }

        public strFacturacionInformacionCobroIncobrable Inco { get; set; }

        public string EnEspera { get; set; }
        public bool SiConta { get; set; }

        public string EstadoCob { get; set; }

        public string Fac { get; set; }
        public string Fe_Fa { get; set; }
        public string Cli { get; set; }
        public string NIF { get; set; }
        public string CCCli { get; set; }
        public string ID_Cli2 { get; set; }
        public string ObsFac { get; set; }
        public string Total { get; set; }
        public string TipoCli { get; set; }

        public string ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
        public string TipoCliRel { get; set; }
        public string ObsRel { get; set; }
        public string DtoRel { get; set; }

        public Int32 ID_Soli2 { get; set; }

        public strerror Err { get; set; }
    }

    public class strFacturacionInformacionCobroDevoluciones
    {
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Fe_Dev { get; set; }
        public string Gastos { get; set; }
        public string Imp { get; set; }
        public string Obs { get; set; }
    }

    public class strFacturacionInformacionCobroIncobrable
    {
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Obs { get; set; }
    }

    #endregion

    #region "Modulo - Facturacion - Cobros a negociar"

    public class strFacturacionCobrosANegociarListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strFacturacionCobrosANegociarListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strFacturacionCobrosANegociarListadoDetalles
    {
        public Int32 ID_PCEE { get; set; }
        public string ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
        public string EnEspera { get; set; }
        public string Fac { get; set; }
        public string ID_Fac { get; set; }
        public string Fe_Fa { get; set; }
        public string Forma { get; set; }
        public string Imp { get; set; }
        public string ID_Soli2 { get; set; }
        public string UsuGest { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Comisiones"

    public class strFacturacionComisionesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public string Usu { get; set; }
        public string Cli { get; set; }
        public string Total { get; set; }
        public strFacturacionComisionesListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strFacturacionComisionesListadoDetalles
    {
        public Int32 ID_Fac { get; set; }
        public string Fac { get; set; }
        public string Fe_Cob { get; set; }
        public string Emp { get; set; }
        public string Base { get; set; }
        public string Por { get; set; }
        public string Importe { get; set; }
    }


    #endregion

    #region "Modulo - Facturación - Facturas"

    public class strFacturacionFacturasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strLista[] Años { get; set; }
        public Int32 Año { get; set; }
        public strLista[] Meses { get; set; }
        public Int32 Mes { get; set; }
        public strLista[] Series { get; set; }
        public strFacturacionFacturasListadoDetalles[] det { get; set; }
        public bool EnMiNombre { get; set; }
        public string Total { get; set; }

        public strerror Err { get; set; }
    }
    public class strFacturacionFacturasListadoDetalles
    {
        public Int32 ID_Fac { get; set; }
        public string Fe_Fa { get; set; }
        public string Fac { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string NIF { get; set; }
        public string Total { get; set; }
        public string Obs { get; set; }
        public bool Negativa { get; set; }
        public string CCCli { get; set; }
        public Int32 ID_Cli2 { get; set; }

        public bool SiConta { get; set; }
        public bool FacEMail { get; set; }
        public bool Pdf { get; set; }
        public Int32 EnvFacOK { get; set; }
        public Int32 EnvFacKO { get; set; }
        public Int32 NumCorr { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public string FacAboO { get; set; }
        public string ID_FacAboO { get; set; }
        public string FacAboD { get; set; }
        public string ID_FacAboD { get; set; }
    }

    public class strFacturacionInformacionFactura
    {
        public Int32 ID_Fac2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Fac { get; set; }
        public bool FacEli { get; set; }
        public string Fe_Fa { get; set; }
        public string Cli { get; set; }
        public string NIF { get; set; }
        public string Dir { get; set; }
        public string Pai { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pob { get; set; }
        public string CCCli { get; set; }
        public string ID_Cli2 { get; set; }
        public string ObsFac { get; set; }
        public string Total { get; set; }
        public string EstadoCob { get; set; }
        public string TipoCli { get; set; }
        public string ObsCli { get; set; }

        public bool FacEMail { get; set; }
        public string[] FacMails { get; set; }
        public bool SiConta { get; set; }
        public bool NoEli { get; set; }
        public bool BloCli { get; set; }
        public bool EsNegativa { get; set; }

        public bool NoEnviadoMail { get; set; }
        public string EnvOK { get; set; }
        public string EnvKO { get; set; }
        public bool EnMiNombre { get; set; }
        public strFacturacionInformacionFacturaFracciones[] Fra { get; set; }

        public string ID_Cli2Rel { get; set; }
        public string EmpRel { get; set; }
        public string TipoCliRel { get; set; }
        public string ObsRel { get; set; }
        public bool EnvioFacRel { get; set; } // Propiedad de EmpresasRelacionadas - Fac
        public bool FacEMailRel { get; set; } // Propiedad del cliente relacionado FacEMail
        public string[] FacMailsRel { get; set; }
        public string DtoRel { get; set; }

        public string MailAsunto { get; set; }
        public string MailTexto { get; set; }
        public strDato[] LisPlan { get; set; }

        public Int32 ID_Soli2 { get; set; }
        public Int32 ID_Fac { get; set; }

        public strerror Err { get; set; }
    }

    public class strFacturacionInformacionFacturaFracciones
    {
        public Int32 ID_Fra { get; set; }
        public Int32 ID_Fac { get; set; }
        public string Fe_Ve { get; set; }
        public string Forma { get; set; }
        public string Cue { get; set; }
        public string WarCue { get; set; }
        public string Fe_Cob { get; set; }
        public string Imp { get; set; }
        public string EnEspera { get; set; }
        public string Fe_Dev { get; set; }

        public bool NoEli { get; set; }
        public bool Inco { get; set; }
        public bool Atraso { get; set; }
    }

    public class strFacturacionInformacionFichero
    {
        public string Tipo { get; set; } // html , pdf
        public string Nombre { get; set; }
        public byte[] Fichero { get; set; }
        public string TipoMime { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Albaranes"

    public class strFacturacionAlbaranesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strLista[] Años { get; set; }
        public Int32 Año { get; set; }
        public strLista[] Meses { get; set; }
        public Int32 Mes { get; set; }
        public bool Fac { get; set; }
        public strFacturacionAlbaranesListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strFacturacionAlbaranesListadoDetalles
    {
        public Int32 ID_Al2 { get; set; }
        public string Fe_Al { get; set; }
        public string Obs { get; set; }
        public string Base { get; set; }
        public string Total { get; set; }
        public string Fac { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Recibos"

    public class strFacturacionRecibosInicio
    {
        public Int32 ID_Cue { get; set; }
        public strLista[] Cues { get; set; }
        public Int32 ID_Tipo { get; set; }
        public strLista[] Tipos { get; set; }
        public Int32 ID_Norma { get; set; }
        public strLista[] Normas { get; set; }
        public string Fe_Pres { get; set; }

        public strerror Err { get; set; }
    }

    public class strFacturacionRecibosDisponibles
    {
        public Int32 ID_Fra { get; set; }
        public Int32 ID_Fac { get; set; }
        public Int32 NumFra { get; set; }
        public string Fe_Ve { get; set; }
        public string Fe_Fa { get; set; }
        public string Fe_Dev { get; set; }
        public string Fac { get; set; }
        public string Emp { get; set; }
        public string Imp { get; set; }
        public string ID_Cli2 { get; set; }

        public strerror Err { get; set; }
    }

    public class strFacturacionRecibosResumenHoy
    {
        public string Num { get; set; }
        public string Total { get; set; }
        public strDatoS[] det { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Remesas"

    public class strFacturacionRemesasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strFacturacionRemesasListadoDetalles[] det { get; set; }
        public strLista[] Años { get; set; }
        public Int32 Año { get; set; }
        public strLista[] Meses { get; set; }
        public Int32 Mes { get; set; }

        public strerror Err { get; set; }
    }
    public class strFacturacionRemesasListadoDetalles
    {
        public Int32 ID_Rem { get; set; }
        public string Fe_Pres { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Cue { get; set; }
        public string Tipo { get; set; }
        public Int32 Num { get; set; }
        public string Total { get; set; }
        public string Fe_Efe { get; set; }
        public bool ExpConta { get; set; }
        public bool Blo { get; set; }
        public bool Eli { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Configuración - Series de facturación"

    public class strFacturacionConfiguracionSeriesDeFacturacion
    {
        public strDatoS[] Series { get; set; }
        public strDatoS[] Elis { get; set; }

        public strerror Err { get; set; }
    }

    public class strFacturacionConfiguracionSerieDeFacturacion
    {
        public Int32 ID_Serie { get; set; }
        public string Serie { get; set; }
        public string Num { get; set; }
        public string Fe_Ul { get; set; }
        public Int32 Prio { get; set; }
        public string Obs { get; set; }
        public bool NoMail { get; set; }
        public bool Visible { get; set; }
        public string EsEdit { get; set; }

        public strerror Err { get; set; }
    }

    #endregion


    #region "Modulo - Facturación - Configuración - Recibos y Remesas"

    public class strFacturacionConfiguracionRecibosRemesas
    {
        public Int32 ID_Cue { get; set; }
        public strLista[] Cues { get; set; }
        public Int32 ID_Tipo { get; set; }
        public strLista[] Tipos { get; set; }
        public Int32 ID_Norma { get; set; }
        public strLista[] Normas { get; set; }

        public bool RecRemPropDia { get; set; }
        public bool RecRemNoBor { get; set; }
        public Int32 RecRemMargenDias { get; set; }
        public bool RecRemEnvMail { get; set; }
        public Int32 ID_Mail { get; set; } // RecRemMailEnvio
        public strLista[] Mails { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Configuración - Abono de facturas

    public class strFacturacionConfiguracionAbonoFacturas
    {
        public Int32 ID_Serie { get; set; }
        public strLista[] Series { get; set; }
        public bool AboFacSoloPred { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Configuración - Envío de facturas"

    public class strFacturacionConfiguracionEnvioFacturas
    {
        public bool EnvFacPorMail { get; set; }
        public Int32 ID_Mail { get; set; } // RecRemMailEnvio
        public strLista[] Mails { get; set; }
        public bool EnvFacCopia { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Facturación - Configuración - Devoluciones"

    public class strFacturacionConfiguracionDevoluciones
    {
        public bool CobDevoSoli { get; set; }
        public bool CobDevoCobNego { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo Tarifas"

    public class strTarifasDePreciosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTarifasDePreciosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strTarifasDePreciosListadoDetalles
    {
        public Int32 ID_Tari2 { get; set; }
        public string Fe_Al { get; set; }
        public string Ho_Al { get; set; }
        public string Usu { get; set; }
        public string Tarifa { get; set; }
        public string Expo { get; set; }
        public bool VerEnWeb { get; set; }
        public bool VerEnMail { get; set; }
        public string Dias { get; set; }
        public string Ocu { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo Empresas Relacionadas"

    public class strEmpresasRelacionadasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strEmpresasRelacionadasListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strEmpresasRelacionadasListadoDetalles
    {
        public Int32 ID_Cli2 { get; set; }
        public string Emp { get; set; }
        public string Pro { get; set; }
        public string Pob { get; set; }
        public string Agente { get; set; }
        public string NumCli { get; set; }
        public string VisPend { get; set; }
        public string VisRea { get; set; }
        public string NumPrecios { get; set; }
    }

    public class strEmpresasRelacionadasResumenAgenteListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strEmpresasRelacionadasResumenAgenteListadoRelacionados[] det { get; set; }
        public strEmpresasRelacionadasResumenAgenteListadoRelacionadosDetalles[] res { get; set; }
        public bool EsAdm { get; set; }
        public strLista[] Agentes { get; set; }
        public strerror Err { get; set; }
    }

    public class strEmpresasRelacionadasResumenAgenteListadoRelacionados
    {
        public Int32 ID_Cli2 { get; set; }
        public string EmpRel { get; set; }
        public string Pob { get; set; }

        public strEmpresasRelacionadasResumenAgenteListadoRelacionadosDetalles[] det { get; set; }
    }

    public class strEmpresasRelacionadasResumenAgenteListadoRelacionadosDetalles
    {
        public bool EsCli { get; set; }
        public string Num { get; set; }
        public string NumAsis { get; set; }
        public string NumCob { get; set; }
        public string TotalDebe { get; set; }
        public string NumPres { get; set; }
        public string TotalPres { get; set; }
        public string NumSoli { get; set; }
        public string NumPrecios { get; set; }
    }

    #endregion

    #region "Documentos publicos"

    public class strDocPubListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strDocPubListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strDocPubListadoDetalles
    {
        public Int32 ID_PoolDoc { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Url { get; set; }
        public string Doc { get; set; }
        public string Titulo { get; set; }
        public string Ext { get; set; }
        public bool VisPrev { get; set; }
        public string Icon { get; set; }
    }

    public class strDocumentosPublicosListadoBuscar
    {
        public string buscar { get; set; }
        public string clase { get; set; }
        public Int32 id { get; set; }
        public Int32 numReg { get; set; }
        public Int32 pagina { get; set; }
        public Int32 ID_Idi { get; set; }
    }

    public class strDocPubListadoDetallesAmpliados
    {
        public Int32 ID_PoolDoc { get; set; }
        public Int32 ID_Doc2 { get; set; }
        public string Titulo { get; set; }
        public string Leye { get; set; }
        public string Alt { get; set; }
        public string Expo { get; set; }
        public string Ext { get; set; }
        public bool EsImg { get; set; }
        public strDocPubListadoDetallesAmpliadosResize[] Imgs { get; set; }
        public string Fe_Pub { get; set; }
        public string Icon { get; set; }
        public Int32 ID_Idi { get; set; }

        public strDatoS[] Fuentes { get; set; }

        public strerror Err { get; set; }
    }

    public class strDocPubListadoDetallesAmpliadosResize
    {
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Doc { get; set; }
        public string Url { get; set; }
        public string Tamaño { get; set; }
        public string Alto { get; set; }
        public string Ancho { get; set; }
    }

    #endregion

    #region "Modulo - Soporte"

    public class strSoporteSolicitudNueva
    {
        public string Titulo { get; set; }
        public string Expo { get; set; }
        public bool Urg { get; set; }
        public bool Priv { get; set; }
        public string RefCli { get; set; }
    }

    public class strSoporteSolicitudesAbiertasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strSoporteSolicitudesAbiertasListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strSoporteSolicitudesAbiertasListadoDetalles
    {
        public Int32 ID_Soli2 { get; set; }
        public string Fe_Al { get; set; }
        public string Ho_Al { get; set; }
        public string Usu { get; set; }
        public string Titulo { get; set; }
        public string RefCli { get; set; }
        public string Estado { get; set; }
        public Int32 NumEst { get; set; }
        public bool Urg { get; set; }
        public bool Priv { get; set; }
        public string Fe_Ul { get; set; }
        public string Ho_Ul { get; set; }
        public bool Mia { get; set; }

        public string Emp { get; set; }
        public string Expo { get; set; }
    }

    #endregion

    #region "Modulo - Servicios"

    #region "Modulo - Servicios - Servicio"

    public class strServiciosServicioDetalles
    {
        public Int32 ID_Serv2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Serv { get; set; }
        public string ServFe_Mod { get; set; }
        public string ServUsuMod { get; set; }
        public string IVA { get; set; }
        public string IRPF { get; set; }
        public Int32 ID_Cat2 { get; set; }
        public string Codigo { get; set; }
        public string lPrecio { get; set; }
        public string Precio { get; set; }
        public string PrecioFe_Mod { get; set; }
        public string PrecioUsuMod { get; set; }
        public string PrecioI { get; set; }
        public string PrecioIFe_Mod { get; set; }
        public string PrecioIUsuMod { get; set; }
        public string Coste { get; set; }
        public string CosteFe_Mod { get; set; }
        public string CosteUsuMod { get; set; }
        public string PrecioMin { get; set; }
        public string PrecioMinFe_Mod { get; set; }
        public string PrecioMinUsuMod { get; set; }
        public string CCC { get; set; }
        public string Obs { get; set; }
        public Int32 ID_ArtRel { get; set; }
        public string ArtRel { get; set; }

        public string Benef { get; set; }
        public string BenefPor { get; set; }

        public strLista[] Cats { get; set; }

        public string NoEli { get; set; }
        public string WarnEli { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Servicios - Categorías"

    public class strServiciosCategoriaServiciosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strServiciosCategoriaServiciosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strServiciosCategoriaServiciosListadoDetalles
    {
        public Int32 ID_Cat2 { get; set; }
        public string Cat { get; set; }
        public string Obs { get; set; }
        public string Fam { get; set; }
        public string numServ { get; set; }
    }


    #endregion

    #region "Modulo - Servicios - Base"

    public class strServiciosServiciosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strServiciosServiciosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strServiciosServiciosListadoDetalles
    {
        public Int32 ID_Serv2 { get; set; }
        public string Codigo { get; set; }
        public string Serv { get; set; }
        public string Cat { get; set; }
        public string Fam { get; set; }
        public string Precio { get; set; }
    }

    #endregion

    #endregion

    #region "Modulo - Solicitudes"

    public class strSolicitudesDashBoardInicio
    {
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }

        public strerror Err { get; set; }
    }

    public class strSolicitudesNuevaSolicitudIni
    {
        public Int32[] ID_Modulo { get; set; }
        public Int32 ID_Us { get; set; }
        public String Nom { get; set; }

        public strerror Err { get; set; }
    }

    public class strSolicitudNueva
    {
        public Int32 ID_Cont2 { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public string Expo { get; set; }
        public Int32 ID_Us_Asi { get; set; }
        public strSolicitudAux Aux { get; set; }
        public strSolicitudVinculado[] Vinc { get; set; }
        public string Fe_Prev_Ini { get; set; }

        public strerror Err { get; set; }
    }


    public class strModuloSolicitudesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloSolicitudesListadoDetalles[] det { get; set; }
        public strLista[] Trabajadores { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloSolicitudesListadoDetalles
    {
        public Int32 ID_Soli2 { get; set; }
        public string Fe_Al { get; set; }
        public string Ho_Al { get; set; }
        public string Usu_Alta { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }
        public string Emp { get; set; }
        public string Expo { get; set; }
        public string Fe_Ul { get; set; }
        public string Ho_Ul { get; set; }
        public string Fe_New { get; set; }
        public string Ho_New { get; set; }
        public string Expo_New { get; set; }
        public string Usu_Tarea { get; set; }
        public bool Atraso { get; set; }
        public string Dom { get; set; }
        public string Referer { get; set; }
        public bool Cerr { get; set; }
    }


    public class strSolicitud
    {
        public Int32 ID_Soli2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Dom { get; set; }
        public Int32 ID_Con2 { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public string Origen { get; set; }

        public string Emp { get; set; }
        public string Expo { get; set; }
        public string UsuAsi { get; set; }
        public string UsuCerr { get; set; }
        public string Fe_Cerr { get; set; }

        public strSolicitudAux Aux { get; set; }
        public strSolicitudVinculado[] Vinc { get; set; }

        public strerror Err { get; set; }
    }

    public class strSolicitudAux
    {
        public string Emp { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
        public bool verSMS { get; set; }
        public string Mail { get; set; }
        public string Dir { get; set; }

        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }
        public string DNI { get; set; }

        public string Fe_Na { get; set; }
        public bool EsMujer { get; set; }
    }

    public class strSolicitudVinculado
    {
        public string Tipo { get; set; }
        public Int32 ID_Tipo { get; set; }
        public Int32 ID_ID { get; set; }
        public object Obj { get; set; }
        public string UsuBaj { get; set; }
        public string Fe_Ba { get; set; }
    }

    public class strSolicitudVinculado_Ocupacion
    {
        public Int32 ID_Ocu2 { get; set; }
        public string Usu { get; set; }
        public string Fe_Al { get; set; }
        public string Curso { get; set; }
        public string Fe_In { get; set; }
        public Int32 ID_Curso2 { get; set; }
        public string Plaza { get; set; }

        public string UsuBaj { get; set; }
        public string Fe_Ba { get; set; }

        public string Ubi { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public Int32 ID_Ubi2 { get; set; }
        public Int32 NumPla { get; set; }
        public Int32 NumIns { get; set; }
    }

    public class strSolicitudVinculado_Presupuesto
    {
        public Int32 ID_Pres2 { get; set; }
        public string Usu { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAsi { get; set; }
        public string Emp { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }
        public string ExpoPres { get; set; }
        public string Breve { get; set; }
        public string Obs { get; set; }
        public string ObsPriv { get; set; }
        public string Procesado { get; set; }
        public bool Aceptado { get; set; }
        public string Total { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }

        public string Fe_Ace { get; set; }
        public string Us_Ace { get; set; }
        public string Fe_Rev { get; set; }
        public string Us_Rev { get; set; }
    }

    #endregion

    #region "Calendario"

    public class strCalendario
    {
        public Int32 Año { get; set; }
        public string Mes { get; set; }
        public Int32 numMes { get; set; }
        public string[] DiaSemana { get; set; }
        public string[] aDiaSemana { get; set; }
        public strCalendarioDia[][] Dia { get; set; }
        public Int32 DiasMes { get; set; }
        public object ObjCal { get; set; }
        public Int32 antAño { get; set; }
        public Int32 antMes { get; set; }
        public Int32 sigAño { get; set; }
        public Int32 sigMes { get; set; }
        public strerror Err { get; set; }
    }

    public class strCalendarioDia
    {
        public string Fecha { get; set; }
        public Int32 nDia { get; set; }
        public string Dia { get; set; }
        public string aDia { get; set; }
        public bool Hoy { get; set; }
        public bool EsOtroMes { get; set; }
        public object[] ObjDiaCal { get; set; }
    }

    public class strCalendarioTarea
    {
        public Int32 ID_Cale { get; set; }
        public string Fe_In { get; set; }
        public string Ho_In { get; set; }
        public string Ho_Fi { get; set; }
        public string Asunto { get; set; }
        public bool Rev { get; set; }
    }

    public class strCalendarioTareas
    {
        public Int32 Año { get; set; }
        public string Mes { get; set; }
        public Int32 numMes { get; set; }
        public string[] DiasSemama { get; set; }
        public strCalendarioTareasDia[] Dia { get; set; }
        public Int32 ID_Idi { get; set; }
        public bool verFeAnt { get; set; } // Parametro que nos indica si podemos visualizar calendarios de una fecha anterior a la actual
        public bool verAnt { get; set; }
        public bool verSig { get; set; }
        public Int32 mesAnt { get; set; }
        public Int32 añoAnt { get; set; }
        public Int32 mesSig { get; set; }
        public Int32 añoSig { get; set; }

        public Int32 TotalDiasLab { get; set; }
        public Int32 Nums { get; set; }
        public Int32 Sumas { get; set; }
        public strerror Err { get; set; }
    }

    public class strCalendarioTareasDia
    {
        public string Fecha { get; set; }
        public Int32 Dia { get; set; }
        public Int32 Num { get; set; }
        public Decimal Suma { get; set; }
        public Int32 Nivel { get; set; }
        public bool Hoy { get; set; }
        public bool Descanso { get; set; }
        public bool Festivo { get; set; }
        public bool Vacaciones { get; set; }
        public bool EsOtroMes { get; set; }
        public bool ToolTip { get; set; }
        public string Titulo { get; set; }
    }


    #endregion

    #region "My Gaolos"

    public class strModuloInicio
    {
        public strModuloInicioDetalles[] Ses { get; set; }
        public strModuloInicioDetalles[] Acc { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloInicioDetalles
    {
        public Int32 ID_Reg { get; set; }
        public string Neg { get; set; }
        public string NumAcc { get; set; }
        public string Fe_Al { get; set; }
        public string Fe_Ul { get; set; }
        public string Fe_Cad { get; set; }
        public Int32 ID_App { get; set; }
        public bool Test { get; set; }
    }

    public class strMiPerfil
    {
        public string NIC { get; set; }
        public string Nom { get; set; }
        public bool EditNom { get; set; }
        public string Fe_Al { get; set; }
        public string Ho_Al { get; set; }
        public strMiPerfilNegocios[] Negs { get; set; }
        public strMiPerfilNegocios[] Clis { get; set; }
        public strerror Err { get; set; }
    }
    public class strMiPerfilNegocios
    {
        public string Fe_Al { get; set; }
        public string Ho_Al { get; set; }
        public string UsuAlta { get; set; }
        public string Neg { get; set; }
        public Int32 NumAcc { get; set; }
        public string Fe_Ul_Acc { get; set; }
        public string Ho_Ul_Acc { get; set; }
        public bool Blo { get; set; }
        public string Fe_Blo { get; set; }
        public string Ho_Blo { get; set; }
        public string RefNeg { get; set; }
        public string LockedMy { get; set; }
        public string LockedNeg { get; set; }
    }


    public class strModuloUsuarioTareasPendientesListado
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strListaS[] Negs { get; set; }
        public strModuloUsuarioTareasPendientesListadoDetalles[] det { get; set; }
        public bool MultiNeg { get; set; }

        public string TareasAtraso { get; set; }
        public string TareasNoAtraso { get; set; }
        public string TareasHoy { get; set; }
        public string TareasMañana { get; set; }
        public string TareasFuturas { get; set; }

        public strerror Err { get; set; }
    }
    public class strModuloUsuarioTareasPendientesListadoDetalles
    {
        public Int32 ID_Twit { get; set; }
        public string Fe_Prev_Ini { get; set; }
        public string Ho_Prev_Ini { get; set; }
        public string Expo { get; set; }
        public string ExpoIni { get; set; }
        public string Usu { get; set; }
        public Int32 Prio { get; set; }
        public bool NoIni { get; set; }
        public bool SinCtrlLec { get; set; }
        public bool NoLeido { get; set; }
        public bool Atraso { get; set; }
        public string Neg { get; set; }
        public string EmpRel { get; set; }
        public Int32 Num { get; set; }
    }

    public class strModuloUsuarioTareaPendiente
    {
        public Int32 ID_Twit { get; set; }
        public Int32 ID_TwitIni { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string ExpoIni { get; set; }
        public Int32 Num { get; set; }
        public strModuloUsuarioTareaPendienteDetalles[] det { get; set; }
        public bool MultiNeg { get; set; }
        public string Neg { get; set; }
        public Int32 Prio { get; set; }
        public strListaS[] Negs { get; set; }
        public strSolicitud Soli { get; set; }
        public strDatoS[] Emps { get; set; }
        public bool Cerrada { get; set; }
        public Int32[] ID_Tipos { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloUsuarioTareaPendienteDetalles
    {
        public Int32 ID_Twit { get; set; }
        public string Fe_Al { get; set; }
        public string Ho_Al { get; set; }
        public string Usu { get; set; }
        public string Expo { get; set; }
        public string Fe_Prev_Ini { get; set; }
        public string Ho_Prev_Ini { get; set; }
        public string Fe_Res { get; set; }
        public string Ho_Res { get; set; }
        public string Res { get; set; }
        public string Dias { get; set; }
        public string Neg { get; set; }
        public bool Abierto { get; set; }
        public bool EsNota { get; set; }
        public bool Atraso { get; set; }
    }

    public class strTareaPendienteNueva
    {
        public string RefNeg { get; set; }
        public Int32 ID_TwitPadre { get; set; }
        public Int32 ID_TwitIni { get; set; }
        public string Expo { get; set; }
        public bool Privado { get; set; }

        public string Fe_Prev_Ini { get; set; }
        public string Fe_Prev_Fin { get; set; }
        public Int32 ID_Us_Asi { get; set; }
        public Int32 Prioridad { get; set; }

        public bool CtrlFin { get; set; }
        public bool Acuse { get; set; }

        public Int32 ID_Emp { get; set; }
        public bool EsCli { get; set; }
    }

    public class strTareaPendienteReplanificar
    {
        public string RefNeg { get; set; }
        public Int32 ID_Twit { get; set; }
        public Int32 ID_TwitIni { get; set; }
        public Int32 ID_Us_Asi { get; set; }
        public string Expo { get; set; }
        public Int32 AplazarRapido { get; set; }
        public string AplazarFecha { get; set; }
        public Int32 Prioridad { get; set; }
    }



    #endregion


    #region "Modulo - Contabilidad"

    #region "Modulo Contabilidad - Cuentas - Cuenta movimientos"

    public class strContabilidadCuentasCuentaMovimientos
    {
        public string Fe_Al { get; set; }
        public string UsuAl { get; set; }
        public string Cue { get; set; }
        public string Banco { get; set; }
        public string Swift { get; set; }
        public string Pai { get; set; }
        public string Sufijo { get; set; }
        public string Obs { get; set; }
        public string Saldo { get; set; }
        public string Fe_Saldo { get; set; }
        public bool EsGlo { get; set; }
        public bool EsOcul { get; set; }
        public string UsuOcul { get; set; }
        public string Fe_Ocul { get; set; }
        public string Fe_Ba { get; set; }
        public string UsuBaj { get; set; }
        public string MotBaj { get; set; }

        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strContabilidadCuentasCuentaMovimientosDetalles[] det { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public strerror Err { get; set; }
    }

    public class strContabilidadCuentasCuentaMovimientosDetalles
    {
        public Int32 ID_Mov { get; set; }
        public string Fe_Ope { get; set; }
        public string Fe_Val { get; set; }
        public string Conc { get; set; }
        public string Pago { get; set; }
        public string Ingreso { get; set; }
        public string Saldo { get; set; }
        public bool Conci { get; set; }
        public string ID_Conc { get; set; }
    }

    #endregion


    #region "Modulo - Contabilidad - Exportar Datos"

    public class strModuloContabilidadExportarDatosInicio
    {
        public strLista[] Series { get; set; }
        public strLista[] Años { get; set; }
        public strLista[] Meses { get; set; }
        public strerror Err { get; set; }
    }


    #endregion

    #region "Modulo - Contabilidad - Cuentas Bancarias"

    public class strModuloContabilidadCuentasBancarias
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strDatoS[] det { get; set; }
        public strLista[] Bans { get; set; }
        public strDatoS[] Imports { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadCuentasBancariasDetalles
    {
        public Int32 ID_Cue { get; set; }
        public string Cue { get; set; }
        public string Ban { get; set; }
        public string Obs { get; set; }
        public string Sufijo { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Ul { get; set; }
        public bool Baja { get; set; }
    }

    public class strModuloContabilidadCuentasBancariasCuenta
    {
        public Int32 ID_Cue { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Cue { get; set; }
        public string Ban { get; set; }
        public string Pai { get; set; }
        public string Dir { get; set; }
        public string Obs { get; set; }
        public string Sufijo { get; set; }
        public string Swift { get; set; }
        public string CC { get; set; }
        public string PrefSepa { get; set; }

        public string Fe_In { get; set; }
        public string Fe_Ul { get; set; }

        public string Fe_Ba { get; set; }
        public string UsuBaj { get; set; }
        public bool Baja { get; set; }

        public strerror Err { get; set; }
    }



    #endregion

    #region "Modulo - Contabilidad - Conciliación"

    public class strModuloContabilidadConciliacionDashBoard
    {
        public Int32 NumFraCli { get; set; }
        public Int32 NumFraProv { get; set; }
        public Int32 NumRem { get; set; }
        public Int32 NumTar { get; set; }
        public Int32 NumPasaBan { get; set; }
        public Int32 NumPasaCli { get; set; }
        public Int32 NumCueMov { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadConciliacionContadoresDashBoard
    {
        public Int16 sema { get; set; }
        public Int32 num { get; set; }
        public strLista[] det { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Contabilidad - Conciliación - Pasarelas"

    public class strModuloContabilidadConciliacionPasarelas
    {
        public strLista[] Pasa { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadMovimientosPasarelaListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloContabilidadMovimientosPasarelaListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloContabilidadMovimientosPasarelaListadoDetalles
    {
        public Int32 ID_Mov { get; set; }
        public string Fe_Val { get; set; }
        public string Conc { get; set; }
        public string Dev { get; set; }
        public string Imp { get; set; }
    }

    public class strModuloContabilidadPasarelaMovimientoInformacion
    {
        public Int32 ID_Mov { get; set; }
        public string Fe_Al { get; set; }
        public string Fe_Val { get; set; }
        public string Terminal { get; set; }
        public string TipoOpe { get; set; }
        public string NumAuto { get; set; }
        public string ID_TransElec2 { get; set; }
        public string TipoPago { get; set; }
        public string Imp { get; set; }
        public string MonedaImp { get; set; }
        public string Dev { get; set; }
        public string MonedaDev { get; set; }
        public string NumTarj { get; set; }
        public string PaiTar { get; set; }
        public string EsCredito { get; set; }
        public string Usuario { get; set; }
        public string Entrada { get; set; }
        public string IP { get; set; }
        public string Pais { get; set; }
        public string CodPai { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Contabilidad - Conciliación - Tarjetas"

    public class strModuloContabilidadConciliacionTarjetas
    {
        public strLista[] Tars { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadMovimientosTarjetaListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloContabilidadMovimientosTarjetaListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloContabilidadMovimientosTarjetaListadoDetalles
    {
        public Int32 ID_Mov { get; set; }
        public string Fe_Val { get; set; }
        public string Conc { get; set; }
        public string Imp { get; set; }
    }


    #endregion


    #region "Modulo - Contabilidad - Conciliación - Remesas"

    public class strModuloContabilidadConciliacionRemesas
    {
        public strLista[] Cues { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadRemesasRemesasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloContabilidadRemesasRemesasListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloContabilidadRemesasRemesasListadoDetalles
    {
        public Int32 ID_Rem { get; set; }
        public string Fe_Ve { get; set; }
        public string Tipo { get; set; }
        public string Conc { get; set; }
        public string NumRec { get; set; }
        public string Imp { get; set; }
    }

    #endregion


    #region "Modulo - Contabilidad - Conciliación - Proveedores"

    public class strModuloContabilidadConciliacionPagosProveedores
    {
        public strLista[] Cues { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadPagosProveedoresFacturasListadoBuscar
    {
        public string buscar { get; set; }
        public string clase { get; set; }
        public Int32 id { get; set; }
        public Int32 numReg { get; set; }
        public Int32 pagina { get; set; }
        public Int32 ID_Idi { get; set; }
        public string Imp { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public bool NoConc { get; set; }
    }

    public class strModuloContabilidadPagosProveedoresFacturasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloContabilidadPagosProveedoresFacturasListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloContabilidadPagosProveedoresFacturasListadoDetalles
    {
        public Int32 ID_Fra { get; set; }
        public string Fe_Ve { get; set; }
        public string Fac { get; set; }
        public string Conc { get; set; }
        public string Forma { get; set; }
        public string Imp { get; set; }
        public bool SiConc { get; set; }
        public string Aux { get; set; }
    }


    #endregion


    #region "Modulo - Contabilidad - Conciliación - Clientes"

    public class strModuloContabilidadConciliacionCobrosClientes
    {
        public strLista[] Cues { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadMovimientosCuentaListadoBuscar
    {
        public string buscar { get; set; }
        public string clase { get; set; }
        public Int32 id { get; set; }
        public Int32 numReg { get; set; }
        public Int32 pagina { get; set; }
        public Int32 ID_Idi { get; set; }
        public string Imp { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public bool Todo { get; set; }
    }

    public class strModuloContabilidadFraccionesFacturasListadoBuscar
    {
        public string buscar { get; set; }
        public string clase { get; set; }
        public Int32 id { get; set; }
        public Int32 numReg { get; set; }
        public Int32 pagina { get; set; }
        public Int32 ID_Idi { get; set; }
        public string Imp { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
    }

    public class strModuloContabilidadMovimientosCuentaListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloContabilidadMovimientosCuentaListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloContabilidadMovimientosCuentaListadoDetalles
    {
        public Int32 ID_Mov { get; set; }
        public string Fe_Val { get; set; }
        public string Conc { get; set; }
        public bool EsPago { get; set; }
        public string Pago { get; set; }
        public string Ingreso { get; set; }
    }

    public class strModuloContabilidadFraccionesFacturasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloContabilidadFraccionesFacturasListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloContabilidadFraccionesFacturasListadoDetalles
    {
        public Int32 ID_Fra { get; set; }
        public string Fe_Ve { get; set; }
        public string Fac { get; set; }
        public string Conc { get; set; }
        public string Forma { get; set; }
        public string Imp { get; set; }
    }

    public class strModuloContabilidadFraccionesFacturaInformacion
    {
        public Int32 ID_Fra { get; set; }
        public string Fe_Ve { get; set; }
        public string Imp { get; set; }
        public string NumFra { get; set; }
        public string NumFraT { get; set; }
        public string Forma { get; set; }
        public string Cue { get; set; }
        public string CueNeg { get; set; }
        public bool Incobrable { get; set; }
        public string Fe_Fa { get; set; }
        public string Fac { get; set; }
        public string Emp { get; set; }
        public string Total { get; set; }
        public string ID_Cli2 { get; set; }
        public bool Cob { get; set; }
        public string Fe_Cob { get; set; }
        public string Obs { get; set; }
        public bool HayDoc { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo - Cotabilidad - Exportacion"

    public class strModuloContabilidadExportacion
    {
        public strModuloContabilidadExportacionDetalles[] det { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public Int32 ID_Mail { get; set; }
        public Int32 ID_Tipo { get; set; }
        public string NumEmp { get; set; }
        public strLista[] Mails { get; set; }
        public strerror Err { get; set; }
    }

    public class strModuloContabilidadExportacionDetalles
    {
        public string Nom { get; set; }
        public Int32 ID { get; set; }
        public Int32 ID_Tipo { get; set; }
        public string Expo { get; set; }
        public string Fe_Min { get; set; }
        public string Fe_Max { get; set; }
        public Int32 Num { get; set; }
        public string SinCn { get; set; }
        public string SinCn2 { get; set; }
    }

    #endregion



    #endregion

    #region "Modulo - Biblioteca"

    public class strModuloBibliotecaDocumentoContenido
    {
        public string NombreFichero { get; set; }
        public byte[] Contenido { get; set; }
        public string TipoMime { get; set; }
        public bool InLine { get; set; }
        public strerror Err { get; set; }
    }


    public class strModuloBibliotecaDocumentosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strModuloBibliotecaDocumentosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strModuloBibliotecaDocumentosListadoDetalles
    {
        public Int32 ID_Doc2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public Int32 NumRev { get; set; }
        public string UsuUl { get; set; }
        public string Fe_Ul { get; set; }
        public string TamUl { get; set; }
        public string TamTot { get; set; }
        public string Fichero { get; set; }
        public string Fe_Mod { get; set; }
        public string Icono { get; set; }
        public Int32 NumPub { get; set; }
    }

    public class strModuloBibliotecaDocumentosDocumentoPublicar
    {
        public string Titulo { get; set; }
        public string Expo { get; set; }
        public Int64 Tamaño { get; set; }
        public string NombreFichero { get; set; }
        public string Ext { get; set; }
        public string RutaYNombreFichero { get; set; }
        public DateTime Fe_Mod { get; set; }
        public string MD5 { get; set; }
        public byte[] Contenido { get; set; }
        public bool Es3ero { get; set; }
        public Int32 ID_Idi { get; set; }

        public bool EsImg { get; set; }
        public Int32 Alto { get; set; }
        public Int32 Ancho { get; set; }
        public Int32 id { get; set; }

        public string Revision { get; set; }
        public Int32 ID_Doc2 { get; set; }
    }

    public class strModuloBibliotecaDocumentosDocumentoPublicarResize
    {
        public Int32 ID_PoolDoc { get; set; }
        public strDato[] Fuentes { get; set; }

        public Int32 AnchoLibre { get; set; }
        public Int32 AltoLibre { get; set; }

        public Int32 ID_Token { get; set; }
        public Int32 ID_Neg { get; set; }
        public Int32 ID_Us { get; set; }
        public Int32 ID_Idi { get; set; }
    }

    public class strModuloBibliotecaDocumentoAmpliado
    {
        public Int32 ID_Doc2 { get; set; }
        public string Fe_Al_Doc { get; set; }
        public string Usu_Al_Doc { get; set; }
        public string Tipo { get; set; }
        public string Icono { get; set; }
        public string Usu_Ul { get; set; }
        public string Fe_Ul { get; set; }
        public string Tam_Tot { get; set; }

        public Int32 ID_Idi { get; set; }
        public string Fe_Al_Idi { get; set; }
        public string Usu_Al_Idi { get; set; }
        public string Titulo { get; set; }
        public string Expo { get; set; }
        public Int32 NumRev { get; set; }
        public string Revision { get; set; }
        public string Usu_Ul_Idi { get; set; }
        public string Fe_Ul_Idi { get; set; }
        public string Tam_Ul_Idi { get; set; }
        public string Tam_Tot_Idi { get; set; }
        public string ObsPriv { get; set; }

        public string Fichero { get; set; }
        public string Ext { get; set; }
        public string FullName { get; set; }
        public string Tamaño { get; set; }
        public string Fe_Mod { get; set; }

        public bool EsImg { get; set; }
        public Int32 Ancho { get; set; }
        public Int32 Alto { get; set; }

        public strDatoS[] LisIdis { get; set; }
        public strDatoS[] LisRev { get; set; }
        public strDocPubListadoDetalles[] pub { get; set; }

        public strerror Err { get; set; }
    }

    #endregion


    #region "Cursos"

    #region "Cursos - Exportar Datos"

    public class strModuloCursosExportarDatosInicio
    {
        public strLista[] Estados { get; set; }
        public strLista[] Años { get; set; }
        public strLista[] Meses { get; set; }
        public strLista[] Tipos { get; set; }
        public strLista[] Pros { get; set; }

        public strDatoS[] ExpOcu { get; set; }

        public strerror Err { get; set; }
    }


    #endregion


    #region "Cursos - Cursos"

    public class strCursosCursosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strLista[] Agentes { get; set; }
        public strCursosCursosListadoDetalles[] det { get; set; }
        public Int32 ID_Tipo { get; set; }
        public strLista[] Tipos { get; set; }
        public strerror Err { get; set; }
    }
    public class strCursosCursosListadoDetalles
    {
        public Int32 ID_Curso2 { get; set; }
        public string Fe_In_Cur { get; set; }
        public string Curso { get; set; }
        public string Codigo { get; set; }
        public string Agente { get; set; }
        public string Tipo { get; set; }
        public string Ubi { get; set; }
        public bool Asis { get; set; }
        public bool AsisCerr { get; set; }
        public bool Eva { get; set; }
        public bool EvaCerr { get; set; }
        public bool Cerr { get; set; }
        public string Plazas { get; set; }
        public string PlazasOcu { get; set; }
        public string PlazasIns { get; set; }
        public string PlazasInt { get; set; }

        public string Fe_Fin_Ofe { get; set; }
        public string Fe_Fin_OfeT { get; set; }
        public bool HayOfe { get; set; }
        public string Precio { get; set; }
        public string DiasOfe { get; set; }
    }

    public class strCursoCabecera
    {
        public string Curso { get; set; }
        public string Tipo { get; set; }
        public string Fe_In { get; set; }
        public string Usu { get; set; }
        public string Fe_Al { get; set; }
        public bool Anulado { get; set; }
        public bool Cerrado { get; set; }
        public string Ubi { get; set; }
        public Int32 NumPla { get; set; }
        public Int32 NumIns { get; set; }
    }

    public class strCursosCursoDetallesEditar_General
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Curso { get; set; }
        public string Obs { get; set; }
        public Int32 ID_TipoCurso { get; set; }
        public string TipoCurso { get; set; }
        public Int32 ID_Nivel { get; set; }
        public Int32 PlazasTotal { get; set; }
        public Int32 PlazasOcu { get; set; }
        public Int32 PlazasDis { get; set; }
        public string Codigo { get; set; }
        public string IVA { get; set; }
        public string Fe_In_Pub { get; set; }
        public string Fe_Fi_Pub { get; set; }
        public string Fe_In_Ins { get; set; }
        public string Fe_Fi_Ins { get; set; }
        public string Fe_In_Cur { get; set; }
        public string Fe_Fi_Cur { get; set; }
        public Int32 ID_CentroCoste { get; set; }
        public Int32 ID_Us_Agente { get; set; }
        public string Prioridad { get; set; }
        public string Fe_Act { get; set; }
        public bool online { get; set; }

        public bool Anulado { get; set; }

        public strLista[] TiposCursos { get; set; }
        public strDatoS[] Edades { get; set; }
        public strDatoS[] Precios { get; set; }
        public strLista[] Agentes { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_General_CursoGuardar
    {
        public Int32 ID_Curso2 { get; set; }
        public string Curso { get; set; }
        public Int32 ID_TipoCurso { get; set; }
        public Int32 ID_Nivel { get; set; }
        public Int32 Plazas { get; set; }
        public Int32 ID_CentroCoste { get; set; }
        public string Fe_In_Pub { get; set; }
        public string Fe_Fi_Pub { get; set; }
        public string Fe_In_Ins { get; set; }
        public string Fe_Fi_Ins { get; set; }
        public string Fe_Act { get; set; }
        public bool online { get; set; }
        public string IVA { get; set; }
        public Int32 Prioridad { get; set; }
        public string Obs { get; set; }
        public Int32 ID_Us_Agente { get; set; }
    }

    public class strCursosCursoDetallesEditar_Asignaturas
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }

        public strDatoS[] Asigs { get; set; }
        public strDatoS[] Tits { get; set; }

        public bool EliEva { get; set; }
        public bool EliEvayAsis { get; set; }
        public bool AsisAbrir { get; set; }
        public bool AsisBorrar { get; set; }

        public bool SoloForm { get; set; }
        public Int32 NumSoloForm { get; set; }
        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_Calendario
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }

        public strCursosCursoDetallesEditar_CalendarioDetalles[] det { get; set; }
        public strDato[] Asigs { get; set; }
        public strDato[] TipoH { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_CalendarioDetalles
    {
        public Int32 ID_Cale { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public string Asig { get; set; }
        public string Tema { get; set; }
        public string Tipo { get; set; }
        public string Ubi { get; set; }
        public string Horas { get; set; }
        public string[] Prof { get; set; }
    }

    public class strCursosCursoDetallesEditar_CalendarioObtenerFecha
    {
        public Int32 ID_Cale { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public Int32 ID_Asig2 { get; set; }
        public Int32 ID_Temario2 { get; set; }
        public Int32 ID_TipoH { get; set; }
        public Int32 ID_Ubi2 { get; set; }
        public string Ubi { get; set; }
        public string TotalMin { get; set; }

        public strDato[] Profs { get; set; }
        public strLista[] Asigs { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_Calendario_CursoGuardar
    {
        public Int32 ID_Curso2 { get; set; }
        public Int32 ID_Cale { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public Int32 ID_Asig2 { get; set; }
        public Int32 ID_Temario2 { get; set; }
        public Int32 ID_Ubi2 { get; set; }
        public Int32 ID_TipoH { get; set; }
        public string TotalMin { get; set; }
    }

    public class strCursosCursoDetallesEditar_Comunicacion
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }
        public string InfoBreve { get; set; }
        public string InfoTextoWeb { get; set; }
        public Int32 Info_ID_Mail { get; set; }
        public string InfoCCO { get; set; }
        public string InfoAsunto { get; set; }
        public string InfoTextoMail { get; set; }
        public string InfoSMS { get; set; }

        public string InscInfoTicket { get; set; }
        public string InscTextoWeb { get; set; }
        public bool InscMailActivado { get; set; }
        public Int32 InscInfo_ID_Mail { get; set; }
        public string InscInfoCCO { get; set; }
        public string InscInfoAsunto { get; set; }
        public string InscInfoTextoMail { get; set; }

        public string PCTextoWeb { get; set; }
        public bool PCMailActivado { get; set; }
        public Int32 PCInfo_ID_Mail { get; set; }
        public string PCInfoCCO { get; set; }
        public string PCInfoAsunto { get; set; }
        public string PCInfoTextoMail { get; set; }
        public string PCInfoSMS { get; set; }

        public string Diploma { get; set; }
        public Int32 ID_DipDoc { get; set; }

        public strLista[] Mails { get; set; }
        public strLista[] DocsCurso { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_Alumnos
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }

        public strCursosCursoDetallesEditar_AlumnosDetalles[] det { get; set; }

        public strLista[] Estados { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_AlumnosDetalles
    {
        public Int32 ID_Ocu2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }
        public string Dom { get; set; }
        public string Estado { get; set; }
        public bool Plaza { get; set; }
        public string Nom { get; set; }
        public string Obs { get; set; }
        public string Grupo { get; set; }
        public bool ReqFoto { get; set; }
        public bool HayFoto { get; set; }
        public string Contenido { get; set; }
        public string Debe { get; set; }
        public string Total { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public string SoliExpo { get; set; }
        public bool Cerr { get; set; }
    }

    public class strCursosCursoDetallesEditar_Alumnos_ObtenerOcupacion
    {
        public Int32 ID_Ocu2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Forma { get; set; }
        public string Url { get; set; }

        public string Obs { get; set; }
        public string Obs2 { get; set; }
        public bool ReqFoto { get; set; }
        public bool HayFoto { get; set; }
        public Int32 ID_Estado { get; set; }
        public bool Plaza { get; set; }
        public string Precio { get; set; }
        public string Debe { get; set; }
        public Int32 ID_Grupo { get; set; }

        public Int32 ID_Cli2 { get; set; }
        public Int32 ID_Cont2 { get; set; }
        public string Emp { get; set; }
        public string NIF { get; set; }
        public string[] Tels { get; set; }
        public string[] Mails { get; set; }

        public string Fe_Na { get; set; }
        public bool Sexo { get; set; }
        public string Dir { get; set; }
        public string Pai { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pob { get; set; }
        public string NIC { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public string Fe_Repla { get; set; }

        public strLista[] Estado { get; set; }
        public strLista[] Grupos { get; set; }
        public strLista[] Precios { get; set; }
        public strLista[] CueNegs { get; set; }

        public string Fe_Ahora { get; set; }
        public Int32 ID_CueNeg { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_Alumnos_OcupacionContacto
    {
        public Int32 ID_Curso2 { get; set; }
        public Int32 ID_Ocu2 { get; set; }
        public string Nom { get; set; }
        public string NIF { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
        public string Fe_Na { get; set; }
        public bool Sexo { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }
    }

    public class strCursosCursoDetallesEditar_Grupos
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }

        public strCursosCursoDetallesEditar_GruposDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_GruposDetalles
    {
        public Int32 ID_Gru { get; set; }
        public string Grupo { get; set; }
        public string ni { get; set; }
        public string nd { get; set; }

    }

    public class strCursosCursoDetallesEditar_Grupo
    {
        public Int32 ID_Curso2 { get; set; }
        public Int32 ID_Grupo { get; set; }
        public string Grupo { get; set; }
        public Int32 ID_Cli2Col { get; set; }
        public string CliCol { get; set; }
        public Int32 ID_Cli2Fac { get; set; }
        public string CliFac { get; set; }
        public string ObsPub { get; set; }
        public string Mails { get; set; }

        public strDato[] Docs { get; set; }

        public strerror Err { get; set; }
    }



    public class strCursosCursoDetallesEditar_Asistencia
    {
        public strCursoCabecera Cab { get; set; }
        public Int32 ID_Curso2 { get; set; }
        public bool SoloLectura { get; set; }
        public strCursosCursoDetallesEditar_AsistenciaDetalles[] det { get; set; }
        public Int32 numInteresados { get; set; }
        public Int32 numAsis { get; set; }
        public Int32 numNoAsis { get; set; }
        public Int32 NoAsis { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_AsistenciaDetalles
    {
        public Int32 ID_Ocu2 { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }
        public string Nom { get; set; }
        public string Grupo { get; set; }
        public bool ReqFoto { get; set; }
        public bool HayFoto { get; set; }
        public string Contenido { get; set; }
        public Int32 ID_Soli2 { get; set; }
        public string textSoli { get; set; }
        public bool Cerr { get; set; }
        public string NIF { get; set; }
        public string Tel { get; set; }
        public bool AsisSi { get; set; }
        public bool AsisSoloLec { get; set; }
        public bool NoAsig { get; set; }
    }

    public class strCursosCursoDetallesEditar_Evaluacion
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }

        public bool SoloLectura { get; set; }
        public strCursosCursoDetallesEditar_EvaluacionDetalles[] det { get; set; }
        public Int32 numInteresados { get; set; }
        public Int32 noEval { get; set; }
        public strDatoS[] Tit { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_EvaluacionDetalles
    {
        public Int32 ID_Ocu2 { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }
        public string Nom { get; set; }
        public string NIF { get; set; }
        public string Grupo { get; set; }
        public Int32 ID_Soli2 { get; set; }

        public Int32 ID_Asig { get; set; }
        public string Asignatura { get; set; }
        public bool EvalSi { get; set; }
        public bool EvalSoloLec { get; set; }

    }

    public class strCursosCursoDetallesEditar_Documentacion
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }

        public strCursosCursoDetallesEditar_DocumentacionDetalles[] det { get; set; }
        public strCursosCursoDetallesEditar_DocumentacionGrupos[] gru { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_DocumentacionDetalles
    {
        public Int32 ID_Ocu2 { get; set; }
        public string Emp { get; set; }
        public string NIF { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public Int32 Imprimir { get; set; }
        public Int32 IMail { get; set; }
        public Int32 QoS { get; set; }
        public Int32 ITar { get; set; }
        public string Grupo { get; set; }
        public string Foto { get; set; }
        public Int32 ID_Cli2 { get; set; }
        public bool Blo { get; set; }
    }

    public class strCursosCursoDetallesEditar_DocumentacionGrupos
    {
        public Int32 ID_Gru { get; set; }
        public string Grupo { get; set; }
        public string Emp { get; set; }
        public string ObsPub { get; set; }
        public string Mails { get; set; }
        public Int32 Imprimir { get; set; }
        public Int32 IMail { get; set; }
        public Int32 QoS { get; set; }
        public Int32 ITar { get; set; }
    }

    public class strCursosCursoDetallesEditar_Formadores
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }
        public strCursosCursoDetallesEditar_FormadoresDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_FormadoresDetalles
    {
        public Int32 ID_Prof2 { get; set; }
        public string Formador { get; set; }
        public bool Eva { get; set; }
    }

    public class strCursosCursoDetallesEditar_FormadoresEvaluacion
    {
        public Int32 ID_Prof2 { get; set; }
        public string Prof { get; set; }
        public string Titulo { get; set; }
        public Int32 ID_NFC { get; set; }
        public strCursosCursoDetallesEditar_FormadoresEvaluacionDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_FormadoresEvaluacionDetalles
    {
        public Int32 ID_Camp { get; set; }
        public string Preg { get; set; }
        public string Expo { get; set; }
        public string Cri { get; set; }
        public Int32 ID_NFCD { get; set; }
        public string Valor { get; set; }
        public bool NP { get; set; }
        public string Come { get; set; }
    }

    public class strCursosCursoDetallesEditar_FormadoresEvaluacionGuardar
    {
        public Int32 ID_Curso2 { get; set; }
        public Int32 ID_Prof2 { get; set; }
        public Int32 ID_NFC { get; set; }
        public Int32[] ID_Camps { get; set; }
        public bool[] NPs { get; set; }
        public string[] Comes { get; set; }
        public string[] Valores { get; set; }
        public Int32[] ID_NFCDs { get; set; }
    }

    public class strCursosCursoDetallesEditar_Cierre
    {
        public Int32 ID_Curso2 { get; set; }
        public strCursoCabecera Cab { get; set; }

        public strCursosCursoDetallesEditar_CierreDetalles[] det { get; set; }
        public bool Anu { get; set; }
        public string MotAnu { get; set; }
        public bool Cerr { get; set; }
        public Int32 numInteresados { get; set; }
        public strerror Err { get; set; }
    }

    public class strCursosCursoDetallesEditar_CierreDetalles
    {
        public Int32 ID_Ocu2 { get; set; }
        public string ID_Cli2 { get; set; }
        public string ID_Cont2 { get; set; }
        public string Nom { get; set; }
        public string NIF { get; set; }
        public string Grupo { get; set; }
        public Int32 ID_Soli2 { get; set; }

        public Int32 ID_Asig { get; set; }
        public string Asignatura { get; set; }
        public bool EvalSi { get; set; }
        public bool EvalSoloLec { get; set; }

    }

    #endregion

    #region "Cursos - Transacciones"

    public class strCursosTransaccionesListadoDetalles
    {
        public Int32 ID_Cab { get; set; }
        public string Fe_Al { get; set; }
        public string ID_TransElec2 { get; set; }
        public string Dom { get; set; }
        public string Emp { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string Total { get; set; }
        public bool Res { get; set; }
        public bool NoFin { get; set; }
    }

    #endregion

    #region "Cursos - Ubicaciones"

    public class strCursosUbicacionesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strCursosUbicacionesListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strCursosUbicacionesListadoDetalles
    {
        public Int32 ID_Ubi2 { get; set; }
        public string Fe_Al { get; set; }
        public string Ubi { get; set; }
        public string Obs { get; set; }
        public string Usu { get; set; }
        public string Tipo { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public bool SinLatLon { get; set; }
    }

    public class strCursosUbicacionDetallesEditar
    {
        public Int32 ID_Ubi2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Ubi { get; set; }
        public Int32 ID_TipoUbi { get; set; }
        public string Codigo { get; set; }
        public string Obs { get; set; }
        public string Contacto { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }
        public bool EnWeb { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public strLista[] TipoUbi { get; set; }
        public strLista[] Pais { get; set; }
        public strLista[] Pros { get; set; }
        public strLista[] CPs { get; set; }
        public strLista[] Pobs { get; set; }
        public strerror Err { get; set; }
    }

    #endregion

    #region "Cursos - Configuración"

    public class strCursosConfiguracionTemariosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strCursosConfiguracionTemariosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strCursosConfiguracionTemariosListadoDetalles
    {
        public Int32 ID2 { get; set; }
        public string Fe_Al { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; }
        public string Obs { get; set; }
    }

    public class strCursosConfiguracionTemariosTemario
    {
        public Int32 ID_Temario2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Temario { get; set; }
        public Int32 ID_Tipo2 { get; set; }
        public string Obs { get; set; }
        public strLista[] Tipos { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #endregion



    #region "Webs"

    #region "Webs - Referidos"

    public class strWebsReferidosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strWebsReferidosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strWebsReferidosListadoDetalles
    {
        public Int32 ID_Ref { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Ref { get; set; }
        public string Url { get; set; }
        public string Obs { get; set; }
        public string Fe_Ul { get; set; }
        public string NumVis { get; set; }
    }

    #endregion

    #region "Webs - Dominios"

    public class strWebsDominiosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strWebsDominiosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strWebsDominiosListadoDetalles
    {
        public Int32 ID_Dom { get; set; }
        public string Dominio { get; set; }
        public string Fe_Crea { get; set; }
        public string Fe_Reno { get; set; }
        public bool ProxReno { get; set; }
    }


    public class strWebsDominiosUrlsListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }

        public string Dominio { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string AñosReno { get; set; }
        public string Fe_Reno { get; set; }
        public string Fe_Crea { get; set; }
        public string ObsDom { get; set; }
        public string Ref { get; set; }
        public Boolean WebAct { get; set; }

        public strWebsDominiosUrlsListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strWebsDominiosUrlsListadoDetalles
    {
        public Int32 ID_Url { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Url { get; set; }
        public string UrlRedirect { get; set; }
        public bool EsBeta { get; set; }
        public string Obs { get; set; }
    }


    #endregion

    #region "Webs - Webs"

    public class strWebsWebsListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strWebsWebsListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strWebsWebsListadoDetalles
    {
        public Int32 ID_Tv { get; set; }
        public string Fe_Al { get; set; }
        public string Url { get; set; }
        public string UrlBeta { get; set; }
        public bool Nodo { get; set; }
        public bool Global { get; set; }
        public bool HayPasa { get; set; }
        public bool PreAuto { get; set; }
    }




    #endregion

    #endregion




    #region "Tags"

    public class strTagsListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strTagsListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strTagsListadoDetalles
    {
        public Int32 ID_Tag { get; set; }
        public string Tag { get; set; }
        public string Expo { get; set; }
    }

    public class strTagsTagDetallesEditar
    {
        public Int32 ID_Tag { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Tag { get; set; }
        public string Expo { get; set; }
        public Int32 ID_Idi { get; set; }
        public bool Ext { get; set; }
        public bool Web { get; set; }
        public strerror Err { get; set; }
    }


    #region "Tags - Webs"

    public class strTagsWebsConfigListado
    {
        public strTagsWebsConfigListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strTagsWebsConfigListadoDetalles
    {
        public Int32 ID_Tv { get; set; }
        public string Url { get; set; }
        public string PrefSeo { get; set; }
    }

    #endregion


    #endregion


    #region "Blogs"

    public class strBlogsListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strBlogsListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strBlogsListadoDetalles
    {
        public Int32 ID_Blog { get; set; }
        public string Fe_Al { get; set; }
        public string Url { get; set; }
        public string Dominio { get; set; }
        public string Titulo { get; set; }
        public string Prefijo { get; set; }
        public bool Blo { get; set; }
    }

    public class strBlogsNuevoBlog
    {
        public Int32 ID_Tv { get; set; }
        public string Titulo { get; set; }
        public string Pref { get; set; }
        public Int32 ID_Nodo { get; set; }
        public Int32 ID_Idi { get; set; }
        public Int32 ID_AjuPlanLista { get; set; }
        public Int32 ID_AjuPlanDet { get; set; }

        public strLista[] Webs { get; set; }
        public strLista[] Idiomas { get; set; }
        public strLista[] AjuPlans { get; set; }

        public strerror Err { get; set; }
    }


    public class strBlogsEntradasListado
    {
        public Int32 ID_Blog { get; set; }
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strBlogsEntradasListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strBlogsEntradasListadoDetalles
    {
        public Int32 ID_Ent { get; set; }
        public string Fe_Al { get; set; }
        public string Titulo { get; set; }
        public string Html { get; set; }
        public string Breve { get; set; }
        public strDato[] Cats { get; set; }
        public strDato[] Tags { get; set; }
        public string Autor { get; set; }
        public string Keyword { get; set; }
    }

    public class strBlogsEntradaDetalles
    {
        public Int32 ID_Ent { get; set; }
        public Int32 ID_Blog { get; set; }
        public Int32 ID_Idi { get; set; }
        public string Fe_Al { get; set; }
        public string Titulo { get; set; }
        public string Breve { get; set; }
        public string Html { get; set; }
        public string SEO { get; set; }
        public strDato[] Cats { get; set; }
        public strDato[] Tags { get; set; }
        public strBlogsEntradaDetallesHistorial[] His { get; set; }
        public string Autor { get; set; }
        public string Keyword { get; set; }
        public string Fe_Pub { get; set; }
        public string UsuPub { get; set; }
        public string Fe_Prog { get; set; }
        public string UsuProg { get; set; }
        public Int32 ID_Nodo { get; set; }
        public string Nodos { get; set; }
        public string ImgUrl { get; set; }
        public string ImgAlt { get; set; }
        public Int32 ImgID_Thumbs { get; set; }
        public strerror Err { get; set; }
    }

    public class strBlogsEstilos
    {
        public Int32 ID_Blog { get; set; }
        public string Titulo { get; set; }
        public string SEO { get; set; }
        public string Fe_Pub { get; set; }
        public string UsuPub { get; set; }

        public string TituloRel { get; set; }
        public string Fe_Pub_Rel { get; set; }
        public string UsuPubRel { get; set; }

        public Int32 ID_ImgLista { get; set; }
        public Int32 ID_ImgDet { get; set; }
        public strDato[] ImgsSize { get; set; }
        public strerror Err { get; set; }
    }

    public class strBlogsEntradaDetallesHistorial
    {
        public Int32 ID_His { get; set; }
        public Int32 id { get; set; }
        public Int32 ID_Idi { get; set; }
        public string Fe_Al { get; set; }
        public string Fe_Ent { get; set; }
        public string Usu { get; set; }
        public string UsuEnt { get; set; }
    }

    #endregion

    #region "Modulo produccion - Pedidos"


    public class strPedProdListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strPedProdListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strPedProdListadoDetalles
    {
        public Int32 ID_PedP2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Emp { get; set; }
        public string Expo { get; set; }
        public string Obs { get; set; }
        public string RefCli { get; set; }
    }

    public class strPedProdNuevo
    {
        public Int32 ID_Cli2 { get; set; }
        public Int32 ID_Cont2 { get; set; }
        public string Expo { get; set; }
        public string Obs { get; set; }
        public string RefCli { get; set; }
        public string Fe_Fin { get; set; }

        public bool nuevoPres { get; set; }
        public Int32 ID_Dir { get; set; }
        public string Breve { get; set; }

        public strerror Err { get; set; }
    }



    #endregion


    #region "Modulo produccion - Ordenes"

    public class strOrdProdEstadoOperariosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strOrdProdEstadoOperariosListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strOrdProdEstadoOperariosListadoDetalles
    {
        public Int32 ID_Us { get; set; }
        public string Usu { get; set; }
        public bool Blo { get; set; }

        public string ID_PedP2 { get; set; }
        public string ID_Ord2 { get; set; }
        public string Fe_Al_Prod { get; set; }
        public string Fe_Ini { get; set; }
        public string Emp { get; set; }
        public string Expo { get; set; }
        public string Estado { get; set; }
    }

    public class strOrdProdListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strOrdProdListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strOrdProdListadoDetalles
    {
        public Int32 ID_Ord2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public Int32 ID_PedP2 { get; set; }
        public string Emp { get; set; }
        public string Expo { get; set; }
        public string RefCli { get; set; }
        public Int32 NumOrd { get; set; }
        public bool EsProd { get; set; }
    }

    public class strOrdProdEnProdListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strOrdProdEnProdListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strOrdProdEnProdListadoDetalles
    {
        public Int32 ID_Ord2 { get; set; }
        public string Emp { get; set; }
        public string Expo { get; set; }
        public string RefCli { get; set; }
        public string Asig { get; set; }
        public string Prio { get; set; }
    }

    public class strOrdProdEnProdOrden
    {
        public strOrdProdEnProdOrdenDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strOrdProdEnProdOrdenDetalles
    {
        public Int32 ID_Ctrl2 { get; set; }
        public string Expo { get; set; }
        public string Ope { get; set; }
        public bool Disp { get; set; }
        public string Orden { get; set; }
    }

    public class strOrdProdConIncidenciasListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strOrdProdConIncidenciasListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strOrdProdConIncidenciasListadoDetalles
    {
        public Int32 ID_CtrlI { get; set; }
        public Int32 ID_Ctrl { get; set; }
        public string Fe_Al { get; set; }
        public string Emp { get; set; }
        public Int32 ID_Ord2 { get; set; }
        public string Expo { get; set; }
        public string Orden { get; set; }
        public string Usu { get; set; }
        public string Inci { get; set; }
    }


    public class strOrdenProduccion
    {
        public Int32 ID_PedP2 { get; set; }
        public Int32 ID_Ord2 { get; set; }
        public string Cli { get; set; }
        public string Fe_Al { get; set; }
        public string Expo { get; set; }
        public strOrdenProduccionDetalles[] det { get; set; }
        public Int32 numt { get; set; }
        public string CosteF { get; set; }
        public string PrecioF { get; set; }
        public string GanF { get; set; }
        public strLista[] Tipos { get; set; }
        public strLista[] Accion { get; set; }
        public strLista[] Maq { get; set; }
        public strLista[] Ope { get; set; }
        public strLista[] Trans { get; set; }
        public strLista[] Provs { get; set; }

        public string ID_Pres2 { get; set; }
        public string PresBase { get; set; }

        public Int32 MatDetNum { get; set; }
        public string MatDetCoste { get; set; }
        public string MatDetPrecio { get; set; }

        public strDatoS[] Opes { get; set; }

        public string TotalCosteMaterial { get; set; }
        public string TotalPrecioMaterial { get; set; }
        public string TotalBenefMaterial { get; set; }
        public bool TotalBenefMaterialNegativo { get; set; }
        public string TotalPrecioOpe { get; set; }
        public string TotalCosteOpe { get; set; }
        public string TotalHoras { get; set; }
        public string TotalCosteOpe2 { get; set; }

        public string ResultadoConPres { get; set; }
        public string ResultadoSinPres { get; set; }
        public bool ResultadoConPresNegativo { get; set; }
        public bool ResultadoSinPresNegativo { get; set; }

        public bool EnProduccion { get; set; }
        public bool Cerrada { get; set; }
        public strDatoS[] Albs { get; set; }

        public strOrdenProduccionPresupuestoDetalles[] Pres { get; set; }
        public strerror Err { get; set; }
    }
    public class strOrdenProduccionDetalles
    {
        public Int32 ID_OrdTrb { get; set; }
        public Int32 Pos { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Tipo { get; set; }
        public string Accion { get; set; }
        public string Expo { get; set; }
        public string Prov { get; set; }
        public string CosteF { get; set; }
        public string PrecioF { get; set; }
        public Int32 ID { get; set; }
    }

    public class strOrdenProduccionPresupuestoDetalles
    {
        public Int32 ID_NPCD { get; set; }
        public string Codigo { get; set; }
        public string Expo { get; set; }
        public string Can { get; set; }
        public string CanPend { get; set; }
        public string CanEntr { get; set; }
        public string Precio { get; set; }
    }

    public class strOrdenProduccionDetallesGuardar
    {
        public Int32 ID_OrdTrb { get; set; }
        public Int32 ID_Ord2 { get; set; }
        public Int32 ID_PedTip2 { get; set; }
        public Int32 ID_Acc { get; set; }
        public Int32 ID_Elem { get; set; }

        public string Expo { get; set; }
        public Int32 ID_Ope { get; set; }
        public string Horas { get; set; }
        public string Coste { get; set; }
        public string Precio { get; set; }

        public Int32 ID_Prov2 { get; set; }
        public string Can { get; set; }

        public Int32 ID_Trans2 { get; set; }
        public bool SusM2 { get; set; }
        public Int32 ID_Ope2 { get; set; }
        public string Expo2 { get; set; }
        public string Coste2 { get; set; }
        public string Precio2 { get; set; }

        public Int32 ID_Trans3 { get; set; }
        public bool SusM3 { get; set; }
        public Int32 ID_Ope3 { get; set; }
        public string Expo3 { get; set; }
        public string Coste3 { get; set; }
        public string Precio3 { get; set; }
    }

    public class strOrdenProduccionDetallesCargar
    {
        public Int32 ID_OrdTrb { get; set; }
        public Int32 ID_Ord2 { get; set; }
        public Int32 ID_PedTip2 { get; set; }
        public Int32 ID_Acc { get; set; }
        public Int32 ID_Elem { get; set; }

        public string Expo { get; set; }
        public Int32 ID_Ope { get; set; }
        public string Horas { get; set; }
        public string Coste { get; set; }
        public string Precio { get; set; }

        public Int32 ID_Prov2 { get; set; }
        public string Can { get; set; }

        public Int32 ID_Trans2 { get; set; }
        public bool SusM2 { get; set; }
        public Int32 ID_Ope2 { get; set; }
        public string Expo2 { get; set; }
        public string Coste2 { get; set; }
        public string Precio2 { get; set; }

        public Int32 ID_Trans3 { get; set; }
        public bool SusM3 { get; set; }
        public Int32 ID_Ope3 { get; set; }
        public string Expo3 { get; set; }
        public string Coste3 { get; set; }
        public string Precio3 { get; set; }

        public strerror Err { get; set; }
    }

    public class strOrdenProduccionDetallesMaterial
    {
        public Int32 ID_Ord2 { get; set; }
        public Int32 ID_OrdTrb { get; set; }
        public Int32 ID_NCPPOTM { get; set; }
        public string Can { get; set; }
        public string Codigo { get; set; }
        public string Expo { get; set; }
        public string Coste { get; set; }
        public string Precio { get; set; }
        public string Obs { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Modulo producción - Consultas"

    public class strProduccionCargaDeTrabajoListado
    {
        public string Titulo { get; set; }
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strProduccionCargaDeTrabajoListadoElementosDetalles[] det { get; set; }

        public string Titulo2 { get; set; }
        public Int32 num2 { get; set; }
        public Int32 numt2 { get; set; }
        public string Paginador2 { get; set; }
        public strProduccionCargaDeTrabajoListadoOperariosDetalles[] det2 { get; set; }

        public strerror Err { get; set; }
    }
    public class strProduccionCargaDeTrabajoListadoElementosDetalles
    {
        public Int32 ID_Elem { get; set; }
        public string Elem { get; set; }
        public string Num { get; set; }

        public string HorasT { get; set; }
        public string Horas { get; set; }
        public string Dias { get; set; }
        public string Minutos { get; set; }
    }

    public class strProduccionCargaDeTrabajoListadoOperariosDetalles
    {
        public Int32 ID_Ope { get; set; }
        public string Ope { get; set; }
        public string Num { get; set; }

        public string HorasT { get; set; }
        public string Horas { get; set; }
        public string Dias { get; set; }
        public string Minutos { get; set; }
    }

    public class strProduccionCargaDeTrabajoElementoDetalles
    {
        public string TituloOrd { get; set; }
        public strDatoS[] detOrd { get; set; }
        public string TituloCli { get; set; }
        public strDatoS[] detCli { get; set; }

        public strerror Err { get; set; }
    }

    public class strProduccionTiemposDeProduccionListado
    {
        public string Titulo { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }

        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strProduccionTiemposDeProduccionListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strProduccionTiemposDeProduccionListadoDetalles
    {
        public Int32 ID_Ope { get; set; }
        public string Nom { get; set; }
        public string Num { get; set; }

        public string Horas { get; set; }
        public string Coste { get; set; }
        public string CosteT { get; set; }
        public string CosteG { get; set; }
    }


    #endregion


    #region "Google"

    public class strSeoAnalyticsInicio
    {
        public strDato[] dom { get; set; }
        public strDato[] query { get; set; }
        public strerror Err { get; set; }
    }

    public class strGoogleAnalyticsDatos
    {
        public string Cert { get; set; }
        public string ids { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public string Metric { get; set; }
        public string Dimension { get; set; }
        public string Sort { get; set; }
        public Int32 MaxResult { get; set; }
    }

    public class strGoogleAnaliticsQuery
    {
        public Int32 ID_Query { get; set; }
        public Int32 ID_Dom { get; set; }
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
    }

    public class strGoogleAnalyticsReturn
    {
        public string dom { get; set; }
        public string query { get; set; }
        public List<string>[] Header { get; set; }
        public IList<IList<string>> Rows { get; set; }
        public string[] Total { get; set; }
        public strWebSesionesPorDominio visitas { get; set; }
        public strerror Err { get; set; }
    }

    public class strWebSesionesPorDominio
    {
        public strDato[] doms { get; set; }
        public string[] labels { get; set; }
        public Int32[][] visitas { get; set; }
        public strerror err { get; set; }
    }

    #endregion

    #region "Graficas"

    public class strGraficaQueso2Valores // sin uso
    {
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public strListado Valores { get; set; }
        public strerror Err { get; set; }
    }

    #endregion  


    #region "Situacion"

    public class strSituacionUsuarios
    {
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public strListado TareasNuevas { get; set; }
        public strListado TareasRe { get; set; }
        public strListado TareasCerradas { get; set; }
        public strListado PresupEnv { get; set; }
        public strerror Err { get; set; }
    }

    public class strSituacionClientes
    {
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public strListado CliNuevos { get; set; }
        public strListado CliBlo { get; set; }
        public strListado CliBaja { get; set; }

        public strListado CliRelNuevos { get; set; }
        public strListado CliRelBaja { get; set; }

        public strListado CliFac { get; set; }
        public strListado CliRelFac { get; set; }
        public strListado CliRelNumFac { get; set; }

        public strerror Err { get; set; }
    }

    public class strSituacionEmpresa
    {
        public string Fe_In { get; set; }
        public string Fe_Fi { get; set; }
        public Int32 NumCliNew { get; set; }
        public Int32 NumFacCliNew { get; set; }
        public string ImpFacCliNew { get; set; }
        public Int32 NumCliDel { get; set; }

        public Int32 NumPresNew { get; set; }
        public string ImpPresNew { get; set; }
        public Int32 NumPresAcep { get; set; }
        public string ImpPresAcep { get; set; }

        public Int32 NumFac { get; set; }
        public string ImpFac { get; set; }

        public Int32 NumCob { get; set; }
        public string ImpCob { get; set; }

        public Int32 NumAsisNew { get; set; }
        public Int32 NumAsisFin { get; set; }
        public Int32 NumPartesCerr { get; set; }

        public Int32 NumManNew { get; set; }
        public Int32 NumManDel { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Permisos"

    public class strPermisosNodos
    {
        public string Titulo { get; set; }
        public Nodo nodos { get; set; }
        public strerror Err { get; set; }
    }

    public class strPermisosNodo
    {
        public string Grupo { get; set; }
        public string Expo { get; set; }
        public strPermisosNodoModulo[] disp { get; set; }
        public strPermisosNodoModulo[] sel { get; set; }
        public strerror Err { get; set; }
    }

    public class strPermisosNodoModulo
    {
        public Int32 ID_Rel { get; set; }
        public Int32 ID_Modulo { get; set; }
        public Int32 ID_Apa { get; set; }
        public string Modulo { get; set; }
        public string Obs { get; set; }
        public bool Crear { get; set; }
        public bool Modificar { get; set; }
        public bool Consultar { get; set; }
        public bool Eliminar { get; set; }
        public bool Listar { get; set; }
        public bool Autor { get; set; }
        public bool Denegar { get; set; }
    }

    #endregion

    #region "Envios - Zonas"

    public class strZonas
    {
        public Int32 ID_ZonaSub { get; set; }
        public string ZonaSub { get; set; }
    }

    #endregion

    #region "Mi Empresa"

    #region "Mi Empresa - Mi Empresa"

    public class strMiEmpresaMiEmpresaCabecera
    {
        public string Neg { get; set; }
        public string NegFis { get; set; }
        public string NIF { get; set; }
        public string Fe_Al { get; set; }
    }

    public class strMiEmpresaMiEmpresa_General
    {
        public strMiEmpresaMiEmpresaCabecera Cab { get; set; }

        public strMiEmpresaMiEmpresaDireccion[] Dirs { get; set; }
        public strDatoS[] Tels { get; set; }
        public strDatoS[] Mails { get; set; }
        public strDatoS[] Centros { get; set; }
        public strDatoS[] Webs { get; set; }

        public strerror Err { get; set; }
    }

    public class strMiEmpresaMiEmpresaDireccion
    {
        public string ID_Dir { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }
        public string Obs { get; set; }
        public bool EsCom { get; set; }
        public bool EsFis { get; set; }
    }


    public class strMiEmpresaMiEmpresaDireccionDetalle
    {
        public Int32 ID_Dir { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string Pro { get; set; }
        public string CP { get; set; }
        public string Pai { get; set; }

        public string Obs { get; set; }

        public strDatoS[] Pais { get; set; }
        public strDatoS[] Pros { get; set; }
        public strDatoS[] CPs { get; set; }
        public strDatoS[] Pobs { get; set; }

        public strerror Err { get; set; }
    }

    public class strMiEmpresaMiEmpresaMailDetalles
    {
        public Int32 ID_Mail { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Mail { get; set; }
        public string ObsMail { get; set; }
        public string Smtp { get; set; }
        public string PuertoSmtp { get; set; }
        public bool SSL { get; set; }
        public string Us { get; set; }
        public string Pass { get; set; }

        public strerror Err { get; set; }
    }

    #endregion

    #region "Mi Empresa - Mi Empresa - Fiscal"

    public class strMiEmpresaMiEmpresa_Fiscal
    {
        public strMiEmpresaMiEmpresaCabecera Cab { get; set; }

        public string Emp { get; set; }
        public string EmpFis { get; set; }
        public string NIF { get; set; }
        public string AxDias { get; set; }
        public string IVA { get; set; }
        public bool REQ { get; set; }
        public string Reg { get; set; }
        public string Obs { get; set; }
        public Int32 ID_Cli2Gaolos { get; set; }
        public bool IVA_Inc { get; set; }
        public Int32 ID_Mon { get; set; }
        public bool Blo { get; set; }


        public strLista[] Monedas { get; set; }
        public strLista[] CueNegs { get; set; }
        public strDatoS[] Formas { get; set; }
        public strLista[] Dias { get; set; }

        // Cuentas empresa

        public strerror Err { get; set; }
    }

    #endregion

    #region "Mi Empresa - Consultas - Situacion"

    public class strMiEmpresaConsultasSituacion
    {
        public string AlbAbiNum { get; set; }
        public string AlbAbibBase { get; set; }
        public string AlbAbiTotal { get; set; }
        public string PresAcepAbiNum { get; set; }
        public string PresAcepAbiBase { get; set; }
        public string PresAcepAbiTotal { get; set; }
        public string PedCliAbiNum { get; set; }
        public string PedCliAbiBase { get; set; }
        public string PedCliAbiTotal { get; set; }
        public string StockValor { get; set; }

        public string CobAtrasoNum { get; set; }
        public string CobAtrasoImp { get; set; }
        public string CobNum { get; set; }
        public string CobImp { get; set; }
        public string CobEsEspeAtrasoNum { get; set; }
        public string CobEsEspeAtrasoImp { get; set; }
        public string CobEsEspeNum { get; set; }
        public string CobEsEspeImp { get; set; }


        public strMiEmpresaConsultasSituacionDetalles[] cli { get; set; }
        public strMiEmpresaConsultasSituacionDetalles[] presNuevo { get; set; }
        public strMiEmpresaConsultasSituacionDetalles[] presAcep { get; set; }
        public strMiEmpresaConsultasSituacionDetalles[] pedCli { get; set; }

        public strerror Err { get; set; }
    }
    public class strMiEmpresaConsultasSituacionDetalles
    {
        public Int32 Año { get; set; }
        public Int32 Num { get; set; }
        public decimal NumRatio { get; set; }
        public decimal Base { get; set; }
        public decimal BaseRatio { get; set; }
        public decimal Total { get; set; }
        public decimal TotalRatio { get; set; }
        public bool AñoCompleto { get; set; }
    }

    #endregion


    #region "Mi Empresa - Grupos Usuarios"

    public class strMiEmpresaGruposUsuariosListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strMiEmpresaGruposUsuariosListadoDetalles[] det { get; set; }

        public strerror Err { get; set; }
    }
    public class strMiEmpresaGruposUsuariosListadoDetalles
    {
        public Int32 ID_Grupo { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Grupo { get; set; }
        public string Obs { get; set; }
        public string Num { get; set; }
    }

    public class strMiEmpresaGruposUsuariosGrupo
    {
        public Int32 ID_Grupo { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Grupo { get; set; }
        public string Obs { get; set; }
        public string Num { get; set; }

        public strDatoS[] Usus { get; set; }

        public strerror Err { get; set; }
    }

    #endregion


    public class strMiEmpresaUsuarios
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strMiEmpresaUsuarioDetalles[] Usu { get; set; }
        public strerror Err { get; set; }
    }

    public class strMiEmpresaUsuarioDetalles
    {
        public Int32 ID_Rel { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAlta { get; set; }
        public string Nom { get; set; }
        public string NIC { get; set; }
        public string Dep { get; set; }
        public bool AccWeb { get; set; }
        public bool AccApp { get; set; }
        public bool EsAgente { get; set; }
        public bool EsTecnico { get; set; }
        public bool EsAdm { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
        public bool Firma { get; set; }
        public string Fe_Ul_Acc { get; set; }
        public string NumAcc { get; set; }
        public bool Blo { get; set; }
        public bool Eli { get; set; }
        public string Fe_Eli { get; set; }
    }

    public class strMiEmpresaUsuarioInfo
    {
        public Int32 ID_Rel { get; set; }
        public string Fe_Al { get; set; }
        public string UsuAlta { get; set; }
        public bool EditNom { get; set; }
        public string Nom { get; set; }
        public string NIC { get; set; }
        public Int32 ID_Dep { get; set; }
        public Int32 ID_Tel { get; set; }
        public Int32 ID_Mail { get; set; }
        public bool AccWeb { get; set; }
        public bool AccApp { get; set; }
        public bool EsAgente { get; set; }
        public bool EsTecnico { get; set; }
        public bool EsAdm { get; set; }
        public string Firma { get; set; }
        public string FirmaPreview { get; set; }
        public string Fe_Ul_Acc { get; set; }
        public string NumAcc { get; set; }
        public bool Blo { get; set; }
        public string UsuBlo { get; set; }
        public string Fe_Blo { get; set; }
        public string Expo_Blo { get; set; }
        public bool Eli { get; set; }
        public string Fe_Eli { get; set; }

        public strLista[] Deps { get; set; }
        public strLista[] Tels { get; set; }
        public strLista[] Mails { get; set; }

        public strPermisosNodoModulo[] disp { get; set; }
        public strPermisosNodoModulo[] sel { get; set; }

        public string usuPrecio { get; set; }
        public string usuCoste { get; set; }

        public strerror Err { get; set; }

    }

    public class strMiEmpresaContratacion
    {
        public strMiEmpresaModulos[] disp { get; set; }
        public strMiEmpresaModulos[] contr { get; set; }
        public strerror Err { get; set; }
    }

    public class strMiEmpresaModulos
    {
        public Int32 ID_Contr { get; set; }
        public Int32 ID_Modulo { get; set; }
        public string Fecha { get; set; }
        public string Usu { get; set; }
        public string Modulo { get; set; }
        public string Obs { get; set; }
        public bool Base { get; set; }
        public string Precio { get; set; }
    }

    public class strMiEmpresaConfiguracionHtmls
    {
        public string HtmlFac { get; set; }
        public string UsuFac { get; set; }
        public string Fe_Ul_Fac { get; set; }
        public string HtmlFacDir { get; set; }
        public string UsuFacDir { get; set; }
        public string Fe_Ul_FacDir { get; set; }
        public string HtmlAlb { get; set; }
        public string UsuAlb { get; set; }
        public string Fe_Ul_Alb { get; set; }
        public string HtmlPres { get; set; }
        public string UsuPres { get; set; }
        public string Fe_Ul_Pres { get; set; }

        public string HtmlRecibo { get; set; }
        public string UsuReci { get; set; }
        public string Fe_Ul_Reci { get; set; }

        public string HtmlAsis { get; set; }
        public string UsuAsis { get; set; }
        public string Fe_Ul_Asis { get; set; }

        public string HtmlParte { get; set; }
        public string UsuParte { get; set; }
        public string Fe_Ul_Parte { get; set; }

        public strerror Err { get; set; }

    }

    #endregion

    #region "Modulo Elementos"

    public class strSubElementoPlantillaDetalles
    {
        public Int32 ID_SubE2 { get; set; }
        public string SubElem { get; set; }
        public string Obs { get; set; }
        public strElementPlantillaDetallesValores[] carac { get; set; }
        public strElementPlantillaDetallesValores[] man { get; set; }
        public strerror Err { get; set; }
    }

    public class strElementoPlantillaDetalles
    {
        public Int32 ID_PlanElem2 { get; set; }
        public string Plantilla { get; set; }
        public string Obs { get; set; }
        public Int32 ID_Cat2 { get; set; }
        public strLista[] Cat { get; set; }
        public strLista[] Tipos { get; set; }
        public strElementPlantillaDetallesValores[] carac { get; set; }
        public strElementPlantillaDetallesValores[] man { get; set; }
        public strerror Err { get; set; }
    }

    public class strElementPlantillaDetallesValores
    {
        public Int32 ID_PlanElemCarac { get; set; }
        public string Carac { get; set; }
        public string Obs { get; set; }
        public bool EsAviso { get; set; }
        public strerror Err { get; set; }
    }

    public class strElementoCaracteristicaDetalles
    {
        public Int32 ID_CareDef2 { get; set; }
        public string Carac { get; set; }
        public string Obs { get; set; }
        public bool Libre { get; set; }
        public strElementoCaracteristicaDetallesValores[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strElementoCaracteristicaDetallesValores
    {
        public Int32 ID_ValDef { get; set; }
        public string Valor { get; set; }
        public bool ValorOK { get; set; }
        public Int32 Orden { get; set; }
        public strerror Err { get; set; }
    }

    public class strElementoCaracteristica
    {
        public Int32 ID_CareDef { get; set; }
        public string Carac { get; set; }
        public bool Libre { get; set; }
        public bool EsAviso { get; set; }
        public strDato[] Lis { get; set; }
        public strerror Err { get; set; }
    }

    public class strElementosTipo
    {
        public Int32 ID_Tipo2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Tipo { get; set; }
        public string NumColCarac { get; set; }
        public string NumColPreg { get; set; }
        public string NumServ { get; set; }
        public strDatoS[] det { get; set; }
        public strDatoS[] Marcas { get; set; }
        public string NumMarcas { get; set; }
        public strerror Err { get; set; }
    }


    #endregion

    #region "Mantenimientos"

    public class strMantenimientoDetalleAsistencia
    {
        public Int32 ID_Elem { get; set; }
        public string RefCli { get; set; }
        public string Ubi { get; set; }
        public string Serv { get; set; }
        public string Año { get; set; }
        public strDato[] Años { get; set; }
        public Int32 ID_Marca { get; set; }
        public strDato[] Marcas { get; set; }
        public Int32 ID_Man2 { get; set; }
        public Int32 ID_ManPlanDet { get; set; }
        public strElementoCaracteristica[] carac { get; set; }
        public strMantenimientoDetalleAsistenciaSubElem[] SubElems { get; set; }
        public strElementoCaracteristica[] caracman { get; set; }
        public strDato[] tiposub { get; set; }
        public strerror Err { get; set; }
    }

    public class strMantenimientoDetalleAsistenciaSubElem
    {
        public Int32 ID_ManPlanDetSub { get; set; }
        public Int32 ID_SubE { get; set; }
        public string Tipo { get; set; }
        public string Num { get; set; }
        public string Marca { get; set; }
        public string Obs { get; set; }
        public strElementoCaracteristica[] carac { get; set; }
        public strElementoCaracteristica[] caracman { get; set; }

    }

    #endregion

    #region "Asistencias"

    public class strAsistenciasRutasOperarioInfoAsistencia
    {
        public Int32 ID_Asis2 { get; set; }
        public string Expo { get; set; }
        public string ObsInt { get; set; }
        public strAsistenciasRutasOperarioInfoAsistenciaPresupuesto Pres { get; set; }
        public bool Man { get; set; }
        public strAsistenciasRutasOperarioInfoAsistenciaMantenimiento ManDet { get; set; }
        public string enpause { get; set; }
        public strDatoS[] partes { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasRutasOperarioInfoAsistenciaPresupuesto
    {
        public Int32 ID_Pres2 { get; set; }
        public strDato[] det { get; set; }
    }

    public class strAsistenciasRutasOperarioInfoAsistenciaMantenimiento
    {
        public Int32 ID_Man2 { get; set; }
        public Int32 ID_ManPlan { get; set; }
        public strDatoS[] det { get; set; }
    }


    public class strAsistenciasRutasPlanificarCargaTrabajoCarga
    {
        public strAsistenciasRutasPlanificarCargaTrabajo[] Carga { get; set; }
        public strerror Err { get; set; }
    }

    public class strAsistenciasRutasPlanificarCargaTrabajo
    {
        public string Nom { get; set; }
        public string Durada { get; set; }
        public Int32 ID_AR { get; set; }
        public string Fecha { get; set; }
        public strAsistenciasRutasPlanificarCargaTrabajoDetalles[] det { get; set; }
    }

    public class strAsistenciasRutasPlanificarCargaTrabajoDetalles
    {
        public string Dir { get; set; }
        public string Pob { get; set; }
        public string CP { get; set; }
        public string Emp { get; set; }
        public string Adm { get; set; }
        public string Durada { get; set; }
        public string Hora { get; set; }
        public Int32 ID_NAR { get; set; }
    }

    public class strAsistenciasRutaAsignarValoresInicio
    {
        public strZonas[] Zonas { get; set; }
        public strAsistenciasTipos[] Tipos { get; set; }
        public Int32 Urg { get; set; }
        public Int32 Man { get; set; }
        public strAsistenciasRutaAsignarDisponibles[] Asis { get; set; }
        public strLista[] Tecnicos { get; set; }
        public strLista[] Trabajadores { get; set; }
        public strerror Err { get; set; }

    }

    public class strAsistenciasRutaAsignarDisponibles
    {
        public Int32 ID_Asis2 { get; set; }
        public string Dir { get; set; }
        public string Emp { get; set; }
        public string Pob { get; set; }
        public string CP { get; set; }
        public string Adm { get; set; }
        public string Tipo { get; set; }
        public bool Urg { get; set; }
        public bool Man { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }

    public class strAsistenciasRutaConfirmarValoresInicio
    {
        public strAsistenciasRutaConfirmar[] Asis { get; set; }
        public strDatoS[] Dias { get; set; }
        public strerror Err { get; set; }

    }

    public class strAsistenciasRutaConfirmar
    {
        public Int32 ID_Asis2 { get; set; }
        public string Dir { get; set; }
        public string Emp { get; set; }
        public string Pob { get; set; }
        public string CP { get; set; }
        public string Adm { get; set; }
        public string Tipo { get; set; }
        public bool Urg { get; set; }
        public bool Man { get; set; }
        public string Cont { get; set; }
        public string Tel { get; set; }
        public string Obs { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Expo { get; set; }
        public Int32 ID_AsisConf { get; set; }
    }

    public class strAsistenciasTipos
    {
        public Int32 ID_Cat2 { get; set; }
        public string Cat { get; set; }
    }

    #region "Asistencias - Android"

    public class strAndroidAsistenciasFiltroOperario
    {
        public strDatoS[] zonas { get; set; }
        public strDatoS[] tipos { get; set; }
        public strDatoS[] mans { get; set; }
        public strDatoS[] pobs { get; set; }
        public strDatoS[] cps { get; set; }
        public strDatoS[] dias { get; set; }
        public strerror err { get; set; }

    }

    #endregion


    #endregion




    #region "CMS"

    public class strWebNodoEdtiarHtml
    {
        public string Html { get; set; }
        public string Titulo { get; set; }
        public string Palabra { get; set; }
        public string Meta { get; set; }
        public string CustomCss { get; set; }
        public string CustomHeadJs { get; set; }
        public string CustomTopHeader { get; set; }
        public string CustomHeader { get; set; }
        public string CustomFooter { get; set; }
        public string CustomFooterJs { get; set; }
        public string Breve { get; set; }

        public string Url { get; set; }
        public Int32 ID_Tv { get; set; }
        public Int32 ID_Nodo { get; set; }
        public string Nodo { get; set; }
        public Int32 ID_Idi { get; set; }

        public Int32 ID_ImgLista { get; set; }
        public Int32 ID_ImgDet { get; set; }
        public strDato[] ImgsSize { get; set; }

        public strerror Err { get; set; }
    }

    public class strWebNodoEdtiar
    {
        public string Ruta { get; set; }
        public string NomNodo { get; set; }
        public string Titulo { get; set; }
        public string Web { get; set; }
        public string Url { get; set; }
        public string UrlParcial { get; set; }
        public string UrlBase { get; set; }
        public string Palabra { get; set; }
        public string Meta { get; set; }
        public bool Base { get; set; }

        public string CustomCss { get; set; }
        public string CustomHeadJs { get; set; }
        public string CustomTopHeader { get; set; }
        public string CustomHeader { get; set; }
        public string CustomFooter { get; set; }
        public string CustomFooterJs { get; set; }
        public string CustomHtml { get; set; }
        public string Breve { get; set; }
        public string Nodos { get; set; }

        public Int32 ID_Tv { get; set; }
        public Int32 ID_Nodo { get; set; }
        public Int32 ID_Idi { get; set; }

        public string ImgUrl { get; set; }
        public string ImgAlt { get; set; }
        public Int32 ImgID_Thumbs { get; set; }

        public strerror Err { get; set; }
    }

    public class strCmsWeb
    {
        public Int32 ID_Tv { get; set; }
        public string Web { get; set; }
        public Nodo nodos { get; set; }
        public string Html { get; set; }
        public Idioma[] Idioma { get; set; }
        public strerror Err { get; set; }
    }


    public class strCmsWebListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strCmsWebListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strCmsWebListadoDetalles
    {
        public Int32 ID_Tv { get; set; }
        public string Fe_Al { get; set; }
        public string Web { get; set; }
        public string Beta { get; set; }
        public string Dom { get; set; }
        public bool Blo { get; set; }


    }

    public class strCmsWebNuevaWebDominios
    {
        public strLista[] doms { get; set; }
        public strLista[] ftps { get; set; }
        public strerror Err { get; set; }
    }

    public class strCmsWebNuevaWebDominiosUrls
    {
        public strDato[] Urls { get; set; }
        public strerror Err { get; set; }
    }

    #region "CMS - Componentes"

    public class strCmsWebComponentesListado
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public strCmsWebComponentesListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strCmsWebComponentesListadoDetalles
    {
        public Int32 ID_Compo2 { get; set; }
        public string Fe_Al { get; set; }
        public string Usu { get; set; }
        public string Breve { get; set; }
        public string Expo { get; set; }
        public bool EsHtml { get; set; }


    }

    public class strCmsWebComponentesPropiedadesComponenteListado
    {
        public string Componente { get; set; }
        public strCmsWebComponentesPropiedadesComponenteListadoDetalles[] det { get; set; }
        public strerror Err { get; set; }
    }
    public class strCmsWebComponentesPropiedadesComponenteListadoDetalles
    {
        public Int32 ID_CompoProp { get; set; }
        public string Prop { get; set; }
        public string ObsPub { get; set; }
        public string Valor { get; set; }
    }


    #endregion

    #endregion

    #region "Listados"

    public class strbuscarlistado
    {
        public string buscar { get; set; }
        public Int32 id { get; set; }
        public Int32 id2 { get; set; }
        public Int32 numReg { get; set; }
        public Int32 pagina { get; set; }
        public Int32 ID_Idi { get; set; }
        public string clase { get; set; }
        public object obj { get; set; }
    }


    #endregion


    #region "Tipo de datos base"

    public class transportout
    {
        //public idiomaGrupo Labels { get; set; }
        public object obj { get; set; }
        public Idioma[] labels { get; set; }
        public string tmp { get; set; }
        public menusCounter[] idc { get; set; }
        //public contenedorMenuLateral MenuLateral { get; set; }
        //public contenedormenusUsuario MenusUsuario { get; set; }
        public strerror err { get; set; }
    }

    public class TransportOutInfo
    {
        public Int32 ID_Ses { get; set; }
        public Int32 ID_Us { get; set; }
        public Int32 ID_Neg { get; set; }
        public Int32 ID_Dom { get; set; }
        public Int32 ID_Emp { get; set; }
        public Int32 ID_Cont { get; set; }
        public bool EsCli { get; set; }
        public bool EsTrabajador { get; set; }
        public bool EsProv { get; set; }
        public bool EsAdm { get; set; }
        public bool EsAgente { get; set; }
        public bool EsTecnico { get; set; }
        public Int32 ID_Modulo { get; set; }
        public Int32 ID_Aci { get; set; }
        public Int32 ID_Apa { get; set; }
        public bool EsSet { get; set; }
        public Int32 ID_App { get; set; }
        public bool EsGlobal { get; set; }
        public bool ErrVali { get; set; } // Error validación
        public bool Test { get; set; }
    }


    public class strDato
    {
        public int datoI { get; set; }
        public decimal datoD { get; set; }
        public string datoS { get; set; }
        public bool datoB { get; set; }
        public strerror Err { get; set; }
    }

    public class strDatoS
    {
        public int datoI { get; set; }
        public decimal datoD { get; set; }
        public bool datoB { get; set; }
        public string datoS1 { get; set; }
        public string datoS2 { get; set; }
        public string datoS3 { get; set; }
        public string datoS4 { get; set; }
        public string datoS5 { get; set; }
        public strerror Err { get; set; }
    }



    #endregion

    #region "Requests"

    public class requestInfoTracert
    {
        public requestInfo[] Tracert { get; set; }
    }


    public class requestInfo
    {
        public string Host { get; set; }
        public string RemoteIp { get; set; }
        public string LocalIp { get; set; }
        public int? Port { get; set; }
        public string Funcion { get; set; }
    }


    #endregion


    #region "Sesion"

    public class sesionInicio
    {
        public string NIC { get; set; } //NIC
        public string Clave { get; set; } //Pass
        public string ClaveSesion { get; set; }
        public int ID_Ses { get; set; }
    }
    public class sesionIniciada
    {
        public string Nom { get; set; }
        public string Emp { get; set; }
        public string RefNeg { get; set; }
        public bool EsGlo { get; set; }
        public Int32 ID_MenuSel { get; set; }
        public Int32 ID_Idi { get; set; }

        public contenedorMenuLateral MenuGaolos { get; set; }
        public contenedormenusUsuario MenuUsuario { get; set; }
        public contenedormenuEmpresas MenuEmpresas { get; set; }
        public contenedormenuusuarioOpciones MenuUsuarioOpciones { get; set; }

    }

    public class sesionExtranet
    {
        public string Nom { get; set; }
        public string Emp { get; set; }
        public string RefNeg { get; set; }
        public string NIC { get; set; }
        public string ClaveSesion { get; set; }
        public int ID_Ses { get; set; }
        public int ID_Idi { get; set; }
        public bool EsGlo { get; set; }

        //public strDato[] Negs { get; set; }
        //public contenedorMenuLateral MenuGaolos { get; set; }
        //public contenedormenusUsuario MenuUsuario { get; set; }
        //public contenedormenuEmpresas MenuEmpresas { get; set; }
        //public contenedormenuusuarioOpciones MenuUsuarioOpciones { get; set; }
    }


    public class transportin
    {
        public param parameters { get; set; }
        public object obj { get; set; }
    }

    public class param
    {
        public string NIC { get; set; }
        public string RefNeg { get; set; }
        public string ClaveSesion { get; set; }
        public int ID_Ses { get; set; }
        public int ID_Idi { get; set; }
        public string IP { get; set; }
        public string Dominio { get; set; }
        public string[] ParamsKeys { get; set; }
        public string[] ParamsValues { get; set; }
        public bool ParamsIsPost { get; set; }
        public requestInfoTracert Tracert { get; set; }
        public string Path { get; set; }
        public string PathParams { get; set; }
        public bool Test { get; set; }
    }

    #endregion


    #region "Idiomas"

    public class Idioma
    {
        public Int32 id { get; set; }
        public string Alias { get; set; }
        public string Mensaje { get; set; }
    }

    // Por revisar

    public class idiomaGrupo
    {
        public List<idiomaGrupoDetalle> _detalle { get; set; }
        public int num { get; set; }
    }

    public class idiomaGrupoDetalle
    {
        public string id { get; set; }
        public string Frase { get; set; }
    }






    #endregion

    #region "Menu empresas"

    public class contenedormenuEmpresas
    {
        public List<menuEmpresa> _menus { get; set; }
        public int num { get; set; }
        public strerror Err { get; set; }
    }

    public class menuEmpresa
    {
        public string RefNeg { get; set; }
        public string Neg { get; set; }
        public bool EsGlo { get; set; }
    }

    #endregion

    #region "Menu usuario opciones extranet"

    public class contenedormenuusuarioOpciones
    {
        public List<menuusuarioOpciones> _menus { get; set; }
        public int num { get; set; }
        public strerror Err { get; set; }
    }

    public class menuusuarioOpciones
    {
        public string Opcion { get; set; }
        public string link { get; set; }
    }

    #endregion


    #region "Menu usuario"

    public class contenedormenusUsuario
    {
        public List<menusUsuario> _menus { get; set; }
        public int num { get; set; }
        public strerror Err { get; set; }
    }

    public class menusUsuario
    {
        public Int64 id { get; set; }
        public string titulo { get; set; }
        public string expo { get; set; }
    }

    #endregion


    #region "Menu lateral"

    public class menusCounter
    {
        public string id { get; set; }
        public string val { get; set; }
        public string clas { get; set; }
    }

    public class contenedorMenuLateral
    {
        public string Deco { get; set; }
        public Int32 ID_MenuSel { get; set; }
        public string MenuSel { get; set; }
        public string MenuSelExpo { get; set; }
        public List<menuLateral> _menus { get; set; }
        public int num { get; set; }

        public List<menuLateral> _menusg { get; set; } // Menus gaolos
        public int numg { get; set; }

        public strerror Err { get; set; }
    }

    public class menuLateral
    {
        public Int64 id { get; set; }
        public string titulo { get; set; }
        public string url { get; set; }
        public string ico { get; set; }
        public List<menuLateralDetalle> _menudetalle { get; set; }
        public int num { get; set; }
        public int orden { get; set; }
        public bool col { get; set; }
    }

    public class menuLateralDetalle
    {
        public Int64 id { get; set; }
        public string subTitulo { get; set; }
        public string contador { get; set; }
        public string action { get; set; }
        public bool ocultar { get; set; }
    }

    #endregion


    #region "Loggin - Loggout"

    public class loginStatus
    {
        public string NIC { get; set; }
        public string Token { get; set; }
        public loginStatusServer srv { get; set; }
    }

    public class loginStatusServer
    {
        public string RefNeg { get; set; }
        public string ClaveSesion { get; set; }
        public int ID_Ses { get; set; }
        public int ID_Idi { get; set; }
        public bool EsGlobal { get; set; }
    }

    public class strSolicitarSesion
    {
        public string Dom { get; set; }
        public string Host { get; set; }
        public int? Port { get; set; }
        public string RemoteIP { get; set; }
        public string LocalIP { get; set; }
        public string Language { get; set; }
        public string UserAgent { get; set; }
        public string Referer { get; set; }
        public string sessionID { get; set; }
        public string Cookie_Fe_Al { get; set; }
        public Int32 Cookie_ID_Idi { get; set; }
        public Int32 Cookie_ID_SesOld { get; set; }
        public string iniUrl { get; set; }
        public string Ref { get; set; }
        public string RefParams { get; set; }
    }

    #endregion


    #region "Errores"

    public class strerror
    {
        public string mensaje { get; set; }
        public string titulo { get; set; }
        public bool eserror { get; set; }
        public bool salir { get; set; }
    }

    #endregion

    #region "Logs"

    public class logPartial
    {
        public logInterno logInfo { get; set; }
        public string nombreFichero { get; set; }
    }

    public class logInterno
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Lib { get; set; }
        public string Origen { get; set; }
        public Exception ex { get; set; }
        public string strSql { get; set; }

    }

    #endregion

    #region "Nodos"

    public class Nodo
    {
        public Int32 ID_Nodo { get; set; }
        public string Titulo { get; set; }
        public string Expo { get; set; }
        public Int32 ID_NodoPadre { get; set; }
        public bool Otros { get; set; }
        public Nodo[] nodos { get; set; }
    }

    #endregion

    #region "datos generales"

    public class strListadoConPaginador
    {
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public string Paginador { get; set; }
        public object[] det { get; set; }
        public object Filtros { get; set; }
        public strerror Err { get; set; }
    }

    public class strListaSErr
    {
        public strListaS[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strListaErr
    {
        public strLista[] det { get; set; }
        public strerror Err { get; set; }
    }

    public class strLista2Err
    {
        public strLista[] det { get; set; }
        public object obj { get; set; }
        public strerror Err { get; set; }
    }

    public class strListaDatoSErr
    {
        public strDatoS[] det { get; set; }
        public object obj { get; set; }
        public strerror Err { get; set; }
    }

    public class strLista
    {
        public Int32 id { get; set; }
        public string det { get; set; }
    }

    public class strListaS
    {
        public string id { get; set; }
        public string det { get; set; }
    }

    public class strListado
    {
        public string Titulo { get; set; }
        public strDatoS[] dat { get; set; }
        public Int32 num { get; set; }
        public Int32 numt { get; set; }
        public String Total { get; set; }
        public string Paginador { get; set; }
        public strerror Err { get; set; }
    }

    #endregion


    #region "Core Web"

    #region "CMS"

    public class strWeb_WebNodoHtml
    {
        public string Html { get; set; }
        public string Titulo { get; set; }
        public string Palabra { get; set; }
        public string Meta { get; set; }
        public string CustomCss { get; set; }
        public string CustomHeadJs { get; set; }
        public string CustomFooter { get; set; }
        public string CustomFooterJs { get; set; }
        public string CustomTopHeader { get; set; }
        public string CustomHeader { get; set; }

        public string Url { get; set; }
        public Int32 ID_Tv { get; set; }
        public Int32 ID_Nodo { get; set; }

        public string Ref { get; set; }
        public string RefParams { get; set; }

        public string UrlImg { get; set; }
        public string AltImg { get; set; }
        public string LeyeImg { get; set; }
        public string Usu { get; set; }
        public string Fecha { get; set; }

        public Int32 Status { get; set; }
        public string TipoMime { get; set; }

        public Int32 ID_Idi { get; set; }
        public strerror Err { get; set; }
    }


    #endregion

    #endregion


}


