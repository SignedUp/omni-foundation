namespace Omniscient.Foundation.ApplicationModel
{
    /// <summary>
    /// Represents a wrapper on an object.  Ideal for implementing the Adapter design pattern.
    /// </summary>
    /// <typeparam name="T">the type of object to wrap.</typeparam>
    public interface IObjectWrapper<T>
    {
        /// <summary>
        /// Adapts the wrapper to the public interface of <paramref name="T"/>.  Returns null if no
        /// adaptation is allowed (or possible).
        /// </summary>
        T Adapt();

        /// <summary>
        /// Wraps an item of type T.  It is recommended that implementers do not allow multiple calls to that function.
        /// </summary>
        /// <param name="item">The item to wrap.</param>
        void Wrap(T item);
    }
}
