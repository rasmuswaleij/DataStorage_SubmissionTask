using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;
public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerEntity> CreateCustomerAsync(string customername)
    {
        var entity = await _customerRepository.GetAsync(x => x.Name == customername);
        entity ??= await _customerRepository.CreateAsync(CustomerFactory.Create(customername) ?? null!);

        return CustomerFactory.Create(customername) ?? null!;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        Debug.WriteLine("GetAllCustomersAsync anropas");
        var entities = await _customerRepository.GetAllAsync() ?? [];
        var customers = entities.Select(CustomerFactory.Create);

        return customers;
    }

    public async Task<Customer> GetCustomerByExpressionAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _customerRepository.GetAsync(expression);
        var customer = CustomerFactory.Create(entity);
        return customer ?? null!;
    }

    public async Task<Customer> UpdateCustomerAsync(Expression<Func<CustomerEntity, bool>> expression, CustomerUpdateForm form)
    {

        var entity = await _customerRepository.UpdateAsync(expression, CustomerFactory.Create(form));
        var customer = CustomerFactory.Create(entity);
        return customer ?? null!;
    }

    public async Task<bool> DeleteCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var result = await _customerRepository.DeleteAsync(expression);
        return result;
    }
}

