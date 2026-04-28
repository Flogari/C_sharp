using System;
using System.Collections.Generic;

public class Pokemon : Entity
{
    // Stats et État
    public StatsContainer Stats { get; private set; }
    public int CurrentHP { get; private set; }
    public int Level { get; private set; }
    public PokemonType PokeType { get; private set; }
    public Abilities PokeAbilitie { get; private set; }

    // Cet événement préviendra l'UI quand les PV changent
    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    // public List<Move> Moves { get; private set; }

    public Pokemon(string name, int hp, int atk, int def, int speatk, int spedef, int spd, PokemonType poketype) : base(name)
    {
        Stats = new StatsContainer(hp, atk, def, speatk, spedef, spd);
        CurrentHP = Stats.BaseMaxHP;
        Level = 1;
        PokeType = poketype;
        // Moves = new List<Move>();
    }

    // Méthode pour subir des dégâts
    public void TakeDamage(int damage)
    {
        int finalDamage = Math.Max(1, damage - (Stats.TotalDefense / 2));
        CurrentHP = CurrentHP - finalDamage;

        if (CurrentHP < 0)
        {
            CurrentHP = 0;
        }

        // On déclenche l'événement pour l'UI
        OnHealthChanged?.Invoke(CurrentHP, Stats.TotalMaxHP);

        if (CurrentHP <= 0)
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