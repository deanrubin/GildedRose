using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.BL;
using GildedRose.BL.Models;

namespace GildedRose.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");
            var inventoryUpdater = new InventoryUpdater();
            var items = InventoryInitializer.Initialize();
            await UpdateQuality(inventoryUpdater, items);

            System.Console.ReadKey();
        }

        private static async Task UpdateQuality(InventoryUpdater inventoryUpdater, IEnumerable<ItemForSell> items)
        {
            await Task.WhenAll(items.Select(inventoryUpdater.Update));
        }
    }
}
