using CourierApi.Models;
using FluentAssertions;
using Xunit;

namespace CourierApi.UnitTests
{
    public class CourierTests
    {
        [Theory]
        [InlineData(1, 1, 1, ParcelSize.Small)]
        [InlineData(9, 9, 9, ParcelSize.Small)]
        [InlineData(10, 9, 9, ParcelSize.Medium)]
        [InlineData(9, 10, 9, ParcelSize.Medium)]
        [InlineData(9, 9, 10, ParcelSize.Medium)]
        [InlineData(10, 10, 10, ParcelSize.Medium)]
        [InlineData(49, 49, 49, ParcelSize.Medium)]
        [InlineData(50, 49, 49, ParcelSize.Large)]
        [InlineData(49, 50, 49, ParcelSize.Large)]
        [InlineData(49, 49, 50, ParcelSize.Large)]
        [InlineData(50, 50, 50, ParcelSize.Large)]
        [InlineData(99, 99, 99, ParcelSize.Large)]
        [InlineData(100, 99, 99, ParcelSize.ExtraLarge)]
        [InlineData(99, 100, 99, ParcelSize.ExtraLarge)]
        [InlineData(99, 99, 100, ParcelSize.ExtraLarge)]
        [InlineData(100, 100, 100, ParcelSize.ExtraLarge)]
        public void Test_Basic_Parcel_Size(int height, int width, int depth, ParcelSize expectedSize)
        {
            // ARRANGE
            // ACT
            var parcel = new Parcel(height, width, depth);

            // ASSERT
            parcel.GetSize().Should().Be(expectedSize);
        }

        [Theory]
        [InlineData(1, 1, 1, 3.0)]
        [InlineData(9, 9, 9, 3.0)]
        [InlineData(10, 9, 9, 8.0)]
        [InlineData(9, 10, 9, 8.0)]
        [InlineData(9, 9, 10, 8.0)]
        [InlineData(10, 10, 10, 8.0)]
        [InlineData(49, 49, 49, 8.0)]
        [InlineData(50, 49, 49, 15.0)]
        [InlineData(49, 50, 49, 15.0)]
        [InlineData(49, 49, 50, 15.0)]
        [InlineData(50, 50, 50, 15.0)]
        [InlineData(99, 99, 99, 15.0)]
        [InlineData(100, 99, 99, 25.0)]
        [InlineData(99, 100, 99, 25.0)]
        [InlineData(99, 99, 100, 25.0)]
        [InlineData(100, 100, 100, 25.0)]
        public void Test_Basic_Parcel_Cost(int height, int width, int depth, decimal expectedCost)
        {
            // ARRANGE
            var parcel = new Parcel(height, width, depth);

            // ACT
            var actualCost = CourierPricingEngine.CalculateCost(parcel);

            // ASSERT
            actualCost.Should().Be(expectedCost);
        }
    }
}