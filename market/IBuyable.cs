interface IBuyable
{
    public string Name { get; }
    public string? Description { get; }

    public IBuyable Clone();
}