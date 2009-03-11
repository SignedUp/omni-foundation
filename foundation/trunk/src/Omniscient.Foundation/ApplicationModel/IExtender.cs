namespace Omniscient.Foundation.ApplicationModel
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
