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
        //private readonly Mock<IRepository<Storeable>> _repository = new Mock<IRepository<Storeable>>();
        private IRepository<Storeable> _repository;
        List<Storeable> dummyData = new List<Storeable>
        {
            new Storeable() {Id = 1},
            new Storeable() {Id = 2},
            new Storeable() {Id = 3}
        };


        [SetUp]
        public void Setup()
        {
            _repository = new Repository<Storeable>();

            _repository.Save(dummyData[0]);
            _repository.Save(dummyData[1]);
            _repository.Save(dummyData[2]);
        }

        /// <summary>
        /// Create a storeable object
        /// Save to repository
        /// Verify added
        /// </summary>
        [Test]
        public void Save_StoreableObjectPassed()
        {
            // Arrange
            var testId = 4;
            Storeable sampleObject = new Storeable() { Id = testId };

            // Act
            _repository.Save(sampleObject);

            // Assert
            var result = _repository.FindById(testId);
            if (result != null) 
                Assert.Pass();
            else 
                Assert.Fail();
        }

        /// <summary>
        /// Creates this instance   
        /// </summary>
        /// <returns>The ID of the new record</returns>
        /*[Test]
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
        }*/

        /// <summary>
        /// Get all from repository
        /// Assert equal to dummyData
        /// Note: I feel that I should be dumping the repo between tests
        /// to ensure that they do not interfere with each other
        /// </summary>
        [Test]
        public void GetAll()
        {
            // Arrange

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.That(result.ToList(), Is.EquivalentTo(dummyData));
        }

        /*
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
        }*/

        /// <summary>
        /// Retrieve item by ID from repository
        /// Assert is equal to item from dummyData
        /// </summary>
        [Test]
        public void GetByID()
        {
            // Arrange
            IComparable testId = 2;
            var sampleItem = dummyData.Find(x => x.Id.Equals(testId));

            // Act
            var result = _repository.FindById(2);

            // Assert
            Assert.AreEqual(sampleItem, result);
        }

        /*
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
        }*/

        /// <summary>
        /// Get an item from dummyData
        /// Remove that item from repository
        /// Assert item is not found
        /// </summary>
        [Test]
        public void Delete_StoreableObject()
        {
            // Arrange
            var testId = 3;

            // Act
            _repository.Delete(testId);

            // Assert
            var result = _repository.FindById(testId);
            if (result == null)
                Assert.Pass();
            else
                Assert.Fail();
        }

        /*
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
        }*/
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
