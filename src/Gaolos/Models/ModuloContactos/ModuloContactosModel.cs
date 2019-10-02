using Models;

namespace ModuloContactosLibrary
{
    public class ModuloContactosContactosListadoViewModel
    {
        public strContactosContactosListado Model { get; }
        public ModuloContactosContactosListadoViewModel(strContactosContactosListado model)
        {
            Model = model;
        }

    }

    public class ModuloContactosContactoGeneralViewModel
    {
        public strContactosContactos_General Model { get; }
        public ModuloContactosContactoGeneralViewModel(strContactosContactos_General model)
        {
            Model = model;
        }

    }

}