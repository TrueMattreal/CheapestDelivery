using CheapestDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheapestDelivery
{
    /// <summary>
    /// Verpackt Gegenstände in Pakete
    /// </summary>
    public class Packer
    {
        private readonly Package[] packages;

        /// <summary>
        /// Erstellt ein Verpacket
        /// </summary>
        /// <param name="packages">mögliche Pakete</param>
        public Packer(IEnumerable<Package> packages)
        {
            this.packages = packages?.ToArray() ?? throw new ArgumentNullException(nameof(packages));
            this.packages = this.packages.OrderBy(p => p.Cost).ToArray();
        }
        /// <summary>
        /// Wenn gesetzt, dann wird ein Paketverbund bevorzugt, wenn der Typ vom Paket bereits verwendet wurde.
        /// Funktioniert nur bei der größten Veriante gut
        /// </summary>
        public IEnumerable<PackageCluster> PackageClusters { get; set; }

        /// <summary>
        /// Verpackt Gegenstände
        /// </summary>
        /// <param name="itemCount">Anzal der Gegenstände</param>
        /// <param name="chunkSize">Gruppengröße der Gegenstände</param>
        /// <returns>Anzahl der gepackten Pakete</returns>
        public IEnumerable<PackedItem> PackItems(int itemCount, int chunkSize)
        {
            var packedItems = new List<PackedItem>();
            while (itemCount > 0)
            {
                Package package = null;
                for (var i = 0; package == null && i < packages.Length; i++)
                {
                    package = GetPackageToFillCluster(packedItems);
                    // Wenn gefunden, dann brich die Suche ab
                    if (package != null)
                    {
                        break;
                    }
                    // ist letztes Item?
                    if (i >= packages.Length - 1)
                    {
                        package = packages[i];
                        break;
                    }
                    // Passt das aktuelle Paket?
                    if (packages[i].AdaptedCapacity(chunkSize) >= itemCount)
                    {
                        package = packages[i];
                        break;
                    }
                    // Wie oft geht das kleine, bis das größere günstiger ist?
                    for (var j = 2; package == null && packages[i].Cost * j < packages[i + 1].Cost; j++)
                    {
                        if (packages[i].AdaptedCapacity(chunkSize) * j >= itemCount)
                        {
                            // markiere nur ein Paket. beim nächsten Durchlauf wird er wieder hierherkommen
                            // oder sogar noch ein kleineres/günstiges finden
                            package = packages[i];
                        }
                    }
                }
                // Füge Paket der Liste hinzu und entferne die verpackten Gegenstände
                packedItems.Add(new PackedItem
                {
                    ContainingItems = Math.Min(package.AdaptedCapacity(chunkSize), itemCount),
                    Package = package,
                });
                itemCount -= package.AdaptedCapacity(chunkSize);
            }
            return packedItems;
        }

        /// <summary>
        /// Findet ein Paket heraus, falls ein Verbund noch nicht komplett gefüllt ist
        /// </summary>
        /// <param name="packedItems">Liste der gepackten Pakete</param>
        /// <returns>ein Paket, welches in ein Verbund passt. Ansonsten null</returns>
        private Package GetPackageToFillCluster(List<PackedItem> packedItems)
        {
            // Kein Verbdund definiert, also nichts zum füllen
            if (PackageClusters == null)
            {
                return null;
            }
            foreach (var packageCluster in PackageClusters)
            {
                // Wenn im Paketverbund noch Platz ist, dann nehme diesen Typ
                var packagedsFromCluster = packedItems.Count(pi => pi.Package == packageCluster.Package);
                if (packagedsFromCluster % packageCluster.ClusterSize != 0)
                {
                    return packageCluster.Package;
                }
            }

            return null;
        }
    }
}
