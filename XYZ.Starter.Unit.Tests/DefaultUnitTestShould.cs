using System;
using Xunit;

namespace XYZ.Starter.Unit.Tests
{
    public class DefaultUnitTestShould
    {
        [Fact]
        public void PassAsFactForNoReason()
        {
            //Arrange

            //Act 

            //Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("Yes")]
        [InlineData("True")]
        public void PassWithThPositiveTheory(string posVal)
        {
            //Arrange

            //Act 

            //Assert
            Assert.Contains(posVal, "Yes or True");
        }
    }
}
