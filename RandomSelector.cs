
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
            // Calculate the probability of the current priority group.
            // The probability is calculated as the number of items in the group times the probability of a single item, divided by the total probability.
            double groupWeight = (double)(priorityInfo.NumberOfItems * priorityInfo.ProbabilityForSingleItem / totalProbability);

            // Checks whether the chosen random number is within the probability range of the current priority group.
            // If so, this means that an item from this priority group should be chosen and remove. 
            if (selectedValue <= accumulatedWeight + groupWeight)
            {
                var selectedItem = GetAndRemoveItemWithPriority(priorityInfo, key);

                return selectedItem;
            }
            accumulatedWeight += groupWeight;
        }

        throw new InvalidOperationException("Failed to select an item.");
    }

    private Item GetAndRemoveItemWithPriority(PriorityInfo priorityInfo, int key)
    {
        if (priorityInfo.NumberOfItems <= 0)
        {
            throw new InvalidOperationException("Cannot select an item from an empty priority group.");
        }

        var selectedItem = items
            .Where(item => item.Priority == key)
            .OrderBy(x => rand.Next())
            .FirstOrDefault();

        if (selectedItem != null)
        {

            items.Remove(selectedItem);

            // decrease number of items for the selected item priority
            priorityInfo.DeleteItem();

            // Update the total chance after removing an item
            // The total chance decreases by the chance of a single item from the selected group
            totalProbability -= priorityInfo.ProbabilityForSingleItem;

            return selectedItem;
        }

        return null;
    }
}

