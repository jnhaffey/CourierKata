using System;

namespace CourierApi.Models
{
    /// <summary>
    ///     Class used to describe parcel object.
    /// </summary>
    public class Parcel
    {
        private const int SmallParcelMaxSize = 10;
        private const int SmallParcelMaxWeight = 1;
        private const int MediumParcelMaxSize = 50;
        private const int MediumParcelMaxWeight = 3;
        private const int LargeParcelMaxSize = 100;
        private const int LargeParcelMaxWeight = 6;
        private const int XLargeParcelMaxWeight = 10;
        private const decimal OverageCharge = 2.0M;

        /// <summary>
        ///     Initialize an instance of a parcel.
        /// </summary>
        /// <param name="height">Value must be greater than 0.</param>
        /// <param name="width">Value must be greater than 0.</param>
        /// <param name="depth">Value must be greater than 0.</param>
        /// <param name="weight">Value must be greater than 0.</param>
        /// <param name="isSpeedy">Optional Speedy Delivery (default: false)</param>
        public Parcel(uint height, uint width, uint depth, int weight, bool isSpeedy = false)
        {
            if (height == 0) throw new ArgumentException("0 Centimeter is not a valid height.", nameof(height));
            Height = height;

            if (height == 0) throw new ArgumentException("0 Centimeter is not a valid width.", nameof(width));
            Width = width;

            if (height == 0) throw new ArgumentException("0 Centimeter is not a valid depth.", nameof(depth));
            Depth = depth;

            if (weight <= 0)
                throw new ArgumentOutOfRangeException(nameof(weight), weight, "Weight must be more than 0.");
            Weight = weight;

            IsSpeedy = isSpeedy;
        }

        /// <summary>
        ///     Initialize an instance of a parcel.
        /// </summary>
        /// <param name="height">Value must be greater than 0.</param>
        /// <param name="width">Value must be greater than 0.</param>
        /// <param name="depth">Value must be greater than 0.</param>
        /// <param name="weight">Value must be greater than 0.</param>
        /// <param name="isSpeedy">Optional Speedy Delivery (default: false)</param>
        public Parcel(int height, int width, int depth, int weight, bool isSpeedy = false)
            : this((uint) height, (uint) width, (uint) depth, weight, isSpeedy)
        {
        }

        /// <summary>
        ///     Parcel's Height in Centimeters.
        /// </summary>
        public uint Height { get; }

        /// <summary>
        ///     Parcel's Width in Centimeters.
        /// </summary>
        public uint Width { get; }

        /// <summary>
        ///     Parcel's Depth in Centimeters.
        /// </summary>
        public uint Depth { get; }

        /// <summary>
        ///     The Parcel's weight in Kilograms.
        /// </summary>
        public int Weight { get; }

        /// <summary>
        ///     Flag to indicate if parcel has Speedy Delivery enabled.
        /// </summary>
        public bool IsSpeedy { get; }

        /// <summary>
        ///     The calculated cost to ship this parcel.
        /// </summary>
        public decimal ShippingCost { get; set; }

        /// <summary>
        ///     Get the Parcel's Size based on the Courier's Rules.
        /// </summary>
        /// <returns>Size Value</returns>
        public ParcelSize GetSize()
        {
            if (Height < SmallParcelMaxSize
                && Width < SmallParcelMaxSize
                && Depth < SmallParcelMaxSize)
                return ParcelSize.Small;

            if (Height < MediumParcelMaxSize
                && Width < MediumParcelMaxSize
                && Depth < MediumParcelMaxSize)
                return ParcelSize.Medium;

            if (Height < LargeParcelMaxSize
                && Width < LargeParcelMaxSize
                && Depth < LargeParcelMaxSize)
                return ParcelSize.Large;

            return ParcelSize.ExtraLarge;
        }

        /// <summary>
        ///     Flag to indicate if parcel is over its max allowed weight limit.
        /// </summary>
        /// <returns></returns>
        public bool IsOverWeightLimit()
        {
            return GetOverWeightCharges() > 0;
        }

        /// <summary>
        ///     Get the charges for exceeding the weight limit.
        /// </summary>
        /// <returns></returns>
        public decimal GetOverWeightCharges()
        {
            var totalOverage = 0;
            switch (GetSize())
            {
                case ParcelSize.Small:
                    totalOverage = Weight - SmallParcelMaxWeight;
                    break;

                case ParcelSize.Medium:
                    totalOverage = Weight - MediumParcelMaxWeight;
                    break;

                case ParcelSize.Large:
                    totalOverage = Weight - LargeParcelMaxWeight;
                    break;

                case ParcelSize.ExtraLarge:
                    totalOverage = Weight - XLargeParcelMaxWeight;
                    break;
            }

            return totalOverage * OverageCharge;
        }
    }
}