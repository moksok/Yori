using Sabio.Web.Models.Requests.Recipes;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class _IngredientsService : BaseService, IIngredientsService
    {

        public void InsertIngredient(List<AddIngredientRequest> Ingredients, int recipeid)
        {
            foreach (AddIngredientRequest Ingredient in Ingredients)
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Ingredients_Insert"
                    , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@recipeid", recipeid);
                        paramCollection.AddWithValue("@name", Ingredient.Name);
                        paramCollection.AddWithValue("@quantity", Ingredient.Quantity);
                        paramCollection.AddWithValue("@measurementtype", Ingredient.MeasurementType);

                    }
                );
            }
        }

        public void UpdateIngredient(UpdateIngredientRequest model)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Ingredients_UpdateById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@id", model.Id);
                    paramCollection.AddWithValue("@name", model.Name);
                    paramCollection.AddWithValue("@quantity", model.Quantity);
                    paramCollection.AddWithValue("@measurementtype", model.MeasurementType);

                }
            );

        }

        public void DeleteIngredient(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Ingredients_DeleteById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@id", id);
                }
            );
        }
    }
}