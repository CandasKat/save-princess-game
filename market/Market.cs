class Market
{
    public List<MarketItemCategory> Categories { get; private set; }  = new List<MarketItemCategory>();
    private IMarketCustomer? _customer;
    private OnMenuBackListener _onBack;

    public Market(OnMenuBackListener onBack)
    {
        _onBack = onBack;
    }
    
    public void ShowMenu(IMarketCustomer customer)
    {
        _customer = customer;
        Console.Clear();
        ShowMarketHeader();
        int menuIndex = ConsoleUtils.PromptItems(
            new List<IPromptable>(Categories.ToArray()),
            null,
            true,
            null);
        if (menuIndex == -1) _onBack();
        else ShowCategory(Categories[menuIndex]);
    }

    private void ShowCategory(MarketItemCategory category)
    {
        Console.Clear();
        ShowMarketHeader();
        int menuIndex = ConsoleUtils.PromptItems(
            new List<IPromptable>(category.Items.ToArray()),
            "List des " + category.Name,
            true,
            null);
        if (menuIndex == -1) ShowMenu(_customer);
        else TryBuy(category.Items[menuIndex], category);
    }

    public bool Buy(IMarketCustomer customer, MarketItem item, MarketItemCategory category)
    {
        if (customer.Money >= item.Price)
        {
            customer.Money -= item.Price;
            customer.OnItemBought(new MarketItem(item.Article.Clone(), item.Price), category);
            return true;
        }
        
        return false;
    }

    private void TryBuy(MarketItem item, MarketItemCategory category)
    {
        string message = Buy(_customer, item, category)
            ? "Merci pour votre achat."
                + "Je suis sûr que cet objet vous donnera entière satisfaction."
            : "Solde insuffisant :"
                + " cet objet coûte " + item.Price
                + " et vous disposez de  " + _customer.Money + ".";

        Console.WriteLine(message);
        ConsoleUtils.PressAnyKeyToContinue();
        ShowCategory(category);
    }

    private void ShowMarketHeader()
    {
        string? customerInfo = _customer.CustomerInformation;
        Console.WriteLine("---------------------------");
        Console.WriteLine("Marché Brico-loisirs");
        Console.WriteLine("Votre solde : " + _customer.Money);
        if (customerInfo != null)
        {
            Console.WriteLine(customerInfo);
        }
        Console.WriteLine("---------------------------");
    }
}