namespace Abstra.Core.Repositories.Actions
{
    public interface IReadAction<R, in P> where R : class
    {
        Task<IEnumerable<R>?> Get();
        Task<R?> Get(P id);
    }
}
