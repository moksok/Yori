using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Recipes
{
    public class CreateRecipeRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Directions { get; set; }

        public int PrepTime { get; set; }

        public int TotalTime { get; set; }

        public int NumberOfServings { get; set; }

        public string UserId { get; set; }

        public List<AddIngredientRequest> Ingredients { get; set; }
    }
}