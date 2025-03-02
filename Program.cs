using System;
using System.Collections.Generic;
using System.Linq;

namespace PriorityPicker
{
    public class Program
    {
        public static void Main()
        {
            List<Item> items = GenerateItems();

            var randomSelector = new RandomSelector(items);

            for (int i = 1; i <= 7; i++)
            {
                var selectedItem = randomSelector.SelectItem();
                Console.WriteLine($"Run {i}: Selected Item: {selectedItem.Description} with Priority {selectedItem.Priority}");
            }
        }

        static List<Item> GenerateItems()
        {
            List<Item> items = new List<Item>();

            for (int priority = 1; priority <= 3; priority++)
            {
                for (int i = 1; i <= 5; i++)
                {
                    items.Add(new Item($"Item{i * priority}", priority));
                }
            }

            return items;
        }

    }
}