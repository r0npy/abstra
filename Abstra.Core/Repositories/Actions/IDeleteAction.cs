namespace Abstra.Core.Repositories.Actions
{
    public interface IDeleteAction<in P>
    {
        Task<int> Remove(P id);
    }
}
