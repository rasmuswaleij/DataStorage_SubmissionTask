using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerEntity> CreateCustomerAsync(string customername);
        Task<bool> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByExpressionAsync(Expression<Func<CustomerEntity, bool>> expression);
        Task<Customer> UpdateCustomerAsync(Expression<Func<CustomerEntity, bool>> expression, CustomerUpdateForm form);
    }
}