using Omniscient.Foundation.ApplicationModel;
using System.Web;
using System;

namespace Omniscient.Foundation.Web
{
    public class WebApplicationContext: IApplicationContext
    {
        public WebApplicationContext()
        {
            if (HttpContext.Current.Application["WebApplicationManager"] == null)
                HttpContext.Current.Application["WebApplicationManager"] = new WebApplicationManager();
        }

        public ApplicationManager Current
        {
            get { return (ApplicationManager)HttpContext.Current.Application["WebApplicationManager"]; }
        }
    }
}
