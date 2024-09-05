using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
        [Table("Movies")]
        public class Movie
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public double Rating { get; set; }
       }
}
