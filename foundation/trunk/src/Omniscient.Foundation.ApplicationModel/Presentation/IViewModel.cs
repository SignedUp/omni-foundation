﻿using System;
namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// IViewModel is a wrapper for the IModel which is bound to IView.
    /// </summary>
    /// <typeparam name="TModel">Type of the model to be wrapped.</typeparam>
    [Obsolete("Use MVVM.IViewModel instead.")]
    public interface IViewModel<TModel> : IObjectWrapper<TModel> where TModel : IModel
    {
    }
}
