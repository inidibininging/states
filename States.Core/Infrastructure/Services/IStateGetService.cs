using System.Collections;
using System.Collections.Generic;

namespace States.Core.Infrastructure.Services
{
    public interface IStateGetService<TIdentifier, TSharedContext>
    {
        IState<TIdentifier, TSharedContext> Get(TIdentifier identifier);
        bool HasState(TIdentifier identifier);
        IEnumerable<TIdentifier> States { get; }
        
        IStateGetService<TIdentifier, TSharedContext> As<TSharedContextConverted>()
            where TSharedContextConverted : TSharedContext;
    }
}