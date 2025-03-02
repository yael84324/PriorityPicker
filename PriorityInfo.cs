

public class PriorityInfo
{
    public int NumberOfItems { get; set; }
    public double ProbabilityForSingleItem { get; set; }
    public PriorityInfo(int numberOfItems, int probabilityPercentage)
    {
        NumberOfItems = numberOfItems;
        ProbabilityForSingleItem = (double)probabilityPercentage / NumberOfItems;
    }
    public void DeleteItem()
    {
        NumberOfItems--;
    }

    public override string ToString()
    {
        return $"(NumberOfItems: {NumberOfItems}), (ProbabilityForSingleItem: {ProbabilityForSingleItem})";
    }
}
