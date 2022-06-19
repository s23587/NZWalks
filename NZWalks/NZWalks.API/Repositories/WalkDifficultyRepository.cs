using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(Data.NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDiff)
        {
            walkDiff.Id = Guid.NewGuid();

            await nZWalksDbContext.AddAsync(walkDiff);
            await nZWalksDbContext.SaveChangesAsync();

            return walkDiff;

        }

        public async Task<WalkDifficulty> DeleteDifficultyAsync(Guid id)
        {
            var existingWalkDiff = await nZWalksDbContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDiff == null) { return null; }

            nZWalksDbContext.WalkDifficulty.Remove(existingWalkDiff);
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDiff;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllDifficultiesAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetDifficultyAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDiff)
        {
            var existingWalkDiff = await nZWalksDbContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDiff == null) { return null; }

            existingWalkDiff.Code = walkDiff.Code;

            await nZWalksDbContext.SaveChangesAsync();

            return existingWalkDiff;
        }
    }
}
