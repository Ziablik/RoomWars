using System.Linq.Expressions;
using Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Repository<TModel> : IRepository<TModel> where TModel : class
{
    private readonly RoomWarsContext _context;

    public Repository(RoomWarsContext context)
    {
        _context = context;
    }

    public async ValueTask<TModel?> FindAsync(object id)
    {
        return await _context.FindAsync<TModel>(id);
    }

    public IQueryable<TModel> FindAll(bool trackChanges)
    {
        return !trackChanges 
            ? _context.Set<TModel>().AsNoTracking() 
            : _context.Set<TModel>();
    }

    public IQueryable<TModel> FindByCondition(Expression<Func<TModel, bool>> expression, bool trackChanges)
    {
        return !trackChanges ?
            _context.Set<TModel>()
                .Where(expression)
                .AsNoTracking() :
            _context.Set<TModel>()
                .Where(expression);
    }

    public async Task CreateAsync(TModel entity, CancellationToken cancellationToken)
    {
        await _context.Set<TModel>().AddAsync(entity, cancellationToken);
    }

    public async Task CreateAsync(TModel entity)
    {
        await _context.Set<TModel>().AddAsync(entity);
    }

    public void Update(TModel entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task ReloadAsync(TModel entity, CancellationToken cancellationToken)
    {
        await _context.Entry(entity).ReloadAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}