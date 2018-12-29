namespace RoastedMarketplace.Core.Plugins
{
    public abstract class FoundationSystemPlugin : FoundationPlugin
    {
        public override bool IsSystemModule
        {
            get { return true; }
        }

        public sealed override void Uninstall()
        {
            //can't uninstall system module
        }
    }
}