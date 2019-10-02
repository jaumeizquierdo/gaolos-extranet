using Models;

namespace MenuEmpresasLibrary
{
    public class MenuEmpresasViewModel
    {
        public contenedormenuEmpresas Menu { get; }
        public MenuEmpresasViewModel(contenedormenuEmpresas menu)
        {
            Menu = menu;
        }

    }
}
