using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Collections.ObjectModel;

namespace Business.Factories;
public static class CustomerFactory
{
    //public static async ObservableCollection<Customer> Wait(Task<IEnumerable<Customer>>)
    //{
    //    await Task.Run(() => {
    //}

    public static CustomerRegistrationForm Create() => new();
    public static CustomerEntity? Create(string customername) => customername == null ? null : new()
    {
        Name = customername,

    };

    public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
    {
        //-- ID is supposed to be here as well --
        Name = entity.Name,
       
    };

    public static CustomerUpdateForm Create(Customer customer) => new()
    {
        Name = customer.Name,
  
    };

    public static CustomerEntity Create(CustomerUpdateForm form) => new()
    {
        Name = form.Name,

    };
}
