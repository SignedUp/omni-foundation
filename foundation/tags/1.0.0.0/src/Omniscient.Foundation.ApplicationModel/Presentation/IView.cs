﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents a view.
    /// 
    /// Views are responsible for displaying <see cref="IModel"/> objects to the UI.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets or sets the Model.
        /// </summary>
        IModel Model { get; set; }
        
        /// <summary>
        /// Called when the Model is modified from the outside of the View (probably in another view).
        /// </summary>
        void UpdateView();
    }
}
