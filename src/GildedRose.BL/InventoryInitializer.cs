using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.BL.Models;

namespace GildedRose.BL
{
    public static class InventoryInitializer
    {
        public static IEnumerable<ItemForSell> Initialize()
        {
            return new List<ItemForSell>
            {

                new ItemForSell {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20, Type = ItemForSellType.Normal},
                new ItemForSell {Name = "Aged Brie", SellIn = 2, Quality = 0, Type = ItemForSellType.AgedBrie},
                new ItemForSell {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7, Type = ItemForSellType.Normal},
                new ItemForSell {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80, Type = ItemForSellType.Legendary},
                new ItemForSell
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20,
                    Type = ItemForSellType.BackstagePasses
                },
                new ItemForSell {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6, Type = ItemForSellType.Conjured}
            };
        }
    }
}
