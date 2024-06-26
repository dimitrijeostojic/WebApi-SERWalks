﻿using System.ComponentModel.DataAnnotations;

namespace SERWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximum 100 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be maximum 1000 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50, ErrorMessage = "Length has to be between 0 and 50 km")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
