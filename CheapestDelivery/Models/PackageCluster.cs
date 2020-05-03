using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestDelivery.Models
{
    /// <summary>
    /// Paketverbdung, bestehend aus meheren Paketen
    /// </summary>
    public class PackageCluster
    {
        /// <summary>
        /// Pakettyp, aus dem der Verbund besteht
        /// </summary>
        public Package Package { get; set; }
        /// <summary>
        /// Wie viele zu einen Verbund zusammengefasst werden können
        /// </summary>
        public int ClusterSize { get; set; }
    }
}
