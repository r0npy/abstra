namespace Abstra.Core.Repositories.Actions
{
    public interface IReadAction<R, in P>
    {
        Task<IEnumerable<R>?> Get();
        Task<R?> Get(P id);
    }
}
