using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(Repositories.IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties() {

            var walkDiffDomain = await walkDifficultyRepository.GetAllDifficultiesAsync();

            var walkDiffDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDiffDomain);

            return Ok(walkDiffDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDiffAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id) {
            var walkDifficulty = await walkDifficultyRepository.GetDifficultyAsync(id);

            if (walkDifficulty == null) { return NotFound(); }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDiffAsync(Models.DTO.AddWalkDiffRequest addWalkDiffRequest) {

            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDiffRequest.Code
            };

            walkDifficultyDomain = await walkDifficultyRepository.AddWalkDifficultyAsync(walkDifficultyDomain);

            var walkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDiffDTO.Id }, walkDiffDTO);
        
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDiffAsync(Guid id, Models.DTO.UpdateRegionRequest updateRegionRequest) {

            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateRegionRequest.Code
            };

            walkDifficultyDomain = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null) { return NotFound(); }

            var walkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDiffDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDiffAsync(Guid id) {

            var existingWalkDiff = await walkDifficultyRepository.DeleteDifficultyAsync(id);

            if (existingWalkDiff == null) { return NotFound(); }

            var walkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(existingWalkDiff);

            return Ok(walkDiffDTO);

        }
    }
}
