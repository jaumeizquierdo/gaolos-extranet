using Models;

namespace ModuloElementosLibrary
{
    public class ElementosCaracteristicasListadoViewModel
    {
        public strListado Model { get; }
        public ElementosCaracteristicasListadoViewModel(strListado model)
        {
            Model = model;
        }

    }

    public class ElementosPlantillasListadoViewModel
    {
        public strListado Model { get; }
        public ElementosPlantillasListadoViewModel(strListado model)
        {
            Model = model;
        }

    }

    public class ElementosTipoDetallesViewModel
    {
        public strElementosTipo Model { get; }
        public ElementosTipoDetallesViewModel(strElementosTipo model)
        {
            Model = model;
        }

    }

}
