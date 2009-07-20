using System;
using Omniscient.Foundation.ApplicationModel.Presentation;
using System.Collections.Generic;
namespace Omniscient.Foundation.ApplicationModel.Modularity
{
    public interface IApplicationModuleManager
    {
        /// <summary>
        /// Gets or sets the presentation controller used to activate modules.  Using the controller, modules and place themselves
        /// in the UI loop.
        /// </summary>
        IPresentationController PresentationController { get; set; }

        /// <summary>
        /// Gets or sets Ninject's module manager.  If provided, then loading and unloading Ninject modules will affect Ninject as well.
        /// </summary>
        Ninject.Core.Modules.IModuleManager ModuleManager { get; set; }

        /// <summary>
        /// Loads a module.  Loading a module through this interface is necessary to make the module part of the application's 
        /// lifetime management.  If <see cref="ModuleManager"/> is provided and the module implements <see cref="Ninject.Core.IModule"/>, 
        /// then the module will also be loaded in Ninject.
        /// </summary>
        /// <param name="module">The application module to load.</param>
        void Load(IApplicationModule module);

        /// <summary>
        /// Returns true if a module with that name is loaded.
        /// </summary>
        /// <param name="name">the name of the module to look fo.</param>
        /// <returns>true if the module is loaded; otherwise, false.</returns>
        bool IsLoaded(string name);

        /// <summary>
        /// Returns a list of all loaded modules.
        /// </summary>
        IEnumerable<IApplicationModule> All
        {
            get;
        }

        /// <summary>
        /// Gets the total number of loaded modules.  Decreases when unloading modules.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Activates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to activate a module that is not loaded yet.
        /// </summary>
        /// <param name="name">Name of the module to activate.</param>
        /// <exception cref="InvalidOperationException">Thrown when activating a module that's not loaded.</exception>
        void Activate(string name);

        /// <summary>
        /// Activates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to activate a module that is not loaded yet.
        /// </summary>
        /// <param name="module">A module that must be loaded.</param>
        /// <exception cref="InvalidOperationException">Thrown when activating a module that's not loaded.</exception>
        void Activate(IApplicationModule module);

        /// <summary>
        /// Activates all currently loaded modules.
        /// </summary>
        void ActivateAll();

        /// <summary>
        /// Deactivates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to deactivated a module that not loaded and not activated.
        /// </summary>
        /// <param name="name">Name of the module to deactivate.</param>
        /// <exception cref="InvalidOperationException">Thrown when deactivating a module that's not loaded and not activated.</exception>
        void Deactivate(string name);

        /// <summary>
        /// Deactivates a module.  Generally this means, for a module, to interact with an <see cref="IPresentationController"/>.
        /// It is invalid to deactivated a module that not loaded and not activated.
        /// </summary>
        /// <param name="module">A module to deactivate</param>
        /// <exception cref="InvalidOperationException">Thrown when deactivating a module that's not loaded.</exception>
        void Deactivate(IApplicationModule module);

        /// <summary>
        /// Deactivates all activated modules in reverse order that they have been loaded.
        /// </summary>
        void DeactivateAll();

        /// <summary>
        /// Unloads a module.  If <see cref="ModuleManager"/> is provided and the module implements <see cref="Ninject.Core.IModule"/>, 
        /// then the module will also be unloaded from Ninject.  It is invalid to unload a module that's not been loaded.
        /// </summary>
        /// <param name="name">Name of a module to unload.</param>
        /// <exception cref="InvalidOperationException">Thrown if the module has not been loaded.</exception>
        void Unload(string name);

        /// <summary>
        /// Unloads a module.  If <see cref="ModuleManager"/> is provided and the module implements <see cref="Ninject.Core.IModule"/>, 
        /// then the module will also be unloaded from Ninject.  It is invalid to unload a module that's not been loaded.
        /// </summary>
        /// <param name="module">A module to unload.</param>
        /// <exception cref="InvalidOperationException">Thrown if the module has not been loaded.</exception>
        void Unload(IApplicationModule module);

        /// <summary>
        /// Unloads all loaded modules.  Automatically deactivates all modules before unloading them.
        /// </summary>
        void UnloadAll();
    }
}
