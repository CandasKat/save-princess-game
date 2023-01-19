interface IMarketCustomer
{
    public int Money { get; set; }
    public string? CustomerInformation { get; }
    public void OnItemBought(MarketItem item, MarketItemCategory category);
}