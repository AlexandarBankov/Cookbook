using Cookbook.Data;
using Cookbook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Controllers
{
    public class MealTypeController
    {
        private CookbookContext context;

        public MealTypeController()
        {
            context = new CookbookContext();
        }

        public MealTypeController(CookbookContext context)
        {
            this.context = context;
        }

        public List<MealType> GetAllMealTypes()
        {
            return context.MealTypes.ToList();
        }

        public MealType GetMealType(int id)
        {
            return context.MealTypes.FirstOrDefault(m => m.Id == id);
        }

        public void AddMealType(MealType mealType)
        {
            context.MealTypes.Add(mealType);
            context.SaveChanges();
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

        public void DeleteMealType(int id)
        {
            MealType mealType = context.MealTypes.FirstOrDefault(m => m.Id == id);
            context.MealTypes.Remove(mealType);
            context.SaveChanges();
        }
    }
}
