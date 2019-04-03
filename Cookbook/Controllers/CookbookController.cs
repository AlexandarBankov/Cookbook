using Cookbook.Data;
using Cookbook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Controllers
{
    class CookbookController
    {
        private CookbookContext context;

        public CookbookController()
        {
            context = new CookbookContext();
        }

        public List<Recipe> GetAllRecipes()
        {
            return context.Recipes.ToList();
        }

        public List<MealType> GetAllMealTypes()
        {
            return context.MealTypes.ToList();
        }

        public List<Tag> GetAllTags()
        {
            return context.Tags.ToList();
        }

        public Recipe GetRecipe(int id)
        {
            return context.Recipes.FirstOrDefault(r => r.Id == id);
        }

        public MealType GetMealType(int id)
        {
            return context.MealTypes.FirstOrDefault(m => m.Id == id);
        }

        public Tag GetTag(int id)
        {
            return context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public void AddRecipe(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
        }

        public void AddMealType(MealType mealType)
        {
            context.MealTypes.Add(mealType);
            context.SaveChanges();
        }

        public void AddTag(Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();
        }

        public void AddRecipeTag(RecipeTag recipeTag)
        {
            context.RecipesTags.Add(recipeTag);
            context.SaveChanges();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            Recipe item=context.Recipes.FirstOrDefault(x => x.Id == recipe.Id);
            if (item!=null)
            {
                context.Entry(item).CurrentValues.SetValues(recipe);
                context.SaveChanges();
            }
        }

        public void UpdateMealType(MealType mealType)
        {
            MealType item = context.MealTypes.FirstOrDefault(x => x.Id == mealType.Id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(mealType);
                context.SaveChanges();
            }
        }

        public void UpdateTag(Tag tag)
        {
            Tag item = context.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(tag);
                context.SaveChanges();
            }
        }

        public void DeleteRecipe(int id)
        {
            var RecipeTags = context.RecipesTags.Where(rt => rt.RecipeId == id);
            context.RecipesTags.RemoveRange(RecipeTags);
            Recipe recipe = context.Recipes.FirstOrDefault(r => r.Id == id);
            context.Recipes.Remove(recipe);
            context.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            var RecipeTags = context.RecipesTags.Where(rt => rt.TagId == id);
            context.RecipesTags.RemoveRange(RecipeTags);
            Tag tag = context.Tags.FirstOrDefault(t => t.Id == id);
            context.Tags.Remove(tag);
            context.SaveChanges();
        }

        public void DeleteMealType(int id)
        {
            MealType mealType = context.MealTypes.FirstOrDefault(m => m.Id == id);
            context.MealTypes.Remove(mealType);
            context.SaveChanges();
        }

        public void DeleteRecipeTag(int RecipeId,int TagId)
        {
            var RecipeTag = context.RecipesTags.Where(rt => rt.RecipeId == RecipeId && rt.TagId==TagId);
            context.RecipesTags.RemoveRange(RecipeTag);
            context.SaveChanges();
        }

    }
}
