using System;
using GildedRose.BL;
using GildedRose.BL.Models;
using Xunit;

namespace GildedRose.Tests
{
    public class InventoryUpdaterTest
    {
        private static readonly InventoryUpdater _inventoryUpdater = new InventoryUpdater(); 


        [Fact]
        public void UpdateBaseItemTest_UpdateBaseItem_QualityDecreaseByOne()
        {
            var baseItem = new ItemForSell {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20, Type = ItemForSellType.Base};
            _inventoryUpdater.Update(baseItem);

            Assert.Equal(19, baseItem.Quality);
            Assert.Equal(9, baseItem.SellIn);
        }
    }
}
