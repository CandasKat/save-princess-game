using ConsoleTables;
class Fight
{
    public FighterSlot Slot1 { get; private set; }
    public FighterSlot Slot2 { get; private set; }
    public ICanFight? Winner
    {
        get
        {
            if (Slot1.RemainingHealth <= 0 || Slot1.Fighter.Weapons.Count == 0)
            {
                return this.Slot2.Fighter;
            }
            if (Slot2.RemainingHealth <= 0 || Slot2.Fighter.Weapons.Count == 0)
            {
                return Slot1.Fighter;
            }

            return null;
        }
    }
    public bool IsCompleted { get => null != Winner; }

    private FighterSlot? _currentPlayer;
    private FighterSlot? _currentTarget;
    private Random _rand = new Random();

    public Fight(
        ICanFight fighter1,
        bool fighter1IsMob,
        ICanFight fighter2,
        bool fighter2IsMob)
    {
        Slot1 = new FighterSlot(fighter1, fighter1IsMob);
        Slot2 = new FighterSlot(fighter2, fighter2IsMob);
    }

    public Fight(
        ICanFight fighter1,
        ICanFight fighter2) : this(fighter1, false, fighter2, true) {}

    public void Start()
    {
        Slot1.GetReadyToFight();
        Slot2.GetReadyToFight();

        Console.Clear();
        ShowHeader();
        Console.WriteLine("Le combat va commencer !");
        ConsoleUtils.PressAnyKeyToContinue();

        do NextRound();
        while(!IsCompleted);

        Console.WriteLine("Le gagnant est : " + Winner.NickName);
        ConsoleUtils.PressAnyKeyToContinue();
    }

    private void NextRound()
    {
        ComputeNextPlayer();

        Console.Clear();
        ShowHeader();
        int weaponIndex = _currentPlayer.IsMob
            ? _rand.Next(0, _currentPlayer.Fighter.Weapons.Count)
            : PromptWeapon(_currentPlayer.Fighter);
        IWeapon weapon = _currentPlayer.Fighter.Weapons[weaponIndex];
        string roundMessage = weapon.Use(_currentTarget);
        _currentTarget.SpeedIndex += _currentTarget.Fighter.Speed;
        Console.Clear();
        ShowHeader();
        Console.WriteLine(roundMessage);
        ConsoleUtils.PressAnyKeyToContinue();
    }

    private void ComputeNextPlayer()
    {
        if (Slot1.SpeedIndex > Slot2.SpeedIndex)
        {
            _currentPlayer = Slot1;
            _currentTarget = Slot2;
        }
        else
        {
            _currentPlayer = Slot2;
            _currentTarget = Slot1;
        }
    }

    private int PromptWeapon(ICanFight fighter)
    {
        return ConsoleUtils.PromptItems(
            new List<IPromptable>(fighter.Weapons.ToArray()),
            "Quelle arme voulez-vous utiliser ?",
            false, null);
    }

    private void ShowHeader()
    {
        ConsoleTable table = new ConsoleTable(Slot1.Fighter.NickName, Slot2.Fighter.NickName);
        table.AddRow(Slot1.Fighter.Stats, Slot2.Fighter.Stats);
        table.AddRow(BuildHPRow(Slot1), BuildHPRow(Slot2));
        table.AddRow(BuildSpeedRow(Slot1), BuildSpeedRow(Slot2));
        table.Write(Format.Alternative);
    }

    private string BuildHPRow(FighterSlot slot)
    {
        int healthRatio = (int)Math.Round(35f * slot.RemainingHealth / slot.Fighter.Health);
        healthRatio = healthRatio > 0 ? healthRatio : 0;
        string bar = "["
            + new string('=', healthRatio)
            + new string(' ', 35 - healthRatio)
            + "]";
        return "PV: " + slot.RemainingHealth + " | " + bar;
    }

    private string BuildSpeedRow(FighterSlot slot)
    {
        return "Indice de vitesse: " + slot.SpeedIndex;
    }
}