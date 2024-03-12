using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SERWalks.API.Data;
using SERWalks.API.Models.Domain;
using SERWalks.API.Models.DTO;
using SERWalks.API.Repositories;

namespace SERWalks.API.Controllers
{

    //https://localhost:1234/api/regions 
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly SERWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(SERWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }


        //GET ALL REGIONS
        //https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            //Return DTOs
            return Ok(regionsDto);
        }


        //GET SINGLE REGION (Get Region By Id)
        //GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Region Domain Model From Database
            //var region=dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map Domain Models to DTOs
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //Return DTOs
            return Ok(regionDto);
        }


        //POST To Create New Region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map Dto to Domain Model
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain model back to Dto
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }




        //Update region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            //Map Dto to Domain Model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };

            //Check if region exists

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Convert Domain Model to Dto

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }



        //Delete region
        //DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }



            //return deleted Region back
            //map Domain Model to Dto

            RegionDto regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };


            return Ok(regionDto);
        }
    }
}

