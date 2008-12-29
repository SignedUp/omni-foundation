namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    ///<summary>
    ///</summary>
    public interface IExtensionPortManager
    {
        ///<summary>
        ///</summary>
        ///<param name="port"></param>
        ///<typeparam name="TContract"></typeparam>
        void RegisterExtensionPort<TContract>(IExtensionPort<TContract> port);
        ///<summary>
        ///</summary>
        ///<typeparam name="TContract"></typeparam>
        ///<returns></returns>
        IExtensionPort<TContract> GetExtensionPort<TContract>();
    }
}
