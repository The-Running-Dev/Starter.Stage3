using NUnit.Framework;
using FluentAssertions;

using Starter.Framework.Extensions;

namespace Starter.Framework.Tests.Extensions
{
    /// <summary>
    /// Tests for the ObjectExtensions class
    /// </summary>
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void ToJson_Convert_Successful()
        {
            dynamic entity = new { Id = 1, Name = "Name" };

            ((object) entity).ToJson().Should().Be("{\"Id\":1,\"Name\":\"Name\"}");
        }

        [Test]
        public void ToJson_ConvertWithFormatting_Successful()
        {
            dynamic entity = new { Id = 1, Name = "Name" };

            var json = ((object) entity).ToJson(true);
            
            json.Should().NotBeEmpty();
            json.Should().Contain("\"Id\": 1");
        }

        [Test]
        public void ToNameValueList_OnEnum_Successful()
        {

        }
    }
}