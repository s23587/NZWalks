using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
     public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllRegionsAsync();

       Task<Region> GetRegionAsync(Guid Id);

       Task<Region> AddRegionAsync(Region region);

       Task<Region> DeleteRegionAsync(Guid Id);

       Task<Region> UpdateRegionAsync(Guid Id, Region region);
    }
}
