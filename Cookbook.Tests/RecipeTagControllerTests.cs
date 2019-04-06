using Cookbook.Data;
using Cookbook.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Cookbook.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace Cookbook.Tests
{

    class RecipeTagControllerTests
    {
        [TestCase]
        public void AddRecipeTag_Adds_A_Recipe_Tag()
        {
            var mockSet = new Mock<DbSet<RecipeTag>>();
            var recipeTag = new RecipeTag();
            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(m => m.RecipesTags).Returns(mockSet.Object);

            var controller = new RecipeTagController(mockContext.Object);
            controller.AddRecipeTag(recipeTag);

            mockSet.Verify(m => m.Add(It.IsAny<RecipeTag>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase]
        public void GetAllRecipeTags_Gives_All_RecipeTags()
        {
            var data = new List<RecipeTag>
            {
                new RecipeTag { RecipeId=1,TagId=1 },
                new RecipeTag { RecipeId=2,TagId=2 },
                new RecipeTag { RecipeId=3,TagId=3 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<RecipeTag>>();
            mockSet.As<IQueryable<RecipeTag>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<RecipeTag>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<RecipeTag>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<RecipeTag>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(c => c.RecipesTags).Returns(mockSet.Object);

            var controller = new RecipeTagController(mockContext.Object);
            var recipeTags = controller.GetAllRecipeTags();

            Assert.AreEqual(3, recipeTags.Count);
            Assert.AreEqual(1, recipeTags[0].RecipeId);
            Assert.AreEqual(2, recipeTags[1].RecipeId);
            Assert.AreEqual(3, recipeTags[2].RecipeId);
            Assert.AreEqual(1, recipeTags[0].TagId);
            Assert.AreEqual(2, recipeTags[1].TagId);
            Assert.AreEqual(3, recipeTags[2].TagId);
        }


    }
}
