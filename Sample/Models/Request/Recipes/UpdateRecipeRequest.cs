using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Recipes
{
    public class UpdateRecipeRequest : CreateRecipeRequest
    {
        [Required]
        public int Id { get; set; }

        public int MediaId { get; set; }

        public List<UpdateIngredientRequest> UpdatedIngredients { get; set; }
    }
}