using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
     public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllAsync();

       Task<Region> GetAsync(Guid Id);

       Task<Region> AddAsync(Region region);

       Task<Region> DeleteAsync(Guid Id);

       Task<Region> UpdateAsync(Guid Id, Region region);
    }
}
