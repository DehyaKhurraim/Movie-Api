using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Interface
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie?>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(int id);
        Task<Movie> AddMovieAsync(Movie movie);
        Task<Movie?> UpdateMovieAsync(Movie movie, int id);
        Task<Movie?> DeleteMovieAsync(int id);
    }
}