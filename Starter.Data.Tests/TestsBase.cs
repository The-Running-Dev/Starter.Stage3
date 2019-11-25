using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

using Starter.Bootstrapper;
using Starter.Data.Entities;
using Starter.Data.Services;
using Starter.Data.ViewModels;
using Starter.Framework.Clients;
using Starter.Framework.Services;

namespace Starter.Data.Tests
{
    /// <summary>
    /// Base class for the Starter.Data.Tests project
    /// </summary>
    [SetUpFixture]
    public class TestsBase
    {
        protected List<Cat> Cats;

        protected MainViewModel ViewModel;

        protected IApiClient ApiClient;

        protected IServiceBus ServiceBus;

        protected ICatService CatService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Setup.Bootstrap();

            CreateCatTestData();

            var mockApiClient = new Mock<IApiClient>();
            var mockServiceBus = new Mock<IServiceBus>();
            var mockCatService = new Mock<ICatService>();

            // Setup the cat service
            mockCatService.Setup(x => x.GetAll()).Returns(Task.FromResult(Cats.AsEnumerable()));

            mockCatService.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => { return Task.FromResult(Cats.FirstOrDefault(x => x.Id == id)); });

            mockCatService.Setup(x => x.Create(It.IsAny<Cat>()))
                .Returns((Cat entity) =>
                {
                    Cats.Add(entity);

                    return Task.CompletedTask;
                });

            mockCatService.Setup(x => x.Update(It.IsAny<Cat>()))
                .Returns((Cat entity) =>
                {
                    var existing = Cats.Find(x => x.Id == entity.Id);

                    Cats.Remove(existing);
                    Cats.Add(entity);

                    return Task.CompletedTask;
                });

            mockCatService.Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    Cats.Remove(Cats.FirstOrDefault(x => x.Id == id));
                    return Task.CompletedTask;
                });

            ApiClient = mockApiClient.Object;
            ServiceBus = mockServiceBus.Object;
            CatService = mockCatService.Object;

            ViewModel = new MainViewModel(CatService);
        }

        protected void CreateCatTestData()
        {
            Cats = new List<Cat>
            {
                new Cat
                {
                    Id = Guid.NewGuid(), SecondaryId = Guid.NewGuid(), Name = "Widget", AbilityId = Ability.Eating
                },
                new Cat
                {
                    Id = Guid.NewGuid(), SecondaryId = Guid.NewGuid(), Name = "Garfield",
                    AbilityId = Ability.Engineering
                },
                new Cat
                {
                    Id = Guid.NewGuid(), SecondaryId = Guid.NewGuid(), Name = "Mr. Boots", AbilityId = Ability.Lounging
                }
            };
        }
    }
}