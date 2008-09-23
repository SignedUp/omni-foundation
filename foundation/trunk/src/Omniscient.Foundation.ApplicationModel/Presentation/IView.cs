﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IView
    {
        IModel Model { get; set; }
        void UpdateView();
    }
}