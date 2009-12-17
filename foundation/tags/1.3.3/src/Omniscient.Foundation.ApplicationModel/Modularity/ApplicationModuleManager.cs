using System;
using System.Collections.Generic;
using Ninject.Core.Modules;
using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public class ApplicationModuleManager: IApplicationModuleManager
    {
        private List<IApplicationModule> _modules;

        public ApplicationModuleManager()
        {
            _modules = new List<IApplicationModule>();
        }

        /// <summary>
        /// Gets or sets the presentation controller used to activate modules.  Using the controller, modules and place themselves
        /// in the UI loop.
        /// </summary>
        public IPresentationController PresentationController { get; set; }

        /// <summary>
        /// Gets or sets Ninject's module manager.  If provided, then loading and unloading Ninject modules will affect Ninject as well.
        /// </summary>
        public Ninject.Core.Modules.IModuleManager ModuleManager { get; set; }

        /// <summary>
        /// Loads a module.  Loading a module through this interface is necessary to make the module part of the application's 
        /// lifetime management.  If <see cref="ModuleManager"/> is provided and the module implements <see cref="Ninject.Core.IModule"/>, 
        /// then the module will also be loaded in Ninject.
        /// </summary>
        /// <param name="module">The application module to load.</param>
        public void Load(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException("module");
            if (_modules.Exists(m => m.Name == module.Name)) throw new InvalidOperationException("Module with name {0} already loaded.".FormatEx(module.Name));

            _modules.Add(module);

            if (ModuleManager != null && typeof(Ninject.Core.IModule).IsAssignableFrom(module.GetType()))
                ModuleManager.Load((Ninject.Core.IModule)module);
            else
                module.Load(); // don't use Ninject.
        }

        /// <summary>
        /// Returns true if a module with that name is loaded.
        /// </summary>
        /// <param name="name">the name of the module to look fo.</param>
        /// <returns>true if the module is loaded; otherwise, false.</returns>
        public bool IsLoaded(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            return _modules.Exists(m => m.Name == name);
        }

        /// <summary>
        /// Returns a list of all loaded modules.
        /// </summary>
        public IEnumerable<IApplicationModule> All 
        {
            get { return _modules.ToArray(); }
        }

        /// <summary>
        /// Gets the total number of loaded modules.  Decreases when unloading modules.
        /// </summary>
        public int Count 
        {
            get { return _modules.Count; }
        }

        /// <summary>
        /// Activates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to activate a module that is not loaded yet.
        /// </summary>
        /// <param name="name">Name of the module to activate.</param>
        /// <exception cref="InvalidOperationException">Thrown when activating a module that's not loaded.</exception>
        public void Activate(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (!IsLoaded(name)) throw new InvalidOperationException("Module with name {0} is not loaded".FormatEx(name));

            IApplicationModule module = _modules.Find(m => m.Name == name);
            module.Activate(PresentationController);
        }

        /// <summary>
        /// Activates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to activate a module that is not loaded yet.
        /// </summary>
        /// <param name="module">A module that must be loaded.</param>
        /// <exception cref="InvalidOperationException">Thrown when activating a module that's not loaded.</exception>
        public void Activate(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException("module");
            if (!IsLoaded(module.Name)) throw new InvalidOperationException("Module with name {0} is not loaded".FormatEx(module.Name));

            module.Activate(PresentationController);
        }

        /// <summary>
        /// Activates all currently loaded modules.
        /// </summary>
        public void ActivateAll()
        {
            _modules.ForEach(m => m.Activate(PresentationController));
        }

        /// <summary>
        /// Deactivates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to deactivated a module that not loaded and not activated.
        /// </summary>
        /// <param name="name">Name of the module to deactivate.</param>
        /// <exception cref="InvalidOperationException">Thrown when deactivating a module that's not loaded and not activated.</exception>
        public void Deactivate(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (!IsLoaded(name)) throw new InvalidOperationException("Module with name {0} is not loaded".FormatEx(name));

            IApplicationModule module = _modules.Find(m => m.Name == name);
            Deactivate(module);
        }

        /// <summary>
        /// Deactivates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to deactivated a module that not loaded and not activated.
        /// </summary>
        /// <param name="module">A module to deactivate</param>
        /// <exception cref="InvalidOperationException">Thrown when deactivating a module that's not loaded.</exception>
        public void Deactivate(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException("module");
            if (!IsLoaded(module.Name)) throw new InvalidOperationException("Module with name {0} is not loaded".FormatEx(module.Name));

            if (!module.IsActivated) return;
            module.Deactivate();
        }

        /// <summary>
        /// Deactivates all activated modules in reverse order that they have been loaded.
        /// </summary>
        public void DeactivateAll()
        {
            for (int i = _modules.Count - 1; i >= 0; i--) Deactivate(_modules[i]);
        }

        /// <summary>
        /// Unloads a module.  If <see cref="ModuleManager"/> is provided and the module implements <see cref="Ninject.Core.IModule"/>, 
        /// then the module will also be unloaded from Ninject.  It is invalid to unload a module that's not been loaded.
        /// </summary>
        /// <param name="name">Name of a module to unload.</param>
        /// <exception cref="InvalidOperationException">Thrown if the module has not been loaded.</exception>
        public void Unload(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (!IsLoaded(name)) throw new InvalidOperationException("Module with name {0} is not loaded".FormatEx(name));

            IApplicationModule module = _modules.Find(m => m.Name == name);
            
            Unload(module);
        }

        /// <summary>
        /// Unloads a module.  If <see cref="ModuleManager"/> is provided and the module implements <see cref="Ninject.Core.IModule"/>, 
        /// then the module will also be unloaded from Ninject.  It is invalid to unload a module that's not been loaded.
        /// </summary>
        /// <param name="module">A module to unload.</param>
        /// <exception cref="InvalidOperationException">Thrown if the module has not been loaded.</exception>
        public void Unload(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException("module");
            if (!IsLoaded(module.Name)) throw new InvalidOperationException("Module with name {0} is not loaded".FormatEx(module.Name));
            
            if (ModuleManager != null && typeof(Ninject.Core.IModule).IsAssignableFrom(module.GetType()))
                ModuleManager.Unload((Ninject.Core.IModule)module);
            else
                //don't use Ninject.
                module.Unload();

            _modules.Remove(module);
        }

        /// <summary>
        /// Unloads all loaded modules.  Automatically deactivates all modules before unloading them.
        /// </summary>
        public void UnloadAll()
        {
            DeactivateAll();
            while (_modules.Count > 0) Unload(_modules[Count - 1]);
        }
    }
}
