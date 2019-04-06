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
    class TagControllerTests
    {
        [TestCase]
        public void AddTag_Adds_A_Tag()
        {
            var mockSet = new Mock<DbSet<Tag>>();
            var Tag = new Tag();
            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(m => m.Tags).Returns(mockSet.Object);

            var controller = new TagController(mockContext.Object);
            controller.AddTag(Tag);

            mockSet.Verify(m => m.Add(It.IsAny<Tag>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase]
        public void GetAllTags_Gives_All_Tags()
        {
            var data = new List<Tag>
            {
                new Tag { Name="First" },
                new Tag { Name="Second" },
                new Tag { Name="Third" },
            }.AsQueryable();
            
            var mockSet = new Mock<DbSet<Tag>>();
            mockSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(c => c.Tags).Returns(mockSet.Object);

            var controller = new TagController(mockContext.Object);
            var Tags = controller.GetAllTags();

            Assert.AreEqual(3, Tags.Count);
            Assert.AreEqual("First", Tags[0].Name);
            Assert.AreEqual("Second", Tags[1].Name);
            Assert.AreEqual("Third", Tags[2].Name);
        }

        [TestCase]
        public void GetTag_Gives_Correct_Tag()
        {
            var data = new List<Tag>
            {
                new Tag { Id=1,Name="First" },
                new Tag { Id=2,Name="Second" },
                new Tag { Id=3,Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tag>>();
            mockSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CookbookContext>();
            mockContext.Setup(c => c.Tags).Returns(mockSet.Object);

            var controller = new TagController(mockContext.Object);
            var tag = controller.GetTag(1);
            
            Assert.AreEqual("First", tag.Name);
        }
    }
}
