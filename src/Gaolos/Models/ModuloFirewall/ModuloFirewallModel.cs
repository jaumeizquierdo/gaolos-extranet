using Models;

namespace ModuloFirewallLibrary
{
    public class ModuloFirewallIPsListadoViewModel
    {
        public strFireWallIPsListado Model { get; }
        public ModuloFirewallIPsListadoViewModel(strFireWallIPsListado model)
        {
            Model = model;
        }

    }

    public class ModuloFirewallAppListadoViewModel
    {
        public strFireWallAppListado Model { get; }
        public ModuloFirewallAppListadoViewModel(strFireWallAppListado model)
        {
            Model = model;
        }

    }

    public class ModuloFirewallRulesListadoViewModel
    {
        public strFireWallRulesListado Model { get; }
        public ModuloFirewallRulesListadoViewModel(strFireWallRulesListado model)
        {
            Model = model;
        }

    }

    public class ModuloFirewallGruposListadoViewModel
    {
        public strFireWallGruposListado Model { get; }
        public ModuloFirewallGruposListadoViewModel(strFireWallGruposListado model)
        {
            Model = model;
        }

    }


}


