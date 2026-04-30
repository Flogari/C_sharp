using System;
using System.Collections.Generic;

public class Pokemon : Entity
{
    public StatsContainer Stats { get; set; }
    public int CurrentHP { get; set; }
    public int Level { get; set; }
    public PokemonType PokeType { get; set; }
    public Abilities PokeAbilitie { get; set; }
    public Weapon CurrentWeapon { get; set; }
    public Armor CurrentArmor { get; set; }
    public Item Item { get; set; }
    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;
    public List<Move> Moves { get; set; }

    public Pokemon(string name, int hp, int atk, int def, int speatk,
    int spedef,int spd, PokemonType poketype) : base(name)
    {
        Stats = new StatsContainer(hp, atk, def, speatk, spedef, spd);
        CurrentHP = Stats.BaseMaxHP;
        Level = 1;
        PokeType = poketype;
        Moves = new List<Move>();
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Math.Max(1, damage);
        this.CurrentHP = this.CurrentHP - finalDamage;
        if (this.CurrentHP < 0)
        {
            this.CurrentHP = 0;
        }

        OnHealthChanged?.Invoke(this.CurrentHP, this.Stats.TotalMaxHP);

        if (this.CurrentHP <= 0)
        {
            OnDeath?.Invoke();
        }
    }
    public void Heal(int amount)
    {
        CurrentHP = CurrentHP + amount;
        if (CurrentHP > Stats.TotalMaxHP)
        {
            CurrentHP = Stats.TotalMaxHP;
        }
        OnHealthChanged?.Invoke(CurrentHP, Stats.TotalMaxHP);
    }

    public void SetTalent(Abilities newTalent)
    {
        this.PokeAbilitie = newTalent;
        this.PokeAbilitie.Initialize(this);
    }
}