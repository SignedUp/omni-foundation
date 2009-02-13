using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    ///<summary>
    ///</summary>
    public interface IModule
    {
        ///<summary>
        ///</summary>
        IPresentationController PresentationController { get; set; }
    }
}
