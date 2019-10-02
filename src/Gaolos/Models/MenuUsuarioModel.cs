using Models;

namespace MenuUsuarioLibrary
{
    public class MenuUsuarioViewModel
    {
        public contenedormenusUsuario Menu { get; }
        public MenuUsuarioViewModel(contenedormenusUsuario menu)
        {
            Menu = menu;
        }
    }
}
