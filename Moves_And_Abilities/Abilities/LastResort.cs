public class LastResort : Abilities
{
    public LastResort()
    {
        Name = "Dernier Recourt";
        Description = "Augmente l'Attaque quand les PV sont sous 30%.";
    }

    public override void SubscribeEvents()
    {
        Owner.OnHealthChanged += CheckHealth;
    }

    private void CheckHealth(int currentHP, int maxHP)
    {
        if (currentHP < maxHP * 0.3f)
        {
            Owner.Stats.BonusAttack += (int)(Owner.Stats.BaseAttack * 1.2 + 1);
            Console.WriteLine($"[TALENT] {Name} s'active ! L'attaque de {Owner.Name} augmente !");
            Owner.OnHealthChanged -= CheckHealth;
        }
    }
}