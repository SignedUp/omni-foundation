using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Represents an object that has a Text property to be displayed on the UI.
    /// </summary>
    public interface IPresentableObject
    {
        /// <summary>
        /// Gets or sets the text displayed on the screen for that object.
        /// </summary>
        string Text { get; set; }
    }
}
