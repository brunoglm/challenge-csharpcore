using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (IslegendaryItem(item)) continue;

                if (IsDefaultItemDecrement(item))
                {
                    UpdateQualityDecrementDefaultItem(item);
                    continue;
                }

                if (IsDefaultItemIncrement(item))
                {
                    UpdateQualityIncrementDefaultItem(item);
                    continue;
                }

                if (IsBackstagePassesItem(item))
                {
                    UpdateQualityBackstagePassesItem(item);
                    continue;
                }
            }
        }

        private void UpdateQualityBackstagePassesItem(Item item)
        {
            var quantityIncrementQuality = item.Quality;

            item.SellIn--;

            if (IsSellDateItemPassed(item))
            {
                item.Quality = 0;
                return;
            }

            quantityIncrementQuality += GetQuantityIncrementQuality(item);

            item.Quality = quantityIncrementQuality;

            if (item.Quality > 50)
                item.Quality = 50;
        }

        private int GetQuantityIncrementQuality(Item item)
        {
            if (item.SellIn < 5)
                return 3;

            if (item.SellIn < 10)
                return 2;

            return 1;
        }

        private void UpdateQualityIncrementDefaultItem(Item item)
        {
            var quantityIncrementQuality = item.Quality;

            item.SellIn--;

            quantityIncrementQuality++;

            if (IsSellDateItemPassed(item))
                quantityIncrementQuality++;

            item.Quality = quantityIncrementQuality;

            if (item.Quality > 50)
                item.Quality = 50;
        }

        private void UpdateQualityDecrementDefaultItem(Item item)
        {
            var quantityDecrementQuality = item.Quality;

            item.SellIn--;

            quantityDecrementQuality -= IsConjuredItem(item) ? 3 : 1;

            if (IsSellDateItemPassed(item))
                quantityDecrementQuality -= IsConjuredItem(item) ? 3 : 1;

            item.Quality = quantityDecrementQuality;

            if (item.Quality < 0)
                item.Quality = 0;
        }

        private bool IsSellDateItemPassed(Item item)
        {
            return item.SellIn < 0;
        }

        private bool IsDefaultItemDecrement(Item item)
        {
            return item.Name != "Sulfuras, Hand of Ragnaros" &&
                item.Name != "Aged Brie" &&
                item.Name != "Backstage passes to a TAFKAL80ETC concert";
        }

        private bool IsDefaultItemIncrement(Item item)
        {
            return item.Name == "Aged Brie";
        }

        private bool IslegendaryItem(Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros";
        }

        private bool IsBackstagePassesItem(Item item)
        {
            return item.Name == "Backstage passes to a TAFKAL80ETC concert";
        }

        private bool IsConjuredItem(Item item)
        {
            return item.Name == "Conjured Mana Cake";
        }
    }
}
