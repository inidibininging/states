using System;
using System.Collections.Generic;
using System.Linq;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class DictionaryGetStateDelegationService<TKey, TValue> 
        : IStateGetService<TKey, TValue>
    {
        private Func<Dictionary<TKey, IState<TKey, TValue>>> GetDelegate { get; set; }

        public IEnumerable<TKey> States => GetDelegate().Keys;

        public DictionaryGetStateDelegationService(Func<Dictionary<TKey, IState<TKey, TValue>>> getDelegate)
        {
            if (getDelegate == null)
                throw new ArgumentNullException(nameof(getDelegate));
            GetDelegate = getDelegate;
        }

        public IState<TKey, TValue> Get(TKey identifier)
        {
            return GetDelegate()[identifier];
        }

        public bool HasState(TKey identifier)
        {
            return GetDelegate().ContainsKey(identifier);
        }

	    // public IStateGetService<TKey, TValue> As<TSharedContextConverted>() where TSharedContextConverted : TValue
        // {

        //     return new GenericGetStateDelegationService<TKey, TValue>(
        //         GetDelegate
        //     );
        // }


        // public bool Can<TState, TSharedContextConverted>()
        //     where TState : IStateConverter<TKey, TSharedContextConverted>
        //     where TSharedContextConverted : TValue
        // => 

        // public TState Convert<TState, TSharedContextConverted>() where TState : IStateConverter<TKey, TSharedContextConverted>
        // => new GenericGetStateDelegationService<TKey, TSharedContextConverted>(
        //         new Func<Dictionary<TKey, IState<TKey, TSharedContextConverted>>>(() => 
        //             GetDelegate().ToDictionary(
        //             (kv) => kv.Key, 
        //             (kv) => 
        //                 new CommandStateActionDelegate<TKey, TSharedContextConverted>(
        //                     (sm) => {
        //                         kv.Value.Handle(sm);
        //                     })))
        //     );
    }
}
