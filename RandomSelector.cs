
public class RandomSelector
{
    private static Random rand = new Random();
    private List<Item> items;
    private Dictionary<int, PriorityInfo> priorityInfos;
    private double totalProbability;

    public RandomSelector(List<Item> items)
    {
        this.items = items;
        totalProbability = 100;
        priorityInfos = new Dictionary<int, PriorityInfo>();

        ResetPriorities();
    }

    private void ResetPriorities()
    {
        var priorityCounts = items
        .GroupBy(item => item.Priority)
        .ToDictionary(group => group.Key, group => group.Count());

        foreach (var kvp in priorityCounts)
        {
            priorityInfos[kvp.Key] = new PriorityInfo(kvp.Value, GetPriorityWeight(kvp.Key));
        }
    }
    private int GetPriorityWeight(int priority)
    {
        return priority switch
        {
            1 => 60,
            2 => 30,
            3 => 10,
            _ => 0
        };
    }

    public Item SelectItem()
    {
        double selectedValue = rand.NextDouble();

        double accumulatedWeight = 0;

        foreach (var (key, priorityInfo) in priorityInfos)
        {
            double groupWeight = (double)(priorityInfo.NumberOfItems * priorityInfo.ProbabilityForSingleItem / totalProbability);

            if (selectedValue <= accumulatedWeight + groupWeight)
            {
                var selectedItem = GetAndRemoveItemWithPriority(priorityInfo, key);

                return selectedItem;
            }
            accumulatedWeight += groupWeight;
        }

        return null;
    }

    private Item GetAndRemoveItemWithPriority(PriorityInfo priorityInfo, int key)
    {
        if (priorityInfo.NumberOfItems > 0)
        {
            var selectedItem = items
                .Where(item => item.Priority == key)
                .OrderBy(x => rand.Next())
                .FirstOrDefault();

            if (selectedItem != null)
            {

                items.Remove(selectedItem);

                priorityInfo.DeleteItem();

                totalProbability -= priorityInfo.ProbabilityForSingleItem;

                return selectedItem;
            }
        }

        return null;
    }
}

