using System;
using GildedRose.BL;
using GildedRose.BL.Models;
using Xunit;

namespace GildedRose.Tests
{
    public class InventoryUpdaterTest
    {
        private static readonly InventoryUpdater _inventoryUpdater = new InventoryUpdater();

        [Theory]
        [InlineData(20, 30)]
        [InlineData(20, 0)]
        public void UpdateBaseItemTest_UpdateBaseItem_QualityDecreaseByOne(int sellIn, int quality)
        {
            var baseItem = new ItemForSell {SellIn = sellIn, Quality = quality, Type = ItemForSellType.Base};
            _inventoryUpdater.Update(baseItem);
            var expQuality = Math.Max(quality - 1, 0);
            var expSellIn = Math.Max(sellIn - 1, 0);

            Assert.Equal(expQuality, baseItem.Quality);
            Assert.Equal(expSellIn, baseItem.SellIn);
        }

        [Theory]
        [InlineData(20, 30)]
        [InlineData(20, 50)]
        [InlineData(20, 0)]
        public void UpdateBackstagePassesItemWhenSellInHighTest_UpdateItem_QualityIncreaseByOne(int sellIn, int quality)
        {
            var backstagePassesItem = new ItemForSell { SellIn = sellIn, Quality = quality, Type = ItemForSellType.BackstagePasses };
            _inventoryUpdater.Update(backstagePassesItem);
            var expQuality = Math.Min(quality + 1, 50);
            Assert.Equal(expQuality, backstagePassesItem.Quality);
        }
    }
}
