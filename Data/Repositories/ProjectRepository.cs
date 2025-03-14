//using Data.Interfaces;
//using Data.Entities;
//using System.Linq.Expressions;
//using Data.Contexts;
//using System.Diagnostics;
//using Microsoft.EntityFrameworkCore;

//namespace Business.Repositories;
//public class ProjectRepository(DataContext context) : IProjectRepository
//{
//    private readonly DataContext _context = context;
//    public async Task<ProjectEntity> CreateAsync(ProjectEntity entity)
//    {
//        if (entity == null)
//            return null!;
//        try
//        {
//            await _context.Projects.AddAsync(entity);
//            await _context.SaveChangesAsync();
//            return entity;
//        }
//        catch (Exception ex) { 
//            Debug.WriteLine($"Error creating project entity :: {ex.Message}");
//            return null!;
//        }
//    }

//    public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
//    {
//        try
//        {
//            var projects = await _context.Projects.ToListAsync();
//            return projects;

//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error reading projects :: {ex.Message}");
//            return null!;
//        }

//    }

//    public async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
//    {
//        if(expression == null)
//            return null!;
//        try
//        {
//            return await _context.Projects.FirstOrDefaultAsync(expression) ?? null!;
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error retrieving project :: {ex.Message}");
//            return null!;
//        }

//    }

//    public async Task<ProjectEntity> UpdateAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity)
//    {
//        if (updatedEntity == null || expression == null)
//            return null!;
//        try
//        {
//            var existingEntity = await _context.Projects.FirstOrDefaultAsync(expression);
//            if (existingEntity == null)
//                return null!;

//            //-- The code below is a more compact way to set the values for the existing entity if all fields are supposed to be updated
//            //_context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
//            //await _context.SaveChangesAsync();
//            //return existingEntity;


//            existingEntity.ProjectName = updatedEntity.ProjectName;
//            existingEntity.ProjectManager = updatedEntity.ProjectManager;
//            existingEntity.Status = updatedEntity.Status;
//            existingEntity.Service = updatedEntity.Service;

//            _context.Projects.Update(existingEntity);
//            await _context.SaveChangesAsync();
//            return existingEntity;


//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error updating project entity:: {ex.Message}");
//            return null!;
//        }
//    }

//    public async Task<bool> DeleteAsync(Expression<Func<ProjectEntity, bool>> expression)
//    {
//        if (expression == null)
//            return false;
//        try
//        {
//            var existingEntity = await _context.Projects.FirstOrDefaultAsync(expression);
//            if (existingEntity == null)
//                return false;

//             _context.Projects.Remove(existingEntity);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine($"Error deleting project entity:: {ex.Message}");
//            return false;
//        }

//    }


//}



using Data.Interfaces;
using Data.Entities;
using System.Linq.Expressions;
using Data.Contexts;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Data.Repositories;
public class ProjectRepository(IDbContextFactory<DataContext> contextFactory) : IProjectRepository
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;

    public async Task<ProjectEntity> CreateAsync(ProjectEntity entity)
    {
        if (entity == null)
            return null!;
        try
        {
            try
            {
                using var context = _contextFactory.CreateDbContext(); // Skapa en ny instans
                try
                {
                    await context.Projects.AddAsync(entity);
                    try
                    {
                        await context.SaveChangesAsync();

                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine($"Error saving changes async to context :: {ex.Message}");
                        return null!;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding entity to context.Projects :: {ex.Message}");
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating DbContext :: {ex.Message}");
                return null!;
            }

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating project entity :: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans av DbContext
            var projects = await context.Projects.Include(x => x.Customer).ToListAsync();
            //För att få med Customers så behövs .Include(x => x.Customer) innan .ToListAsync(); 
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading projects :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return null!;
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans
            return await context.Projects.FirstOrDefaultAsync(expression) ?? null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving project :: {ex.Message}");
            return null!;
        }
    }

    //public async Task<ProjectEntity> UpdateAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity)
    //{

    //    if (updatedEntity == null || expression == null)
    //        return null!;
    //    try
    //    {
    //        using var context = _contextFactory.CreateDbContext(); // Ny instans

    //        var existingEntity = await context.Projects.FirstOrDefaultAsync(expression);
    //        if (existingEntity == null)
    //            return null!;

    //        existingEntity.ProjectName = updatedEntity.ProjectName;
    //        existingEntity.ProjectManager = updatedEntity.ProjectManager;
    //        existingEntity.Status = updatedEntity.Status;
    //        existingEntity.Service = updatedEntity.Service;
    //        Debug.WriteLine($"updatedEntity CustomerName :: {updatedEntity.Customer.Name}");
    //        Debug.WriteLine($"existingEntity CustomerName :: {updatedEntity.Customer.Name}");



    //        context.Projects.Update(existingEntity);
    //        await context.SaveChangesAsync();
    //        return existingEntity;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine($"Error updating project entity:: {ex.Message}");
    //        return null!;
    //    }
    //}
    public async Task<ProjectEntity> UpdateAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity)
    {
        var name = updatedEntity.Customer.Name;

        if (updatedEntity == null || expression == null)
            return null!;
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans

            var existingEntity = await context.Projects.Include(x => x.Customer).FirstOrDefaultAsync(expression);
            if (existingEntity == null)
                return null!;

            existingEntity.ProjectName = updatedEntity.ProjectName;
            existingEntity.ProjectManager = updatedEntity.ProjectManager;
            existingEntity.Status = updatedEntity.Status;
            existingEntity.Service = updatedEntity.Service;
            existingEntity.Customer.Name = updatedEntity.Customer.Name;
            Debug.WriteLine($"updatedEntity CustomerName :: {updatedEntity.Customer.Name}");
            Debug.WriteLine($"existingEntity CustomerName :: {updatedEntity.Customer.Name}");



            context.Projects.Update(existingEntity);
            await context.SaveChangesAsync();
            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating project entity:: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return false;
        try
        {
            using var context = _contextFactory.CreateDbContext(); // Ny instans
            var existingEntity = await context.Projects.FirstOrDefaultAsync(expression);
            if (existingEntity == null)
                return false;

            context.Projects.Remove(existingEntity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting project entity:: {ex.Message}");
            return false;
        }
    }
}

