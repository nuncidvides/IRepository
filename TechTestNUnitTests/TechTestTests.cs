using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using TechTest;

namespace TechTestNUnitTests
{
    public class TechTestTests
    {
        IRepository<IStoreable> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = (IRepository<IStoreable>)RepositoryFactory.Instance("SampleRepository");
        }

        /// <summary>
        /// Creates this instance
        /// </summary>
        /// <returns>The ID of the new record</returns>
        [Test]
        public void Add_StoreableObjectPassed()
        {
            // Arrange
            Storeable sampleObject = new Storeable();

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<Storeable>>();
            context.Setup(x => x.Set<Storeable>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<Storeable>())).Returns(sampleObject);

            // Act
            var repository = new Repository<Storeable>(context.Object);
            repository.Save(sampleObject);

            // Assert
            context.Verify(x => x.Set<Storeable>());
            dbSetMock.Verify(x => x.Add(It.Is<Storeable>(y => y == sampleObject)));
        }

        [Test]
        public void GetAll_StoreableObjectPassed()
        {
            // Arrange
            Storeable sampleObject = new Storeable() { Id = "1" };
            var sampleList = new List<Storeable>() { sampleObject };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<Storeable>>();
            dbSetMock.As<IQueryable<Storeable>>().Setup(x => x.Provider).Returns(sampleList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Storeable>>().Setup(x => x.Expression).Returns(sampleList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Storeable>>().Setup(x => x.ElementType).Returns(sampleList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Storeable>>().Setup(x => x.GetEnumerator()).Returns(sampleList.AsQueryable().GetEnumerator());

            context.Setup(x => x.Set<Storeable>()).Returns(dbSetMock.Object);

            // Act
            var repository = new Repository<Storeable>(context.Object);
            var result = repository.GetAll();

            // Assert
            Assert.That(result.ToList(), Is.EquivalentTo(sampleList));
        }

        [Test]
        public void GetByID()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void Delete()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    internal class RepositoryFactory
    {
        internal static object Instance(string instance)
        {
            string targetAssembly = ConfigurationManager.AppSettings["targetAssembly"];
            return Activator.CreateInstance(targetAssembly, targetAssembly + "." + instance).Unwrap();
        }
    }
}
