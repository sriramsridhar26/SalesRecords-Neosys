using System.Linq.Expressions;

namespace SalesRecords_Neosys.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public int totalPages(char c, int val1, int val2);
        public Task<IList<T>> GetAll(int page);
        public Task<IList<T>> GetGreater(int page, int val);
        public Task<IList<T>> GetLesser(int page, int val);
        public Task<IList<T>> GetBetween(int page, int val1, int val2);
        public Task<IList<T>> GetNotEqual(int page, int val);
    }
}
