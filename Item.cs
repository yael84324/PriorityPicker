public class Item
{
    public string Description { get; set; }
    public int Priority { get; set; }

    public Item(string description, int priority)
    {
        Description = description;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Description} (Priority: {Priority})";
    }
}
