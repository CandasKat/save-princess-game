class FighterSlot
{
    public ICanFight Fighter { get; private set; }
    public int RemainingHealth { get => Fighter.Health - _damageTaken; }
    private int _damageTaken = 0;
    public int SpeedIndex { get; set; }
    public bool IsMob { get; private set; }

    private Random _rand = new Random();

    public FighterSlot(ICanFight fighter, bool isMob)
    {
        Fighter = fighter;
        IsMob = isMob;
    }

    public bool Hit(int damage)
    {
        int dodgeProbability = _rand.Next(0, 1000);
        if (Fighter.Agility > dodgeProbability) return false;
        
        _damageTaken += damage;
        _damageTaken = _damageTaken > Fighter.Health ? Fighter.Health : _damageTaken;
        return true;
    }

    public void GetReadyToFight()
    {
        _damageTaken = 0;
        SpeedIndex = Fighter.Speed;
    }
}