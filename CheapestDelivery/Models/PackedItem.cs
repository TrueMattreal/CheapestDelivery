using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestDelivery.Models
{
    /// <summary>
    /// Paket mit Inhalt
    /// </summary>
    public class PackedItem
    {
        /// <summary>
        /// Pakettyp
        /// </summary>
        public Package Package { get; set; }
        /// <summary>
        /// Anzahl der Teile im Paket
        /// </summary>
        public int ContainingItems { get; set; }
    }
}
