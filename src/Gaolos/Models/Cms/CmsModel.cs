using Models;

namespace CmsLibrary
{
    public class CmsWebsListadoViewModel
    {
        public strCmsWebListado CmsWebLis { get; }
        public CmsWebsListadoViewModel(strCmsWebListado cmsweblis)
        {
            CmsWebLis = cmsweblis;
        }

    }

    public class CmsWebNodosEditarViewModel
    {
        public strCmsWeb CmsWeb { get; }
        public CmsWebNodosEditarViewModel(strCmsWeb cmsweb)
        {
            CmsWeb = cmsweb;
        }

    }

    public class CmsWebNodoEditarHtmlViewModel
    {
        public strWebNodoEdtiarHtml Model { get; }
        public CmsWebNodoEditarHtmlViewModel(strWebNodoEdtiarHtml model)
        {
            Model = model;
        }

    }

    public class CmsTagsWebsListadoViewModel
    {
        public strTagsListado Model { get; }
        public CmsTagsWebsListadoViewModel(strTagsListado model)
        {
            Model = model;
        }

    }

    #region "CMS - Componentes"

    public class CmsWebComponentesListadoViewModel
    {
        public strCmsWebComponentesListado Model { get; }
        public CmsWebComponentesListadoViewModel(strCmsWebComponentesListado model)
        {
            Model = model;
        }

    }

    public class CmsWebComponentesComponenteHtmlViewModel
    {
        public strDatoS Model { get; }
        public CmsWebComponentesComponenteHtmlViewModel(strDatoS model)
        {
            Model = model;
        }

    }


    #endregion



}

