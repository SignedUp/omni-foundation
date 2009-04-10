using System;
using System.Collections;
using Ninject.Core.Activation;
using Ninject.Core.Creation.Providers;

namespace Ninject.Core.Binding.Syntax
{
    ///<summary>
    /// Extends functionality of <typeparamref name="IBindingTargetSyntax"/>
    ///</summary>
    public static class IBindingTargetSyntaxExtension
    {
        ///<summary>
        /// Extends functionality of <typeparamref name="IBindingTargetSyntax"/>
        /// by allowing binding instances of types directly.
        ///</summary>
        ///<param name="syntax"></param>
        ///<param name="kernel"></param>
        ///<param name="instance">Instance of type.</param>
        ///<typeparam name="T">Type of object.</typeparam>
        ///<returns></returns>
        public static IBindingConditionBehaviorHeuristicComponentOrParameterSyntax To<T>(this IBindingTargetSyntax syntax, IKernel kernel, T instance)
        {
            lock (kernel.Components.BindingRegistry)
            {
                IEnumerator enumerator;
                enumerator = kernel.Components.BindingRegistry.GetBindings(typeof (T)).GetEnumerator();

                while (enumerator.MoveNext())
                {
                    IBinding b = (IBinding) enumerator.Current;
                    if (b.Provider == null)
                    {
                        b.Provider = new InstanceProvider<T>(instance);
                        break;
                    }
                }
            }

            return (IBindingConditionBehaviorHeuristicComponentOrParameterSyntax)syntax;
        }
    }

    public class InstanceBinding<T> : StandardBinding
    {
        public InstanceBinding(IKernel kernel, T instance)
            : base(kernel, typeof(T))
        {
            Provider = new InstanceProvider<T>(instance);
        }
    }

    public class InstanceProvider<T> : ProviderBase
    {
        private T _instance;

        public InstanceProvider(T instance)
            : base(typeof(T))
        {
            _instance = instance;
        }

        public override object Create(IContext context)
        {
            return _instance;
        }

        protected override Type DoGetImplementationType(IContext context)
        {
            return typeof(T);
        }
    }
}