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
    class RecipeControllerTests
    {
        [TestCase]
        public void AddRecipe_Adds_A_Recipe()
        {
            var mockSet = new Mock<DbSet<Recipe>>();
            var recipe = new Recipe();
            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

            var controller = new RecipeController(mockContext.Object);
            controller.AddRecipe(recipe);

            mockSet.Verify(m => m.Add(It.IsAny<Recipe>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase]
        public void GetAllRecipes_Gives_All_Recipes()
        {
            var data = new List<Recipe>
            {
                new Recipe { Name="First" },
                new Recipe { Name="Second" },
                new Recipe { Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Recipe>>();
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(c => c.Recipes).Returns(mockSet.Object);

            var controller = new RecipeController(mockContext.Object);
            var recipes = controller.GetAllRecipes();

            Assert.AreEqual(3, recipes.Count);
            Assert.AreEqual("First", recipes[0].Name);
            Assert.AreEqual("Second", recipes[1].Name);
            Assert.AreEqual("Third", recipes[2].Name);
        }
    }
}
