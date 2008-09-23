using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    interface IDashBoardAnimation
    {
        /// <summary>
        /// Updates the panel layout without animation.  This does size and position without animation
        /// </summary>
        void UpdatePanelLayout();

        /// <summary>
        /// Animates the panel sizes
        /// </summary>
        void AnimatePanelSizes();

        /// <summary>
        /// Animate the panel positions
        /// </summary>
        void AnimatePanelLayout();
        
    }
}
