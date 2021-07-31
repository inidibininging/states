using System;
using System.Collections.Generic;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class DictionaryStateMachine<TKey, TValue> 
        : IStateMachine<TKey, TValue>
    {
        IStateGetService<TKey, TValue> IStateMachine<TKey, TValue>.GetService => GetService;
        
        IStateSetService<TKey, TValue> IStateMachine<TKey, TValue>.SetService => SetService;

        IStateNewService<TKey, TValue> IStateMachine<TKey, TValue>.NewService => NewService;
        
        private DictionaryGetStateDelegationService<TKey, TValue> GetService { get; }
        private DictionarySetStateDelegationService<TKey, TValue> SetService { get; }
        private DictionaryNewStateDelegationService<TKey, TValue> NewService { get; }

        public DictionaryStateMachine(Dictionary<TKey, IState<TKey,TValue>> memory, Func<TKey> keyGenerator)
        {
            GetService = new DictionaryGetStateDelegationService<TKey, TValue>(() => memory);
            SetService = new DictionarySetStateDelegationService<TKey, TValue>(() => memory,
                (key, state) => memory[key] = state);
            NewService = new DictionaryNewStateDelegationService<TKey, TValue>(() => memory, 
                keyGenerator,
                (key, state) => memory[key] = state);
        }
        
        public TKey SharedIdentifier { get; private set; }
        public TValue SharedContext { get; set; }
        private Dictionary<TKey,TimeSpan> timeLog = new();

        public IReadOnlyDictionary<TKey, TimeSpan> TimeLog
        {
            get => timeLog;            
        }
        public void Run(TKey key)
        {
            Console.WriteLine(key.ToString());
            if (!GetService.HasState(key))
                throw new ArgumentNullException(nameof(key));

            var before = DateTime.Now;
            SharedIdentifier = key;
            GetService.Get(key).Handle(this);
            timeLog[key] = DateTime.Now - before;
        }
    }
}
