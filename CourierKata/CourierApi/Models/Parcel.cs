using System;

namespace CourierApi.Models
{
    /// <summary>
    ///     Class used to describe parcel object.
    /// </summary>
    public class Parcel
    {
        private const int SmallParcelMaxSize = 10;
        private const int MediumParcelMaxSize = 50;
        private const int LargeParcelMaxSize = 100;

        /// <summary>
        ///     Initialize an instance of a parcel.
        /// </summary>
        /// <param name="height">Value must be greater than 0.</param>
        /// <param name="width">Value must be greater than 0.</param>
        /// <param name="depth">Value must be greater than 0.</param>
        /// <param name="isSpeedy">Optional Speedy Delivery (default: false)</param>
        public Parcel(uint height, uint width, uint depth, bool isSpeedy = false)
        {
            if (height == 0) throw new ArgumentException("0 Centimeter is not a valid height.", nameof(height));
            Height = height;

            if (height == 0) throw new ArgumentException("0 Centimeter is not a valid width.", nameof(width));
            Width = width;

            if (height == 0) throw new ArgumentException("0 Centimeter is not a valid depth.", nameof(depth));
            Depth = depth;

            IsSpeedy = isSpeedy;
        }

        /// <summary>
        ///     Initialize an instance of a parcel.
        /// </summary>
        /// <param name="height">Value must be greater than 0.</param>
        /// <param name="width">Value must be greater than 0.</param>
        /// <param name="depth">Value must be greater than 0.</param>
        /// <param name="isSpeedy">Optional Speedy Delivery (default: false)</param>
        public Parcel(int height, int width, int depth, bool isSpeedy = false)
            : this((uint) height, (uint) width, (uint) depth, isSpeedy)
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
    }
}