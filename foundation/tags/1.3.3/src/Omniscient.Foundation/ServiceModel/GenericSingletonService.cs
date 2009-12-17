namespace Omniscient.Foundation.ServiceModel
{
    ///<summary>
    ///</summary>
    ///<typeparam name="TContract"></typeparam>
    ///<typeparam name="TImplementation"></typeparam>
    public class GenericSingletonService<TContract, TImplementation>: ServiceBase<TContract>
        where TImplementation: TContract, new()
    {
        private TImplementation _singleton;

        ///<summary>
        ///</summary>
        public GenericSingletonService()
        {
            _singleton = new TImplementation();
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public override TContract GetImplementation()
        {
            return _singleton;
        }
    }
}
