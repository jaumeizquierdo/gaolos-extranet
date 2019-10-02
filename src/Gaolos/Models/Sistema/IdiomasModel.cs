using Models;

namespace IdiomasLibrary
{
    public class IdiomasViewModel
    {
        public Idioma[] Idiomas { get; }
        public IdiomasViewModel(Idioma[] idiomas)
        {
            Idiomas = idiomas;
        }

    }
}

