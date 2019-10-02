using Models;

namespace MiEmpresaLibrary
{
    public class MiEmpresaMiEmpresa_GeneralViewModel
    {
        public strMiEmpresaMiEmpresa_General Model { get; }
        public MiEmpresaMiEmpresa_GeneralViewModel(strMiEmpresaMiEmpresa_General model)
        {
            Model = model;
        }
    }
    public class ModulosMiEmpresaFiscalViewModel
    {
        public strMiEmpresaMiEmpresa_Fiscal Model { get; }
        public ModulosMiEmpresaFiscalViewModel(strMiEmpresaMiEmpresa_Fiscal model)
        {
            Model = model;
        }

    }

    public class MiEmpresaGruposUsuariosListadoViewModel
    {
        public strMiEmpresaGruposUsuariosListado Model { get; }
        public MiEmpresaGruposUsuariosListadoViewModel(strMiEmpresaGruposUsuariosListado model)
        {
            Model = model;
        }
    }

    public class MiEmpresaContratacionViewModel
    {
        public strMiEmpresaContratacion Datos { get; }
        public MiEmpresaContratacionViewModel(strMiEmpresaContratacion datos)
        {
            Datos = datos;
        }
    }

    public class MiEmpresaUsuariosViewModel
    {
        public strMiEmpresaUsuarios Datos { get; }
        public MiEmpresaUsuariosViewModel(strMiEmpresaUsuarios datos)
        {
            Datos = datos;
        }
    }

    public class MiEmpresaUsuariosUsuarioInfoViewModel
    {
        public strMiEmpresaUsuarioInfo Datos { get; }
        public MiEmpresaUsuariosUsuarioInfoViewModel(strMiEmpresaUsuarioInfo datos)
        {
            Datos = datos;
        }
    }

    public class MiEmpresaConfiguracionHtmlsViewModel
    {
        public strMiEmpresaConfiguracionHtmls Model { get; }
        public MiEmpresaConfiguracionHtmlsViewModel(strMiEmpresaConfiguracionHtmls model)
        {
            Model = model;
        }
    }

    //public class NodosNegocioViewModel
    //{
    //    public Nodo nodo { get; }
    //    public NodosNegocioViewModel(Nodo _nodo)
    //    {
    //        nodo = _nodo;
    //    }
    //}

}
