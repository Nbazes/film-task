using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmApi.Models;
using System.Text.Json;
using Newtonsoft.Json;
using FilmApi.Repository;
using Microsoft.Extensions.Logging;

namespace FilmApi.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class MovieController : ControllerBase
     {
          private readonly IRepository<Movie> movieRepository;
          private readonly ILogger<MovieController> logger;

          public MovieController(IRepository<Movie> movieRepository, ILogger<MovieController> logger)
          {
               this.logger = logger;
               this.movieRepository = movieRepository;
          }

          [HttpGet("/api/movie")]
          public IActionResult List(bool? asc = false)
          {
               try
               {
                    if (asc == true)
                    {
                         return Ok(movieRepository.List().OrderBy(movie => movie.Rating));
                    }
                    else
                    {
                         return Ok(movieRepository.List().OrderByDescending(movie => movie.Rating));
                    }
               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in list movies");
                    return StatusCode(500);
               }
          }

          [HttpPut("/api/movie/{id}")]
          public IActionResult Edit(int id, Movie movie)
          {
               try
               {
                    Movie existingMovieByName =
                                   movieRepository.Find(m => m.Title.Equals(movie.Title, StringComparison.InvariantCultureIgnoreCase));


                    if (movieRepository.Get(id) == null)
                    {
                         return BadRequest($"movie id {id} not found");
                    }

                    if (existingMovieByName != null && existingMovieByName.Id != id)
                    {
                         return BadRequest("Movie with same title already found");
                    }


                    movieRepository.Update(id, movie);

                    return Ok();
               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in update movie");
                    return StatusCode(500);
               }
          }

          [HttpPost]
          public IActionResult Post(Movie movie)
          {
               try
               {
                    if (TitleExists(movie.Title))
                    {
                         return BadRequest("Movie with same title already found");
                    }

                    if (movie.Id == null)
                    {
                         movieRepository.Insert(movie);
                    }

                    return Ok(movie);
               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in create movie");
                    return StatusCode(500);
               }
          }

          [HttpGet("/api/movie/{id}")]
          public IActionResult Index(int id = 0)
          {

               try
               {
                    Movie movie = movieRepository.Get(id);
                    if (movie == null)
                    {
                         return NotFound($"movie id {id} not found");
                    }
                    else
                    {
                         return Ok(movie);
                    }
               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in get movie");
                    return StatusCode(500);
               }

          }

          [HttpDelete("/api/movie/{id}")]
          public IActionResult Delete(int id)
          {
               try
               {
                    movieRepository.Delete(id);
                    return Ok();
               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in delete move");
                    return StatusCode(500);
               }
          }

          [HttpGet("/api/movie/highest-rating")]
          public IActionResult GetByHighestRating()
          {
               try
               {
                    return Ok(movieRepository.List().OrderByDescending(m => m.Rating).Take(1));

               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in get-by-highest-rating");
                    return StatusCode(500);
               }
          }

          [HttpGet("/api/movie/lowest-rating")]
          public IActionResult GetByLowestRating()
          {
               try
               {
                    return Ok(movieRepository.List().OrderBy(m => m.Rating).Take(1));
               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in get-by-lowest-rating");
                    return StatusCode(500);
               }
          }

          private bool TitleExists(string title)
          {
               Movie existingMovieByName =
                                 movieRepository.Find(m => m.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));

               return existingMovieByName != null;
          }
     }
}