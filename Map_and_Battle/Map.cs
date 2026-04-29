public class Map
{
    private Cell[,] Grid;
    public int Width { get; set; }
    public int Height { get; set; }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Grid = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Grid[x, y] = new Cell(x, y);
            }
        }
    }
    public int GetDistance(Cell start, Cell end)
    {
        return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
    }
    public Cell GetPokemonCell(Pokemon p)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (Grid[x, y].Occupant == p)
                {
                    return Grid[x, y];
                }
            }
        }
        return null;
    }
    public Cell GetCell(int x, int y)
    {
        return Grid[x, y];
    }
    public bool IsTargetInRange(Pokemon attacker, Pokemon target, Move move, Map map)
    {
        Cell attackerCell = map.GetPokemonCell(attacker);
        Cell targetCell = map.GetPokemonCell(target);

        if (attackerCell == null || targetCell == null)
        {
            return false;
        }

        int distance = map.GetDistance(attackerCell, targetCell);

        if (distance >= 1 && distance <= move.Range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void TryMovePokemon(Pokemon p, int targetX, int targetY, Map map)
    {
        Cell startCell = map.GetPokemonCell(p);
        Cell endCell = map.GetCell(targetX, targetY);

        if (endCell == null || !endCell.IsEmpty())
        {
            Console.WriteLine("Action impossible : case occupée ou hors limite.");
            return;
        }

        int distance = map.GetDistance(startCell, endCell);
        int maxMove = p.Stats.TotalSpeed / 10;

        if (distance <= maxMove)
        {
            startCell.Occupant = null;
            endCell.Occupant = p;
            Console.WriteLine($"{p.Name} s'est déplacé en {targetX},{targetY}.");
        }
        else
        {
            Console.WriteLine("Trop loin pour ce Pokémon !");
        }
    }
}