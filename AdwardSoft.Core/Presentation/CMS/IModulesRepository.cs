using AdwardSoft.DTO.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdwardSoft.Core.Identity
{

    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> ReadAsync();

        Task<Module> ReadByIdAsync(int id);

        Task<bool> CreateAsync(Module obj);

        Task<bool> UpdateAsync(Module obj);

        Task<bool> DeleteAsync(int id);

        Task<int> SortAsync(string json);

        Task<IEnumerable<Module>> ReadByUserAsync(long UserId);
    }
}
