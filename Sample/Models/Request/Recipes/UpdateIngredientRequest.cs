using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Recipes
{
    public class UpdateIngredientRequest : AddIngredientRequest
    {
        [Required]
        public int Id { get; set; }

        public int RecipeId { get; set; }
    }
}