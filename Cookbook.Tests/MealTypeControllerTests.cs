using Cookbook.Controllers;
using Cookbook.Data;
using Cookbook.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Tests
{
    class MealTypeControllerTests
    {
        [TestCase]
        public void AddMealType_Adds_A_Meal_Type()
        {
            var mockSet = new Mock<DbSet<MealType>>();
            var mealType = new MealType();
            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(m => m.MealTypes).Returns(mockSet.Object);

            var controller = new MealTypeController(mockContext.Object);
            controller.AddMealType(mealType);

            mockSet.Verify(m => m.Add(It.IsAny<MealType>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase]
        public void GetAllMealTypes_Gives_All_MealTypes()
        {
            var data = new List<MealType>
            {
                new MealType { Name="First" },
                new MealType { Name="Second" },
                new MealType { Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<MealType>>();
            mockSet.As<IQueryable<MealType>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<MealType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<MealType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<MealType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(c => c.MealTypes).Returns(mockSet.Object);

            var controller = new MealTypeController(mockContext.Object);
            var mealTypes = controller.GetAllMealTypes();

            Assert.AreEqual(3, mealTypes.Count);
            Assert.AreEqual("First", mealTypes[0].Name);
            Assert.AreEqual("Second", mealTypes[1].Name);
            Assert.AreEqual("Third", mealTypes[2].Name);
        }

        [TestCase]
        public void GetAllMealType_Gives_Correct_MealType()
        {
            var data = new List<MealType>
            {
                new MealType { Id=1, Name="First" },
                new MealType { Id=2, Name="Second" },
                new MealType { Id=3, Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<MealType>>();
            mockSet.As<IQueryable<MealType>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<MealType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<MealType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<MealType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(c => c.MealTypes).Returns(mockSet.Object);

            var controller = new MealTypeController(mockContext.Object);
            var mealType = controller.GetMealType(1);
            
            Assert.AreEqual("First", mealType.Name);
        }
    }
}
