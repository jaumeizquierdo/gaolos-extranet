using Models;

namespace ModuloSolicitudesLibrary
{
    public class ModuloSolicitudesAbiertasListadoViewModel
    {
        public strModuloSolicitudesListado Model { get; }
        public ModuloSolicitudesAbiertasListadoViewModel(strModuloSolicitudesListado model)
        {
            Model = model;
        }

    }

    public class ModuloSolicitudesNuevaSolicitudIniViewModel
    {
        public strSolicitudesNuevaSolicitudIni Model { get; }
        public ModuloSolicitudesNuevaSolicitudIniViewModel(strSolicitudesNuevaSolicitudIni model)
        {
            Model = model;
        }

    }

    public class ModuloSolicitudesDashBoardInicioViewModel
    {
        public strSolicitudesDashBoardInicio Model { get; }
        public ModuloSolicitudesDashBoardInicioViewModel(strSolicitudesDashBoardInicio model)
        {
            Model = model;
        }

    }

}
