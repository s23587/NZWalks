using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Models.DTO
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        // Navigation properties
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
