using System;
using CourierApi.Models;

namespace CourierApi
{
    /// <summary>
    ///     Class used to run pricing calculations.
    /// </summary>
    public class CourierPricingEngine
    {
        /// <summary>
        ///     Use to calculate the cost of a parcel based on its size.
        /// </summary>
        /// <param name="parcelToTransport">Instance of the Parcel Class.</param>
        /// <returns>Cost</returns>
        public static decimal CalculateCost(Parcel parcelToTransport)
        {
            switch (parcelToTransport.GetSize())
            {
                case ParcelSize.Small:
                    return 3M;

                case ParcelSize.Medium:
                    return 8M;

                case ParcelSize.Large:
                    return 15M;

                case ParcelSize.ExtraLarge:
                    return 25M;
            }

            throw new ArgumentOutOfRangeException(nameof(parcelToTransport), parcelToTransport.GetSize(),
                "The parcel's size value was not valid.");
        }
    }
}