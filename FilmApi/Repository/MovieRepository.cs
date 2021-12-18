using System.Collections.Generic;
using FilmApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FilmApi.Repository
{
     public class MovieRepository : Repository<Movie>
     {
          public MovieRepository(List<Movie> items, IConfiguration config, ILogger<Movie> logger) : base(items, config, logger)
          {

          }

          public override void Update(int id, Movie item)
          {
               Movie movie = Get(id);
               movie.Title = item.Title;
               movie.Rating = item.Rating;
               movie.Category = item.Category;
          }
     }
}