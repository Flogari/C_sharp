void HandleLevelUp(List<Pokemon> team)
{
    Random rng = new Random();
    var survivors = team.Where(p => p.CurrentHP > 0).ToList();
    if (survivors.Count == 0) return;

    Pokemon luckyOne = survivors[rng.Next(survivors.Count)];

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n[LEVEL UP] {luckyOne.Name} gagne en puissance !");

    luckyOne.Stats.BaseMaxHP += 5;
    luckyOne.Stats.BaseAttack += 5;
    luckyOne.Stats.BaseDefense += 5;
    luckyOne.Stats.BaseSpecialAttack += 5;
    luckyOne.Stats.BaseSpecialDefense += 5;
    luckyOne.Stats.BaseSpeed += 5;
    luckyOne.Heal(luckyOne.Stats.BaseMaxHP);

    Move newMove = new PhysicalMove("GIGA IMPACT", 150, 100, 5, new NormalType());
    luckyOne.Moves.Add(newMove);

    Console.WriteLine($"{luckyOne.Name} a appris {newMove.Name} !");
    Console.ResetColor();
}

void HandleRandomEvent(List<Pokemon> team, int floor)
{
    Random rng = new Random();
    List<GameEvent> events = new List<GameEvent>
    {
        new HealEvent(),
        new StatsChange(),
        new LootEvent(floor),
    };
    int i = 0;
    foreach(Pokemon p in team)
    {
        GameEvent currentEvent = events[rng.Next(events.Count)];
        currentEvent.Resolve(team[i]);
        i++;
    }
}

List<Pokemon> playerTeam = Initializer.CreatePlayerTeam();

foreach (var p in playerTeam)
{
    p.OnHealthChanged += (curr, max) => Console.WriteLine($"{p.Name} : {curr}/{max} PV");
    p.SetTalent(new LastResort());
}

int floor = 1;
Console.Clear();
Console.WriteLine("=== BIENVENUE DANS LA MONDE DES POKEMONS ===");

while (playerTeam.Any(p => p.CurrentHP > 0))
{
    Console.WriteLine($"\n--- ÉTAGE {floor} ---");

    List<Pokemon> enemyTeam = Initializer.GenerateEnemyTeam(floor);
    Map map = new MapGenerator().Generate();
    new MapGenerator().SpawnTeams(map, playerTeam, enemyTeam);

    Battle battle = new Battle(playerTeam, enemyTeam);
    battle.OnLogMessage += (msg) => Console.WriteLine(msg);

    bool playerWon = battle.StartBattle(map);

    if (!playerWon)
    {
        break;
    }

    HandleLevelUp(playerTeam);

    HandleRandomEvent(playerTeam, floor);

    floor++;
    Console.WriteLine("\nAppuyez sur une touche pour passer à l'étage suivant...");
    Console.ReadKey();
}
Console.WriteLine($"\nGAME OVER. Vous avez atteint l'étage {floor}.");