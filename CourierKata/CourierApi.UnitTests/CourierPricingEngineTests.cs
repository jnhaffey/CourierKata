using CourierApi.Models;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CourierApi.UnitTests
{
    public class CourierPricingEngineTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly CourierPricingEngine _courierPricingEngine;

        public CourierPricingEngineTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _courierPricingEngine = new CourierPricingEngine();
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 3.0)]
        [InlineData(9, 9, 9, 1, 3.0)]
        [InlineData(10, 9, 9, 1, 8.0)]
        [InlineData(9, 10, 9, 1, 8.0)]
        [InlineData(9, 9, 10, 1, 8.0)]
        [InlineData(10, 10, 10, 1, 8.0)]
        [InlineData(49, 49, 49, 1, 8.0)]
        [InlineData(50, 49, 49, 1, 15.0)]
        [InlineData(49, 50, 49, 1, 15.0)]
        [InlineData(49, 49, 50, 1, 15.0)]
        [InlineData(50, 50, 50, 1, 15.0)]
        [InlineData(99, 99, 99, 1, 15.0)]
        [InlineData(100, 99, 99, 1, 25.0)]
        [InlineData(99, 100, 99, 1, 25.0)]
        [InlineData(99, 99, 100, 1, 25.0)]
        [InlineData(100, 100, 100, 1, 25.0)]
        public void Test_Basic_Parcel_Cost(int height, int width, int depth, int weight, decimal expectedCost)
        {
            // ARRANGE
            var parcel = new Parcel(height, width, depth, weight);

            // ACT
            _courierPricingEngine.AddNewParcel(parcel);

            // ASSERT
            _courierPricingEngine.GetTransportResult().TotalCost.Should().Be(expectedCost);
        }

        [Fact]
        public void Test_TransportResult_Generator_With_Single_Parcel()
        {
            // ARRANGE
            var parcel = new Parcel(10, 10, 10,1);

            // ACT
            _courierPricingEngine.AddNewParcel(parcel);
            var results = _courierPricingEngine.GetTransportResult();

            // ASSERT
            results.Parcels.Should().HaveCount(1);
            results.TotalCost.Should().Be(8.0M);
            _outputHelper.WriteLine(results.ToString());
        }

        [Fact]
        public void Test_TransportResult_Generator_With_Multiple_Parcel()
        {
            // ARRANGE
            var parcel1 = new Parcel(10, 10, 10,1);
            var parcel2 = new Parcel(10, 10, 10,1);
            var parcel3 = new Parcel(10, 10, 10, 1);

            // ACT
            _courierPricingEngine.AddNewParcel(parcel1);
            _courierPricingEngine.AddNewParcel(parcel2);
            _courierPricingEngine.AddNewParcel(parcel3);
            var results = _courierPricingEngine.GetTransportResult();

            // ASSERT
            results.Parcels.Should().HaveCount(3);
            results.TotalCost.Should().Be(24.0M);
            _outputHelper.WriteLine(results.ToString());
        }

        [Fact]
        public void Test_Speedy_Option_On_Single_Parcel()
        {
            // ARRANGE
            var parcel1 = new Parcel(10, 10, 10,1, true);

            // ACT
            _courierPricingEngine.AddNewParcel(parcel1);
            var results = _courierPricingEngine.GetTransportResult();

            // ASSERT
            results.TotalCost.Should().Be(16.0M);
            _outputHelper.WriteLine(results.ToString());
        }

        [Fact]
        public void Test_Speedy_Option_On_Multiple_Parcel()
        {
            // ARRANGE
            var parcel1 = new Parcel(10, 10, 10,1, true);
            var parcel2 = new Parcel(10, 10, 10,1, true);

            // ACT
            _courierPricingEngine.AddNewParcel(parcel1);
            _courierPricingEngine.AddNewParcel(parcel2);
            var results = _courierPricingEngine.GetTransportResult();

            // ASSERT
            results.TotalCost.Should().Be(32.0M);
            _outputHelper.WriteLine(results.ToString());
        }

        [Fact]
        public void Test_Speedy_Option_On_Mixture_Parcel()
        {
            // ARRANGE
            var parcel1 = new Parcel(10, 10, 10,1, true);
            var parcel2 = new Parcel(10, 10, 10,1, false);

            // ACT
            _courierPricingEngine.AddNewParcel(parcel1);
            _courierPricingEngine.AddNewParcel(parcel2);
            var results = _courierPricingEngine.GetTransportResult();

            // ASSERT
            results.TotalCost.Should().Be(24.0M);
            _outputHelper.WriteLine(results.ToString());
        }
    }
}