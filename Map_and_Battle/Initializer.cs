public static class Initializer
{
    private static Random _rng = new Random();

    // Crée l'équipe du joueur au début de la partie
    public static List<Pokemon> CreatePlayerTeam()
    {
        List<Pokemon> team = new List<Pokemon>();

        Pokemon p1 = new Pokemon("Bulbi", 100, 15, 15, 15, 10, 30, new GrassType());
        p1.Moves.Add(new PhysicalMove("Charge", 10, 100, 1, new NormalType()));

        Pokemon p2 = new Pokemon("Sala", 100, 15, 15, 15, 10, 30, new FireType());
        p2.Moves.Add(new SpecialMove("Flammèche", 10, 100, 2, new FireType()));

        Pokemon p3 = new Pokemon("Cara", 100, 15, 15, 15, 10, 30, new WaterType());
        p3.Moves.Add(new PhysicalMove("Baff'do", 10, 100, 1, new WaterType()));

        Pokemon p4 = new Pokemon("Pika", 100, 15, 15, 15, 10, 30, new ElectricType());
        p4.Moves.Add(new SpecialMove("Eclair", 10, 100, 3, new ElectricType()));

        team.Add(p1); team.Add(p2); team.Add(p3); team.Add(p4);

        return team;
    }
    public static List<Pokemon> GenerateEnemyTeam(int floor)
    {
        List<Pokemon> enemies = new List<Pokemon>();
        string[] names = { "Rata", "Nosfé", "Abo", "Smogo" };
        PokemonType[] type = { new NormalType(), new PoisonType(), new PoisonType(), new PoisonType() };

        for (int i = 0; i < 2; i++)
        {
            int hp = 10 + (floor * 3);
            int atk = 10 + (floor * 2);
            int def = 5 + (floor * 1);
            int speatk = 10 + (floor * 2);
            int spedef = 5 + (floor * 1);
            int spd = 15 + (floor * 2);

            Pokemon e = new Pokemon(names[i], hp, atk, def, speatk, spedef, spd, type[i]);
            e.Moves.Add(new PhysicalMove("Gaz poison", 8 + floor, 100, 1, new PoisonType()));
            enemies.Add(e);
        }

        return enemies;
    }
}