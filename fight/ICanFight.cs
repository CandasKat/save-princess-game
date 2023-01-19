interface ICanFight
{
    public string NickName { get; }
    public int Health { get; }
    public int Speed { get; }
    public int Force { get; }
    public int Agility { get; }
    public string Stats { get; }
    public List<IWeapon> Weapons { get; }
}