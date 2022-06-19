using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<Models.Domain.WalkDifficulty>> GetAllDifficultiesAsync();
        Task<Models.Domain.WalkDifficulty> GetDifficultyAsync(Guid id);

        Task<Models.Domain.WalkDifficulty> AddWalkDifficultyAsync(Models.Domain.WalkDifficulty walkDiff);
        Task<Models.Domain.WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, Models.Domain.WalkDifficulty walkDiff);

        Task<Models.Domain.WalkDifficulty> DeleteDifficultyAsync(Guid id);

    }
}
