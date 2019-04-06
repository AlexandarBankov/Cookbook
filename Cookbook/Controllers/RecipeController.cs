using Cookbook.Data;
using Cookbook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Controllers
{
    /// <summary>
    /// Provides the link between the database and the UI.
    /// </summary>
    public class RecipeController
    {
        /// <summary>
        /// Database link.
        /// </summary>
        private CookbookContext context;

        /// <summary>
        /// Creates an instance of the RecipeController class with a link to the actual database.
        /// </summary>
        public RecipeController()
        {
            context = new CookbookContext();
        }

        /// <summary>
        /// Creates an instance of the RecipeController class with an option for an in-memory-database for testing.
        /// </summary>
        /// <param name="context">context for unit testing</param>
        public RecipeController(CookbookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gives all recipes from the database. 
        /// </summary>
        /// <returns>all recipes from the database</returns>
        public List<Recipe> GetAllRecipes()
        {
            return context.Recipes.ToList();
        }

        /// <summary>
        /// Gives the recipe with the wanted Id. 
        /// </summary>
        /// <param name="id">wanted Id</param>
        /// <returns>the recipe with the wanted Id</returns>
        public Recipe GetRecipe(int id)
        {
            return context.Recipes.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Gives the recipe with the wanted name.
        /// </summary>
        /// <param name="name">wanted name</param>
        /// <returns>the recipe with the wanted name</returns>
        public List<Recipe> GetRecipeByName(string name)
        {
            return context.Recipes.Where(r => r.Name == name).ToList();
        }

        /// <summary>
        /// Adds a recipe.
        /// </summary>
        /// <param name="recipe">recipe to be added</param>
        public void AddRecipe(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes the recipe with the wanted Id.
        /// </summary>
        /// <param name="id">wanted Id</param>
        public void DeleteRecipe(int id)
        {
            var RecipeTags = context.RecipesTags.Where(rt => rt.RecipeId == id);
            context.RecipesTags.RemoveRange(RecipeTags);
            Recipe recipe = context.Recipes.FirstOrDefault(r => r.Id == id);
            context.Recipes.Remove(recipe);
            context.SaveChanges();
        }

        /// <summary>
        /// Updates a given recipe. 
        /// </summary>
        /// <param name="recipe">recipe to be updated</param>
        public void UpdateRecipe(Recipe recipe)
        {
            Recipe item = context.Recipes.FirstOrDefault(x => x.Id == recipe.Id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(recipe);
                context.SaveChanges();
            }
        }
    }
}
