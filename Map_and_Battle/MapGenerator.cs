using System;
using System.Collections.Generic;

public class MapGenerator
{
    public Random RNG = new Random();

    public Map Generate()
    {
        int width = RNG.Next(5, 7);
        int height = RNG.Next(5, 7);

        return new Map(width, height);
    }

    public void SpawnTeams(Map map, List<Pokemon> playerTeam, List<Pokemon> enemyTeam)
    {
        for (int i = 0; i < enemyTeam.Count; i++)
        {
            int x = (map.Width / (enemyTeam.Count + 1)) * (i + 1);
            map.GetCell(x, 0).Occupant = enemyTeam[i];
        }

        for (int i = 0; i < playerTeam.Count; i++)
        {
            int x = (map.Width / (playerTeam.Count + 1)) * (i + 1);
            map.GetCell(x, map.Height - 1).Occupant = playerTeam[i];
        }

        List<Pokemon> AllPokemon = new List<Pokemon>(playerTeam);
        foreach (Pokemon p in enemyTeam)
        {
            AllPokemon.Add(p);
        }
        foreach (var p in AllPokemon)
        {
            p.OnDeath += () =>
            {
                Cell cell = map.GetPokemonCell(p);
                if (cell != null)
                {
                    cell.Occupant = null;
                    Console.WriteLine($"[LOG] {p.Name} a été retiré de la carte.");
                }
            };
        }
        foreach (var p in enemyTeam)
        {
            p.OnHealthChanged += (current, max) =>
            {
                Console.WriteLine($"{p.Name} a maintenant {current}/{max} PV !");
            };
        }
    }
}