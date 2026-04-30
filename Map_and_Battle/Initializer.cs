public static class Initializer
{
    private static Random _rng = new Random();

    public static List<Pokemon> CreatePlayerTeam()
    {
        List<Pokemon> team = new List<Pokemon>();

        Pokemon p1 = new Pokemon("Bulbi", 120, 18, 25, 12, 20, 25, new GrassType());
        p1.Moves.Add(new PhysicalMove("Fouet Lianes", 12, 100, 1, new GrassType()));

        Pokemon p2 = new Pokemon("Sala", 90, 12, 15, 25, 15, 45, new FireType());
        p2.Moves.Add(new SpecialMove("Flammèche", 15, 100, 2, new FireType()));
        p2.Moves.Add(new PhysicalMove("Mach Punch", 15, 100, 2, new FightingType()));

        Pokemon p3 = new Pokemon("Cara", 110, 20, 22, 15, 22, 30, new WaterType());
        p3.Moves.Add(new PhysicalMove("Pistolet à O", 14, 100, 1, new WaterType()));

        Pokemon p4 = new Pokemon("Pika", 80, 10, 10, 22, 12, 60, new ElectricType());
        p4.Moves.Add(new SpecialMove("Éclair", 12, 100, 4, new ElectricType()));
        p4.Moves.Add(new SpecialMove("Telluriforce", 12, 100, 3, new GroundType()));

        team.Add(p1); team.Add(p2); team.Add(p3); team.Add(p4);
        return team;
    }
    public static List<Pokemon> GenerateEnemyTeam(int floor)
    {
        List<Pokemon> enemies = new List<Pokemon>();
        Random rng = new Random();

        int numberOfEnemies = Math.Min(2 + (floor / 2), 5);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            int monsterType = rng.Next(0, 8);
            Pokemon e = null;

            switch (monsterType)
            {
                case 0:
                    e = new Pokemon("Racaillou", 50 + (floor * 10), 15 + (floor * 2), 35 + (floor * 5), 5, 15, 10, new RockType());
                    e.Moves.Add(new PhysicalMove("Jet-Pierres", 12 + floor, 90, 2, new RockType()));
                    e.SetTalent(new LastResort());
                    break;

                case 1:
                    e = new Pokemon("Aspicot", 30 + (floor * 7), 18 + (floor * 3), 10 + (floor * 2), 10, 10, 45, new BugType());
                    e.Moves.Add(new PhysicalMove("Dard-Venin", 10 + floor, 100, 1, new PoisonType()));
                    e.SetTalent(new LastResort());
                    break;

                case 2:
                    e = new Pokemon("Taupiqueur", 35 + (floor * 8), 20 + (floor * 3), 15 + (floor * 2), 15, 15, 55, new GroundType());
                    e.Moves.Add(new SpecialMove("Ampleur", 14 + floor, 100, 2, new GroundType()));
                    e.SetTalent(new LastResort());
                    break;

                case 3:
                    e = new Pokemon("Rattata", 40 + (floor * 8), 22 + (floor * 4), 12 + (floor * 2), 10, 10, 50, new NormalType());
                    e.Moves.Add(new PhysicalMove("Vive-Attaque", 11 + floor, 100, 1, new NormalType()));
                    e.SetTalent(new LastResort());
                    break;

                case 4:
                    e = new Pokemon("Roucool", 35 + (floor * 7), 15 + (floor * 3), 15 + (floor * 2), 15, 15, 60, new FlyingType());
                    e.Moves.Add(new PhysicalMove("Tornade", 12 + floor, 100, 2, new FlyingType()));
                    e.SetTalent(new LastResort());
                    break;

                case 5:
                    e = new Pokemon("Miaouss", 45 + (floor * 9), 24 + (floor * 3), 14 + (floor * 2), 18, 14, 55, new NormalType());
                    e.Moves.Add(new PhysicalMove("Morsure", 13 + floor, 100, 1, new NormalType()));
                    e.SetTalent(new LastResort());
                    break;

                case 6:
                    e = new Pokemon("Fantominus", 25 + (floor * 5), 5, 5, 35 + (floor * 4), 25, 65, new GhostType());
                    e.Moves.Add(new SpecialMove("Ombre Nocturne", 18 + floor, 100, 3, new GhostType()));
                    e.SetTalent(new LastResort());
                    break;

                case 7:
                    e = new Pokemon("Smogo", 60 + (floor * 12), 15 + (floor * 2), 20 + (floor * 3), 20, 20, 20, new PoisonType());
                    e.Moves.Add(new SpecialMove("Purédpois", 12 + floor, 100, 2, new PoisonType()));
                    e.SetTalent(new LastResort());
                    break;
            }
            enemies.Add(e);
        }
        return enemies;
    }
}
