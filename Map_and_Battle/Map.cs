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

        IConsumable consumable = activePoke.Item as IConsumable;

        if (consumable != null)
        {
            Console.WriteLine($"P : Utiliser {activePoke.Item.Name} ({activePoke.Item.Description})");
        }

        Console.Write("Choisissez une action (ou 's' pour passer) : ");
        string choice = Console.ReadLine();

        if (choice == "p" && consumable != null)
        {
            consumable.Use(activePoke);
            Console.WriteLine($"{activePoke.Name} utilise {activePoke.Item.Name} !");
            activePoke.Item = null;
        }
        else if (choice.ToLower() != "s")
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
    public void EnemyTurn(Pokemon enemy, Map map, List<Pokemon> players)
    {
        bool AttackWasUsed = false;
        (Move move, Pokemon target, int damage) bestAction = (null, null, 0);

        foreach (var m in enemy.Moves)
        {
            foreach (var p in players.Where(p => p.CurrentHP > 0))
            {
                if (map.IsTargetInRange(enemy, p, m, map))
                {
                    int dmg = CalculateTheoreticalDamage(enemy, p, m);
                    if (dmg > bestAction.damage)
                        bestAction = (m, p, dmg);
                }
            }
        }

        if (bestAction.target != null)
        {
            Console.WriteLine($"{enemy.Name} lance {bestAction.move.Name} sur {bestAction.target.Name} !");
            bestAction.move.Execute(enemy, bestAction.target);
            AttackWasUsed = true;
        }
        else
        {
            MoveTowardsClosestPlayer(enemy, map, players);
        }
        if (AttackWasUsed == false)
        {
            foreach (var m in enemy.Moves)
            {
                foreach (var p in players.Where(p => p.CurrentHP > 0))
                {
                    if (map.IsTargetInRange(enemy, p, m, map))
                    {
                        int dmg = CalculateTheoreticalDamage(enemy, p, m);
                        if (dmg > bestAction.damage)
                            bestAction = (m, p, dmg);
                    }
                }
            }
            if (bestAction.target != null)
            {
                Console.WriteLine($"{enemy.Name} lance {bestAction.move.Name} sur {bestAction.target.Name} !");
                bestAction.move.Execute(enemy, bestAction.target);
            }
        }
    }

    private void MoveTowardsClosestPlayer(Pokemon enemy, Map map, List<Pokemon> players)
    {
        Cell enemyCell = map.GetPokemonCell(enemy);
        Pokemon closestPlayer = null;
        int minDistance = int.MaxValue;

        foreach (var p in players.Where(p => p.CurrentHP > 0))
        {
            Cell pCell = map.GetPokemonCell(p);
            int dist = map.GetDistance(enemyCell, pCell);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestPlayer = p;
            }
        }

        if (closestPlayer != null)
        {
            Cell targetCell = map.GetPokemonCell(closestPlayer);
            int maxSteps = Math.Max(1, enemy.Stats.TotalSpeed / 10);

            int nextX = enemyCell.X;
            int nextY = enemyCell.Y;

            for (int i = 0; i < maxSteps; i++)
            {
                int stepX = nextX;
                int stepY = nextY;

                if (nextX < targetCell.X)
                {
                    stepX++;
                }
                else if (nextX > targetCell.X)
                {
                    stepX--;
                }

                else if (nextY < targetCell.Y)
                {
                    stepY++;
                }
                else if (nextY > targetCell.Y)
                {
                    stepY--;
                }

                if (map.GetCell(stepX, stepY)?.IsEmpty() == true)
                {
                    nextX = stepX;
                    nextY = stepY;
                }
                else
                {
                    break;
                }
            }

            if (nextX != enemyCell.X || nextY != enemyCell.Y)
            {
                map.MovePokemon(enemy, nextX, nextY, map);
                Console.WriteLine($"{enemy.Name} s'approche de {closestPlayer.Name} (Position: {nextX},{nextY})");
            }
        }
    }
    public int CalculateTheoreticalDamage(Pokemon attacker, Pokemon target, Move move)
    {
        int baseDamage = 0;
        float stab = 1.0f;
        if (move is PhysicalMove)
        {
            baseDamage = (attacker.Stats.TotalAttack + move.Power) - (target.Stats.TotalDefense / 2);
        }
        else
        {
            baseDamage = (attacker.Stats.TotalSpecialAttack + move.Power) - (target.Stats.TotalSpecialDefense / 2);
        }

        if (attacker.CurrentWeapon != null)
        {
            baseDamage += (int)(attacker.CurrentWeapon.AttackBonus);
        }
        if (target.CurrentArmor != null)
        {
            baseDamage -= (int)(target.CurrentArmor.DefenseBonus);
        }

        if (attacker.PokeType.Element == move.MoveType.Element)
        {
            stab = 1.5f;
        }

        float multiplier = move.MoveType.GetMultiplier(target.PokeType.Element);

        return (int)(Math.Max(1, baseDamage) * multiplier * stab);
    }

    public void DrawMap(Map map, List<Pokemon> turnOrder, Pokemon currentActing)
    {
        Console.WriteLine("--- CARTE ---");

        Console.Write("   ");
        for (int x = 0; x < map.Width; x++)
        {
            Console.Write($"{x.ToString().PadLeft(2)} ");
        }
        Console.WriteLine("      ORDRE DES TOURS");

        int rowsToDraw = Math.Max(map.Height, turnOrder.Count);

        for (int y = 0; y < rowsToDraw; y++)
        {
            if (y < map.Height)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"{y.ToString().PadLeft(2)} ");
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
            }
            else
            {
                Console.Write("".PadRight((map.Width * 2) + 3));
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  |  ");

            if (y < turnOrder.Count)
            {
                Pokemon p = turnOrder[y];
                bool isCurrent = (p == currentActing);

                if (isCurrent)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("  => ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("     ");
                }

                string status = p.CurrentHP <= 0 ? "[K.O.]" : $"{p.CurrentHP}/{p.Stats.TotalMaxHP} HP";

                Console.Write($"{p.Name.PadRight(10)} {status.PadRight(12)} ATK:{p.Stats.TotalAttack}, DEF:{p.Stats.TotalDefense}, SPEATK:{p.Stats.TotalSpecialAttack}, SPEDEF:{p.Stats.TotalSpecialDefense}, SPE:{p.Stats.TotalSpeed}");
            }

            Console.WriteLine();
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}