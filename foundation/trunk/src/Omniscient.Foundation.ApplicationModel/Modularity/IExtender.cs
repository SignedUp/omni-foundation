namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    ///<summary>
    ///</summary>
    ///<typeparam name="TContract"></typeparam>
    public interface IExtender<TContract>
    {
        ///<summary>
        ///</summary>
        ///<returns></returns>
        TContract GetImplementation();
    }
}
