using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Dtos;
using MovieAPI.Interface;
using MovieAPI.Models;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public readonly IMovieRepository _movieRepository;
        public readonly IMapper _mapper;
        

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            
        }

        // GET: api/<MoviesController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            return Ok(_mapper.Map<IEnumerable<MovieDto>>(movies));
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            
            if(movie == null)
                return NotFound("Movie not found");   
            
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);   
            var createdMovie = await _movieRepository.AddMovieAsync(movie);

            return Ok(_mapper.Map<MovieDto>(createdMovie));
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateMovieDto updateMovieDto)
        {
            var movie = _mapper.Map<Movie>(updateMovieDto);
            var oldMovie = await _movieRepository.UpdateMovieAsync(movie, id);

            if(oldMovie == null)
                return NotFound("Movie not found");

            return Ok(_mapper.Map<MovieDto>(updateMovieDto));
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteMovie = await _movieRepository.DeleteMovieAsync(id);

            if(deleteMovie == null)
                return NotFound("Movie not found");

            return Ok(_mapper.Map<Movie>(deleteMovie));
        }
    }
}
