using Cookbook.Data;
using Cookbook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Controllers
{
    /// <summary>
    /// Provides the link between the database and the UI.
    /// </summary>
    public class TagController
    {
        /// <summary>
        /// Database link.
        /// </summary>
        private CookbookContext context;

        /// <summary>
        /// Creates an instance of the TagController class with a link to the actual database.
        /// </summary>
        public TagController()
        {
            context = new CookbookContext();
        }

        /// <summary>
        /// Creates an instance of the TagController class with an option for an in-memory-database for testing.
        /// </summary>
        /// <param name="context">context for unit testing</param>
        public TagController(CookbookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gives all of the tags from the database.
        /// </summary>
        /// <returns>all of the tags from the database</returns>
        public List<Tag> GetAllTags()
        {
            return context.Tags.ToList();
        }

        /// <summary>
        /// Gives the tag with the wanted Id.
        /// </summary>
        /// <param name="id">wanted Id</param>
        /// <returns>the tag with the wanted Id</returns>
        public Tag GetTag(int id)
        {
            return context.Tags.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Adds a tag.
        /// </summary>
        /// <param name="tag">the added tag</param>
        public void AddTag(Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();
        }

        /// <summary>
        /// Updates the given tag.
        /// </summary>
        /// <param name="tag">the tag that is being updated</param>
        public void UpdateTag(Tag tag)
        {
            Tag item = context.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(tag);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the tag with the wanted Id.
        /// </summary>
        /// <param name="id">Id of the tag to be deleted</param>
        public void DeleteTag(int id)
        {
            var RecipeTags = context.RecipesTags.Where(rt => rt.TagId == id);
            context.RecipesTags.RemoveRange(RecipeTags);
            Tag tag = context.Tags.FirstOrDefault(t => t.Id == id);
            context.Tags.Remove(tag);
            context.SaveChanges();
        }

    }
}
