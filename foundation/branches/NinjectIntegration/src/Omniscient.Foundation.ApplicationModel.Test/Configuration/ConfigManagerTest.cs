using NUnit.Framework;
using Omniscient.Foundation.ApplicationModel.Modularity;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    [TestFixture]
    public class ConfigManagerTest
    {
        [Test]
        public void ConfigureModulesWith2Modules()
        {
            ApplicationConfiguration config = new ApplicationConfiguration();
            config.ModulesConfiguration.Modules.Add(new ModuleDefinition { Type = GetTypeFullname("Module1") });
            config.ModulesConfiguration.Modules.Add(new ModuleDefinition { Type = GetTypeFullname("Module2") });

            ObjectContainer container = new ObjectContainer();
            ConfigManager.LoadModules(container, config);

            Assert.AreEqual(2, container.AllObjects.Length);
        }

        [Test]
        public void ConfigureContainer()
        {
            ApplicationConfiguration config = new ApplicationConfiguration();
            config.ContainerConfiguration.Items.Add(new ObjectContainerAdd { KeyType = GetTypeFullname("IA"), ObjectType = GetTypeFullname("A") });
            config.ContainerConfiguration.Items.Add(new ObjectContainerAdd { KeyType = GetTypeFullname("IB"), ObjectType = GetTypeFullname("B") });

            ObjectContainer container = new ObjectContainer();
            ConfigManager.ConfigureContainer(container, config);

            Assert.AreEqual(2, container.AllObjects.Length);
            IA a = container.Get<IA>();
            Assert.IsNotNull(a);
            Assert.AreSame(typeof(A), a.GetType());
        }

        private string GetTypeFullname(string typeName)
        {
            return string.Format("Omniscient.Foundation.ApplicationModel.Configuration.ConfigManagerTest+{0}, Omniscient.Foundation.ApplicationModel.Test", typeName);
        }

        public interface IA { }
        public class A : IA { }
        public interface IB { }
        public class B : IB { }

        public class Module1 : IApplicationModule
        {
            #region IApplicationModule Members

            public string Name
            {
                get { throw new System.NotImplementedException(); }
            }

            public bool IsLoaded
            {
                get { throw new System.NotImplementedException(); }
            }

            public bool IsActivated
            {
                get { throw new System.NotImplementedException(); }
            }

            public void Load()
            {
                throw new System.NotImplementedException();
            }

            public void Activate(Omniscient.Foundation.ApplicationModel.Presentation.IPresentationController presentation)
            {
                throw new System.NotImplementedException();
            }

            public void Deactivate()
            {
                throw new System.NotImplementedException();
            }

            public void Unload()
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }

        public class Module2 : IApplicationModule
        {
            public Presentation.IPresentationController PresentationController { get; set; }

            #region IApplicationModule Members

            public string Name
            {
                get { throw new System.NotImplementedException(); }
            }

            public bool IsLoaded
            {
                get { throw new System.NotImplementedException(); }
            }

            public bool IsActivated
            {
                get { throw new System.NotImplementedException(); }
            }

            public void Load()
            {
                throw new System.NotImplementedException();
            }

            public void Activate(Omniscient.Foundation.ApplicationModel.Presentation.IPresentationController presentation)
            {
                throw new System.NotImplementedException();
            }

            public void Deactivate()
            {
                throw new System.NotImplementedException();
            }

            public void Unload()
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }
    }       
}
