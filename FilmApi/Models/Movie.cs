using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace FilmApi.Models
{
     public class Movie : IModel
     {
          public Movie()
          {
          }

          [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
          public int? Id { get; set; }
          
          [Required]
          public string? Title { get; set; }

          [Required]
          public int? Rating { get; set; }
          
          [Required]
          public Category? Category { get; set; }
     }

}