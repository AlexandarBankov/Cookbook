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
    public class RecipeTagController
    {
        /// <summary>
        /// Database link.
        /// </summary>
        private CookbookContext context;

        /// <summary>
        /// Creates an instance of the RecipeTagController class with a link to the actual database.
        /// </summary>
        public RecipeTagController()
        {
            context = new CookbookContext();
        }

        /// <summary>
        /// Creates an instance of the RecipeTagController class with an option for an in-memory-database for testing.
        /// </summary>
        /// <param name="context">context for unit testing</param>
        public RecipeTagController(CookbookContext context)
        {
            this.context = context;
        }
     
        /// <summary>
        /// Adds a recipe tag.
        /// </summary>
        /// <param name="recipeTag">the added recipe tag</param>
        public void AddRecipeTag(RecipeTag recipeTag)
        {
            context.RecipesTags.Add(recipeTag);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes a recipe tag pair from the database.
        /// </summary>
        /// <param name="RecipeId">recipe id</param>
        /// <param name="TagId">tag id</param>
        public void DeleteRecipeTag(int RecipeId,int TagId)
        {
            var recipeTags = context.RecipesTags.Where(rt => rt.RecipeId == RecipeId && rt.TagId==TagId);
            context.RecipesTags.RemoveRange(recipeTags);
            context.SaveChanges();
        }

        /// <summary>
        /// Gives all recipe tags.
        /// </summary>
        /// <returns>a list of all recipe tags</returns>
        public List<RecipeTag> GetAllRecipeTags()
        {
            return context.RecipesTags.ToList();
        }
    }
}
