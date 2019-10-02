using Models;

namespace BlogsLibrary
{
    public class BlogsListadoViewModel
    {
        public strBlogsListado Model { get; }
        public BlogsListadoViewModel(strBlogsListado model)
        {
            Model = model;
        }

    }

    public class BlogsEntradasBlogListadoViewModel
    {
        public strBlogsEntradasListado Model { get; }
        public BlogsEntradasBlogListadoViewModel(strBlogsEntradasListado model)
        {
            Model = model;
        }

    }


    public class BlogsEntradaDetalles
    {
        public strBlogsEntradaDetalles Model { get; }
        public BlogsEntradaDetalles(strBlogsEntradaDetalles model)
        {
            Model = model;
        }

    }


    public class BlogsEstilos
    {
        public strBlogsEstilos Model { get; }
        public BlogsEstilos(strBlogsEstilos model)
        {
            Model = model;
        }

    }
}
