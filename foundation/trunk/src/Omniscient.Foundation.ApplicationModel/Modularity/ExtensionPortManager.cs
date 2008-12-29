namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    ///<summary>
    ///</summary>
    public class ExtensionPortManager: IExtensionPortManager
    {
        private IObjectContainer _container;

        ///<summary>
        ///</summary>
        ///<param name="container"></param>
        public ExtensionPortManager(IObjectContainer container)
        {
            _container = container;
        }

        #region IExtensionPortManager Members

        ///<summary>
        ///</summary>
        ///<param name="port"></param>
        ///<typeparam name="TContract"></typeparam>
        public void RegisterExtensionPort<TContract>(IExtensionPort<TContract> port)
        {
            _container.Register(port);
        }

        ///<summary>
        ///</summary>
        ///<typeparam name="TContract"></typeparam>
        ///<returns></returns>
        public IExtensionPort<TContract> GetExtensionPort<TContract>()
        {
            return _container.Get<IExtensionPort<TContract>>();
        }

        #endregion
    }
}
