using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers

{

    [ApiController]
    [Route("[controller]")]

    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllWalks() {

            var walks = await walkRepository.GetAllWalksAsync();

            var walksDTO = mapper.Map<List<Models.DTO.Walks>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkRepository.GetWalkAsync(id);

            if (walk == null) { return NotFound(); }

            var walkDTO = mapper.Map<Models.DTO.Walks>(walk);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequest addWalkRequest)
        {
            // Request to domain model
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                length = addWalkRequest.length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            // Pass details to repository
            walk = await walkRepository.AddWalkAsync(walk);

            // Convert back to DTO
            var walkDTO = new Models.DTO.Walks
            {
                Id = walk.Id,
                Name = walk.Name,
                length = walk.length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // Get region from database
            var deletedWalk = await walkRepository.DeleteWalkAsync(id);

            // if region not found send message
            if (deletedWalk == null) { return NotFound(); }

            // convert response back to dto model
            //var walkDTO = mapper.Map<Models.DTO.Walks>(deletedWalk);

            var walkDTO = new Models.DTO.Walks()
            {
                Name = deletedWalk.Name,
                length = deletedWalk.length,
                RegionId = deletedWalk.RegionId,
                WalkDifficultyId = deletedWalk.WalkDifficultyId
            };

            //return Ok response
            return Ok(walkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            // Convert DTO to domain model
            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                length = updateWalkRequest.length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Update region using repository
            var updatedWalk = await walkRepository.UpdateWalkAsync(id, walk);

            // if null then notfound

            if (updatedWalk == null) { return NotFound(); }

            // convert domain back to dto
            var walkDTO = new Models.DTO.Walks
            {
                Id = updatedWalk.Id,
                Name = updatedWalk.Name,
                length = updatedWalk.length,
                RegionId = updatedWalk.RegionId,
                WalkDifficultyId = updatedWalk.WalkDifficultyId
            };

            // return ok response

            return Ok(walkDTO);
        }

    }
}
