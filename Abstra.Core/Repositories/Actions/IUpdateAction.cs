namespace Abstra.Core.Repositories.Actions
{
    public interface IUpdateAction<in P> where P : class
    {
        Task<int> Update(P t);
    }
}
