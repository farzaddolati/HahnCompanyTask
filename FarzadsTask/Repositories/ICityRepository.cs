using System.Collections.Generic;
using System.Threading.Tasks;
using FarzadsTask.Domain;

namespace FarzadsTask.Repositories
{
    public interface ICityRepository
    {
        Task<City> GetByIdAsync(int id);
        Task<IEnumerable<City>> GetAllAsync();
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(City city);
    }
}