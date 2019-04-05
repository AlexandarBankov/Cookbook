using Cookbook.Data;
using Cookbook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Controllers
{
    class TagController
    {
        private CookbookContext context;
        public TagController()
        {
            context = new CookbookContext();
        }

        public TagController(CookbookContext context)
        {
            this.context = context;
        }

        public List<Tag> GetAllTags()
        {
            return context.Tags.ToList();
        }

        public Tag GetTag(int id)
        {
            return context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public void AddTag(Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();
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
