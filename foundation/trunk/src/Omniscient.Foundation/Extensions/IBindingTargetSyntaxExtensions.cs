using System;
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
            InstanceBinding<T> binding;
            binding = new InstanceBinding<T>(kernel, instance);
            kernel.AddBinding(binding);

            return (IBindingConditionBehaviorHeuristicComponentOrParameterSyntax)syntax;
        }
    }

    ///<summary>
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class InstanceBinding<T> : StandardBinding
    {
        ///<summary>
        ///</summary>
        ///<param name="kernel"></param>
        ///<param name="instance"></param>
        public InstanceBinding(IKernel kernel, T instance)
            : base(kernel, typeof(T))
        {
            Provider = new InstanceProvider(instance);
        }

        private class InstanceProvider : ProviderBase
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
}