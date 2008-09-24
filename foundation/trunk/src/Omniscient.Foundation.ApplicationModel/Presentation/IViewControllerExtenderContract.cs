﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IViewControllerExtenderContract
    {
        IView GetView(IModel model);
    }
}