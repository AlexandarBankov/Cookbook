using Cookbook.Data;
using Cookbook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Controllers
{
    public class RecipeTagController
    {
        private CookbookContext context;

        public RecipeTagController()
        {
            context = new CookbookContext();
        }

        public RecipeTagController(CookbookContext context)
        {
            this.context = context;
        }
     
        public void AddRecipeTag(RecipeTag recipeTag)
        {
            context.RecipesTags.Add(recipeTag);
            context.SaveChanges();
        }

        public void DeleteRecipeTag(int RecipeId,int TagId)
        {
            var recipeTags = context.RecipesTags.Where(rt => rt.RecipeId == RecipeId && rt.TagId==TagId);
            context.RecipesTags.RemoveRange(recipeTags);
            context.SaveChanges();
        }

        public List<RecipeTag> GetAllRecipeTags()
        {
            return context.RecipesTags.ToList();
        }
    }
}
