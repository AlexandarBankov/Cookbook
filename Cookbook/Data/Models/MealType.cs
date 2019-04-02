using System;
using System.Collections.Generic;
using System.Text;

namespace Cookbook.Data.Models
{
    public class MealType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
