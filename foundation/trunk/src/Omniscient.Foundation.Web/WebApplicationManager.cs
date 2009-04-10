using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ApplicationModel;
using System.Web;

namespace Omniscient.Foundation.Web
{
    public class WebApplicationManager: ApplicationManager
    {
        public static WebApplicationManager Current
        {
            get
            {
                if (HttpContext.Current.Application["WebApplicationManager"] == null)
                    HttpContext.Current.Application["WebApplicationManager"] = new WebApplicationManager();
                return (WebApplicationManager)HttpContext.Current.Application["WebApplicationManager"];
            }
        }
    }
}
