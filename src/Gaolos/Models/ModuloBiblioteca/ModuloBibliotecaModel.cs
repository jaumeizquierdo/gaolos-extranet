using Models;

namespace ModuloBibliotecaLibrary
{
    public class ModuloBibliotecaDocumentosListadoViewModel
    {
        public strModuloBibliotecaDocumentosListado Model { get; }
        public ModuloBibliotecaDocumentosListadoViewModel(strModuloBibliotecaDocumentosListado model)
        {
            Model = model;
        }

    }

    public class ModuloBibliotecaDocumentosDocumentoViewModel
    {
        public strModuloBibliotecaDocumentoAmpliado Model { get; }
        public ModuloBibliotecaDocumentosDocumentoViewModel(strModuloBibliotecaDocumentoAmpliado model)
        {
            Model = model;
        }

    }

}
