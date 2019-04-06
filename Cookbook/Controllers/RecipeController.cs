using Cookbook.Data;
using Cookbook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Controllers
{
    public class RecipeController
    {
        private CookbookContext context;

        public RecipeController()
        {
            context = new CookbookContext();
        }

        public RecipeController(CookbookContext context)
        {
            this.context = context;
        }

        public List<Recipe> GetAllRecipes()
        {
            return context.Recipes.ToList();
        }

        public Recipe GetRecipe(int id)
        {
            return context.Recipes.FirstOrDefault(r => r.Id == id);
        }

        public List<Recipe> GetRecipeByName(string name)
        {
            return context.Recipes.Where(r => r.Name == name).ToList();
        }

        public void AddRecipe(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
        }

        public void DeleteRecipe(int id)
        {
            var RecipeTags = context.RecipesTags.Where(rt => rt.RecipeId == id);
            context.RecipesTags.RemoveRange(RecipeTags);
            Recipe recipe = context.Recipes.FirstOrDefault(r => r.Id == id);
            context.Recipes.Remove(recipe);
            context.SaveChanges();
        }

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
