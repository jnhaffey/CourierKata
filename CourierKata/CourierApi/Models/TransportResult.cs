using System.Collections.Generic;
using System.Text;

namespace CourierApi.Models
{
    /// <summary>
    ///     Class to describe a collection of parcels being shipped.
    /// </summary>
    public class TransportResult
    {
        /// <summary>
        ///     Initialize an default instance of the Transport Result.
        /// </summary>
        public TransportResult()
        {
            Parcels = new List<Parcel>();
        }

        /// <summary>
        ///     Initialize an instance of the Transport Result.
        /// </summary>
        /// <param name="parcels">Collection of Parcels to add.</param>
        public TransportResult(List<Parcel> parcels)
            : this()
        {
            AddParcels(parcels);
        }

        /// <summary>
        ///     Initialize an instance of the Transport Result.
        /// </summary>
        /// <param name="parcels">Initial Parcel to add.</param>
        public TransportResult(Parcel parcels)
            : this()
        {
            AddParcel(parcels);
        }

        /// <summary>
        ///     List of all parcels.
        /// </summary>
        public List<Parcel> Parcels { get; }

        /// <summary>
        ///     Total cost of all parcels.
        /// </summary>
        public decimal TotalCost { get; private set; }

        /// <summary>
        ///     Add a new parcel to the collection and update the total cost.
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            Parcels.Add(parcel);
            if (parcel.IsSpeedy) TotalCost += parcel.ShippingCost * 2;
            else TotalCost += parcel.ShippingCost;
        }

        /// <summary>
        ///     Add a list of new parcels to the collection and update tht total cost.
        /// </summary>
        /// <param name="parcels"></param>
        public void AddParcels(List<Parcel> parcels)
        {
            foreach (var parcel in parcels) AddParcel(parcel);
        }

        /// <summary>
        ///     Print out the results of this Transport Result.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var results = new StringBuilder();
            foreach (var parcel in Parcels)
            {
                results.AppendLine(parcel.IsSpeedy
                    ? $"Parcel Size: {parcel.GetSize()} => Price: {parcel.ShippingCost:C2} | Speedy Enabled: {parcel.ShippingCost * 2:C2}"
                    : $"Parcel Size: {parcel.GetSize()} => Price: {parcel.ShippingCost:C2}");
            }

            results.AppendLine($"Total: {TotalCost:C2}");
            return results.ToString();
        }
    }
}