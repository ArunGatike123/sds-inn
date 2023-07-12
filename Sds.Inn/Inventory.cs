namespace Sds.Inn;

public class Inventory
{

 IList<Item> Items { get; }

public Inventory()
{        
    Items = new List<Item>
    {
        new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
        new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
        new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
        new Item { Name = "Sulfuras", SellIn = 0, Quality = 80 },
        new Item { Name = "Backstage passes", SellIn = 15, Quality = 20 },
        new Item { Name = "Conjured", SellIn = 3, Quality = 6 }
    };
}


     public   void UpdateQuality()
        {
            foreach (var item in Items)
            {
                // Skip legendary item "Sulfuras"
                if (item.Name == "Sulfuras, Hand of Ragnaros")
                    continue;

                // Decrease Sell-In value for all items except "Sulfuras"
                item.SellIn--;

                // Update quality based on item type
                if (item.Name == "Aged Brie")
                {
                    // Increase quality for "Aged Brie" as it ages
                    item.Quality++;
                }
                else if (item.Name == "Backstage passes")
                {
                    // Increase quality for "Backstage passes"
                    if (item.SellIn < 0)
                        item.Quality = 0; // Quality drops to 0 after the concert
                    else if (item.SellIn < 5)
                        item.Quality += 3; // Quality increases by 3 when there are 5 days or less
                    else if (item.SellIn < 10)
                        item.Quality += 2; // Quality increases by 2 when there are 10 days or less
                    else
                        item.Quality++; // Quality increases by 1 for other days
                }
                else if (item.Name.StartsWith("Conjured"))
                {
                    // Degrade quality twice as fast for "Conjured" items
                    item.Quality -= 2;
                }
                else
                {
                    // Degrade quality for normal items
                    item.Quality--;
                }

                // Ensure quality is within the valid range of 0 to 50
                item.Quality = Math.Max(0, Math.Min(50, item.Quality));

                // Decrease quality twice as fast after the Sell-In date has passed
                if (item.SellIn < 0)
                {
                    if (item.Name != "Aged Brie" && !item.Name.StartsWith("Backstage passes"))
                    {
                        if (item.Name.StartsWith("Conjured"))
                            item.Quality -= 2; // Degrade quality twice as fast for "Conjured" items after Sell-In date
                        else
                            item.Quality--;
                    }
                }
            }
        }
    }
}
class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }
}
