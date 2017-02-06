using Amazon.Runtime.Internal;
using Microsoft.Practices.Unity;
using Sabio.Web.Domain.Recipes;
using Sabio.Web.Exceptions;
using Sabio.Web.Models.Requests.Recipes;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/recipes")]
    public class RecipesApiController : ApiController
    {
        [Dependency]
        public IRecipesService _RecipesService { get; set; }

        [Dependency]
        public IIngredientsService _IngredientsService { get; set; }

        [Route("post"), HttpPost]
        public HttpResponseMessage PostRecipe(CreateRecipeRequest model)
        {
            ItemResponse<int> ItemResponse = new ItemResponse<int>();

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            // if model valid, then we will pass this into the recipe service interface
            try
            {
                model.UserId = UserService.GetCurrentUserId();

                int RecipeId = _RecipesService.InsertRecipe(model);

                _IngredientsService.InsertIngredient(model.Ingredients, RecipeId);

                ItemResponse.Item = RecipeId;

            }

            // Display error code and message if user was not created
            catch (IdentityResultException) 
            {

                var ExceptionError = new Models.Responses.ErrorResponse("Identity Result Exception Detected");

                return Request.CreateResponse(HttpStatusCode.BadRequest, ExceptionError);

            }


            return Request.CreateResponse(HttpStatusCode.OK, ItemResponse);
        }

        [Route("getcurrentrecipes"), HttpGet]
        public HttpResponseMessage GetCurrentRecipes()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            string UserId = UserService.GetCurrentUserId();

            List<RecipeDomain> Recipes = _RecipesService.GetRecipesByUserId(UserId);

            ItemsResponse<RecipeDomain> Response = new ItemsResponse<RecipeDomain>();

            Response.Items = Recipes;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        [Route("getallrecipes"), HttpGet]
        public HttpResponseMessage GetAllRecipes()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<RecipeDomain> Recipes = _RecipesService.GetRecipesAll();

            ItemsResponse<RecipeDomain> Response = new ItemsResponse<RecipeDomain>();

            Response.Items = Recipes;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        [Route("getbyrecipeid/{recipeid:int}"), HttpGet]
        public HttpResponseMessage GetRecipeByRecipeId(int recipeid)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            RecipeDomain Recipe = _RecipesService.GetRecipeByRecipeId(recipeid);

            ItemResponse<RecipeDomain> Response = new ItemResponse<RecipeDomain>();

            Response.Item = Recipe;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        [Route("delete/{recipeid:int}"), HttpDelete]
        public HttpResponseMessage DeleteRecipe(int recipeid)
        {

            _RecipesService.Delete(recipeid);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("update/{recipeid:int}"), HttpPut]
        public HttpResponseMessage UpdateRecipe(UpdateRecipeRequest model, int recipeid)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.Id = recipeid;

            //Update Recipe
            _RecipesService.Update(model);

            // Insert new ingredients in recipe edit mode
            if (model.Ingredients != null)
            {
                _IngredientsService.InsertIngredient(model.Ingredients, model.Id);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("ingredient/update/{ingredientid:int}"), HttpPut]
        public HttpResponseMessage UpdateIngredient(UpdateIngredientRequest model, int ingredientid)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.Id = ingredientid;

            //Update Recipe
            _IngredientsService.UpdateIngredient(model);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("ingredient/delete/{ingredientid:int}"), HttpDelete]
        public HttpResponseMessage DeleteIngredient(int ingredientid)
        {

            _IngredientsService.DeleteIngredient(ingredientid);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
