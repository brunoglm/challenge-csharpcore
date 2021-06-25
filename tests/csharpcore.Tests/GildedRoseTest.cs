using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class GildedRoseTest
    {
        [Theory(DisplayName = "Sell date item has passed")]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Category", "GildedRose")]
        public void UpdateQuality_SellDateItemPassed_QualityShouldDecrementTwiceAsFast(int sellIn)
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Item test", SellIn = sellIn, Quality = 10 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(8, Items.First().Quality);
        }

        [Theory(DisplayName = "Negative quality test")]
        [InlineData(10)]
        [InlineData(5)]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Category", "GildedRose")]
        public void UpdateQuality_ItemWithQualityZero_ShouldNotTurnNegative(int sellIn)
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Item test", SellIn = sellIn, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(0, Items.First().Quality);
        }

        [Fact(DisplayName = "Item Aged Brie test increment")]
        [Trait("Category", "GildedRose")]
        public void UpdateQuality_WhenItemAgedBrie_ShouldIncrementOneQuantityQuality()
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 5 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(9, Items.First().SellIn);
            Assert.Equal(6, Items.First().Quality);
        }

        [Theory(DisplayName = "Item Aged Brie test quality higher than maximum")]
        [InlineData(49)]
        [InlineData(50)]
        [InlineData(51)]
        [Trait("Category", "GildedRose")]
        public void UpdateQuality_WhenAgedBrieItem_QualityCannotGreaterThanMaximumNumber(int quality)
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = quality } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(50, Items.First().Quality);
        }

        [Theory(DisplayName = "Item Sulfuras")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(44)]
        [Trait("Category", "GildedRose")]
        public void UpdateQuality_WhenItemSulfuras_NotChangeQualityAndSellIn(int sellIn)
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = sellIn, Quality = 80 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(sellIn, Items.First().SellIn);
            Assert.Equal(80, Items.First().Quality);
        }

        [Theory(DisplayName = "Item Backstage Passes test increment")]
        [InlineData(15, 41)]
        [InlineData(10, 42)]
        [InlineData(5, 43)]
        [InlineData(-1, 0)]
        [Trait("Category", "GildedRose")]
        public void UpdateQuality_WhenBackstagePassesItem_ShouldIncrementQualityAccordingToSalesDays(int sellIn, int qualityResult)
        {
            // Arrange
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 40 } };
            GildedRose app = new GildedRose(Items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(qualityResult, Items.First().Quality);
        }

        [Fact(DisplayName = "Item Conjured decrease in quality twice as fast as the other items")]
        [Trait("Category", "GildedRose")]
        public void UpdateQuality_WhenConjuredItem_ShouldDecreaseTwiceAsFastOtherItems()
        {
            // Arrange
            IList<Item> Items = new List<Item> { 
                new Item { Name = "Item test", SellIn = 10, Quality = 20 }, 
                new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 20 } 
            };
            GildedRose app = new GildedRose(Items);
            
            // Act
            app.UpdateQuality();

            // Assert
            var qualityTestItem = Items.First(x => x.Name == "Item test").Quality; 
            var qualityConjuredItem = Items.First(x => x.Name == "Conjured Mana Cake").Quality;
            var differenceQualityItems = qualityTestItem - qualityConjuredItem;

            Assert.Equal(2, differenceQualityItems);
        }
    }
}
