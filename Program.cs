void HandleLevelUp(List<Pokemon> team)
{
    Random rng = new Random();
    // On prend un Pokémon vivant au hasard
    var survivors = team.Where(p => p.CurrentHP > 0).ToList();
    if (survivors.Count == 0) return;

    Pokemon luckyOne = survivors[rng.Next(survivors.Count)];

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n[LEVEL UP] {luckyOne.Name} gagne en puissance !");

    // Boost de stats
    luckyOne.Stats.BaseMaxHP += 5;
    luckyOne.Stats.BaseAttack += 5;
    luckyOne.Stats.BaseDefense += 5;
    luckyOne.Stats.BaseSpecialAttack += 5;
    luckyOne.Stats.BaseSpecialDefense += 5;
    luckyOne.Stats.BaseSpeed += 5;
    luckyOne.Heal(luckyOne.Stats.BaseMaxHP);

    Move newMove = new PhysicalMove("Lance-Flamme", 15, 100, 2, new FireType()); // Range 1
    luckyOne.Moves.Add(newMove);

    Console.WriteLine($"{luckyOne.Name} a appris {newMove.Name} !");
    Console.ResetColor();
}

void HandleRandomEvent(List<Pokemon> team)
{
    Random rng = new Random();
    List<GameEvent> events = new List<GameEvent>
    {
        new HealEvent(),
        new StatsChange(),
    };

    GameEvent currentEvent = events[rng.Next(events.Count)];

    int PokemonEvent = rng.Next(0, 3);
    currentEvent.Resolve(team[PokemonEvent]);
}

List<Pokemon> playerTeam = Initializer.CreatePlayerTeam();
int floor = 1;

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

    HandleRandomEvent(playerTeam);

    floor++;
    Console.WriteLine("\nAppuyez sur une touche pour passer à l'étage suivant...");
    Console.ReadKey();
}
Console.WriteLine($"\nGAME OVER. Vous avez atteint l'étage {floor}.");