using NUnit.Framework;
using FluentAssertions;

using Starter.Framework.Extensions;

namespace Starter.Framework.Tests.Extensions
{
    /// <summary>
    /// Tests for the StringExtensions class
    /// </summary>
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void Verify_StringsAreNotEqual_Successful()
        {
            "Ben".IsEqualTo("Dan").Should().BeFalse();
        }

        [Test]
        public void Verify_StringsAreEqual_Successful()
        {
            "Ben".IsEqualTo("Ben").Should().BeTrue();
        }

        [Test]
        public void Verify_StringsWithDifferentCaseAreEqual_Successful()
        {
            "wolverine".IsEqualTo("WOLVERINE").Should().BeTrue();
        }

        [Test]
        public void Verify_StringIsNotEqualToNull_Successful()
        {
            "Dan".IsEqualTo(null).Should().BeFalse();
        }

        [Test]
        public void IsEmpty_IsFalseForNonEmptyString_Successful()
        {
            "Ben".IsEmpty().Should().BeFalse();
        }

        [Test]
        public void IsEmpty_IsTrueForForEmptyString_Successful()
        {
            "".IsEmpty().Should().BeTrue();
        }

        [Test]
        public void IsEmpty_IsTrueForWhiteSpaceIsString_Successful()
        {
            "     ".IsEmpty().Should().BeTrue();
        }

        [Test]
        public void IsNotEmpty_IsTrueForNotEmptyString_Successful()
        {
            "Ben".IsNotEmpty().Should().BeTrue();
        }

        [Test]
        public void IsNotEmpty_IsFalseForEmptyString_Successful()
        {
            "".IsNotEmpty().Should().BeFalse();
        }

        [Test]
        public void IsNotEmpty_IsFalseForWhiteSpaceString_Successful()
        {
            "     ".IsNotEmpty().Should().BeFalse();
        }
    }
}