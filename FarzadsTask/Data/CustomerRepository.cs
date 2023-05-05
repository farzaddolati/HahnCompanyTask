using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarzadsTask.Domain;
using FarzadsTask.Repositories;

namespace FarzadsTask.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyApiDbContext _dbContext;

        public CustomerRepository(MyApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _dbContext.Customers.Include(c => c.City).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.Include(c => c.City).ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task UpdateAsync(Customer customer)
        //{
        //    _dbContext.Entry(customer).State = EntityState.Modified;
        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task UpdateAsync(Customer customer)
        {
            var existingCustomer = await _dbContext.Customers.Include(c => c.City).FirstOrDefaultAsync(c => c.Id == customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.Address = customer.Address;
                if (customer.City != null)
                {
                    var existingCity = await _dbContext.Cities.FindAsync(customer.City.Id);
                    if (existingCity != null)
                    {
                        existingCustomer.City = existingCity;
                    }
                }
                else
                {
                    existingCustomer.City = null;
                }
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
