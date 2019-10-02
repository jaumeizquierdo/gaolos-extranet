using Models;

namespace WebsLibrary
{
    public class WebsDominiosListadoViewModel
    {
        public strWebsDominiosListado Model { get; }
        public WebsDominiosListadoViewModel(strWebsDominiosListado model)
        {
            Model = model;
        }

    }

    public class WebsWebsListadoViewModel
    {
        public strWebsWebsListado Model { get; }
        public WebsWebsListadoViewModel(strWebsWebsListado model)
        {
            Model = model;
        }

    }

    public class WebsReferidosViewModel
    {
        public strWebsReferidosListado Model { get; }
        public WebsReferidosViewModel(strWebsReferidosListado model)
        {
            Model = model;
        }

    }

}
