﻿using System;
using CourierApi.Models;

namespace CourierApi
{
    /// <summary>
    ///     Class used to run pricing calculations.
    /// </summary>
    public class CourierPricingEngine
    {
        private TransportResult _transportResult;

        public CourierPricingEngine()
        {
            _transportResult = new TransportResult();
        }

        /// <summary>
        ///     Add Parcel to be transported
        /// </summary>
        /// <param name="parcelToTransport"></param>
        public void AddNewParcel(Parcel parcelToTransport)
        {
            parcelToTransport.ShippingCost = CalculateCost(parcelToTransport);
            _transportResult.AddParcel(parcelToTransport);
        }

        public void ClearResults()
        {
            _transportResult = new TransportResult();
        }

        public TransportResult GetTransportResult()
        {
            return _transportResult;
        }

        public string PrintInvoice()
        {
            return _transportResult.ToString();
        }

        private decimal CalculateCost(Parcel parcelToTransport)
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