using Models;

namespace ModuloProduccionLibrary
{
    #region "Ordenes"

    

    public class ProduccionOrdenesConIncidenciasListadoViewModel
    {
        public strOrdProdConIncidenciasListado Model { get; }
        public ProduccionOrdenesConIncidenciasListadoViewModel(strOrdProdConIncidenciasListado model)
        {
            Model = model;
        }

    }

    public class ProduccionOrdenesEnProduccionListadoViewModel
    {
        public strOrdProdEnProdListado Model { get; }
        public ProduccionOrdenesEnProduccionListadoViewModel(strOrdProdEnProdListado model)
        {
            Model = model;
        }

    }

    public class ProduccionOrdenesListadoViewModel
    {
        public strOrdProdListado Model { get; }
        public ProduccionOrdenesListadoViewModel(strOrdProdListado model)
        {
            Model = model;
        }

    }

    public class ProduccionOrdenesOrdenDetallesViewModel
    {
        public strOrdenProduccion Model { get; }
        public ProduccionOrdenesOrdenDetallesViewModel(strOrdenProduccion model)
        {
            Model = model;
        }

    }

    public class ProduccionOrdenesEstadoOperariosDetallesViewModel
    {
        public strOrdProdEstadoOperariosListado Model { get; }
        public ProduccionOrdenesEstadoOperariosDetallesViewModel(strOrdProdEstadoOperariosListado model)
        {
            Model = model;
        }

    }

    #endregion

    #region "Pedidos"

    public class ProduccionPedidosListadoViewModel
    {
        public strPedProdListado Model { get; }
        public ProduccionPedidosListadoViewModel(strPedProdListado model)
        {
            Model = model;
        }

    }

    #endregion

    #region "Consultas"

    public class ProduccionCargaDeTrabajoListadoViewModel
    {
        public strProduccionCargaDeTrabajoListado Model { get; }
        public ProduccionCargaDeTrabajoListadoViewModel(strProduccionCargaDeTrabajoListado model)
        {
            Model = model;
        }

    }

    public class ProduccionTiemposDeProduccionListadoViewModel
    {
        public strProduccionTiemposDeProduccionListado Model { get; }
        public ProduccionTiemposDeProduccionListadoViewModel(strProduccionTiemposDeProduccionListado model)
        {
            Model = model;
        }

    }

    #endregion
}

