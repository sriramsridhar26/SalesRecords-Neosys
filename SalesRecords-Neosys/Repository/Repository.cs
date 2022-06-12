using Microsoft.EntityFrameworkCore;
using SalesRecords_Neosys.Data;
using SalesRecords_Neosys.Repository.IRepository;
using System.Linq.Expressions;

namespace SalesRecords_Neosys.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        float pageResults = 20f;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public int totalPages(char c, int val1, int val2)
        {

            if (c == '>')
            {
                var pageCount = (int)Math.Ceiling(_db.SalesRecords.Where(p => p.TotalProfit > val1).Count() / pageResults);
                return pageCount;

            }
            else if (c == '<')
            {
                var pageCount = (int)Math.Ceiling(_db.SalesRecords.Where(p => p.TotalProfit < val1).Count() / pageResults);
                return pageCount;
            }
            else if (c == 'b')
            {
                var pageCount = (int)Math.Ceiling(_db.SalesRecords.Where(p => (p.TotalProfit >= val1) && (p.TotalProfit <= val2)).Count() / pageResults);
                return pageCount;
            }
            else if (c == 'n')
            {
                var pageCount = (int)Math.Ceiling(_db.SalesRecords.Where(p => p.TotalProfit != val1).Count() / pageResults);
                return pageCount;
            }
            else
            {
                var pageCount = (int)Math.Ceiling(dbSet.Count() / pageResults);
                return pageCount;
            }
        }
        public async Task<IList<T>> GetAll(int page)
        {
            var records = await _db.SalesRecords
                                .Skip((page-1)* (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            return (IList<T>)records;
        }
        public async Task<IList<T>> GetGreater(int page, int val)
        {
            var records = await _db.SalesRecords
                                .Where(p => p.TotalProfit > val)
                                .OrderBy(p => p.id)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            return (IList<T>)records;
        }
        public async Task<IList<T>> GetLesser(int page, int val)
        {
            var records = await _db.SalesRecords
                                .Where(p => p.TotalProfit < val)
                                .OrderBy(p => p.id)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            return (IList<T>)records;
        }
        public async Task<IList<T>> GetBetween(int page, int val1, int val2)
        {
            var records = await _db.SalesRecords
                                .Where(p => (p.TotalProfit >= val1) && (p.TotalProfit <= val2))
                                .OrderBy(p => p.id)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            return (IList<T>)records;
        }
        public async Task<IList<T>> GetNotEqual(int page, int val)
        {
            var records = await _db.SalesRecords
                                .Where(p => p.TotalProfit != val)
                                .OrderBy(p => p.id)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            return (IList<T>)records;
        }

    }
}
