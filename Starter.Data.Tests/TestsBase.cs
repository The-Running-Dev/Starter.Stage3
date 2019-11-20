using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

using Starter.Bootstrapper;
using Starter.Data.Entities;
using Starter.Data.ViewModels;
using Starter.Framework.Clients;

namespace Starter.Data.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [SetUpFixture]
    public class TestsBase
    {
        protected List<Cat> Cats;

        protected MainViewModel ViewModel;

        protected IApiClient ApiClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Setup.Bootstrap();

            CreateCatTestData();

            var apiClient = new Mock<IApiClient>();

            // Setup the API client
            apiClient.Setup(x => x.GetAll<Cat>()).Returns(Task.FromResult(Cats.AsEnumerable()));

            apiClient.Setup(x => x.GetById<Cat>(It.IsAny<Guid>()))
                .Returns((Guid id) => { return Task.FromResult(Cats.FirstOrDefault(x => x.Id == id)); });

            apiClient.Setup(x => x.Create(It.IsAny<Cat>()))
                .Returns((Cat entity) =>
                {
                    Cats.Add(entity);

                    return Task.CompletedTask;
                });

            apiClient.Setup(x => x.Update(It.IsAny<Cat>()))
                .Returns((Cat entity) =>
                {
                    var existing = Cats.Find(x => x.Id == entity.Id);
                    
                    Cats.Remove(existing);
                    Cats.Add(entity);

                    return Task.CompletedTask;
                });

            apiClient.Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    Cats.Remove(Cats.FirstOrDefault(x => x.Id == id));
                    return Task.CompletedTask;
                });

            ApiClient = apiClient.Object;
            ViewModel = new MainViewModel(ApiClient);
        }

        protected void CreateCatTestData()
        {
            Cats = new List<Cat>
            {
                new Cat { Id = Guid.NewGuid(), Name  = "Widget", AbilityId = Ability.Eating },
                new Cat { Id = Guid.NewGuid(), Name  = "Garfield", AbilityId = Ability.Engineering },
                new Cat { Id = Guid.NewGuid(), Name  = "Mr. Boots", AbilityId = Ability.Lounging }
            };
        }
    }
}