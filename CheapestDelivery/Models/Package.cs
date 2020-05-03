using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestDelivery.Models
{
    /// <summary>
    /// Paket
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Fassungsvermögen
        /// </summary>
        public int TotalCapacity { get; set; }
        /// <summary>
        /// Kosten vom Paket
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Die verwendbare Größe, wenn mit Gegenständen gearbeitet werden, die gruppiert sind
        /// </summary>
        /// <param name="chunkSize">Gruppengröße</param>
        /// <returns>verwenbare Größe</returns>
        public int AdaptedCapacity(int chunkSize) => (TotalCapacity / chunkSize) * chunkSize;
    }
}
