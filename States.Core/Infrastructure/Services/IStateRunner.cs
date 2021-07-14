namespace States.Core.Infrastructure.Services
{
    public interface IStateRunner<in TIdentifier>
    {
        void Run(TIdentifier key);
    }
}