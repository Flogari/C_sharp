public class StatsChange : GameEvent
{

    public StatsChange() : base("Mutation", $"Votre pokémon mute, ses stats changent !")
    {

    }
    public override void Resolve(Pokemon playerPokemon)
    {
        Console.WriteLine($"[EVENT] {Title}");
        Console.WriteLine(Description);

        Random random = new Random();

        int GoodRNG = random.Next(1, 10);
        int BadRNG = random.Next(0, 5);

        if (GoodRNG < BadRNG)
        {
            GoodRNG = random.Next(1, 11);
            BadRNG = random.Next(0, 5);
        }

        int UpStat = random.Next(1, 6);
        int DownStat = random.Next(1, 6);

        switch (UpStat)
        {
            case 1:
                playerPokemon.Stats.BaseMaxHP += GoodRNG;
                break;
            case 2:
                playerPokemon.Stats.BaseAttack += GoodRNG;
                break;
            case 3:
                playerPokemon.Stats.BaseDefense += GoodRNG;
                break;
            case 4:
                playerPokemon.Stats.BaseSpecialAttack += GoodRNG;
                break;
            case 5:
                playerPokemon.Stats.BaseSpecialDefense += GoodRNG;
                break;
            case 6:
                playerPokemon.Stats.BaseSpeed += GoodRNG;
                break;
            default:
                break;
        }

        switch (DownStat)
        {
            case 1:
                playerPokemon.Stats.BaseMaxHP += BadRNG;
                break;
            case 2:
                playerPokemon.Stats.BaseAttack += BadRNG;
                break;
            case 3:
                playerPokemon.Stats.BaseDefense += BadRNG;
                break;
            case 4:
                playerPokemon.Stats.BaseSpecialAttack += BadRNG;
                break;
            case 5:
                playerPokemon.Stats.BaseSpecialDefense += BadRNG;
                break;
            case 6:
                playerPokemon.Stats.BaseSpeed += BadRNG;
                break;
            default:
                break;
        }
        List<string> stats = new List<string>();
        stats.Add("HP");
        stats.Add("ATK");
        stats.Add("DEF");
        stats.Add("SPEATK");
        stats.Add("SPEDEF");
        stats.Add("SPEED");
        Console.WriteLine($"{playerPokemon.Name} a muté il gagne +{GoodRNG} en {stats[UpStat+1]} et perd -{BadRNG} en {stats[DownStat+1]} ! ");
    }
}