using Models;

namespace MenuNotificacionesLibrary
{
    public class MenuNotificacionesViewModels
    {
        public contenedormenusUsuario Menu { get; }
        public MenuNotificacionesViewModels(contenedormenusUsuario menu)
        {
            Menu = menu;
        }
    }
}
