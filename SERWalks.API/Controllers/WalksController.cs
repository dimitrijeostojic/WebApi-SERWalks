using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERWalks.API.Data;
using SERWalks.API.Models.Domain;
using SERWalks.API.Models.DTO;
using SERWalks.API.Repositories;

namespace SERWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly SERWalksDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(SERWalksDbContext dbContext, IMapper mapper, IWalkRepository walkRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE Walk
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //map dto to domain model

            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            //save in database

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            //map domain model to dto

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            //return dto

            return Ok(walkDto);
        }

        //GET walks
        //GET: /api.walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await walkRepository.GetAllAsync();

            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walksDto);

        }

        //GET BY ID
        //GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        //Update Walk By Id
        //PUT: /api/walks/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel==null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        //Delete Walk By Id
        //DELETE: /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDomainModel= await walkRepository.DeleteAsync(id);

            if (walkDomainModel==null)
            {
                return NotFound();
            }

            var walkDto=mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

    }
}
