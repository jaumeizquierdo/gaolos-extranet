using Models;

namespace AsistenciasLibrary
{
    public class AsistenciasRutasPlanificarViewModel
    {
        public strAsistenciasRutaAsignarValoresInicio _Model { get; }
        public AsistenciasRutasPlanificarViewModel(strAsistenciasRutaAsignarValoresInicio _model)
        {
            _Model = _model;
        }

    }

    public class AsistenciasRutasConfirmarViewModel
    {
        public strAsistenciasRutaConfirmarValoresInicio _Model { get; }
        public AsistenciasRutasConfirmarViewModel(strAsistenciasRutaConfirmarValoresInicio _model)
        {
            _Model = _model;
        }

    }

}
