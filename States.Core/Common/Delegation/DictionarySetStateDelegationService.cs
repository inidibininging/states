using System;
using System.Collections.Generic;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class DictionarySetStateDelegationService<TKey, TValue> 
        : IStateSetService<TKey, TValue>
    {
        private Func<Dictionary<TKey, IState<TKey, TValue>>> GetDelegate { get; set; }
        public Action<TKey, IState<TKey, TValue>> SetterFunction { get; }

        public DictionarySetStateDelegationService(Func<Dictionary<TKey, IState<TKey, TValue>>> getDelegate, 
            Action<TKey, IState<TKey, TValue>> setterFunction)
        {
            GetDelegate = getDelegate ?? throw new ArgumentNullException(nameof(getDelegate));
            SetterFunction = setterFunction?? throw new ArgumentNullException(nameof(setterFunction));
        }

        public bool Set(TKey identifier, IState<TKey, TValue> state)
        {
            SetterFunction?.Invoke(identifier, state);
            return true;
        }
    }
}