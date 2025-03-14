using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> CreateAsync(CustomerEntity entity);
        Task<bool> DeleteAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity> GetAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<CustomerEntity> UpdateAsync(Expression<Func<CustomerEntity, bool>> expression, CustomerEntity updatedEntity);
    }
}