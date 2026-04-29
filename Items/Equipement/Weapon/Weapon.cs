public class Weapon : Equipment
{
    public int AttackBonus { get; set;}

    public Weapon(string name, int atkBonus) : base(name, $"+{atkBonus} Attaque")
    {
        AttackBonus = atkBonus;
    }

    public override void ApplyStats(Pokemon owner)
    {
        owner.Stats.BonusAttack = owner.Stats.BonusAttack + AttackBonus;
        owner.Stats.BonusSpecialAttack = owner.Stats.BonusSpecialAttack + AttackBonus;
    }
    public override void RemoveStats(Pokemon owner)
    {
        owner.Stats.BonusAttack = owner.Stats.BonusAttack - AttackBonus;
        owner.Stats.BonusSpecialAttack = owner.Stats.BonusSpecialAttack - AttackBonus;
    }
}