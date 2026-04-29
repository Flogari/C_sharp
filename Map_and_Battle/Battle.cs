public class Battle
{
    public List<Pokemon> Players;
    public List<Pokemon> Enemies;
    public Queue<Pokemon> TurnOrder;
    public bool IsBattleOver;

    public event Action<string> OnLogMessage;
    public event Action OnBattleEnd;

    public Battle(List<Pokemon> players, List<Pokemon> enemies)
    {
        Players = players;
        Enemies = enemies;
        TurnOrder = new Queue<Pokemon>();
        IsBattleOver = false;
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

    public void StartBattle()
    {
        OnLogMessage?.Invoke("Le combat commence !");

        while (IsBattleOver == false)
        {
            DetermineTurnOrder();

            while (TurnOrder.Count > 0 && IsBattleOver == false)
            {
                Pokemon activePoke = TurnOrder.Dequeue();
                if (activePoke.CurrentHP <= 0)
                {
                    continue;
                }
                ExecuteTurn(activePoke);
            }
        }
        OnLogMessage?.Invoke("Le combat est terminé !");
        OnBattleEnd?.Invoke();
    }

    public void ExecuteTurn(Pokemon active)
    {
        int moveDistance = active.Stats.TotalSpeed / 10;
    }
}