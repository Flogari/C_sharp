public class Battle
{
    public List<Pokemon> Players;
    public List<Pokemon> Enemies;
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

    public bool StartBattle(Map map) // Renvoie true si victoire, false si défaite
    {
        OnLogMessage?.Invoke("--- DÉBUT DE L'AFFRONTEMENT ---");

        while (!IsBattleOver())
        {
            DetermineTurnOrder();

            while (TurnOrder.Count > 0 && !IsBattleOver())
            {
                Pokemon activePoke = TurnOrder.Dequeue();
                if (activePoke.CurrentHP <= 0) continue;
                ExecuteTurn(activePoke, map);
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
    public void ExecuteTurn(Pokemon active, Map map)
    {
        map.DrawMap(map);

        if (Players.Contains(active))
        {
            map.PlayerTurn(active, map, Enemies);
        }
        else
        {
            OnLogMessage?.Invoke($"{active.Name} (IA) réfléchit...");
            System.Threading.Thread.Sleep(500);
        }
    }

    private bool IsBattleOver()
    {
        bool allPlayersDead = Players.All(p => p.CurrentHP <= 0);
        bool allEnemiesDead = Enemies.All(p => p.CurrentHP <= 0);

        return allPlayersDead || allEnemiesDead;
    }
}