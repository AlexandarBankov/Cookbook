using System;
using System.Collections.Generic;
using System.Text;

namespace Cookbook.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
    }
}
