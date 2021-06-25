using System;
using System.Collections.Generic;

namespace csharpcore
{
    public class Program
    {
        const int LAST_DAY = 31;

        public static void Main(string[] args)
        {    
            Console.WriteLine("OMGHAI!");
            IList<Item> Items = GetItemsList();

            var app = new GildedRose(Items);

            for (var i = 0; i < LAST_DAY; i++)
            {
                WriteResultConsole(Items, i);
                app.UpdateQuality();
            }
        }

        private static IList<Item> GetItemsList()
        {
            return new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				// this conjured item does not work properly yet
				new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };
        }

        private static void WriteResultConsole(IList<Item> Items, int i)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");
            foreach (var item in Items)
            {
                Console.WriteLine(item.Name + ", " + item.SellIn + ", " + item.Quality);
            }
            Console.WriteLine("");
        }
    }
}
