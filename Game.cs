class Game
{
    public const string MARKET_CATEGORY_WEAPONS = "Armes";
    public const string MARKET_CATEGORY_EQUIPMENTS = "Equipements";

    public Hero? Player;

    private int _playerBaseHealth = 100;
    private int _playerBaseSpeed = 100;
    private int _playerBaseForce = 100;
    private int _playerBaseAgility = 100;
    private int _playerBaseMoney = 1000;

    private Market _market;
    private List<Fight> _fights;

    public List<HeroClass> _availableClasses { get; private set; }  = new List<HeroClass>()
    {
        new Warrior(),
        new Paladin(),
        new Voleur()
    };
    private List<Weapon> _availableWeapons = new List<Weapon>()
    {
        /* 0 */ new Weapon("Epée courte", "Dégats : 5-6, Coups: 100", 0, 0, 0, 0 , 5, 6, 100),
        /* 1 */ new Weapon("Epée courte enchantée", "Dégats : 6-8, Coups: 80, agilité +10%", 0, 0, 0, +10, 6, 8, 80),
        /* 2 */ new Weapon("Epée longue", "Dégats : 9-12, Coups: 50", 9, 12, 50),
        /* 3 */ new Weapon("Epée longue enchantée", "Dégats : 11-16, Coups: 35, vitesse et agilité +10%", 0, +10, 0, +10, 11, 16, 35),
        /* 4 */ new Weapon("Marteau de vie", "Dégats : 8-16, Coups: 30, PV +20%", +20, 0, 0, 0, 8, 16, 30),
        /* 5 */ new Weapon("Hache ancestrale", "Dégats : 6-10, Coups: 50, force +20% et agilité -10%", 0, 0, +20, -10, 6, 10, 50),
        /* 6 */ new Weapon("Baton de mage de feu", "20-40, Coups: 15, PV -25%, vitesse, force et agilité +15%", -25, +15, +15, +15, 20, 40, 15),
        /* 7 */ new Weapon("Arc elfique", "Dégats : 30-35, Coups: 5, PV et vitesse +25%", +25, +25, 0, 0, 30, 35, 5),
        /* 8 */ new Weapon("Dague elfique", "Dégats : 18-22, Coups: 20, force et agilité +25%", 0, 0, +25, +25, 18, 22, 20),
        /* 9 */ new Weapon("Lance démoniaque", "Dégats : 30-100, Coups: 5, PV +20%, force +30%, vitesse et agilité -20%", +20, -20, +30, -20, 30, 100, 5)
    };
    private List<Equipment> _availableEquipments = new List<Equipment>()
    {
        /* 0 */ new Equipment("Anneau de vie", "PV +10%", +10, 0, 0, 0),
        /* 1 */ new Equipment("Anneau de vitesse", "Vitesse +10%", 0, +10, 0, 0),
        /* 2 */ new Equipment("Anneau de force", "Force +10%", 0, 0, +10, 0),
        /* 3 */ new Equipment("Anneau d'agilité", "Agilité +10%", 0, 0, 0, +10),
        /* 4 */ new Equipment("Bracelet de roi", "PV, vitesse, force, agilité +10%", +10, +10, +10, +10),
        /* 5 */ new Equipment("Collier de fée", "Vitesse et agilité +20%, PV et force -10%", -10, +20, -10, +20),
        /* 6 */ new Equipment("Chapeau de sorcier", "Vitesse et agilité +20%, force -20%", 0, +20, -20, +20),
        /* 7 */ new Equipment("Heaume de chevalier", "PV +50%, vitesse et agilité -20%", +50, -20, 0, -20),
        /* 8 */ new Equipment("Tunique de shaman", "Agilité +50%, PV et force -20%", -20, 0, -20, +50),
        /* 9 */ new Equipment("Cotte de mailles fines", "PV +25%, vitesse et agilité -10%", +25, -10, 0, -10),
        /* 10 */ new Equipment("Cuirasse de conquérant", "PV +100%, force +20%, vitesse -20%, agilité -50%", +100, -20, +20, -50),
        /* 11 */ new Equipment("Gants de puissance", "Force +50%, agilité -10%", 0, 0, +50, -10),
        /* 12 */ new Equipment("Bottes de contrebandier", "Vitesse +30%, agilité +20%, force -10%", 0, +30, -10, +20),
        /* 13 */ new Equipment("Sandales à crampons", "PV, vitesse, force, agilité +10%",+10, +10, +10, +10),
        /* 14 */ new Equipment("Cape céleste", "Agilité +50%, PV, vitesse et force -20%", -20, -20, -20, +50),
        /* 15 */ new Equipment("Cape de garde royal", "PV et force +50%, vitesse et agilité -40%", +50, -40, +50, -40)
    };

    private Random _rand = new Random();

    public Game()
    {
        _market = new Market(ShowMainMenu);
    }

    public void Start()
    {
        Console.Clear();
        SetupPlayer();
        SetupMarket();
        SetupFights();
        ShowMainMenu();
    }

    private void SetupPlayer()
    {
        ShowPrologue();
        string playerName = ChooseName();
        HeroClass playerClass = ChooseClass();
        Player = new Hero(
            playerName,
            _playerBaseHealth,
            _playerBaseSpeed,
            _playerBaseForce,
            _playerBaseAgility,
            playerClass,
            _playerBaseMoney);
        Console.WriteLine("Excellent, je te conseille maintenant d'acheter de l'équipement avant de partir affronter les gardiens.");
    }

    private void SetupMarket()
    {
        MarketItemCategory weaponsCategory = new MarketItemCategory(Game.MARKET_CATEGORY_WEAPONS);
        List<MarketItem> buyableWeapons = new List<MarketItem>()
        {
            new MarketItem(_availableWeapons[0], 50),
            new MarketItem(_availableWeapons[1], 80),
            new MarketItem(_availableWeapons[2], 80),
            new MarketItem(_availableWeapons[3], 130),
            new MarketItem(_availableWeapons[4], 150),
            new MarketItem(_availableWeapons[5], 150),
            new MarketItem(_availableWeapons[6], 150),
            new MarketItem(_availableWeapons[7], 200),
            new MarketItem(_availableWeapons[8], 200),
            new MarketItem(_availableWeapons[9], 300),
        };
        weaponsCategory.Items.RemoveAll(item => true);
        weaponsCategory.Items.AddRange(buyableWeapons);
        _market.Categories.Add(weaponsCategory);

        MarketItemCategory equipmentsCategory = new MarketItemCategory(Game.MARKET_CATEGORY_EQUIPMENTS);
        List<MarketItem> buyableEquipments = new List<MarketItem>()
        {
            new MarketItem(_availableEquipments[0], 50),
            new MarketItem(_availableEquipments[1], 50),
            new MarketItem(_availableEquipments[2], 50),
            new MarketItem(_availableEquipments[3], 50),
            new MarketItem(_availableEquipments[4], 150),
            new MarketItem(_availableEquipments[5], 150),
            new MarketItem(_availableEquipments[6], 150),
            new MarketItem(_availableEquipments[7], 150),
            new MarketItem(_availableEquipments[8], 150),
            new MarketItem(_availableEquipments[9], 150),
            new MarketItem(_availableEquipments[10], 150),
            new MarketItem(_availableEquipments[11], 150),
            new MarketItem(_availableEquipments[12], 150),
            new MarketItem(_availableEquipments[13], 150),
            new MarketItem(_availableEquipments[14], 250),
            new MarketItem(_availableEquipments[15], 250)
        };
        equipmentsCategory.Items.RemoveAll(item => true);
        equipmentsCategory.Items.AddRange(buyableEquipments);
        _market.Categories.Add(equipmentsCategory);
    }

    private void SetupFights()
    {
        Hero mob1 = RandomHero(
            "Jakkar",
            100, 100, 100, 100, 750,
            new List<HeroClass>(){ new Paladin(), new Warrior(), new Voleur() });
        Hero mob2 = RandomHero(
            "Visérion",
            100, 100, 100, 100, 1000,
            new List<HeroClass>(){ new Paladin(), new Warrior(), new Voleur() });
        Hero mob3 = RandomHero(
            "Ruféus",
            100, 100, 100, 100, 1250,
            new List<HeroClass>(){ new Paladin(), new Warrior(), new Voleur() });
        Hero mob4 = RandomHero(
            "L'impitoyable M. Galli",
            100, 100, 100, 100, 1500,
            new List<HeroClass>(){ new Paladin(), new Warrior(), new Voleur() });
        _fights = new List<Fight>()
        {
            new Fight(Player, mob1),
            new Fight(Player, mob2),
            new Fight(Player, mob3),
            new Fight(Player, mob4)
        };
    }

    private void ShowMainMenu()
    {
        Console.Clear();
        List<IPromptable> menus = new List<IPromptable>()
        {
            new MenuOption("Libérer la princesse"),
            new MenuOption("Voir mon personnage"),
            new MenuOption("Acheter de l'équipement"),
            new MenuOption("Recommencer"),
            new MenuOption("Quitter")
        };
        int menuIndex = ConsoleUtils.PromptItems(menus);
        switch (menuIndex)
        {
            case 0 :
                NextFight();
                break;
            case 1 :
                Player.Show();
                ConsoleUtils.PressAnyKeyToContinue();
                ShowMainMenu();
                break;
            case 2 :
                _market.ShowMenu(Player);
                break;
            case 3 :
                Start();
                break;
            case 4 :
                Quit();
                break;
        }
    }

    private void NextFight()
    {
        Fight nextFight = _fights.Find(fight => !fight.IsCompleted);
        nextFight.Start();
        if (nextFight.Winner != Player)
        {
            GameOver();
        }
        else if (_fights.Last().IsCompleted)
        {
            ShowCredits();
        }
        else
        {
            ShowMainMenu();
        }
    }

    private void GameOver()
    {
        Console.Clear();
        Console.WriteLine("Perdu :,(");
        Console.WriteLine("Vous êtes mort dans d'atroces souffrance.");
        ConsoleUtils.PressAnyKeyToContinue();
    }

    private void ShowCredits()
    {
        Console.Clear();
        Console.WriteLine("Vous avez libéré la princesse ! :D");
        Console.WriteLine("Vous l'épousez, devenez le nouveau roi et tout est bien qui finit bien.");
        ConsoleUtils.PressAnyKeyToContinue();
    }

    private void Quit()
    {
        Environment.Exit(0);
    }

    private void ShowPrologue()
    {
        Console.WriteLine("Psssst, hey toi là.");
        ConsoleUtils.PressAnyKeyToContinue();
        Console.WriteLine("Est-ce que tu savais qu'une princesse était emprisonnée dans ce donjon ?");
        ConsoleUtils.PressAnyKeyToContinue();
        Console.WriteLine("Il parait que son père offrira une récompense hors du commun à celui qui parviendra à la libérer...");
        ConsoleUtils.PressAnyKeyToContinue();
        Console.WriteLine("Mais la princesse est sous bonne garde... Pas moins de 4 super héros gardent le donjon. Tu devras tous les vaincre si tu veux parvenir jusqu'à son cachot !");
        ConsoleUtils.PressAnyKeyToContinue();
        Console.WriteLine("Cette quête t'intéresse ?! Ok commençons...");
        ConsoleUtils.PressAnyKeyToContinue();
    }

    private string ChooseName()
    {
        string message = "Tout d'abord, dis-moi quel est ton nom ?";
        Console.WriteLine(message);
        string name = Console.ReadLine();
        if (name != null && name.Length > 0)
        {
            Console.WriteLine("Ravi de faire ta connaissance " + name + ".");
            return name;
        }
        Console.WriteLine("Nom invalide.");
        return ChooseName();
    }

    private HeroClass ChooseClass()
    {
        List<IPromptable> items = _availableClasses.ToList<IPromptable>();
        string message = "Maintenant dis-moi à quelle classe tu appartiens ?";
        return _availableClasses[ConsoleUtils.PromptItems(items, message, false, null)];
    }

    private Hero RandomHero(
        string name,
        int baseHealth,
        int baseSpeed,
        int baseForce,
        int baseAgility,
        int money,
        List<HeroClass> availableClasses)
    {
        HeroClass heroClass = availableClasses[_rand.Next(0, availableClasses.Count)];
        Hero hero = new Hero(name, baseHealth, baseSpeed, baseForce, baseAgility, heroClass, money);
        
        RandomHeroStuff(hero, _market.Categories[0], 3);
        RandomHeroStuff(hero, _market.Categories[1], 1000);
        
        return hero;
    }

    private void RandomHeroStuff(Hero hero, MarketItemCategory category, int max)
    {
        int count = 0;
        List<MarketItem> items = new List<MarketItem>(category.Items);
        while (items.Count > 0 && hero.Money > 0 && count < max)
        {
            MarketItem item = items[_rand.Next(0, items.Count)];
            if (!_market.Buy(hero, item, category)) { items.Remove(item); }
        }
    }
}