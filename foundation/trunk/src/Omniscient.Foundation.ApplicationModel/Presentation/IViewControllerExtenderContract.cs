namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    ///<summary>
    ///</summary>
    public interface IViewControllerExtenderContract
    {
        ///<summary>
        ///</summary>
        ///<param name="model"></param>
        ///<returns></returns>
        IView GetView(IModel model);
    }
}
