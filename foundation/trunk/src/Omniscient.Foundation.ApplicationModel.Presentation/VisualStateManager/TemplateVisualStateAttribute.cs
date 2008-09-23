using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows
{
    /// <summary>
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class TemplateVisualStateAttribute : Attribute
    {

        // Properties
        public string GroupName
        {

            get;
            set;

        }

        public string Name
        {
            get;
            set;
        }
    }



}
