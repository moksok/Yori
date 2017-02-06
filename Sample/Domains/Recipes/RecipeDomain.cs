using Sabio.Web.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Recipes
{
    public class RecipeDomain
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }

        public string Directions { get; set; }

        public string Description { get; set; }

        public int Preptime { get; set; }

        public int Totaltime { get; set; }

        public int NumberOfServings { get; set; }

        public string UserId { get; set; }

        public List<IngredientDomain> Ingredients { get; set; } = new List<IngredientDomain>();

        public MediaDomain Media { get; set; }

    }
}