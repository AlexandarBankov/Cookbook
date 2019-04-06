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
    public class MealTypeController
    {
        /// <summary>
        /// Database link.
        /// </summary>
        private CookbookContext context;

        /// <summary>
        /// Creates an instance of the MealTypeController class with a link to the actual database.
        /// </summary>
        public MealTypeController()
        {
            context = new CookbookContext();
        }

        /// <summary>
        /// Creates an instance of the MealTypeController class with an option for an in-memory-database for testing.
        /// </summary>
        /// <param name="context">context for unit testing</param>
        public MealTypeController(CookbookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gives all Meal types in the database.
        /// </summary>
        /// <returns>all meal types from the database</returns>
        public List<MealType> GetAllMealTypes()
        {
            return context.MealTypes.ToList();
        }

        /// <summary>
        /// Gives a MealType with the wanted Id.
        /// </summary>
        /// <param name="id">Id of the wanted MealType</param>
        /// <returns>a MealType with a </returns>
        public MealType GetMealType(int id)
        {
            return context.MealTypes.FirstOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Adds a MealType.
        /// </summary>
        /// <param name="mealType">the MealType that will be added</param>
        public void AddMealType(MealType mealType)
        {
            context.MealTypes.Add(mealType);
            context.SaveChanges();
        }

        /// <summary>
        /// Updates a mealtype.
        /// </summary>
        /// <param name="mealType">the MealType that will be updated</param>
        public void UpdateMealType(MealType mealType)
        {
            MealType item = context.MealTypes.FirstOrDefault(x => x.Id == mealType.Id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(mealType);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a MealType with the wanted Id.
        /// </summary>
        /// <param name="id">Id of the wanted MealType</param>
        public void DeleteMealType(int id)
        {
            MealType mealType = context.MealTypes.FirstOrDefault(m => m.Id == id);
            context.MealTypes.Remove(mealType);
            context.SaveChanges();
        }
    }
}
