public class StatsContainer
{
    // Stats de base
    public int BaseMaxHP { get; set; }
    public int BaseAttack { get; set; }
    public int BaseDefense { get; set; }
    public int BaseSpecialAttack { get; set; }
    public int BaseSpecialDefense { get; set; }
    public int BaseSpeed { get; set; }

    // Bonus
    public int BonusHP { get; set; }
    public int BonusAttack { get; set; }
    public int BonusDefense { get; set; }
    public int BonusSpecialAttack { get; set; }
    public int BonusSpecialDefense { get; set; }
    public int BonusSpeed { get; set; }

    // Propriétés calculées
    public int TotalMaxHP => BaseMaxHP + BonusHP;
    public int TotalAttack => BaseAttack + BonusAttack;
    public int TotalDefense => BaseDefense + BonusDefense;
    public int TotalSpecialAttack => BaseSpecialAttack + BonusSpecialAttack;
    public int TotalSpecialDefense => BaseSpecialDefense + BonusSpecialDefense;
    public int TotalSpeed => BaseSpeed + BonusSpeed;

    public StatsContainer(int hp, int atk, int def, int speatk, int spedef, int spd)
    {
        BaseMaxHP = hp;
        BaseAttack = atk;
        BaseDefense = def;
        BaseSpecialAttack = speatk;
        BaseSpecialDefense = spedef;
        BaseSpeed = spd;
    }

    public void ResetBonuses()
    {
        BonusAttack = 0;
        BonusDefense = 0;
        BonusSpeed = 0;
    }
}