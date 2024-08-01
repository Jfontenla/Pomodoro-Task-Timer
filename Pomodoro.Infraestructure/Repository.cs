using Microsoft.EntityFrameworkCore;
using Pomodoro.Domain.IRepositories;

namespace Pomodoro.Infraestructure
{
    public class Repository<T> : IRepository<T> where  T : class
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext appDbContext, DbSet<T> dbSet)
        {
            _appDbContext = appDbContext;
            _dbSet = dbSet;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw new ArgumentException("Entity not found");

            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
