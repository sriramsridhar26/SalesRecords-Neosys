using SalesRecords_Neosys.Data;
using SalesRecords_Neosys.Model;
using SalesRecords_Neosys.Repository.IRepository;

namespace SalesRecords_Neosys.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _databaseContext;
        private IRepository<SalesRecord> _salesRecords;
        public UnitofWork(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IRepository<SalesRecord> SalesRecords => _salesRecords ??= new Repository<SalesRecord>(_databaseContext);

        public void Dispose()
        {
            _databaseContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
