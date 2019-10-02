using Models;

namespace MyGaolosLibrary
{
    public class MyGaolosInicioExtranetViewModel
    {
        public strModuloInicio Model { get; }
        public MyGaolosInicioExtranetViewModel(strModuloInicio model)
        {
            Model = model;
        }

    }

    public class MyGaolosTareasPendienesListadoViewModel
    {
        public strModuloUsuarioTareasPendientesListado Model { get; }
        public MyGaolosTareasPendienesListadoViewModel(strModuloUsuarioTareasPendientesListado model)
        {
            Model = model;
        }

    }

    public class MyGaolosTareasPendienesTareaPendienteViewModel
    {
        public strModuloUsuarioTareaPendiente Model { get; }
        public MyGaolosTareasPendienesTareaPendienteViewModel(strModuloUsuarioTareaPendiente model)
        {
            Model = model;
        }

    }

    public class MyGaolosCalendarioInicioViewModel
    {
        public strDatoS Model { get; }
        public MyGaolosCalendarioInicioViewModel(strDatoS model)
        {
            Model = model;
        }

    }

    public class MyGaolosMiPerfilViewModel
    {
        public strMiPerfil Model { get; }
        public MyGaolosMiPerfilViewModel(strMiPerfil model)
        {
            Model = model;
        }

    }

}
