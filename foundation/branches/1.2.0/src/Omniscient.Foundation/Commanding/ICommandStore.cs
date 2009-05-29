namespace Omniscient.Foundation.Commanding
{
    public interface ICommandStore
    {
        event CommandAddedEventHandler CommandRegistered;
        ICommandCore this[string name] { get; }
        void RegisterCommand<T>() where T : ICommandCore, new();
        T GetCommand<T>() where T : ICommandCore, new(); 
    }

    public delegate void CommandAddedEventHandler(ICommandCore command);
}
