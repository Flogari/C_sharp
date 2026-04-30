public class Battle
{
    public List<Pokemon> Players;
    public List<Pokemon> Enemies;
    public List<Pokemon> AllPokemon;
    public Queue<Pokemon> TurnOrder;
    public event Action<string> OnLogMessage;
    public event Action OnBattleEnd;

    public Battle(List<Pokemon> players, List<Pokemon> enemies)
    {
        Players = players;
        Enemies = enemies;
        TurnOrder = new Queue<Pokemon>();
    }
    private void DetermineTurnOrder()
    {
        var all_pokemon = new List<Pokemon>(Players);
        all_pokemon.AddRange(Enemies);

        var sorted = all_pokemon.OrderByDescending(p => p.Stats.TotalSpeed).ToList();

        TurnOrder.Clear();
        foreach (var p in sorted)
        {
            TurnOrder.Enqueue(p);
        }
    }

    public bool StartBattle(Map map)
    {
        bool first_turn = true;
        OnLogMessage?.Invoke("--- DÉBUT DE L'AFFRONTEMENT ---");

        while (!IsBattleOver())
        {
            DetermineTurnOrder();
            if (first_turn == true)
            {
                AllPokemon = TurnOrder.ToList();
                first_turn = false;
            }

            while (TurnOrder.Count > 0 && !IsBattleOver())
            {
                Pokemon activePoke = TurnOrder.Dequeue();
                if (activePoke.CurrentHP <= 0) continue;
                ExecuteTurn(activePoke, map, AllPokemon);
            }
        }
        
        bool playerWon = false;
        if (Players.Any(p => p.CurrentHP > 0))
        {
            return true;
        }

        OnLogMessage?.Invoke(playerWon ? "VICTOIRE !" : "DÉFAITE...");
        OnBattleEnd?.Invoke();

        return playerWon;
    }
    public void ExecuteTurn(Pokemon active, Map map, List<Pokemon> all_pokemon)
    {
        map.DrawMap(map, all_pokemon, active);

        if (Players.Contains(active))
        {
            map.PlayerTurn(active, map, Enemies);
        }
        else
        {
            map.EnemyTurn(active, map, Players);
            System.Threading.Thread.Sleep(800);
        }
    }

    private bool IsBattleOver()
    {
        bool allPlayersDead = Players.All(p => p.CurrentHP <= 0);
        bool allEnemiesDead = Enemies.All(p => p.CurrentHP <= 0);

        return allPlayersDead || allEnemiesDead;
    }
}