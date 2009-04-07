using Omniscient.Foundation.ApplicationModel.Presentation;
namespace Omniscient.Foundation.Contrib.Silverlight
{
    public class ViewContent
    {
        public ViewContent(IView view)
        {
            View = view;
        }

        public IView View
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return View.Title;
        }
    }
}
