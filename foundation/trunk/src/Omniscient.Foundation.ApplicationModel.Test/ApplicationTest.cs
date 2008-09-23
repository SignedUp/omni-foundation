using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omniscient.Foundation.ApplicationModel
{
    [TestFixture()]
    public class ApplicationTest
    {
        [Test()]
        public void CheckCurrentApplication()
        {
            Assert.IsNotNull(ApplicationManager.Current);
        }

        [Test()]
        public void CheckServiceContainer()
        {
            Assert.IsNull(ApplicationManager.Current.ServiceContainer);
        }

        [Test()]
        public void CheckPresentationController()
        {
            Assert.IsNull(ApplicationManager.Current.PresentationController);
        }

        [Test()]
        public void CheckStarted()
        {
            Assert.IsFalse(ApplicationManager.Current.IsStarted);
            ApplicationManager.Current.StartApplication();
            Assert.IsTrue(ApplicationManager.Current.IsStarted);
        }

        [Test()]
        public void CheckStoped()
        {
            ApplicationManager.Current.StartApplication();
            ApplicationManager.Current.CloseApplication();
            Assert.IsFalse(ApplicationManager.Current.IsStarted);
        }

    }
}
