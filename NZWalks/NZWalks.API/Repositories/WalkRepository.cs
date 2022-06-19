using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {

        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();

            await nZWalksDbContext.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid Id)
        {
            var walk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (walk == null) { return null; }

            //Delete the walk from db

            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await nZWalksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid Id)
        {
            return await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid Id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingWalk == null) { return null; }

            existingWalk.Name = walk.Name;
            existingWalk.length = walk.length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            await nZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
