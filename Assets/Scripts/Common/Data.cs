public class Data
{
    public int RequireCoin { get; set; }
    public int CollectCoin { get; set; }
    public bool ClearCondition { get { return CollectCoin >= RequireCoin; } set { } }

    public Data(int RequireCoin)
    {
        CollectCoin = 0;
        this.RequireCoin = RequireCoin;
    }
}