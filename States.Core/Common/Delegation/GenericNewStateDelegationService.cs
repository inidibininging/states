using System;
using System.Collections.Generic;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class GenericNewStateDelegationService<TKey, TValue>
        : IStateNewService<TKey, TValue>
    {
        private readonly Func<TKey> _keyGenerator;
        private Func<Dictionary<TKey, IState<TKey, TValue>>> GetDelegate { get; set; }

        public GenericNewStateDelegationService(
            Func<Dictionary<TKey, IState<TKey, TValue>>> getDelegate,
            Func<TKey> keyGenerator)
        {
            _keyGenerator = keyGenerator ?? throw new ArgumentNullException(nameof(keyGenerator));
            GetDelegate = getDelegate ?? throw new ArgumentNullException(nameof(getDelegate));
        }

        public TKey New(IState<TKey, TValue> state)
        {
            var id = _keyGenerator();
            GetDelegate()?.Add(id, state);
            return id;
        }

        public TKey New(TKey identifier, IState<TKey, TValue> state)
        {
            GetDelegate()?.Add(identifier, state);
            return identifier;
        }
    }
}