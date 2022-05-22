using System.Linq.Expressions;

namespace Application.Contracts;

public interface IRepository<TModel>
{
    public ValueTask<TModel?> FindAsync(object id);
    public IQueryable<TModel> FindAll(bool trackChanges);
    public IQueryable<TModel> FindByCondition(Expression<Func<TModel, bool>> expression, bool trackChanges);
    public Task CreateAsync(TModel entity, CancellationToken cancellationToken);
    public Task CreateAsync(TModel entity);
    public void Update(TModel entity);
    public Task ReloadAsync(TModel entity, CancellationToken cancellationToken);
    public Task SaveChangesAsync();
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}