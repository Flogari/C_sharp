public class Armor : Equipment
{
    public int DefenseBonus { get; }

    public Armor(string name, int defBonus) : base(name, $"+{defBonus} Défense")
    {
        DefenseBonus = defBonus;
    }

    public override void ApplyStats(Pokemon owner)
    {
        owner.Stats.BonusDefense = owner.Stats.BonusDefense + DefenseBonus;
        owner.Stats.BonusSpecialDefense = owner.Stats.BonusSpecialDefense + DefenseBonus;
    }
    public override void RemoveStats(Pokemon owner)
    {
        owner.Stats.BonusDefense = owner.Stats.BonusDefense - DefenseBonus;
        owner.Stats.BonusSpecialDefense = owner.Stats.BonusSpecialDefense - DefenseBonus;
    }
}