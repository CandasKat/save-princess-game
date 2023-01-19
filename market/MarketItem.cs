using ConsoleTables;

class MarketItem : IPromptable
{
    public List<string> PromptHeader { get; } = new List<string>(){ "#", "Nom", "Description", "Prix" };
    public IBuyable Article { get ; private set; }
    public int Price { get; private set; }

    public MarketItem(IBuyable article, int price)
    {
        Article = article;
        Price = price;
    }

    public void FormatPrompt(int index, ConsoleTable table, string context)
    {
        table.AddRow(index, Article.Name, Article.Description, Price);
    }
}