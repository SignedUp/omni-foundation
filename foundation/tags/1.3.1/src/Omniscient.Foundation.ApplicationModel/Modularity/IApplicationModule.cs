using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    ///<summary>
    /// An application module is 
    ///</summary>
    public interface IApplicationModule
    {
        string Name { get; }
        bool IsLoaded { get; }
        bool IsActivated { get; }

        void Load();
        void Activate(IPresentationController presentation);
        void Deactivate();
        void Unload();
    }
}
