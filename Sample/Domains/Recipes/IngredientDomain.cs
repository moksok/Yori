using Sabio.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Recipes
{
    public class IngredientDomain
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public string Name { get; set; }

        public IngredientMeasurementType MeasurementType { get; set; }

        public int Quantity { get; set; }
    }
}