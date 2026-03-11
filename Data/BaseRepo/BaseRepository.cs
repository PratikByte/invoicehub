using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Data.BaseRepo
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly InvoiceDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(InvoiceDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _dbSet.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entities: {ex.Message}");
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entity by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                await _dbSet.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entity: {ex.Message}");
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating entity: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
                if (entity == null) return false;
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entity: {ex.Message}");
                throw;
            }
        }
    }

}