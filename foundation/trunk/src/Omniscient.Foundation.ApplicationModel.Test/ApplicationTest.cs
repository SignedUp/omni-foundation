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
            Assert.IsNotNull(Application.Current);
        }

        [Test()]
        public void CheckServiceContainer()
        {
            Assert.IsNull(Application.Current.ServiceContainer);
        }

        [Test()]
        public void CheckPresentationController()
        {
            Assert.IsNull(Application.Current.PresentationController);
        }

        [Test()]
        public void CheckStarted()
        {
            Assert.IsFalse(Application.Current.IsStarted);
            Application.Current.StartApplication();
            Assert.IsTrue(Application.Current.IsStarted);
        }

        [Test()]
        public void CheckStoped()
        {
            Application.Current.StartApplication();
            Application.Current.CloseApplication();
            Assert.IsFalse(Application.Current.IsStarted);
        }

    }
}
