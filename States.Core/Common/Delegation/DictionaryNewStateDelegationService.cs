using System;
using System.Collections.Generic;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class DictionaryNewStateDelegationService<TKey, TValue>
        : IStateNewService<TKey, TValue>
    {
        private readonly Func<TKey> _keyGenerator;
        private Func<Dictionary<TKey, IState<TKey, TValue>>> GetDelegate { get; set; }
        public Action<TKey, IState<TKey, TValue>> AddEntry { get; }

        public DictionaryNewStateDelegationService(
            Func<Dictionary<TKey, IState<TKey, TValue>>> getDelegate,
            Func<TKey> keyGenerator,
            Action<TKey, IState<TKey, TValue>> addEntry)
        {
            _keyGenerator = keyGenerator ?? throw new ArgumentNullException(nameof(keyGenerator));
            AddEntry = addEntry ?? throw new ArgumentNullException(nameof(addEntry));
            GetDelegate = getDelegate ?? throw new ArgumentNullException(nameof(getDelegate));
        }

        public TKey New(IState<TKey, TValue> state)
        {
            var id = _keyGenerator();
            AddEntry?.Invoke(id, state);
            return id;
        }

        public TKey New(TKey identifier, IState<TKey, TValue> state)
        {
            AddEntry?.Invoke(identifier, state);
            return identifier;
        }
    }
}