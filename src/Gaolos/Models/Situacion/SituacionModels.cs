using Models;

namespace SituacionLibrary
{
    public class SituacionUsuariosViewModel
    {
        public strSituacionUsuarios _Model { get; }
        public SituacionUsuariosViewModel(strSituacionUsuarios _model)
        {
            _Model = _model;
        }

    }

    public class SituacionClientesViewModel
    {
        public strSituacionClientes _Model { get; }
        public SituacionClientesViewModel(strSituacionClientes _model)
        {
            _Model = _model;
        }

    }

    public class SituacionEmpresaViewModel
    {
        public strSituacionEmpresa Model { get; }
        public SituacionEmpresaViewModel(strSituacionEmpresa model)
        {
            Model = model;
        }

    }

    
}
