namespace States.Core.Infrastructure.Services
{
    /// <summary>
    /// Sets a state of context
    /// </summary>
    /// <typeparam name="TIdentifier">Identifier type</typeparam>
    /// <typeparam name="TSharedContext">The context type</typeparam>
    public interface IStateSetService<TIdentifier, TSharedContext>
    {
        bool Set(TIdentifier identifier, IState<TIdentifier, TSharedContext> state);
    }
}