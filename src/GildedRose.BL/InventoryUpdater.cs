using System;
using System.Collections.Generic;
using GildedRose.BL.Models;

namespace GildedRose.BL
{
    public class InventoryUpdater
    {
        private readonly IDictionary<ItemForSellType, Action<ItemForSell>> _updateFunctions = new Dictionary<ItemForSellType, Action<ItemForSell>>()
        {
            {ItemForSellType.Base, UpdateQualityOfBaseItem},
            {ItemForSellType.BackstagePasses, UpdateQualityOfBackstagePassesItem},
            {ItemForSellType.AgedBrie, UpdateQualityOfAgedBrie},
        };

        public void Update(ItemForSell itemToUpdate)
        {
            if (!_updateFunctions.TryGetValue(itemToUpdate.Type, out var func))
            {
                throw new ArgumentException($"The item with the name: {itemToUpdate.Name} and the type: {itemToUpdate.Type} is invalid");
            }

            func.Invoke(itemToUpdate);
            itemToUpdate.SellIn--;
        }

        private static void UpdateQualityOfAgedBrie(ItemForSell itemToUpdate)
        {
            itemToUpdate.Quality++;
            itemToUpdate.Quality = Math.Min(itemToUpdate.Quality, 50);
        }

        private static void UpdateQualityOfBaseItem(ItemForSell itemToUpdate)
        {
            itemToUpdate.Quality--;
            if (itemToUpdate.SellIn <= 0)
            {
                itemToUpdate.Quality--;
            }

            if (itemToUpdate.Quality < 0)
            {
                itemToUpdate.Quality = 0;
            }
        }

        private static void UpdateQualityOfBackstagePassesItem(ItemForSell itemToUpdate)
        {
            if (itemToUpdate.SellIn <= 0)
            {
                itemToUpdate.Quality = 0;
                return;
            }

            itemToUpdate.Quality++;

            if (itemToUpdate.SellIn <= 10)
            {
                itemToUpdate.Quality++;
            }

            if (itemToUpdate.SellIn <= 5)
            {
                itemToUpdate.Quality++;
            }

            itemToUpdate.Quality = Math.Min(itemToUpdate.Quality, 50);
        }
    }
}
