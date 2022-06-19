using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();

        Task<Walk> GetWalkAsync(Guid Id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk> DeleteWalkAsync(Guid Id);

        Task<Walk> UpdateWalkAsync(Guid Id, Walk walk);
    }
}
