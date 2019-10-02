using Models;

namespace ModuloServiciosLibrary
{
    public class ModuloServiciosServiciosListadoViewModel
    {
        public strServiciosServiciosListado Model { get; }
        public ModuloServiciosServiciosListadoViewModel(strServiciosServiciosListado model)
        {
            Model = model;
        }

    }

    public class ModuloServiciosServicioNuevoViewModel
    {
        public strListaErr Model { get; }
        public ModuloServiciosServicioNuevoViewModel(strListaErr model)
        {
            Model = model;
        }

    }

    public class ModuloServiciosCategoriasServiciosListadoViewModel
    {
        public strListadoConPaginador Model { get; }
        public ModuloServiciosCategoriasServiciosListadoViewModel(strListadoConPaginador model)
        {
            Model = model;
        }

    }

    public class ModuloServiciosServicioDetallesViewModel
    {
        public strServiciosServicioDetalles Model { get; }
        public ModuloServiciosServicioDetallesViewModel(strServiciosServicioDetalles model)
        {
            Model = model;
        }

    }

    public class ModuloServiciosListadoConPaginadorViewModel
    {
        public strListadoConPaginador Model { get; }
        public ModuloServiciosListadoConPaginadorViewModel(strListadoConPaginador model)
        {
            Model = model;
        }

    }

}
