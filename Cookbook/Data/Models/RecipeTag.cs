using System;
using System.Collections.Generic;
using System.Text;

namespace Cookbook.Data.Models
{
    /// <summary>
    /// The struckture of the RecipeTags table in the database. 
    /// </summary>
    public class RecipeTag
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
