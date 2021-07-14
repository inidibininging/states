using System;
using System.Collections.Generic;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class GenericSetStateDelegationService<TKey, TValue> 
        : IStateSetService<TKey, TValue>
    {
        private Func<Dictionary<TKey, IState<TKey, TValue>>> GetDelegate { get; set; }

        public GenericSetStateDelegationService(Func<Dictionary<TKey, IState<TKey, TValue>>> getDelegate)
        {
            GetDelegate = getDelegate ?? throw new ArgumentNullException(nameof(getDelegate));
        }

        public bool Set(IState<TKey, TValue> state, TKey identifier)
        {
            GetDelegate()?.Add(identifier, state);
            return true;
        }
    }
}