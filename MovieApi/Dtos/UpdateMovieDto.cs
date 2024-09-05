using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Dtos
{
    public class UpdateMovieDto
    {
        [Required]
        [MaxLength(10, ErrorMessage ="Must be less than 10 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(10, ErrorMessage ="Must be more than 10 characters")]
        public string Description { get; set; } = string.Empty;
        public double Rating { get; set; }
    }
}