namespace RoastedMarketplace.Core.Modules
{
    public abstract class FoundationSystemModule : FoundationModule
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