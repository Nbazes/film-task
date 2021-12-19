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
     public class CategoryController : ControllerBase
     {
          private readonly ILogger<CategoryController> logger;

          public CategoryController(ILogger<CategoryController> logger)
          {
               this.logger = logger;

          }

          [HttpGet("/api/categories")]
          public IActionResult List()
          {
               try
               {
                    return Ok(Enum.GetNames(typeof(Category)));
               }
               catch (Exception ex)
               {
                    logger.LogError(ex, "error in list categories");
                    return StatusCode(500);
               }
          }

     }
}