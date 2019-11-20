using System;
using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;
using FluentAssertions;

using Starter.Data.Entities;

namespace Starter.API.Tests.Controllers
{
    ///
    [TestFixture]
    public class CatControllerTests : TestsBase
    {
        [Test]
        public async Task GetAll_Cats_Successful()
        {
            var entities = await CatController.GetAll();
            var enumerable = entities as Cat[] ?? entities.ToArray();

            enumerable.Count().Should().Be(Cats.Count);
        }

        [Test]
        public async Task GetCatById_ForCatId_Successful()
        {
            var lastCat = Cats.LastOrDefault();
            var cat = await CatController.GetById(lastCat.Id);

            cat.Id.Should().Be(lastCat.Id);
        }

        [Test]
        public async Task Create_Cat_Successful()
        {
            var cat = new Cat() { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };

            await CatController.Post(cat);

            Cats.FirstOrDefault(x => x.Id == cat.Id).Should().BeEquivalentTo(cat);
        }

        [Test]
        public async Task Update_Cat_Successful()
        {
            var cat = Cats.FirstOrDefault();
            var newName = Guid.NewGuid().ToString();

            cat.Name = newName;

            await CatController.Put(cat);

            Cats.FirstOrDefault(x => x.Name == newName).Should().NotBeNull();
        }

        [Test]
        public async Task Delete_Cat_Successful()
        {
            var cat = Cats.FirstOrDefault();

            await CatController.Delete(cat.Id);

            Cats.FirstOrDefault(x => x.Id == cat.Id).Should().BeNull();
        }
    }
}
