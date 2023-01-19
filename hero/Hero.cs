using ConsoleTables;
using epsic_320_labo3;

class Hero : IMarketCustomer, ICanFight
{
    public string Name { get; set; }
    public string NickName { get => Name + " le " + Class.Name; }
    public HeroClass Class { get; private set; }

    public int Money { get; set; }
    public string Stats
    {
        get
        {
            return "PV: " + Health + " | "
                + "Vitesse: " + Speed + " | " 
                + "Force: " + Force + " | "
                + "Agility: " + Agility;
        }
    }
    public string? CustomerInformation { get => Stats; }

    private int _baseHealth;
    private int _baseSpeed;
    private int _baseForce;
    private int _baseAgility;
    public int Health
    { 
        get
        {
            double computed = _baseHealth;
            List<IFixModifier> fixModifiers = new List<IFixModifier>();
            fixModifiers.AddRange(Weapons);
            fixModifiers.AddRange(Equipments);
            fixModifiers.ForEach(modifier => computed += modifier.FixHealthModifier);

            if (computed < 1)
            {
                return 1;
            }
            computed *= Class.HealthModifier;
            return (int)Math.Round(computed);
        }
    }
    public int Speed
    {
        get
        {
            double computed = _baseSpeed;
            List<IFixModifier> fixModifiers = new List<IFixModifier>();
            fixModifiers.AddRange(Weapons);
            fixModifiers.AddRange(Equipments);
            fixModifiers.ForEach(modifier => computed += modifier.FixSpeedModifier);

            if (computed < 1)
            {
                return 1;
            }
            computed *= Class.SpeedModifier;
            return (int)Math.Round(computed);
        }
    }
    public int Force
    {
        get
        {
            double computed = _baseForce;
            List<IFixModifier> fixModifiers = new List<IFixModifier>();
            fixModifiers.AddRange(Weapons);
            fixModifiers.AddRange(Equipments);
            fixModifiers.ForEach(modifier => computed += modifier.FixForceModifier);

            if (computed < 1)
            {
                return 1;
            }
            computed *= Class.ForceModifier;

            return (int)Math.Round(computed);
        }
    }
    public int Agility
    {
        get
        {
            double computed = _baseAgility;
            List<IFixModifier> fixModifiers = new List<IFixModifier>();
            fixModifiers.AddRange(Weapons);
            fixModifiers.AddRange(Equipments);
            fixModifiers.ForEach(modifier => computed += modifier.FixAgilityModifier);

            if (computed < 1)
            {
                return 1;
            }
            computed *= Class.AgilityModifier;

            return (int)Math.Round(computed);
        }
    }
    public List<IWeapon> Weapons { get; private set; } = new List<IWeapon>();
    public List<Equipment> Equipments { get; private set; }  = new List<Equipment>();
    public Hero(
        string name,
        int baseHealth,
        int baseSpeed,
        int baseForce,
        int baseAgility,
        HeroClass heroClass,
        int money)
    {
        Name = name;
        _baseHealth = baseHealth;
        _baseSpeed = baseSpeed;
        _baseForce = baseForce;
        _baseAgility = baseAgility;
        Class = heroClass;
        Money = money;
        Weapons.Add(Class.BaseWeapon);
        Class.BaseWeapon.Owner = this;
    }

    public void Show()
    {
        Console.Clear();
        
        ConsoleTable table = new ConsoleTable("Nom du héro", Name, "");
        table.AddRow("Classe", Class.Name, Class.Description);
        table.Write(Format.Alternative);
        
        table = new ConsoleTable("Equipment", "Description");
        Equipments.ForEach(equipment => table.AddRow(
            equipment.Name,
            equipment.Description));
        table.Write(Format.Alternative);

        table = new ConsoleTable("Armes", "Description", "Min", "Max", "Coups");
        Weapons.ForEach(weapon => table.AddRow(
            weapon.Name,
            weapon.Description,
            weapon.MinDamage,
            weapon.MaxDamage,
            weapon.MaxUses));
        table.Write(Format.Alternative);

        table = new ConsoleTable("Caractéristiques", "Valeur", "Description");
        table.AddRow("Points de vie", Health, "Représente les dégats que peut encaisser le héro avant de mourir.");
        table.AddRow("Vitesse", Speed, "Représente le nombre point ajouté à la barre de vitesse à la fin de chaque tour. Le héro en ayant le plus joue le tour suivant.");
        table.AddRow("Force", Force, "Les dégats infligés par l'arme du héros sont multipliés par la force en pourcent.");
        table.AddRow("Agilité", Agility, "Représente la probabilité d'esquiver le coup de l'adversaire en pourmille.");
        table.Write(Format.Alternative);
    }

    public void OnItemBought(MarketItem item, MarketItemCategory category)
    {
        switch (category.Name)
        {
            case Game.MARKET_CATEGORY_WEAPONS:
                Weapon weapon = (Weapon)item.Article;
                weapon.Owner = this;
                Weapons.Add(weapon);
                break;
            case Game.MARKET_CATEGORY_EQUIPMENTS:
                Equipments.Add((Equipment) item.Article);
                break;
        }
    }
}