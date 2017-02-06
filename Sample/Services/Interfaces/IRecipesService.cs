using Sabio.Web.Domain.Recipes;
using Sabio.Web.Models.Requests.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IRecipesService
    {
        int InsertRecipe(CreateRecipeRequest Model);

        List<RecipeDomain> GetRecipesByUserId(string UserId);

        List<RecipeDomain> GetRecipesAll();

        RecipeDomain GetRecipeByRecipeId(int RecipeId);

        void Delete(int id);

        void Update(UpdateRecipeRequest model);
    }
}
