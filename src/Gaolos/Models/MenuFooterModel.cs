using Models;

namespace MenuFooterLibrary
{
    public class MenuFooterViewModel
    {
        public contenedorMenuLateral Menu { get; }
        public MenuFooterViewModel(contenedorMenuLateral menu)
        {
            Menu = menu;
        }

    }
}
