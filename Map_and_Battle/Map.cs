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
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return null;
        }

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
    public void MovePokemon(Pokemon p, int targetX, int targetY, Map map)
    {
        Cell currentCell = map.GetPokemonCell(p);
        Cell targetCell = map.GetCell(targetX, targetY);

        if (currentCell == null)
        {
            throw new InvalidMoveException("Erreur critique : Le Pokémon n'est plus sur la carte !");
        }

        if (targetCell == null)
        {
            throw new InvalidMoveException("Coordonnées hors de la carte !");

        }

        if (!targetCell.IsEmpty() && targetCell != currentCell)
        {
            throw new InvalidMoveException("Cette case est déjà occupée ou impraticable.");
        }

        int distance = map.GetDistance(currentCell, targetCell);
        int maxMove = p.Stats.TotalSpeed / 10;

        if (distance > maxMove)
        {
            throw new InvalidMoveException($"Trop loin ! Ce Pokémon peut bouger de {maxMove} cases max.");
        }

        currentCell.Occupant = null;
        targetCell.Occupant = p;
    }

    public void PlayerTurn(Pokemon activePoke, Map map, List<Pokemon> enemies)
    {
        bool moveSuccessful = false;

        while (!moveSuccessful) // MOVE
        {
            try
            {
                Console.WriteLine($"\nTour de {activePoke.Name}");
                Console.Write("Entrez X pour le déplacement : ");
                int x = int.Parse(Console.ReadLine());

                Console.Write("Entrez Y pour le déplacement : ");
                int y = int.Parse(Console.ReadLine());

                MovePokemon(activePoke, x, y, map);
                moveSuccessful = true;
            }
            catch (InvalidMoveException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erreur : {ex.Message}");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.WriteLine("Veuillez entrer des chiffres valides !");
            }
        }

        Console.WriteLine("\nRecherche de cibles à portée..."); //PROPOSITION ATTAQUE

        var validActions = new List<(Move move, Pokemon target)>();

        foreach (var move in activePoke.Moves)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.CurrentHP > 0 && IsTargetInRange(activePoke, enemy, move, map))
                {
                    validActions.Add((move, enemy));
                }
            }
        }
        if (validActions.Count == 0)
        {
            Console.WriteLine("Aucune cible à portée. Fin du tour.");
            return;
        }

        Console.WriteLine("Capacités disponibles :"); //CHOIX USER

        for (int i = 0; i < validActions.Count; i++)
        {
            var action = validActions[i];
            Console.WriteLine($"{i} : {action.move.Name} sur {action.target.Name}");
        }

        Console.Write("Choisissez une action (ou 's' pour passer) : ");
        string choice = Console.ReadLine();

        if (choice.ToLower() != "s")
        {
            int index = int.Parse(choice);
            var selected = validActions[index];
            selected.move.Execute(activePoke, selected.target);


            if (selected.target.CurrentHP <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{selected.target.Name} est K.O");
                Console.ResetColor();

                Cell deadCell = map.GetPokemonCell(selected.target);
                if (deadCell != null)
                {
                    deadCell.Occupant = null;
                }
            }
        }

    }

    public void DrawMap(Map map)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("   ");
        for (int x = 0; x < map.Width; x++)
        {
            Console.Write($"{x.ToString().PadLeft(2)} ");
        }
        Console.WriteLine();
        Console.ResetColor();

        for (int y = 0; y < map.Height; y++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{y.ToString().PadLeft(2)} ");
            Console.ResetColor();

            for (int x = 0; x < map.Width; x++)
            {
                var cell = map.GetCell(x, y);

                if (cell.Occupant == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" . ");
                    Console.ResetColor();
                }
                else if (cell.Occupant is Pokemon p)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" {p.Name[0]} ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }
}