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

        private static void UpdateQualityOfBaseItem(ItemForSell itemToUpdate)
        {
            itemToUpdate.Quality--;
            UpdateQualityOfExpired(itemToUpdate);
        }

        private static void UpdateQualityOfExpired(ItemForSell itemToUpdate)
        {
            if (itemToUpdate.SellIn <= 0 || itemToUpdate.Quality < 0)
            {
                itemToUpdate.Quality = 0;
            }
        }
    }
}
