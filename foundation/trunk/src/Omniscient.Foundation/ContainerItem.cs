namespace Omniscient.Foundation
{
    //todo: this looks like ServiceModel with the use of ServiceContainer; which one is better?  Why two?
    ///<summary>
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class ContainerItem<T>
    {
        private T _instance;

        ///<summary>
        ///</summary>
        public ContainerItem()
            : this(default(T))
        {
        }

        ///<summary>
        ///</summary>
        ///<param name="instance"></param>
        public ContainerItem(T instance)
        {
            _instance = instance;
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public virtual T GetInstance()
        {
            return _instance;
        }
    }
}
