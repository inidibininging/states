using System;
using System.Collections.Generic;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class GenericGetStateDelegationService<TKey, TValue> 
        : IStateGetService<TKey, TValue>
    {
        private Func<Dictionary<TKey, IState<TKey, TValue>>> GetDelegate { get; set; }

        public IEnumerable<TKey> States => GetDelegate().Keys;

        public GenericGetStateDelegationService(Func<Dictionary<TKey, IState<TKey, TValue>>> getDelegate)
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
    }
}