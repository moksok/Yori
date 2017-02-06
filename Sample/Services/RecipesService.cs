using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Models.Requests.Recipes;
using System.Data.SqlClient;
using Sabio.Web.Domain.Recipes;
using System.Data;
using Sabio.Data;
using Sabio.Web.Enums;
using Sabio.Web.Domain.Media;

namespace Sabio.Web.Services
{
    public class _RecipesService : BaseService, IRecipesService
    {
        // Insert new recipe service
        public int InsertRecipe(CreateRecipeRequest Model) // post
        {
            int _id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Recipes_Insert"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {

                  paramCollection.AddWithValue("@name", Model.Name);
                  paramCollection.AddWithValue("@directions", Model.Directions);
                  paramCollection.AddWithValue("@preptime", Model.PrepTime);
                  paramCollection.AddWithValue("@totaltime", Model.TotalTime);
                  paramCollection.AddWithValue("@numberofservings", Model.NumberOfServings);
                  paramCollection.AddWithValue("@userid", Model.UserId);
                  paramCollection.AddWithValue("@description", Model.Description);
                  SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                  p.Direction = System.Data.ParameterDirection.Output;

                  paramCollection.Add(p);

              },
               returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out _id);
               }
            );

            return _id;
        }

        //Get
        public List<RecipeDomain> GetRecipesByUserId(string UserId)
        {
            List<RecipeDomain> ListRecipeDomain = null;
            MediaDomain MediaDomain = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Recipes_GetAllRecipesByUserId"
                , inputParamMapper: delegate (SqlParameterCollection paramcollection)
                {
                    paramcollection.AddWithValue("@userid", UserId);
                }, map: delegate (IDataReader reader, short set)
                {
                    RecipeDomain p = new RecipeDomain();
                    int startingIndex = 0;
                    p.Id = reader.GetSafeInt32(startingIndex++);
                    p.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                    p.Name = reader.GetSafeString(startingIndex++);
                    p.Directions = reader.GetSafeString(startingIndex++);
                    p.Preptime = reader.GetSafeInt32(startingIndex++);
                    p.Totaltime = reader.GetSafeInt32(startingIndex++);
                    p.NumberOfServings = reader.GetSafeInt32(startingIndex++);
                    p.Description = reader.GetSafeString(startingIndex++);

                    MediaDomain = new MediaDomain();

                    MediaDomain.Id = reader.GetSafeInt32(startingIndex++);
                    MediaDomain.DataType = reader.GetSafeString(startingIndex++);
                    MediaDomain.Url = reader.GetSafeString(startingIndex++);
                    MediaDomain.Created = reader.GetSafeDateTime(startingIndex++);

                    p.Media = MediaDomain;

                    if (ListRecipeDomain == null)
                    {
                        ListRecipeDomain = new List<RecipeDomain>();
                    }
                    ListRecipeDomain.Add(p);
                });

            return ListRecipeDomain;
        }

        // Get ALL recipes
        public List<RecipeDomain> GetRecipesAll()
        {
            List<RecipeDomain> ListRecipeDomain = null;
            MediaDomain MediaDomain = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Recipes_GetAllRecipes"
                , inputParamMapper: null
                , map: delegate (IDataReader reader, short set)
                {
                    RecipeDomain p = new RecipeDomain();
                    int startingIndex = 0;
                    p.Id = reader.GetSafeInt32(startingIndex++);
                    p.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                    p.Name = reader.GetSafeString(startingIndex++);
                    p.Description = reader.GetSafeString(startingIndex++);
                    p.UserId = reader.GetSafeString(startingIndex++);
                    p.Directions = reader.GetSafeString(startingIndex++);
                    p.Preptime = reader.GetSafeInt32(startingIndex++);
                    p.Totaltime = reader.GetSafeInt32(startingIndex++);
                    p.NumberOfServings = reader.GetSafeInt32(startingIndex++);

                    MediaDomain = new MediaDomain();

                    MediaDomain.Id = reader.GetSafeInt32(startingIndex++);
                    MediaDomain.DataType = reader.GetSafeString(startingIndex++);
                    MediaDomain.Url = reader.GetSafeString(startingIndex++);
                    MediaDomain.Created = reader.GetSafeDateTime(startingIndex++);

                    p.Media = MediaDomain;

                    if (ListRecipeDomain == null)
                    {
                        ListRecipeDomain = new List<RecipeDomain>();
                    }
                    ListRecipeDomain.Add(p);
                });

            return ListRecipeDomain;
        }

        public RecipeDomain GetRecipeByRecipeId(int RecipeId)
        {
            RecipeDomain RecipeDomain = null;
            MediaDomain MediaDomain = null;
            IngredientDomain IngredientDomain = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Recipes_GetByRecipeId"
                , inputParamMapper: delegate (SqlParameterCollection paramcollection)
                {
                    paramcollection.AddWithValue("@Id", RecipeId);
                }, map: delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        int startingIndex = 0;

                        RecipeDomain = new RecipeDomain();

                        RecipeDomain.Id = reader.GetSafeInt32(startingIndex++);
                        RecipeDomain.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                        RecipeDomain.Name = reader.GetSafeString(startingIndex++);
                        RecipeDomain.Description = reader.GetSafeString(startingIndex++);
                        RecipeDomain.Directions = reader.GetSafeString(startingIndex++);
                        RecipeDomain.Preptime = reader.GetSafeInt32(startingIndex++);
                        RecipeDomain.Totaltime = reader.GetSafeInt32(startingIndex++);
                        RecipeDomain.NumberOfServings = reader.GetSafeInt32(startingIndex++);
                        RecipeDomain.UserId = reader.GetSafeString(startingIndex++);

                        MediaDomain = new MediaDomain();

                        MediaDomain.Id = reader.GetSafeInt32(startingIndex++);
                        MediaDomain.DataType = reader.GetSafeString(startingIndex++);
                        MediaDomain.Url = reader.GetSafeString(startingIndex++);
                        MediaDomain.Created = reader.GetSafeDateTime(startingIndex++);
                        
                        RecipeDomain.Media = MediaDomain;

                        if (RecipeDomain == null)
                        {
                            RecipeDomain = new RecipeDomain();
                        }
                    } else if (set == 1)
                    {
                        IngredientDomain = new IngredientDomain();

                        int startingIndex = 0;
                        IngredientDomain.Id = reader.GetSafeInt32(startingIndex++);
                        IngredientDomain.RecipeId = reader.GetSafeInt32(startingIndex++);
                        IngredientDomain.Name = reader.GetSafeString(startingIndex++);
                        IngredientDomain.MeasurementType = reader.GetSafeEnum<IngredientMeasurementType>(startingIndex++);
                        IngredientDomain.Quantity = reader.GetSafeInt32(startingIndex++);

                        RecipeDomain.Ingredients.Add(IngredientDomain);
                    }

                });

            return RecipeDomain;
        }

        // Delete recipe service
        public void Delete(int id)
        {
 
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Recipes_DeleteById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@id", id);

               });
        }


        public void Update(UpdateRecipeRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Recipes_UpdateById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@id", model.Id);
                    paramCollection.AddWithValue("@name", model.Name);
                    paramCollection.AddWithValue("@description", model.Description);
                    paramCollection.AddWithValue("@directions", model.Directions);
                    paramCollection.AddWithValue("@preptime", model.PrepTime);
                    paramCollection.AddWithValue("@totaltime", model.TotalTime);
                    paramCollection.AddWithValue("@numberofservings", model.NumberOfServings);
                    paramCollection.AddWithValue("@mediaid", model.MediaId);
                }


                );
        }
    }
}