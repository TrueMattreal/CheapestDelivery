using CheapestDelivery.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CheapestDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            Package[] packages = new[]
            {
                new Package()
                {
                    TotalCapacity = 200,
                    Cost = 1.55M,
                },
                new Package()
                {
                    TotalCapacity = 500,
                    Cost = 3M,
                },
                new Package()
                {
                    TotalCapacity = 4000,
                    Cost = 9M,
                },
            };
            var packer = new Packer(packages);
            IEnumerable<PackedItem> packedItems = null;
            var chunkSize = 250;
            packedItems = packer.PackItems(200, chunkSize).ToArray();
            packedItems = packer.PackItems(400, chunkSize).ToArray();
            packedItems = packer.PackItems(900, chunkSize).ToArray();
            packedItems = packer.PackItems(2000, chunkSize).ToArray();
            packedItems = packer.PackItems(4100, chunkSize).ToArray();
            packer.PackageClusters = new[]
            {
                new PackageCluster()
                {
                    ClusterSize = 4,
                    Package = packages.Last(),
                },
            };
            packedItems = packer.PackItems(4100, chunkSize).ToArray();
        }
    }
}
