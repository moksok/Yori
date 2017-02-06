using Sabio.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Recipes
{
    public class AddIngredientRequest
    {
        //public int RecipeId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public IngredientMeasurementType MeasurementType { get; set; }
    }
}