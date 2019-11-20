using NUnit.Framework;
using FluentAssertions;

using Starter.Data.Entities;
using Starter.Framework.Extensions;

namespace Starter.Framework.Tests.Extensions
{
    /// <summary>
    /// Tests for the EnumExtensions class
    /// </summary>
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void GetDescription_OnEnum_Successful()
        {
            (Ability.Eating).GetDescription().Should().Be("Eating");
        }
    }
}