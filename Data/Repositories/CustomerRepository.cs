using Data.Interfaces;
using Data.Entities;
using System.Linq.Expressions;
using Data.Contexts;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class CustomerRepository(IDbContextFactory<DataContext> contextFactory) : ICustomerRepository
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;

    public async Task<CustomerEntity> CreateAsync(CustomerEntity entity)
    {
        if (entity == null)
            return null!;
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Skapa en ny instans
            await context.Customers.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity :: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans av DbContext
            var customers = await context.Customers.ToListAsync();
            //För att få med Customers så behövs .Include(x => x.Customer) innan .ToListAsync(); 
            return customers;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading customers :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerEntity> GetAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        if (expression == null)
            return null!;
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans
            return await context.Customers.FirstOrDefaultAsync(expression) ?? null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving customer :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerEntity> UpdateAsync(Expression<Func<CustomerEntity, bool>> expression, CustomerEntity updatedEntity)
    {
        if (updatedEntity == null || expression == null)
            return null!;
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans

            var existingEntity = await context.Customers.FirstOrDefaultAsync(expression);
            if (existingEntity == null)
                return null!;

            existingEntity.Name = updatedEntity.Name;

            context.Customers.Update(existingEntity);
            await context.SaveChangesAsync();
            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating customer entity:: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        if (expression == null)
            return false;
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans
            var existingEntity = await context.Customers.FirstOrDefaultAsync(expression);
            if (existingEntity == null)
                return false;

            context.Customers.Remove(existingEntity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting customer entity:: {ex.Message}");
            return false;
        }
    }
}