using System;
using System.Collections.Generic;
using States.Core.Infrastructure.Services;

namespace States.Core.Common.Delegation
{
    public class GenericStateMachine<TKey, TValue> 
        : IStateMachine<TKey, TValue>
    {
        IStateGetService<TKey, TValue> IStateMachine<TKey, TValue>.GetService => GetService;
        
        IStateSetService<TKey, TValue> IStateMachine<TKey, TValue>.SetService => SetService;

        IStateNewService<TKey, TValue> IStateMachine<TKey, TValue>.NewService => NewService;
        
        private GenericGetStateDelegationService<TKey, TValue> GetService { get; }
        private GenericSetStateDelegationService<TKey, TValue> SetService { get; }
        private GenericNewStateDelegationService<TKey, TValue> NewService { get; }

        public GenericStateMachine(Dictionary<TKey, IState<TKey,TValue>> memory, Func<TKey> keyGenerator)
        {
            GetService = new GenericGetStateDelegationService<TKey, TValue>(() => memory);
            SetService = new GenericSetStateDelegationService<TKey, TValue>(() => memory);
            NewService = new GenericNewStateDelegationService<TKey, TValue>((() => memory), keyGenerator);
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
