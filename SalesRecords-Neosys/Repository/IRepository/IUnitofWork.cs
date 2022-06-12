using SalesRecords_Neosys.Model;

namespace SalesRecords_Neosys.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IRepository<SalesRecord> SalesRecords { get; }
    }
}
