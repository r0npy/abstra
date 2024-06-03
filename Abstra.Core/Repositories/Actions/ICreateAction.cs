namespace Abstra.Core.Repositories.Actions
{
    public interface ICreateAction<R, in P> where R : class where P : class
    {
        Task<R> Create(P y);
    }
}
