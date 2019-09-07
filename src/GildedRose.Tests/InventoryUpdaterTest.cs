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
        public void UpdateNormalItemTest_UpdateNormalItem_QualityDecreaseByOne(int sellIn, int quality)
        {
            var item = new ItemForSell {SellIn = sellIn, Quality = quality, Type = ItemForSellType.Normal};
            _inventoryUpdater.Update(item);
            var expQuality = Math.Max(quality - 1, 0);
            var expSellIn = Math.Max(sellIn - 1, 0);

            Assert.Equal(expQuality, item.Quality);
            Assert.Equal(expSellIn, item.SellIn);
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

        [Theory]
        [InlineData(10, 30)]
        [InlineData(9, 50)]
        [InlineData(6, 0)]
        public void UpdateBackstagePassesItemWhenSellInMediumTest_UpdateItem_QualityIncreaseByTwo(int sellIn, int quality)
        {
            var backstagePassesItem = new ItemForSell { SellIn = sellIn, Quality = quality, Type = ItemForSellType.BackstagePasses };
            _inventoryUpdater.Update(backstagePassesItem);
            var expQuality = Math.Min(quality + 2, 50);
            Assert.Equal(expQuality, backstagePassesItem.Quality);
        }

        [Theory]
        [InlineData(5, 30)]
        [InlineData(4, 50)]
        [InlineData(1, 0)]
        public void UpdateBackstagePassesItemWhenSellInLowTest_UpdateItem_QualityIncreaseByThree(int sellIn, int quality)
        {
            var backstagePassesItem = new ItemForSell { SellIn = sellIn, Quality = quality, Type = ItemForSellType.BackstagePasses };
            _inventoryUpdater.Update(backstagePassesItem);
            var expQuality = Math.Min(quality + 3, 50);
            Assert.Equal(expQuality, backstagePassesItem.Quality);
        }

        [Theory]
        [InlineData(10, 30)]
        [InlineData(5, 50)]
        [InlineData(5, 30)]
        [InlineData(10, 0)]
        [InlineData(5, 0)]
        [InlineData(20, 0)]
        public void UpdateAgedBrieItem_UpdateItem_QualityIncreaseByOne(int sellIn, int quality)
        {
            var item = new ItemForSell { SellIn = sellIn, Quality = quality, Type = ItemForSellType.AgedBrie };
            _inventoryUpdater.Update(item);
            var expQuality = Math.Min(quality + 1, 50);
            Assert.Equal(expQuality, item.Quality);
        }

        [Theory]
        [InlineData(10, 30)]
        [InlineData(9, 50)]
        [InlineData(6, 0)]
        public void UpdateLegendaryItemTest_UpdateItem_QualityRemainTheSame(int sellIn, int quality)
        {
            var item = new ItemForSell { SellIn = sellIn, Quality = quality, Type = ItemForSellType.Legendary };
            _inventoryUpdater.Update(item);
            Assert.Equal(quality, item.Quality);
        }

        [Theory]
        [InlineData(20, 30)]
        [InlineData(20, 0)]
        public void UpdateConjuredItemTest_UpdateConjuredItem_QualityDecreaseByTwo(int sellIn, int quality)
        {
            var item = new ItemForSell { SellIn = sellIn, Quality = quality, Type = ItemForSellType.Conjured };
            _inventoryUpdater.Update(item);
            var expQuality = Math.Max(quality - 2, 0);
            var expSellIn = Math.Max(sellIn - 1, 0);

            Assert.Equal(expQuality, item.Quality);
            Assert.Equal(expSellIn, item.SellIn);
        }
    }
}
