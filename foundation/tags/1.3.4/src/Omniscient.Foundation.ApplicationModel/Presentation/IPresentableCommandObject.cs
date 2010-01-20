using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Commanding;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    public interface IPresentableCommandObject: IPresentableObject
    {
        ICommandCore Command { get; set; }
    }
}
