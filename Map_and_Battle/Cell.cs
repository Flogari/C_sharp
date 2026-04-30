public class Cell
{
    public int X { get; }
    public int Y { get; }
    public bool IsWalkable { get; set; }
    public Entity Occupant { get; set; }

    public Cell(int x, int y, bool isWalkable = true)
    {
        X = x;
        Y = y;
        IsWalkable = isWalkable;
    }

    public bool IsEmpty()
    {
        if (Occupant == null && IsWalkable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}