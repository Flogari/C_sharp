public class HealEvent : GameEvent
{
    public HealEvent() : base("Centre de Soin", "Un moment de repos pour reprendre des forces.")
    {

    }

    public override void Resolve(Pokemon playerPokemon)
    {
        Console.WriteLine($"[EVENT] {Title}");
        int healAmount = (int)(playerPokemon.Stats.TotalMaxHP * 0.5f);
        playerPokemon.Heal(healAmount);
        Console.WriteLine($"{playerPokemon.Name} a récupéré {healAmount} PV.");
    }
}