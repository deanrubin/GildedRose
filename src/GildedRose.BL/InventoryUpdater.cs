using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GildedRose.BL.Models;

namespace GildedRose.BL
{
    public class InventoryUpdater
    {
        private readonly IDictionary<ItemForSellType, Action<ItemForSell>> _updateQualityActions = new Dictionary<ItemForSellType, Action<ItemForSell>>()
        {
            {ItemForSellType.Normal, UpdateQualityOfNormalItem},
            {ItemForSellType.BackstagePasses, UpdateQualityOfBackstagePassesItem},
            {ItemForSellType.AgedBrie, UpdateQualityOfAgedBrieItem},
            {ItemForSellType.Legendary, x => {}},
            {ItemForSellType.Conjured, UpdateQualityOfConjuredItem},
        };

        public Task Update(ItemForSell itemToUpdate)
        {
            if (!_updateQualityActions.TryGetValue(itemToUpdate.Type, out var action))
            {
                throw new ArgumentException($"The item with the name: {itemToUpdate.Name} and the type: {itemToUpdate.Type} is invalid");
            }

            action.Invoke(itemToUpdate);
            UpdateSellIn(itemToUpdate);
            return Task.CompletedTask;
        }

        private static void UpdateSellIn(Item itemToUpdate)
        {
            itemToUpdate.SellIn--;
            if (itemToUpdate.SellIn < 0)
            {
                itemToUpdate.SellIn = 0;
            }
        }

        private static void UpdateQualityOfAgedBrieItem(ItemForSell itemToUpdate)
        {
            if (itemToUpdate.Type != ItemForSellType.AgedBrie)
            {
                throw new ArgumentException("The function: UpdateQualityOfAgedBrieItem can only accept ItemForSell from type: AgedBrie");
            }

            itemToUpdate.Quality++;
            itemToUpdate.Quality = Math.Min(itemToUpdate.Quality, 50);
        }

        private static void UpdateQualityOfConjuredItem(ItemForSell itemToUpdate)
        {
            if (itemToUpdate.Type != ItemForSellType.Conjured)
            {
                throw new ArgumentException("The function: UpdateQualityOfConjuredItem can only accept ItemForSell from type: Conjured");
            }

            UpdateQualityByValue(itemToUpdate, 2);
        }

        private static void UpdateQualityOfNormalItem(ItemForSell itemToUpdate)
        {
            if (itemToUpdate.Type != ItemForSellType.Normal)
            {
                throw new ArgumentException("The function: UpdateQualityOfNormalItem can only accept ItemForSell from type: Normal");
            }

            UpdateQualityByValue(itemToUpdate, 1);
        }

        private static void UpdateQualityOfBackstagePassesItem(ItemForSell itemToUpdate) 
        {
            if (itemToUpdate.Type != ItemForSellType.BackstagePasses)
            {
                throw new ArgumentException("The function: UpdateQualityOfBackstagePassesItem can only accept ItemForSell from type: BackstagePasses");
            }

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

        private static void UpdateQualityByValue(Item itemToUpdate, int value)
        {
            itemToUpdate.Quality = itemToUpdate.Quality - value;
            if (itemToUpdate.SellIn <= 0)
            {
                itemToUpdate.Quality = itemToUpdate.Quality - value;
            }

            if (itemToUpdate.Quality < 0)
            {
                itemToUpdate.Quality = 0;
            }
        }
    }
}
