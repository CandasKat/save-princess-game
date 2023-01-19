using ConsoleTables;

interface IPromptable
{
    public List<string> PromptHeader { get; }
    public void FormatPrompt(int index, ConsoleTable table, string? context);
}