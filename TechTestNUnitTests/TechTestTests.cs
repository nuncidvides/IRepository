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
        private readonly Mock<IRepository<Storeable>> _repository = new Mock<IRepository<Storeable>>();
        List<Storeable> bookInMemoryDatabase = new List<Storeable>
        {
            new Storeable() {Id = 1},
            new Storeable() {Id = 2},
            new Storeable() {Id = 3}
        };


        [SetUp]
        public void Setup()
        {
            //_repository.Setup(x => x.get)
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
            Assert.Fail();
        }

        [Test]
        public void GetAll_StoreableObjectPassed()
        {
            // Arrange
            Storeable sampleObject = new Storeable() { Id = 1 };
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
        public void GetByID_StoreableObjectPassed()
        {
            // Arrange
            Storeable sampleObject = new Storeable() { Id = 1 };
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
            Storeable result = repository.FindById(1);

            // Assert
            Assert.Equals(result, sampleObject);
        }

        [Test]
        public void Delete_StoreableObjectPassed()
        {
            // Arrange
            var storeableInMemoryDatabase = new List<Storeable>
            {
                new Storeable() {Id = 1},
                new Storeable() {Id = 2},
                new Storeable() {Id = 3}
            };

            var repository = new Mock<IRepository<Storeable>>();

            repository.Setup(x => x.FindById(It.IsAny<int>())).Returns((IComparable i) => storeableInMemoryDatabase.Single(x => x.Id == i));

            // Act
            repository.Object.Delete(1);

            repository.Verify(r => r.Delete(1));
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
