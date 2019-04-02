using System;
using System.Collections.Generic;
using System.Text;

namespace Cookbook.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
        public int MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

    }
}
