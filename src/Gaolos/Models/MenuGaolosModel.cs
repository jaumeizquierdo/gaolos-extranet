using Models;

namespace MenuGaolosLibrary
{
    public class MenuGaolosViewModel
    {
        public contenedorMenuLateral Menu { get; }
        public MenuGaolosViewModel(contenedorMenuLateral menu)
        {
            Menu = menu;
        }

    }

}
