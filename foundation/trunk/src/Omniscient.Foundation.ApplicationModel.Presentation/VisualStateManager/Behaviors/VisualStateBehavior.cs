using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Controls
{
    /// <summary>
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
    public abstract class VisualStateBehavior
    {
        public abstract Type TargetType { get; }
        public abstract void Attach(Control control);
    }
}
