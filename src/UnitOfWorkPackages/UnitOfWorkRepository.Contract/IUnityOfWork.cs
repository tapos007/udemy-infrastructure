using System;
using System.Threading.Tasks;

namespace UdemyInfrastructure.UnitOfWorkRepository.Contract;

public interface IUnityOfWork  : IDisposable
{
    Task BeginTransactionAsync();
    Task RollBackTransactionAsync();
    Task CommitTransactionAsync();
    Task<int> SaveChangesAsync();
}