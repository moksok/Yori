using Sabio.Web.Models.Requests.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IIngredientsService
    {
        void InsertIngredient(List<AddIngredientRequest> Ingredients, int recipeid);

        void UpdateIngredient(UpdateIngredientRequest model);

        void DeleteIngredient(int id);
    }
}
